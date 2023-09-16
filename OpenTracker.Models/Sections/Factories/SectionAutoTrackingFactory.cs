using System.Collections.Generic;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Multiple;
using OpenTracker.Models.AutoTracking.Values.Single;
using OpenTracker.Models.AutoTracking.Values.Static;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements.AutoTracking;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.CompassShuffle;
using OpenTracker.Models.Requirements.MapShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;

namespace OpenTracker.Models.Sections.Factories;

/// <summary>
/// This class containing creation logic for section <see cref="IAutoTrackValue"/> objects.
/// </summary>
public class SectionAutoTrackingFactory : ISectionAutoTrackingFactory
{
    private readonly IMemoryAddressProvider _memoryAddressProvider;
    private readonly IItemDictionary _items;

    private readonly IBigKeyShuffleRequirementDictionary _bigKeyShuffleRequirements;
    private readonly ICompassShuffleRequirementDictionary _compassShuffleRequirements;
    private readonly IMapShuffleRequirementDictionary _mapShuffleRequirements;
    private readonly IRaceIllegalTrackingRequirement _raceIllegalTrackingRequirement;
    private readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements;

    private readonly IMemoryFlag.Factory _memoryFlagFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="memoryAddressProvider">
    ///     The <see cref="IMemoryAddressProvider"/>.
    /// </param>
    /// <param name="items">
    ///     The <see cref="IItemDictionary"/>.
    /// </param>
    /// <param name="bigKeyShuffleRequirements">
    ///     The <see cref="IBigKeyShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="compassShuffleRequirements">
    ///     The <see cref="ICompassShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="mapShuffleRequirements">
    ///     The <see cref="IMapShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="raceIllegalTrackingRequirement">
    ///     The <see cref="IRaceIllegalTrackingRequirement"/>.
    /// </param>
    /// <param name="smallKeyShuffleRequirements">
    ///     The <see cref="ISmallKeyShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="memoryFlagFactory">
    ///     An Autofac factory creating new <see cref="IMemoryFlag"/> objects.
    /// </param>
    public SectionAutoTrackingFactory(
        IMemoryAddressProvider memoryAddressProvider, IItemDictionary items,
        IBigKeyShuffleRequirementDictionary bigKeyShuffleRequirements,
        ICompassShuffleRequirementDictionary compassShuffleRequirements,
        IMapShuffleRequirementDictionary mapShuffleRequirements,
        IRaceIllegalTrackingRequirement raceIllegalTrackingRequirement,
        ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements,
        IMemoryFlag.Factory memoryFlagFactory)
    {
        _memoryAddressProvider = memoryAddressProvider;
        _items = items;

        _memoryFlagFactory = memoryFlagFactory;
        _bigKeyShuffleRequirements = bigKeyShuffleRequirements;
        _compassShuffleRequirements = compassShuffleRequirements;
        _mapShuffleRequirements = mapShuffleRequirements;
        _raceIllegalTrackingRequirement = raceIllegalTrackingRequirement;
        _smallKeyShuffleRequirements = smallKeyShuffleRequirements;
    }

    public IAutoTrackValue? GetAutoTrackValue(LocationID id, int sectionIndex = 0)
    {
        return id switch
        {
            LocationID.LinksHouse => new AutoTrackMultipleOverride(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef001], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef208], 0x10), 1)
            }),
            LocationID.Pedestal => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef300], 0x40), 1),
            LocationID.LumberjackCave => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1c5], 0x02), 1),
            LocationID.BlindsHouse when sectionIndex == 0 => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23a], 0x20), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23a], 0x40), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23a], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23b], 0x01), 1),
            }),
            LocationID.BlindsHouse => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23a], 0x10), 1),
            LocationID.TheWell when sectionIndex == 0 => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef05e], 0x20), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef05e], 0x40), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef05e], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef05f], 0x01), 1),
            }),
            LocationID.TheWell => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef05e], 0x10), 1),
            LocationID.BottleVendor => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x02), 1),
            LocationID.ChickenHouse => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef210], 0x10), 1),
            LocationID.Tavern => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef206], 0x10), 1),
            LocationID.SickKid => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x04), 1),
            LocationID.MagicBat => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x80), 1),
            LocationID.RaceGame => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2a8], 0x40), 1),
            LocationID.Library => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x80), 1),
            LocationID.MushroomSpot => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x10), 1),
            LocationID.ForestHideout => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1c3], 0x02), 1),
            LocationID.CastleSecret when sectionIndex == 1 => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef3c6], 0x01), 1),
            LocationID.CastleSecret => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0aa], 0x10), 1),
            LocationID.WitchsHut => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x20), 1),
            LocationID.SahasrahlasHut when sectionIndex == 0 => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef20a], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef20a], 0x20), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef20a], 0x40), 1),
            }),
            LocationID.SahasrahlasHut => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x10), 1),
            LocationID.BonkRocks => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef248], 0x10), 1),
            LocationID.KingsTomb => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef226], 0x10), 1),
            LocationID.AginahsCave => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef214], 0x10), 1),
            LocationID.GroveDiggingSpot => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2aa], 0x40), 1),
            LocationID.Dam when sectionIndex == 0 => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef216], 0x10), 1),
            LocationID.Dam => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2bb], 0x40), 1),
            LocationID.MiniMoldormCave => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef246], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef246], 0x20), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef246], 0x40), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef246], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef247], 0x04), 1)
            }),
            LocationID.IceRodCave => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef240], 0x10), 1),
            LocationID.Hobo => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x01), 1),
            LocationID.PyramidLedge => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2db], 0x40), 1),
            LocationID.FatFairy => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef22c], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef22c], 0x20), 1)
            }),
            LocationID.HauntedGrove => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x08), 1),
            LocationID.HypeCave => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23c], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23c], 0x20), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23c], 0x40), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23c], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23d], 0x04), 1)
            }),
            LocationID.BombosTablet => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x02), 1),
            LocationID.SouthOfGrove => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef237], 0x04), 1),
            LocationID.DiggingGame => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2e8], 0x40), 1),
            LocationID.WaterfallFairy => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef228], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef228], 0x20), 1)
            }),
            LocationID.ZoraArea when sectionIndex == 0 => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef301], 0x40), 1),
            LocationID.ZoraArea => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x02), 1),
            LocationID.Catfish => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x20), 1),
            LocationID.GraveyardLedge => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef237], 0x02), 1),
            LocationID.DesertLedge => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2b0], 0x40), 1),
            LocationID.CShapedHouse => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef238], 0x10), 1),
            LocationID.TreasureGame => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef20d], 0x04), 1),
            LocationID.BombableShack => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef20c], 0x10), 1),
            LocationID.Blacksmith => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x04), 1),
            LocationID.PurpleChest => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x10), 1),
            LocationID.HammerPegs => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef24f], 0x04), 1),
            LocationID.BumperCave => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2ca], 0x40), 1),
            LocationID.LakeHyliaIsland => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2b5], 0x40), 1),
            LocationID.MireShack => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef21a], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef21a], 0x20), 1)
            }),
            LocationID.CheckerboardCave => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef24d], 0x02), 1),
            LocationID.OldMan => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x01), 1),
            LocationID.SpectacleRock when sectionIndex == 0 => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef283], 0x40), 1),
            LocationID.SpectacleRock => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1d5], 0x04), 1),
            LocationID.EtherTablet => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x01), 1),
            LocationID.SpikeCave => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef22e], 0x10), 1),
            LocationID.SpiralCave => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1fc], 0x10), 1),
            LocationID.ParadoxCave when sectionIndex == 0 => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1fe], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1fe], 0x20), 1)
            }),
            LocationID.ParadoxCave => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1de], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1de], 0x20), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1de], 0x40), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1de], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1df], 0x01), 1)
            }),
            LocationID.SuperBunnyCave => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1f0], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1f0], 0x20), 1)
            }),
            LocationID.HookshotCave when sectionIndex == 0 => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef078], 0x80), 1),
            LocationID.HookshotCave => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef078], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef078], 0x20), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef078], 0x40), 1)
            }),
            LocationID.FloatingIsland => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef285], 0x40), 1),
            LocationID.MimicCave => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef218], 0x10), 1),
            LocationID.HyruleCastle => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4c0], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef434], 0xF0, 4)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.HCMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.HCSmallKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.AgahnimTower when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement,
                new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4c3], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef435], 0x3, 0)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.ATSmallKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.AgahnimTower => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef3c5], 2, 1),
            LocationID.EasternPalace when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4c1], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef436], 0x07, 0)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.EPMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _compassShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.EPCompass]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _bigKeyShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.EPBigKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.EasternPalace => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef191], 0x08), 1),
            LocationID.DesertPalace when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4c2], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef435], 0xE0, 5)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.DPMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _compassShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.DPCompass]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.DPSmallKey]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _bigKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.DPBigKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.DesertPalace => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef067], 0x08), 1),
            LocationID.TowerOfHera when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4c9], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef435], 0x1C, 2)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.ToHMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _compassShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.ToHCompass]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.ToHSmallKey]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _bigKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.ToHBigKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.TowerOfHera => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef00f], 0x08), 1),
            LocationID.PalaceOfDarkness when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4c5], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef434], 0x0F, 0)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.PoDMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _compassShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.PoDCompass]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.PoDSmallKey]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _bigKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.PoDBigKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.PalaceOfDarkness => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0b5], 0x08), 1),
            LocationID.SwampPalace when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4c4], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef439], 0xF, 0),
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.SPMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _compassShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.SPCompass]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.SPSmallKey]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _bigKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.SPBigKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.SwampPalace => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef00d], 0x08), 1),
            LocationID.SkullWoods when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4c7], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef437], 0xF0, 4)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.SWMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _compassShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.SWCompass]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.SWSmallKey]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _bigKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.SWBigKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.SkullWoods => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef053], 0x08), 1),
            LocationID.ThievesTown when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4ca], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef437], 0xF, 0)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.TTMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _compassShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.TTCompass]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.TTSmallKey]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _bigKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.TTBigKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.ThievesTown => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef159], 0x08), 1),
            LocationID.IcePalace when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4c8], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef438], 0xF0, 4)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.IPMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _compassShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.IPCompass]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.IPSmallKey]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _bigKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.IPBigKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.IcePalace => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1bd], 0x08), 1),
            LocationID.MiseryMire when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4c6], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef438], 0xF, 0)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.MMMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _compassShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.MMCompass]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.MMSmallKey]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _bigKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.MMBigKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.MiseryMire => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef121], 0x08), 1),
            LocationID.TurtleRock when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4cb], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef439], 0xF0, 4)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.TRMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _compassShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.TRCompass]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.TRSmallKey]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _bigKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.TRBigKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            LocationID.TurtleRock => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef149], 0x08), 1),
            LocationID.GanonsTower when sectionIndex == 0 => new AutoTrackConditionalValue(
                _raceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                    new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressValue(_memoryAddressProvider.MemoryAddresses[0x7ef4cc], 255),
                        new AutoTrackBitwiseIntegerValue(_memoryAddressProvider.MemoryAddresses[0x7ef436], 0xF8, 3)
                    }),
                    new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackConditionalValue(
                            _mapShuffleRequirements[false], new AutoTrackItemValue(_items[ItemType.GTMap]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _compassShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.GTCompass]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _smallKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.GTSmallKey]),
                            new AutoTrackStaticValue(0)),
                        new AutoTrackConditionalValue(
                            _bigKeyShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.GTBigKey]),
                            new AutoTrackStaticValue(0))
                    })),
                null),
            _ => null
        };
    }
}