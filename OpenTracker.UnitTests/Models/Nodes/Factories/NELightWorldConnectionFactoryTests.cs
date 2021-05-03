using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Crystal;
using OpenTracker.Models.Requirements.Item.Exact;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories
{
    public class NELightWorldConnectionFactoryTests
    {
        private static readonly ICrystalRequirement CrystalRequirement =
            Substitute.For<ICrystalRequirement>();
        private static readonly IComplexRequirementDictionary ComplexRequirements =
            Substitute.For<IComplexRequirementDictionary>();
        private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
            Substitute.For<IEntranceShuffleRequirementDictionary>();
        private static readonly IItemRequirementDictionary ItemRequirements =
            Substitute.For<IItemRequirementDictionary>();
        private static readonly IItemExactRequirementDictionary ItemExactRequirements =
            Substitute.For<IItemExactRequirementDictionary>();
        private static readonly IPrizeRequirementDictionary PrizeRequirements =
            Substitute.For<IPrizeRequirementDictionary>();
        private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
            Substitute.For<IWorldStateRequirementDictionary>();
        
        private static readonly IOverworldNodeDictionary OverworldNodes = Substitute.For<IOverworldNodeDictionary>();

        private static readonly INodeConnection.Factory ConnectionFactory =
            (fromNode, toNode, requirement) => new NodeConnection(fromNode, toNode, requirement);

        private static readonly Dictionary<OverworldNodeID, ExpectedObject> ExpectedValues = new();

        private readonly NELightWorldConnectionFactory _sut = new(
            CrystalRequirement, ComplexRequirements, EntranceShuffleRequirements, ItemRequirements,
            ItemExactRequirements, PrizeRequirements, WorldStateRequirements, OverworldNodes, ConnectionFactory);

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
            {
                var node = OverworldNodes[id];

                var connections = id switch
                {
                    OverworldNodeID.HyruleCastleTop => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorld], node,
                            EntranceShuffleRequirements[EntranceShuffle.None]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DarkWorldEast], node,
                            ComplexRequirements[ComplexRequirementType.DWMirror])
                    },
                    OverworldNodeID.HyruleCastleTopInverted => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.HyruleCastleTop], node,
                            WorldStateRequirements[WorldState.Inverted])
                    },
                    OverworldNodeID.HyruleCastleTopStandardOpen => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.HyruleCastleTop], node,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    OverworldNodeID.AgahnimTowerEntrance => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.HyruleCastleTopInverted], node,
                            CrystalRequirement),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.HyruleCastleTopStandardOpen], node,
                            ComplexRequirements[ComplexRequirementType.ATBarrier])
                    },
                    OverworldNodeID.CastleSecretExitArea => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldNotBunny], node)
                    },
                    OverworldNodeID.ZoraArea => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LWLakeHyliaFlippers], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWWitchAreaNotBunny], node,
                            ItemRequirements[(ItemType.Gloves, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.CatfishArea], node,
                            ComplexRequirements[ComplexRequirementType.DWMirror]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node)
                    },
                    OverworldNodeID.ZoraLedge => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.ZoraArea], node,
                            ItemRequirements[(ItemType.Flippers, 1)]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.ZoraArea], node,
                            ComplexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.ZoraArea], node,
                            ComplexRequirements[ComplexRequirementType.WaterWalk])
                    },
                    OverworldNodeID.WaterfallFairy => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LWLakeHyliaFlippers], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node,
                            ItemRequirements[(ItemType.MoonPearl, 1)]),
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node)
                    },
                    OverworldNodeID.WaterfallFairyNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.WaterfallFairy], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                    },
                    OverworldNodeID.LWWitchArea => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldNotBunny], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.ZoraArea], node,
                            ItemRequirements[(ItemType.Gloves, 1)])
                    },
                    OverworldNodeID.LWWitchAreaNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWWitchArea], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW])
                    },
                    OverworldNodeID.WitchsHut => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWWitchArea], node,
                            ItemExactRequirements[(ItemType.Mushroom, 1)])
                    },
                    OverworldNodeID.Sahasrahla => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LightWorld], node,
                            PrizeRequirements[(PrizeType.GreenPendant, 1)])
                    },
                    OverworldNodeID.LWEastPortal => new List<INodeConnection>
                    {
                        ConnectionFactory(OverworldNodes[OverworldNodeID.LightWorldHammer], node),
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.DWEastPortalInverted], node,
                            ItemRequirements[(ItemType.Gloves, 1)])
                    },
                    OverworldNodeID.LWEastPortalStandardOpen => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWEastPortal], node,
                            WorldStateRequirements[WorldState.StandardOpen])
                    },
                    OverworldNodeID.LWEastPortalNotBunny => new List<INodeConnection>
                    {
                        ConnectionFactory(
                            OverworldNodes[OverworldNodeID.LWEastPortal], node,
                            ComplexRequirements[ComplexRequirementType.NotBunnyLW])
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
            var sut = scope.Resolve<INELightWorldConnectionFactory>();
            
            Assert.NotNull(sut as NELightWorldConnectionFactory);
        }
    }
}