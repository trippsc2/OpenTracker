using System.Collections.Generic;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.Models.NodeConnections
{
    /// <summary>
    /// This interface contains node connection data.
    /// </summary>
    public interface INodeConnection : IReactiveObject
    {
        AccessibilityLevel Accessibility { get; }
        IRequirement Requirement { get; }

        AccessibilityLevel GetConnectionAccessibility(List<IRequirementNode> excludedNodes);
    }
}
