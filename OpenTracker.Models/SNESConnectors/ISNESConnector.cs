using System.Collections.Generic;
using System.ComponentModel;
using WebSocketSharp;

namespace OpenTracker.Models.SNESConnectors
{
    public interface ISNESConnector
    {
        bool Connected { get; }
        string Device { get; set; }
        WebSocket Socket { get; }
        ConnectionStatus Status { get; }
        string Uri { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        void Disconnect();
        bool AttachDeviceIfNeeded(int timeOutInMS = 4096);
        IEnumerable<string> GetDevices(int timeOutInMS = 4096);
        bool Read(ulong address, byte[] buffer, int timeOutInMS = 4096);
        bool Read(ulong address, out byte value);
    }
}