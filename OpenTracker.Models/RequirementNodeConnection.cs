using OpenTracker.Models.Enums;
using System;

namespace OpenTracker.Models
{
    public class RequirementNodeConnection
    {
        public RequirementNodeID FromNode { get; }
        public RequirementType Requirement { get; }
        public Mode RequiredMode { get; }
        public AccessibilityLevel MaximumAccessibility { get; }

        public RequirementNodeConnection(RequirementNodeID fromNode, RequirementType requirement,
            Mode requiredMode, AccessibilityLevel maximumAccessibility = AccessibilityLevel.Normal)
        {
            FromNode = fromNode;
            Requirement = requirement;
            RequiredMode = requiredMode ??
                throw new ArgumentNullException(nameof(requiredMode));
            MaximumAccessibility = maximumAccessibility;
        }
    }
}
