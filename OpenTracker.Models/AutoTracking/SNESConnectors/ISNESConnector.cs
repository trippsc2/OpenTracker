using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This interface contains logic for the SNES connector to (Q)USB2SNES.
    /// </summary>
    public interface ISNESConnector
    {
        event EventHandler<ConnectionStatus>? StatusChanged;

        /// <summary>
        /// Sets the connection URI to be passed to the web socket.
        /// </summary>
        /// <param name="uriString">
        /// A string representing the URI.
        /// </param>
        void SetUri(string uriString);

        /// <summary>
        /// Connects to the USB2SNES web socket.
        /// </summary>
        /// <param name="timeOutInMs">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        Task<bool> Connect(int timeOutInMs = 4096);

        /// <summary>
        /// Disconnects from the web socket and unsets the web socket property.
        /// </summary>
        Task Disconnect();

        Task<bool> AttachDeviceIfNeeded(int timeOutInMs = 4096);
        Task<IEnumerable<string>?> GetDevices(int timeOutInMs = 4096);
        Task<byte[]?> Read(ulong address, int bytesToRead = 1, int timeOutInMs = 4096);
        Task SetDevice(string device);
    }
}
