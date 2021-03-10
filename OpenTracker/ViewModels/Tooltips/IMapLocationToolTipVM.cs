using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.Tooltips
{
    public interface IMapLocationToolTipVM
    {
        delegate IMapLocationToolTipVM Factory(ILocation location);
    }
}