using System;
using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This interface contains requirement node data.
    /// </summary>
    public interface IRequirementNode : IReactiveObject
    {
        AccessibilityLevel Accessibility { get; }
        int ExitsAccessible { get; set; }
        int DungeonExitsAccessible { get; set; }
        int InsanityExitsAccessible { get; set; }

        event EventHandler? ChangePropagated;

        delegate IRequirementNode Factory();

        AccessibilityLevel GetNodeAccessibility(List<IRequirementNode> excludedNodes);
    }
}