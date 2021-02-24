using OpenTracker.Models.Sections;

namespace OpenTracker.ViewModels.Maps.Locations
{
    public interface IMarkingMapLocationVM
    {
        delegate IMarkingMapLocationVM Factory(IMarkableSection section);
    }
}