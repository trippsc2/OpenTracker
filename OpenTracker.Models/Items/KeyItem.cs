using System;
using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;
using ReactiveUI;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains small key item data.
    /// </summary>
    public class KeyItem : Item, IKeyItem
    {
        private readonly IMode _mode;

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
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
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
        /// <param name="genericKey">
        /// The generic key item.
        /// </param>
        /// <param name="nonKeyDropMaximum">
        /// A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        /// <param name="keyDropMaximum">
        /// A 32-bit signed integer representing the delta maximum for key drop shuffle of the item.
        /// </param>
        /// <param name="autoTrackValue">
        /// The auto track value.
        /// </param>
        public KeyItem(
            IMode mode, ISaveLoadManager saveLoadManager, IUndoRedoManager undoRedoManager,
            IAddItem.Factory addItemFactory, IRemoveItem.Factory removeItemFactory, IItem genericKey,
            int nonKeyDropMaximum, int keyDropMaximum, IAutoTrackValue? autoTrackValue)
            : base(saveLoadManager, undoRedoManager, addItemFactory, removeItemFactory, 0, autoTrackValue)
        {
            _mode = mode;

            _genericKey = genericKey;
            _nonKeyDropMaximum = nonKeyDropMaximum;
            _keyDropMaximum = keyDropMaximum;
            
            UpdateEffectiveCurrent();

            PropertyChanged += OnPropertyChanged;
            _mode.PropertyChanged += OnModeChanged;
            _genericKey.PropertyChanged += OnGenericKeyChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateEffectiveCurrent();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
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

                    this.RaisePropertyChanged(nameof(Current));
                }
                    break;
                case nameof(IMode.GenericKeys):
                    UpdateEffectiveCurrent();
                    break;
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItem interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnGenericKeyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current) && _mode.GenericKeys)
            {
                UpdateEffectiveCurrent();
            }
        }

        /// <summary>
        /// Updates the value of the EffectiveCurrent property.
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
