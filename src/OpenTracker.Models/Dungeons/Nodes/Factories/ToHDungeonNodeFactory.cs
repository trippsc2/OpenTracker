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
    /// This class contains the creation logic for Tower of Hera nodes.
    /// </summary>
    public class ToHDungeonNodeFactory : IToHDungeonNodeFactory
    {
        private readonly IBossRequirementDictionary _bossRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly IEntryNodeConnection.Factory _entryFactory;
        private readonly INodeConnection.Factory _connectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossRequirements">
        ///     The <see cref="IBossRequirementDictionary"/>.
        /// </param>
        /// <param name="complexRequirements">
        ///     The <see cref="IComplexRequirementDictionary"/>.
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
        public ToHDungeonNodeFactory(
            IBossRequirementDictionary bossRequirements, IComplexRequirementDictionary complexRequirements,
            IOverworldNodeDictionary overworldNodes, IEntryNodeConnection.Factory entryFactory,
            INodeConnection.Factory connectionFactory)
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
                case DungeonNodeID.ToH:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.ToHEntry]));
                    break;
                case DungeonNodeID.ToHPastKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ToH], node,
                        dungeonData.KeyDoors[KeyDoorID.ToHKeyDoor].Requirement));
                    break;
                case DungeonNodeID.ToHBasementTorchRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ToHPastKeyDoor], node,
                        _complexRequirements[ComplexRequirementType.FireSource]));
                    break;
                case DungeonNodeID.ToHPastBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ToH], node,
                        dungeonData.KeyDoors[KeyDoorID.ToHBigKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ToH], node,
                        _complexRequirements[ComplexRequirementType.ToHHerapot]));
                    break;
                case DungeonNodeID.ToHBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.ToHPastBigKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.ToHBigChest].Requirement));
                    break;
                case DungeonNodeID.ToHBoss:
                    connections.Add(_connectionFactory(dungeonData.Nodes[DungeonNodeID.ToHPastBigKeyDoor], node,
                        _bossRequirements[BossPlacementID.ToHBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}