using System;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;

namespace OpenTracker.Models.Items
{
    /// <summary>
    ///     This class contains item data with a maximum value.
    /// </summary>
    public class CappedItem : Item, ICappedItem
    {
        private readonly ICycleItem.Factory _cycleItemFactory;
        
        public int Maximum { get; }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        ///     The save/load manager.
        /// </param>
        /// <param name="addItemFactory">
        ///     An Autofac factory for creating undoable actions to add items.
        /// </param>
        /// <param name="removeItemFactory">
        ///     An Autofac factory for creating undoable actions to remove items.
        /// </param>
        /// <param name="cycleItemFactory">
        ///     An Autofac factory for creating undoable actions to cycle the item.
        /// </param>
        /// <param name="starting">
        ///     A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="maximum">
        ///     A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-track value.
        /// </param>
        public CappedItem(
            ISaveLoadManager saveLoadManager, IAddItem.Factory addItemFactory, IRemoveItem.Factory removeItemFactory,
            ICycleItem.Factory cycleItemFactory, int starting, int maximum, IAutoTrackValue? autoTrackValue)
            : base(saveLoadManager, addItemFactory, removeItemFactory, starting, autoTrackValue)
        {
            if (starting > maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(starting));
            }

            _cycleItemFactory = cycleItemFactory;

            Maximum = maximum;
        }

        public override bool CanAdd()
        {
            return Current < Maximum;
        }

        public override void Add()
        {
            if (!CanAdd())
            {
                throw new Exception("Item cannot be added, because it is already at maximum.");
            }
            
            base.Add();
        }

        public IUndoable CreateCycleItemAction()
        {
            return _cycleItemFactory(this);
        }

        public virtual void Cycle(bool reverse = false)
        {
            if (reverse)
            {
                if (CanRemove())
                {
                    Remove();
                    return;
                }

                Current = Maximum;
                return;
            }

            if (CanAdd())
            {
                Add();
                return;
            }

            Current = 0;
        }
    }
}
