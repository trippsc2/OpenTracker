using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Multiple;
using OpenTracker.Models.AutoTracking.Values.Single;
using OpenTracker.Models.AutoTracking.Values.Static;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Factories;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements.AutoTracking;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.CompassShuffle;
using OpenTracker.Models.Requirements.MapShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using OpenTracker.Models.Sections.Factories;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Factories;

[ExcludeFromCodeCoverage]
public sealed class SectionAutoTrackingFactoryTests
{
    private static readonly IMemoryAddressProvider MemoryAddressProvider = new MemoryAddressProvider();
    private static readonly IItemDictionary Items = new ItemDictionary(() => Substitute.For<IItemFactory>());

    private static readonly IBigKeyShuffleRequirementDictionary BigKeyShuffleRequirements =
        new BigKeyShuffleRequirementDictionary(_ => Substitute.For<IBigKeyShuffleRequirement>());
    private static readonly ICompassShuffleRequirementDictionary CompassShuffleRequirements =
        new CompassShuffleRequirementDictionary(_ => Substitute.For<ICompassShuffleRequirement>());
    private static readonly IMapShuffleRequirementDictionary MapShuffleRequirements =
        new MapShuffleRequirementDictionary(_ => Substitute.For<IMapShuffleRequirement>());
    private static readonly IRaceIllegalTrackingRequirement RaceIllegalTrackingRequirement =
        Substitute.For<IRaceIllegalTrackingRequirement>();
    private static readonly ISmallKeyShuffleRequirementDictionary SmallKeyShuffleRequirements =
        new SmallKeyShuffleRequirementDictionary(_ => Substitute.For<ISmallKeyShuffleRequirement>());

    private static readonly IMemoryFlag.Factory MemoryFlagFactory = (memoryAddress, flag) =>
        new MemoryFlag(memoryAddress, flag);

    private static readonly Dictionary<(LocationID id, int index), ExpectedObject> ExpectedValues = new();
    private static readonly List<(LocationID id, int index)> NullValues = new();

    private readonly SectionAutoTrackingFactory _sut = new(
        MemoryAddressProvider, Items, BigKeyShuffleRequirements, CompassShuffleRequirements,
        MapShuffleRequirements, RaceIllegalTrackingRequirement, SmallKeyShuffleRequirements, MemoryFlagFactory);

    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
        {
            for (var i = 0; i < 5; i++)
            {
                IAutoTrackValue? value = id switch
                {
                    LocationID.LinksHouse => new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef001], 0x04), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef208], 0x10), 1)
                    }),
                    LocationID.Pedestal => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef300], 0x40), 1),
                    LocationID.LumberjackCave => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1c5], 0x02), 1),
                    LocationID.BlindsHouse when i == 0 => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23a], 0x20), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23a], 0x40), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23a], 0x80), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23b], 0x01), 1),
                    }),
                    LocationID.BlindsHouse => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23a], 0x10), 1),
                    LocationID.TheWell when i == 0 => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef05e], 0x20), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef05e], 0x40), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef05e], 0x80), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef05f], 0x01), 1),
                    }),
                    LocationID.TheWell => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef05e], 0x10), 1),
                    LocationID.BottleVendor => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x02), 1),
                    LocationID.ChickenHouse => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef210], 0x10), 1),
                    LocationID.Tavern => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef206], 0x10), 1),
                    LocationID.SickKid => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x04), 1),
                    LocationID.MagicBat => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x80), 1),
                    LocationID.RaceGame => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2a8], 0x40), 1),
                    LocationID.Library => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x80), 1),
                    LocationID.MushroomSpot => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x10), 1),
                    LocationID.ForestHideout => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1c3], 0x02), 1),
                    LocationID.CastleSecret when i == 1 => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef3c6], 0x01), 1),
                    LocationID.CastleSecret => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef0aa], 0x10), 1),
                    LocationID.WitchsHut => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x20), 1),
                    LocationID.SahasrahlasHut when i == 0 => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef20a], 0x10), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef20a], 0x20), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef20a], 0x40), 1),
                    }),
                    LocationID.SahasrahlasHut => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x10), 1),
                    LocationID.BonkRocks => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef248], 0x10), 1),
                    LocationID.KingsTomb => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef226], 0x10), 1),
                    LocationID.AginahsCave => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef214], 0x10), 1),
                    LocationID.GroveDiggingSpot => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2aa], 0x40), 1),
                    LocationID.Dam when i == 0 => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef216], 0x10), 1),
                    LocationID.Dam => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2bb], 0x40), 1),
                    LocationID.MiniMoldormCave => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef246], 0x10), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef246], 0x20), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef246], 0x40), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef246], 0x80), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef247], 0x04), 1)
                    }),
                    LocationID.IceRodCave => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef240], 0x10), 1),
                    LocationID.Hobo => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x01), 1),
                    LocationID.PyramidLedge => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2db], 0x40), 1),
                    LocationID.FatFairy => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef22c], 0x10), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef22c], 0x20), 1)
                    }),
                    LocationID.HauntedGrove => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x08), 1),
                    LocationID.HypeCave => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23c], 0x10), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23c], 0x20), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23c], 0x40), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23c], 0x80), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23d], 0x04), 1)
                    }),
                    LocationID.BombosTablet => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x02), 1),
                    LocationID.SouthOfGrove => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef237], 0x04), 1),
                    LocationID.DiggingGame => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2e8], 0x40), 1),
                    LocationID.WaterfallFairy => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef228], 0x10), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef228], 0x20), 1)
                    }),
                    LocationID.ZoraArea when i == 0 => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef301], 0x40), 1),
                    LocationID.ZoraArea => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x02), 1),
                    LocationID.Catfish => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x20), 1),
                    LocationID.GraveyardLedge => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef237], 0x02), 1),
                    LocationID.DesertLedge => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2b0], 0x40), 1),
                    LocationID.CShapedHouse => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef238], 0x10), 1),
                    LocationID.TreasureGame => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef20d], 0x04), 1),
                    LocationID.BombableShack => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef20c], 0x10), 1),
                    LocationID.Blacksmith => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x04), 1),
                    LocationID.PurpleChest => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x10), 1),
                    LocationID.HammerPegs => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef24f], 0x04), 1),
                    LocationID.BumperCave => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2ca], 0x40), 1),
                    LocationID.LakeHyliaIsland => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2b5], 0x40), 1),
                    LocationID.MireShack => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef21a], 0x10), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef21a], 0x20), 1)
                    }),
                    LocationID.CheckerboardCave => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef24d], 0x02), 1),
                    LocationID.OldMan => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x01), 1),
                    LocationID.SpectacleRock when i == 0 => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef283], 0x40), 1),
                    LocationID.SpectacleRock => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1d5], 0x04), 1),
                    LocationID.EtherTablet => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x01), 1),
                    LocationID.SpikeCave => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef22e], 0x10), 1),
                    LocationID.SpiralCave => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1fc], 0x10), 1),
                    LocationID.ParadoxCave when i == 0 => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1fe], 0x10), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1fe], 0x20), 1)
                    }),
                    LocationID.ParadoxCave => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1de], 0x10), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1de], 0x20), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1de], 0x40), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1de], 0x80), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1df], 0x01), 1)
                    }),
                    LocationID.SuperBunnyCave => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1f0], 0x10), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1f0], 0x20), 1)
                    }),
                    LocationID.HookshotCave when i == 0 => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef078], 0x80), 1),
                    LocationID.HookshotCave => new AutoTrackMultipleSum(new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef078], 0x10), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef078], 0x20), 1),
                        new AutoTrackFlagBool(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef078], 0x40), 1)
                    }),
                    LocationID.FloatingIsland => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef285], 0x40), 1),
                    LocationID.MimicCave => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef218], 0x10), 1),
                    LocationID.HyruleCastle => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4c0], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef434], 0xF0, 4)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.HCMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.HCSmallKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.AgahnimTower when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement,
                        new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4c3], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef435], 0x3, 0)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.ATSmallKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.AgahnimTower => new AutoTrackAddressBool(
                        MemoryAddressProvider.MemoryAddresses[0x7ef3c5], 2, 1),
                    LocationID.EasternPalace when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4c1], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef436], 0x07, 0)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.EPMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    CompassShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.EPCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    BigKeyShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.EPBigKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.EasternPalace => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef191], 0x08), 1),
                    LocationID.DesertPalace when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4c2], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef435], 0xE0, 5)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.DPMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    CompassShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.DPCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.DPSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    BigKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.DPBigKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.DesertPalace => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef067], 0x08), 1),
                    LocationID.TowerOfHera when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4c9], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef435], 0x1C, 2)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.ToHMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    CompassShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.ToHCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.ToHSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    BigKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.ToHBigKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.TowerOfHera => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef00f], 0x08), 1),
                    LocationID.PalaceOfDarkness when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4c5], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef434], 0x0F, 0)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.PoDMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    CompassShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.PoDCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.PoDSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    BigKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.PoDBigKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.PalaceOfDarkness => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef0b5], 0x08), 1),
                    LocationID.SwampPalace when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4c4], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef439], 0xF, 0),
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.SPMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    CompassShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.SPCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.SPSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    BigKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.SPBigKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.SwampPalace => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef00d], 0x08), 1),
                    LocationID.SkullWoods when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4c7], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef437], 0xF0, 4)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.SWMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    CompassShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.SWCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.SWSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    BigKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.SWBigKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.SkullWoods => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef053], 0x08), 1),
                    LocationID.ThievesTown when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4ca], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef437], 0xF, 0)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.TTMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    CompassShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.TTCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.TTSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    BigKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.TTBigKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.ThievesTown => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef159], 0x08), 1),
                    LocationID.IcePalace when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4c8], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef438], 0xF0, 4)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.IPMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    CompassShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.IPCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.IPSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    BigKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.IPBigKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.IcePalace => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1bd], 0x08), 1),
                    LocationID.MiseryMire when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4c6], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef438], 0xF, 0)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.MMMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    CompassShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.MMCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.MMSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    BigKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.MMBigKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.MiseryMire => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef121], 0x08), 1),
                    LocationID.TurtleRock when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4cb], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef439], 0xF0, 4)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.TRMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    CompassShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.TRCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.TRSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    BigKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.TRBigKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    LocationID.TurtleRock => new AutoTrackFlagBool(
                        MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef149], 0x08), 1),
                    LocationID.GanonsTower when i == 0 => new AutoTrackConditionalValue(
                        RaceIllegalTrackingRequirement, new AutoTrackMultipleDifference(
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(MemoryAddressProvider.MemoryAddresses[0x7ef4cc], 255),
                                new AutoTrackBitwiseIntegerValue(MemoryAddressProvider.MemoryAddresses[0x7ef436], 0xF8, 3)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    MapShuffleRequirements[false], new AutoTrackItemValue(Items[ItemType.GTMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    CompassShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.GTCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    SmallKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.GTSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    BigKeyShuffleRequirements[false],
                                    new AutoTrackItemValue(Items[ItemType.GTBigKey]),
                                    new AutoTrackStaticValue(0))
                            })),
                        null),
                    _ => null
                };

                if (value is null)
                {
                    NullValues.Add((id, i));
                    continue;
                }
                    
                ExpectedValues.Add((id, i), value.ToExpectedObject());
            } 
        }
    }

    [Theory]
    [MemberData(nameof(GetAutoTrackValue_ShouldReturnExpectedValueData))]
    public void GetAutoTrackValue_ShouldReturnExpectedValue(ExpectedObject expected, LocationID id, int index)
    {
        var value = _sut.GetAutoTrackValue(id, index);
            
        expected.ShouldEqual(value);
    }

    public static IEnumerable<object[]> GetAutoTrackValue_ShouldReturnExpectedValueData()
    {
        PopulateExpectedValues();

        return ExpectedValues.Keys.Select(key => new object[] {ExpectedValues[key], key.id, key.index}).ToList();
    }

    [Theory]
    [MemberData(nameof(GetAutoTrackValue_ShouldReturnNullData))]
    public void GetAutoTrackValue_ShouldReturnNull(LocationID id, int index)
    {
        var value = _sut.GetAutoTrackValue(id, index);
            
        Assert.Null(value);
    }
        
    public static IEnumerable<object[]> GetAutoTrackValue_ShouldReturnNullData()
    {
        PopulateExpectedValues();

        return NullValues.Select(value => new object[] {value.id, value.index}).ToList();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<ISectionAutoTrackingFactory>();
            
        Assert.NotNull(sut as SectionAutoTrackingFactory);
    }
}