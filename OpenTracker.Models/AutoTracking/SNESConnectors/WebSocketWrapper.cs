using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    public class WebSocketWrapper : WebSocket, IWebSocketWrapper
    {
        public WebSocketWrapper(string url, params string[] protocols) : base(url, protocols)
        {
        }
    }
}