using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Misery Mire key layouts.
    /// </summary>
    public class MMKeyLayoutFactory : IMMKeyLayoutFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IBigKeyShuffleRequirementDictionary _bigKeyShuffleRequirements;
        private readonly IGuaranteedBossItemsRequirementDictionary _guaranteedBossItemsRequirements;
        private readonly IKeyDropShuffleRequirementDictionary _keyDropShuffleRequirements;
        private readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements;
        
        private readonly IBigKeyLayout.Factory _bigKeyFactory;
        private readonly IEndKeyLayout.Factory _endFactory;
        private readonly ISmallKeyLayout.Factory _smallKeyFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The <see cref="IAggregateRequirementDictionary"/>.
        /// </param>
        /// <param name="bigKeyShuffleRequirements">
        ///     The <see cref="IBigKeyShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="guaranteedBossItemsRequirements">
        ///     The <see cref="IGuaranteedBossItemsRequirementDictionary"/>.
        /// </param>
        /// <param name="keyDropShuffleRequirements">
        ///     The <see cref="IKeyDropShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="smallKeyShuffleRequirements">
        ///     The <see cref="ISmallKeyShuffleRequirementDictionary"/>.
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
        public MMKeyLayoutFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IBigKeyShuffleRequirementDictionary bigKeyShuffleRequirements,
            IGuaranteedBossItemsRequirementDictionary guaranteedBossItemsRequirements,
            IKeyDropShuffleRequirementDictionary keyDropShuffleRequirements,
            ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements, IBigKeyLayout.Factory bigKeyFactory,
            IEndKeyLayout.Factory endFactory, ISmallKeyLayout.Factory smallKeyFactory)
        {
            _aggregateRequirements = aggregateRequirements;
            _bigKeyShuffleRequirements = bigKeyShuffleRequirements;
            _guaranteedBossItemsRequirements = guaranteedBossItemsRequirements;
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
                        DungeonItemID.MMBridgeChest,
                        DungeonItemID.MMSpikeChest,
                        DungeonItemID.MMMainLobby,
                        DungeonItemID.MMBigChest,
                        DungeonItemID.MMMapChest,
                        DungeonItemID.MMBoss
                    },
                    false, new List<IKeyLayout> {_endFactory()}, dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _guaranteedBossItemsRequirements[false],
                        _keyDropShuffleRequirements[false]
                    }]),
                _smallKeyFactory(
                    3, new List<DungeonItemID>
                    {
                        DungeonItemID.MMBridgeChest,
                        DungeonItemID.MMSpikeChest,
                        DungeonItemID.MMMainLobby,
                        DungeonItemID.MMBigChest,
                        DungeonItemID.MMMapChest
                    },
                    true, new List<IKeyLayout> {_endFactory()}, dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _guaranteedBossItemsRequirements[true],
                        _keyDropShuffleRequirements[false]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.MMBridgeChest, DungeonItemID.MMSpikeChest},
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            3, new List<DungeonItemID>
                            {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest,
                                DungeonItemID.MMMainLobby,
                                DungeonItemID.MMBigChest,
                                DungeonItemID.MMMapChest,
                                DungeonItemID.MMBoss
                            },
                            true, new List<IKeyLayout> {_endFactory()}, dungeon,
                            _guaranteedBossItemsRequirements[false]),
                        _smallKeyFactory(
                            3, new List<DungeonItemID>
                            {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest,
                                DungeonItemID.MMMainLobby,
                                DungeonItemID.MMBigChest,
                                DungeonItemID.MMMapChest
                            },
                            true, new List<IKeyLayout> {_endFactory()}, dungeon,
                            _guaranteedBossItemsRequirements[true])
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _keyDropShuffleRequirements[false]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.MMMainLobby, DungeonItemID.MMMapChest},
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID>
                            {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    3, new List<DungeonItemID>
                                    {
                                        DungeonItemID.MMBridgeChest,
                                        DungeonItemID.MMSpikeChest,
                                        DungeonItemID.MMMainLobby,
                                        DungeonItemID.MMBigChest,
                                        DungeonItemID.MMMapChest,
                                        DungeonItemID.MMBoss
                                    },
                                    true, new List<IKeyLayout> {_endFactory()}, dungeon,
                                    _guaranteedBossItemsRequirements[false]),
                                _smallKeyFactory(
                                    3, new List<DungeonItemID>
                                    {
                                        DungeonItemID.MMBridgeChest,
                                        DungeonItemID.MMSpikeChest,
                                        DungeonItemID.MMMainLobby,
                                        DungeonItemID.MMBigChest,
                                        DungeonItemID.MMMapChest
                                    },
                                    true, new List<IKeyLayout> {_endFactory()}, dungeon,
                                    _guaranteedBossItemsRequirements[true])
                            },
                            dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _keyDropShuffleRequirements[false]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.MMCompassChest,
                        DungeonItemID.MMBigKeyChest
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID>
                            {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    2, new List<DungeonItemID>
                                    {
                                        DungeonItemID.MMBridgeChest,
                                        DungeonItemID.MMSpikeChest,
                                        DungeonItemID.MMMainLobby,
                                        DungeonItemID.MMMapChest
                                    },
                                    false, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            3, new List<DungeonItemID>
                                            {
                                                DungeonItemID.MMBridgeChest,
                                                DungeonItemID.MMSpikeChest,
                                                DungeonItemID.MMMainLobby,
                                                DungeonItemID.MMBigChest,
                                                DungeonItemID.MMCompassChest,
                                                DungeonItemID.MMBigKeyChest,
                                                DungeonItemID.MMMapChest,
                                                DungeonItemID.MMBoss
                                            },
                                            true, new List<IKeyLayout> {_endFactory()},
                                            dungeon, _guaranteedBossItemsRequirements[false]),
                                        _smallKeyFactory(
                                            3, new List<DungeonItemID>
                                            {
                                                DungeonItemID.MMBridgeChest,
                                                DungeonItemID.MMSpikeChest,
                                                DungeonItemID.MMMainLobby,
                                                DungeonItemID.MMBigChest,
                                                DungeonItemID.MMCompassChest,
                                                DungeonItemID.MMBigKeyChest,
                                                DungeonItemID.MMMapChest
                                            },
                                            true, new List<IKeyLayout> {_endFactory()},
                                            dungeon, _guaranteedBossItemsRequirements[true])
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _keyDropShuffleRequirements[false]
                    }]),
                _smallKeyFactory(
                    1, new List<DungeonItemID>
                    {
                        DungeonItemID.MMBridgeChest,
                        DungeonItemID.MMSpikeChest,
                        DungeonItemID.MMBigChest,
                        DungeonItemID.MMSpikesPot,
                        DungeonItemID.MMFishbonePot
                    },
                    false, new List<IKeyLayout>
                    {
                        _smallKeyFactory(
                            5, new List<DungeonItemID>
                            {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest,
                                DungeonItemID.MMMainLobby,
                                DungeonItemID.MMBigChest,
                                DungeonItemID.MMMapChest,
                                DungeonItemID.MMSpikesPot,
                                DungeonItemID.MMFishbonePot
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    6, new List<DungeonItemID>
                                    {
                                        DungeonItemID.MMBridgeChest,
                                        DungeonItemID.MMSpikeChest,
                                        DungeonItemID.MMMainLobby,
                                        DungeonItemID.MMBigChest,
                                        DungeonItemID.MMMapChest,
                                        DungeonItemID.MMSpikesPot,
                                        DungeonItemID.MMFishbonePot,
                                        DungeonItemID.MMConveyerCrystalDrop
                                    },
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    dungeon, _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.MMBridgeChest,
                        DungeonItemID.MMSpikeChest,
                        DungeonItemID.MMSpikesPot
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID>
                            {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest,
                                DungeonItemID.MMBigChest,
                                DungeonItemID.MMSpikesPot,
                                DungeonItemID.MMFishbonePot
                            },
                            true, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.MMBridgeChest,
                                        DungeonItemID.MMSpikeChest,
                                        DungeonItemID.MMMainLobby,
                                        DungeonItemID.MMBigChest,
                                        DungeonItemID.MMMapChest,
                                        DungeonItemID.MMSpikesPot,
                                        DungeonItemID.MMFishbonePot
                                    },
                                    true, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            6, new List<DungeonItemID>
                                            {
                                                DungeonItemID.MMBridgeChest,
                                                DungeonItemID.MMSpikeChest,
                                                DungeonItemID.MMMainLobby,
                                                DungeonItemID.MMBigChest,
                                                DungeonItemID.MMMapChest,
                                                DungeonItemID.MMSpikesPot,
                                                DungeonItemID.MMFishbonePot,
                                                DungeonItemID.MMConveyerCrystalDrop
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
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.MMMainLobby,
                        DungeonItemID.MMMapChest,
                        DungeonItemID.MMFishbonePot
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID>
                            {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest,
                                DungeonItemID.MMSpikesPot
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.MMBridgeChest,
                                        DungeonItemID.MMSpikeChest,
                                        DungeonItemID.MMMainLobby,
                                        DungeonItemID.MMBigChest,
                                        DungeonItemID.MMMapChest,
                                        DungeonItemID.MMSpikesPot,
                                        DungeonItemID.MMFishbonePot
                                    },
                                    true, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            6, new List<DungeonItemID>
                                            {
                                                DungeonItemID.MMBridgeChest,
                                                DungeonItemID.MMSpikeChest,
                                                DungeonItemID.MMMainLobby,
                                                DungeonItemID.MMBigChest,
                                                DungeonItemID.MMMapChest,
                                                DungeonItemID.MMSpikesPot,
                                                DungeonItemID.MMFishbonePot,
                                                DungeonItemID.MMConveyerCrystalDrop
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
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.MMConveyerCrystalDrop},
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID>
                            {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest,
                                DungeonItemID.MMSpikesPot
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.MMBridgeChest,
                                        DungeonItemID.MMSpikeChest,
                                        DungeonItemID.MMMainLobby,
                                        DungeonItemID.MMMapChest,
                                        DungeonItemID.MMSpikesPot,
                                        DungeonItemID.MMFishbonePot
                                    },
                                    false, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            6, new List<DungeonItemID>
                                            {
                                                DungeonItemID.MMBridgeChest,
                                                DungeonItemID.MMSpikeChest,
                                                DungeonItemID.MMMainLobby,
                                                DungeonItemID.MMBigChest,
                                                DungeonItemID.MMMapChest,
                                                DungeonItemID.MMSpikesPot,
                                                DungeonItemID.MMFishbonePot,
                                                DungeonItemID.MMConveyerCrystalDrop
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
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.MMCompassChest,
                        DungeonItemID.MMBigKeyChest
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID>
                            {
                                DungeonItemID.MMBridgeChest,
                                DungeonItemID.MMSpikeChest,
                                DungeonItemID.MMSpikesPot
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.MMBridgeChest,
                                        DungeonItemID.MMSpikeChest,
                                        DungeonItemID.MMMainLobby,
                                        DungeonItemID.MMMapChest,
                                        DungeonItemID.MMSpikesPot,
                                        DungeonItemID.MMFishbonePot
                                    },
                                    false, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            6, new List<DungeonItemID>
                                            {
                                                DungeonItemID.MMBridgeChest,
                                                DungeonItemID.MMSpikeChest,
                                                DungeonItemID.MMMainLobby,
                                                DungeonItemID.MMMapChest,
                                                DungeonItemID.MMSpikesPot,
                                                DungeonItemID.MMFishbonePot,
                                                DungeonItemID.MMConveyerCrystalDrop
                                            },
                                            false, new List<IKeyLayout> {_endFactory()},
                                            dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _keyDropShuffleRequirements[true]
                    }])
            };
        }
    }
}