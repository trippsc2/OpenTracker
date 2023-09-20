using System.Collections.Generic;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.AlwaysDisplayDungeonItems;

/// <summary>
///     This class contains the dictionary container for always display dungeon items requirements.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public class AlwaysDisplayDungeonItemsRequirementDictionary : LazyDictionary<bool, IRequirement>, IAlwaysDisplayDungeonItemsRequirementDictionary
{
    private readonly IAlwaysDisplayDungeonItemsRequirement.Factory _factory;
        
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new always display dungeon items requirements.
    /// </param>
    public AlwaysDisplayDungeonItemsRequirementDictionary(IAlwaysDisplayDungeonItemsRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}