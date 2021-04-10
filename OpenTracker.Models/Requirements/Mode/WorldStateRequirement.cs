using System.ComponentModel;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    ///     This class contains world state requirement data.
    /// </summary>
    public class WorldStateRequirement : BooleanRequirement, IWorldStateRequirement
    {
        private readonly IMode _mode;
        private readonly WorldState _expectedValue;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode settings.
        /// </param>
        /// <param name="expectedValue">
        ///     The required world state.
        /// </param>
        public WorldStateRequirement(IMode mode, WorldState expectedValue)
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
            if (e.PropertyName == nameof(IMode.WorldState))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _mode.WorldState == _expectedValue;
        }
    }
}
