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
    ///     This class contains the creation logic for Tower of Hera nodes.
    /// </summary>
    public class ToHDungeonNodeFactory : IToHDungeonNodeFactory
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
        public ToHDungeonNodeFactory(
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
                case DungeonNodeID.ToH:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.ToHEntry]));
                    break;
                case DungeonNodeID.ToHPastKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ToH], node,
                        dungeonData.KeyDoors[KeyDoorID.ToHKeyDoor].Requirement));
                    break;
                case DungeonNodeID.ToHBasementTorchRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ToHPastKeyDoor], node,
                        _requirements[RequirementType.FireSource]));
                    break;
                case DungeonNodeID.ToHPastBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ToH], node,
                        dungeonData.KeyDoors[KeyDoorID.ToHBigKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ToH], node,
                        _requirements[RequirementType.ToHHerapot]));
                    break;
                case DungeonNodeID.ToHBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ToHPastBigKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.ToHBigChest].Requirement));
                    break;
                case DungeonNodeID.ToHBoss:
                    connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ToHPastBigKeyDoor], node,
                        _requirements[RequirementType.ToHBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}