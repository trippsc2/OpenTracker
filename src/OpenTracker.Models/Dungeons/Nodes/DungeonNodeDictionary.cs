using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Utils;

namespace OpenTracker.Models.Dungeons.Nodes;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container of <see cref="IDungeonNode"/> indexed
/// by <see cref="DungeonNodeID"/>.
/// </summary>
public class DungeonNodeDictionary : LazyDictionary<DungeonNodeID, IDungeonNode>, IDungeonNodeDictionary
{
    private readonly IDungeonNode.Factory _factory;

    private readonly IMutableDungeon _dungeonData;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IDungeonNode"/> objects.
    /// </param>
    /// <param name="dungeonData">
    ///     The <see cref="IMutableDungeon"/> parent class.
    /// </param>
    public DungeonNodeDictionary(IDungeonNode.Factory factory, IMutableDungeon dungeonData)
        : base(new Dictionary<DungeonNodeID, IDungeonNode>())
    {
        _factory = factory;

        _dungeonData = dungeonData;
    }

    public void PopulateNodes(IList<DungeonNodeID> dungeonNodes)
    {
        foreach (var node in dungeonNodes)
        {
            _ = this[node];
        }
    }

    protected override IDungeonNode Create(DungeonNodeID key)
    {
        return _factory(_dungeonData);
    }
}