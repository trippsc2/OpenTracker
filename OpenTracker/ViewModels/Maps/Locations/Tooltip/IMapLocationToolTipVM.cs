using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.Maps.Locations.Tooltip
{
    public interface IMapLocationToolTipVM
    {
        delegate IMapLocationToolTipVM Factory(ILocation location);
    }
}