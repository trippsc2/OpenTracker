using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories;

/// <summary>
/// This class contains the creation logic for Swamp Palace key layouts.
/// </summary>
public class SPKeyLayoutFactory : ISPKeyLayoutFactory
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
    public SPKeyLayoutFactory(
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
                1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                new List<IKeyLayout> {_endFactory()}, dungeon,
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[true],
                    _keyDropShuffleRequirements[false]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.SPMapChest,
                    DungeonItemID.SPBigChest,
                    DungeonItemID.SPCompassChest,
                    DungeonItemID.SPWestChest,
                    DungeonItemID.SPBigKeyChest,
                    DungeonItemID.SPFloodedRoomLeft,
                    DungeonItemID.SPFloodedRoomRight,
                    DungeonItemID.SPWaterfallRoom
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout> {_endFactory()}, dungeon)
                },
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[false],
                    _keyDropShuffleRequirements[false]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.SPBoss}, new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout> {_endFactory()}, dungeon)
                },
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[false],
                    _guaranteedBossItemsRequirements[false],
                    _keyDropShuffleRequirements[false]
                }]),
            _smallKeyFactory(
                1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                false, new List<IKeyLayout>
                {
                    _smallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.SPEntrance,
                            DungeonItemID.SPMapChest,
                            DungeonItemID.SPPotRowPot
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot,
                                    DungeonItemID.SPTrench1Pot
                                },
                                false, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        5, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPBigChest,
                                            DungeonItemID.SPCompassChest,
                                            DungeonItemID.SPTrench1Pot,
                                            DungeonItemID.SPHookshotPot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                6, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPBigChest,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPFloodedRoomLeft,
                                                    DungeonItemID.SPFloodedRoomRight,
                                                    DungeonItemID.SPWaterfallRoom,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot,
                                                    DungeonItemID.SPWaterwayPot
                                                },
                                                false, new List<IKeyLayout> {_endFactory()},
                                                dungeon)
                                        }, dungeon)
                                },
                                dungeon)
                        },
                        dungeon)
                },
                dungeon, _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[true],
                    _keyDropShuffleRequirements[true]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.SPMapChest, DungeonItemID.SPPotRowPot},
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                true, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPTrench1Pot
                                        },
                                        true, new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPBigChest,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                true, new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPBigChest,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPTrench1Pot,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        true, new List<IKeyLayout>
                                                        {
                                                            _endFactory()
                                                        }, dungeon)
                                                },
                                                dungeon)
                                        },
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
                new List<DungeonItemID> {DungeonItemID.SPTrench1Pot},
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                false, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPBigChest,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                false, new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPBigChest,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        false, new List<IKeyLayout>
                                                        {
                                                            _endFactory()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
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
                    DungeonItemID.SPBigChest,
                    DungeonItemID.SPCompassChest,
                    DungeonItemID.SPHookshotPot
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                false, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPTrench1Pot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPBigChest,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                true, new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPBigChest,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPTrench1Pot,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        true, new List<IKeyLayout>
                                                        {
                                                            _endFactory()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
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
                    DungeonItemID.SPFloodedRoomLeft,
                    DungeonItemID.SPFloodedRoomRight,
                    DungeonItemID.SPWaterfallRoom,
                    DungeonItemID.SPWaterwayPot
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                false, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPTrench1Pot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                false, new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPBigChest,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPTrench1Pot,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        true, new List<IKeyLayout>
                                                        {
                                                            _endFactory()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
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
                    DungeonItemID.SPWestChest,
                    DungeonItemID.SPBigKeyChest,
                    DungeonItemID.SPTrench2Pot
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                false, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPTrench1Pot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                false, new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPTrench1Pot,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        false, new List<IKeyLayout>
                                                        {
                                                            _endFactory()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
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
                new List<DungeonItemID> {DungeonItemID.SPBoss}, new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                false, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPTrench1Pot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                false, new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPTrench1Pot,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        false, new List<IKeyLayout>
                                                        {
                                                            _endFactory()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                dungeon)
                        },
                        dungeon)
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