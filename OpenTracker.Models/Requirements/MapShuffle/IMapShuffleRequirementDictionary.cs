using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.MapShuffle
{
    public interface IMapShuffleRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}