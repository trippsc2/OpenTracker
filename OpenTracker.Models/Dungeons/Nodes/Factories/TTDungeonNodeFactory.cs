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
    ///     This class contains the creation logic for Thieves' Town nodes.
    /// </summary>
    public class TTDungeonNodeFactory : ITTDungeonNodeFactory
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
        public TTDungeonNodeFactory(
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
                case DungeonNodeID.TT:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.TTEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TTBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.TTBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TT], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.TTPastBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TT], node,
                        dungeonData.KeyDoors[KeyDoorID.TTBigKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TTFirstKeyDoor].Requirement));
                    break;
                case DungeonNodeID.TTFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.TTPastFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TTFirstKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TTBigChestKeyDoor].Requirement));
                    break;
                case DungeonNodeID.TTPastSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TTSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.TTBigChestKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.TTPastBigChestRoomKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TTBigChestKeyDoor].Requirement));
                    break;
                case DungeonNodeID.TTPastHammerBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor], node,
                        _requirements[RequirementType.Hammer]));
                    break;
                case DungeonNodeID.TTBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastHammerBlocks], node,
                        dungeonData.KeyDoors[KeyDoorID.TTBigChest].Requirement));
                    break;
                case DungeonNodeID.TTBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                        _requirements[RequirementType.BossShuffleOn]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTPastSecondKeyDoor], node,
                        _requirements[RequirementType.BossShuffleOff]));
                    break;
                case DungeonNodeID.TTBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TTBossRoom], node,
                        _requirements[RequirementType.TTBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}