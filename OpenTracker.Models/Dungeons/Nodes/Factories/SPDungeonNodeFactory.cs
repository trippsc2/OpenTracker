using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Swamp Palace nodes.
    /// </summary>
    public class SPDungeonNodeFactory : ISPDungeonNodeFactory
    {
        private readonly IRequirementDictionary _requirements;
        private readonly IOverworldNodeDictionary _requirementNodes;

        private readonly EntryNodeConnection.Factory _entryFactory;
        private readonly NodeConnection.Factory _connectionFactory;

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
        public SPDungeonNodeFactory(
            IRequirementDictionary requirements, IOverworldNodeDictionary requirementNodes,
            EntryNodeConnection.Factory entryFactory, NodeConnection.Factory connectionFactory)
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
                case DungeonNodeID.SP:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.SPEntry]));
                    break;
                case DungeonNodeID.SPAfterRiver:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SP], node,
                        _requirements[RequirementType.Flippers]));
                    break;
                case DungeonNodeID.SPB1:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPAfterRiver], node,
                        dungeonData.KeyDoors[KeyDoorID.SP1FKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SPB1FirstRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPB1FirstRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.SPB1PastFirstRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1], node,
                        dungeonData.KeyDoors[KeyDoorID.SPB1FirstRightKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastSecondRightKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SPB1SecondRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPB1SecondRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastSecondRightKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.SPB1PastSecondRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SPB1SecondRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPB1PastRightHammerBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastSecondRightKeyDoor], node,
                        _requirements[RequirementType.Hammer]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastLeftKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SPB1LeftKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPB1KeyLedge:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                        _requirements[RequirementType.Hookshot]));
                    break;
                case DungeonNodeID.SPB1LeftKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastLeftKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.SPB1PastLeftKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                        dungeonData.KeyDoors[KeyDoorID.SPB1LeftKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                        dungeonData.KeyDoors[KeyDoorID.SPBigChest].Requirement));
                    break;
                case DungeonNodeID.SPB1Back:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                        _requirements[RequirementType.Hookshot]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SPB1BackFirstKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPB1BackFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1Back], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.SPB1PastBackFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1Back], node,
                        dungeonData.KeyDoors[KeyDoorID.SPB1BackFirstKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPBossRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.SPBossRoomKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPBossRoomKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPBossRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.SPBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SPBossRoomKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPBossRoom], node,
                        _requirements[RequirementType.SPBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}