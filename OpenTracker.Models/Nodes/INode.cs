using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Nodes
{
    /// <summary>
    ///     This interface contains the node data.
    /// </summary>
    public interface INode : IReactiveObject
    {
        /// <summary>
        ///     The accessibility level of the node.
        /// </summary>
        AccessibilityLevel Accessibility { get; }

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