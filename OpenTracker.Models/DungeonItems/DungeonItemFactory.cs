using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using System;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This is the class for creating dungeon items.
    /// </summary>
    internal static class DungeonItemFactory
    {
        /// <summary>
        /// Returns a dungeon node to which the specified item ID belongs.
        /// </summary>
        /// <param name="id">
        /// The dungeon node identity.
        /// </param>
        /// <param name="dungeonData">
        /// The dungeon mutable data parent class.
        /// </param>
        /// <returns>
        /// A dungeon node.
        /// </returns>
        private static IDungeonNode GetDungeonItemNode(
            DungeonItemID id, IMutableDungeon dungeonData)
        {
            switch (id)
            {
                case DungeonItemID.HCSanctuary:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.HCSanctuary];
                    }
                case DungeonItemID.HCMapChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.HCFront];
                    }
                case DungeonItemID.HCBoomerangChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.HCPastEscapeFirstKeyDoor];
                    }
                case DungeonItemID.HCZeldasCell:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.HCPastEscapeSecondKeyDoor];
                    }
                case DungeonItemID.HCDarkCross:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.HCDarkRoomFront];
                    }
                case DungeonItemID.HCSecretRoomLeft:
                case DungeonItemID.HCSecretRoomMiddle:
                case DungeonItemID.HCSecretRoomRight:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.HCBack];
                    }
                case DungeonItemID.ATRoom03:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.AT];
                    }
                case DungeonItemID.ATDarkMaze:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.ATPastFirstKeyDoor];
                    }
                case DungeonItemID.ATBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.ATBoss];
                    }
                case DungeonItemID.EPCannonballChest:
                case DungeonItemID.EPMapChest:
                case DungeonItemID.EPCompassChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.EP];
                    }
                case DungeonItemID.EPBigChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.EPBigChest];
                    }
                case DungeonItemID.EPBigKeyChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.EPPastRightKeyDoor];
                    }
                case DungeonItemID.EPBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.EPBoss];
                    }
                case DungeonItemID.DPMapChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.DPFront];
                    }
                case DungeonItemID.DPTorch:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.DPTorchItem];
                    }
                case DungeonItemID.DPBigChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.DPBigChest];
                    }
                case DungeonItemID.DPCompassChest:
                case DungeonItemID.DPBigKeyChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.DPPastRightKeyDoor];
                    }
                case DungeonItemID.DPBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.DPBoss];
                    }
                case DungeonItemID.ToHBasementCage:
                case DungeonItemID.ToHMapChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.ToH];
                    }
                case DungeonItemID.ToHBigKeyChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.ToHBasementTorchRoom];
                    }
                case DungeonItemID.ToHCompassChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.ToHPastBigKeyDoor];
                    }
                case DungeonItemID.ToHBigChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.ToHBigChest];
                    }
                case DungeonItemID.ToHBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.ToHBoss];
                    }
                case DungeonItemID.PoDShooterRoom:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.PoD];
                    }
                case DungeonItemID.PoDMapChest:
                case DungeonItemID.PoDArenaLedge:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.PoDPastFirstRedGoriyaRoom];
                    }
                case DungeonItemID.PoDBigKeyChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.PoDBigKeyChestArea];
                    }
                case DungeonItemID.PoDStalfosBasement:
                case DungeonItemID.PoDArenaBridge:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.PoDLobbyArena];
                    }
                case DungeonItemID.PoDCompassChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor];
                    }
                case DungeonItemID.PoDDarkBasementLeft:
                case DungeonItemID.PoDDarkBasementRight:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.PoDDarkBasement];
                    }
                case DungeonItemID.PoDHarmlessHellway:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.PoDHarmlessHellwayRoom];
                    }
                case DungeonItemID.PoDDarkMazeTop:
                case DungeonItemID.PoDDarkMazeBottom:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.PoDDarkMaze];
                    }
                case DungeonItemID.PoDBigChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.PoDBigChest];
                    }
                case DungeonItemID.PoDBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.PoDBoss];
                    }
                case DungeonItemID.SPEntrance:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SPAfterRiver];
                    }
                case DungeonItemID.SPMapChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SPB1];
                    }
                case DungeonItemID.SPBigChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SPBigChest];
                    }
                case DungeonItemID.SPCompassChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SPB1PastRightHammerBlocks];
                    }
                case DungeonItemID.SPWestChest:
                case DungeonItemID.SPBigKeyChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SPB1PastLeftKeyDoor];
                    }
                case DungeonItemID.SPFloodedRoomLeft:
                case DungeonItemID.SPFloodedRoomRight:
                case DungeonItemID.SPWaterfallRoom:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SPB1PastBackFirstKeyDoor];
                    }
                case DungeonItemID.SPBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SPBoss];
                    }
                case DungeonItemID.SWBigKeyChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SWFrontBackConnector];
                    }
                case DungeonItemID.SWMapChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SWBigChestAreaBottom];
                    }
                case DungeonItemID.SWBigChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SWBigChest];
                    }
                case DungeonItemID.SWPotPrison:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SWFrontLeftSide];
                    }
                case DungeonItemID.SWCompassChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SWFrontLeftSide];
                    }
                case DungeonItemID.SWPinballRoom:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SWFrontRightSide];
                    }
                case DungeonItemID.SWBridgeRoom:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SWBack];
                    }
                case DungeonItemID.SWBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.SWBoss];
                    }
                case DungeonItemID.TTMapChest:
                case DungeonItemID.TTAmbushChest:
                case DungeonItemID.TTCompassChest:
                case DungeonItemID.TTBigKeyChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TT];
                    }
                case DungeonItemID.TTAttic:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TTPastSecondKeyDoor];
                    }
                case DungeonItemID.TTBlindsCell:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TTPastFirstKeyDoor];
                    }
                case DungeonItemID.TTBigChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TTBigChest];
                    }
                case DungeonItemID.TTBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TTBoss];
                    }
                case DungeonItemID.IPCompassChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.IPB1LeftSide];
                    }
                case DungeonItemID.IPSpikeRoom:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.IPSpikeRoom];
                    }
                case DungeonItemID.IPMapChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.IPB2PastLiftBlock];
                    }
                case DungeonItemID.IPBigKeyChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.IPB1RightSide];
                    }
                case DungeonItemID.IPFreezorChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.IPFreezorChest];
                    }
                case DungeonItemID.IPBigChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.IPBigChest];
                    }
                case DungeonItemID.IPIcedTRoom:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.IPB5];
                    }
                case DungeonItemID.IPBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.IPBoss];
                    }
                case DungeonItemID.MMBridgeChest:
                case DungeonItemID.MMSpikeChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.MMPastEntranceGap];
                    }
                case DungeonItemID.MMMainLobby:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.MMB1LobbyBeyondBlueBlocks];
                    }
                case DungeonItemID.MMCompassChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.MMB1PastFourTorchRoom];
                    }
                case DungeonItemID.MMBigKeyChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.MMF1PastFourTorchRoom];
                    }
                case DungeonItemID.MMBigChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.MMBigChest];
                    }
                case DungeonItemID.MMMapChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.MMB1RightSideBeyondBlueBlocks];
                    }
                case DungeonItemID.MMBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.MMBoss];
                    }
                case DungeonItemID.TRCompassChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TRF1CompassChestArea];
                    }
                case DungeonItemID.TRRollerRoomLeft:
                case DungeonItemID.TRRollerRoomRight:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TRF1RollerRoom];
                    }
                case DungeonItemID.TRChainChomps:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TRF1PastSecondKeyDoor];
                    }
                case DungeonItemID.TRBigKeyChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TRB1PastBigKeyChestKeyDoor];
                    }
                case DungeonItemID.TRBigChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TRBigChest];
                    }
                case DungeonItemID.TRCrystarollerRoom:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TRB1RightSide];
                    }
                case DungeonItemID.TRLaserBridgeTopLeft:
                case DungeonItemID.TRLaserBridgeTopRight:
                case DungeonItemID.TRLaserBridgeBottomLeft:
                case DungeonItemID.TRLaserBrdigeBottomRight:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TRLaserBridgeChests];
                    }
                case DungeonItemID.TRBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.TRBoss];
                    }
                case DungeonItemID.GTHopeRoomLeft:
                case DungeonItemID.GTHopeRoomRight:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GT1FRight];
                    }
                case DungeonItemID.GTBobsTorch:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GTBobsTorch];
                    }
                case DungeonItemID.GTDMsRoomTopLeft:
                case DungeonItemID.GTDMsRoomTopRight:
                case DungeonItemID.GTDMsRoomBottomLeft:
                case DungeonItemID.GTDMsRoomBottomRight:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GT1FLeftDMsRoom];
                    }
                case DungeonItemID.GTMapChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GT1FLeftMapChestRoom];
                    }
                case DungeonItemID.GTFiresnakeRoom:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomGap];
                    }
                case DungeonItemID.GTRandomizerRoomTopLeft:
                case DungeonItemID.GTRandomizerRoomTopRight:
                case DungeonItemID.GTRandomizerRoomBottomLeft:
                case DungeonItemID.GTRandomizerRoomBottomRight:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GT1FLeftRandomizerRoom];
                    }
                case DungeonItemID.GTTileRoom:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GT1FRightTileRoom];
                    }
                case DungeonItemID.GTCompassRoomTopLeft:
                case DungeonItemID.GTCompassRoomTopRight:
                case DungeonItemID.GTCompassRoomBottomLeft:
                case DungeonItemID.GTCompassRoomBottomRight:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GT1FRightCompassRoom];
                    }
                case DungeonItemID.GTBobsChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GT1FBottomRoom];
                    }
                case DungeonItemID.GTBigKeyRoomTopLeft:
                case DungeonItemID.GTBigKeyRoomTopRight:
                case DungeonItemID.GTBigKeyChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GTB1BossChests];
                    }
                case DungeonItemID.GTBigChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GTBigChest];
                    }
                case DungeonItemID.GTMiniHelmasaurRoomLeft:
                case DungeonItemID.GTMiniHelmasaurRoomRight:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GT5FPastFourTorchRooms];
                    }
                case DungeonItemID.GTPreMoldormChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GT6FPastFirstKeyDoor];
                    }
                case DungeonItemID.GTMoldormChest:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GT6FPastBossRoomGap];
                    }
                case DungeonItemID.GTBoss1:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GTBoss1];
                    }
                case DungeonItemID.GTBoss2:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GTBoss2];
                    }
                case DungeonItemID.GTBoss3:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GTBoss3];
                    }
                case DungeonItemID.GTFinalBoss:
                    {
                        return dungeonData.RequirementNodes[DungeonNodeID.GTFinalBoss];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a new dungeon item instance of the specified ID.
        /// </summary>
        /// <param name="id">
        /// The dungeon node identity.
        /// </param>
        /// <param name="dungeonData">
        /// The dungeon mutable data parent class.
        /// </param>
        /// <returns>A new dungeon item instance.</returns>
        internal static IDungeonItem GetDungeonItem(
            DungeonItemID id, IMutableDungeon dungeonData)
        {
            if (dungeonData == null)
            {
                throw new ArgumentNullException(nameof(dungeonData));
            }

            return new DungeonItem(id, dungeonData, GetDungeonItemNode(id, dungeonData));
        }
    }
}
