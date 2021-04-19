using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;

namespace OpenTracker.ViewModels.Markings
{
    public interface IMarkingSelectFactory
    {
        IMarkingSelectVM GetMarkingSelectVM(ISection section);
        INoteMarkingSelectVM GetNoteMarkingSelectVM(IMarking marking, ILocation location);
    }
}