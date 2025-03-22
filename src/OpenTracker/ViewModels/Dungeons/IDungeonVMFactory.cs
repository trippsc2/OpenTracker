using System.Collections.Generic;
using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.Dungeons
{
    public interface IDungeonVMFactory
    {
        List<IDungeonItemVM> GetDungeonItemVMs(LocationID id);
    }
}