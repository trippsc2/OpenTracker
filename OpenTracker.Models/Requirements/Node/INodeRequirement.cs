using OpenTracker.Models.Nodes;

namespace OpenTracker.Models.Requirements.Node
{
    /// <summary>
    ///     This interface containing node requirement data.
    /// </summary>
    public interface INodeRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new node requirements.
        /// </summary>
        /// <param name="node">
        ///     The required node.
        /// </param>
        /// <returns>
        ///     A new node requirement.
        /// </returns>
        delegate INodeRequirement Factory(INode node);
    }
}