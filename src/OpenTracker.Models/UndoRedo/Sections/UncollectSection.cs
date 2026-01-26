using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo.Sections;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to uncollect a <see cref="ISection"/>.
/// </summary>
public class UncollectSection : IUncollectSection
{
    private readonly ISection _section;

    private bool _previousUserManipulated;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="section">
    ///     The <see cref="ISection"/>.
    /// </param>
    public UncollectSection(ISection section)
    {
        _section = section;
    }

    public bool CanExecute()
    {
        return _section.CanBeUncleared();
    }

    public void ExecuteDo()
    {
        _previousUserManipulated = _section.UserManipulated;
        _section.UserManipulated = true;
        _section.Available++;
    }

    public void ExecuteUndo()
    {
        _section.UserManipulated = _previousUserManipulated;
        _section.Available--;
    }
}