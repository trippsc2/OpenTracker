namespace OpenTracker.Models.Requirements.Complex
{
    /// <summary>
    ///     This interface contains the creation logic for complex requirements.
    /// </summary>
    public interface IComplexRequirementFactory
    {
        /// <summary>
        ///     A factory for creating the complex requirement factory.
        /// </summary>
        /// <returns>
        ///     The complex requirement factory.
        /// </returns>
        delegate IComplexRequirementFactory Factory();
        
        /// <summary>
        ///     Returns a new complex requirement of the specified type.
        /// </summary>
        /// <param name="type">
        ///     The complex requirement type.
        /// </param>
        /// <returns>
        ///     A new complex requirement.
        /// </returns>
        IRequirement GetComplexRequirement(ComplexRequirementType type);
    }
}