using OpenTracker.ViewModels.Maps.Locations;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Maps
{
    public interface IMapAreaVMFactory
    {
        List<IMapVM> GetMapControlVMs();
        List<IMapLocationVMBase> GetMapLocationControlVMs();
    }
}