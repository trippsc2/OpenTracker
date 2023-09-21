using System.ComponentModel;
using OpenTracker.Models.Items;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Prize;

namespace OpenTracker.Models.PrizePlacements;

/// <summary>
/// This interface contains prize placement data.
/// </summary>
public interface IPrizePlacement 
    : INotifyPropertyChanged, INotifyPropertyChanging, IResettable, ISaveable<PrizePlacementSaveData>
{
    /// <summary>
    /// The nullable <see cref="IItem"/> representing the current prize.
    /// </summary>
    IItem? Prize { get; }

    /// <summary>
    /// A factory for creating new <see cref="IPrizePlacement"/> objects.
    /// </summary>
    /// <param name="startingPrize">
    ///     The nullable <see cref="IItem"/> representing the starting prize.
    /// </param>
    /// <returns>
    ///     A new <see cref="IPrizePlacement"/> object.
    /// </returns>
    delegate IPrizePlacement Factory(IItem? startingPrize = null);

    /// <summary>
    /// Returns whether the prize can be cycled.
    /// </summary>
    /// <returns>
    ///     A <see cref="bool"/> representing whether the prize can be cycled.
    /// </returns>
    bool CanCycle();

    /// <summary>
    /// Cycles the prize.
    /// </summary>
    /// <param name="reverse">
    ///     A <see cref="bool"/> representing whether to cycle the prize in reverse.
    /// </param>
    void Cycle(bool reverse = false);
        
    /// <summary>
    /// Returns a new <see cref="IChangePrize"/> object.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IChangePrize"/> object.
    /// </returns>
    IUndoable CreateChangePrizeAction();
}