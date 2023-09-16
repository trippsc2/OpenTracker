using System.Collections.Generic;
using Avalonia;
using OpenTracker.Models.Locations.Map;

namespace OpenTracker.ViewModels.MapLocations;

/// <summary>
/// This interface contains the entrance map location control ViewModel data.
/// </summary>
public interface IEntranceMapLocationVM : IShapedMapLocationVMBase
{
    IMapLocation MapLocation { get; }

    void ConnectLocation(IMapLocation mapLocation);

    delegate IEntranceMapLocationVM Factory(
        IMapLocation mapLocation, double offsetX, double offsetY, List<Point> points);
}