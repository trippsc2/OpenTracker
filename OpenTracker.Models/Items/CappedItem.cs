using System;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.SaveLoad;

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
        /// <param name="starting">
        /// A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="maximum">
        /// A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        /// <param name="autoTrackValue">
        /// The auto track value.
        /// </param>
        public CappedItem(ISaveLoadManager saveLoadManager, int starting, int maximum, IAutoTrackValue? autoTrackValue)
            : base(saveLoadManager, starting, autoTrackValue)
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
                Current = Maximum;
            }
        }
    }
}
