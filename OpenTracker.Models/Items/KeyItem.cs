using System;
using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains small key item data.
    /// </summary>
    public class KeyItem : Item
    {
        private readonly IMode _mode;
        private readonly int _maximum;
        private readonly int _keyDropMaximumDelta;

        private int EffectiveMaximum => _mode.KeyDropShuffle ? _maximum + _keyDropMaximumDelta : _maximum;

        public delegate KeyItem Factory(
            int maximum, int keyDropMaximumDelta, int starting, IAutoTrackValue? autoTrackValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="maximum">
        /// A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        /// <param name="keyDropMaximumDelta">
        /// A 32-bit signed integer representing the delta maximum for key drop shuffle of the item.
        /// </param>
        /// <param name="starting">
        /// A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="autoTrackValue">
        /// The auto track value.
        /// </param>
        public KeyItem(
            ISaveLoadManager saveLoadManager, IMode mode, int maximum, int keyDropMaximumDelta, int starting,
            IAutoTrackValue? autoTrackValue) : base(saveLoadManager, starting, autoTrackValue)
        {
            if (starting > maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(starting));
            }

            _mode = mode;
            _maximum = maximum;
            _keyDropMaximumDelta = keyDropMaximumDelta;

            _mode.PropertyChanged += OnModeChanged;
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
            if (e.PropertyName == nameof(IMode.KeyDropShuffle))
            {
                if (Current > EffectiveMaximum)
                {
                    Current = EffectiveMaximum;
                }

                this.RaisePropertyChanged(nameof(Current));
            }
        }

        /// <summary>
        /// Returns whether an item can be added.
        /// </summary>
        /// <returns>
        /// A boolean representing whether an item can be added.
        /// </returns>
        public override bool CanAdd()
        {
            return Current < EffectiveMaximum;
        }

        /// <summary>
        /// Adds an item.
        /// </summary>
        public override void Add()
        {
            if (Current < EffectiveMaximum)
            {
                base.Add();
            }
            else
            {
                Current = 0;
            }
        }

        /// <summary>
        /// Removes an item.
        /// </summary>
        public override void Remove()
        {
            if (Current > 0)
            {
                base.Remove();
            }
            else
            {
                Current = EffectiveMaximum;
            }
        }
    }
}
