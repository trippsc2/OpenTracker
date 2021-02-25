namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This interface contains creation logic for location data.
    /// </summary>
    public interface ILocationFactory
    {
        string GetLocationName(LocationID id);
    }
}