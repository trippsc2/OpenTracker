using OpenTracker.Models.Dungeons;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonNodes
{
	/// <summary>
	/// This is the class for creating dungeon nodes.
	/// </summary>
	internal static class DungeonNodeFactory
    {
		/// <summary>
		/// Returns the number of free keys provided by the specified node.
		/// </summary>
		/// <param name="id">
		/// The node ID.
		/// </param>
		/// <returns>
		/// A 32-bit signed integer representing the number of free keys provided.
		/// </returns>
        private static int GetDungeonNodeFreeKeys(DungeonNodeID id)
        {
            switch (id)
            {
                case DungeonNodeID.HCFront:
                case DungeonNodeID.HCPastEscapeFirstKeyDoor:
                case DungeonNodeID.HCPastDarkCrossKeyDoor:
                case DungeonNodeID.ATPastSecondKeyDoor:
                case DungeonNodeID.ATPastThirdKeyDoor:
                case DungeonNodeID.EPRightDarkRoom:
                case DungeonNodeID.EPBackDarkRoom:
                case DungeonNodeID.DPBack:
                case DungeonNodeID.DP2F:
                case DungeonNodeID.DP2FPastFirstKeyDoor:
                case DungeonNodeID.SPB1:
                case DungeonNodeID.SPB1PastFirstRightKeyDoor:
                case DungeonNodeID.SPB1KeyLedge:
                case DungeonNodeID.SPB1PastRightHammerBlocks:
                case DungeonNodeID.SPB1PastBackFirstKeyDoor:
                case DungeonNodeID.SWFrontBackConnector:
                case DungeonNodeID.SWBackPastCurtains:
                case DungeonNodeID.TTPastBigKeyDoor:
                case DungeonNodeID.TTPastFirstKeyDoor:
                case DungeonNodeID.IPPastEntranceFreezorRoom:
                case DungeonNodeID.IPB2LeftSide:
                case DungeonNodeID.IPB2PastLiftBlock:
                case DungeonNodeID.IPB5:
                case DungeonNodeID.MMPastEntranceGap:
                case DungeonNodeID.MMB1TopSide:
                case DungeonNodeID.MMB1LeftSidePastFirstKeyDoor:
                case DungeonNodeID.TRF1PastFirstKeyDoor:
                case DungeonNodeID.TRB1:
                case DungeonNodeID.GT1FLeft:
                case DungeonNodeID.GT1FLeftPastBonkableGaps:
                case DungeonNodeID.GT1FRightPastCompassRoomPortal:
                case DungeonNodeID.GT5FPastFourTorchRooms:
                    {
                        return 1;
                    }
            }

            return 0;
        }

		/// <summary>
		/// Returns a list of connections from outside of the dungeon for the specified node.
		/// </summary>
		/// <param name="id">
		/// The node ID.
		/// </param>
		/// <returns>
		/// A list of connections from outside of the dungeon.
		/// </returns>
        internal static List<RequirementNodeConnection> GetDungeonEntryConnections(
			DungeonNodeID id)
        {
			switch (id)
			{
				case DungeonNodeID.HCSanctuary:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.HCSanctuaryEntry)
						};
					}
				case DungeonNodeID.HCFront:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.HCFrontEntry)
						};
					}
				case DungeonNodeID.HCBack:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.HCBackEntry)
						};
					}
				case DungeonNodeID.AT:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.ATEntry)
						};
					}
				case DungeonNodeID.EP:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.EPEntry)
						};
					}
				case DungeonNodeID.DPFront:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.DPFrontEntry),
							new RequirementNodeConnection(RequirementNodeID.DPLeftEntry)
						};
					}
				case DungeonNodeID.DPBack:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.DPBackEntry)
						};
					}
				case DungeonNodeID.ToH:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.ToHEntry)
						};
					}
				case DungeonNodeID.PoD:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.PoDEntry)
						};
					}
				case DungeonNodeID.SP:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.SPEntry)
						};
					}
				case DungeonNodeID.SWBigChestAreaBottom:
				case DungeonNodeID.SWBigChestAreaTop:
				case DungeonNodeID.SWFrontLeftSide:
				case DungeonNodeID.SWFrontRightSide:
				case DungeonNodeID.SWFrontBackConnector:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.SWFrontEntry)
						};
					}
				case DungeonNodeID.SWBack:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.SWBackEntry)
						};
					}
				case DungeonNodeID.TT:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.TTEntry)
						};
					}
				case DungeonNodeID.IP:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.IPEntry)
						};
					}
				case DungeonNodeID.MM:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.MMEntry)
						};
					}
				case DungeonNodeID.TRFront:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.TRFrontEntry)
						};
					}
				case DungeonNodeID.TRB1:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.TRMiddleEntry)
						};
					}
				case DungeonNodeID.TRB1MiddleRightEntranceArea:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.TRMiddleEntry)
						};
					}
				case DungeonNodeID.TRB2PastDarkMaze:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.TRBackEntry)
						};
					}
				case DungeonNodeID.GT:
					{
						return new List<RequirementNodeConnection>
						{
							new RequirementNodeConnection(RequirementNodeID.GTEntry)
						};
					}
				default:
					{
						return new List<RequirementNodeConnection>(0);
					}
			}
        }

        /// <summary>
        /// Returns a list of key doors to which the specified node is connected.
        /// </summary>
        /// <param name="id">
        /// The node ID.
        /// </param>
        /// <param name="dungeon">
        /// The dungeon parent class.
        /// </param>
        /// <returns>
        /// A list of key doors.
        /// </returns>
        private static List<KeyDoorID> GetDungeonKeyDoorConnections(
            DungeonNodeID id, IDungeon dungeon)
        {
            if (dungeon == null)
            {
                throw new ArgumentNullException(nameof(dungeon));
            }

            List<KeyDoorID> keyDoorConnections = new List<KeyDoorID>();

            foreach (var smallKeyDoor in dungeon.SmallKeyDoors)
            {
                if (KeyDoorFactory.GetKeyDoorConnectedNodeIDs(smallKeyDoor).Contains(id))
                {
                    keyDoorConnections.Add(smallKeyDoor);
                }    
            }

            foreach (var bigKeyDoor in dungeon.BigKeyDoors)
            {
                if (KeyDoorFactory.GetKeyDoorConnectedNodeIDs(bigKeyDoor).Contains(id))
                {
                    keyDoorConnections.Add(bigKeyDoor);
                }
            }

            return keyDoorConnections;
        }

		/// <summary>
		/// Returns a list of connections from inside the dungeon for the specified node.
		/// </summary>
		/// <param name="id">
		/// The node ID.
		/// </param>
		/// <returns>
		/// A list of connections from inside the dungeon.
		/// </returns>
        internal static List<DungeonNodeConnection> GetDungeonConnections(DungeonNodeID id)
        {
            switch (id)
            {
				case DungeonNodeID.HCSanctuary:
                    {
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.HCBack)
						};
					}
				case DungeonNodeID.HCFront:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.HCDarkRoomFront)
						};
					}
				case DungeonNodeID.HCDarkRoomFront:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.HCFront,
								RequirementDictionary.Instance[RequirementType.DarkRoomHC])
						};
					}
				case DungeonNodeID.HCPastSewerRatRoomKeyDoor:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.HCDarkRoomBack)
						};
					}
				case DungeonNodeID.HCDarkRoomBack:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.HCPastSewerRatRoomKeyDoor),
							new DungeonNodeConnection(
								DungeonNodeID.HCBack,
								RequirementDictionary.Instance[RequirementType.DarkRoomHC])
						};
					}
				case DungeonNodeID.HCBack:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.HCDarkRoomBack)
						};
					}
				case DungeonNodeID.ATDarkMaze:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.AT,
								RequirementDictionary.Instance[RequirementType.DarkRoomAT])
						};
					}
				case DungeonNodeID.ATBossRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.ATPastFourthKeyDoor,
								RequirementDictionary.Instance[RequirementType.Curtains])
						};
					}
				case DungeonNodeID.ATBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.ATBossRoom,
								RequirementDictionary.Instance[RequirementType.ATBoss])
						};
					}
				case DungeonNodeID.EP:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.EPPastRightKeyDoor)
						};
					}
				case DungeonNodeID.EPRightDarkRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.EP,
								RequirementDictionary.Instance[RequirementType.DarkRoomEPRight])
						};
					}
				case DungeonNodeID.EPBackDarkRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.EPPastBigKeyDoor,
								RequirementDictionary.Instance[RequirementType.DarkRoomEPBack])
						};
					}
				case DungeonNodeID.EPBossRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.EPPastBackKeyDoor,
								RequirementDictionary.Instance[RequirementType.RedEyegoreGoriya])
						};
					}
				case DungeonNodeID.EPBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.EPBossRoom,
								RequirementDictionary.Instance[RequirementType.EPBoss])
						};
					}
				case DungeonNodeID.DPTorchItem:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.DPFront,
								RequirementDictionary.Instance[RequirementType.Torch])
						};
					}
				case DungeonNodeID.DPPastFourTorchWall:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.DP2FPastSecondKeyDoor,
								RequirementDictionary.Instance[RequirementType.FireSource])
						};
					}
				case DungeonNodeID.DPBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.DPBossRoom,
								RequirementDictionary.Instance[RequirementType.DPBoss])
						};
					}
				case DungeonNodeID.ToHBasementTorchRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.ToHPastKeyDoor,
								RequirementDictionary.Instance[RequirementType.FireSource])
						};
					}
				case DungeonNodeID.ToHPastBigKeyDoor:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.ToH,
								RequirementDictionary.Instance[RequirementType.ToHHerapot])
						};
					}
				case DungeonNodeID.ToHBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.ToHPastBigKeyDoor,
								RequirementDictionary.Instance[RequirementType.ToHBoss])
						};
					}
				case DungeonNodeID.PoDPastFirstRedGoriyaRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.PoD,
								RequirementDictionary.Instance[RequirementType.RedEyegoreGoriya]),
							new DungeonNodeConnection(
								DungeonNodeID.PoD,
								RequirementDictionary.Instance[RequirementType.CameraUnlock]),
							new DungeonNodeConnection(
								DungeonNodeID.PoD,
								RequirementDictionary.Instance[RequirementType.SBMimicClip])
						};
					}
				case DungeonNodeID.PoDLobbyArena:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.PoDPastFirstRedGoriyaRoom,
								RequirementDictionary.Instance[RequirementType.Hammer]),
							new DungeonNodeConnection(DungeonNodeID.PoDBigKeyChestArea),
							new DungeonNodeConnection(DungeonNodeID.PoDHarmlessHellwayRoom),
							new DungeonNodeConnection(
								DungeonNodeID.PoDPastSecondRedGoriyaRoom,
								RequirementDictionary.Instance[RequirementType.RedEyegoreGoriya]),
							new DungeonNodeConnection(
								DungeonNodeID.PoDPastSecondRedGoriyaRoom,
								RequirementDictionary.Instance[RequirementType.SBMimicClip])
						};
					}
				case DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.PoDDarkBasement)
						};
					}
				case DungeonNodeID.PoDDarkBasement:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
								RequirementDictionary.Instance[RequirementType.DarkRoomPoDDarkBasement])
						};
					}
				case DungeonNodeID.PoDPastDarkMazeKeyDoor:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.PoDDarkMaze)
						};
					}
				case DungeonNodeID.PoDDarkMaze:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.PoDPastDarkMazeKeyDoor,
								RequirementDictionary.Instance[RequirementType.DarkRoomPoDDarkMaze]),
							new DungeonNodeConnection(
								DungeonNodeID.PoDBigChestLedge,
								RequirementDictionary.Instance[RequirementType.DarkRoomPoDDarkMaze])
						};
					}
				case DungeonNodeID.PoDBigChestLedge:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.PoDDarkMaze),
							new DungeonNodeConnection(
								DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
								RequirementDictionary.Instance[RequirementType.BombJumpPoDHammerJump])
						};
					}
				case DungeonNodeID.PoDPastSecondRedGoriyaRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.PoDLobbyArena,
								RequirementDictionary.Instance[RequirementType.RedEyegoreGoriya]),
							new DungeonNodeConnection(
								DungeonNodeID.PoDLobbyArena,
								RequirementDictionary.Instance[RequirementType.SBMimicClip]),
							new DungeonNodeConnection(
								DungeonNodeID.PoDPastBowStatue,
								RequirementDictionary.Instance[RequirementType.Bow])
						};
					}
				case DungeonNodeID.PoDPastBowStatue:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.PoDPastSecondRedGoriyaRoom,
								RequirementDictionary.Instance[RequirementType.Bow]),
							new DungeonNodeConnection(DungeonNodeID.PoDBossAreaDarkRooms)
						};
					}
				case DungeonNodeID.PoDBossAreaDarkRooms:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.PoDPastBowStatue,
								RequirementDictionary.Instance[RequirementType.DarkRoomPoDBossArea]),
							new DungeonNodeConnection(
								DungeonNodeID.PoDPastHammerBlocks,
								RequirementDictionary.Instance[RequirementType.Hammer])
						};
					}
				case DungeonNodeID.PoDPastHammerBlocks:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.PoDBossAreaDarkRooms,
								RequirementDictionary.Instance[RequirementType.Hammer])
						};
					}
				case DungeonNodeID.PoDBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.PoDBossRoom,
								RequirementDictionary.Instance[RequirementType.PoDBoss])
						};
					}
				case DungeonNodeID.SP:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.SPAfterRiver,
								RequirementDictionary.Instance[RequirementType.Flippers])
						};
					}
				case DungeonNodeID.SPAfterRiver:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.SP,
								RequirementDictionary.Instance[RequirementType.Flippers])
						};
					}
				case DungeonNodeID.SPB1PastSecondRightKeyDoor:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.SPB1PastRightHammerBlocks)
						};
					}
				case DungeonNodeID.SPB1PastRightHammerBlocks:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.SPB1PastSecondRightKeyDoor,
								RequirementDictionary.Instance[RequirementType.Hammer]),
							new DungeonNodeConnection(DungeonNodeID.SPB1KeyLedge)
						};
					}
				case DungeonNodeID.SPB1KeyLedge:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.SPB1PastRightHammerBlocks,
								RequirementDictionary.Instance[RequirementType.Hookshot])
						};
					}
				case DungeonNodeID.SPB1Back:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.SPB1PastRightHammerBlocks,
								RequirementDictionary.Instance[RequirementType.Hookshot])
						};
					}
				case DungeonNodeID.SPBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.SPBossRoom,
								RequirementDictionary.Instance[RequirementType.SPBoss])
						};
					}
				case DungeonNodeID.SWBigChestAreaBottom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.SWBigChestAreaTop)
						};
					}
				case DungeonNodeID.SWBigChestAreaTop:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.SWBigChestAreaBottom,
								RequirementDictionary.Instance[RequirementType.Hookshot]),
							new DungeonNodeConnection(
								DungeonNodeID.SWBigChestAreaBottom,
								RequirementDictionary.Instance[RequirementType.BombJumpSWBigChest])
						};
					}
				case DungeonNodeID.SWFrontRightSide:
                    {
                        return new List<DungeonNodeConnection>
                        {
                            new DungeonNodeConnection(DungeonNodeID.SWFrontLeftSide)
                        };
                    }
				case DungeonNodeID.SWBackPastFirstKeyDoor:
                    {
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.SWBackPastFourTorchRoom)
						};
                    }
				case DungeonNodeID.SWBackPastFourTorchRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.SWBackPastFirstKeyDoor,
								RequirementDictionary.Instance[RequirementType.FireRod]),
							new DungeonNodeConnection(DungeonNodeID.SWBackPastCurtains)
						};
					}
				case DungeonNodeID.SWBackPastCurtains:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.SWBackPastFourTorchRoom,
								RequirementDictionary.Instance[RequirementType.Curtains])
						};
					}
				case DungeonNodeID.SWBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.SWBossRoom,
								RequirementDictionary.Instance[RequirementType.SWBoss])
						};
					}
				case DungeonNodeID.TTPastBigChestRoomKeyDoor:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TTPastHammerBlocks,
								RequirementDictionary.Instance[RequirementType.Hammer])
						};
					}
				case DungeonNodeID.TTPastHammerBlocks:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TTPastBigChestRoomKeyDoor,
								RequirementDictionary.Instance[RequirementType.Hammer])
						};
					}
				case DungeonNodeID.TTBossRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TTPastSecondKeyDoor,
								RequirementDictionary.Instance[RequirementType.BossShuffleOff]),
							new DungeonNodeConnection(
								DungeonNodeID.TTPastBigKeyDoor,
								RequirementDictionary.Instance[RequirementType.BossShuffleOn])
						};
					}
				case DungeonNodeID.TTBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.TTBossRoom)
						};
					}
				case DungeonNodeID.IP:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.IPPastEntranceFreezorRoom)
						};
					}
				case DungeonNodeID.IPPastEntranceFreezorRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.IP,
								RequirementDictionary.Instance[RequirementType.MeltThings])
						};
					}
				case DungeonNodeID.IPB1RightSide:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.IPB1LeftSide,
								RequirementDictionary.Instance[RequirementType.IPIceBreaker]),
							new DungeonNodeConnection(DungeonNodeID.IPB2PastLiftBlock)
						};
					}
				case DungeonNodeID.IPB2LeftSide:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.IPB1LeftSide)
						};
					}
				case DungeonNodeID.IPB2PastKeyDoor:
                    {
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.IPB4FreezorRoom)
						};
					}
				case DungeonNodeID.IPB2PastHammerBlocks:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.IPSpikeRoom,
								RequirementDictionary.Instance[RequirementType.Hammer]),
							new DungeonNodeConnection(
								DungeonNodeID.IPB2PastLiftBlock,
								RequirementDictionary.Instance[RequirementType.Gloves1])
						};
					}
				case DungeonNodeID.IPB2PastLiftBlock:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.IPB2PastHammerBlocks,
								RequirementDictionary.Instance[RequirementType.Gloves1])
						};
					}
				case DungeonNodeID.IPSpikeRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.IPB1RightSide),
							new DungeonNodeConnection(
								DungeonNodeID.IPB2PastHammerBlocks,
								RequirementDictionary.Instance[RequirementType.Hammer]),
							new DungeonNodeConnection(DungeonNodeID.IPB4RightSide)
						};
					}
				case DungeonNodeID.IPB4RightSide:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.IPSpikeRoom),
							new DungeonNodeConnection(
								DungeonNodeID.IPB4IceRoom,
								RequirementDictionary.Instance[RequirementType.Hookshot]),
							new DungeonNodeConnection(
								DungeonNodeID.IPB4IceRoom,
								RequirementDictionary.Instance[RequirementType.BombJumpIPHookshotGap])
						};
					}
				case DungeonNodeID.IPB4IceRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.IPB4FreezorRoom,
								RequirementDictionary.Instance[RequirementType.BombJumpIPFreezorRoomGap]),
							new DungeonNodeConnection(DungeonNodeID.IPB2PastKeyDoor)
						};
					}
				case DungeonNodeID.IPB4FreezorRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.IPB2PastKeyDoor),
							new DungeonNodeConnection(
								DungeonNodeID.IPB4IceRoom,
								RequirementDictionary.Instance[RequirementType.BombJumpIPFreezorRoomGap])
						};
					}
				case DungeonNodeID.IPFreezorChest:
                    {
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.IPB4FreezorRoom,
								RequirementDictionary.Instance[RequirementType.MeltThings])
						};
					}
				case DungeonNodeID.IPB4PastKeyDoor:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.IPB5)
						};
					}
				case DungeonNodeID.IPBigChestArea:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.IPB4FreezorRoom)
						};
					}
				case DungeonNodeID.IPB5:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.IPB4FreezorRoom),
							new DungeonNodeConnection(DungeonNodeID.IPB4PastKeyDoor)
						};
					}
				case DungeonNodeID.IPB6:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.IPB5,
								new AlternativeRequirement(new List<IRequirement>
								{
									RequirementDictionary.Instance[RequirementType.IPIceBreaker],
									RequirementDictionary.Instance[RequirementType.BombJumpIPBJ]
								}))
						};
					}
				case DungeonNodeID.IPB6PreBossRoom:
                    {
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.IPB6,
								new AlternativeRequirement(new List<IRequirement>
								{
									RequirementDictionary.Instance[RequirementType.CaneOfSomaria],
									RequirementDictionary.Instance[RequirementType.BombJumpIPBJ]
								})),
							new DungeonNodeConnection(DungeonNodeID.IPB6PastKeyDoor)
						};
                    }
				case DungeonNodeID.IPB6PastHammerBlocks:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.IPB6PreBossRoom,
								RequirementDictionary.Instance[RequirementType.Hammer])
						};
					}
				case DungeonNodeID.IPB6PastLiftBlock:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.IPB6PastHammerBlocks,
								RequirementDictionary.Instance[RequirementType.Gloves1])
						};
					}
				case DungeonNodeID.IPBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.IPB6PastLiftBlock,
								RequirementDictionary.Instance[RequirementType.IPBoss])
						};
					}
				case DungeonNodeID.MM:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.MMPastEntranceGap,
								new AlternativeRequirement(new List<IRequirement>
                                {
									RequirementDictionary.Instance[RequirementType.Hookshot],
									RequirementDictionary.Instance[RequirementType.BonkOverLedge]
								}))
						};
					}
				case DungeonNodeID.MMPastEntranceGap:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.MM,
								new AlternativeRequirement(new List<IRequirement>
								{
									RequirementDictionary.Instance[RequirementType.Hookshot],
									RequirementDictionary.Instance[RequirementType.BonkOverLedge]
								})),
							new DungeonNodeConnection(DungeonNodeID.MMB1PastFourTorchRoom)
						};
					}
				case DungeonNodeID.MMB1TopSide:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.MMB1PastPortalBigKeyDoor)
						};
					}
				case DungeonNodeID.MMB1LobbyBeyondBlueBlocks:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.MMB1TopSide),
							new DungeonNodeConnection(DungeonNodeID.MMB1LeftSidePastFirstKeyDoor)
						};
					}
				case DungeonNodeID.MMB1RightSideBeyondBlueBlocks:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.MMB1TopSide),
							new DungeonNodeConnection(DungeonNodeID.MMB1LeftSidePastFirstKeyDoor)
						};
					}
				case DungeonNodeID.MMB1LeftSidePastSecondKeyDoor:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.MMB1PastFourTorchRoom)
						};
					}
				case DungeonNodeID.MMB1PastFourTorchRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
								RequirementDictionary.Instance[RequirementType.FireSource])
						};
					}
				case DungeonNodeID.MMF1PastFourTorchRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
								RequirementDictionary.Instance[RequirementType.FireSource])
						};
					}
				case DungeonNodeID.MMDarkRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.MMB1PastBridgeBigKeyDoor,
								RequirementDictionary.Instance[RequirementType.DarkRoomMM])
						};
					}
				case DungeonNodeID.MMB2PastCaneOfSomariaSwitch:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.MMDarkRoom,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria])
						};
					}
				case DungeonNodeID.MMBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.MMBossRoom,
								RequirementDictionary.Instance[RequirementType.MMBoss])
						};
					}
				case DungeonNodeID.TRFront:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TRF1CompassChestArea,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria]),
							new DungeonNodeConnection(
								DungeonNodeID.TRF1FourTorchRoom,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria]),
							new DungeonNodeConnection(
								DungeonNodeID.TRF1FirstKeyDoorArea,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria])
						};
					}
				case DungeonNodeID.TRF1CompassChestArea:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TRFront,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria]),
							new DungeonNodeConnection(
								DungeonNodeID.TRF1FourTorchRoom,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria]),
							new DungeonNodeConnection(
								DungeonNodeID.TRF1FirstKeyDoorArea,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria])
						};
					}
				case DungeonNodeID.TRF1FourTorchRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TRFront,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria]),
							new DungeonNodeConnection(
								DungeonNodeID.TRF1CompassChestArea,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria]),
							new DungeonNodeConnection(DungeonNodeID.TRF1RollerRoom),
							new DungeonNodeConnection(
								DungeonNodeID.TRF1FirstKeyDoorArea,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria])
						};
					}
				case DungeonNodeID.TRF1RollerRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TRF1FourTorchRoom,
								RequirementDictionary.Instance[RequirementType.FireRod])
						};
					}
				case DungeonNodeID.TRF1FirstKeyDoorArea:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TRFront,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria]),
							new DungeonNodeConnection(
								DungeonNodeID.TRF1CompassChestArea,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria]),
							new DungeonNodeConnection(
								DungeonNodeID.TRF1FourTorchRoom,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria])
						};
					}
				case DungeonNodeID.TRF1PastSecondKeyDoor:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.TRB1)
						};
					}
				case DungeonNodeID.TRB1:
                    {
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.TRB1BigChestArea)
						};
                    }
				case DungeonNodeID.TRB1MiddleRightEntranceArea:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.TRB1)
						};
					}
				case DungeonNodeID.TRB1BigChestArea:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TRB1MiddleRightEntranceArea,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria]),
							new DungeonNodeConnection(
								DungeonNodeID.TRB1MiddleRightEntranceArea,
								RequirementDictionary.Instance[RequirementType.Hookshot])
						};
					}
				case DungeonNodeID.TRB1RightSide:
                    {
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.TRPastB1toB2KeyDoor)
						};
					}
				case DungeonNodeID.TRPastB1toB2KeyDoor:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.TRB2DarkRoomTop)
						};
					}
				case DungeonNodeID.TRB2DarkRoomTop:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TRPastB1toB2KeyDoor,
								RequirementDictionary.Instance[RequirementType.DarkRoomTR]),
							new DungeonNodeConnection(
								DungeonNodeID.TRB2DarkRoomBottom,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria])
						};
					}
				case DungeonNodeID.TRB2DarkRoomBottom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TRB2DarkRoomTop,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria]),
							new DungeonNodeConnection(
								DungeonNodeID.TRB2PastDarkMaze,
								RequirementDictionary.Instance[RequirementType.DarkRoomTR])
						};
					}
				case DungeonNodeID.TRB2PastDarkMaze:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.TRB2DarkRoomBottom)
						};
					}
				case DungeonNodeID.TRLaserBridgeChests:
                    {
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TRB2PastDarkMaze,
								RequirementDictionary.Instance[RequirementType.LaserBridge])
						};
					}
				case DungeonNodeID.TRB2PastKeyDoor:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.TRB3)
						};
					}
				case DungeonNodeID.TRB3:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.TRB2PastKeyDoor),
							new DungeonNodeConnection(
								DungeonNodeID.TRB3BossRoomEntry,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria])
						};
					}
				case DungeonNodeID.TRB3BossRoomEntry:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TRB3,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria])
						};
					}
				case DungeonNodeID.TRBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.TRBossRoom,
								RequirementDictionary.Instance[RequirementType.TRBoss])
						};
					}
				case DungeonNodeID.GT:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.GT1FLeft),
							new DungeonNodeConnection(DungeonNodeID.GT1FRight),
							new DungeonNodeConnection(
								DungeonNodeID.GT3FPastRedGoriyaRooms,
								new AlternativeRequirement(new List<IRequirement>
								{
									RequirementDictionary.Instance[RequirementType.RedEyegoreGoriya],
									RequirementDictionary.Instance[RequirementType.SBMimicClip]
								}))
						};
					}
				case DungeonNodeID.GTBobsTorch:
                    {
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT1FLeft,
								RequirementDictionary.Instance[RequirementType.Torch])
						};
					}
				case DungeonNodeID.GT1FLeft:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.GT),
							new DungeonNodeConnection(DungeonNodeID.GT1FLeftPastHammerBlocks),
							new DungeonNodeConnection(DungeonNodeID.GT1FBottomRoom)
						};
					}
				case DungeonNodeID.GT1FLeftPastHammerBlocks:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT1FLeft,
								RequirementDictionary.Instance[RequirementType.Hammer]),
							new DungeonNodeConnection(
								DungeonNodeID.GT1FLeftDMsRoom,
								new AlternativeRequirement(new List<IRequirement>
								{
									RequirementDictionary.Instance[RequirementType.Hookshot],
									RequirementDictionary.Instance[RequirementType.Hover]
								})),
							new DungeonNodeConnection(
								DungeonNodeID.GT1FLeftPastBonkableGaps,
								new AlternativeRequirement(new List<IRequirement>
								{
									RequirementDictionary.Instance[RequirementType.Hookshot],
									RequirementDictionary.Instance[RequirementType.Hover]
								}))
						};
					}
				case DungeonNodeID.GT1FLeftDMsRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT1FLeftPastHammerBlocks,
								new AlternativeRequirement(new List<IRequirement>
								{
									RequirementDictionary.Instance[RequirementType.Hookshot],
									RequirementDictionary.Instance[RequirementType.Hover]
								}))
						};
					}
				case DungeonNodeID.GT1FLeftPastBonkableGaps:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT1FLeftPastHammerBlocks,
								new AlternativeRequirement(new List<IRequirement>
								{
									RequirementDictionary.Instance[RequirementType.Hookshot],
									RequirementDictionary.Instance[RequirementType.BonkOverLedge],
									RequirementDictionary.Instance[RequirementType.Hover]
								}))
						};
					}
				case DungeonNodeID.GT1FLeftFiresnakeRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.GT1FLeftSpikeTrapPortalRoom)
						};
					}
				case DungeonNodeID.GT1FLeftPastFiresnakeRoomGap:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT1FLeftFiresnakeRoom,
								new AlternativeRequirement(new List<IRequirement>
								{
									RequirementDictionary.Instance[RequirementType.Hookshot],
									RequirementDictionary.Instance[RequirementType.Hover]
								}))
						};
					}
				case DungeonNodeID.GT1FLeftRandomizerRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor)
						};
					}
				case DungeonNodeID.GT1FRight:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.GT),
							new DungeonNodeConnection(DungeonNodeID.GT1FRightTileRoom)
						};
					}
				case DungeonNodeID.GT1FRightTileRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT1FRight,
								RequirementDictionary.Instance[RequirementType.CaneOfSomaria])
						};
					}
				case DungeonNodeID.GT1FRightFourTorchRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.GT1FRightCompassRoom)
						};
					}
				case DungeonNodeID.GT1FRightCompassRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT1FRightFourTorchRoom,
								RequirementDictionary.Instance[RequirementType.FireRod])
						};
					}
				case DungeonNodeID.GT1FRightPastCompassRoomPortal:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.GT1FRightCompassRoom)
						};
					}
				case DungeonNodeID.GT1FBottomRoom:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.GT1FLeftRandomizerRoom),
							new DungeonNodeConnection(DungeonNodeID.GT1FRightCollapsingWalkway),
							new DungeonNodeConnection(DungeonNodeID.GTBoss1)
						};
					}
				case DungeonNodeID.GTBoss1:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT1FBottomRoom,
								RequirementDictionary.Instance[RequirementType.GTBoss1])
						};
					}
				case DungeonNodeID.GTB1BossChests:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.GTBoss1)
						};
					}
				case DungeonNodeID.GT3FPastRedGoriyaRooms:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT,
								new AlternativeRequirement(new List<IRequirement>
								{
									RequirementDictionary.Instance[RequirementType.RedEyegoreGoriya],
									RequirementDictionary.Instance[RequirementType.SBMimicClip]
								}))
						};
					}
				case DungeonNodeID.GT3FPastBigKeyDoor:
                    {
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.GTBoss2)
						};
                    }
				case DungeonNodeID.GTBoss2:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT3FPastBigKeyDoor,
								RequirementDictionary.Instance[RequirementType.GTBoss2])
						};
					}
				case DungeonNodeID.GT4FPastBoss2:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(DungeonNodeID.GTBoss2)
						};
					}
				case DungeonNodeID.GT5FPastFourTorchRooms:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT4FPastBoss2,
								RequirementDictionary.Instance[RequirementType.FireSource])
						};
					}
				case DungeonNodeID.GTBoss3:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GT6FBossRoom,
								RequirementDictionary.Instance[RequirementType.GTBoss3])
						};
					}
				case DungeonNodeID.GT6FPastBossRoomGap:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GTBoss3,
								RequirementDictionary.Instance[RequirementType.Hookshot]),
							new DungeonNodeConnection(
								DungeonNodeID.GT6FBossRoom,
								RequirementDictionary.Instance[RequirementType.Hover])
						};
					}
				case DungeonNodeID.GTFinalBoss:
					{
						return new List<DungeonNodeConnection>
						{
							new DungeonNodeConnection(
								DungeonNodeID.GTFinalBossRoom,
								RequirementDictionary.Instance[RequirementType.GTFinalBoss])
						};
					}
			}

            return new List<DungeonNodeConnection>(0);
        }

		/// <summary>
		/// Returns a new dungeon node instance for the specified node ID.
		/// </summary>
		/// <param name="id">
		/// The node ID.
		/// </param>
		/// <param name="dungeonData">
		/// The mutable dungeon data parent class.
		/// </param>
		/// <param name="dungeon">
		/// The dungeon parent class.
		/// </param>
		/// <returns>
		/// A new dungeon node instance.
		/// </returns>
        internal static IDungeonNode GetDungeonNode(
            DungeonNodeID id, IMutableDungeon dungeonData, IDungeon dungeon)
        {
            if (dungeonData == null)
            {
                throw new ArgumentNullException(nameof(dungeonData));
            }

			if (dungeon == null)
            {
				throw new ArgumentNullException(nameof(dungeon));
            }

			return new DungeonNode(id, dungeonData, GetDungeonNodeFreeKeys(id),
				GetDungeonEntryConnections(id), GetDungeonKeyDoorConnections(id, dungeon),
				GetDungeonConnections(id));
        }
    }
}
