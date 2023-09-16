using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.GuaranteedBossItems;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="IGuaranteedBossItemsRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public interface IGuaranteedBossItemsRequirementDictionary : IDictionary<bool, IRequirement>
{
}