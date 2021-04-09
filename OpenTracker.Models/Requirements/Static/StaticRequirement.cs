using System;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Requirements.Static
{
    /// <summary>
    ///     This class contains unchanging requirement data.
    /// </summary>
    public class StaticRequirement : ReactiveObject, IStaticRequirement
    {
        public bool Met => true;
        
        public AccessibilityLevel Accessibility { get; }

        public event EventHandler? ChangePropagated;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="accessibility">
        ///     The accessibility level of the requirement.
        /// </param>
        public StaticRequirement(AccessibilityLevel accessibility)
        {
            Accessibility = accessibility;
        }
    }
}
