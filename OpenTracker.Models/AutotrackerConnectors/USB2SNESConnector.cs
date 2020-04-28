using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace OpenTracker.Models.AutotrackerConnectors
{
    public class USB2SNESConnector : INotifyPropertyChanging, INotifyPropertyChanged, IDisposable
    {
        private readonly Action<string> _messageHandler;
        private bool _shutdown;
        private volatile bool _open;
        private readonly ManualResetEvent _memoryReadEvent = new ManualResetEvent(false);
        private volatile Task _keepAliveTask;
        private readonly object _transmitLock = new object();

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<(ConnectionStatus, string)> ConnectionStatusChanged;

        private WebSocket _socket;
        public WebSocket Socket
        {
            get => _socket;
            set
            {
                if (_socket != value)
                {
                    if (_socket != null)
                    {
                        _socket.OnMessage -= Socket_OnMessageReceived;
                        _socket.OnOpen -= Socket_OnOpen;
                        _socket.OnClose -= Socket_OnClose;
                    }

                    OnPropertyChanging(nameof(Socket));
                    _socket = value;
                    OnPropertyChanged(nameof(Socket));

                    if (_socket != null)
                    {
                        _socket.OnMessage += Socket_OnMessageReceived;
                        _socket.OnOpen += Socket_OnOpen;
                        _socket.OnClose += Socket_OnClose;
                    }
                }
            }
        }

        public bool Connected => _socket != null && _socket.IsAlive;

        public Action<MessageEventArgs> PendingMessageHandler { get; private set; }
        public string Usb2SnesApplicationName { get; set; } = "OpenTracker";

        public USB2SNESConnector(Action<string> messageHandler = null)
        {
            _messageHandler = messageHandler;
            ConnectIfNecessary();
        }

        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Socket_OnMessageReceived(object sender, MessageEventArgs e)
        {
            PendingMessageHandler?.Invoke(e);

            PendingMessageHandler = null;
        }

        private void Socket_OnOpen(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Connected));
        }

        private void Socket_OnClose(object sender, CloseEventArgs e)
        {
            OnPropertyChanged(nameof(Connected));
        }

        public void Dispose()
        {
            Disconnect(true);
        }

        private void Disconnect(bool shutdown = false)
        {
            Socket?.Close();
            Socket = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (shutdown)
            {
                _shutdown = true;
                _open = false;
            }

            ConnectionStatusChanged?.Invoke(this, (ConnectionStatus.Closed, "Disconnected from usb2snes websocket."));
        }

        private void Output(string message, params object[] tokens)
        {
            _messageHandler?.Invoke(string.Format(message, tokens));
        }

        private void KeepAlive()
        {
            ReadByte(8257536U);
        }

        public byte? ReadByte(ulong address)
        {
            Output(string.Format("USB2SNESConnector is reading a byte: [${0:X6}]", address));
            byte[] buffer = new byte[1];

            if (Read(address, buffer))
                return buffer[0];

            Output(string.Format("USB2SNESConnector failed to read a byte: [${0:X6}]", address));

            return null;
        }

        public bool ReadByte(ulong address, out byte value)
        {
            Output(string.Format("USB2SNESConnector is reading a byte: [${0:X6}]", address));

            byte[] buffer = new byte[1];

            if (Read(address, buffer))
            {
                value = buffer[0];
                return true;
            }

            Output(string.Format("USB2SNESConnector failed to read a byte: [${0:X6}]", address));
            value = (byte)0;

            return false;
        }

        public bool Read(ulong address, byte[] buffer)
        {
            bool success = false;
            while (!success)
            {
                ConnectIfNecessary();

                if (!Connected)
                    return false;

                try
                {
                    using (ManualResetEvent readEvent = new ManualResetEvent(false))
                    {
                        lock (_transmitLock)
                        {
                            RequestType requestType = new RequestType()
                            {
                                Opcode = OpcodeType.GetAddress.ToString(),
                                Space = "SNES",
                                Operands = new List<string>()
                                {
                                    TranslateAddress((uint)address, TranslationMode.Read).ToString("X"),
                                    buffer.Length.ToString("X")
                                }
                            };

                            PendingMessageHandler = e =>
                            {
                                try
                                {
                                    if (!e.IsBinary || e.RawData == null)
                                        return;

                                    for (int i = 0; i < buffer.Length; ++i)
                                        buffer[i] = e.RawData[Math.Min(i, e.RawData.Length - 1)];

                                    success = true;
                                }
                                catch { }
                                finally { readEvent.Set(); }
                            };

                            if (Socket == null || !Socket.IsAlive)
                            {
                                Output("ERROR: Connection to USB2SNES lost.");

                                ConnectionStatusChanged?.Invoke(this, (ConnectionStatus.Closed,
                                    "Connection to USB2SNES lost."));
                            }
                            else
                                Socket?.Send(JsonConvert.SerializeObject(requestType));

                            try
                            {
                                if (!readEvent.WaitOne(4096))
                                {
                                    Disconnect(false);
                                    return false;
                                }
                            }
                            catch
                            {
                                Disconnect(false);
                                return false;
                            }
                            finally { readEvent.Reset(); }
                        }
                    }
                }
                catch { return false; }
            }

            return success;
        }

        public static uint TranslateAddress(uint address, TranslationMode mode)
        {
            uint mappedAddress;
            if (mode == TranslationMode.Read && MapAddressInRange(address, 8257536U, 8388607U, 16056320U, out mappedAddress))
                return mappedAddress;

            for (uint index = 0; index < 63U; ++index)
            {
                if (MapAddressInRange(address, (uint)((int)index * 65536 + 32768), (uint)((int)index * 65536 + ushort.MaxValue),
                    index * 32768U, out mappedAddress) ||
                    MapAddressInRange(address, (uint)((int)index * 65536 + 8421376), (uint)((int)index * 65536 + 8454143),
                    index * 32768U, out mappedAddress))
                    return mappedAddress;
            }

            for (uint index = 0; index < 8U; ++index)
            {
                if (MapAddressInRange(address, (uint)(7340032 + (int)index * 65536), (uint)(7372799 + (int)index * 65536),
                    (uint)(14680064 + (int)index * 32768), out mappedAddress))
                    return mappedAddress;
            }

            return address;
        }
        
        public static bool MapAddressInRange(uint address, uint srcRangeBegin, uint srcRangeEnd, uint dstRangeBegin,
            out uint mappedAddress)
        {
            if (address >= srcRangeBegin && address <= srcRangeEnd)
            {
                mappedAddress = address - srcRangeBegin + dstRangeBegin;
                return true;
            }

            mappedAddress = 0U;

            return false;
        }

        private bool? ConnectIfNecessary()
        {
            if (Connected || _shutdown)
                return null;

            Disconnect();

            Socket = new WebSocket("ws://localhost:8080");
            Socket.Log.Output = (data, message) =>
            {
                if (!string.IsNullOrWhiteSpace(data.Message))
                    Output(data.Message);
            };

            Socket.Connect();
            Thread.Sleep(1000);

            if (Socket.IsAlive)
            {
                Output("Connected to WebSocket");
                string port = null;

                PendingMessageHandler = e =>
                {
                    try
                    {
                        if (JsonConvert.DeserializeObject<Dictionary<string, string[]>>(e.Data) is Dictionary<string, string[]> dictionary &&
                            dictionary.TryGetValue("Results", out string[] results))
                        {
                            foreach (string result in (results as IEnumerable<string>) ?? new string[0])
                            {
                                port = result as string;

                                if (!string.IsNullOrWhiteSpace(port))
                                    break;
                            }
                        }
                    }
                    catch { }
                    finally { _memoryReadEvent.Set(); }
                };

                Socket.Send(JsonConvert.SerializeObject(new RequestType()
                {
                    Opcode = OpcodeType.DeviceList.ToString(),
                    Space = "SNES"
                }));

                _memoryReadEvent.WaitOne();
                _memoryReadEvent.Reset();

                if (!string.IsNullOrWhiteSpace(port))
                {
                    Output("Connected to SNES via {0}", port);

                    Socket.Send(JsonConvert.SerializeObject(new RequestType()
                    {
                        Opcode = OpcodeType.Attach.ToString(),
                        Space = "SNES",
                        Operands = new List<string>() { port }
                    }));

                    Socket.Send(JsonConvert.SerializeObject(new RequestType()
                    {
                        Opcode = OpcodeType.Name.ToString(),
                        Space = "SNES",
                        Operands = new List<string>() { Usb2SnesApplicationName }
                    }));

                    ConnectionStatusChanged?.Invoke(this, (ConnectionStatus.Open,
                        "Connection to USB2SNES socket service established."));

                    _open = true;

                    if (_keepAliveTask == null)
                    {
                        _keepAliveTask = Task.Factory.StartNew(() =>
                        {
                            while (_open)
                            {
                                try { KeepAlive(); }
                                catch { }

                                Thread.Sleep(1500);
                            }

                            _messageHandler?.Invoke("USB2SNESConnector keepalive is quitting.");
                        }, TaskCreationOptions.LongRunning);
                    }
                    return true;
                }

                Output("ERROR: No SNES was detected by the system.");
                
                ConnectionStatusChanged?.Invoke(this, (ConnectionStatus.Closed,
                    "No SNES was detected by the system."));
                
                return false;
            }

            Output("ERROR: Failed to connect to web socket; the USB2SNES websocket application may not be running.");

            ConnectionStatusChanged?.Invoke(this, (ConnectionStatus.Closed,
                "Failed to connect to websocket; the USB2SNES websocket application may not be running."));
            
            return false;
        }
    }
}
