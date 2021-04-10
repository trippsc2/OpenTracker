using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Items;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;

namespace OpenTracker.Models.Dungeons.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Turtle Rock nodes.
    /// </summary>
    public class TRDungeonNodeFactory : ITRDungeonNodeFactory
    {
        private readonly IBossRequirementDictionary _bossRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        
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
        /// <param name="overworldNodes">
        ///     The overworld node dictionary.
        /// </param>
        /// <param name="entryFactory">
        ///     An Autofac factory for creating entry node connections.
        /// </param>
        /// <param name="connectionFactory">
        ///     An Autofac factory for creating regular node connections.
        /// </param>
        public TRDungeonNodeFactory(
            IBossRequirementDictionary bossRequirements, IComplexRequirementDictionary complexRequirements,
            IItemRequirementDictionary itemRequirements, IOverworldNodeDictionary overworldNodes,
            IEntryNodeConnection.Factory entryFactory, INodeConnection.Factory connectionFactory)
        {
            _bossRequirements = bossRequirements;
            _complexRequirements = complexRequirements;
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
                case DungeonNodeID.TRFront:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.TRFrontEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1SomariaTrack], node));
                    break;
                case DungeonNodeID.TRF1SomariaTrack:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRFront], node,
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1CompassChestArea], node,
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1FourTorchRoom], node,
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1FirstKeyDoorArea], node,
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)]));
                    break;
                case DungeonNodeID.TRF1CompassChestArea:
                case DungeonNodeID.TRF1FourTorchRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1SomariaTrack], node));
                    break;
                case DungeonNodeID.TRF1RollerRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1FourTorchRoom], node,
                        _itemRequirements[(ItemType.FireRod, 1)]));
                    break;
                case DungeonNodeID.TRF1FirstKeyDoorArea:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1SomariaTrack], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TR1FFirstKeyDoor].Requirement));
                    break;
                case DungeonNodeID.TRF1FirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1FirstKeyDoorArea], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor], node));
                    break;
                case DungeonNodeID.TRF1PastFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1FirstKeyDoorArea], node,
                        dungeonData.KeyDoors[KeyDoorID.TR1FFirstKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1PastSecondKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TR1FSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.TRF1SecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1PastSecondKeyDoor], node));
                    break;
                case DungeonNodeID.TRF1PastSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1PastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TR1FSecondKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1], node));
                    break;
                case DungeonNodeID.TRB1:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.TRMiddleEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRF1PastSecondKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TR1FThirdKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1BigChestArea], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1RightSide], node,
                        dungeonData.KeyDoors[KeyDoorID.TRB1BigKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1PastBigKeyChestKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TRB1BigKeyChestKeyDoor].Requirement));
                    break;
                case DungeonNodeID.TRB1BigKeyChestKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1PastBigKeyChestKeyDoor], node));
                    break;
                case DungeonNodeID.TRB1PastBigKeyChestKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1], node,
                        dungeonData.KeyDoors[KeyDoorID.TRB1BigKeyChestKeyDoor].Requirement));
                    break;
                case DungeonNodeID.TRB1MiddleRightEntranceArea:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.TRMiddleEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1], node));
                    break;
                case DungeonNodeID.TRB1BigChestArea:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1MiddleRightEntranceArea], node,
                        _itemRequirements[(ItemType.Hookshot, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1MiddleRightEntranceArea], node,
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1MiddleRightEntranceArea], node,
                        _complexRequirements[ComplexRequirementType.Hover]));
                    break;
                case DungeonNodeID.TRBigChest:
                    connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TRB1BigChestArea], node,
                        dungeonData.KeyDoors[KeyDoorID.TRBigChest].Requirement));
                    break;
                case DungeonNodeID.TRB1BigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1RightSide], node));
                    break;
                case DungeonNodeID.TRB1RightSide:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1], node,
                        dungeonData.KeyDoors[KeyDoorID.TRB1BigKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRPastB1ToB2KeyDoor], node));
                    break;
                case DungeonNodeID.TRPastB1ToB2KeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB1RightSide], node,
                        dungeonData.KeyDoors[KeyDoorID.TRB1ToB2KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomTop], node));
                    break;
                case DungeonNodeID.TRB2DarkRoomTop:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRPastB1ToB2KeyDoor], node,
                        _complexRequirements[ComplexRequirementType.DarkRoomTR]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomBottom], node,
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)]));
                    break;
                case DungeonNodeID.TRB2DarkRoomBottom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomTop], node,
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze], node,
                        _complexRequirements[ComplexRequirementType.DarkRoomTR]));
                    break;
                case DungeonNodeID.TRB2PastDarkMaze:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.TRBackEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB2DarkRoomBottom], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB2PastKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.TRB2KeyDoor].Requirement));
                    break;
                case DungeonNodeID.TRLaserBridgeChests:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze], node,
                        _complexRequirements[ComplexRequirementType.LaserBridge]));
                    break;
                case DungeonNodeID.TRB2KeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB2PastKeyDoor], node));
                    break;
                case DungeonNodeID.TRB2PastKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB2PastDarkMaze], node,
                        dungeonData.KeyDoors[KeyDoorID.TRB2KeyDoor].Requirement));
                    break;
                case DungeonNodeID.TRB3:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB2PastKeyDoor], node));
                    break;
                case DungeonNodeID.TRB3BossRoomEntry:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB3], node,
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)]));
                    break;
                case DungeonNodeID.TRBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRB3BossRoomEntry], node,
                        dungeonData.KeyDoors[KeyDoorID.TRBossRoomBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.TRBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.TRBossRoom], node,
                        _bossRequirements[BossPlacementID.TRBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}