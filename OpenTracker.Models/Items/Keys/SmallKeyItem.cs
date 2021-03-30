using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;
using ReactiveUI;

namespace OpenTracker.Models.Items.Keys
{
    /// <summary>
    ///     This class contains the item data for small keys.
    /// </summary>
    public class SmallKeyItem : Item, ISmallKeyItem
    {
        private readonly IMode _mode;

        private readonly ICycleItem.Factory _cycleItemFactory;

        private readonly IItem _genericKey;
        private readonly int _nonKeyDropMaximum;
        private readonly int _keyDropMaximum;
        
        public int Maximum => _mode.KeyDropShuffle ? _keyDropMaximum : _nonKeyDropMaximum;

        private int _effectiveCurrent;
        public int EffectiveCurrent
        {
            get => _effectiveCurrent;
            private set => this.RaiseAndSetIfChanged(ref _effectiveCurrent, value);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode settings.
        /// </param>
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
        /// <param name="genericKey">
        ///     The generic key item.
        /// </param>
        /// <param name="nonKeyDropMaximum">
        ///     A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        /// <param name="keyDropMaximum">
        ///     A 32-bit signed integer representing the delta maximum for key drop shuffle of the item.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-track value.
        /// </param>
        public SmallKeyItem(
            IMode mode, ISaveLoadManager saveLoadManager, IAddItem.Factory addItemFactory,
            IRemoveItem.Factory removeItemFactory, ICycleItem.Factory cycleItemFactory, IItem genericKey,
            int nonKeyDropMaximum, int keyDropMaximum, IAutoTrackValue? autoTrackValue)
            : base(saveLoadManager, addItemFactory, removeItemFactory, 0, autoTrackValue)
        {
            _mode = mode;

            _cycleItemFactory = cycleItemFactory;

            _genericKey = genericKey;
            _nonKeyDropMaximum = nonKeyDropMaximum;
            _keyDropMaximum = keyDropMaximum;
            
            UpdateEffectiveCurrent();

            PropertyChanged += OnPropertyChanged;
            _mode.PropertyChanged += OnModeChanged;
            _genericKey.PropertyChanged += OnGenericKeyChanged;
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

        public List<int> GetKeyValues()
        {
            return _mode.SmallKeyShuffle ? new List<int> {EffectiveCurrent}
                : Enumerable.Range(0, Maximum).ToList();
        }
        
        /// <summary>
        ///     Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateEffectiveCurrent();
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IMode.KeyDropShuffle):
                {
                    if (Current > Maximum)
                    {
                        Current = Maximum;
                    }

                    this.RaisePropertyChanged(nameof(Maximum));
                }
                    break;
                case nameof(IMode.GenericKeys):
                    UpdateEffectiveCurrent();
                    break;
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IItem interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnGenericKeyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current) && _mode.GenericKeys)
            {
                UpdateEffectiveCurrent();
            }
        }

        /// <summary>
        ///     Updates the value of the EffectiveCurrent property.
        /// </summary>
        private void UpdateEffectiveCurrent()
        {
            var effectiveCurrent = Current;

            if (_mode.GenericKeys)
            {
                effectiveCurrent += _genericKey.Current;
            }
            
            EffectiveCurrent = Math.Min(Maximum, effectiveCurrent);
        }
    }
}