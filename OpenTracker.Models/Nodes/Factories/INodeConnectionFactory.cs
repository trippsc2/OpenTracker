using System.Collections.Generic;
using OpenTracker.Models.Nodes.Connections;

namespace OpenTracker.Models.Nodes.Factories
{
    /// <summary>
    ///     This interface contains the creation logic for node connections.
    /// </summary>
    public interface INodeConnectionFactory
    {
        /// <summary>
        ///     Populates the list of connections to the specified requirement node ID.
        /// </summary>
        /// <param name="id">
        ///     The requirement node ID.
        /// </param>
        /// <param name="node">
        ///     The node.
        /// </param>
        IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node);
    }
}