using System.ComponentModel;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains key drop shuffle setting requirement data.
    /// </summary>
    public class KeyDropShuffleRequirement : BooleanRequirement
    {
        private readonly IMode _mode;
        private readonly bool _expectedValue;

        public delegate KeyDropShuffleRequirement Factory(bool expectedValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="expectedValue">
        /// A boolean expected key door shuffle value.
        /// </param>
        public KeyDropShuffleRequirement(IMode mode, bool expectedValue)
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
            if (e.PropertyName == nameof(IMode.KeyDropShuffle))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _mode.KeyDropShuffle == _expectedValue;
        }
    }
}
