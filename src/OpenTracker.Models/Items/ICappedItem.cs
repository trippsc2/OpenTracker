using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;

namespace OpenTracker.Models.Items;

/// <summary>
/// This interface contains item data with a maximum value.
/// </summary>
public interface ICappedItem : IItem
{
    /// <summary>
    /// A <see cref="int"/> representing the maximum value.
    /// </summary>
    int Maximum { get; }
        
    /// <summary>
    /// A factory for creating new <see cref="ICappedItem"/> objects.
    /// </summary>
    /// <param name="starting">
    ///     A <see cref="int"/> representing the starting value.
    /// </param>
    /// <param name="maximum">
    ///     A <see cref="int"/> representing the maximum.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="ICappedItem"/> object.
    /// </returns>
    new delegate ICappedItem Factory(int starting, int maximum, IAutoTrackValue? autoTrackValue);

    /// <summary>
    /// Returns a new <see cref="ICycleItem"/> object.
    /// </summary>
    /// <returns>
    ///     A new <see cref="ICycleItem"/> object.
    /// </returns>
    IUndoable CreateCycleItemAction();

    /// <summary>
    /// Cycles the item.
    /// </summary>
    /// <param name="reverse">
    ///     A <see cref="bool"/> representing whether to cycle in reverse.
    /// </param>
    void Cycle(bool reverse = false);
}