namespace OpenTracker.Models.Nodes.Factories
{
    /// <summary>
    ///     This interface contains creation logic for requirement node data.
    /// </summary>
    public interface IOverworldNodeFactory : INodeConnectionFactory
    {
        /// <summary>
        ///     A factory for creating the overworld node factory.
        /// </summary>
        /// <returns>
        ///     The overworld node factory.
        /// </returns>
        delegate IOverworldNodeFactory Factory();

        /// <summary>
        ///     Returns a new requirement node for the specified requirement node ID.
        /// </summary>
        /// <param name="id">
        ///     The requirement node ID.
        /// </param>
        /// <returns>
        ///     A new requirement node.
        /// </returns>
        INode GetOverworldNode(OverworldNodeID id);
    }
}