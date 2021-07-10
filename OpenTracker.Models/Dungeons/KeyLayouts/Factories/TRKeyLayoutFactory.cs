using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SmallKeyShuffle;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Turtle Rock key layouts.
    /// </summary>
    public class TRKeyLayoutFactory : ITRKeyLayoutFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IBigKeyShuffleRequirementDictionary _bigKeyShuffleRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        private readonly IKeyDropShuffleRequirementDictionary _keyDropShuffleRequirements;
        private readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements;
        private readonly IWorldStateRequirementDictionary _worldStateRequirements;

        private readonly IBigKeyLayout.Factory _bigKeyFactory;
        private readonly IEndKeyLayout.Factory _endFactory;
        private readonly ISmallKeyLayout.Factory _smallKeyFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The <see cref="IAggregateRequirementDictionary"/>.
        /// </param>
        /// <param name="alternativeRequirements">
        ///     The <see cref="IAlternativeRequirementDictionary"/>.
        /// </param>
        /// <param name="bigKeyShuffleRequirements">
        ///     The <see cref="IBigKeyShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The <see cref="IEntranceShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="keyDropShuffleRequirements">
        ///     The <see cref="IKeyDropShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="smallKeyShuffleRequirements">
        ///     The <see cref="ISmallKeyShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="worldStateRequirements">
        ///     The <see cref="IWorldStateRequirementDictionary"/>.
        /// </param>
        /// <param name="bigKeyFactory">
        ///     An Autofac factory for creating new <see cref="IBigKeyLayout"/> objects.
        /// </param>
        /// <param name="endFactory">
        ///     An Autofac factory for creating new <see cref="IEndKeyLayout"/> objects.
        /// </param>
        /// <param name="smallKeyFactory">
        ///     An Autofac factory for creating new <see cref="ISmallKeyLayout"/> objects.
        /// </param>
        public TRKeyLayoutFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IAlternativeRequirementDictionary alternativeRequirements,
            IBigKeyShuffleRequirementDictionary bigKeyShuffleRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
            IKeyDropShuffleRequirementDictionary keyDropShuffleRequirements,
            ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements,
            IWorldStateRequirementDictionary worldStateRequirements, IBigKeyLayout.Factory bigKeyFactory,
            IEndKeyLayout.Factory endFactory, ISmallKeyLayout.Factory smallKeyFactory)
        {
            _alternativeRequirements = alternativeRequirements;
            _aggregateRequirements = aggregateRequirements;
            _bigKeyShuffleRequirements = bigKeyShuffleRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
            _keyDropShuffleRequirements = keyDropShuffleRequirements;
            _smallKeyShuffleRequirements = smallKeyShuffleRequirements;
            _worldStateRequirements = worldStateRequirements;

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
                    2, new List<DungeonItemID>
                    {
                        DungeonItemID.TRCompassChest,
                        DungeonItemID.TRRollerRoomLeft,
                        DungeonItemID.TRRollerRoomRight,
                        DungeonItemID.TRChainChomps
                    },
                    false, new List<IKeyLayout>
                    {
                        _smallKeyFactory(
                            3, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRBigKeyChest,
                                DungeonItemID.TRBigChest,
                                DungeonItemID.TRCrystarollerRoom
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    4, new List<DungeonItemID>
                                    {
                                        DungeonItemID.TRCompassChest,
                                        DungeonItemID.TRRollerRoomLeft,
                                        DungeonItemID.TRRollerRoomRight,
                                        DungeonItemID.TRChainChomps,
                                        DungeonItemID.TRBigKeyChest,
                                        DungeonItemID.TRBigChest,
                                        DungeonItemID.TRCrystarollerRoom,
                                        DungeonItemID.TRLaserBridgeTopLeft,
                                        DungeonItemID.TRLaserBridgeTopRight,
                                        DungeonItemID.TRLaserBridgeBottomLeft,
                                        DungeonItemID.TRLaserBridgeBottomRight
                                    },
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    dungeon, _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _entranceShuffleRequirements[EntranceShuffle.None],
                        _keyDropShuffleRequirements[false],
                        _worldStateRequirements[WorldState.StandardOpen]
                    }]),
                _smallKeyFactory(
                    4, new List<DungeonItemID>
                    {
                        DungeonItemID.TRCompassChest,
                        DungeonItemID.TRRollerRoomLeft,
                        DungeonItemID.TRRollerRoomRight,
                        DungeonItemID.TRChainChomps,
                        DungeonItemID.TRBigKeyChest,
                        DungeonItemID.TRBigChest,
                        DungeonItemID.TRCrystarollerRoom,
                        DungeonItemID.TRLaserBridgeTopLeft,
                        DungeonItemID.TRLaserBridgeTopRight,
                        DungeonItemID.TRLaserBridgeBottomLeft,
                        DungeonItemID.TRLaserBridgeBottomRight
                    },
                    false, new List<IKeyLayout> {_endFactory()}, dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity],
                            _worldStateRequirements[WorldState.Inverted]
                        }],
                        _bigKeyShuffleRequirements[true],
                        _keyDropShuffleRequirements[false]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.TRCompassChest,
                        DungeonItemID.TRRollerRoomLeft,
                        DungeonItemID.TRRollerRoomRight,
                        DungeonItemID.TRChainChomps
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            2, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps
                            },
                            true, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    3, new List<DungeonItemID>
                                    {
                                        DungeonItemID.TRCompassChest,
                                        DungeonItemID.TRRollerRoomLeft,
                                        DungeonItemID.TRRollerRoomRight,
                                        DungeonItemID.TRChainChomps,
                                        DungeonItemID.TRBigKeyChest,
                                        DungeonItemID.TRBigChest,
                                        DungeonItemID.TRCrystarollerRoom
                                    },
                                    true, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            4, new List<DungeonItemID>
                                            {
                                                DungeonItemID.TRCompassChest,
                                                DungeonItemID.TRRollerRoomLeft,
                                                DungeonItemID.TRRollerRoomRight,
                                                DungeonItemID.TRChainChomps,
                                                DungeonItemID.TRBigKeyChest,
                                                DungeonItemID.TRBigChest,
                                                DungeonItemID.TRCrystarollerRoom,
                                                DungeonItemID.TRLaserBridgeTopLeft,
                                                DungeonItemID.TRLaserBridgeTopRight,
                                                DungeonItemID.TRLaserBridgeBottomLeft,
                                                DungeonItemID.TRLaserBridgeBottomRight
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
                        _entranceShuffleRequirements[EntranceShuffle.None],
                        _keyDropShuffleRequirements[false],
                        _worldStateRequirements[WorldState.StandardOpen]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.TRBigKeyChest}, new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            2, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    3, new List<DungeonItemID>
                                    {
                                        DungeonItemID.TRCompassChest,
                                        DungeonItemID.TRRollerRoomLeft,
                                        DungeonItemID.TRRollerRoomRight,
                                        DungeonItemID.TRChainChomps,
                                        DungeonItemID.TRBigChest,
                                        DungeonItemID.TRCrystarollerRoom
                                    },
                                    false, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            4, new List<DungeonItemID>
                                            {
                                                DungeonItemID.TRCompassChest,
                                                DungeonItemID.TRRollerRoomLeft,
                                                DungeonItemID.TRRollerRoomRight,
                                                DungeonItemID.TRChainChomps,
                                                DungeonItemID.TRBigChest,
                                                DungeonItemID.TRCrystarollerRoom,
                                                DungeonItemID.TRLaserBridgeTopLeft,
                                                DungeonItemID.TRLaserBridgeTopRight,
                                                DungeonItemID.TRLaserBridgeBottomLeft,
                                                DungeonItemID.TRLaserBridgeBottomRight
                                            },
                                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _entranceShuffleRequirements[EntranceShuffle.None],
                        _keyDropShuffleRequirements[false],
                        _worldStateRequirements[WorldState.StandardOpen]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.TRCompassChest,
                        DungeonItemID.TRRollerRoomLeft,
                        DungeonItemID.TRRollerRoomRight,
                        DungeonItemID.TRChainChomps,
                        DungeonItemID.TRCrystarollerRoom,
                        DungeonItemID.TRLaserBridgeTopLeft,
                        DungeonItemID.TRLaserBridgeTopRight,
                        DungeonItemID.TRLaserBridgeBottomLeft,
                        DungeonItemID.TRLaserBridgeBottomRight
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[false]),
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRBigKeyChest,
                                DungeonItemID.TRBigChest,
                                DungeonItemID.TRCrystarollerRoom,
                                DungeonItemID.TRLaserBridgeTopLeft,
                                DungeonItemID.TRLaserBridgeTopRight,
                                DungeonItemID.TRLaserBridgeBottomLeft,
                                DungeonItemID.TRLaserBridgeBottomRight
                            },
                            true, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity],
                            _worldStateRequirements[WorldState.Inverted]
                        }],
                        _bigKeyShuffleRequirements[false],
                        _keyDropShuffleRequirements[false]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.TRBigKeyChest},
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRCrystarollerRoom,
                                DungeonItemID.TRLaserBridgeTopLeft,
                                DungeonItemID.TRLaserBridgeTopRight,
                                DungeonItemID.TRLaserBridgeBottomLeft,
                                DungeonItemID.TRLaserBridgeBottomRight
                            },
                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity],
                            _worldStateRequirements[WorldState.Inverted]
                        }],
                        _bigKeyShuffleRequirements[false],
                        _keyDropShuffleRequirements[false]
                    }]),
                _smallKeyFactory(
                    3, new List<DungeonItemID>
                    {
                        DungeonItemID.TRCompassChest,
                        DungeonItemID.TRRollerRoomLeft,
                        DungeonItemID.TRRollerRoomRight,
                        DungeonItemID.TRChainChomps,
                        DungeonItemID.TRPokey1Drop
                    },
                    false, new List<IKeyLayout>
                    {
                        _smallKeyFactory(
                            5, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRBigChest,
                                DungeonItemID.TRCrystarollerRoom,
                                DungeonItemID.TRPokey1Drop,
                                DungeonItemID.TRPokey2Drop
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    6, new List<DungeonItemID>
                                    {
                                        DungeonItemID.TRCompassChest,
                                        DungeonItemID.TRRollerRoomLeft,
                                        DungeonItemID.TRRollerRoomRight,
                                        DungeonItemID.TRChainChomps,
                                        DungeonItemID.TRBigChest,
                                        DungeonItemID.TRCrystarollerRoom,
                                        DungeonItemID.TRLaserBridgeTopLeft,
                                        DungeonItemID.TRLaserBridgeTopRight,
                                        DungeonItemID.TRLaserBridgeBottomLeft,
                                        DungeonItemID.TRLaserBridgeBottomRight,
                                        DungeonItemID.TRPokey1Drop,
                                        DungeonItemID.TRPokey2Drop
                                    },
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    dungeon,
                   _aggregateRequirements[new HashSet<IRequirement>
                   {
                       _bigKeyShuffleRequirements[true],
                       _entranceShuffleRequirements[EntranceShuffle.None],
                       _keyDropShuffleRequirements[true],
                       _worldStateRequirements[WorldState.StandardOpen]
                   }]),
                _smallKeyFactory(
                    6, new List<DungeonItemID>
                    {
                        DungeonItemID.TRCompassChest,
                        DungeonItemID.TRRollerRoomLeft,
                        DungeonItemID.TRRollerRoomRight,
                        DungeonItemID.TRChainChomps,
                        DungeonItemID.TRBigKeyChest,
                        DungeonItemID.TRBigChest,
                        DungeonItemID.TRCrystarollerRoom,
                        DungeonItemID.TRLaserBridgeTopLeft,
                        DungeonItemID.TRLaserBridgeTopRight,
                        DungeonItemID.TRLaserBridgeBottomLeft,
                        DungeonItemID.TRLaserBridgeBottomRight,
                        DungeonItemID.TRPokey1Drop,
                        DungeonItemID.TRPokey2Drop
                    },
                    false, new List<IKeyLayout> {_endFactory()}, dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity],
                            _worldStateRequirements[WorldState.Inverted]
                        }],
                        _bigKeyShuffleRequirements[true],
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.TRCompassChest,
                        DungeonItemID.TRRollerRoomLeft,
                        DungeonItemID.TRRollerRoomRight,
                        DungeonItemID.TRChainChomps,
                        DungeonItemID.TRPokey1Drop
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            3, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRPokey1Drop
                            },
                            true, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.TRCompassChest,
                                        DungeonItemID.TRRollerRoomLeft,
                                        DungeonItemID.TRRollerRoomRight,
                                        DungeonItemID.TRChainChomps,
                                        DungeonItemID.TRBigChest,
                                        DungeonItemID.TRCrystarollerRoom,
                                        DungeonItemID.TRPokey1Drop,
                                        DungeonItemID.TRPokey2Drop
                                    },
                                    true, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            6, new List<DungeonItemID>
                                            {
                                                DungeonItemID.TRCompassChest,
                                                DungeonItemID.TRRollerRoomLeft,
                                                DungeonItemID.TRRollerRoomRight,
                                                DungeonItemID.TRChainChomps,
                                                DungeonItemID.TRBigChest,
                                                DungeonItemID.TRCrystarollerRoom,
                                                DungeonItemID.TRLaserBridgeTopLeft,
                                                DungeonItemID.TRLaserBridgeTopRight,
                                                DungeonItemID.TRLaserBridgeBottomLeft,
                                                DungeonItemID.TRLaserBridgeBottomRight,
                                                DungeonItemID.TRPokey1Drop,
                                                DungeonItemID.TRPokey2Drop
                                            },
                                            true, new List<IKeyLayout> {_endFactory()},
                                            dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _entranceShuffleRequirements[EntranceShuffle.None],
                        _keyDropShuffleRequirements[true],
                        _worldStateRequirements[WorldState.StandardOpen]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.TRPokey2Drop}, new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            3, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRPokey1Drop
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.TRCompassChest,
                                        DungeonItemID.TRRollerRoomLeft,
                                        DungeonItemID.TRRollerRoomRight,
                                        DungeonItemID.TRChainChomps,
                                        DungeonItemID.TRBigChest,
                                        DungeonItemID.TRCrystarollerRoom,
                                        DungeonItemID.TRPokey1Drop
                                    },
                                    false, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            6, new List<DungeonItemID>
                                            {
                                                DungeonItemID.TRCompassChest,
                                                DungeonItemID.TRRollerRoomLeft,
                                                DungeonItemID.TRRollerRoomRight,
                                                DungeonItemID.TRChainChomps,
                                                DungeonItemID.TRBigChest,
                                                DungeonItemID.TRCrystarollerRoom,
                                                DungeonItemID.TRLaserBridgeTopLeft,
                                                DungeonItemID.TRLaserBridgeTopRight,
                                                DungeonItemID.TRLaserBridgeBottomLeft,
                                                DungeonItemID.TRLaserBridgeBottomRight,
                                                DungeonItemID.TRPokey1Drop
                                            },
                                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _entranceShuffleRequirements[EntranceShuffle.None],
                        _keyDropShuffleRequirements[true],
                        _worldStateRequirements[WorldState.StandardOpen]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.TRBigKeyChest}, new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRPokey1Drop
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.TRCompassChest,
                                        DungeonItemID.TRRollerRoomLeft,
                                        DungeonItemID.TRRollerRoomRight,
                                        DungeonItemID.TRChainChomps,
                                        DungeonItemID.TRBigChest,
                                        DungeonItemID.TRCrystarollerRoom,
                                        DungeonItemID.TRPokey1Drop,
                                        DungeonItemID.TRPokey2Drop
                                    },
                                    false, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(6,
                                            new List<DungeonItemID>
                                            {
                                                DungeonItemID.TRCompassChest,
                                                DungeonItemID.TRRollerRoomLeft,
                                                DungeonItemID.TRRollerRoomRight,
                                                DungeonItemID.TRChainChomps,
                                                DungeonItemID.TRBigChest,
                                                DungeonItemID.TRCrystarollerRoom,
                                                DungeonItemID.TRLaserBridgeTopLeft,
                                                DungeonItemID.TRLaserBridgeTopRight,
                                                DungeonItemID.TRLaserBridgeBottomLeft,
                                                DungeonItemID.TRLaserBridgeBottomRight,
                                                DungeonItemID.TRPokey1Drop,
                                                DungeonItemID.TRPokey2Drop
                                            },
                                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _entranceShuffleRequirements[EntranceShuffle.None],
                        _keyDropShuffleRequirements[true],
                        _worldStateRequirements[WorldState.StandardOpen]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.TRCompassChest,
                        DungeonItemID.TRRollerRoomLeft,
                        DungeonItemID.TRRollerRoomRight,
                        DungeonItemID.TRChainChomps,
                        DungeonItemID.TRCrystarollerRoom,
                        DungeonItemID.TRLaserBridgeTopLeft,
                        DungeonItemID.TRLaserBridgeTopRight,
                        DungeonItemID.TRLaserBridgeBottomLeft,
                        DungeonItemID.TRLaserBridgeBottomRight
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            6, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRBigKeyChest,
                                DungeonItemID.TRBigChest,
                                DungeonItemID.TRCrystarollerRoom,
                                DungeonItemID.TRLaserBridgeTopLeft,
                                DungeonItemID.TRLaserBridgeTopRight,
                                DungeonItemID.TRLaserBridgeBottomLeft,
                                DungeonItemID.TRLaserBridgeBottomRight,
                                DungeonItemID.TRPokey1Drop,
                                DungeonItemID.TRPokey2Drop
                            },
                            true, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity],
                            _worldStateRequirements[WorldState.Inverted]
                        }],
                        _bigKeyShuffleRequirements[false],
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.TRBigKeyChest},
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            6, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps,
                                DungeonItemID.TRCrystarollerRoom,
                                DungeonItemID.TRLaserBridgeTopLeft,
                                DungeonItemID.TRLaserBridgeTopRight,
                                DungeonItemID.TRLaserBridgeBottomLeft,
                                DungeonItemID.TRLaserBridgeBottomRight,
                                DungeonItemID.TRPokey1Drop,
                                DungeonItemID.TRPokey2Drop
                            },
                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity],
                            _worldStateRequirements[WorldState.Inverted]
                        }],
                        _bigKeyShuffleRequirements[false],
                        _keyDropShuffleRequirements[true]
                    }]),
            };
        }
    }
}