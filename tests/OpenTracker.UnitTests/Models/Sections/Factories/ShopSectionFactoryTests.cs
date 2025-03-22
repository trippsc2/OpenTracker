using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.ShopShuffle;
using OpenTracker.Models.Requirements.TakeAnyLocations;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections.Boolean;
using OpenTracker.Models.Sections.Factories;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Factories
{
    public class ShopSectionFactoryTests
    {
        private static readonly IAggregateRequirementDictionary AggregateRequirements =
            new AggregateRequirementDictionary(requirements =>
                new AggregateRequirement(requirements));
        private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
            new AlternativeRequirementDictionary(requirements =>
                new AlternativeRequirement(requirements));
        private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
            new EntranceShuffleRequirementDictionary(_ => Substitute.For<IEntranceShuffleRequirement>());
        private static readonly IShopShuffleRequirementDictionary ShopShuffleRequirements =
            new ShopShuffleRequirementDictionary(_ =>
                Substitute.For<IShopShuffleRequirement>());
        private static readonly ITakeAnyLocationsRequirementDictionary TakeAnyLocationsRequirements =
            new TakeAnyLocationsRequirementDictionary(_ => 
                Substitute.For<ITakeAnyLocationsRequirement>());

        private static readonly IOverworldNodeDictionary OverworldNodes =
            new OverworldNodeDictionary(() => Substitute.For<IOverworldNodeFactory>());

        private static readonly ISaveLoadManager SaveLoadManager = Substitute.For<ISaveLoadManager>();

        private static readonly ICollectSection.Factory CollectSectionFactory = (section, force) =>
            new CollectSection(section, force);
        private static readonly IUncollectSection.Factory UncollectSectionFactory = section =>
            new UncollectSection(section);

        private static readonly IShopSection.Factory Factory = (node, requirement) => 
                new ShopSection(SaveLoadManager, CollectSectionFactory, UncollectSectionFactory, node, requirement);

        private static readonly Dictionary<LocationID, ExpectedObject> ExpectedValues = new();

        private readonly ShopSectionFactory _sut = new ShopSectionFactory(
            AggregateRequirements, AlternativeRequirements, EntranceShuffleRequirements, ShopShuffleRequirements,
            TakeAnyLocationsRequirements, OverworldNodes, Factory);

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
            {
                switch (id)
                {
                    case LocationID.KakarikoShop:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.LightWorld],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement> 
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }], 
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }]).ToExpectedObject());
                        break;
                    case LocationID.LakeHyliaShop:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.LightWorld],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement> 
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }], 
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }]).ToExpectedObject());
                        break;
                    case LocationID.DeathMountainShop:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DeathMountainEastBottom],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement> 
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }], 
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }]).ToExpectedObject());
                        break;
                    case LocationID.PotionShop:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.LWWitchArea],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement> 
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }], 
                                ShopShuffleRequirements[true]
                            }]).ToExpectedObject());
                        break;
                    case LocationID.DarkLumberjackShop:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DarkWorldWest],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement> 
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }], 
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }]).ToExpectedObject());
                        break;
                    case LocationID.RedShieldShop:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DarkWorldWest],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement> 
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }], 
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }]).ToExpectedObject());
                        break;
                    case LocationID.VillageOfOutcastsShop:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.HammerHouse],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement> 
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }], 
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }]).ToExpectedObject());
                        break;
                    case LocationID.DarkLakeHyliaShop:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouth],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement> 
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }], 
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }]).ToExpectedObject());
                        break;
                    case LocationID.DarkPotionShop:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DWWitchArea],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement> 
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }], 
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }]).ToExpectedObject());
                        break;
                    case LocationID.DarkDeathMountainShop:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DarkDeathMountainEastBottom],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement> 
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }], 
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    ShopShuffleRequirements[true],
                                    TakeAnyLocationsRequirements[true]
                                }]
                            }]).ToExpectedObject());
                        break;
                }
            }
        }

        [Fact]
        public void GetShopSection_ShouldThrowException_WhenIDIsUnexpected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _ = _sut.GetShopSection((LocationID) int.MaxValue));
        }

        [Theory]
        [MemberData(nameof(GetShopSection_ShouldReturnExpectedData))]
        public void GetShopSection_ShouldReturnExpected(ExpectedObject expected, LocationID id)
        {
            expected.ShouldEqual(_sut.GetShopSection(id));
        }

        public static IEnumerable<object[]> GetShopSection_ShouldReturnExpectedData()
        {
            PopulateExpectedValues();

            return ExpectedValues.Keys.Select(id => new object[] {ExpectedValues[id], id}).ToList();
        }
        
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IShopSectionFactory>();
            
            Assert.NotNull(sut as ShopSectionFactory);
        }
    }
}