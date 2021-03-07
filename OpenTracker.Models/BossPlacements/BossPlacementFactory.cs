using System;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This is the class for creation logic for boss placements.
    /// </summary>
    public class BossPlacementFactory : IBossPlacementFactory
    {
        private readonly IBossPlacement.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// An Autofac factory for creating boss placements.
        /// </param>
        public BossPlacementFactory(IBossPlacement.Factory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Returns a newly created boss placement.
        /// </summary>
        /// <param name="id">
        /// The boss placement ID.
        /// </param>
        /// <returns>
        /// A newly created boss placement.
        /// </returns>
        public IBossPlacement GetBossPlacement(BossPlacementID id)
        {
            switch (id)
            {
                case BossPlacementID.ATBoss:
                case BossPlacementID.GTFinalBoss:
                    {
                        return _factory(BossType.Aga);
                    }
                case BossPlacementID.EPBoss:
                case BossPlacementID.GTBoss1:
                    {
                        return _factory(BossType.Armos);
                    }
                case BossPlacementID.DPBoss:
                case BossPlacementID.GTBoss2:
                    {
                        return _factory(BossType.Lanmolas);
                    }
                case BossPlacementID.ToHBoss:
                case BossPlacementID.GTBoss3:
                    {
                        return _factory(BossType.Moldorm);
                    }
                case BossPlacementID.PoDBoss:
                    {
                        return _factory(BossType.HelmasaurKing);
                    }
                case BossPlacementID.SPBoss:
                    {
                        return _factory(BossType.Arrghus);
                    }
                case BossPlacementID.SWBoss:
                    {
                        return _factory(BossType.Mothula);
                    }
                case BossPlacementID.TTBoss:
                    {
                        return _factory(BossType.Blind);
                    }
                case BossPlacementID.IPBoss:
                    {
                        return _factory(BossType.Kholdstare);
                    }
                case BossPlacementID.MMBoss:
                    {
                        return _factory(BossType.Vitreous);
                    }
                case BossPlacementID.TRBoss:
                    {
                        return _factory(BossType.Trinexx);
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}
