namespace OpenTracker.Models.Nodes.Connections
{
    /// <summary>
    ///     This interface contains dungeon entry node connection data.
    /// </summary>
    public interface IEntryNodeConnection : INodeConnection
    {
        /// <summary>
        ///     A factory for creating new entry node connections.
        /// </summary>
        /// <param name="fromNode">
        ///     The node from which the connection originates.
        /// </param>
        /// <returns>
        ///     A new entry node connection.
        /// </returns>
        delegate IEntryNodeConnection Factory(INode fromNode);
    }
}