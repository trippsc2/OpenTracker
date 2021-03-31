using System.ComponentModel;
using OpenTracker.Models.Items.Keys;

namespace OpenTracker.Models.Requirements.Item
{
    /// <summary>
    /// This class contains small key requirement data.
    /// </summary>
    public class SmallKeyRequirement : BooleanRequirement, ISmallKeyRequirement
    {
        private readonly ISmallKeyItem _item;
        private readonly int _count;

        public delegate SmallKeyRequirement Factory(ISmallKeyItem item, int count = 1);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item of the requirement.
        /// </param>
        /// <param name="count">
        /// A 32-bit integer representing the number of the item required.
        /// </param>
        public SmallKeyRequirement(ISmallKeyItem item, int count = 1)
        {
            _item = item;
            _count = count;

            UpdateValue();
            
            _item.PropertyChanged += OnItemChanged;
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
            if (e.PropertyName == nameof(ISmallKeyItem.EffectiveCurrent))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _item.EffectiveCurrent >= _count;
        }
    }
}
