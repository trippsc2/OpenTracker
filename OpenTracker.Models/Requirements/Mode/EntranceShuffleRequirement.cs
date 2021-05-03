using System.ComponentModel;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    ///     This class contains entrance shuffle requirement data.
    /// </summary>
    public class EntranceShuffleRequirement : BooleanRequirement, IEntranceShuffleRequirement
    {
        private readonly IMode _mode;
        private readonly EntranceShuffle _expectedValue;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode settings.
        /// </param>
        /// <param name="expectedValue">
        ///     The required entrance shuffle value.
        /// </param>
        public EntranceShuffleRequirement(IMode mode, EntranceShuffle expectedValue)
        {
            _mode = mode;
            _expectedValue = expectedValue;

            _mode.PropertyChanged += OnModeChanged;

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
            if (e.PropertyName == nameof(IMode.EntranceShuffle))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _mode.EntranceShuffle == _expectedValue;
        }
    }
}
