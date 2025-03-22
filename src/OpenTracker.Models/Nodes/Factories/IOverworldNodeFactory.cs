namespace OpenTracker.Models.Nodes.Factories
{
    /// <summary>
    /// This interface contains creation logic for <see cref="IStartNode"/> and <see cref="IOverworldNode"/> objects.
    /// </summary>
    public interface IOverworldNodeFactory : INodeConnectionFactory
    {
        /// <summary>
        /// A factory for creating the <see cref="IOverworldNodeFactory"/> object.
        /// </summary>
        /// <returns>
        ///     The <see cref="IOverworldNodeFactory"/> object.
        /// </returns>
        delegate IOverworldNodeFactory Factory();

        /// <summary>
        /// Returns a new <see cref="INode"/> for the specified <see cref="OverworldNodeID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="OverworldNodeID"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="INode"/>.
        /// </returns>
        INode GetOverworldNode(OverworldNodeID id);
    }
}