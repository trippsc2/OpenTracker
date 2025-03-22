using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.Complex
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IRequirement"/> objects
    /// indexed by <see cref="ComplexRequirementType"/>.
    /// </summary>
    public interface IComplexRequirementDictionary : IDictionary<ComplexRequirementType, IRequirement>
    {
    }
}