using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.Complex
{
    /// <summary>
    ///     This interface contains the creation logic for complex requirements.
    /// </summary>
    public interface IComplexRequirementDictionary : IDictionary<ComplexRequirementType, IRequirement>
    {
    }
}