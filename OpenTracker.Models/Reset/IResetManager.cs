namespace OpenTracker.Models.Reset
{
    /// <summary>
    /// This interface contains logic for resetting the tracker.
    /// </summary>
    public interface IResetManager
    {
        /// <summary>
        /// Resets all the tracker data to its starting values.
        /// </summary>
        void Reset();
    }
}