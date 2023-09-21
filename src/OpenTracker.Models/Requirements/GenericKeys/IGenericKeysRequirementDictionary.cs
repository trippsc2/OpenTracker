using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.GenericKeys;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="GenericKeysRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public interface IGenericKeysRequirementDictionary : IDictionary<bool, IRequirement>
{
}