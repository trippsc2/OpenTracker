using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the interface containing autotracking data and methods
    /// </summary>
    public interface IAutoTracker : INotifyPropertyChanged
    {
        IEnumerable<string>? Devices { get; }
        bool RaceIllegalTracking { get; set; }
        ISNESConnector SNESConnector { get; }
        Dictionary<ulong, IMemoryAddress> MemoryAddresses { get; }

        void InGameCheck();
        void MemoryCheck();
        Task GetDevices();
        Task Stop();
    }
}
