using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Item.SmallKey;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="SmallKeyRequirement"/>
/// objects indexed by <see cref="DungeonID"/> and count.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class SmallKeyRequirementDictionary : LazyDictionary<(DungeonID id, int count), IRequirement>,
    ISmallKeyRequirementDictionary
{
    private readonly IDungeonDictionary _dungeons;

    private readonly SmallKeyRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dungeons">
    ///     The <see cref="IDungeonDictionary"/>.
    /// </param>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="SmallKeyRequirement"/> objects.
    /// </param>
    public SmallKeyRequirementDictionary(IDungeonDictionary dungeons, SmallKeyRequirement.Factory factory)
        : base(new Dictionary<(DungeonID id, int count), IRequirement>())
    {
        _dungeons = dungeons;
            
        _factory = factory;
    }

    protected override IRequirement Create((DungeonID id, int count) key)
    {
        return _factory(_dungeons[key.id].SmallKey, key.count);
    }
}