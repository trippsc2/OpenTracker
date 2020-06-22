using OpenTracker.Models.Enums;
using System;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class for the requirement node connection.
    /// </summary>
    public class RequirementNodeConnection
    {
        public RequirementNodeID FromNode { get; }
        public RequirementType Requirement { get; }
        public ModeRequirement ModeRequirement { get; }
        public AccessibilityLevel MaximumAccessibility { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fromNode">
        /// The identity of the connecting node.
        /// </param>
        /// <param name="requirement">
        /// The requirement for the connection.
        /// </param>
        /// <param name="modeRequirement">
        /// The game mode requirement of the connection.
        /// </param>
        /// <param name="maximumAccessibility">
        /// The maximum accessibility of the connection.
        /// </param>
        public RequirementNodeConnection(RequirementNodeID fromNode, RequirementType requirement,
            ModeRequirement modeRequirement,
            AccessibilityLevel maximumAccessibility = AccessibilityLevel.Normal)
        {
            FromNode = fromNode;
            Requirement = requirement;
            ModeRequirement = modeRequirement ?? throw new ArgumentNullException(nameof(modeRequirement));
            MaximumAccessibility = maximumAccessibility;
        }
    }
}
