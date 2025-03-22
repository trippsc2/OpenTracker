using OpenTracker.Models.Nodes;

namespace OpenTracker.Models.Requirements.Node
{
    /// <summary>
    /// This interface containing <see cref="INode"/> <see cref="IRequirement"/> data.
    /// </summary>
    public interface INodeRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="INodeRequirement"/> objects.
        /// </summary>
        /// <param name="node">
        ///     The <see cref="INode"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="INodeRequirement"/> object.
        /// </returns>
        delegate INodeRequirement Factory(INode node);
    }
}