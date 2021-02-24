using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for World State requirements.
    /// </summary>
    public class WorldStateRequirement : BooleanRequirement
    {
        private readonly IMode _mode;
        private readonly WorldState _expectedValue;

        public delegate WorldStateRequirement Factory(WorldState expectedValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="expectedValue">
        /// The required world state.
        /// </param>
        public WorldStateRequirement(IMode mode, WorldState expectedValue)
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
