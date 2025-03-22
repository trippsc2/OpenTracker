using OpenTracker.Models.Sections;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    public interface ISectionVMFactory
    {
        ISectionVM GetSectionVM(ISection section);
    }
}