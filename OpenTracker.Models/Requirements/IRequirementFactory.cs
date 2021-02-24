namespace OpenTracker.Models.Requirements
{
    public interface IRequirementFactory
    {
        delegate IRequirementFactory Factory();

        IRequirement GetRequirement(RequirementType type);
    }
}