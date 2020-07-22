using System;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the class to containing creation logic for a SNES connector.
    /// </summary>
    public static class SNESConnectorFactory
    {
        /// <summary>
        /// Returns a new SNES connector instance.
        /// </summary>
        /// <param name="logHandler">
        /// The action to be performed on new log messages.
        /// </param>
        /// <returns>
        /// A new SNES connector instance.
        /// </returns>
        public static ISNESConnector GetSNESConnector(Action<LogLevel, string> logHandler)
        {
            return new USB2SNESConnector(logHandler);
        }
    }
}
