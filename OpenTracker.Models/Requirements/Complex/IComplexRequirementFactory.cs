namespace OpenTracker.Models.Requirements.Complex;

/// <summary>
/// This interface contains the creation logic for complex <see cref="IRequirement"/> objects.
/// </summary>
public interface IComplexRequirementFactory
{
    /// <summary>
    /// A factory for creating the <see cref="IComplexRequirementFactory"/> object.
    /// </summary>
    /// <returns>
    ///     The <see cref="IComplexRequirementFactory"/> object.
    /// </returns>
    delegate IComplexRequirementFactory Factory();
        
    /// <summary>
    /// Returns a new <see cref="IRequirement"/> object of the specified <see cref="ComplexRequirementType"/>.
    /// </summary>
    /// <param name="type">
    ///     The <see cref="ComplexRequirementType"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IRequirement"/> object.
    /// </returns>
    IRequirement GetComplexRequirement(ComplexRequirementType type);
}