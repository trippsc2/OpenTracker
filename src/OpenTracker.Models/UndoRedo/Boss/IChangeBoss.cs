using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.UndoRedo.Boss;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to change the <see cref="BossType"/> of a
/// <see cref="IBossPlacement"/>.
/// </summary>
public interface IChangeBoss : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IChangeBoss"/> objects.
    /// </summary>
    /// <param name="bossPlacement">
    ///     The <see cref="IBossPlacement"/>.
    /// </param>
    /// <param name="newValue">
    ///     The new nullable <see cref="BossType"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IChangeBoss"/> object.
    /// </returns>
    delegate IChangeBoss Factory(IBossPlacement bossPlacement, BossType? newValue);
}