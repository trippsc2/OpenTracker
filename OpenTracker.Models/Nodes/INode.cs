using System;
using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Nodes
{
    public interface INode : IReactiveObject
    {
        AccessibilityLevel Accessibility { get; }
        
        event EventHandler? ChangePropagated;

        /// <summary>
        ///     Returns the node accessibility.
        /// </summary>
        /// <param name="excludedNodes">
        ///     The list of node IDs from which to not check accessibility.
        /// </param>
        /// <returns>
        ///     The accessibility of the node.
        /// </returns>
        AccessibilityLevel GetNodeAccessibility(IList<INode> excludedNodes);
    }
}