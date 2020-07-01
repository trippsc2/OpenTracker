using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for unchanging requirements, such as no requirement or
    /// inspect requirements.
    /// </summary>
    public class StaticRequirement : IRequirement
    {
        public AccessibilityLevel Accessibility { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        
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
