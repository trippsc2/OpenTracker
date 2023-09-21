namespace OpenTracker.Models.Nodes;

/// <summary>
/// This interface contains the starting node data.
/// </summary>
public interface IStartNode : INode
{
    /// <summary>
    /// A factory for creating new <see cref="IStartNode"/> objects.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IStartNode"/> object.
    /// </returns>
    delegate IStartNode Factory();
}