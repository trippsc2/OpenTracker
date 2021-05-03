using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Items
{
    public class DungeonItemFactoryTests
    {
        private readonly IDungeonItem.Factory _factory = (_, _) => Substitute.For<IDungeonItem>();
        private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();

        private readonly DungeonItemFactory _sut;

        private static readonly Dictionary<DungeonItemID, DungeonNodeID> ExpectedValues = new();

        public DungeonItemFactoryTests()
        {
            _sut = new DungeonItemFactory(_factory);
        }

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();

            foreach (DungeonItemID id in Enum.GetValues(typeof(DungeonItemID)))
            {
                DungeonNodeID nodeID;

                switch (id)
                {
                    case DungeonItemID.HCSanctuary:
                        nodeID = DungeonNodeID.HCSanctuary;
                        break;
                    case DungeonItemID.HCMapChest:
                    case DungeonItemID.HCMapGuardDrop:
                        nodeID = DungeonNodeID.HCFront;
                        break;
                    case DungeonItemID.HCBoomerangChest:
                    case DungeonItemID.HCBoomerangGuardDrop:
                        nodeID = DungeonNodeID.HCPastEscapeFirstKeyDoor;
                        break;
                    case DungeonItemID.HCZeldasCell:
                        nodeID = DungeonNodeID.HCZeldasCell;
                        break;
                    case DungeonItemID.HCDarkCross:
                        nodeID = DungeonNodeID.HCDarkRoomFront;
                        break;
                    case DungeonItemID.HCSecretRoomLeft:
                    case DungeonItemID.HCSecretRoomMiddle:
                    case DungeonItemID.HCSecretRoomRight:
                        nodeID = DungeonNodeID.HCBack;
                        break;
                    case DungeonItemID.HCKeyRatDrop:
                        nodeID = DungeonNodeID.HCPastDarkCrossKeyDoor;
                        break;
                    case DungeonItemID.HCBigKeyDrop:
                        nodeID = DungeonNodeID.HCPastEscapeSecondKeyDoor;
                        break;
                    case DungeonItemID.ATRoom03:
                        nodeID = DungeonNodeID.AT;
                        break;
                    case DungeonItemID.ATDarkMaze:
                        nodeID = DungeonNodeID.ATPastFirstKeyDoor;
                        break;
                    case DungeonItemID.ATBoss:
                        nodeID = DungeonNodeID.ATBoss;
                        break;
                    case DungeonItemID.ATDarkArcherDrop:
                        nodeID = DungeonNodeID.ATPastSecondKeyDoor;
                        break;
                    case DungeonItemID.ATCircleOfPotsDrop:
                        nodeID = DungeonNodeID.ATPastThirdKeyDoor;
                        break;
                    case DungeonItemID.EPCannonballChest:
                    case DungeonItemID.EPMapChest:
                    case DungeonItemID.EPCompassChest:
                        nodeID = DungeonNodeID.EP;
                        break;
                    case DungeonItemID.EPBigChest:
                        nodeID = DungeonNodeID.EPBigChest;
                        break;
                    case DungeonItemID.EPBigKeyChest:
                        nodeID = DungeonNodeID.EPPastRightKeyDoor;
                        break;
                    case DungeonItemID.EPBoss:
                        nodeID = DungeonNodeID.EPBoss;
                        break;
                    case DungeonItemID.EPDarkSquarePot:
                        nodeID = DungeonNodeID.EPRightDarkRoom;
                        break;
                    case DungeonItemID.EPDarkEyegoreDrop:
                        nodeID = DungeonNodeID.EPBackDarkRoom;
                        break;
                    case DungeonItemID.DPMapChest:
                        nodeID = DungeonNodeID.DPFront;
                        break;
                    case DungeonItemID.DPTorch:
                        nodeID = DungeonNodeID.DPTorch;
                        break;
                    case DungeonItemID.DPBigChest:
                        nodeID = DungeonNodeID.DPBigChest;
                        break;
                    case DungeonItemID.DPCompassChest:
                    case DungeonItemID.DPBigKeyChest:
                        nodeID = DungeonNodeID.DPPastRightKeyDoor;
                        break;
                    case DungeonItemID.DPBoss:
                        nodeID = DungeonNodeID.DPBoss;
                        break;
                    case DungeonItemID.DPTiles1Pot:
                        nodeID = DungeonNodeID.DPBack;
                        break;
                    case DungeonItemID.DPBeamosHallPot:
                        nodeID = DungeonNodeID.DP2F;
                        break;
                    case DungeonItemID.DPTiles2Pot:
                        nodeID = DungeonNodeID.DP2FPastFirstKeyDoor;
                        break;
                    case DungeonItemID.ToHBasementCage:
                    case DungeonItemID.ToHMapChest:
                        nodeID = DungeonNodeID.ToH;
                        break;
                    case DungeonItemID.ToHBigKeyChest:
                        nodeID = DungeonNodeID.ToHBasementTorchRoom;
                        break;
                    case DungeonItemID.ToHCompassChest:
                        nodeID = DungeonNodeID.ToHPastBigKeyDoor;
                        break;
                    case DungeonItemID.ToHBigChest:
                        nodeID = DungeonNodeID.ToHBigChest;
                        break;
                    case DungeonItemID.ToHBoss:
                        nodeID = DungeonNodeID.ToHBoss;
                        break;
                    case DungeonItemID.PoDShooterRoom:
                        nodeID = DungeonNodeID.PoD;
                        break;
                    case DungeonItemID.PoDMapChest:
                    case DungeonItemID.PoDArenaLedge:
                        nodeID = DungeonNodeID.PoDPastFirstRedGoriyaRoom;
                        break;
                    case DungeonItemID.PoDBigKeyChest:
                        nodeID = DungeonNodeID.PoDBigKeyChestArea;
                        break;
                    case DungeonItemID.PoDStalfosBasement:
                    case DungeonItemID.PoDArenaBridge:
                        nodeID = DungeonNodeID.PoDLobbyArena;
                        break;
                    case DungeonItemID.PoDCompassChest:
                        nodeID = DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor;
                        break;
                    case DungeonItemID.PoDDarkBasementLeft:
                    case DungeonItemID.PoDDarkBasementRight:
                        nodeID = DungeonNodeID.PoDDarkBasement;
                        break;
                    case DungeonItemID.PoDHarmlessHellway:
                        nodeID = DungeonNodeID.PoDHarmlessHellwayRoom;
                        break;
                    case DungeonItemID.PoDDarkMazeTop:
                    case DungeonItemID.PoDDarkMazeBottom:
                        nodeID = DungeonNodeID.PoDDarkMaze;
                        break;
                    case DungeonItemID.PoDBigChest:
                        nodeID = DungeonNodeID.PoDBigChest;
                        break;
                    case DungeonItemID.PoDBoss:
                        nodeID = DungeonNodeID.PoDBoss;
                        break;
                    case DungeonItemID.SPEntrance:
                        nodeID = DungeonNodeID.SPAfterRiver;
                        break;
                    case DungeonItemID.SPMapChest:
                    case DungeonItemID.SPPotRowPot:
                        nodeID = DungeonNodeID.SPB1;
                        break;
                    case DungeonItemID.SPBigChest:
                        nodeID = DungeonNodeID.SPBigChest;
                        break;
                    case DungeonItemID.SPCompassChest:
                    case DungeonItemID.SPTrench2Pot:
                        nodeID = DungeonNodeID.SPB1PastRightHammerBlocks;
                        break;
                    case DungeonItemID.SPWestChest:
                    case DungeonItemID.SPBigKeyChest:
                        nodeID = DungeonNodeID.SPB1PastLeftKeyDoor;
                        break;
                    case DungeonItemID.SPFloodedRoomLeft:
                    case DungeonItemID.SPFloodedRoomRight:
                    case DungeonItemID.SPWaterfallRoom:
                    case DungeonItemID.SPWaterwayPot:
                        nodeID = DungeonNodeID.SPB1PastBackFirstKeyDoor;
                        break;
                    case DungeonItemID.SPBoss:
                        nodeID = DungeonNodeID.SPBoss;
                        break;
                    case DungeonItemID.SPTrench1Pot:
                        nodeID = DungeonNodeID.SPB1PastFirstRightKeyDoor;
                        break;
                    case DungeonItemID.SPHookshotPot:
                        nodeID = DungeonNodeID.SPB1KeyLedge;
                        break;
                    case DungeonItemID.SWBigKeyChest:
                    case DungeonItemID.SWWestLobbyPot:
                        nodeID = DungeonNodeID.SWFrontBackConnector;
                        break;
                    case DungeonItemID.SWMapChest:
                        nodeID = DungeonNodeID.SWBigChestAreaBottom;
                        break;
                    case DungeonItemID.SWBigChest:
                        nodeID = DungeonNodeID.SWBigChest;
                        break;
                    case DungeonItemID.SWPotPrison:
                    case DungeonItemID.SWCompassChest:
                        nodeID = DungeonNodeID.SWFrontLeftSide;
                        break;
                    case DungeonItemID.SWPinballRoom:
                        nodeID = DungeonNodeID.SWFrontRightSide;
                        break;
                    case DungeonItemID.SWBridgeRoom:
                        nodeID = DungeonNodeID.SWBack;
                        break;
                    case DungeonItemID.SWBoss:
                        nodeID = DungeonNodeID.SWBoss;
                        break;
                    case DungeonItemID.SWSpikeCornerDrop:
                        nodeID = DungeonNodeID.SWBackPastCurtains;
                        break;
                    case DungeonItemID.TTMapChest:
                    case DungeonItemID.TTAmbushChest:
                    case DungeonItemID.TTCompassChest:
                    case DungeonItemID.TTBigKeyChest:
                        nodeID = DungeonNodeID.TT;
                        break;
                    case DungeonItemID.TTAttic:
                        nodeID = DungeonNodeID.TTPastSecondKeyDoor;
                        break;
                    case DungeonItemID.TTBlindsCell:
                    case DungeonItemID.TTSpikeSwitchPot:
                        nodeID = DungeonNodeID.TTPastFirstKeyDoor;
                        break;
                    case DungeonItemID.TTBigChest:
                        nodeID = DungeonNodeID.TTBigChest;
                        break;
                    case DungeonItemID.TTBoss:
                        nodeID = DungeonNodeID.TTBoss;
                        break;
                    case DungeonItemID.TTHallwayPot:
                        nodeID = DungeonNodeID.TTPastBigKeyDoor;
                        break;
                    case DungeonItemID.IPCompassChest:
                        nodeID = DungeonNodeID.IPB1LeftSide;
                        break;
                    case DungeonItemID.IPSpikeRoom:
                        nodeID = DungeonNodeID.IPSpikeRoom;
                        break;
                    case DungeonItemID.IPMapChest:
                    case DungeonItemID.IPHammerBlockDrop:
                        nodeID = DungeonNodeID.IPB2PastLiftBlock;
                        break;
                    case DungeonItemID.IPBigKeyChest:
                        nodeID = DungeonNodeID.IPB1RightSide;
                        break;
                    case DungeonItemID.IPFreezorChest:
                        nodeID = DungeonNodeID.IPFreezorChest;
                        break;
                    case DungeonItemID.IPBigChest:
                        nodeID = DungeonNodeID.IPBigChest;
                        break;
                    case DungeonItemID.IPIcedTRoom:
                    case DungeonItemID.IPManyPotsPot:
                        nodeID = DungeonNodeID.IPB5;
                        break;
                    case DungeonItemID.IPBoss:
                        nodeID = DungeonNodeID.IPBoss;
                        break;
                    case DungeonItemID.IPJellyDrop:
                        nodeID = DungeonNodeID.IPPastEntranceFreezorRoom;
                        break;
                    case DungeonItemID.IPConveyerDrop:
                        nodeID = DungeonNodeID.IPB2LeftSide;
                        break;
                    case DungeonItemID.MMBridgeChest:
                    case DungeonItemID.MMSpikeChest:
                    case DungeonItemID.MMSpikesPot:
                        nodeID = DungeonNodeID.MMPastEntranceGap;
                        break;
                    case DungeonItemID.MMMainLobby:
                        nodeID = DungeonNodeID.MMB1LobbyBeyondBlueBlocks;
                        break;
                    case DungeonItemID.MMCompassChest:
                        nodeID = DungeonNodeID.MMB1PastFourTorchRoom;
                        break;
                    case DungeonItemID.MMBigKeyChest:
                        nodeID = DungeonNodeID.MMF1PastFourTorchRoom;
                        break;
                    case DungeonItemID.MMBigChest:
                        nodeID = DungeonNodeID.MMBigChest;
                        break;
                    case DungeonItemID.MMMapChest:
                        nodeID = DungeonNodeID.MMB1RightSideBeyondBlueBlocks;
                        break;
                    case DungeonItemID.MMBoss:
                        nodeID = DungeonNodeID.MMBoss;
                        break;
                    case DungeonItemID.MMFishbonePot:
                        nodeID = DungeonNodeID.MMB1TopSide;
                        break;
                    case DungeonItemID.MMConveyerCrystalDrop:
                        nodeID = DungeonNodeID.MMB1LeftSidePastFirstKeyDoor;
                        break;
                    case DungeonItemID.TRCompassChest:
                        nodeID = DungeonNodeID.TRF1CompassChestArea;
                        break;
                    case DungeonItemID.TRRollerRoomLeft:
                    case DungeonItemID.TRRollerRoomRight:
                        nodeID = DungeonNodeID.TRF1RollerRoom;
                        break;
                    case DungeonItemID.TRChainChomps:
                        nodeID = DungeonNodeID.TRF1PastSecondKeyDoor;
                        break;
                    case DungeonItemID.TRBigKeyChest:
                        nodeID = DungeonNodeID.TRB1PastBigKeyChestKeyDoor;
                        break;
                    case DungeonItemID.TRBigChest:
                        nodeID = DungeonNodeID.TRBigChest;
                        break;
                    case DungeonItemID.TRCrystarollerRoom:
                        nodeID = DungeonNodeID.TRB1RightSide;
                        break;
                    case DungeonItemID.TRLaserBridgeTopLeft:
                    case DungeonItemID.TRLaserBridgeTopRight:
                    case DungeonItemID.TRLaserBridgeBottomLeft:
                    case DungeonItemID.TRLaserBridgeBottomRight:
                        nodeID = DungeonNodeID.TRLaserBridgeChests;
                        break;
                    case DungeonItemID.TRBoss:
                        nodeID = DungeonNodeID.TRBoss;
                        break;
                    case DungeonItemID.TRPokey1Drop:
                        nodeID = DungeonNodeID.TRF1PastFirstKeyDoor;
                        break;
                    case DungeonItemID.TRPokey2Drop:
                        nodeID = DungeonNodeID.TRB1;
                        break;
                    case DungeonItemID.GTHopeRoomLeft:
                    case DungeonItemID.GTHopeRoomRight:
                        nodeID = DungeonNodeID.GT1FRight;
                        break;
                    case DungeonItemID.GTBobsTorch:
                        nodeID = DungeonNodeID.GTBobsTorch;
                        break;
                    case DungeonItemID.GTDMsRoomTopLeft:
                    case DungeonItemID.GTDMsRoomTopRight:
                    case DungeonItemID.GTDMsRoomBottomLeft:
                    case DungeonItemID.GTDMsRoomBottomRight:
                        nodeID = DungeonNodeID.GT1FLeftDMsRoom;
                        break;
                    case DungeonItemID.GTMapChest:
                        nodeID = DungeonNodeID.GT1FLeftMapChestRoom;
                        break;
                    case DungeonItemID.GTFiresnakeRoom:
                        nodeID = DungeonNodeID.GT1FLeftPastFiresnakeRoomGap;
                        break;
                    case DungeonItemID.GTRandomizerRoomTopLeft:
                    case DungeonItemID.GTRandomizerRoomTopRight:
                    case DungeonItemID.GTRandomizerRoomBottomLeft:
                    case DungeonItemID.GTRandomizerRoomBottomRight:
                        nodeID = DungeonNodeID.GT1FLeftRandomizerRoom;
                        break;
                    case DungeonItemID.GTTileRoom:
                        nodeID = DungeonNodeID.GT1FRightTileRoom;
                        break;
                    case DungeonItemID.GTCompassRoomTopLeft:
                    case DungeonItemID.GTCompassRoomTopRight:
                    case DungeonItemID.GTCompassRoomBottomLeft:
                    case DungeonItemID.GTCompassRoomBottomRight:
                        nodeID = DungeonNodeID.GT1FRightCompassRoom;
                        break;
                    case DungeonItemID.GTBobsChest:
                        nodeID = DungeonNodeID.GT1FBottomRoom;
                        break;
                    case DungeonItemID.GTBigKeyRoomTopLeft:
                    case DungeonItemID.GTBigKeyRoomTopRight:
                    case DungeonItemID.GTBigKeyChest:
                        nodeID = DungeonNodeID.GTB1BossChests;
                        break;
                    case DungeonItemID.GTBigChest:
                        nodeID = DungeonNodeID.GTBigChest;
                        break;
                    case DungeonItemID.GTMiniHelmasaurRoomLeft:
                    case DungeonItemID.GTMiniHelmasaurRoomRight:
                    case DungeonItemID.GTMiniHelmasaurDrop:
                        nodeID = DungeonNodeID.GT5FPastFourTorchRooms;
                        break;
                    case DungeonItemID.GTPreMoldormChest:
                        nodeID = DungeonNodeID.GT6FPastFirstKeyDoor;
                        break;
                    case DungeonItemID.GTMoldormChest:
                        nodeID = DungeonNodeID.GTBoss3Item;
                        break;
                    case DungeonItemID.GTBoss1:
                        nodeID = DungeonNodeID.GTBoss1;
                        break;
                    case DungeonItemID.GTBoss2:
                        nodeID = DungeonNodeID.GTBoss2;
                        break;
                    case DungeonItemID.GTBoss3:
                        nodeID = DungeonNodeID.GTBoss3;
                        break;
                    case DungeonItemID.GTFinalBoss:
                        nodeID = DungeonNodeID.GTFinalBoss;
                        break;
                    case DungeonItemID.GTConveyorCrossPot:
                        nodeID = DungeonNodeID.GT1FLeft;
                        break;
                    case DungeonItemID.GTDoubleSwitchPot:
                        nodeID = DungeonNodeID.GT1FLeftPastBonkableGaps;
                        break;
                    case DungeonItemID.GTConveyorStarPitsPot:
                        nodeID = DungeonNodeID.GT1FRightPastCompassRoomPortal;
                        break;
                    default:
                        continue;
                }
                
                ExpectedValues.Add(id, nodeID);
            }
        }

        [Fact]
        public void GetDungeonItem_ShouldReturnNewDungeonItem()
        {
            var dungeonItem = _sut.GetDungeonItem(_dungeonData, DungeonItemID.ATBoss);
            
            Assert.NotNull(dungeonItem);
        }

        [Fact]
        public void GetDungeonItem_ShouldThrowException_WhenIDIsUnexpected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _ = _sut.GetDungeonItem(_dungeonData, (DungeonItemID)int.MaxValue));
        }

        [Theory]
        [MemberData(nameof(GetDungeonItem_ShouldCallExpectedIndexOnDungeonNodesData))]
        public void GetDungeonItem_ShouldCallExpectedIndexOnDungeonNodes(DungeonNodeID expected, DungeonItemID id)
        {
            _ = _sut.GetDungeonItem(_dungeonData, id);

            _dungeonData.Nodes[expected].Received();
        }

        public static IEnumerable<object[]> GetDungeonItem_ShouldCallExpectedIndexOnDungeonNodesData()
        {
            PopulateExpectedValues();

            return ExpectedValues.Select(keyValuePair => new object[] {keyValuePair.Value, keyValuePair.Key}).ToList();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IDungeonItemFactory.Factory>();
            var sut = factory();
            
            Assert.NotNull(sut as DungeonItemFactory);
        }
    }
}