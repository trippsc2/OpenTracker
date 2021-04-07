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
    ///     This class contains the creation logic for Desert Palace nodes.
    /// </summary>
    public class DPDungeonNodeFactory : IDPDungeonNodeFactory
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
        public DPDungeonNodeFactory(
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
                case DungeonNodeID.DPFront:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.DPFrontEntry]));
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.DPLeftEntry]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DPPastRightKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.DPRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.DPTorch:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DPFront], node,
                        _requirements[RequirementType.Torch]));
                    break;
                case DungeonNodeID.DPBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DPFront], node,
                        dungeonData.KeyDoors[KeyDoorID.DPBigChest].Requirement));
                    break;
                case DungeonNodeID.DPRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DPFront], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DPPastRightKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.DPPastRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DPFront], node,
                        dungeonData.KeyDoors[KeyDoorID.DPRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.DPBack:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.DPBackEntry]));
                    break;
                case DungeonNodeID.DP2F:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DPBack], node,
                        dungeonData.KeyDoors[KeyDoorID.DP1FKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.DP2FFirstKeyDoor].Requirement));
                    break;
                case DungeonNodeID.DP2FFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DP2F], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.DP2FPastFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DP2F], node,
                        dungeonData.KeyDoors[KeyDoorID.DP2FFirstKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DP2FPastSecondKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.DP2FSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.DP2FSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DP2FPastSecondKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.DP2FPastSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.DP2FSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.DPPastFourTorchWall:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DP2FPastSecondKeyDoor], node,
                        _requirements[RequirementType.FireSource]));
                    break;
                case DungeonNodeID.DPBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DPPastFourTorchWall], node,
                        dungeonData.KeyDoors[KeyDoorID.DPBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.DPBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.DPBossRoom], node,
                        _requirements[RequirementType.DPBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}