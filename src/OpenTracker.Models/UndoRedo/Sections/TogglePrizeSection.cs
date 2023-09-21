using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Boss;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.UndoRedo.Sections;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to toggle the <see cref="IPrizeSection"/>.
/// </summary>
[DependencyInjection]
public sealed class TogglePrizeSection : ITogglePrizeSection
{
    private readonly ISection _section;
    private readonly bool _force;

    private bool _previousUserManipulated;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="section">
    ///     The <see cref="ISection"/>.
    /// </param>
    /// <param name="force">
    ///     A <see cref="bool"/> representing whether the accessibility logic should be obeyed.
    /// </param>
    public TogglePrizeSection(ISection section, bool force)
    {
        _section = section;
        _force = force;
    }

    public bool CanExecute()
    {
        return _section.CanBeUncleared() || _section.CanBeCleared(_force);
    }

    public void ExecuteDo()
    {
        _section.Available = _section.IsAvailable() ? 0 : 1;
        _previousUserManipulated = _section.UserManipulated;
        _section.UserManipulated = true;
    }

    public void ExecuteUndo()
    {
        _section.Available = _section.IsAvailable() ? 0 : 1;
        _section.UserManipulated = _previousUserManipulated;
    }
}