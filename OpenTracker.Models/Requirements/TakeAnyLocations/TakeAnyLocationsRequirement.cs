using System.ComponentModel;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.TakeAnyLocations
{
    /// <summary>
    /// This class contains the <see cref="IMode.TakeAnyLocations"/> <see cref="IRequirement"/> data.
    /// </summary>
    public class TakeAnyLocationsRequirement : BooleanRequirement, ITakeAnyLocationsRequirement
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
        ///     A <see cref="bool"/> expected <see cref="IMode.TakeAnyLocations"/> value.
        /// </param>
        public TakeAnyLocationsRequirement(IMode mode, bool expectedValue)
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
            if (e.PropertyName == nameof(IMode.TakeAnyLocations))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _mode.TakeAnyLocations == _expectedValue;
        }
    }
}
