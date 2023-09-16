using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Complex;

namespace OpenTracker.Models.Dungeons.Nodes.Factories;

/// <summary>
/// This class contains the creation logic for Desert Palace nodes.
/// </summary>
public class DPDungeonNodeFactory : IDPDungeonNodeFactory
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
    public DPDungeonNodeFactory(
        IBossRequirementDictionary bossRequirements, IComplexRequirementDictionary complexRequirements,
        IOverworldNodeDictionary overworldNodes, IEntryNodeConnection.Factory entryFactory,
        INodeConnection.Factory connectionFactory)
    {
        _overworldNodes = overworldNodes;

        _entryFactory = entryFactory;
        _connectionFactory = connectionFactory;
        _bossRequirements = bossRequirements;
        _complexRequirements = complexRequirements;
    }

    public void PopulateNodeConnections(
        IMutableDungeon dungeonData, DungeonNodeID id, INode node, IList<INodeConnection> connections)
    {
        switch (id)
        {
            case DungeonNodeID.DPFront:
                connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.DPFrontEntry]));
                connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.DPLeftEntry]));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DPPastRightKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.DPRightKeyDoor].Requirement));
                break;
            case DungeonNodeID.DPTorch:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DPFront], node,
                    _complexRequirements[ComplexRequirementType.Torch]));
                break;
            case DungeonNodeID.DPBigChest:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DPFront], node,
                    dungeonData.KeyDoors[KeyDoorID.DPBigChest].Requirement));
                break;
            case DungeonNodeID.DPRightKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DPFront], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DPPastRightKeyDoor], node));
                break;
            case DungeonNodeID.DPPastRightKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DPFront], node,
                    dungeonData.KeyDoors[KeyDoorID.DPRightKeyDoor].Requirement));
                break;
            case DungeonNodeID.DPBack:
                connections.Add(_entryFactory(_overworldNodes[OverworldNodeID.DPBackEntry]));
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
                    dungeonData.Nodes[DungeonNodeID.DP2F], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node));
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
                    dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node));
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DP2FPastSecondKeyDoor], node));
                break;
            case DungeonNodeID.DP2FPastSecondKeyDoor:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DP2FPastFirstKeyDoor], node,
                    dungeonData.KeyDoors[KeyDoorID.DP2FSecondKeyDoor].Requirement));
                break;
            case DungeonNodeID.DPPastFourTorchWall:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DP2FPastSecondKeyDoor], node,
                    _complexRequirements[ComplexRequirementType.FireSource]));
                break;
            case DungeonNodeID.DPBossRoom:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DPPastFourTorchWall], node,
                    dungeonData.KeyDoors[KeyDoorID.DPBigKeyDoor].Requirement));
                break;
            case DungeonNodeID.DPBoss:
                connections.Add(_connectionFactory(
                    dungeonData.Nodes[DungeonNodeID.DPBossRoom], node,
                    _bossRequirements[BossPlacementID.DPBoss]));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}