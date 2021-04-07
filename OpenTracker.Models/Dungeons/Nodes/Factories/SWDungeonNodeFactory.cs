using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Skull Woods nodes.
    /// </summary>
    public class SWDungeonNodeFactory : ISWDungeonNodeFactory
    {
        private readonly IRequirementDictionary _requirements;
        private readonly IOverworldNodeDictionary _requirementNodes;

        private readonly IEntryNodeConnection.Factory _entryFactory;
        private readonly INodeConnection.Factory _connectionFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="requirements">
        ///     The requirement dictionary.
        /// </param>
        /// <param name="requirementNodes">
        ///     The requirement node dictionary.
        /// </param>
        /// <param name="entryFactory">
        ///     An Autofac factory for creating entry node connections.
        /// </param>
        /// <param name="connectionFactory">
        ///     An Autofac factory for creating regular node connections.
        /// </param>
        public SWDungeonNodeFactory(
            IRequirementDictionary requirements, IOverworldNodeDictionary requirementNodes,
            IEntryNodeConnection.Factory entryFactory, INodeConnection.Factory connectionFactory)
        {
            _requirements = requirements;
            _requirementNodes = requirementNodes;

            _entryFactory = entryFactory;
            _connectionFactory = connectionFactory;
        }

        public void PopulateNodeConnections(
            IMutableDungeon dungeonData, DungeonNodeID id, INode node, IList<INodeConnection> connections)
        {
            switch (id)
            {
                case DungeonNodeID.SWBigChestAreaBottom:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.SWFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaTop], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide], node,
                        dungeonData.KeyDoors[KeyDoorID.SWFrontLeftKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontRightSide], node,
                        dungeonData.KeyDoors[KeyDoorID.SWFrontRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBigChestAreaTop:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.SWFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                        _requirements[RequirementType.Hookshot]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                        _requirements[RequirementType.BombJumpSWBigChest]));
                    break;
                case DungeonNodeID.SWBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaTop], node,
                        dungeonData.KeyDoors[KeyDoorID.SWBigChest].Requirement));
                    break;
                case DungeonNodeID.SWFrontLeftKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.SWFrontLeftSide:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.SWFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                        dungeonData.KeyDoors[KeyDoorID.SWFrontLeftKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWFrontRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontRightSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.SWFrontRightSide:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.SWFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                        dungeonData.KeyDoors[KeyDoorID.SWFrontRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWFrontBackConnector:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.SWFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWPastTheWorthlessKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SWWorthlessKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWWorthlessKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontBackConnector], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWPastTheWorthlessKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.SWPastTheWorthlessKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontBackConnector], node,
                        dungeonData.KeyDoors[KeyDoorID.SWWorthlessKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBack:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.SWBackEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SWBackFirstKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBackFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBack], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackPastFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.SWBackPastFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBack], node,
                        dungeonData.KeyDoors[KeyDoorID.SWBackFirstKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBackPastFourTorchRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackPastFirstKeyDoor], node,
                        _requirements[RequirementType.FireRod]));
                    break;
                case DungeonNodeID.SWBackPastCurtains:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackPastFourTorchRoom], node,
                        _requirements[RequirementType.Curtains]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBossRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.SWBackSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBackSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackPastCurtains], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBossRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.SWBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackPastCurtains], node,
                        dungeonData.KeyDoors[KeyDoorID.SWBackSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBossRoom], node,
                        _requirements[RequirementType.SWBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}