using System.Collections.Generic;
using System.Threading.Tasks;
using ReactiveUI;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This interface contains auto-tracking logic and data.
    /// </summary>
    public interface IAutoTracker : IReactiveObject
    {
        List<string> Devices { get; }
        bool RaceIllegalTracking { get; set; }
        Dictionary<ulong, IMemoryAddress> MemoryAddresses { get; }
        ConnectionStatus Status { get; }

        Task InGameCheck();
        Task MemoryCheck();
        Task GetDevices();
        Task Disconnect();
        Task Connect(string uriString);
        bool CanConnect();
        bool CanDisconnect();
        bool CanGetDevices();
        bool CanStart();
        Task Start(string device);
    }
}
