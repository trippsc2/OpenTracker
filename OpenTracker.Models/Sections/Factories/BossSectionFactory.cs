using System;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Requirements.BossShuffle;
using OpenTracker.Models.Sections.Boss;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This class contains the creation logic for boss sections.
    /// </summary>
    public class BossSectionFactory : IBossSectionFactory
    {
        private readonly IBossShuffleRequirementDictionary _bossShuffleRequirements;

        private readonly IBossPlacementDictionary _bossPlacements;
        private readonly IPrizePlacementDictionary _prizePlacements;
        
        private readonly IBossSection.Factory _bossSectionFactory;
        private readonly IPrizeSection.Factory _prizeSectionFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="bossShuffleRequirements">
        ///     The boss shuffle requirement dictionary.
        /// </param>
        /// <param name="bossPlacements">
        ///     The boss placement dictionary.
        /// </param>
        /// <param name="prizePlacements">
        ///     The prize placement dictionary.
        /// </param>
        /// <param name="bossSectionFactory">
        ///     An Autofac factory for creating new boss sections.
        /// </param>
        /// <param name="prizeSectionFactory">
        ///     An Autofac factory for creating new prize sections.
        /// </param>
        public BossSectionFactory(
            IBossShuffleRequirementDictionary bossShuffleRequirements, IBossPlacementDictionary bossPlacements,
            IPrizePlacementDictionary prizePlacements, IBossSection.Factory bossSectionFactory,
            IPrizeSection.Factory prizeSectionFactory)
        {
            _bossShuffleRequirements = bossShuffleRequirements;

            _bossPlacements = bossPlacements;
            _prizePlacements = prizePlacements;
            
            _bossSectionFactory = bossSectionFactory;
            _prizeSectionFactory = prizeSectionFactory;
        }

        public IBossSection GetBossSection(
            IBossAccessibilityProvider accessibilityProvider, IAutoTrackValue? autoTrackValue, LocationID id,
            int index = 1)
        {
            switch (id)
            {
                case LocationID.GanonsTower when index == 1:
                case LocationID.GanonsTower when index == 2:
                case LocationID.GanonsTower when index == 3:
                    return _bossSectionFactory(
                        accessibilityProvider, GetName(id, index), GetBossPlacement(id, index),
                        _bossShuffleRequirements[true]);
                default: 
                    return _prizeSectionFactory(
                        accessibilityProvider, GetName(id, index), GetBossPlacement(id, index), GetPrizePlacement(id),
                        autoTrackValue);
            }
        }

        /// <summary>
        ///     Returns the section name.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <param name="index">
        ///     The section index.
        /// </param>
        /// <returns>
        ///     A string representing the section name.
        /// </returns>
        private static string GetName(LocationID id, int index)
        {
            switch (id)
            {
                case LocationID.GanonsTower when index == 1:
                case LocationID.GanonsTower when index == 2:
                case LocationID.GanonsTower when index == 3:
                    return $"Boss {index}";
                case LocationID.GanonsTower:
                    return "Final Boss";
                default:
                    return "Boss";
            }
        }

        /// <summary>
        ///     Returns the boss placement for the specified section.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <param name="index">
        ///     The section index.
        /// </param>
        /// <returns>
        ///     The boss placement.
        /// </returns>
        private IBossPlacement GetBossPlacement(LocationID id, int index)
        {
            switch (id)
            {
                case LocationID.AgahnimTower:
                    return _bossPlacements[BossPlacementID.ATBoss];
                case LocationID.EasternPalace:
                    return _bossPlacements[BossPlacementID.EPBoss];
                case LocationID.DesertPalace:
                    return _bossPlacements[BossPlacementID.DPBoss];
                case LocationID.TowerOfHera:
                    return _bossPlacements[BossPlacementID.ToHBoss];
                case LocationID.PalaceOfDarkness:
                    return _bossPlacements[BossPlacementID.PoDBoss];
                case LocationID.SwampPalace:
                    return _bossPlacements[BossPlacementID.SPBoss];
                case LocationID.SkullWoods:
                    return _bossPlacements[BossPlacementID.SWBoss];
                case LocationID.ThievesTown:
                    return _bossPlacements[BossPlacementID.TTBoss];
                case LocationID.IcePalace:
                    return _bossPlacements[BossPlacementID.IPBoss];
                case LocationID.MiseryMire:
                    return _bossPlacements[BossPlacementID.MMBoss];
                case LocationID.TurtleRock:
                    return _bossPlacements[BossPlacementID.TRBoss];
                case LocationID.GanonsTower when index == 1:
                    return _bossPlacements[BossPlacementID.GTBoss1];
                case LocationID.GanonsTower when index == 2:
                    return _bossPlacements[BossPlacementID.GTBoss2];
                case LocationID.GanonsTower when index == 3:
                    return _bossPlacements[BossPlacementID.GTBoss3];
                case LocationID.GanonsTower:
                    return _bossPlacements[BossPlacementID.GTFinalBoss];
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }

        /// <summary>
        ///     Returns the prize placement for the specified section.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     The prize placement.
        /// </returns>
        private IPrizePlacement GetPrizePlacement(LocationID id)
        {
            switch (id)
            {
                case LocationID.AgahnimTower:
                    return _prizePlacements[PrizePlacementID.ATPrize];
                case LocationID.EasternPalace:
                    return _prizePlacements[PrizePlacementID.EPPrize];
                case LocationID.DesertPalace:
                    return _prizePlacements[PrizePlacementID.DPPrize];
                case LocationID.TowerOfHera:
                    return _prizePlacements[PrizePlacementID.ToHPrize];
                case LocationID.PalaceOfDarkness:
                    return _prizePlacements[PrizePlacementID.PoDPrize];
                case LocationID.SwampPalace:
                    return _prizePlacements[PrizePlacementID.SPPrize];
                case LocationID.SkullWoods:
                    return _prizePlacements[PrizePlacementID.SWPrize];
                case LocationID.ThievesTown:
                    return _prizePlacements[PrizePlacementID.TTPrize];
                case LocationID.IcePalace:
                    return _prizePlacements[PrizePlacementID.IPPrize];
                case LocationID.MiseryMire:
                    return _prizePlacements[PrizePlacementID.MMPrize];
                case LocationID.TurtleRock:
                    return _prizePlacements[PrizePlacementID.TRPrize];
                case LocationID.GanonsTower:
                    return _prizePlacements[PrizePlacementID.GTPrize];
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }
    }
}