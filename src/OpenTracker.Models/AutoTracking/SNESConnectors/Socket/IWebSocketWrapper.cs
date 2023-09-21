using System;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Socket;

/// <summary>
/// This interface wraps the <see cref="WebSocket"/> class to allow for unit testing.
/// </summary>
public interface IWebSocketWrapper : IDisposable
{
    /// <inheritdoc cref="WebSocket.Url"/>
    Uri Url { get; }

    /// <inheritdoc cref="WebSocket.OnClose"/>
    event EventHandler<CloseEventArgs> OnClose;

    /// <inheritdoc cref="WebSocket.OnError"/>
    event EventHandler<ErrorEventArgs> OnError; 
        
    /// <inheritdoc cref="WebSocket.OnMessage"/>
    event EventHandler<MessageEventArgs> OnMessage;
        
    /// <inheritdoc cref="WebSocket.OnOpen"/>
    event EventHandler OnOpen;

    /// <summary>
    /// A factory for creating new <see cref="IWebSocketWrapper"/> objects.
    /// </summary>
    /// <param name="url">
    /// A <see cref="T:System.String" /> that specifies the URL of the WebSocket
    /// server to connect.
    /// </param>
    /// <param name="protocols">
    ///     <para>
    ///         An array of <see cref="T:System.String" /> that specifies the names of
    ///         the sub-protocols, if necessary.
    ///     </para>
    ///     <para>
    ///         Each value of the array must be a token defined in
    ///         <see href="http://tools.ietf.org/html/rfc2616#section-2.2">
    ///         RFC 2616</see>.
    ///     </para>
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///     <paramref name="url" /> is <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///     <para>
    ///         <paramref name="url" /> is an empty string.
    ///     </para>
    ///     <para>
    ///     -or-
    ///     </para>
    ///     <para>
    ///         <paramref name="url" /> is an invalid WebSocket URL string.
    ///     </para>
    ///     <para>
    ///     -or-
    ///     </para>
    ///     <para>
    ///         <paramref name="protocols" /> contains a value that is not a token.
    ///     </para>
    ///     <para>
    ///     -or-
    ///     </para>
    ///     <para>
    ///         <paramref name="protocols" /> contains a value twice.
    ///     </para>
    /// </exception>
    /// <returns>
    ///     A new <see cref="IWebSocketWrapper"/> object.
    /// </returns>
    delegate IWebSocketWrapper Factory(string url, params string[] protocols);

    /// <inheritdoc cref="WebSocket.Close()"/>
    void Close();

    /// <inheritdoc cref="WebSocket.Connect()"/>
    void Connect();

    /// <inheritdoc cref="WebSocket.Send(string)"/>
    void Send(string data);
}