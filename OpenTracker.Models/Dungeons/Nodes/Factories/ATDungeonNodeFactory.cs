using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Complex;

namespace OpenTracker.Models.Dungeons.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Agahnim's Tower nodes.
    /// </summary>
    public class ATDungeonNodeFactory : IATDungeonNodeFactory
    {
        private readonly IBossRequirementDictionary _bossRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly IEntryNodeConnection.Factory _entryFactory;
        private readonly INodeConnection.Factory _connectionFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="bossRequirements">
        ///     The boss requirement dictionary.
        /// </param>
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
        public ATDungeonNodeFactory(
            IBossRequirementDictionary bossRequirements, IComplexRequirementDictionary complexRequirements,
            IOverworldNodeDictionary overworldNodes, IEntryNodeConnection.Factory entryFactory, INodeConnection.Factory connectionFactory)
        {
            _bossRequirements = bossRequirements;
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
                case DungeonNodeID.AT:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.ATEntry]));
                    break;
                case DungeonNodeID.ATDarkMaze:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.AT], node,
                        _complexRequirements[ComplexRequirementType.DarkRoomAT]));
                    break;
                case DungeonNodeID.ATPastFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATDarkMaze], node,
                        dungeonData.KeyDoors[KeyDoorID.ATFirstKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATPastSecondKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.ATSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.ATSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATPastFirstKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATPastSecondKeyDoor], node));
                    break;
                case DungeonNodeID.ATPastSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATPastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.ATSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.ATPastThirdKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATPastSecondKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.ATThirdKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATPastFourthKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.ATFourthKeyDoor].Requirement));
                    break;
                case DungeonNodeID.ATFourthKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATPastThirdKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATPastFourthKeyDoor], node));
                    break;
                case DungeonNodeID.ATPastFourthKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATPastThirdKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.ATFourthKeyDoor].Requirement));
                    break;
                case DungeonNodeID.ATBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATPastFourthKeyDoor], node,
                        _complexRequirements[ComplexRequirementType.Curtains]));
                    break;
                case DungeonNodeID.ATBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ATBossRoom], node,
                        _bossRequirements[BossPlacementID.ATBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}