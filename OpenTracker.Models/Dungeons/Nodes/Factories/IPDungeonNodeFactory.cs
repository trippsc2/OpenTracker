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
    ///     This class contains the creation logic for Ice Palace nodes.
    /// </summary>
    public class IPDungeonNodeFactory : IIPDungeonNodeFactory
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
        public IPDungeonNodeFactory(
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
                case DungeonNodeID.IP:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.IPEntry]));
                    break;
                case DungeonNodeID.IPPastEntranceFreezorRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IP], node,
                        _requirements[RequirementType.MeltThings]));
                    break;
                case DungeonNodeID.IPB1LeftSide:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPPastEntranceFreezorRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.IP1FKeyDoor].Requirement));
                    break;
                case DungeonNodeID.IPB1RightSide:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastLiftBlock], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.IPB1LeftSide], node,
                        _requirements[RequirementType.IPIceBreaker]));
                    break;
                case DungeonNodeID.IPB2LeftSide:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB1LeftSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB2KeyDoor].Requirement));
                    break;
                case DungeonNodeID.IPB2KeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2LeftSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.IPB2PastKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2LeftSide], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB2KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB3KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.IPB2PastHammerBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node,
                        _requirements[RequirementType.Hammer]));
                    break;
                case DungeonNodeID.IPB2PastLiftBlock:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastHammerBlocks], node,
                        _requirements[RequirementType.Gloves1]));
                    break;
                case DungeonNodeID.IPB3KeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.IPSpikeRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB1RightSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB3KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4RightSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.IPB4RightSide:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPSpikeRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        _requirements[RequirementType.Hookshot]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        _requirements[RequirementType.BombJumpIPHookshotGap]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        _requirements[RequirementType.Hover]));
                    break;
                case DungeonNodeID.IPB4IceRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB4KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                        _requirements[RequirementType.BombJumpIPFreezorRoomGap]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                        _requirements[RequirementType.Hover]));
                    break;
                case DungeonNodeID.IPB4FreezorRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB2PastKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        _requirements[RequirementType.BombJumpIPFreezorRoomGap]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        _requirements[RequirementType.Hover]));
                    break;
                case DungeonNodeID.IPFreezorChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                        _requirements[RequirementType.MeltThings]));
                    break;
                case DungeonNodeID.IPB4KeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.IPB4PastKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4IceRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB4KeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB5], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.IPBigChestArea:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.IPBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPBigChestArea], node,
                        dungeonData.KeyDoors[KeyDoorID.IPBigChest].Requirement));
                    break;
                case DungeonNodeID.IPB5:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4FreezorRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB4PastKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
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
                        _requirements[RequirementType.BombJumpIPBJ]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB5], node,
                        _requirements[RequirementType.IPIceBreaker]));
                    break;
                case DungeonNodeID.IPB6KeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6PastKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.IPB6PastKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6], node,
                        dungeonData.KeyDoors[KeyDoorID.IPB6KeyDoor].Requirement));
                    break;
                case DungeonNodeID.IPB6PreBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6], node,
                        _requirements[RequirementType.CaneOfSomaria]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6PastKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6], node,
                        _requirements[RequirementType.BombJumpIPBJ]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6], node,
                        _requirements[RequirementType.IPIceBreaker]));
                    break;
                case DungeonNodeID.IPB6PastHammerBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6PreBossRoom], node,
                        _requirements[RequirementType.Hammer]));
                    break;
                case DungeonNodeID.IPB6PastLiftBlock:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6PastHammerBlocks], node,
                        _requirements[RequirementType.Gloves1]));
                    break;
                case DungeonNodeID.IPBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.IPB6PastLiftBlock], node,
                        _requirements[RequirementType.IPBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}