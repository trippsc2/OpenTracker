using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Items;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Item;

namespace OpenTracker.Models.Dungeons.Nodes.Factories
{
    /// <summary>
    /// This class contains the creation logic for Swamp Palace nodes.
    /// </summary>
    public class SPDungeonNodeFactory : ISPDungeonNodeFactory
    {
        private readonly IBossRequirementDictionary _bossRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly IEntryNodeConnection.Factory _entryFactory;
        private readonly INodeConnection.Factory _connectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossRequirements">
        ///     The <see cref="IBossRequirementDictionary"/>.
        /// </param>
        /// <param name="itemRequirements">
        ///     The <see cref="IItemRequirementDictionary"/>.
        /// </param>
        /// <param name="overworldNodes">
        ///     The <see cref="IOverworldNodeDictionary"/>.
        /// </param>
        /// <param name="entryFactory">
        ///     An Autofac factory for creating new <see cref="IEntryNodeConnection"/> objects.
        /// </param>
        /// <param name="connectionFactory">
        ///     An Autofac factory for creating new <see cref="INodeConnection"/> objects.
        /// </param>
        public SPDungeonNodeFactory(
            IBossRequirementDictionary bossRequirements, IItemRequirementDictionary itemRequirements,
            IOverworldNodeDictionary overworldNodes, IEntryNodeConnection.Factory entryFactory,
            INodeConnection.Factory connectionFactory)
        {
            _bossRequirements = bossRequirements;
            _itemRequirements = itemRequirements;

            _overworldNodes = overworldNodes;

            _entryFactory = entryFactory;
            _connectionFactory = connectionFactory;
        }

        public void PopulateNodeConnections(
            IMutableDungeon dungeonData, DungeonNodeID id, INode node, IList<INodeConnection> connections)
        {
            switch (id)
            {
                case DungeonNodeID.SP:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.SPEntry]));
                    break;
                case DungeonNodeID.SPAfterRiver:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SP], node,
                        _itemRequirements[(ItemType.Flippers, 1)]));
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
                        dungeonData.Nodes[DungeonNodeID.SPB1], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node));
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
                        dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastSecondRightKeyDoor], node));
                    break;
                case DungeonNodeID.SPB1PastSecondRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastFirstRightKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SPB1SecondRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPB1PastRightHammerBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastSecondRightKeyDoor], node,
                        _itemRequirements[(ItemType.Hammer, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastLeftKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SPB1LeftKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPB1KeyLedge:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node,
                        _itemRequirements[(ItemType.Hookshot, 1)]));
                    break;
                case DungeonNodeID.SPB1LeftKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastLeftKeyDoor], node));
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
                        _itemRequirements[(ItemType.Hookshot, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SPB1BackFirstKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPB1BackFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1Back], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node));
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
                        dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPBossRoom], node));
                    break;
                case DungeonNodeID.SPBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPB1PastBackFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.SPBossRoomKeyDoor].Requirement));
                    break;
                case DungeonNodeID.SPBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.SPBossRoom], node,
                        _bossRequirements[BossPlacementID.SPBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}