using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Exact;
using OpenTracker.Models.Requirements.SequenceBreak;

namespace OpenTracker.Models.Dungeons.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Agahnim's Tower nodes.
    /// </summary>
    public class ATDungeonNodeFactory : IATDungeonNodeFactory
    {
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        private readonly IItemExactRequirementDictionary _itemExactRequirements;
        private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;
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
        public ATDungeonNodeFactory(
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
                case DungeonNodeID.AT:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.ATEntry]));
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
                        _requirements[RequirementType.ATBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}