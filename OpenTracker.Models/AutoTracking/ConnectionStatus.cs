namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the enum type for Autotracking connector status.
    /// </summary>
    public enum ConnectionStatus 
    {
        NotConnected,
        Connecting,
        SelectDevice,
        Attaching,
        Connected,
        Error
    }
}