using OpenTracker.Models.Locations;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Items.Small
{
    public interface ISmallItemVMFactory
    {
        List<ISmallItemVMBase> GetSmallItemControlVMs(LocationID location);
    }
}
