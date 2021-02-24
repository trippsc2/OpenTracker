namespace OpenTracker.Models.Locations
{
    public interface ILocationFactory
    {
        string GetLocationName(LocationID id);
    }
}