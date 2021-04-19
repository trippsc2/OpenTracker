using System;
using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for light world death mountain node connections.
    /// </summary>
    public class LWDeathMountainConnectionFactory : ILWDeathMountainConnectionFactory
    {
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;
        private readonly IWorldStateRequirementDictionary _worldStateRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly INodeConnection.Factory _connectionFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="alternativeRequirements">
        ///     The alternative requirement dictionary.
        /// </param>
        /// <param name="complexRequirements">
        ///     The complex requirement dictionary.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The entrance shuffle requirement dictionary.
        /// </param>
        /// <param name="itemRequirements">
        ///     The item requirement dictionary.
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
        ///     An Autofac factory for creating new node collections.
        /// </param>
        public LWDeathMountainConnectionFactory(
            IAlternativeRequirementDictionary alternativeRequirements,
            IComplexRequirementDictionary complexRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
            IItemRequirementDictionary itemRequirements, ISequenceBreakRequirementDictionary sequenceBreakRequirements,
            IWorldStateRequirementDictionary worldStateRequirements, IOverworldNodeDictionary overworldNodes,
            INodeConnection.Factory connectionFactory)
        {
            _alternativeRequirements = alternativeRequirements;
            _complexRequirements = complexRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
            _itemRequirements = itemRequirements;
            _sequenceBreakRequirements = sequenceBreakRequirements;
            _worldStateRequirements = worldStateRequirements;
            
            _overworldNodes = overworldNodes;
            
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node)
        {
            return id switch
            {
                OverworldNodeID.DeathMountainWestBottom => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.FluteStandardOpen], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEntryCaveDark], node,
                        _worldStateRequirements[WorldState.StandardOpen]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainExitCaveDark], node,
                        _worldStateRequirements[WorldState.StandardOpen]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DeathMountainWestTop], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastBottomNotBunny], node,
                        _itemRequirements[(ItemType.Hookshot, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkDeathMountainWestBottomInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkDeathMountainWestBottomMirror], node)
                },
                OverworldNodeID.DeathMountainWestBottomNonEntrance => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainWestBottom], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.DeathMountainWestBottomNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainWestBottom], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.SpectacleRockTop => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainWestTop], node,
                        _worldStateRequirements[WorldState.Inverted]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkDeathMountainWestBottomMirror], node)
                },
                OverworldNodeID.DeathMountainWestTop => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.SpectacleRockTop], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastTopNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkDeathMountainTopMirror], node)
                },
                OverworldNodeID.DeathMountainWestTopNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainWestTop], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.EtherTablet => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainWestTop], node,
                        _complexRequirements[ComplexRequirementType.Tablet])
                },
                OverworldNodeID.DeathMountainEastBottom => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainWestBottomNotBunny], node,
                        _itemRequirements[(ItemType.Hookshot, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DeathMountainEastBottomConnector], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.ParadoxCave], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DeathMountainEastTop], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.SpiralCaveLedge], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.MimicCaveLedge], node,
                            _worldStateRequirements[WorldState.Inverted]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                        _complexRequirements[ComplexRequirementType.DWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainEastBottomInverted], node,
                        _itemRequirements[(ItemType.Gloves, 2)])
                },
                OverworldNodeID.DeathMountainEastBottomNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastBottom], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.DeathMountainEastBottomLift2 => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastBottomNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 2)])
                },
                OverworldNodeID.DeathMountainEastBottomConnector => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.DeathMountainEastBottomLift2], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DeathMountainEastTopConnector], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainEastBottomConnector], node,
                        _complexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.ParadoxCave => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastBottom], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastTop], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.ParadoxCaveSuperBunnyFallInHole => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.ParadoxCave], node,
                        _sequenceBreakRequirements[SequenceBreakType.SuperBunnyFallInHole])
                },
                OverworldNodeID.ParadoxCaveNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.ParadoxCave], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.ParadoxCaveTop => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.ParadoxCaveNotBunny], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.ParadoxCaveSuperBunnyFallInHole], node,
                        _itemRequirements[(ItemType.Sword, 3)])
                },
                OverworldNodeID.DeathMountainEastTop => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainWestTopNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.ParadoxCave], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWTurtleRockTopInvertedNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkDeathMountainTopMirror], node)
                },
                OverworldNodeID.DeathMountainEastTopInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastTop], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DeathMountainEastTopNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastTop], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.DeathMountainEastTopConnector => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.DeathMountainEastTop], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.TurtleRockSafetyDoor], node,
                        _complexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.SpiralCaveLedge => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.DeathMountainEastTop], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.TurtleRockTunnelMirror], node)
                },
                OverworldNodeID.SpiralCave => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SpiralCaveLedge], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SpiralCaveLedge], node,
                        _sequenceBreakRequirements[SequenceBreakType.SuperBunnyFallInHole])
                },
                OverworldNodeID.MimicCaveLedge => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.DeathMountainEastTopInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.TurtleRockTunnelMirror], node)
                },
                OverworldNodeID.MimicCaveLedgeNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.MimicCaveLedge], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.MimicCave => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.MimicCaveLedgeNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)])
                },
                OverworldNodeID.LWFloatingIsland => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.DeathMountainEastTopInverted], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWFloatingIsland], node,
                        _complexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.LWTurtleRockTop => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastTopNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 2)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWTurtleRockTopInverted], node,
                        _itemRequirements[(ItemType.Gloves, 2)])
                },
                OverworldNodeID.LWTurtleRockTopInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWTurtleRockTop], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.LWTurtleRockTopInvertedNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWTurtleRockTopInverted], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.LWTurtleRockTopStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWTurtleRockTop], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}