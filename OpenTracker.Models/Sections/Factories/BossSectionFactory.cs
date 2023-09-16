using System;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Requirements.BossShuffle;
using OpenTracker.Models.Sections.Boss;

namespace OpenTracker.Models.Sections.Factories;

/// <summary>
/// This class contains the creation logic for <see cref="IBossSection"/> and <see cref="IPrizeSection"/> objects.
/// </summary>
public sealed class BossSectionFactory : IBossSectionFactory
{
    private readonly IBossShuffleRequirementDictionary _bossShuffleRequirements;

    private readonly IBossPlacementDictionary _bossPlacements;
    private readonly IPrizePlacementDictionary _prizePlacements;
        
    private readonly IBossSection.Factory _bossSectionFactory;
    private readonly IPrizeSection.Factory _prizeSectionFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bossShuffleRequirements">
    ///     The <see cref="IBossShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="bossPlacements">
    ///     The <see cref="IBossPlacementDictionary"/>.
    /// </param>
    /// <param name="prizePlacements">
    ///     The <see cref="IPrizePlacementDictionary"/>.
    /// </param>
    /// <param name="bossSectionFactory">
    ///     An Autofac factory for creating new <see cref="IBossSection"/> objects.
    /// </param>
    /// <param name="prizeSectionFactory">
    ///     An Autofac factory for creating new <see cref="IPrizeSection"/> objects.
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
        BossAccessibilityProvider accessibilityProvider, IAutoTrackValue? autoTrackValue, LocationID id,
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
    /// Returns the section name for the specified <see cref="LocationID"/> and index.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <param name="index">
    ///     A <see cref="int"/> representing the section index.
    /// </param>
    /// <returns>
    ///     A <see cref="string"/> representing the section name.
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
    /// Returns the <see cref="IBossPlacement"/> for the specified <see cref="LocationID"/> and index.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <param name="index">
    ///     A <see cref="int"/> representing the section index.
    /// </param>
    /// <returns>
    ///     The <see cref="IBossPlacement"/>.
    /// </returns>
    private IBossPlacement GetBossPlacement(LocationID id, int index)
    {
        return id switch
        {
            LocationID.AgahnimTower => _bossPlacements[BossPlacementID.ATBoss],
            LocationID.EasternPalace => _bossPlacements[BossPlacementID.EPBoss],
            LocationID.DesertPalace => _bossPlacements[BossPlacementID.DPBoss],
            LocationID.TowerOfHera => _bossPlacements[BossPlacementID.ToHBoss],
            LocationID.PalaceOfDarkness => _bossPlacements[BossPlacementID.PoDBoss],
            LocationID.SwampPalace => _bossPlacements[BossPlacementID.SPBoss],
            LocationID.SkullWoods => _bossPlacements[BossPlacementID.SWBoss],
            LocationID.ThievesTown => _bossPlacements[BossPlacementID.TTBoss],
            LocationID.IcePalace => _bossPlacements[BossPlacementID.IPBoss],
            LocationID.MiseryMire => _bossPlacements[BossPlacementID.MMBoss],
            LocationID.TurtleRock => _bossPlacements[BossPlacementID.TRBoss],
            LocationID.GanonsTower when index == 1 => _bossPlacements[BossPlacementID.GTBoss1],
            LocationID.GanonsTower when index == 2 => _bossPlacements[BossPlacementID.GTBoss2],
            LocationID.GanonsTower when index == 3 => _bossPlacements[BossPlacementID.GTBoss3],
            LocationID.GanonsTower => _bossPlacements[BossPlacementID.GTFinalBoss],
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
    }

    /// <summary>
    /// Returns the <see cref="IPrizePlacement"/> for the specified <see cref="LocationID"/>.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <returns>
    ///     The <see cref="IPrizePlacement"/>.
    /// </returns>
    private IPrizePlacement GetPrizePlacement(LocationID id)
    {
        return id switch
        {
            LocationID.AgahnimTower => _prizePlacements[PrizePlacementID.ATPrize],
            LocationID.EasternPalace => _prizePlacements[PrizePlacementID.EPPrize],
            LocationID.DesertPalace => _prizePlacements[PrizePlacementID.DPPrize],
            LocationID.TowerOfHera => _prizePlacements[PrizePlacementID.ToHPrize],
            LocationID.PalaceOfDarkness => _prizePlacements[PrizePlacementID.PoDPrize],
            LocationID.SwampPalace => _prizePlacements[PrizePlacementID.SPPrize],
            LocationID.SkullWoods => _prizePlacements[PrizePlacementID.SWPrize],
            LocationID.ThievesTown => _prizePlacements[PrizePlacementID.TTPrize],
            LocationID.IcePalace => _prizePlacements[PrizePlacementID.IPPrize],
            LocationID.MiseryMire => _prizePlacements[PrizePlacementID.MMPrize],
            LocationID.TurtleRock => _prizePlacements[PrizePlacementID.TRPrize],
            LocationID.GanonsTower => _prizePlacements[PrizePlacementID.GTPrize],
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
    }
}