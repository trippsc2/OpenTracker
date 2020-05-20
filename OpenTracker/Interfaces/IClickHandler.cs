namespace OpenTracker.Interfaces
{
    public interface IClickHandler
    {
        void OnLeftClick(bool force = false);
        void OnRightClick(bool force = false);
    }
}
