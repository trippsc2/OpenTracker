using Newtonsoft.Json;
using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
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
        public Uri WebSocketURI { get; }
        public ClientWebSocket WebSocket { get; private set; }

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

        private void DisconnectAndDispose(int timeOutInMS = 1000)
        {
            if (WebSocket.State != WebSocketState.None && WebSocket.State != WebSocketState.Closed)
            {
                using CancellationTokenSource cts = new CancellationTokenSource(timeOutInMS);
                Task task = WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cts.Token);
                Task.WaitAll(new Task[] { task });
            }

            WebSocket.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public async Task ConnectIfNeeded(int timeOutInMS = 1000)
        {
            if (WebSocket.State == WebSocketState.Open)
            {
                Output(LogLevel.Debug, "Already connected to USB2SNES.");
                return;
            }

            if (WebSocket.State != WebSocketState.None)
            {
                Output(LogLevel.Debug, "Disposing of existing connection attempt.");
                DisconnectAndDispose(timeOutInMS);
                WebSocket = new ClientWebSocket();
            }

            Output(LogLevel.Debug, "Attempting to connect to USB2SNES.");
            using CancellationTokenSource cts = new CancellationTokenSource(timeOutInMS);
            await WebSocket.ConnectAsync(WebSocketURI, cts.Token).ConfigureAwait(false);
            Output(LogLevel.Info, $"Connection state is: { WebSocket.State }.");
        }

        public async Task<IEnumerable<string>> GetDeviceList()
        {
            var buffer = new ArraySegment<byte>(new byte[2048]);

            await WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(new RequestType(OpcodeType.DeviceList.ToString())))),
                WebSocketMessageType.Text, true, CancellationToken.None).ConfigureAwait(false);
            using var ms = new MemoryStream();
            WebSocketReceiveResult result;
            do
            {
                result = await WebSocket.ReceiveAsync(buffer, CancellationToken.None)
                    .ConfigureAwait(false);
                ms.Write(buffer.Array, buffer.Offset, result.Count);
            } while (result == null || !result.EndOfMessage);

            ms.Seek(0, SeekOrigin.Begin);
            
            using var reader = new StreamReader(ms, Encoding.UTF8);

            return JsonConvert.DeserializeObject<Dictionary<string, string[]>>(
                await reader.ReadToEndAsync().ConfigureAwait(false))["Results"];
        }

        public void Dispose()
        {
            DisconnectAndDispose();
        }
    }
}
