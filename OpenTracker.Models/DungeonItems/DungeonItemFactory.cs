using System;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.RequirementNodes;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This class contains the creation logic for dungeon items.
    /// </summary>
    public class DungeonItemFactory : IDungeonItemFactory
    {
        private readonly IDungeonItem.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The factory for creating dungeon items.
        /// </param>
        public DungeonItemFactory(IDungeonItem.Factory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Returns a dungeon node to which the specified item ID belongs.
        /// </summary>
        /// <param name="dungeonData">
        /// The dungeon mutable data parent class.
        /// </param>
        /// <param name="id">
        /// The dungeon node identity.
        /// </param>
        /// <returns>
        /// A dungeon node.
        /// </returns>
        private static IRequirementNode GetDungeonItemNode(IMutableDungeon dungeonData, DungeonItemID id)
        {
            switch (id)
            {
                case DungeonItemID.HCSanctuary:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCSanctuary];
                    }
                case DungeonItemID.HCMapChest:
                case DungeonItemID.HCMapGuardDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCFront];
                    }
                case DungeonItemID.HCBoomerangChest:
                case DungeonItemID.HCBoomerangGuardDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor];
                    }
                case DungeonItemID.HCZeldasCell:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCZeldasCell];
                    }
                case DungeonItemID.HCDarkCross:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCDarkRoomFront];
                    }
                case DungeonItemID.HCSecretRoomLeft:
                case DungeonItemID.HCSecretRoomMiddle:
                case DungeonItemID.HCSecretRoomRight:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCBack];
                    }
                case DungeonItemID.HCKeyRatDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor];
                    }
                case DungeonItemID.HCBigKeyDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor];
                    }
                case DungeonItemID.ATRoom03:
                    {
                        return dungeonData.Nodes[DungeonNodeID.AT];
                    }
                case DungeonItemID.ATDarkMaze:
                    {
                        return dungeonData.Nodes[DungeonNodeID.ATPastFirstKeyDoor];
                    }
                case DungeonItemID.ATBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.ATBoss];
                    }
                case DungeonItemID.ATDarkArcherDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.ATPastSecondKeyDoor];
                    }
                case DungeonItemID.ATCircleOfPotsDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.ATPastThirdKeyDoor];
                    }
                case DungeonItemID.EPCannonballChest:
                case DungeonItemID.EPMapChest:
                case DungeonItemID.EPCompassChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.EP];
                    }
                case DungeonItemID.EPBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.EPBigChest];
                    }
                case DungeonItemID.EPBigKeyChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.EPPastRightKeyDoor];
                    }
                case DungeonItemID.EPBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.EPBoss];
                    }
                case DungeonItemID.EPDarkSquarePot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.EPRightDarkRoom];
                    }
                case DungeonItemID.EPDarkEyegoreDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.EPBackDarkRoom];
                    }
                case DungeonItemID.DPMapChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.DPFront];
                    }
                case DungeonItemID.DPTorch:
                    {
                        return dungeonData.Nodes[DungeonNodeID.DPTorch];
                    }
                case DungeonItemID.DPBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.DPBigChest];
                    }
                case DungeonItemID.DPCompassChest:
                case DungeonItemID.DPBigKeyChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.DPPastRightKeyDoor];
                    }
                case DungeonItemID.DPBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.DPBoss];
                    }
                case DungeonItemID.DPTiles1Pot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.DPBack];
                    }
                case DungeonItemID.DPBeamosHallPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.DP2F];
                    }
                case DungeonItemID.DPTiles2Pot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor];
                    }
                case DungeonItemID.ToHBasementCage:
                case DungeonItemID.ToHMapChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.ToH];
                    }
                case DungeonItemID.ToHBigKeyChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.ToHBasementTorchRoom];
                    }
                case DungeonItemID.ToHCompassChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.ToHPastBigKeyDoor];
                    }
                case DungeonItemID.ToHBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.ToHBigChest];
                    }
                case DungeonItemID.ToHBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.ToHBoss];
                    }
                case DungeonItemID.PoDShooterRoom:
                    {
                        return dungeonData.Nodes[DungeonNodeID.PoD];
                    }
                case DungeonItemID.PoDMapChest:
                case DungeonItemID.PoDArenaLedge:
                    {
                        return dungeonData.Nodes[DungeonNodeID.PoDPastFirstRedGoriyaRoom];
                    }
                case DungeonItemID.PoDBigKeyChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.PoDBigKeyChestArea];
                    }
                case DungeonItemID.PoDStalfosBasement:
                case DungeonItemID.PoDArenaBridge:
                    {
                        return dungeonData.Nodes[DungeonNodeID.PoDLobbyArena];
                    }
                case DungeonItemID.PoDCompassChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor];
                    }
                case DungeonItemID.PoDDarkBasementLeft:
                case DungeonItemID.PoDDarkBasementRight:
                    {
                        return dungeonData.Nodes[DungeonNodeID.PoDDarkBasement];
                    }
                case DungeonItemID.PoDHarmlessHellway:
                    {
                        return dungeonData.Nodes[DungeonNodeID.PoDHarmlessHellwayRoom];
                    }
                case DungeonItemID.PoDDarkMazeTop:
                case DungeonItemID.PoDDarkMazeBottom:
                    {
                        return dungeonData.Nodes[DungeonNodeID.PoDDarkMaze];
                    }
                case DungeonItemID.PoDBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.PoDBigChest];
                    }
                case DungeonItemID.PoDBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.PoDBoss];
                    }
                case DungeonItemID.SPEntrance:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPAfterRiver];
                    }
                case DungeonItemID.SPMapChest:
                case DungeonItemID.SPPotRowPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPB1];
                    }
                case DungeonItemID.SPBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPBigChest];
                    }
                case DungeonItemID.SPCompassChest:
                case DungeonItemID.SPTrench2Pot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks];
                    }
                case DungeonItemID.SPWestChest:
                case DungeonItemID.SPBigKeyChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPB1PastLeftKeyDoor];
                    }
                case DungeonItemID.SPFloodedRoomLeft:
                case DungeonItemID.SPFloodedRoomRight:
                case DungeonItemID.SPWaterfallRoom:
                case DungeonItemID.SPWaterwayPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor];
                    }
                case DungeonItemID.SPBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPBoss];
                    }
                case DungeonItemID.SPTrench1Pot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor];
                    }
                case DungeonItemID.SPHookshotPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPB1KeyLedge];
                    }
                case DungeonItemID.SWBigKeyChest:
                case DungeonItemID.SWWestLobbyPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SWFrontBackConnector];
                    }
                case DungeonItemID.SWMapChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom];
                    }
                case DungeonItemID.SWBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SWBigChest];
                    }
                case DungeonItemID.SWPotPrison:
                case DungeonItemID.SWCompassChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide];
                    }
                case DungeonItemID.SWPinballRoom:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SWFrontRightSide];
                    }
                case DungeonItemID.SWBridgeRoom:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SWBack];
                    }
                case DungeonItemID.SWBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SWBoss];
                    }
                case DungeonItemID.SWSpikeCornerDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SWBackPastCurtains];
                    }
                case DungeonItemID.TTMapChest:
                case DungeonItemID.TTAmbushChest:
                case DungeonItemID.TTCompassChest:
                case DungeonItemID.TTBigKeyChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TT];
                    }
                case DungeonItemID.TTAttic:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TTPastSecondKeyDoor];
                    }
                case DungeonItemID.TTBlindsCell:
                case DungeonItemID.TTSpikeSwitchPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor];
                    }
                case DungeonItemID.TTBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TTBigChest];
                    }
                case DungeonItemID.TTBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TTBoss];
                    }
                case DungeonItemID.TTHallwayPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor];
                    }
                case DungeonItemID.IPCompassChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPB1LeftSide];
                    }
                case DungeonItemID.IPSpikeRoom:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPSpikeRoom];
                    }
                case DungeonItemID.IPMapChest:
                case DungeonItemID.IPHammerBlockDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPB2PastLiftBlock];
                    }
                case DungeonItemID.IPBigKeyChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPB1RightSide];
                    }
                case DungeonItemID.IPFreezorChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPFreezorChest];
                    }
                case DungeonItemID.IPBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPBigChest];
                    }
                case DungeonItemID.IPIcedTRoom:
                case DungeonItemID.IPManyPotsPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPB5];
                    }
                case DungeonItemID.IPBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPBoss];
                    }
                case DungeonItemID.IPJellyDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPPastEntranceFreezorRoom];
                    }
                case DungeonItemID.IPConveyerDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPB2LeftSide];
                    }
                case DungeonItemID.MMBridgeChest:
                case DungeonItemID.MMSpikeChest:
                case DungeonItemID.MMSpikesPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap];
                    }
                case DungeonItemID.MMMainLobby:
                    {
                        return dungeonData.Nodes[DungeonNodeID.MMB1LobbyBeyondBlueBlocks];
                    }
                case DungeonItemID.MMCompassChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.MMB1PastFourTorchRoom];
                    }
                case DungeonItemID.MMBigKeyChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.MMF1PastFourTorchRoom];
                    }
                case DungeonItemID.MMBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.MMBigChest];
                    }
                case DungeonItemID.MMMapChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.MMB1RightSideBeyondBlueBlocks];
                    }
                case DungeonItemID.MMBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.MMBoss];
                    }
                case DungeonItemID.MMFishbonePot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.MMB1TopSide];
                    }
                case DungeonItemID.MMConveyerCrystalDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor];
                    }
                case DungeonItemID.TRCompassChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRF1CompassChestArea];
                    }
                case DungeonItemID.TRRollerRoomLeft:
                case DungeonItemID.TRRollerRoomRight:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRF1RollerRoom];
                    }
                case DungeonItemID.TRChainChomps:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRF1PastSecondKeyDoor];
                    }
                case DungeonItemID.TRBigKeyChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRB1PastBigKeyChestKeyDoor];
                    }
                case DungeonItemID.TRBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRBigChest];
                    }
                case DungeonItemID.TRCrystarollerRoom:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRB1RightSide];
                    }
                case DungeonItemID.TRLaserBridgeTopLeft:
                case DungeonItemID.TRLaserBridgeTopRight:
                case DungeonItemID.TRLaserBridgeBottomLeft:
                case DungeonItemID.TRLaserBridgeBottomRight:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRLaserBridgeChests];
                    }
                case DungeonItemID.TRBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRBoss];
                    }
                case DungeonItemID.TRPokey1Drop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor];
                    }
                case DungeonItemID.TRPokey2Drop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRB1];
                    }
                case DungeonItemID.GTHopeRoomLeft:
                case DungeonItemID.GTHopeRoomRight:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT1FRight];
                    }
                case DungeonItemID.GTBobsTorch:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GTBobsTorch];
                    }
                case DungeonItemID.GTDMsRoomTopLeft:
                case DungeonItemID.GTDMsRoomTopRight:
                case DungeonItemID.GTDMsRoomBottomLeft:
                case DungeonItemID.GTDMsRoomBottomRight:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT1FLeftDMsRoom];
                    }
                case DungeonItemID.GTMapChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT1FLeftMapChestRoom];
                    }
                case DungeonItemID.GTFiresnakeRoom:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomGap];
                    }
                case DungeonItemID.GTRandomizerRoomTopLeft:
                case DungeonItemID.GTRandomizerRoomTopRight:
                case DungeonItemID.GTRandomizerRoomBottomLeft:
                case DungeonItemID.GTRandomizerRoomBottomRight:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT1FLeftRandomizerRoom];
                    }
                case DungeonItemID.GTTileRoom:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT1FRightTileRoom];
                    }
                case DungeonItemID.GTCompassRoomTopLeft:
                case DungeonItemID.GTCompassRoomTopRight:
                case DungeonItemID.GTCompassRoomBottomLeft:
                case DungeonItemID.GTCompassRoomBottomRight:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT1FRightCompassRoom];
                    }
                case DungeonItemID.GTBobsChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT1FBottomRoom];
                    }
                case DungeonItemID.GTBigKeyRoomTopLeft:
                case DungeonItemID.GTBigKeyRoomTopRight:
                case DungeonItemID.GTBigKeyChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GTB1BossChests];
                    }
                case DungeonItemID.GTBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GTBigChest];
                    }
                case DungeonItemID.GTMiniHelmasaurRoomLeft:
                case DungeonItemID.GTMiniHelmasaurRoomRight:
                case DungeonItemID.GTMiniHelmasaurDrop:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT5FPastFourTorchRooms];
                    }
                case DungeonItemID.GTPreMoldormChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor];
                    }
                case DungeonItemID.GTMoldormChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GTBoss3Item];
                    }
                case DungeonItemID.GTBoss1:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GTBoss1];
                    }
                case DungeonItemID.GTBoss2:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GTBoss2];
                    }
                case DungeonItemID.GTBoss3:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GTBoss3];
                    }
                case DungeonItemID.GTFinalBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GTFinalBoss];
                    }
                case DungeonItemID.GTConveyorCrossPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT1FLeft];
                    }
                case DungeonItemID.GTDoubleSwitchPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps];
                    }
                case DungeonItemID.GTConveyorStarPitsPot:
                    {
                        return dungeonData.Nodes[DungeonNodeID.GT1FRightPastCompassRoomPortal];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a new dungeon item for the specified dungeon data and dungeon item ID.
        /// </summary>
        /// <param name="dungeonData">
        /// The dungeon data.
        /// </param>
        /// <param name="id">
        /// The dungeon item ID.
        /// </param>
        /// <returns>
        /// A new dungeon item.
        /// </returns>
        public IDungeonItem GetDungeonItem(IMutableDungeon dungeonData, DungeonItemID id)
        {
            return _factory(dungeonData, id, GetDungeonItemNode(dungeonData, id));
        }
    }
}
