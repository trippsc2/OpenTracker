using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.Models.Nodes.Connections
{
    /// <summary>
    /// This interface contains node connection data.
    /// </summary>
    public interface INodeConnection : IReactiveObject
    {
        /// <summary>
        /// The <see cref="AccessibilityLevel"/>.
        /// </summary>
        AccessibilityLevel Accessibility { get; }
        
        /// <summary>
        /// The <see cref="IRequirement"/> for the connection to be accessible.
        /// </summary>
        IRequirement? Requirement { get; }
        
        /// <summary>
        /// A factory for creating new <see cref="INodeConnection"/> objects.
        /// </summary>
        /// <param name="fromNode">
        ///     The <see cref="INode"/> from which the connection originates.
        /// </param>
        /// <param name="toNode">
        ///     The <see cref="INode"/> to which the connection belongs.
        /// </param>
        /// <param name="requirement">
        ///     The <see cref="IRequirement"/> for the connection to be accessible.
        /// </param>
        /// <returns>
        ///     A new <see cref="INodeConnection"/> object.
        /// </returns>
        delegate INodeConnection Factory(INode fromNode, INode toNode, IRequirement? requirement = null);

        /// <summary>
        /// Returns the <see cref="AccessibilityLevel"/> of the connection, excluding loops from the specified nodes.
        /// </summary>
        /// <param name="excludedNodes">
        ///     A <see cref="IList{T}"/> of <see cref="INode"/> to exclude to prevent loops.
        /// </param>
        /// <returns>
        ///     The <see cref="AccessibilityLevel"/> of the connection.
        /// </returns>
        AccessibilityLevel GetConnectionAccessibility(IList<INode> excludedNodes);
    }
}
