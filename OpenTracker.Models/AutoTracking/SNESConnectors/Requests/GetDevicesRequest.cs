using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;
using LogLevel = OpenTracker.Models.Logging.LogLevel;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests
{
    /// <summary>
    /// This class contains the request to get the list of devices. 
    /// </summary>
    public class GetDevicesRequest : RequestBase<IEnumerable<string>>, IGetDevicesRequest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">
        ///     The <see cref="IAutoTrackerLogger"/>.
        /// </param>
        public GetDevicesRequest(IAutoTrackerLogger logger)
            : base(logger, "DeviceList", "SNES", ConnectionStatus.SelectDevice, "Get device list")
        {
        }

        public override IEnumerable<string> ProcessResponseAndReturnResults(IMessageEventArgsWrapper messageEventArgs,
            ManualResetEvent sendEvent)
        {
            Logger.Log(LogLevel.Debug, $"Received response message from request \'{Description}\'");

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string[]>?>(messageEventArgs.Data);

            if (!deserialized!.TryGetValue("Results", out var results))
            {
                throw new Exception(
                    $"Request \'{Description}\' is invalid and does not contain a \'Results\' key.");
            }
            
            Logger.Log(LogLevel.Debug, $"Request \'{Description}\' response successfully deserialized.");
            sendEvent.Set();
            return results;
        }
    }
}