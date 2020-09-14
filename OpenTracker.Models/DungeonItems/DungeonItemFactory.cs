using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.RequirementNodes;
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
        private static IRequirementNode GetDungeonItemNode(
            DungeonItemID id, IMutableDungeon dungeonData)
        {
            switch (id)
            {
                case DungeonItemID.HCSanctuary:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCSanctuary];
                    }
                case DungeonItemID.HCMapChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCFront];
                    }
                case DungeonItemID.HCBoomerangChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor];
                    }
                case DungeonItemID.HCZeldasCell:
                    {
                        return dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor];
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
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPB1];
                    }
                case DungeonItemID.SPBigChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPBigChest];
                    }
                case DungeonItemID.SPCompassChest:
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
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor];
                    }
                case DungeonItemID.SPBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.SPBoss];
                    }
                case DungeonItemID.SWBigKeyChest:
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
                case DungeonItemID.IPCompassChest:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPB1LeftSide];
                    }
                case DungeonItemID.IPSpikeRoom:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPSpikeRoom];
                    }
                case DungeonItemID.IPMapChest:
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
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPB5];
                    }
                case DungeonItemID.IPBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.IPBoss];
                    }
                case DungeonItemID.MMBridgeChest:
                case DungeonItemID.MMSpikeChest:
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
                case DungeonItemID.TRLaserBrdigeBottomRight:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRLaserBridgeChests];
                    }
                case DungeonItemID.TRBoss:
                    {
                        return dungeonData.Nodes[DungeonNodeID.TRBoss];
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
