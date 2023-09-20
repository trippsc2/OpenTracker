using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Items;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.BossShuffle;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Dungeons.Nodes.Factories;

/// <summary>
/// This class contains the creation logic for Thieves' Town nodes.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class TTDungeonNodeFactory : ITTDungeonNodeFactory
{
    private readonly IBossRequirementDictionary _bossRequirements;
    private readonly IBossShuffleRequirementDictionary _bossShuffleRequirements;
    private readonly IItemRequirementDictionary _itemRequirements;
        
    private readonly IOverworldNodeDictionary _overworldNodes;

    private readonly IEntryNodeConnection.Factory _entryFactory;
    private readonly INodeConnection.Factory _connectionFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bossRequirements">
    ///     The <see cref="IBossRequirementDictionary"/>.
    /// </param>
    /// <param name="bossShuffleRequirements">
    ///     The <see cref="IBossShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="itemRequirements">
    ///     The <see cref="IItemRequirementDictionary"/>.
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
    public TTDungeonNodeFactory(
        IBossRequirementDictionary bossRequirements, IBossShuffleRequirementDictionary bossShuffleRequirements,
        IItemRequirementDictionary itemRequirements, IOverworldNodeDictionary overworldNodes,
        IEntryNodeConnection.Factory entryFactory, INodeConnection.Factory connectionFactory)
    {
        _bossRequirements = bossRequirements;
        _bossShuffleRequirements = bossShuffleRequirements;
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
            case DungeonNodeID.TT:
                connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.TTEntry]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.TTBigKeyDoor].Requirement));
                break;
            case DungeonNodeID.TTBigKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TT], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node));
                break;
            case DungeonNodeID.TTPastBigKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TT], node,
                    dungeonData.KeyDoors[KeyDoorID.TTBigKeyDoor].Requirement));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.TTFirstKeyDoor].Requirement));
                break;
            case DungeonNodeID.TTFirstKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node));
                break;
            case DungeonNodeID.TTPastFirstKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.TTFirstKeyDoor].Requirement));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.TTBigChestKeyDoor].Requirement));
                break;
            case DungeonNodeID.TTPastSecondKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.TTSecondKeyDoor].Requirement));
                break;
            case DungeonNodeID.TTBigChestKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor], node));
                break;
            case DungeonNodeID.TTPastBigChestRoomKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.TTBigChestKeyDoor].Requirement));
                break;
            case DungeonNodeID.TTPastHammerBlocks:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastBigChestRoomKeyDoor], node,
                    _itemRequirements[(ItemType.Hammer, 1)]));
                break;
            case DungeonNodeID.TTBigChest:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastHammerBlocks], node,
                    dungeonData.KeyDoors[KeyDoorID.TTBigChest].Requirement));
                break;
            case DungeonNodeID.TTBossRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastBigKeyDoor], node,
                    _bossShuffleRequirements[true]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTPastSecondKeyDoor], node,
                    _bossShuffleRequirements[false]));
                break;
            case DungeonNodeID.TTBoss:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.TTBossRoom], node,
                    _bossRequirements[BossPlacementID.TTBoss]));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}