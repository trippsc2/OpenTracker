using System.Collections.Generic;
using System.Threading.Tasks;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using ReactiveUI;

namespace OpenTracker.Models.AutoTracking;

/// <summary>
/// This interface contains auto-tracking logic and data.
/// </summary>
public interface IAutoTracker : IReactiveObject
{
    /// <summary>
    /// The <see cref="IList{T}"/> of <see cref="string"/> representing the devices most recently returned by the
    /// SNES connector.
    /// </summary>
    IList<string> Devices { get; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether to allow race illegal auto-tracking features.
    /// </summary>
    bool RaceIllegalTracking { get; set; }
        
    /// <summary>
    /// The current auto-tracking <see cref="ConnectionStatus"/>.
    /// </summary>
    ConnectionStatus Status { get; }
        
    /// <summary>
    /// Returns whether the web socket can be connected to.
    /// </summary>
    /// <returns>
    ///     A <see cref="bool"/> representing whether the web socket can be connected to.
    /// </returns>
    bool CanConnect();

    /// <summary>
    /// Connects to the web socket with the specified URI string.
    /// </summary>
    /// <param name="uriString">
    ///     A <see cref="string"/> representing the web socket URI.
    /// </param>
    Task Connect(string uriString);

    /// <summary>
    /// Returns whether the web socket is able to provide the devices.
    /// </summary>
    /// <returns>
    ///     A <see cref="bool"/> representing whether the web socket is able to provide the devices.
    /// </returns>
    bool CanGetDevices();

    /// <summary>
    /// Updates the <see cref="Devices"/> property from the SNES connector.
    /// </summary>
    Task GetDevices();

    /// <summary>
    /// Returns whether the web socket can be disconnected.
    /// </summary>
    /// <returns>
    ///     A <see cref="bool"/> representing whether the web socket can be disconnected.
    /// </returns>
    bool CanDisconnect();

    /// <summary>
    /// Disconnects the auto-tracker.
    /// </summary>
    Task Disconnect();

    /// <summary>
    /// Returns whether auto-tracking can be started.
    /// </summary>
    /// <returns>
    ///     A <see cref="bool"/> representing whether auto-tracking can be started.
    /// </returns>
    bool CanStart();

    /// <summary>
    /// Starts auto-tracking.
    /// </summary>
    /// <param name="device">
    ///     A <see cref="string"/> representing the device to which to connect.
    /// </param>
    Task Start(string device);
        
    /// <summary>
    /// Updates cached value of the SNES memory address that provides game status.
    /// </summary>
    Task InGameCheck();

    /// <summary>
    /// Updates cached values of all SNES memory addresses.
    /// </summary>
    Task MemoryCheck();
}