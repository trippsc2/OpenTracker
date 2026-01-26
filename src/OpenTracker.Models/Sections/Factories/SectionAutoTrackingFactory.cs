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
        
    private readonly IAutoTrackAddressBool.Factory _boolFactory;
    private readonly IAutoTrackAddressValue.Factory _valueFactory;
    private readonly IAutoTrackBitwiseIntegerValue.Factory _bitwiseIntegerFactory;
    private readonly IAutoTrackConditionalValue.Factory _conditionalFactory;
    private readonly IAutoTrackFlagBool.Factory _flagBoolFactory;
    private readonly IAutoTrackItemValue.Factory _itemValueFactory;
    private readonly IAutoTrackMultipleDifference.Factory _differenceFactory;
    private readonly IAutoTrackMultipleOverride.Factory _overrideFactory;
    private readonly IAutoTrackMultipleSum.Factory _sumFactory;
    private readonly IAutoTrackStaticValue.Factory _staticValueFactory;
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
    /// <param name="boolFactory">
    ///     An Autofac factory creating new <see cref="IAutoTrackAddressBool"/> objects.
    /// </param>
    /// <param name="valueFactory">
    ///     An Autofac factory creating new <see cref="IAutoTrackAddressValue"/> objects.
    /// </param>
    /// <param name="bitwiseIntegerFactory">
    ///     An Autofac factory creating new <see cref="IAutoTrackBitwiseIntegerValue"/> objects.
    /// </param>
    /// <param name="conditionalFactory">
    ///     An Autofac factory creating new <see cref="IAutoTrackConditionalValue"/> objects.
    /// </param>
    /// <param name="flagBoolFactory">
    ///     An Autofac factory creating new <see cref="IAutoTrackFlagBool"/> objects.
    /// </param>
    /// <param name="itemValueFactory">
    ///     An Autofac factory creating new <see cref="IAutoTrackItemValue"/> objects.
    /// </param>
    /// <param name="differenceFactory">
    ///     An Autofac factory creating new <see cref="IAutoTrackMultipleDifference"/> objects.
    /// </param>
    /// <param name="overrideFactory">
    ///     An Autofac factory creating new <see cref="IAutoTrackMultipleOverride"/> objects.
    /// </param>
    /// <param name="sumFactory">
    ///     An Autofac factory creating new <see cref="IAutoTrackMultipleSum"/> objects.
    /// </param>
    /// <param name="staticValueFactory">
    ///     An Autofac factory creating new <see cref="IAutoTrackStaticValue"/> objects.
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
        IAutoTrackAddressBool.Factory boolFactory, IAutoTrackAddressValue.Factory valueFactory,
        IAutoTrackBitwiseIntegerValue.Factory bitwiseIntegerFactory,
        IAutoTrackConditionalValue.Factory conditionalFactory,
        IAutoTrackFlagBool.Factory flagBoolFactory, IAutoTrackItemValue.Factory itemValueFactory,
        IAutoTrackMultipleDifference.Factory differenceFactory,
        IAutoTrackMultipleOverride.Factory overrideFactory,
        IAutoTrackMultipleSum.Factory sumFactory, IAutoTrackStaticValue.Factory staticValueFactory,
        IMemoryFlag.Factory memoryFlagFactory)
    {
        _memoryAddressProvider = memoryAddressProvider;
        _items = items;

        _boolFactory = boolFactory;
        _valueFactory = valueFactory;
        _bitwiseIntegerFactory = bitwiseIntegerFactory;
        _conditionalFactory = conditionalFactory;
        _flagBoolFactory = flagBoolFactory;
        _itemValueFactory = itemValueFactory;
        _differenceFactory = differenceFactory;
        _overrideFactory = overrideFactory;
        _sumFactory = sumFactory;
        _staticValueFactory = staticValueFactory;
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
            LocationID.LinksHouse => _overrideFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef001], 0x04), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef208], 0x10), 1)
            }),
            LocationID.Pedestal => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef300], 0x40), 1),
            LocationID.LumberjackCave => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1c5], 0x02), 1),
            LocationID.BlindsHouse when sectionIndex == 0 => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23a], 0x20), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23a], 0x40), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23a], 0x80), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23b], 0x01), 1),
            }),
            LocationID.BlindsHouse => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23a], 0x10), 1),
            LocationID.TheWell when sectionIndex == 0 => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef05e], 0x20), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef05e], 0x40), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef05e], 0x80), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef05f], 0x01), 1),
            }),
            LocationID.TheWell => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef05e], 0x10), 1),
            LocationID.BottleVendor => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x02), 1),
            LocationID.ChickenHouse => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef210], 0x10), 1),
            LocationID.Tavern => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef206], 0x10), 1),
            LocationID.SickKid => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x04), 1),
            LocationID.MagicBat => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x80), 1),
            LocationID.RaceGame => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2a8], 0x40), 1),
            LocationID.Library => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x80), 1),
            LocationID.MushroomSpot => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x10), 1),
            LocationID.ForestHideout => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1c3], 0x02), 1),
            LocationID.CastleSecret when sectionIndex == 1 => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef3c6], 0x01), 1),
            LocationID.CastleSecret => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0aa], 0x10), 1),
            LocationID.WitchsHut => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x20), 1),
            LocationID.SahasrahlasHut when sectionIndex == 0 => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef20a], 0x10), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef20a], 0x20), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef20a], 0x40), 1),
            }),
            LocationID.SahasrahlasHut => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x10), 1),
            LocationID.BonkRocks => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef248], 0x10), 1),
            LocationID.KingsTomb => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef226], 0x10), 1),
            LocationID.AginahsCave => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef214], 0x10), 1),
            LocationID.GroveDiggingSpot => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2aa], 0x40), 1),
            LocationID.Dam when sectionIndex == 0 => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef216], 0x10), 1),
            LocationID.Dam => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2bb], 0x40), 1),
            LocationID.MiniMoldormCave => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef246], 0x10), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef246], 0x20), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef246], 0x40), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef246], 0x80), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef247], 0x04), 1)
            }),
            LocationID.IceRodCave => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef240], 0x10), 1),
            LocationID.Hobo => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x01), 1),
            LocationID.PyramidLedge => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2db], 0x40), 1),
            LocationID.FatFairy => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef22c], 0x10), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef22c], 0x20), 1)
            }),
            LocationID.HauntedGrove => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x08), 1),
            LocationID.HypeCave => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23c], 0x10), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23c], 0x20), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23c], 0x40), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23c], 0x80), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef23d], 0x04), 1)
            }),
            LocationID.BombosTablet => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x02), 1),
            LocationID.SouthOfGrove => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef237], 0x04), 1),
            LocationID.DiggingGame => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2e8], 0x40), 1),
            LocationID.WaterfallFairy => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef228], 0x10), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef228], 0x20), 1)
            }),
            LocationID.ZoraArea when sectionIndex == 0 => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef301], 0x40), 1),
            LocationID.ZoraArea => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x02), 1),
            LocationID.Catfish => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x20), 1),
            LocationID.GraveyardLedge => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef237], 0x02), 1),
            LocationID.DesertLedge => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2b0], 0x40), 1),
            LocationID.CShapedHouse => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef238], 0x10), 1),
            LocationID.TreasureGame => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef20d], 0x04), 1),
            LocationID.BombableShack => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef20c], 0x10), 1),
            LocationID.Blacksmith => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x04), 1),
            LocationID.PurpleChest => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x10), 1),
            LocationID.HammerPegs => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef24f], 0x04), 1),
            LocationID.BumperCave => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2ca], 0x40), 1),
            LocationID.LakeHyliaIsland => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef2b5], 0x40), 1),
            LocationID.MireShack => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef21a], 0x10), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef21a], 0x20), 1)
            }),
            LocationID.CheckerboardCave => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef24d], 0x02), 1),
            LocationID.OldMan => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef410], 0x01), 1),
            LocationID.SpectacleRock when sectionIndex == 0 => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef283], 0x40), 1),
            LocationID.SpectacleRock => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1d5], 0x04), 1),
            LocationID.EtherTablet => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x01), 1),
            LocationID.SpikeCave => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef22e], 0x10), 1),
            LocationID.SpiralCave => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1fc], 0x10), 1),
            LocationID.ParadoxCave when sectionIndex == 0 => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1fe], 0x10), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1fe], 0x20), 1)
            }),
            LocationID.ParadoxCave => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1de], 0x10), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1de], 0x20), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1de], 0x40), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1de], 0x80), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1df], 0x01), 1)
            }),
            LocationID.SuperBunnyCave => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1f0], 0x10), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1f0], 0x20), 1)
            }),
            LocationID.HookshotCave when sectionIndex == 0 => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef078], 0x80), 1),
            LocationID.HookshotCave => _sumFactory(new List<IAutoTrackValue>
            {
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef078], 0x10), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef078], 0x20), 1),
                _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef078], 0x40), 1)
            }),
            LocationID.FloatingIsland => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef285], 0x40), 1),
            LocationID.MimicCave => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef218], 0x10), 1),
            LocationID.HyruleCastle => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4c0], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef434], 0xF0, 4)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.HCMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.HCSmallKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.AgahnimTower when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement,
                _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4c3], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef435], 0x3, 0)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.ATSmallKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.AgahnimTower => _boolFactory(
                _memoryAddressProvider.MemoryAddresses[0x7ef3c5], 2, 1),
            LocationID.EasternPalace when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4c1], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef436], 0x07, 0)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false], _itemValueFactory(_items[ItemType.EPMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _compassShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.EPCompass]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _bigKeyShuffleRequirements[false], _itemValueFactory(_items[ItemType.EPBigKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.EasternPalace => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef191], 0x08), 1),
            LocationID.DesertPalace when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4c2], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef435], 0xE0, 5)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false], _itemValueFactory(_items[ItemType.DPMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _compassShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.DPCompass]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.DPSmallKey]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _bigKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.DPBigKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.DesertPalace => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef067], 0x08), 1),
            LocationID.TowerOfHera when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4c9], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef435], 0x1C, 2)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false], _itemValueFactory(_items[ItemType.ToHMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _compassShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.ToHCompass]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.ToHSmallKey]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _bigKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.ToHBigKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.TowerOfHera => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef00f], 0x08), 1),
            LocationID.PalaceOfDarkness when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4c5], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef434], 0x0F, 0)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false], _itemValueFactory(_items[ItemType.PoDMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _compassShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.PoDCompass]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.PoDSmallKey]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _bigKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.PoDBigKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.PalaceOfDarkness => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0b5], 0x08), 1),
            LocationID.SwampPalace when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4c4], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef439], 0xF, 0),
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false], _itemValueFactory(_items[ItemType.SPMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _compassShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.SPCompass]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.SPSmallKey]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _bigKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.SPBigKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.SwampPalace => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef00d], 0x08), 1),
            LocationID.SkullWoods when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4c7], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef437], 0xF0, 4)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false], _itemValueFactory(_items[ItemType.SWMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _compassShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.SWCompass]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.SWSmallKey]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _bigKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.SWBigKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.SkullWoods => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef053], 0x08), 1),
            LocationID.ThievesTown when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4ca], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef437], 0xF, 0)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false], _itemValueFactory(_items[ItemType.TTMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _compassShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.TTCompass]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.TTSmallKey]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _bigKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.TTBigKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.ThievesTown => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef159], 0x08), 1),
            LocationID.IcePalace when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4c8], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef438], 0xF0, 4)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false], _itemValueFactory(_items[ItemType.IPMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _compassShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.IPCompass]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.IPSmallKey]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _bigKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.IPBigKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.IcePalace => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1bd], 0x08), 1),
            LocationID.MiseryMire when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4c6], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef438], 0xF, 0)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false], _itemValueFactory(_items[ItemType.MMMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _compassShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.MMCompass]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.MMSmallKey]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _bigKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.MMBigKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.MiseryMire => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef121], 0x08), 1),
            LocationID.TurtleRock when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4cb], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef439], 0xF0, 4)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false], _itemValueFactory(_items[ItemType.TRMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _compassShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.TRCompass]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.TRSmallKey]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _bigKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.TRBigKey]),
                            _staticValueFactory(0))
                    })),
                null),
            LocationID.TurtleRock => _flagBoolFactory(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef149], 0x08), 1),
            LocationID.GanonsTower when sectionIndex == 0 => _conditionalFactory(
                _raceIllegalTrackingRequirement, _differenceFactory(
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(_memoryAddressProvider.MemoryAddresses[0x7ef4cc], 255, 0),
                        _bitwiseIntegerFactory(_memoryAddressProvider.MemoryAddresses[0x7ef436], 0xF8, 3)
                    }),
                    _sumFactory(new List<IAutoTrackValue>
                    {
                        _conditionalFactory(
                            _mapShuffleRequirements[false], _itemValueFactory(_items[ItemType.GTMap]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _compassShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.GTCompass]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _smallKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.GTSmallKey]),
                            _staticValueFactory(0)),
                        _conditionalFactory(
                            _bigKeyShuffleRequirements[false],
                            _itemValueFactory(_items[ItemType.GTBigKey]),
                            _staticValueFactory(0))
                    })),
                null),
            _ => null
        };
    }
}