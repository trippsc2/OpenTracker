using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using OpenTracker.Models.Logging;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests
{
    /// <summary>
    /// This class contains the request to attach a device. 
    /// </summary>
    public class AttachDeviceRequest : RequestBase<Unit>, IAttachDeviceRequest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">
        ///     The <see cref="IAutoTrackerLogger"/>.
        /// </param>
        /// <param name="device">
        ///     A <see cref="string"/> representing the device to be attached.
        /// </param>
        public AttachDeviceRequest(IAutoTrackerLogger logger, string device)
            : base(logger, "Attach", "SNES", ConnectionStatus.Attaching, $"Select {device}",
                operands: new List<string> {device})
        {
        }

        public override Unit ProcessResponseAndReturnResults(
            MessageEventArgs messageEventArgs, ManualResetEvent sendEvent)
        {
            return default;
        }
    }
}