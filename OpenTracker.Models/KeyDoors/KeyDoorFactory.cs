using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This is the class for creating key doors.
    /// </summary>
    internal static class KeyDoorFactory
    {
        /// <summary>
        /// Returns the list of dungeon node IDs for the specified key door ID.
        /// </summary>
        /// <param name="id">
        /// The key door ID.
        /// </param>
        /// <returns>
        /// The list of dungeon node IDs.
        /// </returns>
        internal static List<DungeonNodeID> GetKeyDoorConnectedNodeIDs(KeyDoorID id)
        {
            switch (id)
            {
                case KeyDoorID.HCEscapeFirstKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.HCFront,
                            DungeonNodeID.HCPastEscapeFirstKeyDoor
                        };
                    }
                case KeyDoorID.HCEscapeSecondKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.HCPastEscapeFirstKeyDoor,
                            DungeonNodeID.HCPastEscapeSecondKeyDoor
                        };
                    }
                case KeyDoorID.HCDarkCrossRoomKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.HCDarkRoomFront,
                            DungeonNodeID.HCPastDarkCrossKeyDoor
                        };
                    }
                case KeyDoorID.HCSewerRatRoomKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.HCPastDarkCrossKeyDoor,
                            DungeonNodeID.HCPastSewerRatRoomKeyDoor
                        };
                    }
                case KeyDoorID.ATFirstKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.ATDarkMaze,
                            DungeonNodeID.ATPastFirstKeyDoor
                        };
                    }
                case KeyDoorID.ATSecondKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.ATPastFirstKeyDoor,
                            DungeonNodeID.ATPastSecondKeyDoor
                        };
                    }
                case KeyDoorID.ATThirdKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.ATPastSecondKeyDoor,
                            DungeonNodeID.ATPastThirdKeyDoor
                        };
                    }
                case KeyDoorID.ATFourthKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.ATPastThirdKeyDoor,
                            DungeonNodeID.ATPastFourthKeyDoor
                        };
                    }
                case KeyDoorID.EPRightKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.EPRightDarkRoom,
                            DungeonNodeID.EPPastRightKeyDoor
                        };
                    }
                case KeyDoorID.EPBackKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.EPBackDarkRoom,
                            DungeonNodeID.EPPastBackKeyDoor
                        };
                    }
                case KeyDoorID.EPBigChest:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.EP,
                            DungeonNodeID.EPBigChest
                        };
                    }
                case KeyDoorID.EPBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.EP,
                            DungeonNodeID.EPPastBigKeyDoor
                        };
                    }
                case KeyDoorID.DPRightKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.DPFront,
                            DungeonNodeID.DPPastRightKeyDoor
                        };
                    }
                case KeyDoorID.DP1FKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.DPBack,
                            DungeonNodeID.DP2F
                        };
                    }
                case KeyDoorID.DP2FFirstKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.DP2F,
                            DungeonNodeID.DP2FPastFirstKeyDoor
                        };
                    }
                case KeyDoorID.DP2FSecondKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.DP2FPastFirstKeyDoor,
                            DungeonNodeID.DP2FPastSecondKeyDoor
                        };
                    }
                case KeyDoorID.DPBigChest:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.DPFront,
                            DungeonNodeID.DPBigChest
                        };
                    }
                case KeyDoorID.DPBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.DPPastFourTorchWall,
                            DungeonNodeID.DPBossRoom
                        };
                    }
                case KeyDoorID.ToHKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.ToH,
                            DungeonNodeID.ToHPastKeyDoor
                        };
                    }
                case KeyDoorID.ToHBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.ToH,
                            DungeonNodeID.ToHPastBigKeyDoor
                        };
                    }
                case KeyDoorID.ToHBigChest:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.ToHPastBigKeyDoor,
                            DungeonNodeID.ToHBigChest
                        };
                    }
                case KeyDoorID.PoDFrontKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.PoD,
                            DungeonNodeID.PoDLobbyArena
                        };
                    }
                case KeyDoorID.PoDBigKeyChestKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.PoDLobbyArena,
                            DungeonNodeID.PoDBigKeyChestArea
                        };
                    }
                case KeyDoorID.PoDCollapsingWalkwayKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.PoDLobbyArena,
                            DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                        };
                    }
                case KeyDoorID.PoDDarkMazeKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                            DungeonNodeID.PoDPastDarkMazeKeyDoor
                        };
                    }
                case KeyDoorID.PoDHarmlessHellwayKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                            DungeonNodeID.PoDHarmlessHellwayRoom
                        };
                    }
                case KeyDoorID.PoDBossAreaKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.PoDPastHammerBlocks,
                            DungeonNodeID.PoDPastBossAreaKeyDoor
                        };
                    }
                case KeyDoorID.PoDBigChest:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.PoDBigChestLedge,
                            DungeonNodeID.PoDBigChest
                        };
                    }
                case KeyDoorID.PoDBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.PoDPastBossAreaKeyDoor,
                            DungeonNodeID.PoDBossRoom
                        };
                    }
                case KeyDoorID.SP1FKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPAfterRiver,
                            DungeonNodeID.SPB1
                        };
                    }
                case KeyDoorID.SPB1FirstRightKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPB1,
                            DungeonNodeID.SPB1PastFirstRightKeyDoor
                        };
                    }
                case KeyDoorID.SPB1SecondRightKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPB1PastFirstRightKeyDoor,
                            DungeonNodeID.SPB1PastSecondRightKeyDoor
                        };
                    }
                case KeyDoorID.SPB1LeftKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPB1PastRightHammerBlocks,
                            DungeonNodeID.SPB1PastLeftKeyDoor
                        };
                    }
                case KeyDoorID.SPB1BackFirstKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPB1Back,
                            DungeonNodeID.SPB1PastBackFirstKeyDoor
                        };
                    }
                case KeyDoorID.SPBossRoomKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPB1PastBackFirstKeyDoor,
                            DungeonNodeID.SPBossRoom
                        };
                    }
                case KeyDoorID.SPBigChest:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SPB1PastRightHammerBlocks,
                            DungeonNodeID.SPBigChest
                        };
                    }
                case KeyDoorID.SWFrontLeftKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SWBigChestAreaBottom,
                            DungeonNodeID.SWFrontLeftSide
                        };
                    }
                case KeyDoorID.SWFrontRightKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SWBigChestAreaBottom,
                            DungeonNodeID.SWFrontRightSide
                        };
                    }
                case KeyDoorID.SWWorthlessKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SWFrontBackConnector,
                            DungeonNodeID.SWPastTheWorthlessKeyDoor
                        };
                    }
                case KeyDoorID.SWBackFirstKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SWBack,
                            DungeonNodeID.SWBackPastFirstKeyDoor
                        };
                    }
                case KeyDoorID.SWBackSecondKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SWBackPastCurtains,
                            DungeonNodeID.SWBossRoom
                        };
                    }
                case KeyDoorID.SWBigChest:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.SWBigChestAreaTop,
                            DungeonNodeID.SWBigChest
                        };
                    }
                case KeyDoorID.TTFirstKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TTPastBigKeyDoor,
                            DungeonNodeID.TTPastFirstKeyDoor
                        };
                    }
                case KeyDoorID.TTSecondKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TTPastFirstKeyDoor,
                            DungeonNodeID.TTPastSecondKeyDoor
                        };
                    }
                case KeyDoorID.TTBigChestKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TTPastFirstKeyDoor,
                            DungeonNodeID.TTPastBigChestRoomKeyDoor
                        };
                    }
                case KeyDoorID.TTBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TT,
                            DungeonNodeID.TTPastBigKeyDoor
                        };
                    }
                case KeyDoorID.TTBigChest:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TTPastHammerBlocks,
                            DungeonNodeID.TTBigChest
                        };
                    }
                case KeyDoorID.IP1FKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.IPPastEntranceFreezorRoom,
                            DungeonNodeID.IPB1LeftSide
                        };
                    }
                case KeyDoorID.IPB2KeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.IPB2LeftSide,
                            DungeonNodeID.IPB2PastKeyDoor
                        };
                    }
                case KeyDoorID.IPB3KeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.IPB2PastKeyDoor,
                            DungeonNodeID.IPSpikeRoom
                        };
                    }
                case KeyDoorID.IPB4KeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.IPB4IceRoom,
                            DungeonNodeID.IPB4PastKeyDoor
                        };
                    }
                case KeyDoorID.IPB5KeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.IPB5PastBigKeyDoor,
                            DungeonNodeID.IPB6
                        };
                    }
                case KeyDoorID.IPB6KeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.IPB6,
                            DungeonNodeID.IPB6PastKeyDoor
                        };
                    }
                case KeyDoorID.IPBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.IPB5,
                            DungeonNodeID.IPB5PastBigKeyDoor
                        };
                    }
                case KeyDoorID.IPBigChest:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.IPBigChestArea,
                            DungeonNodeID.IPBigChest
                        };
                    }
                case KeyDoorID.MMB1TopRightKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.MMPastEntranceGap,
                            DungeonNodeID.MMB1TopSide
                        };
                    }
                case KeyDoorID.MMB1TopLeftKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.MMPastEntranceGap,
                            DungeonNodeID.MMB1TopSide
                        };
                    }
                case KeyDoorID.MMB1LeftSideFirstKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.MMPastEntranceGap,
                            DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                        };
                    }
                case KeyDoorID.MMB1LeftSideSecondKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                            DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                        };
                    }
                case KeyDoorID.MMB1RightSideKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                            DungeonNodeID.MMB1RightSideBeyondBlueBlocks
                        };
                    }
                case KeyDoorID.MMB2WorthlessKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.MMDarkRoom,
                            DungeonNodeID.MMB2PastWorthlessKeyDoor
                        };
                    }
                case KeyDoorID.MMBigChest:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.MMPastEntranceGap,
                            DungeonNodeID.MMBigChest
                        };
                    }
                case KeyDoorID.MMPortalBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.MMPastEntranceGap,
                            DungeonNodeID.MMB1PastPortalBigKeyDoor
                        };
                    }
                case KeyDoorID.MMBridgeBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.MMB1TopSide,
                            DungeonNodeID.MMB1PastBridgeBigKeyDoor
                        };
                    }
                case KeyDoorID.MMBossRoomBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                            DungeonNodeID.MMBossRoom
                        };
                    }
                case KeyDoorID.TR1FFirstKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRF1FirstKeyDoorArea,
                            DungeonNodeID.TRF1PastFirstKeyDoor
                        };
                    }
                case KeyDoorID.TR1FSecondKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRF1PastFirstKeyDoor,
                            DungeonNodeID.TRF1PastSecondKeyDoor
                        };
                    }
                case KeyDoorID.TR1FThirdKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRF1PastSecondKeyDoor,
                            DungeonNodeID.TRB1
                        };
                    }
                case KeyDoorID.TRB1BigKeyChestKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB1,
                            DungeonNodeID.TRB1PastBigKeyChestKeyDoor
                        };
                    }
                case KeyDoorID.TRB1toB2KeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB1RightSide,
                            DungeonNodeID.TRPastB1toB2KeyDoor
                        };
                    }
                case KeyDoorID.TRB2KeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB2PastDarkMaze,
                            DungeonNodeID.TRB2PastKeyDoor
                        };
                    }
                case KeyDoorID.TRBigChest:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB1BigChestArea,
                            DungeonNodeID.TRBigChest
                        };
                    }
                case KeyDoorID.TRB1BigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB1,
                            DungeonNodeID.TRB1RightSide
                        };
                    }
                case KeyDoorID.TRBossRoomBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB3BossRoomEntry,
                            DungeonNodeID.TRBossRoom
                        };
                    }
                case KeyDoorID.GT1FLeftToRightKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT1FLeft,
                            DungeonNodeID.GT1FRight
                        };
                    }
                case KeyDoorID.GT1FMapChestRoomKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT1FLeftPastBonkableGaps,
                            DungeonNodeID.GT1FLeftMapChestRoom
                        };
                    }
                case KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT1FLeftPastBonkableGaps,
                            DungeonNodeID.GT1FLeftSpikeTrapPortalRoom
                        };
                    }
                case KeyDoorID.GT1FFiresnakeRoomKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                            DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor
                        };
                    }
                case KeyDoorID.GT1FTileRoomKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT1FRightTileRoom,
                            DungeonNodeID.GT1FRightFourTorchRoom
                        };
                    }
                case KeyDoorID.GT1FCollapsingWalkwayKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT1FRightPastCompassRoomPortal,
                            DungeonNodeID.GT1FRightCollapsingWalkway
                        };
                    }
                case KeyDoorID.GT6FFirstKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT5FPastFourTorchRooms,
                            DungeonNodeID.GT6FPastFirstKeyDoor
                        };
                    }
                case KeyDoorID.GT6FSecondKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT6FPastFirstKeyDoor,
                            DungeonNodeID.GT6FBossRoom
                        };
                    }
                case KeyDoorID.GTBigChest:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT1FBottomRoom,
                            DungeonNodeID.GTBigChest
                        };
                    }
                case KeyDoorID.GT3FBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT3FPastRedGoriyaRooms,
                            DungeonNodeID.GT3FPastBigKeyDoor
                        };
                    }
                case KeyDoorID.GT7FBigKeyDoor:
                    {
                        return new List<DungeonNodeID>
                        {
                            DungeonNodeID.GT6FPastBossRoomGap,
                            DungeonNodeID.GTFinalBossRoom
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the list of dungeon node for the specified key door ID.
        /// </summary>
        /// <param name="id">
        /// The key door ID.
        /// </param>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param> 
        /// <returns>
        /// The list of dungeon node IDs.
        /// </returns>
        internal static List<IDungeonNode> GetKeyDoorConnectedNodes(
            KeyDoorID id, IMutableDungeon dungeonData)
        {
            if (dungeonData == null)
            {
                throw new ArgumentNullException(nameof(dungeonData));
            }

            List<IDungeonNode> keyDoorConnectedNodes = new List<IDungeonNode>();

            foreach (var keyDoor in GetKeyDoorConnectedNodeIDs(id))
            {
                keyDoorConnectedNodes.Add(dungeonData.RequirementNodes[keyDoor]);
            }

            return keyDoorConnectedNodes;
        }

        /// <summary>
        /// Returns a new key door for the specified key door ID.
        /// </summary>
        /// <param name="id">
        /// The key door ID.
        /// </param>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        /// <returns></returns>
        internal static IKeyDoor GetKeyDoor(KeyDoorID id, IMutableDungeon dungeonData)
        {
            if (dungeonData == null)
            {
                throw new ArgumentNullException(nameof(dungeonData));
            }

            return new KeyDoor(id, dungeonData, GetKeyDoorConnectedNodes(id, dungeonData));

            throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}
