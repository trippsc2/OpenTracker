using System;

namespace OpenTracker.Models.Items
{
    public class CappedItem : Item
    {
        private readonly int _maximum;

        public override int Current
        {
            get => base.Current;
            set
            {
                if (value > _maximum)
                {
                    value = 0;
                }

                if (value < 0)
                {
                    value = _maximum;
                }

                base.Current = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="starting">
        /// A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="maximum">
        /// A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        public CappedItem(int starting, int maximum) : base(starting)
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
    }
}
