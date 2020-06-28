using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.WebSockets;

namespace OpenTracker.Models.SNESConnectors
{
    public interface ISNESConnector : INotifyPropertyChanged
    {
        string Device { get; set; }
        ClientWebSocket Socket { get; }
        ConnectionStatus Status { get; }
        Uri Uri { get; set; }

        void AttachDevice(int timeOutInMS = 1000, int retries = 3);
        IEnumerable<string> GetDeviceList(int timeOutInMS = 1000, int retries = 3);
        bool Read(ulong address, byte[] buffer);
        bool Read(ulong address, out byte value);
    }
}