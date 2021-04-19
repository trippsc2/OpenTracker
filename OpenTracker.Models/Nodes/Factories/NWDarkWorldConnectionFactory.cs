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
using OpenTracker.Models.Requirements.Node;

namespace OpenTracker.Models.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for northwest dark world node connections.
    /// </summary>
    public class NWDarkWorldConnectionFactory : INWDarkWorldConnectionFactory
    {
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        private readonly INodeRequirementDictionary _nodeRequirements;
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
        /// <param name="nodeRequirements">
        ///     The node requirement dictionary.
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
        public NWDarkWorldConnectionFactory(
            IAlternativeRequirementDictionary alternativeRequirements,
            IComplexRequirementDictionary complexRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
            IItemRequirementDictionary itemRequirements, INodeRequirementDictionary nodeRequirements,
            IWorldStateRequirementDictionary worldStateRequirements, IOverworldNodeDictionary overworldNodes,
            INodeConnection.Factory connectionFactory)
        {
            _alternativeRequirements = alternativeRequirements;
            _complexRequirements = complexRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
            _itemRequirements = itemRequirements;
            _nodeRequirements = nodeRequirements;
            _worldStateRequirements = worldStateRequirements;
            _overworldNodes = overworldNodes;
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node)
        {
            return id switch
            {
                OverworldNodeID.DWKakarikoPortal => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWKakarikoPortalStandardOpen], node,
                        _itemRequirements[(ItemType.Gloves, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node)
                },
                OverworldNodeID.DWKakarikoPortalInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWKakarikoPortal], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DarkWorldWest => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceNoneInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.FluteInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldMirror], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWKakarikoPortal], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SkullWoodsBackArea], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All]
                        }]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.BumperCaveEntry], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.BumperCaveTop], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.HammerHouseNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 2)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                        _itemRequirements[(ItemType.Hookshot, 1)])
                },
                OverworldNodeID.DarkWorldWestNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWest], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWest], node,
                        _complexRequirements[ComplexRequirementType.SuperBunnyMirror])
                },
                OverworldNodeID.DarkWorldWestLift2 => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 2)])
                },
                OverworldNodeID.SkullWoodsBackArea => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWest], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All]
                        }])
                },
                OverworldNodeID.SkullWoodsBackAreaNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SkullWoodsBackArea], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.SkullWoodsBack => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SkullWoodsBackAreaNotBunny], node,
                        _itemRequirements[(ItemType.FireRod, 1)])
                },
                OverworldNodeID.BumperCaveEntry => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEntry], node,
                        _complexRequirements[ComplexRequirementType.LWMirror])
                },
                OverworldNodeID.BumperCaveEntryNonEntrance => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BumperCaveEntry], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.BumperCaveFront => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEntryNonEntrance], node,
                        _worldStateRequirements[WorldState.Inverted]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BumperCaveEntryNonEntrance], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.BumperCaveNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BumperCaveFront], node,
                        _itemRequirements[(ItemType.MoonPearl, 1)])
                },
                OverworldNodeID.BumperCavePastGap => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BumperCaveNotBunny], node,
                        _complexRequirements[ComplexRequirementType.BumperCaveGap])
                },
                OverworldNodeID.BumperCaveBack => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BumperCavePastGap], node,
                        _itemRequirements[(ItemType.Cape, 1)])
                },
                OverworldNodeID.BumperCaveTop => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainExit], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BumperCaveBack], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.HammerHouse => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)])
                },
                OverworldNodeID.HammerHouseNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.HammerHouse], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.HammerPegsArea => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldMirror], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldWestLift2], node)
                },
                OverworldNodeID.HammerPegs => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.HammerPegsArea], node,
                        _itemRequirements[(ItemType.Hammer, 1)])
                },
                OverworldNodeID.PurpleChest => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.Blacksmith], node,
                        _nodeRequirements[OverworldNodeID.HammerPegsArea])
                },
                OverworldNodeID.BlacksmithPrison => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldMirror], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldWestLift2], node)
                },
                OverworldNodeID.Blacksmith => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BlacksmithPrison], node,
                        _nodeRequirements[OverworldNodeID.LightWorld])
                },
                OverworldNodeID.DWGraveyard => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node)
                },
                OverworldNodeID.DWGraveyardMirror => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWGraveyard], node,
                        _complexRequirements[ComplexRequirementType.DWMirror])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}