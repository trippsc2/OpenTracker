using System.Collections.Generic;
using RxVoid = ReactiveUI.Primitives.RxVoid;
using System.Threading;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests;

/// <summary>
/// This class contains the request to register the app name. 
/// </summary>
public class RegisterNameRequest : RequestBase<RxVoid>, IRegisterNameRequest
{
    private const string AppName = "OpenTracker";
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">
    ///     The <see cref="IAutoTrackerLogger"/>.
    /// </param>
    public RegisterNameRequest(IAutoTrackerLogger logger)
        : base(logger, "Name", "SNES", ConnectionStatus.Connected, "Register app name",
            operands: new List<string> {AppName})
    {
    }

    public override RxVoid ProcessResponseAndReturnResults(IMessageEventArgsWrapper messageEventArgs,
        ManualResetEvent sendEvent)
    {
        return default;
    }
}