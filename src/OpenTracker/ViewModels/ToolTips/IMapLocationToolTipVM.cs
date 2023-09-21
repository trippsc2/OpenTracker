using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.ToolTips;

public interface IMapLocationToolTipVM
{
    delegate IMapLocationToolTipVM Factory(ILocation location);
}