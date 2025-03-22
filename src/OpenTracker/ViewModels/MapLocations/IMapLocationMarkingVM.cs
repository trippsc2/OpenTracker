using OpenTracker.Models.Sections;

namespace OpenTracker.ViewModels.MapLocations
{
    public interface IMapLocationMarkingVM
    {
        delegate IMapLocationMarkingVM Factory(ISection section);
    }
}