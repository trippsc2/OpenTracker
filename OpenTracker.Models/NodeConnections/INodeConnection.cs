using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.NodeConnections
{
    public interface INodeConnection : INotifyPropertyChanged, IDisposable
    {
        AccessibilityLevel Accessibility { get; }
        IRequirement Requirement { get; }

        AccessibilityLevel GetConnectionAccessibility(List<IRequirementNode> excludedNodes);
    }
}
