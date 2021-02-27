using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This interface contains logic for the SNES connector using WebSocketSharp to connect to
    /// (Q)USB2SNES.
    /// </summary>
    public interface ISNESConnector
    {
        event EventHandler<ConnectionStatus>? StatusChanged;

        void Disconnect();
        Task<bool> AttachDeviceIfNeeded(int timeOutInMS = 4096);
        Task<IEnumerable<string>?> GetDevices(int timeOutInMS = 4096);
        Task<bool> Read(ulong address, byte[] buffer, int timeOutInMS = 4096);
        Task<byte?> Read(ulong address);
        Task<bool> Connect(int timeOutInMS = 4096);
        void SetUri(string uriString);
        void SetDevice(string device);
    }
}
