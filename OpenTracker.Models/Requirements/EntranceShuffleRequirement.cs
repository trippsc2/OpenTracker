using OpenTracker.Models.Modes;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for entrance shuffle requirements.
    /// </summary>
    public class EntranceShuffleRequirement : BooleanRequirement
    {
        private readonly IMode _mode;
        private readonly EntranceShuffle _expectedValue;

        public delegate EntranceShuffleRequirement Factory(EntranceShuffle expectedValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="expectedValue">
        /// The required entrance shuffle value.
        /// </param>
        public EntranceShuffleRequirement(IMode mode, EntranceShuffle expectedValue)
        {
            _mode = mode;
            _expectedValue = expectedValue;

            _mode.PropertyChanged += OnModeChanged;

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
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
