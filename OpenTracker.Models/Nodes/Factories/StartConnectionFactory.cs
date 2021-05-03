using System;
using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Mode;

namespace OpenTracker.Models.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for starting node connections.
    /// </summary>
    public class StartConnectionFactory : IStartConnectionFactory
    {
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        private readonly IItemRequirementDictionary _itemRequirements; 
        private readonly IWorldStateRequirementDictionary _worldStateRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly INodeConnection.Factory _connectionFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="alternativeRequirements">
        ///     The alternative requirement dictionary.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The entrance shuffle requirement dictionary.
        /// </param>
        /// <param name="itemRequirements">
        ///     The item requirement dictionary.
        /// </param>
        /// <param name="worldStateRequirements">
        ///     The world state requirement dictionary.
        /// </param>
        /// <param name="overworldNodes">
        ///     The overworld node dictionary.
        /// </param>
        /// <param name="connectionFactory">
        ///     An Autofac factory for creating node connections.
        /// </param>
        public StartConnectionFactory(
            IAlternativeRequirementDictionary alternativeRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
            IItemRequirementDictionary itemRequirements, IWorldStateRequirementDictionary worldStateRequirements,
            IOverworldNodeDictionary overworldNodes, INodeConnection.Factory connectionFactory)
        {
            _alternativeRequirements = alternativeRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
            _itemRequirements = itemRequirements;
            _worldStateRequirements = worldStateRequirements;

            _overworldNodes = overworldNodes;
            
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node)
        {
            return id switch
            {
                OverworldNodeID.EntranceDungeonAllInsanity => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.Start], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        }])
                },
                OverworldNodeID.EntranceNone => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.Start], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }])
                },
                OverworldNodeID.EntranceNoneInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.EntranceNone], node,
                        _worldStateRequirements[WorldState.Inverted])  
                },
                OverworldNodeID.Flute => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.Start], node,
                        _itemRequirements[(ItemType.Flute, 1)])
                },
                OverworldNodeID.FluteActivated => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.Flute], node,
                        _itemRequirements[(ItemType.FluteActivated, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldFlute], node)
                },
                OverworldNodeID.FluteInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.FluteActivated], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.FluteStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.FluteActivated], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}