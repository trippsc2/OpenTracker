using System;

namespace OpenTracker.Models.BossPlacements;

/// <summary>
/// This class contains the creation logic for <see cref="IBossPlacement"/> objects.
/// </summary>
public sealed class BossPlacementFactory : IBossPlacementFactory
{
    private readonly IBossPlacement.Factory _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IBossPlacement"/> objects.
    /// </param>
    public BossPlacementFactory(IBossPlacement.Factory factory)
    {
        _factory = factory;
    }

    public IBossPlacement GetBossPlacement(BossPlacementID id)
    {
        return id switch
        {
            BossPlacementID.ATBoss => _factory(BossType.Aga),
            BossPlacementID.GTFinalBoss => _factory(BossType.Aga),
            BossPlacementID.EPBoss => _factory(BossType.Armos),
            BossPlacementID.GTBoss1 => _factory(BossType.Armos),
            BossPlacementID.DPBoss => _factory(BossType.Lanmolas),
            BossPlacementID.GTBoss2 => _factory(BossType.Lanmolas),
            BossPlacementID.ToHBoss => _factory(BossType.Moldorm),
            BossPlacementID.GTBoss3 => _factory(BossType.Moldorm),
            BossPlacementID.PoDBoss => _factory(BossType.HelmasaurKing),
            BossPlacementID.SPBoss => _factory(BossType.Arrghus),
            BossPlacementID.SWBoss => _factory(BossType.Mothula),
            BossPlacementID.TTBoss => _factory(BossType.Blind),
            BossPlacementID.IPBoss => _factory(BossType.Kholdstare),
            BossPlacementID.MMBoss => _factory(BossType.Vitreous),
            BossPlacementID.TRBoss => _factory(BossType.Trinexx),
            _ => throw new ArgumentOutOfRangeException(nameof(id))
        };
    }
}