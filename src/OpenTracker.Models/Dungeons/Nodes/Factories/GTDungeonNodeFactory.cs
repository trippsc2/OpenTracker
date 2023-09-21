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
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Dungeons.Nodes.Factories;

/// <summary>
/// This class contains the creation logic for Ganon's Tower nodes.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class GTDungeonNodeFactory : IGTDungeonNodeFactory
{
    private readonly IBossRequirementDictionary _bossRequirements;
    private readonly IComplexRequirementDictionary _complexRequirements;
    private readonly IItemRequirementDictionary _itemRequirements;
    private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;
        
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
    /// <param name="itemRequirements">
    ///     The <see cref="IItemRequirementDictionary"/>.
    /// </param>
    /// <param name="sequenceBreakRequirements">
    ///     The <see cref="ISequenceBreakRequirementDictionary"/>.
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
    public GTDungeonNodeFactory(
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
            case DungeonNodeID.GT:
                connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.GTEntry]));
                break;
            case DungeonNodeID.GTBobsTorch:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                    _complexRequirements[ComplexRequirementType.Torch]));
                break;
            case DungeonNodeID.GT1FLeft:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRight], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FLeftToRightKeyDoor].Requirement));
                break;
            case DungeonNodeID.GT1FLeftToRightKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeft], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRight], node));
                break;
            case DungeonNodeID.GT1FLeftPastHammerBlocks:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                    _itemRequirements[(ItemType.Hammer, 1)]));
                break;
            case DungeonNodeID.GT1FLeftDMsRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                    _itemRequirements[(ItemType.Hookshot, 1)]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                    _complexRequirements[ComplexRequirementType.Hover]));
                break;
            case DungeonNodeID.GT1FLeftPastBonkableGaps:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                    _itemRequirements[(ItemType.Hookshot, 1)]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                    _complexRequirements[ComplexRequirementType.BonkOverLedge]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastHammerBlocks], node,
                    _complexRequirements[ComplexRequirementType.Hover]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftMapChestRoom], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FMapChestRoomKeyDoor].Requirement));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor].Requirement));
                break;
            case DungeonNodeID.GT1FMapChestRoomKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftMapChestRoom], node));
                break;
            case DungeonNodeID.GT1FLeftMapChestRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FMapChestRoomKeyDoor].Requirement));
                break;
            case DungeonNodeID.GT1FSpikeTrapPortalRoomKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom], node));
                break;
            case DungeonNodeID.GT1FLeftSpikeTrapPortalRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastBonkableGaps], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor].Requirement));
                break;
            case DungeonNodeID.GT1FLeftFiresnakeRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftSpikeTrapPortalRoom], node));
                break;
            case DungeonNodeID.GT1FLeftPastFiresnakeRoomGap:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftFiresnakeRoom], node,
                    _itemRequirements[(ItemType.Hookshot, 1)]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FFiresnakeRoomKeyDoor].Requirement));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftFiresnakeRoom], node,
                    _complexRequirements[ComplexRequirementType.Hover]));
                break;
            case DungeonNodeID.GT1FFiresnakeRoomKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomGap], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor], node));
                break;
            case DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomGap], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FFiresnakeRoomKeyDoor].Requirement));
                break;
            case DungeonNodeID.GT1FLeftRandomizerRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor], node));
                break;
            case DungeonNodeID.GT1FRight:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeft], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FLeftToRightKeyDoor].Requirement));
                break;
            case DungeonNodeID.GT1FRightTileRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRight], node,
                    _itemRequirements[(ItemType.CaneOfSomaria, 1)]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FTileRoomKeyDoor].Requirement));
                break;
            case DungeonNodeID.GT1FTileRoomKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRightTileRoom], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom], node));
                break;
            case DungeonNodeID.GT1FRightFourTorchRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRightTileRoom], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FTileRoomKeyDoor].Requirement));
                break;
            case DungeonNodeID.GT1FRightCompassRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRightFourTorchRoom], node,
                    _itemRequirements[(ItemType.FireRod, 1)]));
                break;
            case DungeonNodeID.GT1FRightPastCompassRoomPortal:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRightCompassRoom], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FCollapsingWalkwayKeyDoor].Requirement));
                break;
            case DungeonNodeID.GT1FCollapsingWalkwayKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRightPastCompassRoomPortal], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway], node));
                break;
            case DungeonNodeID.GT1FRightCollapsingWalkway:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRightPastCompassRoomPortal], node,
                    dungeonData.KeyDoors[KeyDoorID.GT1FCollapsingWalkwayKeyDoor].Requirement));
                break;
            case DungeonNodeID.GT1FBottomRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FLeftRandomizerRoom], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FRightCollapsingWalkway], node));
                break;
            case DungeonNodeID.GTBoss1:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FBottomRoom], node,
                    _bossRequirements[BossPlacementID.GTBoss1]));
                break;
            case DungeonNodeID.GTB1BossChests:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GTBoss1], node));
                break;
            case DungeonNodeID.GTBigChest:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT1FBottomRoom], node,
                    dungeonData.KeyDoors[KeyDoorID.GTBigChest].Requirement));
                break;
            case DungeonNodeID.GT3FPastRedGoriyaRooms:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT], node,
                    _complexRequirements[ComplexRequirementType.RedEyegoreGoriya]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.GT3FBigKeyDoor].Requirement));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT], node,
                    _sequenceBreakRequirements[SequenceBreakType.MimicClip]));
                break;
            case DungeonNodeID.GT3FBigKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT3FPastRedGoriyaRooms], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor], node));
                break;
            case DungeonNodeID.GT3FPastBigKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT3FPastRedGoriyaRooms], node,
                    dungeonData.KeyDoors[KeyDoorID.GT3FBigKeyDoor].Requirement));
                break;
            case DungeonNodeID.GTBoss2:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT3FPastBigKeyDoor], node,
                    _bossRequirements[BossPlacementID.GTBoss2]));
                break;
            case DungeonNodeID.GT4FPastBoss2:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GTBoss2], node));
                break;
            case DungeonNodeID.GT5FPastFourTorchRooms:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT4FPastBoss2], node,
                    _complexRequirements[ComplexRequirementType.FireSource]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.GT6FFirstKeyDoor].Requirement));
                break;
            case DungeonNodeID.GT6FFirstKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT5FPastFourTorchRooms], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node));
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
                    dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node));
                break;
            case DungeonNodeID.GT6FBossRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT6FPastFirstKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.GT6FSecondKeyDoor].Requirement));
                break;
            case DungeonNodeID.GTBoss3:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                    _bossRequirements[BossPlacementID.GTBoss3]));
                break;
            case DungeonNodeID.GTBoss3Item:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GTBoss3], node,
                    _itemRequirements[(ItemType.Hookshot, 1)]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GTBoss3], node,
                    _complexRequirements[ComplexRequirementType.Hover]));
                break;
            case DungeonNodeID.GT6FPastBossRoomGap:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GTBoss3Item], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT6FBossRoom], node,
                    _complexRequirements[ComplexRequirementType.Hover]));
                break;
            case DungeonNodeID.GTFinalBossRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GT6FPastBossRoomGap], node,
                    dungeonData.KeyDoors[KeyDoorID.GT7FBigKeyDoor].Requirement));
                break;
            case DungeonNodeID.GTFinalBoss:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.GTFinalBossRoom], node,
                    _bossRequirements[BossPlacementID.GTFinalBoss]));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}