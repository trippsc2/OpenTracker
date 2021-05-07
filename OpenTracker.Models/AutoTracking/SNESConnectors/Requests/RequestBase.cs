using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using OpenTracker.Models.Logging;
using WebSocketSharp;
using LogLevel = OpenTracker.Models.Logging.LogLevel;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests
{
    /// <summary>
    /// This base class contains the USB2SNES request and response data and logic.
    /// </summary>
    /// <typeparam name="T">
    ///     The return type returned by the request response.
    /// </typeparam>
    public abstract class RequestBase<T> : IRequest<T>
    {
        protected readonly IAutoTrackerLogger Logger;
        
        private readonly string _opcode;
        private readonly string _space;
        private readonly List<string> _flags;
        private readonly List<string> _operands;

        public ConnectionStatus StatusRequired { get; }
        public string Description { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">
        ///     The <see cref="IAutoTrackerLogger"/>.
        /// </param>
        /// <param name="opcode">
        ///     A <see cref="string"/> representing the opcode of the request.
        /// </param>
        /// <param name="space">
        ///     A <see cref="string"/> representing the space of the request.
        /// </param>
        /// <param name="statusRequired">
        ///     A <see cref="ConnectionStatus"/> representing the status required for this request.
        /// </param>
        /// <param name="description">
        ///     A <see cref="string"/> representing the description of the request for logging.
        /// </param>
        /// <param name="flags">
        ///     A <see cref="IList{T}"/> of <see cref="string"/> representing the flags of the request.
        /// </param>
        /// <param name="operands">
        ///     A <see cref="IList{T}"/> of <see cref="string"/> representing the operands of the request.
        /// </param>
        protected RequestBase(
            IAutoTrackerLogger logger, string opcode, string space, ConnectionStatus statusRequired, string description,
            List<string>? flags = null, List<string>? operands = null)
        {
            Logger = logger;

            _opcode = opcode;
            _space = space;
            _flags = flags ?? new List<string>();
            _operands = operands ?? new List<string>();
            
            StatusRequired = statusRequired;
            Description = description;
        }
        
        public string ToJsonString()
        {
            Logger.Log(LogLevel.Debug, $"Attempting to create JSON request \'{Description}\'.");
            var requestObject = new Dictionary<string, object>
            {
                {"Opcode", _opcode},
                {"Space", _space}
            };

            if (_flags.Count > 0)
            {
                requestObject.Add("Flags", _flags);
            }

            if (_operands.Count > 0)
            {
                requestObject.Add("Operands", _operands);
            }
            
            var serializedObject = JsonConvert.SerializeObject(requestObject);
            Logger.Log(LogLevel.Debug, $"Successfully created JSON request \'{Description}\'.");

            return serializedObject;
        }

        public abstract T ProcessResponseAndReturnResults(MessageEventArgs messageEventArgs, ManualResetEvent sendEvent);
    }
}