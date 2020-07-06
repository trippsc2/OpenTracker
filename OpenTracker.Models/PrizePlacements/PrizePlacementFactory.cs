namespace OpenTracker.Models.PrizePlacements
{
    internal static class PrizePlacementFactory
    {
        internal static IPrizePlacement GetPrizePlacement()
        {
            return new PrizePlacement();
        }
    }
}
