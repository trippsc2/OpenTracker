using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    public class CompassShuffleRequirement : IRequirement
    {
        private readonly bool _compassShuffle;

        public bool Met =>
            Accessibility == AccessibilityLevel.Normal;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ChangePropagated;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="compassShuffle">
        /// A boolean representing the required compass shuffle value.
        /// </param>
        public CompassShuffleRequirement(bool compassShuffle)
        {
            _compassShuffle = compassShuffle;

            Mode.Instance.PropertyChanged += OnModeChanged;

            UpdateAccessibility();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ChangePropagated?.Invoke(this, new EventArgs());
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
            if (e.PropertyName == nameof(Mode.CompassShuffle))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = Mode.Instance.CompassShuffle == _compassShuffle ?
                AccessibilityLevel.Normal : AccessibilityLevel.None;
        }
    }
}
