using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.Models.Nodes.Connections
{
    /// <summary>
    ///     This interface contains node connection data.
    /// </summary>
    public interface INodeConnection : IReactiveObject
    {
        /// <summary>
        ///     The accessibility level of this node connection.
        /// </summary>
        AccessibilityLevel Accessibility { get; }
        
        /// <summary>
        ///     The node requirement, if applicable.
        /// </summary>
        IRequirement? Requirement { get; }
        
        /// <summary>
        ///     A factory for creating new node connections.
        /// </summary>
        /// <param name="fromNode">
        ///     The node from which the connection originates.
        /// </param>
        /// <param name="toNode">
        ///     The node to which the connection belongs.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the connection to be accessible.
        /// </param>
        /// <returns>
        ///     A new node connections.
        /// </returns>
        delegate INodeConnection Factory(INode fromNode, INode toNode, IRequirement? requirement = null);

        /// <summary>
        ///     Returns the availability of the connection, excluding loops from the specified nodes.
        /// </summary>
        /// <param name="excludedNodes">
        ///     A list of nodes to exclude to prevent loops.
        /// </param>
        /// <returns>
        ///     The availability of the connection.
        /// </returns>
        AccessibilityLevel GetConnectionAccessibility(IList<INode> excludedNodes);
    }
}
