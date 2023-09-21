using System.Collections.Generic;
using OpenTracker.Models.Prizes;

namespace OpenTracker.Models.Requirements.Item.Prize;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="ItemRequirement"/>
/// objects indexed by <see cref="PrizeType"/> and count.
/// </summary>
public interface IPrizeRequirementDictionary : IDictionary<(PrizeType type, int count), IRequirement>
{
}