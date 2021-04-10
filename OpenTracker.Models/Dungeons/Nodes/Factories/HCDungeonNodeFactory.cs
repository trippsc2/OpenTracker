using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements.Complex;

namespace OpenTracker.Models.Dungeons.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Hyrule Castle nodes.
    /// </summary>
    public class HCDungeonNodeFactory : IHCDungeonNodeFactory
    {
        private readonly IComplexRequirementDictionary _complexRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly IEntryNodeConnection.Factory _entryFactory;
        private readonly INodeConnection.Factory _connectionFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="complexRequirements">
        ///     The complex requirement dictionary.
        /// </param>
        /// <param name="overworldNodes">
        ///     The overworld node dictionary.
        /// </param>
        /// <param name="entryFactory">
        ///     An Autofac factory for creating entry node connections.
        /// </param>
        /// <param name="connectionFactory">
        ///     An Autofac factory for creating regular node connections.
        /// </param>
        public HCDungeonNodeFactory(
            IComplexRequirementDictionary complexRequirements, IOverworldNodeDictionary overworldNodes,
            IEntryNodeConnection.Factory entryFactory, INodeConnection.Factory connectionFactory)
        {
            _complexRequirements = complexRequirements;

            _overworldNodes = overworldNodes;

            _entryFactory = entryFactory;
            _connectionFactory = connectionFactory;
        }

        public void PopulateNodeConnections(
            IMutableDungeon dungeonData, DungeonNodeID id, INode node, IList<INodeConnection> connections)
        {
            switch (id)
            {
                case DungeonNodeID.HCSanctuary:
                    connections.Add(_entryFactory(
                        _overworldNodes[OverworldNodeID.HCSanctuaryEntry]));
                    connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCBack],
                        node));
                    break;
                case DungeonNodeID.HCFront:
                    connections.Add(_entryFactory(
                        _overworldNodes[OverworldNodeID.HCFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.HCEscapeFirstKeyDoor].Requirement));
                    connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCDarkRoomFront],
                        node));
                    break;
                case DungeonNodeID.HCEscapeFirstKeyDoor:
                    connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCFront],
                        node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor],
                        node));
                    break;
                case DungeonNodeID.HCPastEscapeFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCFront], node,
                        dungeonData.KeyDoors[KeyDoorID.HCEscapeFirstKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.HCEscapeSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.HCEscapeSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor], node));
                    break;
                case DungeonNodeID.HCPastEscapeSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastEscapeFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.HCEscapeSecondKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCZeldasCell], node,
                        dungeonData.KeyDoors[KeyDoorID.HCZeldasCellDoor].Requirement));
                    break;
                case DungeonNodeID.HCZeldasCellDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCZeldasCell], node));
                    break;
                case DungeonNodeID.HCZeldasCell:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastEscapeSecondKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.HCZeldasCellDoor].Requirement));
                    break;
                case DungeonNodeID.HCDarkRoomFront:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCFront], node,
                        _complexRequirements[ComplexRequirementType.DarkRoomHC]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.HCDarkCrossKeyDoor].Requirement));
                    break;
                case DungeonNodeID.HCDarkCrossKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCDarkRoomFront], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor], node));
                    break;
                case DungeonNodeID.HCPastDarkCrossKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCDarkRoomFront], node,
                        dungeonData.KeyDoors[KeyDoorID.HCDarkCrossKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastSewerRatRoomKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.HCSewerRatRoomKeyDoor].Requirement));
                    break;
                case DungeonNodeID.HCSewerRatRoomKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastSewerRatRoomKeyDoor], node));
                    break;
                case DungeonNodeID.HCPastSewerRatRoomKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCDarkRoomBack], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastDarkCrossKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.HCSewerRatRoomKeyDoor].Requirement));
                    break;
                case DungeonNodeID.HCDarkRoomBack:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCPastSewerRatRoomKeyDoor], node));
                    connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.HCBack],
                        node, _complexRequirements[ComplexRequirementType.DarkRoomHC]));
                    break;
                case DungeonNodeID.HCBack:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.HCBackEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.HCDarkRoomBack], node));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}