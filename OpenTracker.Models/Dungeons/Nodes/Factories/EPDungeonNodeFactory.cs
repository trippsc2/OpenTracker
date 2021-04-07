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
    ///     This class contains the creation logic for Eastern Palace nodes.
    /// </summary>
    public class EPDungeonNodeFactory : IEPDungeonNodeFactory
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
        public EPDungeonNodeFactory(
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
                case DungeonNodeID.EP:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.EPEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPPastBigKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.EPBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.EPBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EP], node,
                        dungeonData.KeyDoors[KeyDoorID.EPBigChest].Requirement));
                    break;
                case DungeonNodeID.EPRightDarkRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EP], node,
                        _requirements[RequirementType.DarkRoomEPRight]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPPastRightKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.EPRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.EPRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPRightDarkRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPPastRightKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.EPPastRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPRightDarkRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.EPRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.EPBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EP], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPPastBigKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.EPPastBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EP], node,
                        dungeonData.KeyDoors[KeyDoorID.EPBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.EPBackDarkRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPPastBigKeyDoor], node,
                        _requirements[RequirementType.DarkRoomEPBack]));
                    break;
                case DungeonNodeID.EPPastBackKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPBackDarkRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.EPBackKeyDoor].Requirement));
                    break;
                case DungeonNodeID.EPBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPPastBackKeyDoor], node,
                        _requirements[RequirementType.RedEyegoreGoriya]));
                    break;
                case DungeonNodeID.EPBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPBossRoom], node,
                        _requirements[RequirementType.EPBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}