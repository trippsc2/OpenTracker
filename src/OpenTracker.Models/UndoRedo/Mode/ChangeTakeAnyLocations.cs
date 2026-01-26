using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to change the <see cref="IMode.TakeAnyLocations"/>
/// property.
/// </summary>
public class ChangeTakeAnyLocations : IChangeTakeAnyLocations
{
    private readonly IMode _mode;
    private readonly bool _newValue;
    private bool _previousValue;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new <see cref="IMode.TakeAnyLocations"/> value.
    /// </param>
    public ChangeTakeAnyLocations(IMode mode, bool newValue)
    {
        _mode = mode;
        _newValue = newValue;
    }

    public bool CanExecute()
    {
        return true;
    }

    public void ExecuteDo()
    {
        _previousValue = _mode.TakeAnyLocations;
        _mode.TakeAnyLocations = _newValue;
    }

    public void ExecuteUndo()
    {
        _mode.TakeAnyLocations = _previousValue;
    }
}