using Newtonsoft.Json;
using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenTracker.Models.AutotrackerConnectors
{
    public sealed class USB2SNESConnector : IDisposable
    {
        private readonly Action<LogLevel, string> _logHandler;
        private readonly object _transmitLock = new object();

        private bool Connected =>
            WebSocket != null && WebSocket.State == WebSocketState.Open;

        public Uri WebSocketURI { get; }
        public ClientWebSocket WebSocket { get; private set; }
        public string Device { get; set; }

        public USB2SNESConnector(Uri webSocketURI, Action<LogLevel, string> logHandler)
        {
            _logHandler = logHandler;
            WebSocketURI = webSocketURI;
            WebSocket = new ClientWebSocket();
        }

        private void Output(LogLevel logLevel, string logMessage)
        {
            _logHandler(logLevel, logMessage);
        }

        private bool Connect(int timeOutInMS = 1000)
        {
            Output(LogLevel.Info, "Attempting to connect to USB2SNES.");
            using CancellationTokenSource cts = new CancellationTokenSource(timeOutInMS);
            Task task = WebSocket.ConnectAsync(WebSocketURI, cts.Token);

            try
            {
                Task.WaitAll(new Task[] { task });
            }
            catch (AggregateException)
            {
                Output(LogLevel.Info, "Attempt to connect timed out.");
                return false;
            }

            if (WebSocket.State == WebSocketState.Open)
            {
                Output(LogLevel.Info, "Successfully connected to USB2SNES.");
                return true;
            }

            Output(LogLevel.Info, "Failed to connect to USB2SNES for unknown reason.");
            return false;
        }

        private void DisconnectAndDispose(int timeOutInMS = 1000)
        {
            if (WebSocket.State != WebSocketState.None && WebSocket.State != WebSocketState.Closed)
            {
                using CancellationTokenSource cts = new CancellationTokenSource(timeOutInMS);
                Task task = WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cts.Token);

                try
                {
                    Task.WaitAll(new Task[] { task });
                }
                catch (AggregateException)
                {
                    Output(LogLevel.Debug, "Attempt to close connection timed out.");
                }
            }

            WebSocket.Dispose();
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
                if (WebSocket.State != WebSocketState.None)
                {
                    Output(LogLevel.Debug, "Disposing of existing connection attempt.");
                    DisconnectAndDispose(timeOutInMS);
                    WebSocket = new ClientWebSocket();
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
                await WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(requestType))),
                    WebSocketMessageType.Text, true, cts.Token).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                Output(LogLevel.Warn, "Failed to send data.");
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
                    result = await WebSocket.ReceiveAsync(buffer, cts.Token).ConfigureAwait(false);
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

        private static bool MapAddressInRange(uint address, uint sourceRangeBegin, uint sourceRangeEnd,
            uint destinationRangeBegin, out uint mappedAddress)
        {
            if (address >= sourceRangeBegin && address <= sourceRangeEnd)
            {
                mappedAddress = address - sourceRangeBegin + destinationRangeBegin;
                return true;
            }

            mappedAddress = 0U;
            return false;
        }

        private static uint TranslateAddress(uint address, TranslationMode mode)
        {
            if (mode == TranslationMode.Read &&
                MapAddressInRange(address, 8257536U, 8388607U, 16056320U, out uint mappedAddress))
            {
                return mappedAddress;
            }

            for (uint index = 0; index < 63U; ++index)
            {
                if (MapAddressInRange(address, (uint)((int)index * 65536 + 32768),
                    (uint)((int)index * 65536 + ushort.MaxValue),
                    index * 32768U, out mappedAddress) ||
                    MapAddressInRange(address, (uint)((int)index * 65536 + 8421376),
                    (uint)((int)index * 65536 + 8454143),
                    index * 32768U, out mappedAddress))
                {
                    return mappedAddress;
                }
            }

            for (uint index = 0; index < 8U; ++index)
            {
                if (MapAddressInRange(address, (uint)(7340032 + (int)index * 65536),
                    (uint)(7372799 + (int)index * 65536),
                    (uint)(14680064 + (int)index * 32768), out mappedAddress))
                {
                    return mappedAddress;
                }
            }

            return address;
        }

        public IEnumerable<string> GetDeviceList(int timeOutInMS = 1000, int retries = 3)
        {
            Output(LogLevel.Info, "Requesting device list from USB2SNES.");

            string deviceList = SendAndReceive(new RequestType(OpcodeType.DeviceList.ToString()),
                timeOutInMS, retries);

            if (deviceList == null)
            {
                Output(LogLevel.Error, "Failed to receive device list from USB2SNES.");
                return null;
            }

            return JsonConvert.DeserializeObject<Dictionary<string, string[]>>(deviceList)
                ["Results"];
        }

        public bool AttachDevice(int timeOutInMS = 1000, int retries = 3)
        {
            Output(LogLevel.Info, $"Attempting to attach to device: { Device }");

            if (!SendOnly(new RequestType(OpcodeType.Attach.ToString(),
                operands: new List<string>(1) { Device }), timeOutInMS, retries))
            {
                Output(LogLevel.Error, "Unable to send attach command.");
                return false;
            }

            if (GetInfo(timeOutInMS, retries) == null || WebSocket.State != WebSocketState.Open)
            {
                Output(LogLevel.Error, $"Failed to attach to device: { Device }");
                return false;
            }

            Output(LogLevel.Info, $"Successfully attached to device: { Device }");
            return true;
        }

        public bool Read(ulong address, out byte value)
        {
            Output(LogLevel.Debug, $"Reading byte at address {address:X6}.");

            byte[] buffer = new byte[1];

            if (Read(address, buffer))
            {
                value = buffer[0];
                Output(LogLevel.Debug, $"Read value {value} at address {address:X6}.");
                return true;
            }

            Output(LogLevel.Error, $"Failed to read value at {address:X6}.");
            value = 0;
            return false;
        }

        public bool Read(ulong address, byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            string result = SendAndReceive(new RequestType(OpcodeType.GetAddress.ToString(),
                operands: new List<string>(2)
                {
                    TranslateAddress((uint)address, TranslationMode.Read)
                        .ToString("X", CultureInfo.InvariantCulture),
                    buffer.Length.ToString("X", CultureInfo.InvariantCulture)
                }));

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(result);

                if (bytes.Length != buffer.Length)
                {
                    Output(LogLevel.Error, $"Failed to retrieve memory data.");
                    return false;
                }

                for (int i = 0; i < bytes.Length; i++)
                {
                    buffer[i] = bytes[i];
                }

                Output(LogLevel.Debug, $"Read {buffer.Length} bytes at {address:X6}.");
                return true;
            }
            catch (EncoderFallbackException)
            {
                Output(LogLevel.Error, $"Failed to convert output to byte array.");
                return false;
            }
            catch (ArgumentNullException)
            {
                Output(LogLevel.Error, $"Failed to receive output.");
                return false;
            }
        }

        public void Dispose()
        {
            DisconnectAndDispose();
        }
    }
}
