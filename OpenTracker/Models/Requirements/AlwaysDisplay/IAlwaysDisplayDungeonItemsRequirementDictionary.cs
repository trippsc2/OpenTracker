using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.AlwaysDisplay
{
    /// <summary>
    ///     This interface contains the dictionary container for always display dungeon items requirements.
    /// </summary>
    public interface IAlwaysDisplayDungeonItemsRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}