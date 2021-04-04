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
    ///     This class contains the creation logic for Palace of Darkness nodes.
    /// </summary>
    public class PoDDungeonNodeFactory : IPoDDungeonNodeFactory
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
        public PoDDungeonNodeFactory(
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
                case DungeonNodeID.PoD:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.PoDEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDFrontKeyDoor].Requirement));
                    break;
                case DungeonNodeID.PoDPastFirstRedGoriyaRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoD], node,
                        _requirements[RequirementType.RedEyegoreGoriya]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoD], node,
                        _requirements[RequirementType.CameraUnlock]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoD], node,
                        _requirements[RequirementType.MimicClip]));
                    break;
                case DungeonNodeID.PoDFrontKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoD], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.PoDLobbyArena:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoD], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDFrontKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastFirstRedGoriyaRoom], node,
                        _requirements[RequirementType.Hammer]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDCollapsingWalkwayKeyDoor].Requirement));
                    break;
                case DungeonNodeID.PoDBigKeyChestArea:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDBigKeyChestKeyDoor].Requirement));
                    break;
                case DungeonNodeID.PoDCollapsingWalkwayKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDCollapsingWalkwayKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDDarkMazeKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDHarmlessHellwayRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDHarmlessHellwayKeyDoor].Requirement));
                    break;
                case DungeonNodeID.PoDDarkBasement:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                        _requirements[RequirementType.DarkRoomPoDDarkBasement]));
                    break;
                case DungeonNodeID.PoDHarmlessHellwayKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDHarmlessHellwayRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.PoDHarmlessHellwayRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDHarmlessHellwayKeyDoor].Requirement));
                    break;
                case DungeonNodeID.PoDDarkMazeKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.PoDPastDarkMazeKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDDarkMazeKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDDarkMaze], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.PoDDarkMaze:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor], node,
                        _requirements[RequirementType.DarkRoomPoDDarkMaze]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDBigChestLedge], node,
                        _requirements[RequirementType.DarkRoomPoDDarkMaze]));
                    break;
                case DungeonNodeID.PoDBigChestLedge:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDDarkMaze], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                        _requirements[RequirementType.BombJumpPoDHammerJump]));
                    break;
                case DungeonNodeID.PoDBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDBigChestLedge], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDBigChest].Requirement));
                    break;
                case DungeonNodeID.PoDPastSecondRedGoriyaRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                        _requirements[RequirementType.RedEyegoreGoriya]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                        _requirements[RequirementType.MimicClip]));
                    break;
                case DungeonNodeID.PoDPastBowStatue:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastSecondRedGoriyaRoom], node,
                        _requirements[RequirementType.Bow]));
                    break;
                case DungeonNodeID.PoDBossAreaDarkRooms:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastBowStatue], node,
                        _requirements[RequirementType.DarkRoomPoDBossArea]));
                    break;
                case DungeonNodeID.PoDPastHammerBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDBossAreaDarkRooms], node,
                        _requirements[RequirementType.Hammer]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDBossAreaKeyDoor].Requirement));
                    break;
                case DungeonNodeID.PoDBossAreaKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastHammerBlocks], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.PoDPastBossAreaKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastHammerBlocks], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDBossAreaKeyDoor].Requirement));
                    break;
                case DungeonNodeID.PoDBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.PoDBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.PoDBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.PoDBossRoom], node,
                        _requirements[RequirementType.PoDBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}