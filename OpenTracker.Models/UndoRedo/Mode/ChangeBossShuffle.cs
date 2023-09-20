using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.UndoRedo.Mode;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to change the <see cref="IMode.BossShuffle"/> property.
/// </summary>
[DependencyInjection]
public sealed class ChangeBossShuffle : IChangeBossShuffle
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
    ///     A <see cref="bool"/> representing the new <see cref="IMode.BossShuffle"/> value.
    /// </param>
    public ChangeBossShuffle(IMode mode, bool newValue)
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
        _previousValue = _mode.BossShuffle;
        _mode.BossShuffle = _newValue;
    }

    public void ExecuteUndo()
    {
        _mode.BossShuffle = _previousValue;
    }
}