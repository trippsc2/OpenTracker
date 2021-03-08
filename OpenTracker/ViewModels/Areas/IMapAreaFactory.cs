using OpenTracker.ViewModels.Maps;
using OpenTracker.ViewModels.Maps.Locations;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Areas
{
    public interface IMapAreaFactory
    {
        List<IMapVM> GetMapControlVMs();
        List<IMapLocationVMBase> GetMapLocationControlVMs();
    }
}