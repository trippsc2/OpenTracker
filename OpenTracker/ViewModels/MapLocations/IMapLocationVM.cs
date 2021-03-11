using Avalonia.Controls;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using OpenTracker.ViewModels.Markings;
using OpenTracker.ViewModels.Tooltips;

namespace OpenTracker.ViewModels.MapLocations
{
    public interface IMapLocationVM
    {
        delegate IMapLocationVM Factory(
            IMapLocation mapLocation, IRequirement? dockRequirement, Dock metDock, Dock unmetDock,
            IMapLocationMarkingVM? marking, IShapedMapLocationVMBase location, IMapLocationToolTipVM toolTip);
    }
}