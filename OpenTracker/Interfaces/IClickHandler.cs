namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface for handling clicks.
    /// </summary>
    public interface IClickHandler
    {
        void OnLeftClick(bool force = false);
        void OnRightClick(bool force = false);
    }
}
