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
using OpenTracker.Models.Requirements.TakeAnyLocations;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections.Boolean;
using OpenTracker.Models.Sections.Factories;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Factories
{
    public class TakeAnySectionFactoryTests
    {
        private static readonly IAggregateRequirementDictionary AggregateRequirements =
            new AggregateRequirementDictionary(requirements =>
                new AggregateRequirement(requirements));
        private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
            new AlternativeRequirementDictionary(requirements =>
                new AlternativeRequirement(requirements));
        private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
            new EntranceShuffleRequirementDictionary(_ => Substitute.For<IEntranceShuffleRequirement>());
        private static readonly ITakeAnyLocationsRequirementDictionary TakeAnyLocationsRequirements =
            new TakeAnyLocationsRequirementDictionary(_ => Substitute.For<ITakeAnyLocationsRequirement>());

        private static readonly IOverworldNodeDictionary OverworldNodes =
            new OverworldNodeDictionary(() => Substitute.For<IOverworldNodeFactory>());

        private static readonly ISaveLoadManager SaveLoadManager = Substitute.For<ISaveLoadManager>();

        private static readonly ICollectSection.Factory CollectSectionFactory =
            (section, force) => new CollectSection(section, force);
        private static readonly IUncollectSection.Factory UncollectSectionFactory =
            section => new UncollectSection(section);

        private static readonly ITakeAnySection.Factory Factory = (node, requirement) => new TakeAnySection(
            SaveLoadManager, CollectSectionFactory, UncollectSectionFactory, node, requirement);

        private static readonly Dictionary<LocationID, ExpectedObject> ExpectedValues = new();

        private readonly TakeAnySectionFactory _sut = new TakeAnySectionFactory(
            AggregateRequirements, AlternativeRequirements, EntranceShuffleRequirements,
            TakeAnyLocationsRequirements, OverworldNodes, Factory);

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
            {
                switch (id)
                {
                    case LocationID.TreesFairyCaveTakeAny:
                    case LocationID.PegsFairyCaveTakeAny:
                    case LocationID.KakarikoFortuneTellerTakeAny:
                    case LocationID.ForestChestGameTakeAny:
                    case LocationID.LumberjackHouseTakeAny:
                    case LocationID.LeftSnitchHouseTakeAny:
                    case LocationID.RightSnitchHouseTakeAny:
                    case LocationID.ThiefCaveTakeAny:
                    case LocationID.IceBeeCaveTakeAny:
                    case LocationID.FortuneTellerTakeAny:
                    case LocationID.ChestGameTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.LightWorld],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.GrassHouseTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.GrassHouse],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.BombHutTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.BombHut],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.IceFairyCaveTakeAny:
                    case LocationID.RupeeCaveTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.LightWorldLift1],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.CentralBonkRocksTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.LightWorldDash],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.HypeFairyCaveTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.EDMFairyCaveTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DeathMountainEastBottom],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.DarkChapelTakeAny:
                    case LocationID.DarkVillageFortuneTellerTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DarkWorldWest],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.DarkTreesFairyCaveTakeAny:
                    case LocationID.DarkSahasrahlaTakeAny:
                    case LocationID.DarkFluteSpotFiveTakeAny:
                    case LocationID.ArrowGameTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DarkWorldEast],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.DarkCentralBonkRocksTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthDash],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.DarkIceRodCaveTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.DarkFakeIceRodCaveTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthEast],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.DarkIceRodRockTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthEastLift1],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.DarkMountainFairyTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.DarkDeathMountainWestBottom],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                    case LocationID.MireRightShackTakeAny:
                    case LocationID.MireCaveTakeAny:
                        ExpectedValues.Add(id, Factory(
                            OverworldNodes[OverworldNodeID.MireArea],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                AlternativeRequirements[new HashSet<IRequirement>
                                {
                                    EntranceShuffleRequirements[EntranceShuffle.None],
                                    EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                                }],
                                TakeAnyLocationsRequirements[true]
                            }]).ToExpectedObject()); 
                        break;
                }
            }
        }

        [Fact]
        public void GetTakeAnySection_ShouldThrowException_WhenIDIsUnexpected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _ = _sut.GetTakeAnySection((LocationID) int.MaxValue));
        }

        [Theory]
        [MemberData(nameof(GetTakeAnySection_ShouldReturnExpectedData))]
        public void GetTakeAnySection_ShouldReturnExpected(ExpectedObject expected, LocationID id)
        {
            expected.ShouldEqual(_sut.GetTakeAnySection(id));
        }

        public static IEnumerable<object[]> GetTakeAnySection_ShouldReturnExpectedData()
        {
            PopulateExpectedValues();

            return ExpectedValues.Keys.Select(id => new object[] {ExpectedValues[id], id}).ToList();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<ITakeAnySectionFactory>();
            
            Assert.NotNull(sut as TakeAnySectionFactory);
        }
    }
}