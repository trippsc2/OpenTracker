using System.ComponentModel;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    ///     This class contains item placement setting requirement data.
    /// </summary>
    public class ItemPlacementRequirement : BooleanRequirement, IItemPlacementRequirement
    {
        private readonly IMode _mode;
        private readonly ItemPlacement _expectedValue;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode settings.
        /// </param>
        /// <param name="expectedValue">
        ///     The expected item placement value.
        /// </param>
        public ItemPlacementRequirement(IMode mode, ItemPlacement expectedValue)
        {
            _mode = mode;
            _expectedValue = expectedValue;

            _mode.PropertyChanged += OnModeChanged;

            UpdateValue();
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
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
