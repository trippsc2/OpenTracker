namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This interface contains creation logic for requirement data.
    /// </summary>
    public interface IRequirementFactory
    {
        delegate IRequirementFactory Factory();

        IRequirement GetRequirement(RequirementType type);
    }
}