using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.UndoRedo.Mode;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to change the <see cref="IMode.EntranceShuffle"/>
/// property.
/// </summary>
[DependencyInjection]
public sealed class ChangeEntranceShuffle : IChangeEntranceShuffle
{
    private readonly IMode _mode;
    private readonly EntranceShuffle _newValue;

    private EntranceShuffle _previousValue;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="newValue">
    ///     A <see cref="EntranceShuffle"/> representing the new <see cref="IMode.EntranceShuffle"/> value.
    /// </param>
    public ChangeEntranceShuffle(IMode mode, EntranceShuffle newValue)
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
        _previousValue = _mode.EntranceShuffle;
        _mode.EntranceShuffle = _newValue;
    }

    public void ExecuteUndo()
    {
        _mode.EntranceShuffle = _previousValue;
    }
}