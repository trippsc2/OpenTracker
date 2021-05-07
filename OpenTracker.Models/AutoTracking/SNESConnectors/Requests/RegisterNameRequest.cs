using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using OpenTracker.Models.Logging;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests
{
    /// <summary>
    /// This class contains the request to register the app name. 
    /// </summary>
    public class RegisterNameRequest : RequestBase<Unit>, IRegisterNameRequest
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

        public override Unit ProcessResponseAndReturnResults(
            MessageEventArgs messageEventArgs, ManualResetEvent sendEvent)
        {
            return default;
        }
    }
}