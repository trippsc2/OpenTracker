using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Socket
{
    /// <summary>
    /// This interface wraps the <see cref="MessageEventArgs"/> class to allow for unit testing.
    /// </summary>
    public interface IMessageEventArgsWrapper
    {
        /// <inheritdoc cref="MessageEventArgs.IsBinary"/>
        bool IsBinary { get; }
        
        /// <inheritdoc cref="MessageEventArgs.Data"/>
        string Data { get; }
        
        /// <inheritdoc cref="MessageEventArgs.RawData"/>
        byte[] RawData { get; }

        /// <summary>
        /// A factory for creating new <see cref="IMessageEventArgsWrapper"/> objects.
        /// </summary>
        /// <param name="messageEventArgs">
        ///     The <see cref="MessageEventArgs"/> to be wrapped.
        /// </param>
        /// <returns>
        ///     A new <see cref="IMessageEventArgsWrapper"/> object.
        /// </returns>
        delegate IMessageEventArgsWrapper Factory(MessageEventArgs messageEventArgs);
    }
}