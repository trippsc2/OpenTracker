using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.UndoRedo.Notes;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to add a note to a <see cref="ILocation"/>.
/// </summary>
[DependencyInjection]
public sealed class AddNote : IAddNote
{
    private readonly IMarking.Factory _factory;
    private readonly ILocationNoteCollection _notes;
    private IMarking? _note;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IMarking"/> objects.
    /// </param>
    /// <param name="location">
    ///     The <see cref="ILocation"/>.
    /// </param>
    public AddNote(IMarking.Factory factory, ILocation location)
    {
        _factory = factory;
        _notes = location.Notes;
    }

    public bool CanExecute()
    {
        return _notes.Count < 4;
    }

    public void ExecuteDo()
    {
        _note = _factory();
        _notes.Add(_note);
    }

    public void ExecuteUndo()
    {
        _notes.Remove(_note!);
    }
}