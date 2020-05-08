namespace OpenTracker.Models.AutotrackerConnectors
{
    public enum ConnectionStatus : byte
    {
        NotConnected,
        Connecting,
        Open,
        Error
    }
}