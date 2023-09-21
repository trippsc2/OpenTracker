using System.Collections.Generic;
using OpenTracker.Models.Items;

namespace OpenTracker.Models.Prizes;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IItem"/> indexed by
/// <see cref="PrizeType"/>.
/// </summary>
public interface IPrizeDictionary : IDictionary<PrizeType, IItem>
{
}