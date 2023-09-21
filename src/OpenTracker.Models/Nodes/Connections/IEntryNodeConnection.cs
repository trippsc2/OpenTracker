namespace OpenTracker.Models.Nodes.Connections;

/// <summary>
/// This interface contains dungeon entry node connection data.
/// </summary>
public interface IEntryNodeConnection : INodeConnection
{
    /// <summary>
    /// A factory for creating new <see cref="IEntryNodeConnection"/> objects.
    /// </summary>
    /// <param name="fromNode">
    ///     The <see cref="INode"/> from which the connection originates.
    /// </param>
    /// <returns>
    ///     A new <see cref="IEntryNodeConnection"/> object.
    /// </returns>
    new delegate IEntryNodeConnection Factory(INode fromNode);
}