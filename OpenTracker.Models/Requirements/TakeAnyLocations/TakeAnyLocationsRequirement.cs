using System.ComponentModel;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.TakeAnyLocations
{
    /// <summary>
    ///     This class contains take any locations requirement data.
    /// </summary>
    public class TakeAnyLocationsRequirement : BooleanRequirement, ITakeAnyLocationsRequirement
    {
        private readonly IMode _mode;
        private readonly bool _expectedValue;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode settings.
        /// </param>
        /// <param name="expectedValue">
        ///     A boolean expected take any locations value.
        /// </param>
        public TakeAnyLocationsRequirement(IMode mode, bool expectedValue)
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
