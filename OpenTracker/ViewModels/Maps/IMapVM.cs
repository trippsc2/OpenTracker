using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.Maps
{
    public interface IMapVM
    {
        delegate IMapVM Factory(MapID id);
    }
}