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

namespace OpenTracker.Models.Dungeons.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Misery Mire nodes.
    /// </summary>
    public class MMDungeonNodeFactory : IMMDungeonNodeFactory
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
        ///     The complex requirement dictionary
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
        public MMDungeonNodeFactory(
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
                case DungeonNodeID.MM:
                    connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.MMEntry]));
                    break;
                case DungeonNodeID.MMPastEntranceGap:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MM], node,
                        _itemRequirements[(ItemType.Hookshot, 1)]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MM], node,
                        _complexRequirements[ComplexRequirementType.BonkOverLedge]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MM], node,
                        _complexRequirements[ComplexRequirementType.Hover]));
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
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node));
                    break;
                case DungeonNodeID.MMB1TopRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node));
                    break;
                case DungeonNodeID.MMB1TopSide:
                    connections.Add(_connectionFactory(dungeonData
                            .Nodes[DungeonNodeID.MMPastEntranceGap], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1TopLeftKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1TopRightKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1PastPortalBigKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1PastBridgeBigKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.MMBridgeBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMB1LobbyBeyondBlueBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1RightSideBeyondBlueBlocks], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1RightSideKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMB1RightSideKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LobbyBeyondBlueBlocks], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1RightSideBeyondBlueBlocks], node));
                    break;
                case DungeonNodeID.MMB1RightSideBeyondBlueBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSideFirstKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LobbyBeyondBlueBlocks], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB1RightSideKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMB1LeftSideFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node));
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
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastFirstKeyDoor], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1LeftSidePastSecondKeyDoor], node));
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
                        _complexRequirements[ComplexRequirementType.FireSource]));
                    break;
                case DungeonNodeID.MMB1PastPortalBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap], node,
                        dungeonData.KeyDoors[KeyDoorID.MMPortalBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMBridgeBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1TopSide], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB1PastBridgeBigKeyDoor], node));
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
                        _complexRequirements[ComplexRequirementType.DarkRoomMM]));
                    break;
                case DungeonNodeID.MMB2WorthlessKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMDarkRoom], node));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB2PastWorthlessKeyDoor], node));
                    break;
                case DungeonNodeID.MMB2PastWorthlessKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMDarkRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.MMB2WorthlessKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMB2PastCaneOfSomariaSwitch:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMDarkRoom], node,
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)]));
                    break;
                case DungeonNodeID.MMBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMB2PastCaneOfSomariaSwitch], node,
                        dungeonData.KeyDoors[KeyDoorID.MMBossRoomBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.MMBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.MMBossRoom], node,
                        _bossRequirements[BossPlacementID.MMBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}