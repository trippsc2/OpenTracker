using System;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This interface wraps the <see cref="WebSocket"/> class to allow for unit testing.
    /// </summary>
    public interface IWebSocketWrapper
    {
        bool IsAlive { get; }
        Uri Url { get; }

        event EventHandler<MessageEventArgs> OnMessage;
        event EventHandler OnOpen;

        delegate IWebSocketWrapper Factory(string url, params string[] protocols);

        void Close();
        void Connect();
        void Send(string data);
    }
}