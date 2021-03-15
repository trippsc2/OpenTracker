using System;
using OpenTracker.Models.AccessibilityLevels;
using ReactiveUI;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains unchanging requirement data.
    /// </summary>
    public class StaticRequirement : ReactiveObject, IRequirement
    {
        public bool Met => true;

        public bool Testing { get; set; }

        public AccessibilityLevel Accessibility { get; }

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
