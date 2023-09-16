using System.Diagnostics.CodeAnalysis;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Socket;

/// <summary>
/// This class wraps the <see cref="MessageEventArgs"/> class to allow for unit testing.
/// </summary>
[ExcludeFromCodeCoverage]
public class MessageEventArgsWrapper : IMessageEventArgsWrapper
{
    private readonly MessageEventArgs _messageEventArgs;

    public bool IsBinary => _messageEventArgs.IsBinary;
    public string Data => _messageEventArgs.Data;
    public byte[] RawData => _messageEventArgs.RawData;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="messageEventArgs">
    ///     The <see cref="MessageEventArgs"/> to be wrapped.
    /// </param>
    public MessageEventArgsWrapper(MessageEventArgs messageEventArgs)
    {
        _messageEventArgs = messageEventArgs;
    }
}