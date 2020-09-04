using OpenTracker.Models.Dungeons;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.DungeonNodes
{
	/// <summary>
	/// This is the class for creating dungeon nodes.
	/// </summary>
	public static class DungeonNodeFactory
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

		public static void PopulateNodeConnections(
			DungeonNodeID id, IRequirementNode node, IMutableDungeon dungeonData,
			List<INodeConnection> connections)
        {
			if (node == null)
			{
				throw new ArgumentNullException(nameof(node));
			}

			if (connections == null)
			{
				throw new ArgumentNullException(nameof(connections));
			}

			if (dungeonData == null)
			{
				throw new ArgumentNullException(nameof(dungeonData));
			}

            switch (id)
            {
                case DungeonNodeID.HCSanctuary:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HCSanctuaryEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCBack], node));
                    }
                    break;
                case DungeonNodeID.HCFront:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HCFrontEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.HCEscapeFirstKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCDarkRoomFront], node));
                    }
                    break;
                case DungeonNodeID.HCEscapeFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCFront], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.HCPastEscapeFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCFront], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.HCEscapeFirstKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.HCEscapeSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.HCEscapeSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.HCPastEscapeSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.HCEscapeSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.HCDarkRoomFront:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCFront], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomHC]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.HCDarkCrossKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.HCDarkCrossKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCDarkRoomFront], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.HCPastDarkCrossKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCDarkRoomFront], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.HCDarkCrossKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastSewerRatRoomKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.HCSewerRatRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.HCSewerRatRoomKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastSewerRatRoomKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.HCPastSewerRatRoomKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCDarkRoomBack], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.HCSewerRatRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.HCDarkRoomBack:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCPastSewerRatRoomKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCBack], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomHC]));
                    }
                    break;
                case DungeonNodeID.HCBack:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HCBackEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.HCDarkRoomBack], node));
                    }
                    break;
                case DungeonNodeID.AT:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ATEntry]));
                    }
                    break;
                case DungeonNodeID.ATDarkMaze:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.AT], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomAT]));
                    }
                    break;
                case DungeonNodeID.ATPastFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATDarkMaze], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.ATFirstKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATPastSecondKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.ATSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.ATSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATPastFirstKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATPastSecondKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.ATPastSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATPastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.ATSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.ATPastThirdKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATPastSecondKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.ATThirdKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATPastFourthKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.ATFourthKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.ATFourthKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATPastThirdKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATPastFourthKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.ATPastFourthKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATPastThirdKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.ATFourthKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.ATBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATPastFourthKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.Curtains]));
                    }
                    break;
                case DungeonNodeID.ATBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ATBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.ATBoss]));
                    }
                    break;
                case DungeonNodeID.EP:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EPEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EPPastBigKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.EPBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.EPBigChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EP], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.EPBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.EPRightDarkRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EP], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomEPRight]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EPPastRightKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.EPRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.EPRightKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EPRightDarkRoom], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EPPastRightKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.EPPastRightKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EPRightDarkRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.EPRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.EPBigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EP], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EPPastBigKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.EPPastBigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EP], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.EPBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.EPBackDarkRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EPPastBigKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomEPBack]));
                    }
                    break;
                case DungeonNodeID.EPPastBackKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EPBackDarkRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.EPBackKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.EPBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EPPastBackKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.RedEyegoreGoriya]));
                    }
                    break;
                case DungeonNodeID.EPBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.EPBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.EPBoss]));
                    }
                    break;
                case DungeonNodeID.DPFront:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DPFrontEntry]));
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DPLeftEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DPPastRightKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.DPRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DPTorchItem:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DPFront], node,
                            RequirementDictionary.Instance[RequirementType.Torch]));
                    }
                    break;
                case DungeonNodeID.DPBigChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DPFront], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.DPBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.DPRightKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DPFront], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DPPastRightKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.DPPastRightKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DPFront], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.DPRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DPBack:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DPBackEntry]));
                    }
                    break;
                case DungeonNodeID.DP2F:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DPBack], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.DP1FKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.DP2FFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DP2FFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DP2F], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.DP2FPastFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DP2F], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.DP2FFirstKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DP2FPastSecondKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.DP2FSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DP2FSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DP2FPastSecondKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.DP2FPastSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.DP2FSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DPPastFourTorchWall:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DP2FPastSecondKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.FireSource]));
                    }
                    break;
                case DungeonNodeID.DPBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DPPastFourTorchWall], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.DPBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DPBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.DPBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.DPBoss]));
                    }
                    break;
                case DungeonNodeID.ToH:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ToHEntry]));
                    }
                    break;
                case DungeonNodeID.ToHPastKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ToH], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.ToHKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.ToHBasementTorchRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ToHPastKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.FireSource]));
                    }
                    break;
                case DungeonNodeID.ToHPastBigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ToH], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.ToHBigKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ToH], node,
                            RequirementDictionary.Instance[RequirementType.ToHHerapot]));
                    }
                    break;
                case DungeonNodeID.ToHBigChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ToHPastBigKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.ToHBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.ToHBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.ToHPastBigKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.ToHBoss]));
                    }
                    break;
                case DungeonNodeID.PoD:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.PoDEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDFrontKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDPastFirstRedGoriyaRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoD], node,
                            RequirementDictionary.Instance[RequirementType.RedEyegoreGoriya]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoD], node,
                            RequirementDictionary.Instance[RequirementType.CameraUnlock]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoD], node,
                            RequirementDictionary.Instance[RequirementType.SBMimicClip]));
                    }
                    break;
                case DungeonNodeID.PoDFrontKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoD], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node));
                    }
                    break;
                case DungeonNodeID.PoDLobbyArena:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoD], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDFrontKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastFirstRedGoriyaRoom], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDCollapsingWalkwayKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDBigKeyChestArea:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDBigKeyChestKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDCollapsingWalkwayKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDCollapsingWalkwayKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDDarkMazeKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDHarmlessHellwayRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDHarmlessHellwayKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDDarkBasement:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomPoDDarkBasement]));
                    }
                    break;
                case DungeonNodeID.PoDHarmlessHellwayKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDHarmlessHellwayRoom], node));
                    }
                    break;
                case DungeonNodeID.PoDHarmlessHellwayRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDHarmlessHellwayKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDDarkMazeKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.PoDPastDarkMazeKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDDarkMazeKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDDarkMaze], node));
                    }
                    break;
                case DungeonNodeID.PoDDarkMaze:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomPoDDarkMaze]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDBigChestLedge], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomPoDDarkMaze]));
                    }
                    break;
                case DungeonNodeID.PoDBigChestLedge:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDDarkMaze], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.BombJumpPoDHammerJump]));
                    }
                    break;
                case DungeonNodeID.PoDBigChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDBigChestLedge], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDPastSecondRedGoriyaRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                            RequirementDictionary.Instance[RequirementType.RedEyegoreGoriya]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                            RequirementDictionary.Instance[RequirementType.SBMimicClip]));
                    }
                    break;
                case DungeonNodeID.PoDPastBowStatue:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastSecondRedGoriyaRoom], node,
                            RequirementDictionary.Instance[RequirementType.Bow]));
                    }
                    break;
                case DungeonNodeID.PoDBossAreaDarkRooms:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastBowStatue], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomPoDBossArea]));
                    }
                    break;
                case DungeonNodeID.PoDPastHammerBlocks:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDBossAreaDarkRooms], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDBossAreaKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDBossAreaKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastHammerBlocks], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.PoDPastBossAreaKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastHammerBlocks], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDBossAreaKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.PoDBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.PoDBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.PoDBoss]));
                    }
                    break;
                case DungeonNodeID.SP:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SPEntry]));
                    }
                    break;
                case DungeonNodeID.SPAfterRiver:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SP], node,
                            RequirementDictionary.Instance[RequirementType.Flippers]));
                    }
                    break;
                case DungeonNodeID.SPB1:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPAfterRiver], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SP1FKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SPB1FirstRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1FirstRightKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.SPB1PastFirstRightKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SPB1FirstRightKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastSecondRightKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SPB1SecondRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1SecondRightKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastSecondRightKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.SPB1PastSecondRightKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SPB1SecondRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1PastRightHammerBlocks:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastSecondRightKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastLeftKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SPB1LeftKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1KeyLedge:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                    }
                    break;
                case DungeonNodeID.SPB1LeftKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastLeftKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.SPB1PastLeftKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SPB1LeftKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPBigChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SPBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1Back:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SPB1BackFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1BackFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1Back], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.SPB1PastBackFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1Back], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SPB1BackFirstKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPBossRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SPBossRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPBossRoomKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPBossRoom], node));
                    }
                    break;
                case DungeonNodeID.SPBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SPBossRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SPBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.SPBoss]));
                    }
                    break;
                case DungeonNodeID.SWBigChestAreaBottom:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SWFrontEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaTop], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SWFrontLeftKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWFrontRightSide], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SWFrontRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBigChestAreaTop:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SWFrontEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                            RequirementDictionary.Instance[RequirementType.BombJumpSWBigChest]));
                    }
                    break;
                case DungeonNodeID.SWBigChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaTop], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SWBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.SWFrontLeftKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node));
                    }
                    break;
                case DungeonNodeID.SWFrontLeftSide:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SWFrontEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SWFrontLeftKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWFrontRightKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWFrontRightSide], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node));
                    }
                    break;
                case DungeonNodeID.SWFrontRightSide:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SWFrontEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SWFrontRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWFrontBackConnector:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SWFrontEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWPastTheWorthlessKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SWWorthlessKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWWorthlessKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWFrontBackConnector], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWPastTheWorthlessKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.SWPastTheWorthlessKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWFrontBackConnector], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SWWorthlessKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBack:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SWBackEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBackFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SWBackFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBackFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBack], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBackPastFirstKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.SWBackPastFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBack], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SWBackFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBackPastFourTorchRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBackPastFirstKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.FireRod]));
                    }
                    break;
                case DungeonNodeID.SWBackPastCurtains:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBackPastFourTorchRoom], node,
                            RequirementDictionary.Instance[RequirementType.Curtains]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBossRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.SWBackSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBackSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBackPastCurtains], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBossRoom], node));
                    }
                    break;
                case DungeonNodeID.SWBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBackPastCurtains], node,
                             dungeonData.KeyDoorDictionary[KeyDoorID.SWBackSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.SWBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.SWBoss]));
                    }
                    break;
                case DungeonNodeID.TT:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TTEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TTBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TTBigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TT], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.TTPastBigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TT], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TTBigKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TTFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TTFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.TTPastFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TTFirstKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TTBigChestKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TTPastSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TTSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TTBigChestKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.TTPastBigChestRoomKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TTBigChestKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TTPastHammerBlocks:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                    }
                    break;
                case DungeonNodeID.TTBigChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastHammerBlocks], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TTBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.TTBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.BossShuffleOn]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTPastSecondKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.BossShuffleOff]));
                    }
                    break;
                case DungeonNodeID.TTBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TTBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.TTBoss]));
                    }
                    break;
                case DungeonNodeID.IP:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.IPEntry]));
                    }
                    break;
                case DungeonNodeID.IPPastEntranceFreezorRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IP], node,
                            RequirementDictionary.Instance[RequirementType.MeltThings]));
                    }
                    break;
                case DungeonNodeID.IPB1LeftSide:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPPastEntranceFreezorRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IP1FKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.IPB1RightSide:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2PastLiftBlock], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB1LeftSide], node,
                            RequirementDictionary.Instance[RequirementType.IPIceBreaker]));
                    }
                    break;
                case DungeonNodeID.IPB2LeftSide:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB1LeftSide], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IPB2KeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.IPB2KeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2LeftSide], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.IPB2PastKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2LeftSide], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IPB2KeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IPB3KeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node));
                    }
                    break;
                case DungeonNodeID.IPB2PastHammerBlocks:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2PastLiftBlock], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                    }
                    break;
                case DungeonNodeID.IPB2PastLiftBlock:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2PastHammerBlocks], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case DungeonNodeID.IPB3KeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node));
                    }
                    break;
                case DungeonNodeID.IPSpikeRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB1RightSide], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2PastHammerBlocks], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IPB3KeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4RightSide], node));
                    }
                    break;
                case DungeonNodeID.IPB4RightSide:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            RequirementDictionary.Instance[RequirementType.BombJumpIPHookshotGap]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            RequirementDictionary.Instance[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.IPB4IceRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IPB4KeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                            RequirementDictionary.Instance[RequirementType.BombJumpIPFreezorRoomGap]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                            RequirementDictionary.Instance[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.IPB4FreezorRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            RequirementDictionary.Instance[RequirementType.BombJumpIPFreezorRoomGap]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            RequirementDictionary.Instance[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.IPFreezorChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                            RequirementDictionary.Instance[RequirementType.MeltThings]));
                    }
                    break;
                case DungeonNodeID.IPB4KeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.IPB4PastKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IPB4KeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB5], node));
                    }
                    break;
                case DungeonNodeID.IPBigChestArea:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node));
                    }
                    break;
                case DungeonNodeID.IPBigChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPBigChestArea], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IPBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.IPB5:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.IPB5PastBigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB5], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IPBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.IPB6:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB5PastBigKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IPB5KeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB6PastKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IPB6KeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB5], node,
                            RequirementDictionary.Instance[RequirementType.BombJumpIPBJ]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB5], node,
                            RequirementDictionary.Instance[RequirementType.IPIceBreaker]));
                    }
                    break;
                case DungeonNodeID.IPB6KeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB6], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB6PastKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.IPB6PastKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB6], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.IPB6KeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.IPB6PreBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB6], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB6PastKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB6], node,
                            RequirementDictionary.Instance[RequirementType.BombJumpIPBJ]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB6], node,
                            RequirementDictionary.Instance[RequirementType.IPIceBreaker]));
                    }
                    break;
                case DungeonNodeID.IPB6PastHammerBlocks:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB6PreBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                    }
                    break;
                case DungeonNodeID.IPB6PastLiftBlock:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB6PastHammerBlocks], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case DungeonNodeID.IPBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.IPB6PastLiftBlock], node,
                            RequirementDictionary.Instance[RequirementType.IPBoss]));
                    }
                    break;
                case DungeonNodeID.MM:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MMEntry]));
                    }
                    break;
                case DungeonNodeID.MMPastEntranceGap:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MM], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MM], node,
                            RequirementDictionary.Instance[RequirementType.BonkOverLedge]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MM], node,
                            RequirementDictionary.Instance[RequirementType.Hover]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB1TopLeftKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB1TopRightKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB1LeftSideFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMBigChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1TopLeftKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node));
                    }
                    break;
                case DungeonNodeID.MMB1TopRightKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node));
                    }
                    break;
                case DungeonNodeID.MMB1TopSide:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB1TopLeftKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB1TopRightKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1PastPortalBigKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1PastBridgeBigKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMBridgeBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1LobbyBeyondBlueBlocks:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1RightSideBeyondBlueBlocks], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB1RightSideKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1RightSideKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1LobbyBeyondBlueBlocks], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1RightSideBeyondBlueBlocks], node));
                    }
                    break;
                case DungeonNodeID.MMB1RightSideBeyondBlueBlocks:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1LeftSideFirstKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1LobbyBeyondBlueBlocks], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB1RightSideKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1LeftSideFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.MMB1LeftSidePastFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB1LeftSideFirstKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastSecondKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB1LeftSideSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1LeftSideSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastSecondKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.MMB1LeftSidePastSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB1LeftSideSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1PastFourTorchRoom:
                case DungeonNodeID.MMF1PastFourTorchRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastSecondKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.FireSource]));
                    }
                    break;
                case DungeonNodeID.MMB1PastPortalBigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMPortalBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMBridgeBigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1PastBridgeBigKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.MMB1PastBridgeBigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMBridgeBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMDarkRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB2PastWorthlessKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB2WorthlessKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB1PastBridgeBigKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomMM]));
                    }
                    break;
                case DungeonNodeID.MMB2WorthlessKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMDarkRoom], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB2PastWorthlessKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.MMB2PastWorthlessKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMDarkRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMB2WorthlessKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB2PastCaneOfSomariaSwitch:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMDarkRoom], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                    }
                    break;
                case DungeonNodeID.MMBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMB2PastCaneOfSomariaSwitch], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.MMBossRoomBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.MMBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.MMBoss]));
                    }
                    break;
                case DungeonNodeID.TRFront:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TRFrontEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1SomariaTrack], node));
                    }
                    break;
                case DungeonNodeID.TRF1SomariaTrack:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRFront], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1CompassChestArea], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1FourTorchRoom], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1FirstKeyDoorArea], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                    }
                    break;
                case DungeonNodeID.TRF1CompassChestArea:
                case DungeonNodeID.TRF1FourTorchRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1SomariaTrack], node));
                    }
                    break;
                case DungeonNodeID.TRF1RollerRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1FourTorchRoom], node,
                            RequirementDictionary.Instance[RequirementType.FireRod]));
                    }
                    break;
                case DungeonNodeID.TRF1FirstKeyDoorArea:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1SomariaTrack], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TR1FFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRF1FirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1FirstKeyDoorArea], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.TRF1PastFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1FirstKeyDoorArea], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TR1FFirstKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1PastSecondKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TR1FSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRF1SecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1PastSecondKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.TRF1PastSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TR1FSecondKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1], node));
                    }
                    break;
                case DungeonNodeID.TRB1:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TRMiddleEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRF1PastSecondKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TR1FThirdKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1BigChestArea], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1RightSide], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TRB1BigKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1PastBigKeyChestKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TRB1BigKeyChestKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRB1BigKeyChestKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1PastBigKeyChestKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.TRB1PastBigKeyChestKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TRB1BigKeyChestKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRB1MiddleRightEntranceArea:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TRMiddleEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1], node));
                    }
                    break;
                case DungeonNodeID.TRB1BigChestArea:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1MiddleRightEntranceArea], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1MiddleRightEntranceArea], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1MiddleRightEntranceArea], node,
                            RequirementDictionary.Instance[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.TRBigChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1BigChestArea], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TRBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.TRB1BigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1RightSide], node));
                    }
                    break;
                case DungeonNodeID.TRB1RightSide:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TRB1BigKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRPastB1toB2KeyDoor], node));
                    }
                    break;
                case DungeonNodeID.TRPastB1toB2KeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB1RightSide], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TRB1toB2KeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomTop], node));
                    }
                    break;
                case DungeonNodeID.TRB2DarkRoomTop:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRPastB1toB2KeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomTR]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomBottom], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                    }
                    break;
                case DungeonNodeID.TRB2DarkRoomBottom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomTop], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomTR]));
                    }
                    break;
                case DungeonNodeID.TRB2PastDarkMaze:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TRBackEntry]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomBottom], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB2PastKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TRB2KeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRLaserBridgeChests:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze], node,
                            RequirementDictionary.Instance[RequirementType.LaserBridge]));
                    }
                    break;
                case DungeonNodeID.TRB2KeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB2PastKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.TRB2PastKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TRB2KeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRB3:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB2PastKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.TRB3BossRoomEntry:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB3], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                    }
                    break;
                case DungeonNodeID.TRBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRB3BossRoomEntry], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.TRBossRoomBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.TRBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.TRBoss]));
                    }
                    break;
                case DungeonNodeID.GT:
                    {
                        connections.Add(new EntryNodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.GTEntry]));
                    }
                    break;
                case DungeonNodeID.GTBobsTorch:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                            RequirementDictionary.Instance[RequirementType.Torch]));
                    }
                    break;
                case DungeonNodeID.GT1FLeft:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRight], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FLeftToRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FLeftToRightKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeft], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRight], node));
                    }
                    break;
                case DungeonNodeID.GT1FLeftPastHammerBlocks:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                    }
                    break;
                case DungeonNodeID.GT1FLeftDMsRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                            RequirementDictionary.Instance[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.GT1FLeftPastBonkableGaps:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                            RequirementDictionary.Instance[RequirementType.BonkOverLedge]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                            RequirementDictionary.Instance[RequirementType.Hover]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftMapChestRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FMapChestRoomKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FMapChestRoomKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftMapChestRoom], node));
                    }
                    break;
                case DungeonNodeID.GT1FLeftMapChestRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FMapChestRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FSpikeTrapPortalRoomKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom], node));
                    }
                    break;
                case DungeonNodeID.GT1FLeftSpikeTrapPortalRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FLeftFiresnakeRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom], node));
                    }
                    break;
                case DungeonNodeID.GT1FLeftPastFiresnakeRoomGap:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftFiresnakeRoom], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FFiresnakeRoomKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftFiresnakeRoom], node,
                            RequirementDictionary.Instance[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.GT1FFiresnakeRoomKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomGap], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomGap], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FFiresnakeRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FLeftRandomizerRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.GT1FRight:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FLeftToRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FRightTileRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRight], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FTileRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FTileRoomKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRightTileRoom], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom], node));
                    }
                    break;
                case DungeonNodeID.GT1FRightFourTorchRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRightTileRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FTileRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FRightCompassRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom], node,
                            RequirementDictionary.Instance[RequirementType.FireRod]));
                    }
                    break;
                case DungeonNodeID.GT1FRightPastCompassRoomPortal:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRightCompassRoom], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FCollapsingWalkwayKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FCollapsingWalkwayKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRightPastCompassRoomPortal], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway], node));
                    }
                    break;
                case DungeonNodeID.GT1FRightCollapsingWalkway:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRightPastCompassRoomPortal], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT1FCollapsingWalkwayKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FBottomRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FLeftRandomizerRoom], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway], node));
                    }
                    break;
                case DungeonNodeID.GTBoss1:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FBottomRoom], node,
                            RequirementDictionary.Instance[RequirementType.GTBoss1]));
                    }
                    break;
                case DungeonNodeID.GTB1BossChests:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GTBoss1], node));
                    }
                    break;
                case DungeonNodeID.GTBigChest:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT1FBottomRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GTBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.GT3FPastRedGoriyaRooms:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT], node,
                            RequirementDictionary.Instance[RequirementType.RedEyegoreGoriya]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT3FBigKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT], node,
                            RequirementDictionary.Instance[RequirementType.SBMimicClip]));
                    }
                    break;
                case DungeonNodeID.GT3FBigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT3FPastRedGoriyaRooms], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.GT3FPastBigKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT3FPastRedGoriyaRooms], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT3FBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GTBoss2:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor], node,
                            RequirementDictionary.Instance[RequirementType.GTBoss2]));
                    }
                    break;
                case DungeonNodeID.GT4FPastBoss2:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GTBoss2], node));
                    }
                    break;
                case DungeonNodeID.GT5FPastFourTorchRooms:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT4FPastBoss2], node,
                            RequirementDictionary.Instance[RequirementType.FireSource]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT6FFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT6FFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT5FPastFourTorchRooms], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node));
                    }
                    break;
                case DungeonNodeID.GT6FPastFirstKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT5FPastFourTorchRooms], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT6FFirstKeyDoor].Requirement));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT6FSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT6FSecondKeyDoor:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node));
                    }
                    break;
                case DungeonNodeID.GT6FBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT6FSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GTBoss3:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.GTBoss3]));
                    }
                    break;
                case DungeonNodeID.GTBoss3Item:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GTBoss3], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GTBoss3], node,
                            RequirementDictionary.Instance[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.GT6FPastBossRoomGap:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GTBoss3Item], node));
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.GTFinalBossRoom:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GT6FPastBossRoomGap], node,
                            dungeonData.KeyDoorDictionary[KeyDoorID.GT7FBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GTFinalBoss:
                    {
                        connections.Add(new NodeConnection(dungeonData.Nodes[DungeonNodeID.GTFinalBossRoom], node,
                            RequirementDictionary.Instance[RequirementType.GTFinalBoss]));
                    }
                    break;
            }
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
        public static IDungeonNode GetDungeonNode(
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

			return new DungeonNode(id, dungeonData, GetDungeonNodeFreeKeys(id));
        }
    }
}
