using System;
using System.ComponentModel;
using OpenTracker.Models.Items;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains item exact value requirement data.
    /// </summary>
    public class ItemExactRequirement : BooleanRequirement
    {
        private readonly IItem _item;
        private readonly int _count;

        public delegate ItemExactRequirement Factory(IItem item, int count);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item of the requirement.
        /// </param>
        /// <param name="count">
        /// A 32-bit integer representing the number of the item required.
        /// </param>
        public ItemExactRequirement(IItem item, int count)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _count = count;

            _item.PropertyChanged += OnItemChanged;

            UpdateValue();
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
        private void OnItemChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _item.Current == _count;
        }
    }
}
