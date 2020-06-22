namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface for handling the pointer over a UI element.
    /// </summary>
    public interface IPointerOver
    {
        void OnPointerEnter();
        void OnPointerLeave();
    }
}
