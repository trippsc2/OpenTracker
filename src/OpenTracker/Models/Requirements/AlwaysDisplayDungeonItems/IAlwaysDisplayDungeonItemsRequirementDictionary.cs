using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.AlwaysDisplayDungeonItems;

/// <summary>
///     This interface contains the dictionary container for always display dungeon items requirements.
/// </summary>
public interface IAlwaysDisplayDungeonItemsRequirementDictionary : IDictionary<bool, IRequirement>
{
}