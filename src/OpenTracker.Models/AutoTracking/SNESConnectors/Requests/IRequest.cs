using System.Threading;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests;

/// <summary>
/// This interface contains the USB2SNES request and response data and logic.
/// </summary>
/// <typeparam name="T">
///     The return type returned by the request response.
/// </typeparam>
public interface IRequest<out T>
{
    /// <summary>
    /// The <see cref="ConnectionStatus"/> required to process the request.
    /// </summary>
    ConnectionStatus StatusRequired { get; }

    /// <summary>
    /// A <see cref="string"/> representing a description of the request for logging purposes.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Returns the <see cref="IRequest{T}"/> as a JSON-formatted <see cref="string"/>.
    /// </summary>
    /// <returns>
    ///     A <see cref="string"/> representing the <see cref="IRequest{T}"/> in JSON format. 
    /// </returns>
    string ToJsonString();

    /// <summary>
    /// Returns the processed result of the request from the specified response message.
    /// </summary>
    /// <param name="messageEventArgs">
    ///     The wrapped <see cref="MessageEventArgs"/> of the response.
    /// </param>
    /// <param name="sendEvent">
    ///     A <see cref="ManualResetEvent"/> that waits for data to be received or a 2 second timeout.
    /// </param>
    /// <returns>
    ///     The processed result of the request.
    /// </returns>
    T ProcessResponseAndReturnResults(IMessageEventArgsWrapper messageEventArgs, ManualResetEvent sendEvent);
}