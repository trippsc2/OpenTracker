using System;
using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.RequirementNodes
{
    public class StartRequirementNode : ReactiveObject, IStartRequirementNode
    {
        public AccessibilityLevel Accessibility { get; } = AccessibilityLevel.Normal;

        public int DungeonExitsAccessible { get; set; } = 0;
        public int ExitsAccessible { get; set; } = 0;
        public int InsanityExitsAccessible { get; set; } = 0;
        
        public event EventHandler? ChangePropagated;

        public AccessibilityLevel GetNodeAccessibility(IList<IRequirementNode> excludedNodes)
        {
            return AccessibilityLevel.Normal;
        }
    }
}