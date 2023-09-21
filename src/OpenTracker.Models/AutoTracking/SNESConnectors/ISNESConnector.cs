using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace OpenTracker.Models.AutoTracking.SNESConnectors;

/// <summary>
/// Represents the logic for connecting to a SNES game via (Q)USB2SNES or compatible software.
/// </summary>
public interface ISNESConnector : INotifyPropertyChanged
{
    /// <summary>
    /// A <see cref="ConnectionStatus"/> representing the current connection status.
    /// </summary>
    ConnectionStatus Status { get; }

    /// <summary>
    /// Sets the connection URI to be passed to the web socket.
    /// </summary>
    /// <param name="uriString">
    ///     A <see cref="string"/> representing the URI.
    /// </param>
    void SetURI(string uriString);

    /// <summary>
    /// Connects to the web socket asynchronously.
    /// </summary>
    Task ConnectAsync();

    /// <summary>
    /// Disconnects from the web socket and unsets the web socket property asynchronously.
    /// </summary>
    Task DisconnectAsync();

    /// <summary>
    /// Returns the devices able to be selected asynchronously.
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerable{T}"/> of <see cref="string"/> representing the devices able to be selected.
    /// </returns>
    Task<IEnumerable<string>?> GetDevicesAsync();
                
    /// <summary>
    /// Sets the device to the specified device asynchronously.
    /// </summary>
    /// <param name="device">
    ///     A <see cref="string"/> representing the device.
    /// </param>
    Task AttachDeviceAsync(string device);

    /// <summary>
    /// Returns the values of the specified memory addresses asynchronously.
    /// </summary>
    /// <param name="address">
    ///     A <see cref="ulong"/> representing the starting memory address.
    /// </param>
    /// <param name="bytesToRead">
    ///     A <see cref="int"/> representing the number of addresses to read.
    /// </param>
    /// <returns>
    ///     An array of <see cref="byte"/> representing the values of the memory addresses.
    /// </returns>
    Task<byte[]?> ReadMemoryAsync(ulong address, int bytesToRead = 1);
}