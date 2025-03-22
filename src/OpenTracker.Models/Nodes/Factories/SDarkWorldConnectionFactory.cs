using System;
using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Nodes.Factories
{
    /// <summary>
    /// This class contains the creation logic for south dark work <see cref="INodeConnection"/> objects.
    /// </summary>
    public class SDarkWorldConnectionFactory : ISDarkWorldConnectionFactory
    {
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        private readonly IPrizeRequirementDictionary _prizeRequirements;
        private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;
        private readonly IWorldStateRequirementDictionary _worldStateRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly INodeConnection.Factory _connectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="complexRequirements">
        ///     The <see cref="IComplexRequirementDictionary"/>.
        /// </param>
        /// <param name="itemRequirements">
        ///     The <see cref="IItemRequirementDictionary"/>.
        /// </param>
        /// <param name="prizeRequirements">
        ///     The <see cref="IPrizeRequirementDictionary"/>.
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
        public SDarkWorldConnectionFactory(
            IComplexRequirementDictionary complexRequirements, IItemRequirementDictionary itemRequirements,
            IPrizeRequirementDictionary prizeRequirements,
            ISequenceBreakRequirementDictionary sequenceBreakRequirements,
            IWorldStateRequirementDictionary worldStateRequirements, IOverworldNodeDictionary overworldNodes,
            INodeConnection.Factory connectionFactory)
        {
            _complexRequirements = complexRequirements;
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
                OverworldNodeID.DarkWorldSouth => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.EntranceNoneInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.FluteInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldMirror], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWSouthPortalNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldWest], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldEastHammer], node)
                },
                OverworldNodeID.DarkWorldSouthInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouth], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DarkWorldSouthStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouth], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.DarkWorldSouthStandardOpenNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthStandardOpen], node,
                        _itemRequirements[(ItemType.MoonPearl, 1)])
                },
                OverworldNodeID.DarkWorldSouthMirror => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouth], node,
                        _complexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.DarkWorldSouthNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouth], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.DarkWorldSouthDash => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                        _itemRequirements[(ItemType.Boots, 1)])
                },
                OverworldNodeID.DarkWorldSouthHammer => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)])
                },
                OverworldNodeID.BuyBigBomb => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldInvertedNotBunny], node,
                        _prizeRequirements[(PrizeType.RedCrystal, 2)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthStandardOpenNotBunny], node,
                        _prizeRequirements[(PrizeType.RedCrystal, 2)])
                },
                OverworldNodeID.BuyBigBombStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BuyBigBomb], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.BigBombToLightWorld => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BuyBigBomb], node,
                        _complexRequirements[ComplexRequirementType.DWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BuyBigBomb], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.BigBombToLightWorldStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BigBombToLightWorld], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.BigBombToDWLakeHylia => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BuyBigBombStandardOpen], node,
                        _complexRequirements[ComplexRequirementType.BombDuplicationMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BuyBigBombStandardOpen], node,
                        _complexRequirements[ComplexRequirementType.BombDuplicationAncillaOverload])
                },
                OverworldNodeID.BigBombToWall => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BuyBigBombStandardOpen], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BigBombToLightWorld], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BigBombToLightWorldStandardOpen], node,
                        _prizeRequirements[(PrizeType.Aga1, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.BigBombToDWLakeHylia], node)
                },
                OverworldNodeID.DWSouthPortal => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWSouthPortalStandardOpen], node,
                        _itemRequirements[(ItemType.Gloves, 1)]),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DarkWorldSouthHammer], node)
                },
                OverworldNodeID.DWSouthPortalInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWSouthPortal], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DWSouthPortalNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWSouthPortal], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.MireArea => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldMirror], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWMirePortal], node)
                },
                OverworldNodeID.MireAreaMirror => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.MireArea], node,
                        _complexRequirements[ComplexRequirementType.DWMirror])
                },
                OverworldNodeID.MireAreaNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.MireArea], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.MireAreaNotBunnyOrSuperBunnyMirror => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.MireAreaNotBunny], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.MireArea], node,
                        _complexRequirements[ComplexRequirementType.SuperBunnyMirror])
                },
                OverworldNodeID.MiseryMireEntrance => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.MireAreaNotBunny], node,
                        _complexRequirements[ComplexRequirementType.MMMedallion])
                },
                OverworldNodeID.DWMirePortal => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.FluteInverted], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWMirePortalStandardOpen], node,
                        _itemRequirements[(ItemType.Gloves, 2)])
                },
                OverworldNodeID.DWMirePortalInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWMirePortal], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DWLakeHyliaFlippers => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                        _itemRequirements[(ItemType.Flippers, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                        _itemRequirements[(ItemType.Flippers, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                        _itemRequirements[(ItemType.Flippers, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                        _itemRequirements[(ItemType.Flippers, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                        _itemRequirements[(ItemType.Flippers, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.IcePalaceIslandInverted], node,
                        _itemRequirements[(ItemType.Flippers, 1)])
                },
                OverworldNodeID.DWLakeHyliaFakeFlippers => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                        _sequenceBreakRequirements[SequenceBreakType.FakeFlippersQirnJump]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                        _sequenceBreakRequirements[SequenceBreakType.FakeFlippersFairyRevival]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.IcePalaceIslandInverted], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersFairyRevival]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.IcePalaceIslandInverted], node,
                        _complexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                },
                OverworldNodeID.DWLakeHyliaWaterWalk => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                        _complexRequirements[ComplexRequirementType.WaterWalk])
                },
                OverworldNodeID.IcePalaceIsland => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LakeHyliaFairyIsland], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LakeHyliaFairyIslandStandardOpen], node,
                        _itemRequirements[(ItemType.Gloves, 2)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWLakeHyliaFlippers], node,
                        _worldStateRequirements[WorldState.Inverted]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node,
                        _worldStateRequirements[WorldState.Inverted]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.IcePalaceIslandInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.IcePalaceIsland], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.DarkWorldSouthEast => new List<INodeConnection>
                {
                    _connectionFactory(_overworldNodes[OverworldNodeID.FluteInverted], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldMirror], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWLakeHyliaFlippers], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node),
                    _connectionFactory(_overworldNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node)
                },
                OverworldNodeID.DarkWorldSouthEastNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthEast], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyDW])
                },
                OverworldNodeID.DarkWorldSouthEastLift1 => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 1)])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}