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
    ///     This class contains the creation logic for Ganon's Tower nodes.
    /// </summary>
    public class GTDungeonNodeFactory : IGTDungeonNodeFactory
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
        public GTDungeonNodeFactory(
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
                case DungeonNodeID.GT:
                    connections.Add(_entryFactory(_requirementNodes[OverworldNodeID.GTEntry]));
                    break;
                case DungeonNodeID.GTBobsTorch:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                        _requirements[RequirementType.Torch]));
                    break;
                case DungeonNodeID.GT1FLeft:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRight], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FLeftToRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT1FLeftToRightKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRight], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT1FLeftPastHammerBlocks:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                        _requirements[RequirementType.Hammer]));
                    break;
                case DungeonNodeID.GT1FLeftDMsRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                        _requirements[RequirementType.Hookshot]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                        _requirements[RequirementType.Hover]));
                    break;
                case DungeonNodeID.GT1FLeftPastBonkableGaps:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                        _requirements[RequirementType.Hookshot]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                        _requirements[RequirementType.BonkOverLedge]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                        _requirements[RequirementType.Hover]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftMapChestRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FMapChestRoomKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT1FMapChestRoomKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftMapChestRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT1FLeftMapChestRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FMapChestRoomKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT1FSpikeTrapPortalRoomKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT1FLeftSpikeTrapPortalRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT1FLeftFiresnakeRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT1FLeftPastFiresnakeRoomGap:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftFiresnakeRoom], node,
                        _requirements[RequirementType.Hookshot]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FFiresnakeRoomKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftFiresnakeRoom], node,
                        _requirements[RequirementType.Hover]));
                    break;
                case DungeonNodeID.GT1FFiresnakeRoomKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomGap], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomGap], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FFiresnakeRoomKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT1FLeftRandomizerRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT1FRight:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FLeftToRightKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT1FRightTileRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRight], node,
                        _requirements[RequirementType.CaneOfSomaria]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FTileRoomKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT1FTileRoomKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRightTileRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT1FRightFourTorchRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRightTileRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FTileRoomKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT1FRightCompassRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom], node,
                        _requirements[RequirementType.FireRod]));
                    break;
                case DungeonNodeID.GT1FRightPastCompassRoomPortal:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRightCompassRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FCollapsingWalkwayKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT1FCollapsingWalkwayKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRightPastCompassRoomPortal], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT1FRightCollapsingWalkway:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRightPastCompassRoomPortal], node,
                        dungeonData.KeyDoors[KeyDoorID.GT1FCollapsingWalkwayKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT1FBottomRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FLeftRandomizerRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GTBoss1:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FBottomRoom], node,
                        _requirements[RequirementType.GTBoss1]));
                    break;
                case DungeonNodeID.GTB1BossChests:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GTBoss1], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GTBigChest:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT1FBottomRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.GTBigChest].Requirement));
                    break;
                case DungeonNodeID.GT3FPastRedGoriyaRooms:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT], node,
                        _requirements[RequirementType.RedEyegoreGoriya]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.GT3FBigKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT], node,
                        _requirements[RequirementType.MimicClip]));
                    break;
                case DungeonNodeID.GT3FBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT3FPastRedGoriyaRooms], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT3FPastBigKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT3FPastRedGoriyaRooms], node,
                        dungeonData.KeyDoors[KeyDoorID.GT3FBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GTBoss2:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor], node,
                        _requirements[RequirementType.GTBoss2]));
                    break;
                case DungeonNodeID.GT4FPastBoss2:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GTBoss2], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT5FPastFourTorchRooms:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT4FPastBoss2], node,
                        _requirements[RequirementType.FireSource]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.GT6FFirstKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT6FFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT5FPastFourTorchRooms], node,
                        _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT6FPastFirstKeyDoor:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT5FPastFourTorchRooms], node,
                        dungeonData.KeyDoors[KeyDoorID.GT6FFirstKeyDoor].Requirement));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                        dungeonData.KeyDoors[KeyDoorID.GT6FSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GT6FSecondKeyDoor:
                    connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node,
                    _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                        _requirements[RequirementType.NoRequirement]));
                    break;
                case DungeonNodeID.GT6FBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node,
                        dungeonData.KeyDoors[KeyDoorID.GT6FSecondKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GTBoss3:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                        _requirements[RequirementType.GTBoss3]));
                    break;
                case DungeonNodeID.GTBoss3Item:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GTBoss3], node,
                        _requirements[RequirementType.Hookshot]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GTBoss3], node,
                        _requirements[RequirementType.Hover]));
                    break;
                case DungeonNodeID.GT6FPastBossRoomGap:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GTBoss3Item],
                        node, _requirements[RequirementType.NoRequirement]));
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                        _requirements[RequirementType.Hover]));
                    break;
                case DungeonNodeID.GTFinalBossRoom:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GT6FPastBossRoomGap], node,
                        dungeonData.KeyDoors[KeyDoorID.GT7FBigKeyDoor].Requirement));
                    break;
                case DungeonNodeID.GTFinalBoss:
                    connections.Add(_connectionFactory(
                        dungeonData.Nodes[DungeonNodeID.GTFinalBossRoom], node,
                        _requirements[RequirementType.GTFinalBoss]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}