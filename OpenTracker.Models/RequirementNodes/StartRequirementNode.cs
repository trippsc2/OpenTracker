using System;
using System.Collections.Generic;
using OpenTracker.Models.AccessibilityLevels;
using ReactiveUI;

namespace OpenTracker.Models.RequirementNodes
{
    public class StartRequirementNode : ReactiveObject, IStartRequirementNode
    {
        public AccessibilityLevel Accessibility { get; } = AccessibilityLevel.Normal;

        public event EventHandler? ChangePropagated;

        public bool AlwaysAccessible { get; set; } = false;
        public int DungeonExitsAccessible { get; set; } = 0;
        public int ExitsAccessible { get; set; } = 0;
        public int InsanityExitsAccessible { get; set; } = 0;

        public AccessibilityLevel GetNodeAccessibility(List<IRequirementNode> excludedNodes)
        {
            return AccessibilityLevel.Normal;
        }
    }
}