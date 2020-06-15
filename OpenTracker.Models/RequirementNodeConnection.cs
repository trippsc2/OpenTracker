using OpenTracker.Models.Enums;
using System;

namespace OpenTracker.Models
{
    public class RequirementNodeConnection
    {
        public RequirementNodeID FromNode { get; }
        public RequirementType Requirement { get; }
        public ModeRequirement ModeRequirement { get; }
        public AccessibilityLevel MaximumAccessibility { get; }

        public RequirementNodeConnection(RequirementNodeID fromNode, RequirementType requirement,
            ModeRequirement modeRequirement, AccessibilityLevel maximumAccessibility = AccessibilityLevel.Normal)
        {
            FromNode = fromNode;
            Requirement = requirement;
            ModeRequirement = modeRequirement ??
                throw new ArgumentNullException(nameof(modeRequirement));
            MaximumAccessibility = maximumAccessibility;
        }
    }
}
