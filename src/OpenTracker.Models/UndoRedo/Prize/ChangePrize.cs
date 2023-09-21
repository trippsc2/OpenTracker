using OpenTracker.Models.PrizePlacements;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.UndoRedo.Prize;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to change the prize of a <see cref="IPrizePlacement"/>.
/// </summary>
[DependencyInjection]
public sealed class ChangePrize : IChangePrize
{
    private readonly IPrizePlacement _prizePlacement;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="prizePlacement">
    ///     The <see cref="IPrizePlacement"/>.
    /// </param>
    public ChangePrize(IPrizePlacement prizePlacement)
    {
        _prizePlacement = prizePlacement;
    }

    public bool CanExecute()
    {
        return _prizePlacement.CanCycle();
    }

    public void ExecuteDo()
    {
        _prizePlacement.Cycle();
    }

    public void ExecuteUndo()
    {
        _prizePlacement.Cycle(true);
    }
}