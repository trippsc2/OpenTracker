using OpenTracker.Models.AutoTracking.AutotrackValues;
using OpenTracker.Models.Modes;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Items
{
    public class KeyItem : Item
    {
        private readonly int _maximum;
        private readonly int _keyDropMaximumDelta;

        private int EffectiveMaximum =>
            Mode.Instance.KeyDropShuffle ? _maximum + _keyDropMaximumDelta : _maximum;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="starting">
        /// A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="maximum">
        /// A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        /// <param name="keyDropMaximumDelta">
        /// A 32-bit signed integer representing the delta maximum for key drop shuffle of the item.
        /// </param>
        /// <param name="autoTrackValue">
        /// The autotracking value for the item.
        /// </param>
        public KeyItem(int starting, int maximum, int keyDropMaximumDelta, IAutoTrackValue autoTrackValue) : base(starting, autoTrackValue)
        {
            if (starting > maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(starting));
            }

            _maximum = maximum;
            _keyDropMaximumDelta = keyDropMaximumDelta;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.KeyDropShuffle))
            {
                if (Current > EffectiveMaximum)
                {
                    Current = EffectiveMaximum;
                }

                OnPropertyChanged(nameof(Current));
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
