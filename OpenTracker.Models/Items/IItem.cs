using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;
using ReactiveUI;

namespace OpenTracker.Models.Items;

/// <summary>
/// This interface contains item data.
/// </summary>
public interface IItem : IReactiveObject, IResettable, ISaveable<ItemSaveData>
{
    /// <summary>
    /// A <see cref="int"/> representing the current item count.
    /// </summary>
    int Current { get; set; }
        
    /// <summary>
    /// A factory for creating new <see cref="IItem"/> objects.
    /// </summary>
    /// <param name="starting">
    ///     A <see cref="int"/> representing the starting value.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IItem"/> object.
    /// </returns>
    delegate IItem Factory(int starting, IAutoTrackValue? autoTrackValue);

    /// <summary>
    /// Creates a new <see cref="IAddItem"/> object.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IAddItem"/> object.
    /// </returns>
    IUndoable CreateAddItemAction();

    /// <summary>
    /// Returns whether an item can be added.
    /// </summary>
    /// <returns>
    ///     A <see cref="bool"/> representing whether an item can be added.
    /// </returns>
    bool CanAdd();

    /// <summary>
    /// Adds an item.
    /// </summary>
    void Add();

    /// <summary>
    /// Creates a new <see cref="IRemoveItem"/> object.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IRemoveItem"/> object.
    /// </returns>
    IUndoable CreateRemoveItemAction();
        
    /// <summary>
    /// Returns whether an item can be removed.
    /// </summary>
    /// <returns>
    ///     A <see cref="bool"/> representing whether an item can be removed.
    /// </returns>
    bool CanRemove();
        
    /// <summary>
    /// Removes an item.
    /// </summary>
    void Remove();
}