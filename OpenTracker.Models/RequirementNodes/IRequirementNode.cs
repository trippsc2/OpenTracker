using OpenTracker.Models.AccessibilityLevels;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This is the interface for the requirement node.
    /// </summary>
    public interface IRequirementNode : INotifyPropertyChanged
    {
        AccessibilityLevel Accessibility { get; }

        AccessibilityLevel GetNodeAccessibility(List<RequirementNodeID> excludedNodes);
    }
}