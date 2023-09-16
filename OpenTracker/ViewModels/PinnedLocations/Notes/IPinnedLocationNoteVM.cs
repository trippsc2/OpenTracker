using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings;

namespace OpenTracker.ViewModels.PinnedLocations.Notes;

/// <summary>
/// This interface contains pinned location note control ViewModel data.
/// </summary>
public interface IPinnedLocationNoteVM : IModelWrapper
{
    delegate IPinnedLocationNoteVM Factory(IMarking marking, INoteMarkingSelectVM markingSelect);
}