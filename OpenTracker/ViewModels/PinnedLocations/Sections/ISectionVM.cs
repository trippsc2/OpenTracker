using OpenTracker.Models.Sections;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    public interface ISectionVM
    {
        delegate ISectionVM Factory(ISection section, List<ISectionIconVMBase> icons);
    }
}