using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Items;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Dungeons.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Skull Woods nodes.
    /// </summary>
    public class SWDungeonNodeFactory : ISWDungeonNodeFactory
    {
        private readonly IBossRequirementDictionary _bossRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;
        
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
        /// <param name="itemRequirements">
        ///     The item requirement dictionary.
        /// </param>
        /// <param name="sequenceBreakRequirements">
        ///     The sequence break requirement dictionary.
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
        public SWDungeonNodeFactory(
            IBossRequirementDictionary bossRequirements, IComplexRequirementDictionary complexRequirements, IItemRequirementDictionary itemRequirements, ISequenceBreakRequirementDictionary sequenceBreakRequirements, IOverworldNodeDictionary overworldNodes,
            IEntryNodeConnection.Factory entryFactory, INodeConnection.Factory connectionFactory)
        {
            _bossRequirements = bossRequirements;
            _complexRequirements = complexRequirements;
            _itemRequirements = itemRequirements;
            _sequenceBreakRequirements = sequenceBreakRequirements;

            _overworldNodes = overworldNodes;

            _entryFactory = entryFactory;
            _connectionFactory = connectionFactory;
        }

        public void PopulateNodeConnections(
            IMutableDungeon dungeonData, DungeonNodeID id, INode node, IList<INodeConnection> connections)
        {
            switch (id)
            {
                case DungeonNodeID.SWBigChestAreaBottom:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.SWFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaTop], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide], node,
                        dungeonData.KeyDoors[KeyDoorID.SWFrontLeftKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontRightSide], node,
                        dungeonData.KeyDoors[KeyDoorID.SWFrontRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBigChestAreaTop:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.SWFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                        _itemRequirements[(ItemType.Hookshot, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                        _sequenceBreakRequirements[SequenceBreakType.BombJumpSWBigChest]));
                    break;
                case DungeonNodeID.SWBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaTop], node,
                        dungeonData.KeyDoors[KeyDoorID.SWBigChest].Requirement));
                    break;
                case DungeonNodeID.SWFrontLeftKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node));
                    break;
                case DungeonNodeID.SWFrontLeftSide:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.SWFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                        dungeonData.KeyDoors[KeyDoorID.SWFrontLeftKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWFrontRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontRightSide], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node));
                    break;
                case DungeonNodeID.SWFrontRightSide:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.SWFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontLeftSide], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBigChestAreaBottom], node,
                        dungeonData.KeyDoors[KeyDoorID.SWFrontRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWFrontBackConnector:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.SWFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWPastTheWorthlessKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SWWorthlessKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWWorthlessKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontBackConnector], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWPastTheWorthlessKeyDoor], node));
                    break;
                case DungeonNodeID.SWPastTheWorthlessKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWFrontBackConnector], node,
                        dungeonData.KeyDoors[KeyDoorID.SWWorthlessKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBack:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.SWBackEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SWBackFirstKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBackFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBack], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackPastFirstKeyDoor], node));
                    break;
                case DungeonNodeID.SWBackPastFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBack], node,
                        dungeonData.KeyDoors[KeyDoorID.SWBackFirstKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBackPastFourTorchRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackPastFirstKeyDoor], node,
                        _itemRequirements[(ItemType.FireRod, 1)]));
                    break;
                case DungeonNodeID.SWBackPastCurtains:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackPastFourTorchRoom], node,
                        _complexRequirements[ComplexRequirementType.Curtains]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBossRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.SWBackSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBackSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackPastCurtains], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBossRoom], node));
                    break;
                case DungeonNodeID.SWBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBackPastCurtains], node,
                        dungeonData.KeyDoors[KeyDoorID.SWBackSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SWBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SWBossRoom], node,
                        _bossRequirements[BossPlacementID.SWBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}