using System;
using System.Collections.Generic;
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

namespace OpenTracker.UnitTests.Models.Sections.Factories
{
    public class SectionAutoTrackingFactoryTests
    {
        private static readonly IMemoryAddressProvider MemoryAddressProvider = new MemoryAddressProvider(() => 
            Substitute.For<IMemoryAddress>());
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
        
        private static readonly IAutoTrackAddressBool.Factory BoolFactory = (memoryAddress, comparison, trueValue) => 
            new AutoTrackAddressBool(memoryAddress, comparison, trueValue);
        private static readonly IAutoTrackAddressValue.Factory ValueFactory = (memoryAddress, maximum, adjustment) => 
            new AutoTrackAddressValue(memoryAddress, maximum, adjustment);
        private static readonly IAutoTrackBitwiseIntegerValue.Factory BitwiseIntegerFactory = (memoryAddress, mask, shift) =>
            new AutoTrackBitwiseIntegerValue(memoryAddress, mask, shift);
        private static readonly IAutoTrackConditionalValue.Factory ConditionalFactory = (condition, trueValue, falseValue) => 
            new AutoTrackConditionalValue(condition, trueValue, falseValue);
        private static readonly IAutoTrackFlagBool.Factory FlagBoolFactory = (flag, trueValue) =>
            new AutoTrackFlagBool(flag, trueValue);
        private static readonly IAutoTrackItemValue.Factory ItemValueFactory = item => new AutoTrackItemValue(item);
        private static readonly IAutoTrackMultipleDifference.Factory DifferenceFactory = (value1, value2) => 
                new AutoTrackMultipleDifference(value1, value2);
        private static readonly IAutoTrackMultipleOverride.Factory OverrideFactory =
            values => new AutoTrackMultipleOverride(values);
        private static readonly IAutoTrackMultipleSum.Factory SumFactory = values => new AutoTrackMultipleSum(values);
        private static readonly IAutoTrackStaticValue.Factory StaticValueFactory = value => new AutoTrackStaticValue(value);
        
        private static readonly IMemoryFlag.Factory MemoryFlagFactory = (memoryAddress, flag) =>
            new MemoryFlag(memoryAddress, flag);

        private static readonly Dictionary<(LocationID id, int index), ExpectedObject> ExpectedValues = new();
        private static readonly List<(LocationID id, int index)> NullValues = new();

        private readonly SectionAutoTrackingFactory _sut = new(
            MemoryAddressProvider, Items, BigKeyShuffleRequirements, CompassShuffleRequirements,
            MapShuffleRequirements, RaceIllegalTrackingRequirement, SmallKeyShuffleRequirements,
            BoolFactory, ValueFactory, BitwiseIntegerFactory, ConditionalFactory,
            FlagBoolFactory, ItemValueFactory, DifferenceFactory, OverrideFactory, SumFactory,
            StaticValueFactory, MemoryFlagFactory);

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
            {
                for (var i = 0; i < 5; i++)
                {
                    IAutoTrackValue? value = id switch
                    {
                        LocationID.LinksHouse => OverrideFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef001], 0x04), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef208], 0x10), 1)
                        }),
                        LocationID.Pedestal => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef300], 0x40), 1),
                        LocationID.LumberjackCave => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1c5], 0x02), 1),
                        LocationID.BlindsHouse when i == 0 => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23a], 0x20), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23a], 0x40), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23a], 0x80), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23b], 0x01), 1),
                        }),
                        LocationID.BlindsHouse => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23a], 0x10), 1),
                        LocationID.TheWell when i == 0 => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef05e], 0x20), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef05e], 0x40), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef05e], 0x80), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef05f], 0x01), 1),
                        }),
                        LocationID.TheWell => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef05e], 0x10), 1),
                        LocationID.BottleVendor => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x02), 1),
                        LocationID.ChickenHouse => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef210], 0x10), 1),
                        LocationID.Tavern => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef206], 0x10), 1),
                        LocationID.SickKid => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x04), 1),
                        LocationID.MagicBat => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x80), 1),
                        LocationID.RaceGame => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2a8], 0x40), 1),
                        LocationID.Library => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x80), 1),
                        LocationID.MushroomSpot => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x10), 1),
                        LocationID.ForestHideout => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1c3], 0x02), 1),
                        LocationID.CastleSecret when i == 1 => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef3c6], 0x01), 1),
                        LocationID.CastleSecret => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef0aa], 0x10), 1),
                        LocationID.WitchsHut => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x20), 1),
                        LocationID.SahasrahlasHut when i == 0 => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef20a], 0x10), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef20a], 0x20), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef20a], 0x40), 1),
                        }),
                        LocationID.SahasrahlasHut => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x10), 1),
                        LocationID.BonkRocks => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef248], 0x10), 1),
                        LocationID.KingsTomb => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef226], 0x10), 1),
                        LocationID.AginahsCave => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef214], 0x10), 1),
                        LocationID.GroveDiggingSpot => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2aa], 0x40), 1),
                        LocationID.Dam when i == 0 => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef216], 0x10), 1),
                        LocationID.Dam => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2bb], 0x40), 1),
                        LocationID.MiniMoldormCave => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef246], 0x10), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef246], 0x20), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef246], 0x40), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef246], 0x80), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef247], 0x04), 1)
                        }),
                        LocationID.IceRodCave => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef240], 0x10), 1),
                        LocationID.Hobo => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x01), 1),
                        LocationID.PyramidLedge => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2db], 0x40), 1),
                        LocationID.FatFairy => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef22c], 0x10), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef22c], 0x20), 1)
                        }),
                        LocationID.HauntedGrove => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x08), 1),
                        LocationID.HypeCave => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23c], 0x10), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23c], 0x20), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23c], 0x40), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23c], 0x80), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef23d], 0x04), 1)
                        }),
                        LocationID.BombosTablet => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x02), 1),
                        LocationID.SouthOfGrove => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef237], 0x04), 1),
                        LocationID.DiggingGame => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2e8], 0x40), 1),
                        LocationID.WaterfallFairy => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef228], 0x10), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef228], 0x20), 1)
                        }),
                        LocationID.ZoraArea when i == 0 => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef301], 0x40), 1),
                        LocationID.ZoraArea => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x02), 1),
                        LocationID.Catfish => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x20), 1),
                        LocationID.GraveyardLedge => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef237], 0x02), 1),
                        LocationID.DesertLedge => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2b0], 0x40), 1),
                        LocationID.CShapedHouse => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef238], 0x10), 1),
                        LocationID.TreasureGame => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef20d], 0x04), 1),
                        LocationID.BombableShack => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef20c], 0x10), 1),
                        LocationID.Blacksmith => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x04), 1),
                        LocationID.PurpleChest => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef3c9], 0x10), 1),
                        LocationID.HammerPegs => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef24f], 0x04), 1),
                        LocationID.BumperCave => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2ca], 0x40), 1),
                        LocationID.LakeHyliaIsland => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef2b5], 0x40), 1),
                        LocationID.MireShack => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef21a], 0x10), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef21a], 0x20), 1)
                        }),
                        LocationID.CheckerboardCave => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef24d], 0x02), 1),
                        LocationID.OldMan => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef410], 0x01), 1),
                        LocationID.SpectacleRock when i == 0 => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef283], 0x40), 1),
                        LocationID.SpectacleRock => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1d5], 0x04), 1),
                        LocationID.EtherTablet => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef411], 0x01), 1),
                        LocationID.SpikeCave => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef22e], 0x10), 1),
                        LocationID.SpiralCave => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1fc], 0x10), 1),
                        LocationID.ParadoxCave when i == 0 => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1fe], 0x10), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1fe], 0x20), 1)
                        }),
                        LocationID.ParadoxCave => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1de], 0x10), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1de], 0x20), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1de], 0x40), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1de], 0x80), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1df], 0x01), 1)
                        }),
                        LocationID.SuperBunnyCave => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1f0], 0x10), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1f0], 0x20), 1)
                        }),
                        LocationID.HookshotCave when i == 0 => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef078], 0x80), 1),
                        LocationID.HookshotCave => SumFactory(new List<IAutoTrackValue>
                        {
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef078], 0x10), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef078], 0x20), 1),
                            FlagBoolFactory(
                                MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef078], 0x40), 1)
                        }),
                        LocationID.FloatingIsland => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef285], 0x40), 1),
                        LocationID.MimicCave => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef218], 0x10), 1),
                        LocationID.HyruleCastle => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4c0], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef434], 0xF0, 4)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.HCMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.HCSmallKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.AgahnimTower when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement,
                            DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4c3], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef435], 0x3, 0)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.ATSmallKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.AgahnimTower => BoolFactory(
                            MemoryAddressProvider.MemoryAddresses[0x7ef3c5], 2, 1),
                        LocationID.EasternPalace when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4c1], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef436], 0x07, 0)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false], ItemValueFactory(Items[ItemType.EPMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        CompassShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.EPCompass]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        BigKeyShuffleRequirements[false], ItemValueFactory(Items[ItemType.EPBigKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.EasternPalace => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef191], 0x08), 1),
                        LocationID.DesertPalace when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4c2], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef435], 0xE0, 5)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false], ItemValueFactory(Items[ItemType.DPMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        CompassShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.DPCompass]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.DPSmallKey]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        BigKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.DPBigKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.DesertPalace => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef067], 0x08), 1),
                        LocationID.TowerOfHera when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4c9], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef435], 0x1C, 2)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false], ItemValueFactory(Items[ItemType.ToHMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        CompassShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.ToHCompass]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.ToHSmallKey]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        BigKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.ToHBigKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.TowerOfHera => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef00f], 0x08), 1),
                        LocationID.PalaceOfDarkness when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4c5], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef434], 0x0F, 0)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false], ItemValueFactory(Items[ItemType.PoDMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        CompassShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.PoDCompass]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.PoDSmallKey]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        BigKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.PoDBigKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.PalaceOfDarkness => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef0b5], 0x08), 1),
                        LocationID.SwampPalace when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4c4], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef439], 0xF, 0),
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false], ItemValueFactory(Items[ItemType.SPMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        CompassShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.SPCompass]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.SPSmallKey]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        BigKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.SPBigKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.SwampPalace => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef00d], 0x08), 1),
                        LocationID.SkullWoods when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4c7], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef437], 0xF0, 4)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false], ItemValueFactory(Items[ItemType.SWMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        CompassShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.SWCompass]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.SWSmallKey]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        BigKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.SWBigKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.SkullWoods => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef053], 0x08), 1),
                        LocationID.ThievesTown when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4ca], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef437], 0xF, 0)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false], ItemValueFactory(Items[ItemType.TTMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        CompassShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.TTCompass]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.TTSmallKey]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        BigKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.TTBigKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.ThievesTown => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef159], 0x08), 1),
                        LocationID.IcePalace when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4c8], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef438], 0xF0, 4)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false], ItemValueFactory(Items[ItemType.IPMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        CompassShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.IPCompass]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.IPSmallKey]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        BigKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.IPBigKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.IcePalace => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef1bd], 0x08), 1),
                        LocationID.MiseryMire when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4c6], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef438], 0xF, 0)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false], ItemValueFactory(Items[ItemType.MMMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        CompassShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.MMCompass]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.MMSmallKey]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        BigKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.MMBigKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.MiseryMire => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef121], 0x08), 1),
                        LocationID.TurtleRock when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4cb], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef439], 0xF0, 4)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false], ItemValueFactory(Items[ItemType.TRMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        CompassShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.TRCompass]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.TRSmallKey]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        BigKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.TRBigKey]),
                                        StaticValueFactory(0))
                                })),
                            null),
                        LocationID.TurtleRock => FlagBoolFactory(
                            MemoryFlagFactory(MemoryAddressProvider.MemoryAddresses[0x7ef149], 0x08), 1),
                        LocationID.GanonsTower when i == 0 => ConditionalFactory(
                            RaceIllegalTrackingRequirement, DifferenceFactory(
                                OverrideFactory(new List<IAutoTrackValue>
                                {
                                    ValueFactory(MemoryAddressProvider.MemoryAddresses[0x7ef4cc], 255, 0),
                                    BitwiseIntegerFactory(MemoryAddressProvider.MemoryAddresses[0x7ef436], 0xF8, 3)
                                }),
                                SumFactory(new List<IAutoTrackValue>
                                {
                                    ConditionalFactory(
                                        MapShuffleRequirements[false], ItemValueFactory(Items[ItemType.GTMap]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        CompassShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.GTCompass]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        SmallKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.GTSmallKey]),
                                        StaticValueFactory(0)),
                                    ConditionalFactory(
                                        BigKeyShuffleRequirements[false],
                                        ItemValueFactory(Items[ItemType.GTBigKey]),
                                        StaticValueFactory(0))
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
}