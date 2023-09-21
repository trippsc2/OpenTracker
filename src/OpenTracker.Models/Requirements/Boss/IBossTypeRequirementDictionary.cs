using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Requirements.Boss;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IRequirement"/>
/// objects indexed by <see cref="BossType"/>. 
/// </summary>
public interface IBossTypeRequirementDictionary : IDictionary<BossType, IRequirement>
{
    /// <summary>
    /// The <see cref="IRequirement"/> for no boss being set.
    /// </summary>
    Lazy<IRequirement> NoBoss { get; }
}