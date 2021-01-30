using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.AutoTracking;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for race illegal tracking requirements.
    /// </summary>
    public class RaceIllegalTrackingRequirement : IRequirement
    {
        private readonly bool _raceIllegalTracking;

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
        /// <param name="value">
        /// The required value.
        /// </param>
        public RaceIllegalTrackingRequirement(bool value)
        {
            _raceIllegalTracking = value;

            AutoTracker.Instance.PropertyChanged += OnAutoTrackerChanged;

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
        /// Subscribes to the PropertyChanged event on the AutoTracker class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAutoTrackerChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AutoTracker.RaceIllegalTracking))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = AutoTracker.Instance.RaceIllegalTracking == _raceIllegalTracking ?
                AccessibilityLevel.Normal : AccessibilityLevel.None;
        }
    }
}
