using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings;

namespace OpenTracker.ViewModels.PinnedLocations.Notes;

public class PinnedLocationNoteVMCollection : ViewModelCollection<IPinnedLocationNoteVM, IMarking>,
    IPinnedLocationNoteVMCollection
{
    private readonly IMarkingSelectFactory _markingSelectFactory;
    private readonly IPinnedLocationNoteVM.Factory _factory;

    private readonly ILocation _location;

    public delegate IPinnedLocationNoteVMCollection Factory(ILocation location);

    public PinnedLocationNoteVMCollection(
        IMarkingSelectFactory markingSelectFactory, IPinnedLocationNoteVM.Factory factory, ILocation location)
        : base(location.Notes)
    {
        _markingSelectFactory = markingSelectFactory;
        _factory = factory;

        _location = location;
    }

    protected override IPinnedLocationNoteVM CreateViewModel(IMarking model)
    {
        return _factory(model, _markingSelectFactory.GetNoteMarkingSelectVM(model, _location));
    }
}