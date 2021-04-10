using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    ///     This interface contains the dictionary container for boss type requirements. 
    /// </summary>
    public interface IBossTypeRequirementDictionary : IDictionary<BossType, IRequirement>
    {
        Lazy<IRequirement> NoBoss { get; }
    }
}