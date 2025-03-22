using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.Alternative
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
    /// <see cref="IAlternativeRequirement"/> objects indexed by <see cref="HashSet{T}"/> of <see cref="IRequirement"/>.
    /// </summary>
    public interface IAlternativeRequirementDictionary : IDictionary<HashSet<IRequirement>, IRequirement>
    {
    }
}