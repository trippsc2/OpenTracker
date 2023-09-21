using OpenTracker.Utils.Autofac;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Socket;

/// <summary>
/// This class wraps the <see cref="WebSocket"/> class to allow for unit testing.
/// </summary>
[DependencyInjection]
public sealed class WebSocketWrapper : WebSocket, IWebSocketWrapper
{
    /// <inheritdoc />
    public WebSocketWrapper(string url, params string[] protocols) : base(url, protocols)
    {
    }
}