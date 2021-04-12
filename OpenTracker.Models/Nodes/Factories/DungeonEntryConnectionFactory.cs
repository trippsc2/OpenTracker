using System;
using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Prizes;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for dungeon entry node connections.
    /// </summary>
    public class DungeonEntryConnectionFactory : IDungeonEntryConnectionFactory
    {
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        private readonly IPrizeRequirementDictionary _prizeRequirements;
        private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;
        private readonly IWorldStateRequirementDictionary _worldStateRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly INodeConnection.Factory _connectionFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="complexRequirements">
        ///     The complex requirement dictionary.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The entrance shuffle requirement dictionary.
        /// </param>
        /// <param name="itemRequirements">
        ///     The item requirement dictionary.
        /// </param>
        /// <param name="prizeRequirements">
        ///     The prize requirement dictionary.
        /// </param>
        /// <param name="sequenceBreakRequirements">
        ///     The sequence break requirement dictionary.
        /// </param>
        /// <param name="worldStateRequirements">
        ///     The world state requirement dictionary.
        /// </param>
        /// <param name="overworldNodes">
        ///     The overworld node dictionary.
        /// </param>
        /// <param name="connectionFactory">
        ///     An Autofac factory for creating new node connections.
        /// </param>
        public DungeonEntryConnectionFactory(
            IComplexRequirementDictionary complexRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
            IItemRequirementDictionary itemRequirements, IPrizeRequirementDictionary prizeRequirements,
            ISequenceBreakRequirementDictionary sequenceBreakRequirements,
            IWorldStateRequirementDictionary worldStateRequirements, IOverworldNodeDictionary overworldNodes,
            INodeConnection.Factory connectionFactory)
        {
            _complexRequirements = complexRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
            _itemRequirements = itemRequirements;
            _prizeRequirements = prizeRequirements;
            _sequenceBreakRequirements = sequenceBreakRequirements;
            _worldStateRequirements = worldStateRequirements;
            
            _overworldNodes = overworldNodes;
            
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node)
        {
            return id switch
            {
                OverworldNodeID.GanonHole => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.HyruleCastleTopInverted], node,
                        _prizeRequirements[(PrizeType.Aga2, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldEastStandardOpen], node,
                        _prizeRequirements[(PrizeType.Aga2, 1)])
                },
                OverworldNodeID.HCSanctuaryEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldNotBunnyOrSuperBunnyMirror], node)
                },
                OverworldNodeID.HCFrontEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldNotBunnyOrDungeonRevive], node)
                },
                OverworldNodeID.HCBackEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.EscapeGrave], node)
                },
                OverworldNodeID.ATEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.AgahnimTowerEntrance], node,
                        _worldStateRequirements[WorldState.StandardOpen]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.GanonsTowerEntrance], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.EPEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldNotBunnyOrDungeonRevive], node)
                },
                OverworldNodeID.DPFrontEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DesertPalaceFrontEntrance], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DesertPalaceFrontEntrance], node,
                        _sequenceBreakRequirements[SequenceBreakType.DungeonRevive])
                },
                OverworldNodeID.DPLeftEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DesertLedge], node)
                },
                OverworldNodeID.DPBackEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DesertBack], node)
                },
                OverworldNodeID.ToHEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DeathMountainWestTopNotBunny], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainWestTop], node,
                        _sequenceBreakRequirements[SequenceBreakType.DungeonRevive])
                },
                OverworldNodeID.PoDEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node)
                },
                OverworldNodeID.SPEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldInvertedNotBunny], node,
                        _itemRequirements[(ItemType.Mirror, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthStandardOpenNotBunny], node,
                        _itemRequirements[(ItemType.Mirror, 1)])
                },
                OverworldNodeID.SWFrontEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWest], node,
                        _sequenceBreakRequirements[SequenceBreakType.DungeonRevive]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.Start], node,
                        _entranceShuffleRequirements[EntranceShuffle.Insanity])
                },
                OverworldNodeID.SWBackEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.SkullWoodsBack], node)
                },
                OverworldNodeID.TTEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node)
                },
                OverworldNodeID.IPEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.IcePalaceIsland], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.IcePalaceIsland], node,
                        _sequenceBreakRequirements[SequenceBreakType.DungeonRevive])
                },
                OverworldNodeID.MMEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.MiseryMireEntrance], node)
                },
                OverworldNodeID.TRFrontEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.TurtleRockFrontEntrance], node)
                },
                OverworldNodeID.TRFrontEntryStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.TRFrontEntry], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.TRFrontEntryStandardOpenEntranceNone => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.TRFrontEntryStandardOpen], node,
                        _entranceShuffleRequirements[EntranceShuffle.None])
                },
                OverworldNodeID.TRFrontToKeyDoors => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.TRFrontEntryStandardOpenEntranceNone], node,
                        _itemRequirements[(ItemType.CaneOfSomaria, 1)])
                },
                OverworldNodeID.TRKeyDoorsToMiddleExit => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.TRFrontToKeyDoors], node,
                        _complexRequirements[ComplexRequirementType.TRKeyDoorsToMiddleExit])
                },
                OverworldNodeID.TRMiddleEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.TurtleRockTunnel], node)
                },
                OverworldNodeID.TRBackEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.TurtleRockSafetyDoor], node)
                },
                OverworldNodeID.GTEntry => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceDungeonAllInsanity], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.AgahnimTowerEntrance], node,
                        _worldStateRequirements[WorldState.Inverted]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.GanonsTowerEntranceStandardOpen], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.GanonsTowerEntranceStandardOpen], node,
                        _sequenceBreakRequirements[SequenceBreakType.DungeonRevive])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}