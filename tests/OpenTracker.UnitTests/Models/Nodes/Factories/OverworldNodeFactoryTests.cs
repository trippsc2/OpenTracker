using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Factories;
using Xunit;

namespace OpenTracker.UnitTests.Models.Nodes.Factories
{
    public class OverworldNodeFactoryTests
    {
        private static readonly IStartConnectionFactory StartConnectionFactory =
            Substitute.For<IStartConnectionFactory>();
        private static readonly ILightWorldConnectionFactory LightWorldConnectionFactory =
            Substitute.For<ILightWorldConnectionFactory>();
        private static readonly INWLightWorldConnectionFactory NWLightWorldConnectionFactory =
            Substitute.For<INWLightWorldConnectionFactory>();
        private static readonly ISLightWorldConnectionFactory SLightWorldConnectionFactory =
            Substitute.For<ISLightWorldConnectionFactory>();
        private static readonly INELightWorldConnectionFactory NELightWorldConnectionFactory =
            Substitute.For<INELightWorldConnectionFactory>();
        private static readonly ILWDeathMountainConnectionFactory LWDeathMountainConnectionFactory =
            Substitute.For<ILWDeathMountainConnectionFactory>();
        private static readonly INWDarkWorldConnectionFactory NWDarkWorldConnectionFactory =
            Substitute.For<INWDarkWorldConnectionFactory>();
        private static readonly ISDarkWorldConnectionFactory SDarkWorldConnectionFactory =
            Substitute.For<ISDarkWorldConnectionFactory>();
        private static readonly INEDarkWorldConnectionFactory NEDarkWorldConnectionFactory =
            Substitute.For<INEDarkWorldConnectionFactory>();
        private static readonly IDWDeathMountainConnectionFactory DWDeathMountainConnectionFactory =
            Substitute.For<IDWDeathMountainConnectionFactory>();
        private static readonly IDungeonEntryConnectionFactory DungeonEntryConnectionFactory =
            Substitute.For<IDungeonEntryConnectionFactory>();

        private static readonly Dictionary<OverworldNodeID, INodeConnectionFactory> ExpectedFactories = new();
        private static readonly Dictionary<OverworldNodeID, Type> ExpectedTypes = new();

        private readonly OverworldNodeFactory _sut;

        public OverworldNodeFactoryTests()
        {
            _sut = new OverworldNodeFactory(
                StartConnectionFactory, LightWorldConnectionFactory,
                NWLightWorldConnectionFactory,
                SLightWorldConnectionFactory,
                NELightWorldConnectionFactory, LWDeathMountainConnectionFactory,
                NWDarkWorldConnectionFactory,
                SDarkWorldConnectionFactory,
                NEDarkWorldConnectionFactory, DWDeathMountainConnectionFactory,
                DungeonEntryConnectionFactory, () => new OverworldNode(
                    Substitute.For<IMode>(), new OverworldNodeDictionary(() => _sut!),
                    _sut!), () => new StartNode());
        }

        private static void PopulateExpectedValues()
        {
            ExpectedFactories.Clear();
            ExpectedTypes.Clear();
            
            foreach (OverworldNodeID id in Enum.GetValues(typeof(OverworldNodeID)))
            {
                switch (id)
                {
                    case OverworldNodeID.Start:
                        ExpectedTypes.Add(id, typeof(StartNode));
                        break;
                    case OverworldNodeID.Inaccessible:
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                    case OverworldNodeID.EntranceDungeonAllInsanity:
                    case OverworldNodeID.EntranceNone:
                    case OverworldNodeID.EntranceNoneInverted:
                    case OverworldNodeID.Flute:
                    case OverworldNodeID.FluteActivated:
                    case OverworldNodeID.FluteInverted:
                    case OverworldNodeID.FluteStandardOpen:
                        ExpectedFactories.Add(id, StartConnectionFactory);
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                    case OverworldNodeID.LightWorld:
                    case OverworldNodeID.LightWorldInverted:
                    case OverworldNodeID.LightWorldInvertedNotBunny:
                    case OverworldNodeID.LightWorldStandardOpen:
                    case OverworldNodeID.LightWorldMirror:
                    case OverworldNodeID.LightWorldNotBunny:
                    case OverworldNodeID.LightWorldNotBunnyOrDungeonRevive:
                    case OverworldNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole:
                    case OverworldNodeID.LightWorldNotBunnyOrSuperBunnyMirror:
                    case OverworldNodeID.LightWorldDash:
                    case OverworldNodeID.LightWorldHammer:
                    case OverworldNodeID.LightWorldLift1:
                    case OverworldNodeID.LightWorldFlute:
                        ExpectedFactories.Add(id, LightWorldConnectionFactory);
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                    case OverworldNodeID.Pedestal:
                    case OverworldNodeID.LumberjackCaveHole:
                    case OverworldNodeID.DeathMountainEntry:
                    case OverworldNodeID.DeathMountainEntryNonEntrance:
                    case OverworldNodeID.DeathMountainEntryCave:
                    case OverworldNodeID.DeathMountainEntryCaveDark:
                    case OverworldNodeID.DeathMountainExit:
                    case OverworldNodeID.DeathMountainExitNonEntrance:
                    case OverworldNodeID.DeathMountainExitCave:
                    case OverworldNodeID.DeathMountainExitCaveDark:
                    case OverworldNodeID.LWKakarikoPortal:
                    case OverworldNodeID.LWKakarikoPortalStandardOpen:
                    case OverworldNodeID.LWKakarikoPortalNotBunny:
                    case OverworldNodeID.SickKid:
                    case OverworldNodeID.GrassHouse:
                    case OverworldNodeID.BombHut:
                    case OverworldNodeID.MagicBatLedge:
                    case OverworldNodeID.MagicBat:
                    case OverworldNodeID.LWGraveyard:
                    case OverworldNodeID.LWGraveyardNotBunny:
                    case OverworldNodeID.LWGraveyardLedge:
                    case OverworldNodeID.EscapeGrave:
                    case OverworldNodeID.KingsTomb:
                    case OverworldNodeID.KingsTombNotBunny:
                    case OverworldNodeID.KingsTombGrave:
                        ExpectedFactories.Add(id, NWLightWorldConnectionFactory);
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                    case OverworldNodeID.RaceGameLedge:
                    case OverworldNodeID.RaceGameLedgeNotBunny:
                    case OverworldNodeID.SouthOfGroveLedge:
                    case OverworldNodeID.SouthOfGrove:
                    case OverworldNodeID.GroveDiggingSpot:
                    case OverworldNodeID.DesertLedge:
                    case OverworldNodeID.DesertLedgeNotBunny:
                    case OverworldNodeID.DesertBack:
                    case OverworldNodeID.DesertBackNotBunny:
                    case OverworldNodeID.CheckerboardLedge:
                    case OverworldNodeID.CheckerboardLedgeNotBunny:
                    case OverworldNodeID.CheckerboardCave:
                    case OverworldNodeID.DesertPalaceFrontEntrance:
                    case OverworldNodeID.BombosTabletLedge:
                    case OverworldNodeID.BombosTablet:
                    case OverworldNodeID.LWMirePortal:
                    case OverworldNodeID.LWMirePortalStandardOpen:
                    case OverworldNodeID.LWSouthPortal:
                    case OverworldNodeID.LWSouthPortalStandardOpen:
                    case OverworldNodeID.LWSouthPortalNotBunny:
                    case OverworldNodeID.LWLakeHyliaFakeFlippers:
                    case OverworldNodeID.LWLakeHyliaFlippers:
                    case OverworldNodeID.LWLakeHyliaWaterWalk:
                    case OverworldNodeID.Hobo:
                    case OverworldNodeID.LakeHyliaIsland:
                    case OverworldNodeID.LakeHyliaFairyIsland:
                    case OverworldNodeID.LakeHyliaFairyIslandStandardOpen:
                        ExpectedFactories.Add(id, SLightWorldConnectionFactory);
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                    case OverworldNodeID.HyruleCastleTop:
                    case OverworldNodeID.HyruleCastleTopInverted:
                    case OverworldNodeID.HyruleCastleTopStandardOpen:
                    case OverworldNodeID.AgahnimTowerEntrance:
                    case OverworldNodeID.CastleSecretExitArea:
                    case OverworldNodeID.ZoraArea:
                    case OverworldNodeID.ZoraLedge:
                    case OverworldNodeID.WaterfallFairy:
                    case OverworldNodeID.WaterfallFairyNotBunny:
                    case OverworldNodeID.LWWitchArea:
                    case OverworldNodeID.LWWitchAreaNotBunny:
                    case OverworldNodeID.WitchsHut:
                    case OverworldNodeID.Sahasrahla:
                    case OverworldNodeID.LWEastPortal:
                    case OverworldNodeID.LWEastPortalStandardOpen:
                    case OverworldNodeID.LWEastPortalNotBunny:
                        ExpectedFactories.Add(id, NELightWorldConnectionFactory);
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                    case OverworldNodeID.DeathMountainWestBottom:
                    case OverworldNodeID.DeathMountainWestBottomNonEntrance:
                    case OverworldNodeID.DeathMountainWestBottomNotBunny:
                    case OverworldNodeID.SpectacleRockTop:
                    case OverworldNodeID.DeathMountainWestTop:
                    case OverworldNodeID.DeathMountainWestTopNotBunny:
                    case OverworldNodeID.EtherTablet:
                    case OverworldNodeID.DeathMountainEastBottom:
                    case OverworldNodeID.DeathMountainEastBottomNotBunny:
                    case OverworldNodeID.DeathMountainEastBottomLift2:
                    case OverworldNodeID.DeathMountainEastBottomConnector:
                    case OverworldNodeID.ParadoxCave:
                    case OverworldNodeID.ParadoxCaveSuperBunnyFallInHole:
                    case OverworldNodeID.ParadoxCaveNotBunny:
                    case OverworldNodeID.ParadoxCaveTop:
                    case OverworldNodeID.DeathMountainEastTop:
                    case OverworldNodeID.DeathMountainEastTopInverted:
                    case OverworldNodeID.DeathMountainEastTopNotBunny:
                    case OverworldNodeID.DeathMountainEastTopConnector:
                    case OverworldNodeID.SpiralCaveLedge:
                    case OverworldNodeID.SpiralCave:
                    case OverworldNodeID.MimicCaveLedge:
                    case OverworldNodeID.MimicCaveLedgeNotBunny:
                    case OverworldNodeID.MimicCave:
                    case OverworldNodeID.LWFloatingIsland:
                    case OverworldNodeID.LWTurtleRockTop:
                    case OverworldNodeID.LWTurtleRockTopInverted:
                    case OverworldNodeID.LWTurtleRockTopInvertedNotBunny:
                    case OverworldNodeID.LWTurtleRockTopStandardOpen:
                        ExpectedFactories.Add(id, LWDeathMountainConnectionFactory);
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                    case OverworldNodeID.DWKakarikoPortal:
                    case OverworldNodeID.DWKakarikoPortalInverted:
                    case OverworldNodeID.DarkWorldWest:
                    case OverworldNodeID.DarkWorldWestNotBunny:
                    case OverworldNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror:
                    case OverworldNodeID.DarkWorldWestLift2:
                    case OverworldNodeID.SkullWoodsBackArea:
                    case OverworldNodeID.SkullWoodsBackAreaNotBunny:
                    case OverworldNodeID.SkullWoodsBack:
                    case OverworldNodeID.BumperCaveEntry:
                    case OverworldNodeID.BumperCaveEntryNonEntrance:
                    case OverworldNodeID.BumperCaveFront:
                    case OverworldNodeID.BumperCaveNotBunny:
                    case OverworldNodeID.BumperCavePastGap:
                    case OverworldNodeID.BumperCaveBack:
                    case OverworldNodeID.BumperCaveTop:
                    case OverworldNodeID.HammerHouse:
                    case OverworldNodeID.HammerHouseNotBunny:
                    case OverworldNodeID.HammerPegsArea:
                    case OverworldNodeID.HammerPegs:
                    case OverworldNodeID.PurpleChest:
                    case OverworldNodeID.BlacksmithPrison:
                    case OverworldNodeID.Blacksmith:
                    case OverworldNodeID.DWGraveyard:
                    case OverworldNodeID.DWGraveyardMirror:
                        ExpectedFactories.Add(id, NWDarkWorldConnectionFactory);
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                    case OverworldNodeID.DarkWorldSouth:
                    case OverworldNodeID.DarkWorldSouthInverted:
                    case OverworldNodeID.DarkWorldSouthStandardOpen:
                    case OverworldNodeID.DarkWorldSouthStandardOpenNotBunny:
                    case OverworldNodeID.DarkWorldSouthMirror:
                    case OverworldNodeID.DarkWorldSouthNotBunny:
                    case OverworldNodeID.DarkWorldSouthDash:
                    case OverworldNodeID.DarkWorldSouthHammer:
                    case OverworldNodeID.BuyBigBomb:
                    case OverworldNodeID.BuyBigBombStandardOpen:
                    case OverworldNodeID.BigBombToLightWorld:
                    case OverworldNodeID.BigBombToLightWorldStandardOpen:
                    case OverworldNodeID.BigBombToDWLakeHylia:
                    case OverworldNodeID.BigBombToWall:
                    case OverworldNodeID.DWSouthPortal:
                    case OverworldNodeID.DWSouthPortalInverted:
                    case OverworldNodeID.DWSouthPortalNotBunny:
                    case OverworldNodeID.MireArea:
                    case OverworldNodeID.MireAreaMirror:
                    case OverworldNodeID.MireAreaNotBunny:
                    case OverworldNodeID.MireAreaNotBunnyOrSuperBunnyMirror:
                    case OverworldNodeID.MiseryMireEntrance:
                    case OverworldNodeID.DWMirePortal:
                    case OverworldNodeID.DWMirePortalInverted:
                    case OverworldNodeID.DWLakeHyliaFlippers:
                    case OverworldNodeID.DWLakeHyliaFakeFlippers:
                    case OverworldNodeID.DWLakeHyliaWaterWalk:
                    case OverworldNodeID.IcePalaceIsland:
                    case OverworldNodeID.IcePalaceIslandInverted:
                    case OverworldNodeID.DarkWorldSouthEast:
                    case OverworldNodeID.DarkWorldSouthEastNotBunny:
                    case OverworldNodeID.DarkWorldSouthEastLift1:
                        ExpectedFactories.Add(id, SDarkWorldConnectionFactory);
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                    case OverworldNodeID.DWWitchArea:
                    case OverworldNodeID.DWWitchAreaNotBunny:
                    case OverworldNodeID.CatfishArea:
                    case OverworldNodeID.DarkWorldEast:
                    case OverworldNodeID.DarkWorldEastStandardOpen:
                    case OverworldNodeID.DarkWorldEastNotBunny:
                    case OverworldNodeID.DarkWorldEastHammer:
                    case OverworldNodeID.FatFairyEntrance:
                    case OverworldNodeID.DWEastPortal:
                    case OverworldNodeID.DWEastPortalInverted:
                    case OverworldNodeID.DWEastPortalNotBunny:
                        ExpectedFactories.Add(id, NEDarkWorldConnectionFactory);
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                    case OverworldNodeID.DarkDeathMountainWestBottom:
                    case OverworldNodeID.DarkDeathMountainWestBottomInverted:
                    case OverworldNodeID.DarkDeathMountainWestBottomNonEntrance:
                    case OverworldNodeID.DarkDeathMountainWestBottomMirror:
                    case OverworldNodeID.DarkDeathMountainWestBottomNotBunny:
                    case OverworldNodeID.SpikeCavePastHammerBlocks:
                    case OverworldNodeID.SpikeCavePastSpikes:
                    case OverworldNodeID.SpikeCaveChest:
                    case OverworldNodeID.DarkDeathMountainTop:
                    case OverworldNodeID.DarkDeathMountainTopInverted:
                    case OverworldNodeID.DarkDeathMountainTopStandardOpen:
                    case OverworldNodeID.DarkDeathMountainTopMirror:
                    case OverworldNodeID.DarkDeathMountainTopNotBunny:
                    case OverworldNodeID.SuperBunnyCave:
                    case OverworldNodeID.SuperBunnyCaveChests:
                    case OverworldNodeID.GanonsTowerEntrance:
                    case OverworldNodeID.GanonsTowerEntranceStandardOpen:
                    case OverworldNodeID.DWFloatingIsland:
                    case OverworldNodeID.HookshotCaveEntrance:
                    case OverworldNodeID.HookshotCaveEntranceHookshot:
                    case OverworldNodeID.HookshotCaveEntranceHover:
                    case OverworldNodeID.HookshotCaveBonkableChest:
                    case OverworldNodeID.HookshotCaveBack:
                    case OverworldNodeID.DWTurtleRockTop:
                    case OverworldNodeID.DWTurtleRockTopInverted:
                    case OverworldNodeID.DWTurtleRockTopNotBunny:
                    case OverworldNodeID.TurtleRockFrontEntrance:
                    case OverworldNodeID.DarkDeathMountainEastBottom:
                    case OverworldNodeID.DarkDeathMountainEastBottomInverted:
                    case OverworldNodeID.DarkDeathMountainEastBottomConnector:
                    case OverworldNodeID.TurtleRockTunnel:
                    case OverworldNodeID.TurtleRockTunnelMirror:
                    case OverworldNodeID.TurtleRockSafetyDoor:
                        ExpectedFactories.Add(id, DWDeathMountainConnectionFactory);
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                    case OverworldNodeID.GanonHole:
                    case OverworldNodeID.HCSanctuaryEntry:
                    case OverworldNodeID.HCFrontEntry:
                    case OverworldNodeID.HCBackEntry:
                    case OverworldNodeID.ATEntry:
                    case OverworldNodeID.EPEntry:
                    case OverworldNodeID.DPFrontEntry:
                    case OverworldNodeID.DPLeftEntry:
                    case OverworldNodeID.DPBackEntry:
                    case OverworldNodeID.ToHEntry:
                    case OverworldNodeID.PoDEntry:
                    case OverworldNodeID.SPEntry:
                    case OverworldNodeID.SWFrontEntry:
                    case OverworldNodeID.SWBackEntry:
                    case OverworldNodeID.TTEntry:
                    case OverworldNodeID.IPEntry:
                    case OverworldNodeID.MMEntry:
                    case OverworldNodeID.TRFrontEntry:
                    case OverworldNodeID.TRFrontEntryStandardOpen:
                    case OverworldNodeID.TRFrontEntryStandardOpenEntranceNone:
                    case OverworldNodeID.TRFrontToKeyDoors:
                    case OverworldNodeID.TRKeyDoorsToMiddleExit:
                    case OverworldNodeID.TRMiddleEntry:
                    case OverworldNodeID.TRBackEntry:
                    case OverworldNodeID.GTEntry:
                        ExpectedFactories.Add(id, DungeonEntryConnectionFactory);
                        ExpectedTypes.Add(id, typeof(OverworldNode));
                        break;
                }
            }
        }
        
        [Theory]
        [MemberData(nameof(GetOverworldNode_ShouldReturnExpectedTypeData))]
        public void GetOverworldNode_ShouldReturnExpectedType(Type expected, OverworldNodeID id)
        {
            var node = _sut.GetOverworldNode(id);
            
            Assert.Equal(expected, node.GetType());
        }

        public static IEnumerable<object[]> GetOverworldNode_ShouldReturnExpectedTypeData()
        {
            PopulateExpectedValues();

            return ExpectedTypes.Keys.Select(id => new object[] {ExpectedTypes[id], id}).ToList();
        }

        [Fact]
        public void GetNodeConnections_ShouldThrowException_WhenIDIsUnexpected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _ = _sut.GetNodeConnections((OverworldNodeID) int.MaxValue, Substitute.For<INode>()));
        }

        [Fact]
        public void GetNodeConnections_ShouldReturnEmptyList_WhenIDIsInaccessible()
        {
            const OverworldNodeID id = OverworldNodeID.Inaccessible;
            var connections = _sut.GetNodeConnections(id, Substitute.For<INode>());
            
            Assert.Empty(connections);
        }

        [Theory]
        [MemberData(nameof(GetNodeConnections_ShouldCallExpectedFactoryMethodData))]
        public void GetNodeConnections_ShouldCallExpectedFactoryMethod(
            INodeConnectionFactory expected, OverworldNodeID id)
        {
            _ = _sut.GetNodeConnections(id, Substitute.For<INode>());

            expected.Received().GetNodeConnections(id, Arg.Any<INode>());
        }
        
        public static IEnumerable<object[]> GetNodeConnections_ShouldCallExpectedFactoryMethodData()
        {
            PopulateExpectedValues();

            return ExpectedFactories.Keys.Select(id => new object[] {ExpectedFactories[id], id}).ToList();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IOverworldNodeFactory.Factory>();
            var sut = factory();
            
            Assert.NotNull(sut as OverworldNodeFactory);
        }
    }
}