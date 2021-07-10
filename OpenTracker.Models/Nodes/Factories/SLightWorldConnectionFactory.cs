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
    /// This class contains the creation logic for south light world <see cref="INodeConnection"/> objects.
    /// </summary>
    public class SLightWorldConnectionFactory : ISLightWorldConnectionFactory
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
        /// Constructor
        /// </summary>
        /// <param name="alternativeRequirements">
        ///     The <see cref="IAlternativeRequirementDictionary"/>.
        /// </param>
        /// <param name="complexRequirements">
        ///     The <see cref="IComplexRequirementDictionary"/>.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The <see cref="IEntranceShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="itemRequirements">
        ///     The <see cref="IItemRequirementDictionary"/>.
        /// </param>
        /// <param name="sequenceBreakRequirements">
        ///     The <see cref="ISequenceBreakRequirementDictionary"/>.
        /// </param>
        /// <param name="worldStateRequirements">
        ///     The <see cref="IWorldStateRequirementDictionary"/>.
        /// </param>
        /// <param name="overworldNodes">
        ///     The <see cref="IOverworldNodeDictionary"/>.
        /// </param>
        /// <param name="connectionFactory">
        ///     An Autofac factory for creating new <see cref="INodeConnection"/> objects.
        /// </param>
        public SLightWorldConnectionFactory(
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
                OverworldNodeID.RaceGameLedge => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }]),
                        _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldSouthMirror], node)
                },
                OverworldNodeID.RaceGameLedgeNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.RaceGameLedge], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.SouthOfGroveLedge => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldSouthMirror], node)
                },
                OverworldNodeID.SouthOfGrove => new List<INodeConnection>()
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SouthOfGroveLedge], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SouthOfGroveLedge], node,
                        _complexRequirements[ComplexRequirementType.SuperBunnyMirror])
                },
                OverworldNodeID.GroveDiggingSpot => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        _itemRequirements[(ItemType.Shovel, 1)])
                },
                OverworldNodeID.DesertLedge => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DesertBackNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.MireAreaMirror], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DPFrontEntry], node,
                        _entranceShuffleRequirements[EntranceShuffle.None])
                },
                OverworldNodeID.DesertLedgeNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DesertLedge], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.DesertBack => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DesertLedgeNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.MireAreaMirror], node)
                },
                OverworldNodeID.DesertBackNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DesertBack], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.CheckerboardLedge => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.MireAreaMirror], node)
                },
                OverworldNodeID.CheckerboardLedgeNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.CheckerboardLedge], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.CheckerboardCave => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.CheckerboardLedgeNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 1)])
                },
                OverworldNodeID.DesertPalaceFrontEntrance => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorld], node,
                        _itemRequirements[(ItemType.Book, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.MireAreaMirror], node)
                },
                OverworldNodeID.BombosTabletLedge => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldSouthMirror], node)
                },
                OverworldNodeID.BombosTablet => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BombosTabletLedge], node,
                        _complexRequirements[ComplexRequirementType.Tablet])
                },
                OverworldNodeID.LWMirePortal => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.FluteStandardOpen], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWMirePortalInverted], node,
                        _itemRequirements[(ItemType.Gloves, 2)])
                },
                OverworldNodeID.LWMirePortalStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWMirePortal], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.LWSouthPortal => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldHammer], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWSouthPortalInverted], node,
                        _itemRequirements[(ItemType.Gloves, 1)])
                },
                OverworldNodeID.LWSouthPortalStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWSouthPortal], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.LWSouthPortalNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWSouthPortal], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.LWLakeHyliaFakeFlippers => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        _sequenceBreakRequirements[SequenceBreakType.FakeFlippersScreenTransition]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion])
                },
                OverworldNodeID.LWLakeHyliaFlippers => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        _itemRequirements[(ItemType.Flippers, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.WaterfallFairyNotBunny], node,
                        _itemRequirements[(ItemType.Flippers, 1)])
                },
                OverworldNodeID.LWLakeHyliaWaterWalk => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        _complexRequirements[ComplexRequirementType.WaterWalk]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.WaterfallFairyNotBunny], node,
                        _complexRequirements[ComplexRequirementType.WaterWalkFromWaterfallCave])
                },
                OverworldNodeID.Hobo => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.LWLakeHyliaFlippers], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node)
                },
                OverworldNodeID.LakeHyliaIsland => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWLakeHyliaFlippers], node,
                        _worldStateRequirements[WorldState.Inverted]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWLakeHyliaFlippers], node,
                        _complexRequirements[ComplexRequirementType.DWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node,
                        _worldStateRequirements[WorldState.Inverted]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node,
                        _worldStateRequirements[WorldState.Inverted]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node,
                        _complexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.LakeHyliaFairyIsland => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.LWLakeHyliaFlippers], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.IcePalaceIsland], node,
                        _complexRequirements[ComplexRequirementType.DWMirror]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node)
                },
                OverworldNodeID.LakeHyliaFairyIslandStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LakeHyliaFairyIsland], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}