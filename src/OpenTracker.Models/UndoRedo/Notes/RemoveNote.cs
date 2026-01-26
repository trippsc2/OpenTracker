using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;

namespace OpenTracker.Models.UndoRedo.Notes;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to remove a note to a <see cref="ILocation"/>.
/// </summary>
public class RemoveNote : IRemoveNote
{
    private readonly ILocation _location;
    private readonly IMarking _note;
    private int _index;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="note">
    ///     The <see cref="IMarking"/> representing the note to be removed.
    /// </param>
    /// <param name="location">
    ///     The <see cref="ILocation"/>.
    /// </param>
    public RemoveNote(IMarking note, ILocation location)
    {
        _note = note;
        _location = location;
    }

    public bool CanExecute()
    {
        return true;
    }

    public void ExecuteDo()
    {
        _index = _location.Notes.IndexOf(_note);
        _location.Notes.Remove(_note);
    }

    public void ExecuteUndo()
    {
        _location.Notes.Insert(_index, _note);
    }
}