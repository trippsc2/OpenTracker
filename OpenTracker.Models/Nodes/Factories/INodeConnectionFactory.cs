using System.Collections.Generic;
using OpenTracker.Models.Nodes.Connections;

namespace OpenTracker.Models.Nodes.Factories;

/// <summary>
/// This interface contains the creation logic for <see cref="INodeConnection"/> objects.
/// </summary>
public interface INodeConnectionFactory
{
    /// <summary>
    /// Returns an <see cref="IEnumerable{T}"/> of <see cref="INodeConnection"/> for the specified
    /// <see cref="OverworldNodeID"/>.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="OverworldNodeID"/>.
    /// </param>
    /// <param name="node">
    ///     The <see cref="INode"/>.
    /// </param>
    IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node);
}