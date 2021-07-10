using System;
using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;
using ReactiveUI;

namespace OpenTracker.Models.Items.Keys
{
    /// <summary>
    /// This class contains the big key item data.
    /// </summary>
    public class BigKeyItem : Item, IBigKeyItem
    {
        private readonly IMode _mode;

        private readonly ICycleItem.Factory _cycleItemFactory;

        private readonly int _nonKeyDropMaximum;
        private readonly int _keyDropMaximum;
        
        public int Maximum => _mode.KeyDropShuffle ? _keyDropMaximum : _nonKeyDropMaximum;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        ///     The <see cref="IMode"/> data.
        /// </param>
        /// <param name="saveLoadManager">
        ///     The <see cref="ISaveLoadManager"/>.
        /// </param>
        /// <param name="addItemFactory">
        ///     An Autofac factory for creating new <see cref="IAddItem"/> objects.
        /// </param>
        /// <param name="removeItemFactory">
        ///     An Autofac factory for creating new <see cref="IRemoveItem"/> objects.
        /// </param>
        /// <param name="cycleItemFactory">
        ///     An Autofac factory for creating new <see cref="ICycleItem"/> objects.
        /// </param>
        /// <param name="nonKeyDropMaximum">
        ///     A <see cref="int"/> representing the item maximum when key drop shuffle is disabled.
        /// </param>
        /// <param name="keyDropMaximum">
        ///     A <see cref="int"/> representing the item maximum when key drop shuffle is enabled.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The nullable <see cref="IAutoTrackValue"/>.
        /// </param>
        public BigKeyItem(
            IMode mode, ISaveLoadManager saveLoadManager, IAddItem.Factory addItemFactory,
            IRemoveItem.Factory removeItemFactory, ICycleItem.Factory cycleItemFactory, int nonKeyDropMaximum,
            int keyDropMaximum, IAutoTrackValue? autoTrackValue)
            : base(saveLoadManager, addItemFactory, removeItemFactory, 0, autoTrackValue)
        {
            _mode = mode;

            _cycleItemFactory = cycleItemFactory;

            _nonKeyDropMaximum = nonKeyDropMaximum;
            _keyDropMaximum = keyDropMaximum;
            
            _mode.PropertyChanged += OnModeChanged;
        }

        public override bool CanAdd()
        {
            return Current < Maximum;
        }

        public override void Add()
        {
            if (Current >= Maximum)
            {
                throw new Exception("Cannot add item, because it is already at maximum.");
            }

            base.Add();
        }

        public override void Remove()
        {
            if (Current <= 0)
            {
                throw new Exception("Cannot remove item, because it is already 0.");
            }
            
            base.Remove();
        }

        public IUndoable CreateCycleItemAction()
        {
            return _cycleItemFactory(this);
        }

        public void Cycle(bool reverse = false)
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

        public IList<bool> GetKeyValues()
        {
            if (Maximum == 0)
            {
                return new List<bool> {false};
            }
            
            return _mode.BigKeyShuffle ? new List<bool> {Current > 0} 
                : new List<bool> {false, true};
        }

        /// <summary>
        /// Subscribes to the <see cref="IMode.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(IMode.KeyDropShuffle))
            {
                return;
            }
            
            if (Current > Maximum)
            {
                Current = Maximum;
            }

            this.RaisePropertyChanged(nameof(Maximum));
        }
    }
}