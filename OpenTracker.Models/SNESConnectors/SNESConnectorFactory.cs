using OpenTracker.Models.Enums;
using System;

namespace OpenTracker.Models.SNESConnectors
{
    public static class SNESConnectorFactory
    {
        public static ISNESConnector GetSNESConnector(Action<LogLevel, string> logHandler)
        {
            return new USB2SNESConnector(logHandler);
        }
    }
}
