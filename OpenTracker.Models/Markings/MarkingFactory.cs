namespace OpenTracker.Models.Markings
{
    /// <summary>
    /// This is the class for creating marking classes.
    /// </summary>
    public static class MarkingFactory
    {
        /// <summary>
        /// Returns a new marking instance.
        /// </summary>
        public static IMarking GetMarking()
        {
            return new Marking();
        }
    }
}
