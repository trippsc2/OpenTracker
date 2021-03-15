using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This class contains the creation logic for dungeon nodes.
    /// </summary>
    public class DungeonNodeFactory : IDungeonNodeFactory
    {
        private readonly IRequirementDictionary _requirements;
        private readonly IRequirementNodeDictionary _requirementNodes;

        private readonly EntryNodeConnection.Factory _entryFactory;
        private readonly NodeConnection.Factory _connectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        /// The requirement dictionary.
        /// </param>
        /// <param name="requirementNodes">
        /// The requirement node dictionary.
        /// </param>
        /// <param name="entryFactory">
        /// An Autofac factory for creating entry node connections.
        /// </param>
        /// <param name="connectionFactory">
        /// An Autofac factory for creating regular node connections.
        /// </param>
        public DungeonNodeFactory(
            IRequirementDictionary requirements, IRequirementNodeDictionary requirementNodes,
            EntryNodeConnection.Factory entryFactory, NodeConnection.Factory connectionFactory)
        {
            _requirements = requirements;
            _requirementNodes = requirementNodes;

            _entryFactory = entryFactory;
            _connectionFactory = connectionFactory;
        }
        
        /// <summary>
        /// Populates the dungeon node connections.
        /// </summary>
        /// <param name="id">
        /// The dungeon node ID.
        /// </param>
        /// <param name="node">
        /// The dungeon node.
        /// </param>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        /// <param name="connections">
        /// The list of connections to be populated.
        /// </param>
		public void PopulateNodeConnections(
			IMutableDungeon dungeonData, DungeonNodeID id, IRequirementNode node,
            List<INodeConnection> connections)
        {
            switch (id)
            {
                case DungeonNodeID.HCSanctuary:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.HCSanctuaryEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCBack],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.HCFront:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.HCFrontEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.HCEscapeFirstKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCDarkRoomFront],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.HCEscapeFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCFront],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.HCPastEscapeFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCFront], node,
                            dungeonData.KeyDoors[KeyDoorID.HCEscapeFirstKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.HCEscapeSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.HCEscapeSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.HCPastEscapeSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.HCEscapeSecondKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCZeldasCell], node,
                            dungeonData.KeyDoors[KeyDoorID.HCZeldasCellDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.HCZeldasCellDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCZeldasCell],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.HCZeldasCell:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.HCZeldasCellDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.HCDarkRoomFront:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCFront], node,
                            _requirements[RequirementType.DarkRoomHC]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.HCDarkCrossKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.HCDarkCrossKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCDarkRoomFront],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.HCPastDarkCrossKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCDarkRoomFront], node,
                            dungeonData.KeyDoors[KeyDoorID.HCDarkCrossKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastSewerRatRoomKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.HCSewerRatRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.HCSewerRatRoomKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastSewerRatRoomKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.HCPastSewerRatRoomKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCDarkRoomBack],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.HCSewerRatRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.HCDarkRoomBack:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCPastSewerRatRoomKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCBack], node,
                            _requirements[RequirementType.DarkRoomHC]));
                    }
                    break;
                case DungeonNodeID.HCBack:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.HCBackEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCDarkRoomBack],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.AT:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.ATEntry]));
                    }
                    break;
                case DungeonNodeID.ATDarkMaze:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.AT], node,
                            _requirements[RequirementType.DarkRoomAT]));
                    }
                    break;
                case DungeonNodeID.ATPastFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATDarkMaze], node,
                            dungeonData.KeyDoors[KeyDoorID.ATFirstKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATPastSecondKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.ATSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.ATSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATPastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATPastSecondKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.ATPastSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATPastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.ATSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.ATPastThirdKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATPastSecondKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.ATThirdKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATPastFourthKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.ATFourthKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.ATFourthKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATPastThirdKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATPastFourthKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.ATPastFourthKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATPastThirdKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.ATFourthKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.ATBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATPastFourthKeyDoor], node,
                            _requirements[RequirementType.Curtains]));
                    }
                    break;
                case DungeonNodeID.ATBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ATBossRoom], node,
                            _requirements[RequirementType.ATBoss]));
                    }
                    break;
                case DungeonNodeID.EP:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.EPEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EPPastBigKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.EPBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.EPBigChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EP], node,
                            dungeonData.KeyDoors[KeyDoorID.EPBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.EPRightDarkRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EP], node,
                            _requirements[RequirementType.DarkRoomEPRight]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EPPastRightKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.EPRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.EPRightKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EPRightDarkRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EPPastRightKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.EPPastRightKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EPRightDarkRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.EPRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.EPBigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EP],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EPPastBigKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.EPPastBigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EP], node,
                            dungeonData.KeyDoors[KeyDoorID.EPBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.EPBackDarkRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EPPastBigKeyDoor], node,
                            _requirements[RequirementType.DarkRoomEPBack]));
                    }
                    break;
                case DungeonNodeID.EPPastBackKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EPBackDarkRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.EPBackKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.EPBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EPPastBackKeyDoor], node,
                            _requirements[RequirementType.RedEyegoreGoriya]));
                    }
                    break;
                case DungeonNodeID.EPBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.EPBossRoom], node,
                            _requirements[RequirementType.EPBoss]));
                    }
                    break;
                case DungeonNodeID.DPFront:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.DPFrontEntry]));
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.DPLeftEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DPPastRightKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.DPRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DPTorch:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DPFront], node,
                            _requirements[RequirementType.Torch]));
                    }
                    break;
                case DungeonNodeID.DPBigChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DPFront], node,
                            dungeonData.KeyDoors[KeyDoorID.DPBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.DPRightKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DPFront],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DPPastRightKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.DPPastRightKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DPFront], node,
                            dungeonData.KeyDoors[KeyDoorID.DPRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DPBack:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.DPBackEntry]));
                    }
                    break;
                case DungeonNodeID.DP2F:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DPBack], node,
                            dungeonData.KeyDoors[KeyDoorID.DP1FKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.DP2FFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DP2FFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DP2F],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.DP2FPastFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DP2F], node,
                            dungeonData.KeyDoors[KeyDoorID.DP2FFirstKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DP2FPastSecondKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.DP2FSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DP2FSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DP2FPastSecondKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.DP2FPastSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.DP2FSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DPPastFourTorchWall:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DP2FPastSecondKeyDoor], node,
                            _requirements[RequirementType.FireSource]));
                    }
                    break;
                case DungeonNodeID.DPBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DPPastFourTorchWall], node,
                            dungeonData.KeyDoors[KeyDoorID.DPBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.DPBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.DPBossRoom], node,
                            _requirements[RequirementType.DPBoss]));
                    }
                    break;
                case DungeonNodeID.ToH:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.ToHEntry]));
                    }
                    break;
                case DungeonNodeID.ToHPastKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ToH], node,
                            dungeonData.KeyDoors[KeyDoorID.ToHKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.ToHBasementTorchRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ToHPastKeyDoor], node,
                            _requirements[RequirementType.FireSource]));
                    }
                    break;
                case DungeonNodeID.ToHPastBigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ToH], node,
                            dungeonData.KeyDoors[KeyDoorID.ToHBigKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ToH], node,
                            _requirements[RequirementType.ToHHerapot]));
                    }
                    break;
                case DungeonNodeID.ToHBigChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ToHPastBigKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.ToHBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.ToHBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ToHPastBigKeyDoor], node,
                            _requirements[RequirementType.ToHBoss]));
                    }
                    break;
                case DungeonNodeID.PoD:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.PoDEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDFrontKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDPastFirstRedGoriyaRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoD], node,
                            _requirements[RequirementType.RedEyegoreGoriya]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoD], node,
                            _requirements[RequirementType.CameraUnlock]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoD], node,
                            _requirements[RequirementType.MimicClip]));
                    }
                    break;
                case DungeonNodeID.PoDFrontKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoD],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.PoDLobbyArena:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoD], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDFrontKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastFirstRedGoriyaRoom], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDCollapsingWalkwayKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDBigKeyChestArea:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDBigKeyChestKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDCollapsingWalkwayKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDCollapsingWalkwayKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDDarkMazeKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDHarmlessHellwayRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDHarmlessHellwayKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDDarkBasement:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                            _requirements[RequirementType.DarkRoomPoDDarkBasement]));
                    }
                    break;
                case DungeonNodeID.PoDHarmlessHellwayKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDHarmlessHellwayRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.PoDHarmlessHellwayRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDHarmlessHellwayKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDDarkMazeKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.PoDPastDarkMazeKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDDarkMazeKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDDarkMaze],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.PoDDarkMaze:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor], node,
                            _requirements[RequirementType.DarkRoomPoDDarkMaze]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDBigChestLedge], node,
                            _requirements[RequirementType.DarkRoomPoDDarkMaze]));
                    }
                    break;
                case DungeonNodeID.PoDBigChestLedge:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDDarkMaze],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                            _requirements[RequirementType.BombJumpPoDHammerJump]));
                    }
                    break;
                case DungeonNodeID.PoDBigChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDBigChestLedge], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDPastSecondRedGoriyaRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                            _requirements[RequirementType.RedEyegoreGoriya]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                            _requirements[RequirementType.MimicClip]));
                    }
                    break;
                case DungeonNodeID.PoDPastBowStatue:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastSecondRedGoriyaRoom], node,
                            _requirements[RequirementType.Bow]));
                    }
                    break;
                case DungeonNodeID.PoDBossAreaDarkRooms:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastBowStatue], node,
                            _requirements[RequirementType.DarkRoomPoDBossArea]));
                    }
                    break;
                case DungeonNodeID.PoDPastHammerBlocks:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDBossAreaDarkRooms], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDBossAreaKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDBossAreaKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastHammerBlocks],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.PoDPastBossAreaKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastHammerBlocks], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDBossAreaKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.PoDBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.PoDBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.PoDBossRoom], node,
                            _requirements[RequirementType.PoDBoss]));
                    }
                    break;
                case DungeonNodeID.SP:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.SPEntry]));
                    }
                    break;
                case DungeonNodeID.SPAfterRiver:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SP], node,
                            _requirements[RequirementType.Flippers]));
                    }
                    break;
                case DungeonNodeID.SPB1:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPAfterRiver], node,
                            dungeonData.KeyDoors[KeyDoorID.SP1FKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.SPB1FirstRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1FirstRightKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.SPB1PastFirstRightKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1], node,
                            dungeonData.KeyDoors[KeyDoorID.SPB1FirstRightKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastSecondRightKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.SPB1SecondRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1SecondRightKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastSecondRightKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.SPB1PastSecondRightKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.SPB1SecondRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1PastRightHammerBlocks:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastSecondRightKeyDoor], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastLeftKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.SPB1LeftKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1KeyLedge:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                            _requirements[RequirementType.Hookshot]));
                    }
                    break;
                case DungeonNodeID.SPB1LeftKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastLeftKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.SPB1PastLeftKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                            dungeonData.KeyDoors[KeyDoorID.SPB1LeftKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPBigChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                            dungeonData.KeyDoors[KeyDoorID.SPBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1Back:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.SPB1BackFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPB1BackFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1Back],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.SPB1PastBackFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1Back], node,
                            dungeonData.KeyDoors[KeyDoorID.SPB1BackFirstKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPBossRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.SPBossRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPBossRoomKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPBossRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.SPBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.SPBossRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SPBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SPBossRoom], node,
                            _requirements[RequirementType.SPBoss]));
                    }
                    break;
                case DungeonNodeID.SWBigChestAreaBottom:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.SWFrontEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaTop],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide], node,
                            dungeonData.KeyDoors[KeyDoorID.SWFrontLeftKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWFrontRightSide], node,
                            dungeonData.KeyDoors[KeyDoorID.SWFrontRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBigChestAreaTop:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.SWFrontEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                            _requirements[RequirementType.BombJumpSWBigChest]));
                    }
                    break;
                case DungeonNodeID.SWBigChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaTop], node,
                            dungeonData.KeyDoors[KeyDoorID.SWBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.SWFrontLeftKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.SWFrontLeftSide:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.SWFrontEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                            dungeonData.KeyDoors[KeyDoorID.SWFrontLeftKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWFrontRightKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWFrontRightSide],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.SWFrontRightSide:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.SWFrontEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                            dungeonData.KeyDoors[KeyDoorID.SWFrontRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWFrontBackConnector:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.SWFrontEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWPastTheWorthlessKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.SWWorthlessKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWWorthlessKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWFrontBackConnector],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWPastTheWorthlessKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.SWPastTheWorthlessKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWFrontBackConnector], node,
                            dungeonData.KeyDoors[KeyDoorID.SWWorthlessKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBack:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.SWBackEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBackFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.SWBackFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBackFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBack],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBackPastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.SWBackPastFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBack], node,
                            dungeonData.KeyDoors[KeyDoorID.SWBackFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBackPastFourTorchRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBackPastFirstKeyDoor], node,
                            _requirements[RequirementType.FireRod]));
                    }
                    break;
                case DungeonNodeID.SWBackPastCurtains:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBackPastFourTorchRoom], node,
                            _requirements[RequirementType.Curtains]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBossRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.SWBackSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBackSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBackPastCurtains],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBossRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.SWBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBackPastCurtains], node,
                             dungeonData.KeyDoors[KeyDoorID.SWBackSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.SWBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.SWBossRoom], node,
                            _requirements[RequirementType.SWBoss]));
                    }
                    break;
                case DungeonNodeID.TT:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.TTEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TTBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TTBigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TT],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TTPastBigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TT], node,
                            dungeonData.KeyDoors[KeyDoorID.TTBigKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TTFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TTFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TTPastFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TTFirstKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TTBigChestKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TTPastSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TTSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TTBigChestKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TTPastBigChestRoomKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TTBigChestKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TTPastHammerBlocks:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case DungeonNodeID.TTBigChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastHammerBlocks], node,
                            dungeonData.KeyDoors[KeyDoorID.TTBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.TTBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                            _requirements[RequirementType.BossShuffleOn]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTPastSecondKeyDoor], node,
                            _requirements[RequirementType.BossShuffleOff]));
                    }
                    break;
                case DungeonNodeID.TTBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TTBossRoom], node,
                            _requirements[RequirementType.TTBoss]));
                    }
                    break;
                case DungeonNodeID.IP:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.IPEntry]));
                    }
                    break;
                case DungeonNodeID.IPPastEntranceFreezorRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IP], node,
                            _requirements[RequirementType.MeltThings]));
                    }
                    break;
                case DungeonNodeID.IPB1LeftSide:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPPastEntranceFreezorRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.IP1FKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.IPB1RightSide:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB2PastLiftBlock],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB1LeftSide], node,
                            _requirements[RequirementType.IPIceBreaker]));
                    }
                    break;
                case DungeonNodeID.IPB2LeftSide:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB1LeftSide],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.IPB2KeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.IPB2KeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB2LeftSide],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.IPB2PastKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB2LeftSide], node,
                            dungeonData.KeyDoors[KeyDoorID.IPB2KeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.IPB3KeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.IPB2PastHammerBlocks:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case DungeonNodeID.IPB2PastLiftBlock:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB2PastHammerBlocks], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case DungeonNodeID.IPB3KeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPSpikeRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.IPSpikeRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB1RightSide],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.IPB3KeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4RightSide],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.IPB4RightSide:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPSpikeRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            _requirements[RequirementType.BombJumpIPHookshotGap]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            _requirements[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.IPB4IceRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.IPB4KeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                            _requirements[RequirementType.BombJumpIPFreezorRoomGap]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                            _requirements[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.IPB4FreezorRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            _requirements[RequirementType.BombJumpIPFreezorRoomGap]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            _requirements[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.IPFreezorChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                            _requirements[RequirementType.MeltThings]));
                    }
                    break;
                case DungeonNodeID.IPB4KeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.IPB4PastKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.IPB4KeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB5],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.IPBigChestArea:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.IPBigChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPBigChestArea], node,
                            dungeonData.KeyDoors[KeyDoorID.IPBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.IPB5:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.IPB5PastBigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB5], node,
                            dungeonData.KeyDoors[KeyDoorID.IPBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.IPB6:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB5PastBigKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.IPB5KeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB6PastKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.IPB6KeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB5], node,
                            _requirements[RequirementType.BombJumpIPBJ]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB5], node,
                            _requirements[RequirementType.IPIceBreaker]));
                    }
                    break;
                case DungeonNodeID.IPB6KeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB6],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB6PastKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.IPB6PastKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB6], node,
                            dungeonData.KeyDoors[KeyDoorID.IPB6KeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.IPB6PreBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB6], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB6PastKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB6], node,
                            _requirements[RequirementType.BombJumpIPBJ]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB6], node,
                            _requirements[RequirementType.IPIceBreaker]));
                    }
                    break;
                case DungeonNodeID.IPB6PastHammerBlocks:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB6PreBossRoom], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case DungeonNodeID.IPB6PastLiftBlock:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB6PastHammerBlocks], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case DungeonNodeID.IPBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.IPB6PastLiftBlock], node,
                            _requirements[RequirementType.IPBoss]));
                    }
                    break;
                case DungeonNodeID.MM:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.MMEntry]));
                    }
                    break;
                case DungeonNodeID.MMPastEntranceGap:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MM], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MM], node,
                            _requirements[RequirementType.BonkOverLedge]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MM], node,
                            _requirements[RequirementType.Hover]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB1TopLeftKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB1TopRightKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB1LeftSideFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMBigChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                            dungeonData.KeyDoors[KeyDoorID.MMBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1TopLeftKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1TopSide],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.MMB1TopRightKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1TopSide],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.MMB1TopSide:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB1TopLeftKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB1TopRightKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1PastPortalBigKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1PastBridgeBigKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.MMBridgeBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1LobbyBeyondBlueBlocks:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1TopSide],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1RightSideBeyondBlueBlocks], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB1RightSideKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1RightSideKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1LobbyBeyondBlueBlocks],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1RightSideBeyondBlueBlocks],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.MMB1RightSideBeyondBlueBlocks:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1LeftSideFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1TopSide],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1LobbyBeyondBlueBlocks], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB1RightSideKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1LeftSideFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.MMB1LeftSidePastFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB1LeftSideFirstKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastSecondKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB1LeftSideSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1LeftSideSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastSecondKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.MMB1LeftSidePastSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB1LeftSideSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB1PastFourTorchRoom:
                case DungeonNodeID.MMF1PastFourTorchRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastSecondKeyDoor], node,
                            _requirements[RequirementType.FireSource]));
                    }
                    break;
                case DungeonNodeID.MMB1PastPortalBigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                            dungeonData.KeyDoors[KeyDoorID.MMPortalBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMBridgeBigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1TopSide],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1PastBridgeBigKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.MMB1PastBridgeBigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                            dungeonData.KeyDoors[KeyDoorID.MMBridgeBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMDarkRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB2PastWorthlessKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB2WorthlessKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB1PastBridgeBigKeyDoor], node,
                            _requirements[RequirementType.DarkRoomMM]));
                    }
                    break;
                case DungeonNodeID.MMB2WorthlessKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMDarkRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB2PastWorthlessKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.MMB2PastWorthlessKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMDarkRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.MMB2WorthlessKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMB2PastCaneOfSomariaSwitch:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMDarkRoom], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                    }
                    break;
                case DungeonNodeID.MMBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMB2PastCaneOfSomariaSwitch], node,
                            dungeonData.KeyDoors[KeyDoorID.MMBossRoomBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.MMBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.MMBossRoom], node,
                            _requirements[RequirementType.MMBoss]));
                    }
                    break;
                case DungeonNodeID.TRFront:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.TRFrontEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1SomariaTrack],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRF1SomariaTrack:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRFront], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1CompassChestArea], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1FourTorchRoom], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1FirstKeyDoorArea], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                    }
                    break;
                case DungeonNodeID.TRF1CompassChestArea:
                case DungeonNodeID.TRF1FourTorchRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1SomariaTrack],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRF1RollerRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1FourTorchRoom], node,
                            _requirements[RequirementType.FireRod]));
                    }
                    break;
                case DungeonNodeID.TRF1FirstKeyDoorArea:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1SomariaTrack],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TR1FFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRF1FirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1FirstKeyDoorArea],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRF1PastFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1FirstKeyDoorArea], node,
                            dungeonData.KeyDoors[KeyDoorID.TR1FFirstKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1PastSecondKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TR1FSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRF1SecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1PastSecondKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRF1PastSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TR1FSecondKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRB1:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.TRMiddleEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRF1PastSecondKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TR1FThirdKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1BigChestArea],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1RightSide], node,
                            dungeonData.KeyDoors[KeyDoorID.TRB1BigKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1PastBigKeyChestKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TRB1BigKeyChestKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRB1BigKeyChestKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1PastBigKeyChestKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRB1PastBigKeyChestKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1], node,
                            dungeonData.KeyDoors[KeyDoorID.TRB1BigKeyChestKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRB1MiddleRightEntranceArea:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.TRMiddleEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRB1BigChestArea:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1MiddleRightEntranceArea], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1MiddleRightEntranceArea], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1MiddleRightEntranceArea], node,
                            _requirements[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.TRBigChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1BigChestArea], node,
                            dungeonData.KeyDoors[KeyDoorID.TRBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.TRB1BigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1RightSide],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRB1RightSide:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1], node,
                            dungeonData.KeyDoors[KeyDoorID.TRB1BigKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRPastB1toB2KeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRPastB1toB2KeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB1RightSide], node,
                            dungeonData.KeyDoors[KeyDoorID.TRB1toB2KeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomTop],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRB2DarkRoomTop:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRPastB1toB2KeyDoor], node,
                            _requirements[RequirementType.DarkRoomTR]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomBottom], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                    }
                    break;
                case DungeonNodeID.TRB2DarkRoomBottom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomTop], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze], node,
                            _requirements[RequirementType.DarkRoomTR]));
                    }
                    break;
                case DungeonNodeID.TRB2PastDarkMaze:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.TRBackEntry]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomBottom],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB2PastKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.TRB2KeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRLaserBridgeChests:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze], node,
                            _requirements[RequirementType.LaserBridge]));
                    }
                    break;
                case DungeonNodeID.TRB2KeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB2PastKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRB2PastKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze], node,
                            dungeonData.KeyDoors[KeyDoorID.TRB2KeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRB3:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB2PastKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.TRB3BossRoomEntry:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB3], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                    }
                    break;
                case DungeonNodeID.TRBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRB3BossRoomEntry], node,
                            dungeonData.KeyDoors[KeyDoorID.TRBossRoomBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.TRBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.TRBossRoom], node,
                            _requirements[RequirementType.TRBoss]));
                    }
                    break;
                case DungeonNodeID.GT:
                    {
                        connections.Add(_entryFactory(
                            _requirementNodes[RequirementNodeID.GTEntry]));
                    }
                    break;
                case DungeonNodeID.GTBobsTorch:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                            _requirements[RequirementType.Torch]));
                    }
                    break;
                case DungeonNodeID.GT1FLeft:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRight], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FLeftToRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FLeftToRightKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeft],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRight],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT1FLeftPastHammerBlocks:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case DungeonNodeID.GT1FLeftDMsRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                            _requirements[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.GT1FLeftPastBonkableGaps:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                            _requirements[RequirementType.BonkOverLedge]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                            _requirements[RequirementType.Hover]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftMapChestRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FMapChestRoomKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FMapChestRoomKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftMapChestRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT1FLeftMapChestRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FMapChestRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FSpikeTrapPortalRoomKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT1FLeftSpikeTrapPortalRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FLeftFiresnakeRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT1FLeftPastFiresnakeRoomGap:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftFiresnakeRoom], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FFiresnakeRoomKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftFiresnakeRoom], node,
                            _requirements[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.GT1FFiresnakeRoomKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomGap],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomGap], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FFiresnakeRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FLeftRandomizerRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT1FRight:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FLeftToRightKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FRightTileRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRight], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FTileRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FTileRoomKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRightTileRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT1FRightFourTorchRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRightTileRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FTileRoomKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FRightCompassRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom], node,
                            _requirements[RequirementType.FireRod]));
                    }
                    break;
                case DungeonNodeID.GT1FRightPastCompassRoomPortal:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRightCompassRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FCollapsingWalkwayKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FCollapsingWalkwayKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRightPastCompassRoomPortal],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT1FRightCollapsingWalkway:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRightPastCompassRoomPortal], node,
                            dungeonData.KeyDoors[KeyDoorID.GT1FCollapsingWalkwayKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT1FBottomRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FLeftRandomizerRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GTBoss1:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FBottomRoom], node,
                            _requirements[RequirementType.GTBoss1]));
                    }
                    break;
                case DungeonNodeID.GTB1BossChests:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GTBoss1],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GTBigChest:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT1FBottomRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.GTBigChest].Requirement));
                    }
                    break;
                case DungeonNodeID.GT3FPastRedGoriyaRooms:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT], node,
                            _requirements[RequirementType.RedEyegoreGoriya]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.GT3FBigKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT], node,
                            _requirements[RequirementType.MimicClip]));
                    }
                    break;
                case DungeonNodeID.GT3FBigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT3FPastRedGoriyaRooms],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT3FPastBigKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT3FPastRedGoriyaRooms], node,
                            dungeonData.KeyDoors[KeyDoorID.GT3FBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GTBoss2:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor], node,
                            _requirements[RequirementType.GTBoss2]));
                    }
                    break;
                case DungeonNodeID.GT4FPastBoss2:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GTBoss2],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT5FPastFourTorchRooms:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT4FPastBoss2], node,
                            _requirements[RequirementType.FireSource]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.GT6FFirstKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT6FFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT5FPastFourTorchRooms],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT6FPastFirstKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT5FPastFourTorchRooms], node,
                            dungeonData.KeyDoors[KeyDoorID.GT6FFirstKeyDoor].Requirement));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                            dungeonData.KeyDoors[KeyDoorID.GT6FSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GT6FSecondKeyDoor:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT6FBossRoom],
                            node, _requirements[RequirementType.NoRequirement]));
                    }
                    break;
                case DungeonNodeID.GT6FBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node,
                            dungeonData.KeyDoors[KeyDoorID.GT6FSecondKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GTBoss3:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                            _requirements[RequirementType.GTBoss3]));
                    }
                    break;
                case DungeonNodeID.GTBoss3Item:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GTBoss3], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GTBoss3], node,
                            _requirements[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.GT6FPastBossRoomGap:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GTBoss3Item],
                            node, _requirements[RequirementType.NoRequirement]));
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                            _requirements[RequirementType.Hover]));
                    }
                    break;
                case DungeonNodeID.GTFinalBossRoom:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GT6FPastBossRoomGap], node,
                            dungeonData.KeyDoors[KeyDoorID.GT7FBigKeyDoor].Requirement));
                    }
                    break;
                case DungeonNodeID.GTFinalBoss:
                    {
                        connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.GTFinalBossRoom], node,
                            _requirements[RequirementType.GTFinalBoss]));
                    }
                    break;
            }
        }
    }
}
