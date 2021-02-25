using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains small key requirement data.
    /// </summary>
    public class SmallKeyRequirement : BooleanRequirement
    {
        private readonly IMode _mode;
        private readonly IItem _genericKey;
        private readonly IItem _item;
        private readonly int _count;

        public delegate SmallKeyRequirement Factory(IItem item, int count = 1);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="items">
        /// The item dictionary.
        /// </param>
        /// <param name="item">
        /// The item of the requirement.
        /// </param>
        /// <param name="count">
        /// A 32-bit integer representing the number of the item required.
        /// </param>
        public SmallKeyRequirement(
            IMode mode, IItemDictionary items, IItem item, int count = 1)
        {
            _mode = mode;
            _genericKey = items[ItemType.SmallKey];
            _item = item;
            _count = count;

            _mode.PropertyChanged += OnModeChanged;
            _genericKey.PropertyChanged += OnItemChanged;
            _item.PropertyChanged += OnItemChanged;

            UpdateValue();
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
            if (e.PropertyName == nameof(IMode.GenericKeys))
            {
                UpdateValue();
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
        private void OnItemChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            var keyTotal = _item.Current + (_mode.GenericKeys ? _genericKey.Current : 0);

            return keyTotal >= _count;
        }
    }
}
