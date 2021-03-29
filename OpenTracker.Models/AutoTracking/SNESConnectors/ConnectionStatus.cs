namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    ///     This enum type contains auto-tracking connector status data.
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