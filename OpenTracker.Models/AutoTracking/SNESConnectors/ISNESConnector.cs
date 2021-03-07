using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This interface contains logic for the SNES connector to (Q)USB2SNES.
    /// </summary>
    public interface ISNESConnector
    {
        event EventHandler<ConnectionStatus>? StatusChanged;

        Task Disconnect();
        Task<bool> AttachDeviceIfNeeded(int timeOutInMs = 4096);
        Task<IEnumerable<string>?> GetDevices(int timeOutInMs = 4096);
        Task<byte[]?> Read(ulong address, int bytesToRead = 1, int timeOutInMs = 4096);
        Task<bool> Connect(int timeOutInMs = 4096);
        void SetUri(string uriString);
        Task SetDevice(string device);
    }
}
