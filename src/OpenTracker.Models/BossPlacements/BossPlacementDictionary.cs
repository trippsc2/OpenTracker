using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.BossPlacements;

/// <summary>
/// This class contains the dictionary container for <see cref="IBossPlacement"/> objects.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class BossPlacementDictionary : LazyDictionary<BossPlacementID, IBossPlacement>,
    IBossPlacementDictionary
{
    private readonly Lazy<IBossPlacementFactory> _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating the <see cref="IBossPlacementFactory"/> object.
    /// </param>
    public BossPlacementDictionary(IBossPlacementFactory.Factory factory)
        : base(new Dictionary<BossPlacementID, IBossPlacement>())
    {
        _factory = new Lazy<IBossPlacementFactory>(() => factory());
    }

    public void Reset()
    {
        foreach (var placement in Values)
        {
            placement.Reset();
        }
    }

    public IDictionary<BossPlacementID, BossPlacementSaveData> Save()
    {
        return Keys.ToDictionary(
            bossPlacement => bossPlacement, bossPlacement => this[bossPlacement].Save());
    }

    public void Load(IDictionary<BossPlacementID, BossPlacementSaveData>? saveData)
    {
        if (saveData == null)
        {
            return;
        }

        foreach (var bossPlacement in saveData.Keys)
        {
            this[bossPlacement].Load(saveData[bossPlacement]);
        }
    }

    protected override IBossPlacement Create(BossPlacementID key)
    {
        return _factory.Value.GetBossPlacement(key);
    }
}