using OpenTracker.Models.Locations.Map;

namespace OpenTracker.ViewModels.Maps;

public interface IMapVM
{
    delegate IMapVM Factory(MapID id);
}