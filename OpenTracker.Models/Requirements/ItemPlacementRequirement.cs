using OpenTracker.Models.Modes;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for Item Placement requirements.
    /// </summary>
    public class ItemPlacementRequirement : BooleanRequirement
    {
        private readonly IMode _mode;
        private readonly ItemPlacement _expectedValue;

        public delegate ItemPlacementRequirement Factory(ItemPlacement expectedValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="expectedValue">
        /// The required item placement.
        /// </param>
        public ItemPlacementRequirement(IMode mode, ItemPlacement expectedValue)
        {
            _mode = mode;
            _expectedValue = expectedValue;

            _mode.PropertyChanged += OnModeChanged;

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.ItemPlacement))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _mode.ItemPlacement == _expectedValue;
        }
    }
}
