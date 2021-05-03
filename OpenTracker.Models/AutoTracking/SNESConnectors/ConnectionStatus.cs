namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This enum type defines auto-tracking connector status values.
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