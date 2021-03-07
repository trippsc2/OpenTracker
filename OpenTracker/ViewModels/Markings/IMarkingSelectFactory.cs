using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;

namespace OpenTracker.ViewModels.Markings
{
    public interface IMarkingSelectFactory
    {
        IMarkingSelectVM GetMarkingSelectVM(IMarkableSection section);
        INoteMarkingSelectVM GetNoteMarkingSelectVM(IMarking marking, ILocation location);
    }
}