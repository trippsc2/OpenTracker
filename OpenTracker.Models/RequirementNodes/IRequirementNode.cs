using OpenTracker.Models.AccessibilityLevels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This interface contains requirement node data.
    /// </summary>
    public interface IRequirementNode : INotifyPropertyChanged
    {
        AccessibilityLevel Accessibility { get; }
        bool AlwaysAccessible { get; set; }
        int ExitsAccessible { get; set; }
        int DungeonExitsAccessible { get; set; }
        int InsanityExitsAccessible { get; set; }

        event EventHandler ChangePropagated;

        delegate IRequirementNode Factory(RequirementNodeID id, bool start);

        AccessibilityLevel GetNodeAccessibility(List<IRequirementNode> excludedNodes);
        void Reset();
    }
}