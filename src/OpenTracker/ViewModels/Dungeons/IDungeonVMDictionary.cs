using System.Collections.Generic;
using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.Dungeons
{
    public interface IDungeonVMDictionary : IDictionary<LocationID, List<IDungeonItemVM>>
    {
    }
}