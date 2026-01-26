using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to change the <see cref="IMode.WorldState"/> property.
/// </summary>
public class ChangeWorldState : IChangeWorldState
{
    private readonly IMode _mode;
    private readonly WorldState _newValue;
    private WorldState _previousValue;
    private ItemPlacement _previousItemPlacement;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="newValue">
    ///     A <see cref="WorldState"/> representing the new <see cref="IMode.WorldState"/> value.
    /// </param>
    public ChangeWorldState(IMode mode, WorldState newValue)
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
        _previousValue = _mode.WorldState;
        _previousItemPlacement = _mode.ItemPlacement;
        _mode.WorldState = _newValue;
    }

    public void ExecuteUndo()
    {
        _mode.WorldState = _previousValue;
        _mode.ItemPlacement = _previousItemPlacement;
    }
}