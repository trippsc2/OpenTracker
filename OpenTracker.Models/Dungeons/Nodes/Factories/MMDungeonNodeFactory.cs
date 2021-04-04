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
    ///     This class contains the creation logic for Misery Mire nodes.
    /// </summary>
    public class MMDungeonNodeFactory : IMMDungeonNodeFactory
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
        public MMDungeonNodeFactory(
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
                case DungeonNodeID.MM:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.MMEntry]));
                    break;
                case DungeonNodeID.MMPastEntranceGap:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MM], node,
                        _requirements[RequirementType.Hookshot]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MM], node,
                        _requirements[RequirementType.BonkOverLedge]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MM], node,
                        _requirements[RequirementType.Hover]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1TopLeftKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1TopRightKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1LeftSideFirstKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                        dungeonData.KeyDoors[KeyDoorID.MMBigChest].Requirement));
                    break;
                case DungeonNodeID.MMB1TopLeftKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.MMB1TopRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.MMB1TopSide:
                    connections.Add(_connectionFactory(dungeonData
                            .Nodes[DungeonNodeID.MMPastEntranceGap], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1TopLeftKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1TopRightKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1PastPortalBigKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1PastBridgeBigKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.MMBridgeBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMB1LobbyBeyondBlueBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1RightSideBeyondBlueBlocks], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1RightSideKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMB1RightSideKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LobbyBeyondBlueBlocks], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1RightSideBeyondBlueBlocks], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.MMB1RightSideBeyondBlueBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSideFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LobbyBeyondBlueBlocks], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1RightSideKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMB1LeftSideFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.MMB1LeftSidePastFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1LeftSideFirstKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastSecondKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1LeftSideSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMB1LeftSideSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastSecondKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.MMB1LeftSidePastSecondKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1LeftSideSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMB1PastFourTorchRoom:
                case DungeonNodeID.MMF1PastFourTorchRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastSecondKeyDoor], node,
                        _requirements[RequirementType.FireSource]));
                    break;
                case DungeonNodeID.MMB1PastPortalBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                        dungeonData.KeyDoors[KeyDoorID.MMPortalBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMBridgeBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1PastBridgeBigKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.MMB1PastBridgeBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node,
                        dungeonData.KeyDoors[KeyDoorID.MMBridgeBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMDarkRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB2PastWorthlessKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB2WorthlessKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1PastBridgeBigKeyDoor], node,
                        _requirements[RequirementType.DarkRoomMM]));
                    break;
                case DungeonNodeID.MMB2WorthlessKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMDarkRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB2PastWorthlessKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.MMB2PastWorthlessKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMDarkRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB2WorthlessKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMB2PastCaneOfSomariaSwitch:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMDarkRoom], node,
                        _requirements[RequirementType.CaneOfSomaria]));
                    break;
                case DungeonNodeID.MMBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB2PastCaneOfSomariaSwitch], node,
                        dungeonData.KeyDoors[KeyDoorID.MMBossRoomBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMBossRoom], node,
                        _requirements[RequirementType.MMBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}