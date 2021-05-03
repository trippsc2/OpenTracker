using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.UndoRedo.Boss
{
    /// <summary>
    /// This interface contains undoable action to change the boss of a dungeon.
    /// </summary>
    public interface IChangeBoss : IUndoable
    {
        delegate IChangeBoss Factory(IBossPlacement bossPlacement, BossType? newValue);
    }
}