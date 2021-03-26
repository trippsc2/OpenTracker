using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains logic for a set of requirement alternatives.
    /// </summary>
    public class AlternativeRequirement : AccessibilityRequirement
    {
        private readonly List<IRequirement> _requirements;

        public delegate AlternativeRequirement Factory(List<IRequirement> requirements);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        /// A list of requirements to be aggregated.
        /// </param>
        public AlternativeRequirement(List<IRequirement> requirements)
        {
            _requirements = requirements;

            foreach (var requirement in requirements)
            {
                requirement.PropertyChanged += OnRequirementChanged;
            }

            UpdateValue();
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
        private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                UpdateValue();
            }
        }

        protected override AccessibilityLevel GetAccessibility()
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

            return accessibility;
        }
    }
}
