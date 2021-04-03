namespace OpenTracker.Models.Nodes
{
    public interface IStartNode : INode
    {
        delegate IStartNode Factory();
    }
}