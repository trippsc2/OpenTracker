using System;
using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Crystal;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for dark world death mountain node connections.
    /// </summary>
    public class DWDeathMountainConnectionFactory : IDWDeathMountainConnectionFactory
    {
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly ICrystalRequirement _crystalRequirement;
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
        /// <param name="crystalRequirement">
        ///     The crystal requirement.
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
        ///     An Autofac factory for creating new node connections.
        /// </param>
        public DWDeathMountainConnectionFactory(IAlternativeRequirementDictionary alternativeRequirements, IComplexRequirementDictionary complexRequirements, ICrystalRequirement crystalRequirement, IEntranceShuffleRequirementDictionary entranceShuffleRequirements, IItemRequirementDictionary itemRequirements, ISequenceBreakRequirementDictionary sequenceBreakRequirements, IWorldStateRequirementDictionary worldStateRequirements, IOverworldNodeDictionary overworldNodes, INodeConnection.Factory connectionFactory)
        {
            _alternativeRequirements = alternativeRequirements;
            _complexRequirements = complexRequirements;
            _crystalRequirement = crystalRequirement;
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
                OverworldNodeID.DarkDeathMountainWestBottom => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.FluteInverted], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEntryCaveDark], node,
                        _worldStateRequirements[WorldState.Inverted]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainWestBottom], node,
                        _worldStateRequirements[WorldState.StandardOpen]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainWestBottom], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkDeathMountainTop], node)
                },
                OverworldNodeID.DarkDeathMountainWestBottomInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DarkDeathMountainWestBottomNonEntrance => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.DarkDeathMountainWestBottomMirror => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                        _complexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.DarkDeathMountainWestBottomNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.SpikeCavePastHammerBlocks => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainWestBottomNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)])
                },
                OverworldNodeID.SpikeCavePastSpikes => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SpikeCavePastHammerBlocks], node,
                        _complexRequirements[ComplexRequirementType.SpikeCave])
                },
                OverworldNodeID.SpikeCaveChest => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SpikeCavePastSpikes], node,
                        _itemRequirements[(ItemType.Gloves, 1)])
                },
                OverworldNodeID.DarkDeathMountainTop => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainWestTop], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastTop], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkDeathMountainWestBottomInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.SuperBunnyCave], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWFloatingIsland], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWTurtleRockTop], node)
                },
                OverworldNodeID.DarkDeathMountainTopInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainTop], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DarkDeathMountainTopStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainTop], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.DarkDeathMountainTopMirror => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainTop], node,
                        _complexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.DarkDeathMountainTopNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainTop], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.SuperBunnyCave => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainTop], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.SuperBunnyCaveChests => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SuperBunnyCave], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SuperBunnyCave], node,
                        _sequenceBreakRequirements[SequenceBreakType.SuperBunnyFallInHole])
                },
                OverworldNodeID.GanonsTowerEntrance => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkDeathMountainTopInverted], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainTopStandardOpen], node,
                        _crystalRequirement)
                },
                OverworldNodeID.GanonsTowerEntranceStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.GanonsTowerEntrance], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.DWFloatingIsland => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWFloatingIsland], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.HookshotCaveEntrance], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.HookshotCaveEntrance => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainTopNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 1)])
                },
                OverworldNodeID.HookshotCaveEntranceHookshot => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.HookshotCaveEntrance], node,
                        _itemRequirements[(ItemType.Hookshot, 1)])
                },
                OverworldNodeID.HookshotCaveEntranceHover => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.HookshotCaveEntrance], node,
                        _complexRequirements[ComplexRequirementType.Hover])
                },
                OverworldNodeID.HookshotCaveBonkableChest => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.HookshotCaveEntranceHookshot], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.HookshotCaveEntrance], node,
                        _complexRequirements[ComplexRequirementType.BonkOverLedge]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.HookshotCaveEntranceHover], node)
                },
                OverworldNodeID.HookshotCaveBack => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.HookshotCaveEntranceHookshot], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.HookshotCaveEntranceHover], node)
                },
                OverworldNodeID.DWTurtleRockTop => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWTurtleRockTopStandardOpen], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkDeathMountainTopInverted], node)
                },
                OverworldNodeID.DWTurtleRockTopInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWTurtleRockTop], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DWTurtleRockTopNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWTurtleRockTop], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.TurtleRockFrontEntrance => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWTurtleRockTopNotBunny], node,
                        _complexRequirements[ComplexRequirementType.TRMedallion])
                },
                OverworldNodeID.DarkDeathMountainEastBottom => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastBottom], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastBottomLift2], node,
                        _worldStateRequirements[WorldState.StandardOpen]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkDeathMountainTop], node)
                },
                OverworldNodeID.DarkDeathMountainEastBottomInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DarkDeathMountainEastBottomConnector => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.TurtleRockTunnel => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SpiralCaveLedge], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.MimicCaveLedge], node,
                        _complexRequirements[ComplexRequirementType.LWMirror])
                },
                OverworldNodeID.TurtleRockTunnelMirror => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.TurtleRockTunnel], node,
                        _complexRequirements[ComplexRequirementType.DWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.TRKeyDoorsToMiddleExit], node,
                        _complexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.TurtleRockSafetyDoor => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEastTopConnector], node,
                        _complexRequirements[ComplexRequirementType.LWMirror])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}