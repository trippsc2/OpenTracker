using OpenTracker.Interfaces;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings;

namespace OpenTracker.ViewModels.PinnedLocations.Notes
{
    public interface IPinnedLocationNoteVM : IModelWrapper, IClickHandler
    {
        delegate IPinnedLocationNoteVM Factory(
            IMarking marking, INoteMarkingSelectVM markingSelect);
    }
}
