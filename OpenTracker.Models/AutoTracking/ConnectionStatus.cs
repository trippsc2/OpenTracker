namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This enum type contains auto-tracking connector status data.
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