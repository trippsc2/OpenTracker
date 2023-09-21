using Avalonia.Controls;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Requirements;
using OpenTracker.ViewModels.ToolTips;

namespace OpenTracker.ViewModels.MapLocations;

public interface IMapLocationVM
{
    delegate IMapLocationVM Factory(
        IMapLocation mapLocation,
        IRequirement? dockRequirement,
        IMapLocationMarkingVM? marking,
        IShapedMapLocationVMBase location,
        IMapLocationToolTipVM toolTip,
        Dock metDock,
        Dock unmetDock);

    double CanvasX { get; }
    double CanvasY { get; }
}