using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Complex;

namespace OpenTracker.Models.Dungeons.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Eastern Palace nodes.
    /// </summary>
    public class EPDungeonNodeFactory : IEPDungeonNodeFactory
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
        public EPDungeonNodeFactory(
            IBossRequirementDictionary bossRequirements, IComplexRequirementDictionary complexRequirements,
            IOverworldNodeDictionary overworldNodes, IEntryNodeConnection.Factory entryFactory,
            INodeConnection.Factory connectionFactory)
        {
            _overworldNodes = overworldNodes;

            _entryFactory = entryFactory;
            _connectionFactory = connectionFactory;
            _bossRequirements = bossRequirements;
            _complexRequirements = complexRequirements;
        }

        public void PopulateNodeConnections(
            IMutableDungeon dungeonData, DungeonNodeID id, INode node, IList<INodeConnection> connections)
        {
            switch (id)
            {
                case DungeonNodeID.EP:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.EPEntry]));
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
                        _complexRequirements[ComplexRequirementType.DarkRoomEPRight]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPPastRightKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.EPRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.EPRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPRightDarkRoom], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPPastRightKeyDoor], node));
                    break;
                case DungeonNodeID.EPPastRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPRightDarkRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.EPRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.EPBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EP], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPPastBigKeyDoor], node));
                    break;
                case DungeonNodeID.EPPastBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EP], node,
                        dungeonData.KeyDoors[KeyDoorID.EPBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.EPBackDarkRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPPastBigKeyDoor], node,
                        _complexRequirements[ComplexRequirementType.DarkRoomEPBack]));
                    break;
                case DungeonNodeID.EPPastBackKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPBackDarkRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.EPBackKeyDoor].Requirement));
                    break;
                case DungeonNodeID.EPBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPPastBackKeyDoor], node,
                        _complexRequirements[ComplexRequirementType.RedEyegoreGoriya]));
                    break;
                case DungeonNodeID.EPBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.EPBossRoom], node,
                        _bossRequirements[BossPlacementID.EPBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}