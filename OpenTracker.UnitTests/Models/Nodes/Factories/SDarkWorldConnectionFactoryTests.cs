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
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories
{
    public class SDarkWorldConnectionFactoryTests
    {
        private static readonly IComplexRequirementDictionary ComplexRequirements =
            Substitute.For<IComplexRequirementDictionary>();
        private static readonly IItemRequirementDictionary ItemRequirements =
            Substitute.For<IItemRequirementDictionary>();
        private static readonly IPrizeRequirementDictionary PrizeRequirements =
            Substitute.For<IPrizeRequirementDictionary>();
        private static readonly ISequenceBreakRequirementDictionary SequenceBreakRequirements =
            Substitute.For<ISequenceBreakRequirementDictionary>();
        private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
            Substitute.For<IWorldStateRequirementDictionary>();
        
        private static readonly IOverworldNodeDictionary OverworldNodes =
            Substitute.For<IOverworldNodeDictionary>();

        private static readonly INodeConnection.Factory ConnectionFactory = 
            (fromNode, toNode, requirement) => new NodeConnection(fromNode, toNode, requirement);

        private static readonly Dictionary<OverworldNodeID, ExpectedObject> ExpectedValues = new();

        private readonly SDarkWorldConnectionFactory _sut = new(
            ComplexRequirements, ItemRequirements, PrizeRequirements, SequenceBreakRequirements,
            WorldStateRequirements, OverworldNodes, ConnectionFactory);

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
            {
                var node = OverworldNodes[id];

                var connections = id switch
                {
                    OverworldNodeID.DarkWorldSouth => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.EntranceNoneInverted], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.FluteInverted], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldMirror], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWSouthPortalNotBunny], node,
                            ItemRequirements[(ItemType.Hammer, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldWest], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldEastHammer], node)
                    },
                    OverworldNodeID.DarkWorldSouthInverted => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouth], node,
                            WorldStateRequirements[WorldState.Inverted])
                    },
                    OverworldNodeID.DarkWorldSouthStandardOpen => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouth], node,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    OverworldNodeID.DarkWorldSouthStandardOpenNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthStandardOpen], node,
                            ItemRequirements[(ItemType.MoonPearl, 1)])
                    },
                    OverworldNodeID.DarkWorldSouthMirror => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouth], node,
                            ComplexRequirements[ComplexRequirementType.DWMirror])
                    },
                    OverworldNodeID.DarkWorldSouthNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouth], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                    },
                    OverworldNodeID.DarkWorldSouthDash => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                            ItemRequirements[(ItemType.Boots, 1)])
                    },
                    OverworldNodeID.DarkWorldSouthHammer => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                            ItemRequirements[(ItemType.Hammer, 1)])
                    },
                    OverworldNodeID.BuyBigBomb => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorldInvertedNotBunny], node,
                            PrizeRequirements[(PrizeType.RedCrystal, 2)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthStandardOpenNotBunny], node,
                            PrizeRequirements[(PrizeType.RedCrystal, 2)])
                    },
                    OverworldNodeID.BuyBigBombStandardOpen => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.BuyBigBomb], node,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    OverworldNodeID.BigBombToLightWorld => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.BuyBigBomb], node,
                            ComplexRequirements[ComplexRequirementType.DWMirror]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.BuyBigBomb], node,
                            WorldStateRequirements[WorldState.Inverted])
                    },
                    OverworldNodeID.BigBombToLightWorldStandardOpen => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.BigBombToLightWorld], node,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    OverworldNodeID.BigBombToDWLakeHylia => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.BuyBigBombStandardOpen], node,
                            ComplexRequirements[ComplexRequirementType.BombDuplicationMirror]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.BuyBigBombStandardOpen], node,
                            ComplexRequirements[ComplexRequirementType.BombDuplicationAncillaOverload])
                    },
                    OverworldNodeID.BigBombToWall => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.BuyBigBombStandardOpen], node,
                            ItemRequirements[(ItemType.Hammer, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.BigBombToLightWorld], node,
                            ComplexRequirements[ComplexRequirementType.LWMirror]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.BigBombToLightWorldStandardOpen], node,
                            PrizeRequirements[(PrizeType.Aga1, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.BigBombToDWLakeHylia], node)
                    },
                    OverworldNodeID.DWSouthPortal => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWSouthPortalStandardOpen], node,
                            ItemRequirements[(ItemType.Gloves, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DarkWorldSouthHammer], node)
                    },
                    OverworldNodeID.DWSouthPortalInverted => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWSouthPortal], node,
                            WorldStateRequirements[WorldState.Inverted])
                    },
                    OverworldNodeID.DWSouthPortalNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWSouthPortal], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                    },
                    OverworldNodeID.MireArea => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldMirror], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DWMirePortal], node)
                    },
                    OverworldNodeID.MireAreaMirror => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.MireArea], node,
                            ComplexRequirements[ComplexRequirementType.DWMirror])
                    },
                    OverworldNodeID.MireAreaNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.MireArea], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                    },
                    OverworldNodeID.MireAreaNotBunnyOrSuperBunnyMirror => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.MireAreaNotBunny], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.MireArea], node,
                            ComplexRequirements[ComplexRequirementType.SuperBunnyMirror])
                    },
                    OverworldNodeID.MiseryMireEntrance => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.MireAreaNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.MMMedallion])
                    },
                    OverworldNodeID.DWMirePortal => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.FluteInverted], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWMirePortalStandardOpen], node,
                            ItemRequirements[(ItemType.Gloves, 2)])
                    },
                    OverworldNodeID.DWMirePortalInverted => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWMirePortal], node,
                            WorldStateRequirements[WorldState.Inverted])
                    },
                    OverworldNodeID.DWLakeHyliaFlippers => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            ItemRequirements[(ItemType.Flippers, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                            ItemRequirements[(ItemType.Flippers, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            ItemRequirements[(ItemType.Flippers, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            ItemRequirements[(ItemType.Flippers, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                            ItemRequirements[(ItemType.Flippers, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.IcePalaceIslandInverted], node,
                            ItemRequirements[(ItemType.Flippers, 1)])
                    },
                    OverworldNodeID.DWLakeHyliaFakeFlippers => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            SequenceBreakRequirements[SequenceBreakType.FakeFlippersQirnJump]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                            SequenceBreakRequirements[SequenceBreakType.FakeFlippersFairyRevival]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.IcePalaceIslandInverted], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.IcePalaceIslandInverted], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                    },
                    OverworldNodeID.DWLakeHyliaWaterWalk => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            ComplexRequirements[ComplexRequirementType.WaterWalk])
                    },
                    OverworldNodeID.IcePalaceIsland => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LakeHyliaFairyIsland], node,
                            ComplexRequirements[ComplexRequirementType.LWMirror]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LakeHyliaFairyIslandStandardOpen], node,
                            ItemRequirements[(ItemType.Gloves, 2)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWLakeHyliaFlippers], node,
                            WorldStateRequirements[WorldState.Inverted]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node,
                            WorldStateRequirements[WorldState.Inverted]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node,
                            WorldStateRequirements[WorldState.Inverted])
                    },
                    OverworldNodeID.IcePalaceIslandInverted => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.IcePalaceIsland], node,
                            WorldStateRequirements[WorldState.Inverted])
                    },
                    OverworldNodeID.DarkWorldSouthEast => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.FluteInverted], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldMirror], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DWLakeHyliaFlippers], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node)
                    },
                    OverworldNodeID.DarkWorldSouthEastNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthEast], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyDW])
                    },
                    OverworldNodeID.DarkWorldSouthEastLift1 => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                            ItemRequirements[(ItemType.Gloves, 1)])
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
            var sut = scope.Resolve<ISDarkWorldConnectionFactory>();
            
            Assert.NotNull(sut as SDarkWorldConnectionFactory);
        }
    }
}