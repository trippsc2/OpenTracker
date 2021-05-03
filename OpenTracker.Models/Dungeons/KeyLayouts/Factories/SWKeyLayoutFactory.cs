using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SmallKeyShuffle;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Skull Woods key layouts.
    /// </summary>
    public class SWKeyLayoutFactory : ISWKeyLayoutFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IBigKeyShuffleRequirementDictionary _bigKeyShuffleRequirements;
        private readonly IGuaranteedBossItemsRequirementDictionary _guaranteedBossItemsRequirements;
        private readonly IItemPlacementRequirementDictionary _itemPlacementRequirements;
        private readonly IKeyDropShuffleRequirementDictionary _keyDropShuffleRequirements;
        private readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements;
        
        private readonly IBigKeyLayout.Factory _bigKeyFactory;
        private readonly IEndKeyLayout.Factory _endFactory;
        private readonly ISmallKeyLayout.Factory _smallKeyFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The aggregate requirement dictionary.
        /// </param>
        /// <param name="bigKeyShuffleRequirements">
        ///     The big key shuffle requirement dictionary.
        /// </param>
        /// <param name="guaranteedBossItemsRequirements">
        ///     The guaranteed boss items requirement dictionary.
        /// </param>
        /// <param name="itemPlacementRequirements">
        ///     The item placement requirement dictionary.
        /// </param>
        /// <param name="keyDropShuffleRequirements">
        ///     The key drop shuffle requirement dictionary.
        /// </param>
        /// <param name="smallKeyShuffleRequirements">
        ///     The small key shuffle requirement dictionary.
        /// </param>
        /// <param name="bigKeyFactory">
        ///     An Autofac factory for creating big key layouts.
        /// </param>
        /// <param name="endFactory">
        ///     An Autofac factory for ending key layouts.
        /// </param>
        /// <param name="smallKeyFactory">
        ///     An Autofac factory for creating small key layouts.
        /// </param>
        public SWKeyLayoutFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IBigKeyShuffleRequirementDictionary bigKeyShuffleRequirements,
            IGuaranteedBossItemsRequirementDictionary guaranteedBossItemsRequirements,
            IItemPlacementRequirementDictionary itemPlacementRequirements,
            IKeyDropShuffleRequirementDictionary keyDropShuffleRequirements,
            ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements, IBigKeyLayout.Factory bigKeyFactory,
            IEndKeyLayout.Factory endFactory, ISmallKeyLayout.Factory smallKeyFactory)
        {
            _aggregateRequirements = aggregateRequirements;
            _bigKeyShuffleRequirements = bigKeyShuffleRequirements;
            _guaranteedBossItemsRequirements = guaranteedBossItemsRequirements;
            _itemPlacementRequirements = itemPlacementRequirements;
            _keyDropShuffleRequirements = keyDropShuffleRequirements;
            _smallKeyShuffleRequirements = smallKeyShuffleRequirements;

            _bigKeyFactory = bigKeyFactory;
            _endFactory = endFactory;
            _smallKeyFactory = smallKeyFactory;
        }

        public IList<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            return new List<IKeyLayout>
            {
                _endFactory(_aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[true],
                    _smallKeyShuffleRequirements[true]
                }]),
                _smallKeyFactory(
                    3, new List<DungeonItemID>
                    {
                        DungeonItemID.SWBigKeyChest,
                        DungeonItemID.SWMapChest,
                        DungeonItemID.SWBigChest,
                        DungeonItemID.SWPotPrison,
                        DungeonItemID.SWCompassChest,
                        DungeonItemID.SWPinballRoom,
                        DungeonItemID.SWBridgeRoom
                    },
                    false, new List<IKeyLayout> {_endFactory()}, dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _keyDropShuffleRequirements[false],
                    }]),
                _smallKeyFactory(
                    1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                    new List<IKeyLayout>
                    {
                        _smallKeyFactory(
                            3, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWBigChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom
                            },
                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    dungeon, _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _itemPlacementRequirements[ItemPlacement.Basic],
                        _keyDropShuffleRequirements[false]
                    }]),
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
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            3, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWBigChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom
                            },
                            true, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _keyDropShuffleRequirements[false]
                    }]),
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
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    3, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWBigChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom
                                    },
                                    true, new List<IKeyLayout> {_endFactory()}, dungeon)
                            }, dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _itemPlacementRequirements[ItemPlacement.Basic],
                        _keyDropShuffleRequirements[false]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.SWBoss}, new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            3, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom
                            },
                            false, new List<IKeyLayout> {_endFactory()}, dungeon,
                            _itemPlacementRequirements[ItemPlacement.Advanced]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    3, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom
                                    },
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon, _itemPlacementRequirements[ItemPlacement.Basic])
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _guaranteedBossItemsRequirements[false],
                        _keyDropShuffleRequirements[false]
                    }]),
                _smallKeyFactory(
                    4, new List<DungeonItemID>
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
                    false, new List<IKeyLayout>
                    {
                        _smallKeyFactory(
                            5, new List<DungeonItemID>
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
                            },
                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _keyDropShuffleRequirements[true]
                    }]),
                _smallKeyFactory(
                    1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                    new List<IKeyLayout>
                    {
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
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
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
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
                                    },
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    dungeon, _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _itemPlacementRequirements[ItemPlacement.Basic],
                        _keyDropShuffleRequirements[true]
                    }]),
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
                    },
                    new List<IKeyLayout> {_endFactory()}, _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _guaranteedBossItemsRequirements[false],
                        _keyDropShuffleRequirements[true],
                        _smallKeyShuffleRequirements[true]
                    }]),
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
                    },
                    new List<IKeyLayout> {_endFactory()}, _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _guaranteedBossItemsRequirements[true],
                        _keyDropShuffleRequirements[true],
                        _smallKeyShuffleRequirements[true]
                    }]),
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
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
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
                            true, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
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
                                    },
                                    true, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _keyDropShuffleRequirements[true]
                    }]),
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
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    4, new List<DungeonItemID>
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
                                    true, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            5, new List<DungeonItemID>
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
                                            },
                                            true, new List<IKeyLayout> {_endFactory()}, dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _itemPlacementRequirements[ItemPlacement.Basic],
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.SWSpikeCornerDrop}, new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            5, new List<DungeonItemID>
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
                            false, new List<IKeyLayout> {_endFactory()}, dungeon,
                            _itemPlacementRequirements[ItemPlacement.Advanced]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
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
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon, _itemPlacementRequirements[ItemPlacement.Basic])
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.SWBoss}, new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom,
                                DungeonItemID.SWWestLobbyPot
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom,
                                        DungeonItemID.SWWestLobbyPot,
                                        DungeonItemID.SWSpikeCornerDrop
                                    },
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon, _itemPlacementRequirements[ItemPlacement.Advanced]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    4, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom,
                                        DungeonItemID.SWWestLobbyPot
                                    },
                                    false, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            5, new List<DungeonItemID>
                                            {
                                                DungeonItemID.SWBigKeyChest,
                                                DungeonItemID.SWMapChest,
                                                DungeonItemID.SWPotPrison,
                                                DungeonItemID.SWCompassChest,
                                                DungeonItemID.SWPinballRoom,
                                                DungeonItemID.SWBridgeRoom,
                                                DungeonItemID.SWWestLobbyPot,
                                                DungeonItemID.SWSpikeCornerDrop
                                            },
                                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon, _itemPlacementRequirements[ItemPlacement.Basic])
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _guaranteedBossItemsRequirements[false],
                        _keyDropShuffleRequirements[true]
                    }])
            };
        }
    }
}