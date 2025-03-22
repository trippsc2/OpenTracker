using System.Collections.Generic;
using OpenTracker.Models.Sections;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    public interface ISectionVM
    {
        delegate ISectionVM Factory(ISection section, List<ISectionIconVM> icons);
    }
}