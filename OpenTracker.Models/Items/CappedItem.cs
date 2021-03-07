using OpenTracker.Models.AutoTracking.Values;
using System;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains item data with a maximum value.
    /// </summary>
    public class CappedItem : Item
    {
        private readonly int _maximum;

        public new delegate CappedItem Factory(
            int starting, int maximum, IAutoTrackValue? autoTrackValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="starting">
        /// A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="maximum">
        /// A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        /// <param name="autoTrackValue">
        /// The auto track value.
        /// </param>
        public CappedItem(int starting, int maximum, IAutoTrackValue? autoTrackValue)
            : base(starting, autoTrackValue)
        {
            if (starting > maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(starting));
            }

            _maximum = maximum;
        }

        /// <summary>
        /// Returns whether an item can be added.
        /// </summary>
        /// <returns>
        /// A boolean representing whether an item can be added.
        /// </returns>
        public override bool CanAdd()
        {
            return Current < _maximum;
        }

        /// <summary>
        /// Adds an item.
        /// </summary>
        public override void Add()
        {
            if (Current < _maximum)
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
                Current = _maximum;
            }
        }
    }
}
