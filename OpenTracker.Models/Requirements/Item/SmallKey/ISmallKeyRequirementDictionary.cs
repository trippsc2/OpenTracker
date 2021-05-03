using System.Collections.Generic;
using OpenTracker.Models.Dungeons;

namespace OpenTracker.Models.Requirements.Item.SmallKey
{
    /// <summary>
    ///     This interface contains the dictionary container for small key requirements.
    /// </summary>
    public interface ISmallKeyRequirementDictionary : IDictionary<(DungeonID id, int count), IRequirement>
    {
    }
}