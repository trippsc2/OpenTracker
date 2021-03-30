using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Skull Woods key layouts.
    /// </summary>
    public class SWKeyLayoutFactory : ISWKeyLayoutFactory
    {
        private readonly IRequirementDictionary _requirements;
        
        private readonly BigKeyLayout.Factory _bigKeyFactory;
        private readonly EndKeyLayout.Factory _endFactory;
        private readonly SmallKeyLayout.Factory _smallKeyFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        /// The requirement dictionary.
        /// </param>
        /// <param name="bigKeyFactory">
        /// An Autofac factory for creating big key layouts.
        /// </param>
        /// <param name="endFactory">
        /// An Autofac factory for ending key layouts.
        /// </param>
        /// <param name="smallKeyFactory">
        /// An Autofac factory for creating small key layouts.
        /// </param>
        public SWKeyLayoutFactory(
            IRequirementDictionary requirements, BigKeyLayout.Factory bigKeyFactory, EndKeyLayout.Factory endFactory,
            SmallKeyLayout.Factory smallKeyFactory)
        {
            _requirements = requirements;
            
            _bigKeyFactory = bigKeyFactory;
            _endFactory = endFactory;
            _smallKeyFactory = smallKeyFactory;
        }
        
        public List<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            return new()
            {
                    _endFactory(_requirements[RequirementType.AllKeyShuffle]),
                    _smallKeyFactory(3,
                        new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWBigChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom
                        }, false,
                        new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
                        _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnlyItemPlacementAdvanced]),
                    _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                        new List<IKeyLayout>
                        {
                            _smallKeyFactory(3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon,
                        _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnlyItemPlacementBasic]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWBigChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom
                        },
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom
                                }, true,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOffItemPlacementAdvanced]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWBigChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWBridgeRoom
                        },
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOffItemPlacementBasic]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.SWBoss},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.SmallKeyShuffleOffItemPlacementAdvanced]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.SmallKeyShuffleOffItemPlacementBasic])
                        },
                        _requirements[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOff]),
                    _smallKeyFactory(4,
                        new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWBigChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom,
                            DungeonItemID.SWWestLobbyPot
                        }, false,
                        new List<IKeyLayout>
                        {
                            _smallKeyFactory(5,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWWestLobbyPot,
                                    DungeonItemID.SWSpikeCornerDrop
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon,
                        _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnlyItemPlacementAdvanced]),
                    _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                        new List<IKeyLayout>
                        {
                            _smallKeyFactory(4,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWWestLobbyPot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(5,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom,
                                            DungeonItemID.SWWestLobbyPot,
                                            DungeonItemID.SWSpikeCornerDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon,
                        _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnlyItemPlacementBasic]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWBigChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom,
                            DungeonItemID.SWBoss,
                            DungeonItemID.SWWestLobbyPot,
                            DungeonItemID.SWSpikeCornerDrop
                        }, new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])},
                        _requirements[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffSmallKeyShuffleOnly]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWBigChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom,
                            DungeonItemID.SWWestLobbyPot,
                            DungeonItemID.SWSpikeCornerDrop
                        }, new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])},
                        _requirements[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOnSmallKeyShuffleOnly]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWBigChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom,
                            DungeonItemID.SWWestLobbyPot
                        },
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(4,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWWestLobbyPot
                                }, true,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(5,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom,
                                            DungeonItemID.SWWestLobbyPot,
                                            DungeonItemID.SWSpikeCornerDrop
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOffItemPlacementAdvanced]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWBigChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWBridgeRoom,
                            DungeonItemID.SWWestLobbyPot
                        },
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom,
                                            DungeonItemID.SWWestLobbyPot
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWBigChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom,
                                                    DungeonItemID.SWWestLobbyPot,
                                                    DungeonItemID.SWSpikeCornerDrop
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOffItemPlacementBasic]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.SWSpikeCornerDrop},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(5,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWWestLobbyPot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.SmallKeyShuffleOffItemPlacementAdvanced]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(5,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom,
                                            DungeonItemID.SWWestLobbyPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.SmallKeyShuffleOffItemPlacementBasic])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.SWBoss},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(4,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWWestLobbyPot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(5,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom,
                                            DungeonItemID.SWWestLobbyPot,
                                            DungeonItemID.SWSpikeCornerDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.SmallKeyShuffleOffItemPlacementAdvanced]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom,
                                            DungeonItemID.SWWestLobbyPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom,
                                                    DungeonItemID.SWWestLobbyPot,
                                                    DungeonItemID.SWSpikeCornerDrop
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.SmallKeyShuffleOffItemPlacementBasic])
                        },
                        _requirements[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffBigKeyShuffleOff])
                };
        }
    }
}