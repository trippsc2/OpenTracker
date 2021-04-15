using System;
using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Mode;

namespace OpenTracker.Models.Nodes.Factories
{
    /// <summary>
    ///     This class contains the creation logic for northeast dark world node connections.
    /// </summary>
    public class NEDarkWorldConnectionFactory : INEDarkWorldConnectionFactory
    {
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        private readonly IPrizeRequirementDictionary _prizeRequirements;
        private readonly IWorldStateRequirementDictionary _worldStateRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly INodeConnection.Factory _connectionFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="complexRequirements">
        ///     The complex requirement dictionary.
        /// </param>
        /// <param name="itemRequirements">
        ///     The item requirement dictionary.
        /// </param>
        /// <param name="prizeRequirements">
        ///     The prize requirement dictionary.
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
        public NEDarkWorldConnectionFactory(
            IComplexRequirementDictionary complexRequirements, IItemRequirementDictionary itemRequirements,
            IPrizeRequirementDictionary prizeRequirements, IWorldStateRequirementDictionary worldStateRequirements,
            IOverworldNodeDictionary overworldNodes, INodeConnection.Factory connectionFactory)
        {
            _complexRequirements = complexRequirements;
            _itemRequirements = itemRequirements;
            _prizeRequirements = prizeRequirements;
            _worldStateRequirements = worldStateRequirements;
            _overworldNodes = overworldNodes;
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node)
        {
            return id switch
            {
                OverworldNodeID.DWWitchArea => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWWitchArea], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.FluteInverted], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWLakeHyliaFlippers], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node)
                },
                OverworldNodeID.DWWitchAreaNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWWitchArea], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.CatfishArea => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.ZoraArea], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 1)])
                },
                OverworldNodeID.DarkWorldEast => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldStandardOpen], node,
                        _prizeRequirements[(PrizeType.Aga1, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.FluteInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldMirror], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldSouthHammer], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWEastPortalNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWLakeHyliaFlippers], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node)
                },
                OverworldNodeID.DarkWorldEastStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldEast], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.DarkWorldEastNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldEast], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.DarkWorldEastHammer => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)])
                },
                OverworldNodeID.FatFairyEntrance => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                        _prizeRequirements[(PrizeType.RedCrystal, 2)])
                },
                OverworldNodeID.DWEastPortal => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWEastPortalStandardOpen], node,
                        _itemRequirements[(ItemType.Gloves, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldEastHammer], node)
                },
                OverworldNodeID.DWEastPortalInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWEastPortal], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DWEastPortalNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWEastPortal], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}