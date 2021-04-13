using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories
{
    public class SLightWorldConnectionFactoryTests
    {
        private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
            new AlternativeRequirementDictionary(requirements =>
                new AlternativeRequirement(requirements));
        private static readonly IComplexRequirementDictionary ComplexRequirements =
            Substitute.For<IComplexRequirementDictionary>();
        private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
            Substitute.For<IEntranceShuffleRequirementDictionary>();
        private static readonly IItemRequirementDictionary ItemRequirements =
            Substitute.For<IItemRequirementDictionary>();
        private static readonly ISequenceBreakRequirementDictionary SequenceBreakRequirements =
            Substitute.For<ISequenceBreakRequirementDictionary>();
        private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
            Substitute.For<IWorldStateRequirementDictionary>();
        
        private static readonly IOverworldNodeDictionary OverworldNodes =
            Substitute.For<IOverworldNodeDictionary>();

        private static readonly INodeConnection.Factory ConnectionFactory =
            (fromNode, toNode, requirement) => new NodeConnection(fromNode, toNode, requirement);

        private static readonly Dictionary<OverworldNodeID, ExpectedObject> ExpectedValues = new();

        private readonly SLightWorldConnectionFactory _sut = new(
            AlternativeRequirements, ComplexRequirements, EntranceShuffleRequirements, ItemRequirements,
            SequenceBreakRequirements, WorldStateRequirements, OverworldNodes, ConnectionFactory);

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
            {
                var node = OverworldNodes[id];

                var connections = id switch
                {
                    OverworldNodeID.RaceGameLedge => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                EntranceShuffleRequirements[EntranceShuffle.None],
                                EntranceShuffleRequirements[EntranceShuffle.Dungeon]
                            }]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldSouthMirror], node)
                    },
                    OverworldNodeID.RaceGameLedgeNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.RaceGameLedge], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                    },
                    OverworldNodeID.SouthOfGroveLedge => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldInverted], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldSouthMirror], node)
                    },
                    OverworldNodeID.SouthOfGrove => new List<INodeConnection>()
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.SouthOfGroveLedge], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.SouthOfGroveLedge], node,
                            ComplexRequirements[ComplexRequirementType.SuperBunnyMirror])
                    },
                    OverworldNodeID.GroveDiggingSpot => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                            ItemRequirements[(ItemType.Shovel, 1)])
                    },
                    OverworldNodeID.DesertLedge => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DesertBackNotBunny], node,
                            ItemRequirements[(ItemType.Gloves, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.MireAreaMirror], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DPFrontEntry], node,
                            EntranceShuffleRequirements[EntranceShuffle.None])
                    },
                    OverworldNodeID.DesertLedgeNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DesertLedge], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                    },
                    OverworldNodeID.DesertBack => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DesertLedgeNotBunny], node,
                            ItemRequirements[(ItemType.Gloves, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.MireAreaMirror], node)
                    },
                    OverworldNodeID.DesertBackNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DesertBack], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                    },
                    OverworldNodeID.CheckerboardLedge => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldInverted], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.MireAreaMirror], node)
                    },
                    OverworldNodeID.CheckerboardLedgeNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.CheckerboardLedge], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                    },
                    OverworldNodeID.CheckerboardCave => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.CheckerboardLedgeNotBunny], node,
                            ItemRequirements[(ItemType.Gloves, 1)])
                    },
                    OverworldNodeID.DesertPalaceFrontEntrance => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorld], node,
                            ItemRequirements[(ItemType.Book, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.MireAreaMirror], node)
                    },
                    OverworldNodeID.BombosTabletLedge => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldInverted], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldSouthMirror], node)
                    },
                    OverworldNodeID.BombosTablet => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.BombosTabletLedge], node,
                            ComplexRequirements[ComplexRequirementType.Tablet])
                    },
                    OverworldNodeID.LWMirePortal => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.FluteStandardOpen], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWMirePortalInverted], node,
                            ItemRequirements[(ItemType.Gloves, 2)])
                    },
                    OverworldNodeID.LWMirePortalStandardOpen => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWMirePortal], node,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    OverworldNodeID.LWSouthPortal => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldHammer], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWSouthPortalInverted], node,
                            ItemRequirements[(ItemType.Gloves, 1)])
                    },
                    OverworldNodeID.LWSouthPortalStandardOpen => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWSouthPortal], node,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    OverworldNodeID.LWSouthPortalNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWSouthPortal], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                    },
                    OverworldNodeID.LWLakeHyliaFakeFlippers => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                            SequenceBreakRequirements[SequenceBreakType.FakeFlippersScreenTransition]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion])
                    },
                    OverworldNodeID.LWLakeHyliaFlippers => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                            ItemRequirements[(ItemType.Flippers, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.WaterfallFairyNotBunny], node,
                            ItemRequirements[(ItemType.Flippers, 1)])
                    },
                    OverworldNodeID.LWLakeHyliaWaterWalk => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.WaterWalk]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.WaterfallFairyNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.WaterWalkFromWaterfallCave])
                    },
                    OverworldNodeID.Hobo => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LWLakeHyliaFlippers], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node)
                    },
                    OverworldNodeID.LakeHyliaIsland => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWLakeHyliaFlippers], node,
                            WorldStateRequirements[WorldState.Inverted]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWLakeHyliaFlippers], node,
                            ComplexRequirements[ComplexRequirementType.DWMirror]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node,
                            WorldStateRequirements[WorldState.Inverted]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node,
                            WorldStateRequirements[WorldState.Inverted]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node,
                            ComplexRequirements[ComplexRequirementType.DWMirror])
                    },
                    OverworldNodeID.LakeHyliaFairyIsland => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LWLakeHyliaFlippers], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.IcePalaceIsland], node,
                            ComplexRequirements[ComplexRequirementType.DWMirror]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node)
                    },
                    OverworldNodeID.LakeHyliaFairyIslandStandardOpen => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LakeHyliaFairyIsland], node,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    _ => null
                };

                if (connections is null)
                {
                    continue;
                }

                ExpectedValues.Add(id, connections.ToExpectedObject());
            }
        }

        [Fact]
        public void GetNodeConnections_ShouldThrowException_WhenIDIsUnexpected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _sut.GetNodeConnections((OverworldNodeID)int.MaxValue, Substitute.For<INode>()));
        }

        [Theory]
        [MemberData(nameof(GetNodeConnections_ShouldReturnExpectedValueData))]
        public void GetNodeConnections_ShouldReturnExpectedValue(ExpectedObject expected, OverworldNodeID id)
        {
            expected.ShouldEqual(_sut.GetNodeConnections(id, OverworldNodes[id]));
        }

        public static IEnumerable<object[]> GetNodeConnections_ShouldReturnExpectedValueData()
        {
            PopulateExpectedValues();

            return ExpectedValues.Keys.Select(id => new object[] {ExpectedValues[id], id}).ToList();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<ISLightWorldConnectionFactory>();
            
            Assert.NotNull(sut as SLightWorldConnectionFactory);
        }
    }
}