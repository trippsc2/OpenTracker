using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
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
        
        delegate INodeConnection Factory(INode fromNode, INode toNode, IRequirement requirement);

        AccessibilityLevel GetConnectionAccessibility(IList<INode> excludedNodes);
    }
}
