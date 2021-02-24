using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking
{
    public interface ISNESConnector : INotifyPropertyChanged
    {
        string? Device { get; set; }
        WebSocket? Socket { get; }
        ConnectionStatus Status { get; }
        string? Uri { get; set; }

        void Disconnect();
        bool AttachDeviceIfNeeded(int timeOutInMS = 4096);
        Task<IEnumerable<string>?> GetDevices(int timeOutInMS = 4096);
        bool Read(ulong address, byte[] buffer, int timeOutInMS = 4096);
        bool Read(ulong address, out byte value);
    }
}
