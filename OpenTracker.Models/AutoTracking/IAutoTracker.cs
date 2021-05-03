using System.Collections.Generic;
using System.Threading.Tasks;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using ReactiveUI;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    ///     This interface contains auto-tracking logic and data.
    /// </summary>
    public interface IAutoTracker : IReactiveObject
    {
        /// <summary>
        ///     The list of devices most recently returned by the SNES connector.
        /// </summary>
        IList<string> Devices { get; }
        
        /// <summary>
        ///     A boolean representing whether to allow race illegal auto-tracking features.
        /// </summary>
        bool RaceIllegalTracking { get; set; }
        
        /// <summary>
        ///     The current auto-tracking status.
        /// </summary>
        ConnectionStatus Status { get; }
        
        /// <summary>
        ///     Returns whether the web socket can be connected to.
        /// </summary>
        /// <returns>
        ///     A boolean representing whether the web socket can be connected to.
        /// </returns>
        bool CanConnect();

        /// <summary>
        ///     Connects to the web socket with the specified URI string.
        /// </summary>
        /// <param name="uriString">
        ///     A string representing the web socket URI.
        /// </param>
        Task Connect(string uriString);

        /// <summary>
        ///     Returns whether the web socket is able to provide the devices.
        /// </summary>
        /// <returns>
        ///     A boolean representing whether the web socket is able to provide the devices.
        /// </returns>
        bool CanGetDevices();

        /// <summary>
        ///     Updates the list of devices.
        /// </summary>
        Task GetDevices();

        /// <summary>
        ///     Returns whether the web socket can be disconnected.
        /// </summary>
        /// <returns>
        ///     A boolean representing whether the web socket can be disconnected.
        /// </returns>
        bool CanDisconnect();

        /// <summary>
        ///     Disconnects the auto-tracker.
        /// </summary>
        Task Disconnect();

        /// <summary>
        ///     Returns whether auto-tracking can be started.
        /// </summary>
        /// <returns>
        ///     A boolean representing whether auto-tracking can be started.
        /// </returns>
        bool CanStart();

        /// <summary>
        ///     Starts auto-tracking.
        /// </summary>
        Task Start(string device);
        
        /// <summary>
        ///     Updates cached value of the SNES memory address that provides game status.
        /// </summary>
        Task InGameCheck();

        /// <summary>
        ///     Updates cached values of all SNES memory addresses.
        /// </summary>
        Task MemoryCheck();
    }
}
