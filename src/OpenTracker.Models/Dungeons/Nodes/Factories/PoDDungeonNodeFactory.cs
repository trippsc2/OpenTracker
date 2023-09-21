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
/// This class contains the creation logic for Palace of Darkness nodes.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class PoDDungeonNodeFactory : IPoDDungeonNodeFactory
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
    public PoDDungeonNodeFactory(
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
            case DungeonNodeID.PoD:
                connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.PoDEntry]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDFrontKeyDoor].Requirement));
                break;
            case DungeonNodeID.PoDPastFirstRedGoriyaRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoD], node,
                    _complexRequirements[ComplexRequirementType.RedEyegoreGoriya]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoD], node,
                    _complexRequirements[ComplexRequirementType.CameraUnlock]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoD], node,
                    _sequenceBreakRequirements[SequenceBreakType.MimicClip]));
                break;
            case DungeonNodeID.PoDFrontKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoD], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node));
                break;
            case DungeonNodeID.PoDLobbyArena:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoD], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDFrontKeyDoor].Requirement));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastFirstRedGoriyaRoom], node,
                    _itemRequirements[(ItemType.Hammer, 1)]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDCollapsingWalkwayKeyDoor].Requirement));
                break;
            case DungeonNodeID.PoDBigKeyChestArea:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDBigKeyChestKeyDoor].Requirement));
                break;
            case DungeonNodeID.PoDCollapsingWalkwayKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node));
                break;
            case DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDCollapsingWalkwayKeyDoor].Requirement));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDDarkMazeKeyDoor].Requirement));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDHarmlessHellwayRoom], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDHarmlessHellwayKeyDoor].Requirement));
                break;
            case DungeonNodeID.PoDDarkBasement:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                    _complexRequirements[ComplexRequirementType.DarkRoomPoDDarkBasement]));
                break;
            case DungeonNodeID.PoDHarmlessHellwayKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDHarmlessHellwayRoom], node));
                break;
            case DungeonNodeID.PoDHarmlessHellwayRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDHarmlessHellwayKeyDoor].Requirement));
                break;
            case DungeonNodeID.PoDDarkMazeKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor], node));
                break;
            case DungeonNodeID.PoDPastDarkMazeKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDDarkMazeKeyDoor].Requirement));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDDarkMaze], node));
                break;
            case DungeonNodeID.PoDDarkMaze:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastDarkMazeKeyDoor], node,
                    _complexRequirements[ComplexRequirementType.DarkRoomPoDDarkMaze]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDBigChestLedge], node,
                    _complexRequirements[ComplexRequirementType.DarkRoomPoDDarkMaze]));
                break;
            case DungeonNodeID.PoDBigChestLedge:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDDarkMaze], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor], node,
                    _sequenceBreakRequirements[SequenceBreakType.BombJumpPoDHammerJump]));
                break;
            case DungeonNodeID.PoDBigChest:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDBigChestLedge], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDBigChest].Requirement));
                break;
            case DungeonNodeID.PoDPastSecondRedGoriyaRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                    _complexRequirements[ComplexRequirementType.RedEyegoreGoriya]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDLobbyArena], node,
                    _sequenceBreakRequirements[SequenceBreakType.MimicClip]));
                break;
            case DungeonNodeID.PoDPastBowStatue:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastSecondRedGoriyaRoom], node,
                    _itemRequirements[(ItemType.Bow, 1)]));
                break;
            case DungeonNodeID.PoDBossAreaDarkRooms:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastBowStatue], node,
                    _complexRequirements[ComplexRequirementType.DarkRoomPoDBossArea]));
                break;
            case DungeonNodeID.PoDPastHammerBlocks:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDBossAreaDarkRooms], node,
                    _itemRequirements[(ItemType.Hammer, 1)]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDBossAreaKeyDoor].Requirement));
                break;
            case DungeonNodeID.PoDBossAreaKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastHammerBlocks], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor], node));
                break;
            case DungeonNodeID.PoDPastBossAreaKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastHammerBlocks], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDBossAreaKeyDoor].Requirement));
                break;
            case DungeonNodeID.PoDBossRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.PoDBigKeyDoor].Requirement));
                break;
            case DungeonNodeID.PoDBoss:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.PoDBossRoom], node,
                    _bossRequirements[BossPlacementID.PoDBoss]));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}