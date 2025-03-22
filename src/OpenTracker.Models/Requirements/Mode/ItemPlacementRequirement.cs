using System.ComponentModel;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    /// This class contains <see cref="IMode.ItemPlacement"/> <see cref="IRequirement"/> data.
    /// </summary>
    public class ItemPlacementRequirement : BooleanRequirement, IItemPlacementRequirement
    {
        private readonly IMode _mode;
        private readonly ItemPlacement _expectedValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        ///     The <see cref="IMode"/> data.
        /// </param>
        /// <param name="expectedValue">
        ///     A <see cref="ItemPlacement"/> representing the expected <see cref="IMode.ItemPlacement"/> value.
        /// </param>
        public ItemPlacementRequirement(IMode mode, ItemPlacement expectedValue)
        {
            _mode = mode;
            _expectedValue = expectedValue;

            _mode.PropertyChanged += OnModeChanged;

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the <see cref="IMode.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
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
