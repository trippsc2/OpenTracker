using System;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains item data with a maximum value.
    /// </summary>
    public class CappedItem : Item, ICappedItem
    {
        public int Maximum { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="addItemFactory">
        /// An Autofac factory for creating undoable actions to add items.
        /// </param>
        /// <param name="removeItemFactory">
        /// An Autofac factory for creating undoable actions to remove items.
        /// </param>
        /// <param name="starting">
        /// A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="maximum">
        /// A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        /// <param name="autoTrackValue">
        /// The auto track value.
        /// </param>
        public CappedItem(
            ISaveLoadManager saveLoadManager, IUndoRedoManager undoRedoManager, IAddItem.Factory addItemFactory,
            IRemoveItem.Factory removeItemFactory, int starting, int maximum, IAutoTrackValue? autoTrackValue)
            : base(saveLoadManager, undoRedoManager, addItemFactory, removeItemFactory, starting, autoTrackValue)
        {
            if (starting > maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(starting));
            }

            Maximum = maximum;
        }

        /// <summary>
        /// Returns whether an item can be added.
        /// </summary>
        /// <returns>
        /// A boolean representing whether an item can be added.
        /// </returns>
        public override bool CanAdd()
        {
            return Current < Maximum;
        }

        /// <summary>
        /// Adds an item.
        /// </summary>
        public override void Add()
        {
            if (Current < Maximum)
            {
                base.Add();
                return;
            }

            Current = 0;
        }

        /// <summary>
        /// Removes an item.
        /// </summary>
        public override void Remove()
        {
            if (Current > 0)
            {
                base.Remove();
                return;
            }

            Current = Maximum;
        }
    }
}
