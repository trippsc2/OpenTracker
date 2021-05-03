using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Nodes
{
    public class StartNode : ReactiveObject, IStartNode
    {
        public AccessibilityLevel Accessibility { get; } = AccessibilityLevel.Normal;

        public AccessibilityLevel GetNodeAccessibility(IList<INode> excludedNodes)
        {
            return AccessibilityLevel.Normal;
        }
    }
}