namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This interface contains the creation logic for prize placement data.
    /// </summary>
    public interface IPrizePlacementFactory
    {
        IPrizePlacement GetPrizePlacement(PrizePlacementID id);

        delegate IPrizePlacementFactory Factory();
    }
}