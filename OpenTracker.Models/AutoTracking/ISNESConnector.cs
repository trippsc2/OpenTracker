using System.Collections.Generic;
using System.ComponentModel;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the interface providing a SNES connector.
    /// </summary>
    public interface ISNESConnector : INotifyPropertyChanged
    {
        bool Connected { get; }
        string Device { get; set; }
        WebSocket Socket { get; }
        ConnectionStatus Status { get; }
        string Uri { get; set; }

        void Disconnect();
        bool AttachDeviceIfNeeded(int timeOutInMS = 4096);
        IEnumerable<string> GetDevices(int timeOutInMS = 4096);
        bool Read(ulong address, byte[] buffer, int timeOutInMS = 4096);
        bool Read(ulong address, out byte value);
    }
}