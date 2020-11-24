using OpenTracker.Models.AccessibilityLevels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for a set of requirement alternatives.
    /// </summary>
    public class AlternativeRequirement : IRequirement
    {
        private readonly List<IRequirement> _requirements;

        public bool Met =>
            Accessibility != AccessibilityLevel.None;

        public event PropertyChangedEventHandler PropertyChanged;

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
        /// <param name="requirements">
        /// A list of requirements to be aggregated.
        /// </param>
        public AlternativeRequirement(List<IRequirement> requirements)
        {
            _requirements = requirements ?? throw new ArgumentNullException(nameof(requirements));

            foreach (var requirement in requirements)
            {
                requirement.PropertyChanged += OnRequirementChanged;
            }

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
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            var accessibility = AccessibilityLevel.None;

            foreach (var requirement in _requirements)
            {
                accessibility = AccessibilityLevelMethods.Max(accessibility, requirement.Accessibility);

                if (accessibility == AccessibilityLevel.Normal)
                {
                    break;
                }
            }

            Accessibility = accessibility;
        }
    }
}
