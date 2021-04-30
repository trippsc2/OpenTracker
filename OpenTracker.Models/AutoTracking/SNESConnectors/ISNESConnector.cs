using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    ///     This interface contains logic for the SNES connector to (Q)USB2SNES.
    /// </summary>
    public interface ISNESConnector
    {
        /// <summary>
        ///     The event raised when the status of the connector changes.
        /// </summary>
        event EventHandler<ConnectionStatus>? StatusChanged;

        /// <summary>
        ///     Sets the connection URI to be passed to the web socket.
        /// </summary>
        /// <param name="uriString">
        ///     A string representing the URI.
        /// </param>
        void SetUri(string uriString);

        /// <summary>
        ///     Connects to the USB2SNES web socket.
        /// </summary>
        /// <param name="timeOutInMs">
        ///     A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        ///     A boolean representing whether the method is successful.
        /// </returns>
        Task<bool> Connect(int timeOutInMs = 4096);

        /// <summary>
        ///     Disconnects from the web socket and unsets the web socket property.
        /// </summary>
        Task Disconnect();

        /// <summary>
        ///     Returns the devices able to be selected.
        /// </summary>
        /// <param name="timeOutInMs">
        ///     A 32-bit signed integer representing the time in milliseconds before timeout.
        /// </param>
        /// <returns>
        ///     An enumerable of strings representing the devices able to be selected.
        /// </returns>
        Task<IEnumerable<string>?> GetDevices(int timeOutInMs = 4096);
        
        /// <summary>
        ///     Returns the byte values of the specified memory addresses.
        /// </summary>
        /// <param name="address">
        ///     A 64-bit unsigned integer representing the starting memory address.
        /// </param>
        /// <param name="bytesToRead">
        ///     A 32-bit signed integer representing the number of addresses to read.
        /// </param>
        /// <param name="timeOutInMs">
        ///     A 32-bit signed integer representing the time in milliseconds before timeout.
        /// </param>
        /// <returns>
        ///     An array of 8-bit unsigned integers representing the values of the memory addresses.
        /// </returns>
        Task<byte[]?> Read(ulong address, int bytesToRead = 1, int timeOutInMs = 4096);
        
        /// <summary>
        ///     Sets the device to the specified device.
        /// </summary>
        /// <param name="device">
        ///     A string representing the device.
        /// </param>
        Task SetDevice(string device);
    }
}
