using OpenTracker.Models.AccessibilityLevels;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains unchanging requirement data.
    /// </summary>
    public class StaticRequirement : IRequirement
    {
        public bool Met =>
            true;

        public AccessibilityLevel Accessibility { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? ChangePropagated;

        public delegate StaticRequirement Factory(AccessibilityLevel accessibility);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accessibility">
        /// The accessibility level of the requirement.
        /// </param>
        public StaticRequirement(AccessibilityLevel accessibility)
        {
            Accessibility = accessibility;
        }
    }
}
