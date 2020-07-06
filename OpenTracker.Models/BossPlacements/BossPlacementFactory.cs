using OpenTracker.Models.Enums;
using System;

namespace OpenTracker.Models.BossPlacements
{
    public static class BossPlacementFactory
    {
        internal static IBossPlacement GetBossPlacement(BossPlacementID id)
        {
            switch (id)
            {
                case BossPlacementID.ATBoss:
                case BossPlacementID.GTFinalBoss:
                    {
                        return new BossPlacement(BossType.Aga);
                    }
                case BossPlacementID.EPBoss:
                case BossPlacementID.GTBoss1:
                    {
                        return new BossPlacement(BossType.Armos);
                    }
                case BossPlacementID.DPBoss:
                case BossPlacementID.GTBoss2:
                    {
                        return new BossPlacement(BossType.Lanmolas);
                    }
                case BossPlacementID.ToHBoss:
                case BossPlacementID.GTBoss3:
                    {
                        return new BossPlacement(BossType.Moldorm);
                    }
                case BossPlacementID.PoDBoss:
                    {
                        return new BossPlacement(BossType.HelmasaurKing);
                    }
                case BossPlacementID.SPBoss:
                    {
                        return new BossPlacement(BossType.Arrghus);
                    }
                case BossPlacementID.SWBoss:
                    {
                        return new BossPlacement(BossType.Mothula);
                    }
                case BossPlacementID.TTBoss:
                    {
                        return new BossPlacement(BossType.Blind);
                    }
                case BossPlacementID.IPBoss:
                    {
                        return new BossPlacement(BossType.Kholdstare);
                    }
                case BossPlacementID.MMBoss:
                    {
                        return new BossPlacement(BossType.Vitreous);
                    }
                case BossPlacementID.TRBoss:
                    {
                        return new BossPlacement(BossType.Trinexx);
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}
