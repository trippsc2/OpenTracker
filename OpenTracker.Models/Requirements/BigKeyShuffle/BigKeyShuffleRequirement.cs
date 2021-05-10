using System.ComponentModel;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.BigKeyShuffle
{
    /// <summary>
    /// This class contains <see cref="IMode.BigKeyShuffle"/> requirement data.
    /// </summary>
    public class BigKeyShuffleRequirement : BooleanRequirement, IBigKeyShuffleRequirement
    {
        private readonly IMode _mode;
        private readonly bool _expectedValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        ///     The <see cref="IMode"/> data.
        /// </param>
        /// <param name="expectedValue">
        ///     A <see cref="bool"/> representing the expected value.
        /// </param>
        public BigKeyShuffleRequirement(IMode mode, bool expectedValue)
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
            if (e.PropertyName == nameof(IMode.BigKeyShuffle))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _mode.BigKeyShuffle == _expectedValue;
        }
    }
}
