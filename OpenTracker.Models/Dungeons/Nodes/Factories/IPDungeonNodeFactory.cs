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
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Dungeons.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Ice Palace nodes.
    /// </summary>
    public class IPDungeonNodeFactory : IIPDungeonNodeFactory
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
        ///     The item requirement dictionary
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
        public IPDungeonNodeFactory(
            IBossRequirementDictionary bossRequirements, IComplexRequirementDictionary complexRequirements,
            IItemRequirementDictionary itemRequirements, ISequenceBreakRequirementDictionary sequenceBreakRequirements,
            IOverworldNodeDictionary overworldNodes, IEntryNodeConnection.Factory entryFactory,
            INodeConnection.Factory connectionFactory)
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
                case DungeonNodeID.IP:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.IPEntry]));
                    break;
                case DungeonNodeID.IPPastEntranceFreezorRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IP], node,
                        _complexRequirements[ComplexRequirementType.MeltThings]));
                    break;
                case DungeonNodeID.IPB1LeftSide:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPPastEntranceFreezorRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.IP1FKeyDoor].Requirement));
                    break;
                case DungeonNodeID.IPB1RightSide:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastLiftBlock], node));
                    connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.IPB1LeftSide], node,
                        _complexRequirements[ComplexRequirementType.IPIceBreaker]));
                    break;
                case DungeonNodeID.IPB2LeftSide:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB1LeftSide], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB2KeyDoor].Requirement));
                    break;
                case DungeonNodeID.IPB2KeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2LeftSide], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node));
                    break;
                case DungeonNodeID.IPB2PastKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2LeftSide], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB2KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB3KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node));
                    break;
                case DungeonNodeID.IPB2PastHammerBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node,
                        _itemRequirements[(ItemType.Hammer, 1)]));
                    break;
                case DungeonNodeID.IPB2PastLiftBlock:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastHammerBlocks], node,
                        _itemRequirements[(ItemType.Gloves, 1)]));
                    break;
                case DungeonNodeID.IPB3KeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node));
                    break;
                case DungeonNodeID.IPSpikeRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB1RightSide], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB3KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4RightSide], node));
                    break;
                case DungeonNodeID.IPB4RightSide:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        _itemRequirements[(ItemType.Hookshot, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        _sequenceBreakRequirements[SequenceBreakType.BombJumpIPHookshotGap]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        _complexRequirements[ComplexRequirementType.Hover]));
                    break;
                case DungeonNodeID.IPB4IceRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB4KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                        _sequenceBreakRequirements[SequenceBreakType.BombJumpIPFreezorRoomGap]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                        _complexRequirements[ComplexRequirementType.Hover]));
                    break;
                case DungeonNodeID.IPB4FreezorRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        _sequenceBreakRequirements[SequenceBreakType.BombJumpIPFreezorRoomGap]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        _complexRequirements[ComplexRequirementType.Hover]));
                    break;
                case DungeonNodeID.IPFreezorChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                        _complexRequirements[ComplexRequirementType.MeltThings]));
                    break;
                case DungeonNodeID.IPB4KeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor], node));
                    break;
                case DungeonNodeID.IPB4PastKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB4KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB5], node));
                    break;
                case DungeonNodeID.IPBigChestArea:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node));
                    break;
                case DungeonNodeID.IPBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPBigChestArea], node,
                        dungeonData.KeyDoors[KeyDoorID.IPBigChest].Requirement));
                    break;
                case DungeonNodeID.IPB5:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor], node));
                    break;
                case DungeonNodeID.IPB5PastBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB5], node,
                        dungeonData.KeyDoors[KeyDoorID.IPBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.IPB6:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB5PastBigKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB5KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6PastKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB6KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB5], node,
                        _sequenceBreakRequirements[SequenceBreakType.BombJumpIPBJ]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB5], node,
                        _complexRequirements[ComplexRequirementType.IPIceBreaker]));
                    break;
                case DungeonNodeID.IPB6KeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6PastKeyDoor], node));
                    break;
                case DungeonNodeID.IPB6PastKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB6KeyDoor].Requirement));
                    break;
                case DungeonNodeID.IPB6PreBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6], node,
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6PastKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6], node,
                        _sequenceBreakRequirements[SequenceBreakType.BombJumpIPBJ]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6], node,
                        _complexRequirements[ComplexRequirementType.IPIceBreaker]));
                    break;
                case DungeonNodeID.IPB6PastHammerBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6PreBossRoom], node,
                        _itemRequirements[(ItemType.Hammer, 1)]));
                    break;
                case DungeonNodeID.IPB6PastLiftBlock:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6PastHammerBlocks], node,
                        _itemRequirements[(ItemType.Gloves, 1)]));
                    break;
                case DungeonNodeID.IPBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6PastLiftBlock], node,
                        _bossRequirements[BossPlacementID.IPBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}