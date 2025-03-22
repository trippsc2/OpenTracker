using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Requirements.Aggregate
{
    /// <summary>
    /// This class contains logic aggregating a set of <see cref="IRequirement"/>.
    /// </summary>
    public class AggregateRequirement : AccessibilityRequirement, IAggregateRequirement
    {
        private readonly IList<IRequirement> _requirements;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        ///     A <see cref="IList{T}"/> of <see cref="IRequirement"/> to be aggregated.
        /// </param>
        public AggregateRequirement(IList<IRequirement> requirements)
        {
            _requirements = requirements;

            foreach (var requirement in requirements)
            {
                requirement.PropertyChanged += OnRequirementChanged;
            }

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the <see cref="IRequirement.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
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
            var accessibility = AccessibilityLevel.Normal;

            foreach (var requirement in _requirements)
            {
                accessibility = AccessibilityLevelMethods.Min(accessibility, requirement.Accessibility);

                if (accessibility == AccessibilityLevel.None)
                {
                    break;
                }
            }

            return accessibility;
        }
    }
}
