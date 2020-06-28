using Newtonsoft.Json;
using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenTracker.Models.SNESConnectors
{
    public class USB2SNESConnector : INotifyPropertyChanged, ISNESConnector
    {
        private readonly object _transmitLock = new object();
        private readonly Action<LogLevel, string> _logHandler;

        private bool Connected =>
            Socket != null && Socket.State == WebSocketState.Open;

        public Uri Uri { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ClientWebSocket _socket = new ClientWebSocket();
        public ClientWebSocket Socket
        {
            get => _socket;
            private set
            {
                if (_socket != value)
                {
                    _socket = value;
                    OnPropertyChanged(nameof(Socket));
                    Status = ConnectionStatus.NotConnected;
                }
            }
        }

        private string _device = "";
        public string Device
        {
            get => _device;
            set
            {
                if (_device != value)
                {
                    _device = value;
                    OnPropertyChanged(nameof(Device));
                }
            }
        }

        private ConnectionStatus _status;
        public ConnectionStatus Status
        {
            get => _status;
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public USB2SNESConnector(Action<LogLevel, string> logHandler)
        {
            _logHandler = logHandler ??
                throw new ArgumentNullException(nameof(logHandler));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this, new PropertyChangedEventArgs(propertyName));
        }

        private void Log(LogLevel logLevel, string message)
        {
            _logHandler(logLevel, message);
        }

        private bool Connect(int timeOutInMS = 1000)
        {
            Log(LogLevel.Info, "Attempting to connect to USB2SNES.");
            Status = ConnectionStatus.Connecting;
            using CancellationTokenSource cts = new CancellationTokenSource(timeOutInMS);
            Task task = Socket.ConnectAsync(Uri, cts.Token);

            try
            {
                Task.WaitAll(new Task[] { task });
            }
            catch (AggregateException)
            {
                Log(LogLevel.Info, "Attempt to connect timed out.");
                Status = ConnectionStatus.Error;
                return false;
            }

            if (Socket.State == WebSocketState.Open)
            {
                Log(LogLevel.Info, "Successfully connected to USB2SNES.");
                Status = ConnectionStatus.SelectDevice;
                return true;
            }

            Log(LogLevel.Info, "Failed to connect to USB2SNES for unknown reason.");
            Status = ConnectionStatus.Error;
            return false;
        }

        private void Disconnect(int timeOutInMS = 1000)
        {
            Log(LogLevel.Debug, "Attempting to disconnect connection.");
            using CancellationTokenSource cts = new CancellationTokenSource(timeOutInMS);
            Task task = Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cts.Token);

            try
            {
                Task.WaitAll(new Task[] { task });
            }
            catch (AggregateException)
            {
                Log(LogLevel.Debug, "Attempt to disconnect connection timed out.");
            }

            Status = ConnectionStatus.NotConnected;
        }

        private void DisconnectAndDispose(int timeOutInMS = 1000)
        {
            if (Socket.State != WebSocketState.None && Socket.State != WebSocketState.Closed)
            {
                Disconnect(timeOutInMS);
            }

            Socket.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private bool ConnectIfNeeded(int timeOutInMS = 1000, int retries = 3)
        {
            if (Connected)
            {
                return true;
            }

            bool connected;
            int i = 0;

            do
            {
                if (Socket.State != WebSocketState.None)
                {
                    Log(LogLevel.Debug, "Disposing of existing connection attempt.");
                    DisconnectAndDispose(timeOutInMS);
                    Socket = new ClientWebSocket();
                }

                connected = Connect(timeOutInMS);
                i++;
            } while (i < retries && !connected);

            return connected;
        }

        private async Task<bool> Send(RequestType requestType, int timeOutInMS = 1000)
        {
            if (requestType == null)
            {
                throw new ArgumentNullException(nameof(requestType));
            }

            using CancellationTokenSource cts = new CancellationTokenSource(timeOutInMS);

            try
            {
                await Socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(requestType))),
                    WebSocketMessageType.Text, true, cts.Token).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                Log(LogLevel.Warn, "Failed to send data.");
                return false;
            }

            return true;
        }

        private async Task<string> Receive(int timeOutInMS = 1000)
        {
            var buffer = new ArraySegment<byte>(new byte[2048]);
            using CancellationTokenSource cts = new CancellationTokenSource(timeOutInMS);

            using var ms = new MemoryStream();
            WebSocketReceiveResult result;

            try
            {
                do
                {
                    result = await Socket.ReceiveAsync(buffer, cts.Token).ConfigureAwait(false);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                } while (result == null || !result.EndOfMessage);
            }
            catch (TaskCanceledException)
            {
                return null;
            }

            ms.Seek(0, SeekOrigin.Begin);

            using var reader = new StreamReader(ms, Encoding.UTF8);

            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        private bool SendOnly(RequestType requestType, int timeOutInMS = 1000, int retries = 3)
        {
            if (!ConnectIfNeeded(timeOutInMS, retries))
            {
                return false;
            }
            lock (_transmitLock)
            {
                Task sendTask = Send(requestType, timeOutInMS);

                try
                {
                    Task.WaitAll(new Task[] { sendTask });
                }
                catch (AggregateException)
                {
                    return false;
                }
            }

            return true;
        }

        private string SendAndReceive(RequestType requestType, int timeOutInMS = 1000, int retries = 3)
        {
            if (!ConnectIfNeeded(timeOutInMS, retries))
            {
                return null;
            }

            Task<string> receiveTask;

            lock (_transmitLock)
            {
                Task sendTask = Send(requestType, timeOutInMS);
                receiveTask = Receive(timeOutInMS);

                try
                {
                    Task.WaitAll(new Task[] { sendTask, receiveTask });
                }
                catch (AggregateException)
                {
                    Status = ConnectionStatus.Error;
                    return null;
                }
            }

            return receiveTask.Result;
        }

        private string GetInfo(int timeOutInMS = 1000, int retries = 3)
        {
            return SendAndReceive(new RequestType(OpcodeType.Info.ToString()),
                timeOutInMS, retries);
        }

        public IEnumerable<string> GetDeviceList(int timeOutInMS = 1000, int retries = 3)
        {
            Log(LogLevel.Info, "Requesting device list from USB2SNES.");

            string deviceList = SendAndReceive(new RequestType(OpcodeType.DeviceList.ToString()),
                timeOutInMS, retries);

            if (deviceList == null)
            {
                Log(LogLevel.Error, "Failed to receive device list from USB2SNES.");
                return null;
            }

            return JsonConvert.DeserializeObject<Dictionary<string, string[]>>(deviceList)
                ["Results"];
        }

        public void AttachDevice(int timeOutInMS = 1000, int retries = 3)
        {
            Log(LogLevel.Info, $"Attempting to attach to device: {Device}");
            Status = ConnectionStatus.Attaching;

            if (!SendOnly(new RequestType(OpcodeType.Attach.ToString(),
                operands: new List<string>(1) { Device }), timeOutInMS, retries))
            {
                Log(LogLevel.Error, "Unable to send attach command.");
                Status = ConnectionStatus.Error;
            }

            if (GetInfo(timeOutInMS, retries) == null || Socket.State != WebSocketState.Open)
            {
                Log(LogLevel.Error, $"Failed to attach to device: { Device }");
                Status = ConnectionStatus.Error;
                return;
            }

            Log(LogLevel.Info, $"Successfully attached to device: {Device}");
            Status = ConnectionStatus.Connected;
        }

        public bool Read(ulong address, out byte value)
        {
            Log(LogLevel.Debug, $"Reading byte at address {address:X6}.");

            byte[] buffer = new byte[1];

            if (Read(address, buffer))
            {
                value = buffer[0];
                Log(LogLevel.Debug, $"Read value {value} at address {address:X6}.");
                return true;
            }

            Log(LogLevel.Error, $"Failed to read value at {address:X6}.");
            value = 0;
            return false;
        }

        public bool Read(ulong address, byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            string result = SendAndReceive(
                new RequestType(OpcodeType.GetAddress.ToString(),
                operands: new List<string>(2)
                {
                    AddressTranslator.TranslateAddress((uint)address, TranslationMode.Read)
                        .ToString("X", CultureInfo.InvariantCulture),
                    buffer.Length.ToString("X", CultureInfo.InvariantCulture)
                }));

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(result);

                if (bytes.Length != buffer.Length)
                {
                    Log(LogLevel.Error, $"Failed to retrieve memory data.");
                    return false;
                }

                for (int i = 0; i < bytes.Length; i++)
                {
                    buffer[i] = bytes[i];
                }

                Log(LogLevel.Debug, $"Read {buffer.Length} bytes at {address:X6}.");
                return true;
            }
            catch (EncoderFallbackException)
            {
                Log(LogLevel.Error, $"Failed to convert output to byte array.");
                return false;
            }
            catch (ArgumentNullException)
            {
                Log(LogLevel.Error, $"Failed to receive output.");
                return false;
            }
        }
    }
}
