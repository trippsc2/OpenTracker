using OpenTracker.Models.PrizePlacements;

namespace OpenTracker.Models.UndoRedo.Prize;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to change the prize of a
/// <see cref="IPrizePlacement"/>.
/// </summary>
public interface IChangePrize : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IChangePrize"/> objects.
    /// </summary>
    /// <param name="prizePlacement">
    ///     The <see cref="IPrizePlacement"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IChangePrize"/> object.
    /// </returns>
    delegate IChangePrize Factory(IPrizePlacement prizePlacement);
}