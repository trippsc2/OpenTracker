namespace OpenTracker.Models.RequirementNodes
{
    public interface IStartRequirementNode : IRequirementNode
    {
        delegate IStartRequirementNode Factory();
    }
}