using System.Collections.Generic;
using OpenTracker.ViewModels.MapLocations;
using OpenTracker.ViewModels.Maps;

namespace OpenTracker.ViewModels.Areas
{
    public interface IMapAreaFactory
    {
        List<IMapVM> GetMapControlVMs();
        List<IMapLocationVM> GetMapLocationControlVMs();
    }
}