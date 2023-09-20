using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories;

/// <summary>
/// This class contains the creation logic for Ice Palace key layouts.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class IPKeyLayoutFactory : IIPKeyLayoutFactory
{
    private readonly IAggregateRequirementDictionary _aggregateRequirements;
    private readonly IAlternativeRequirementDictionary _alternativeRequirements;
    private readonly IBigKeyShuffleRequirementDictionary _bigKeyShuffleRequirements;
    private readonly IGuaranteedBossItemsRequirementDictionary _guaranteedBossItemsRequirements;
    private readonly IItemPlacementRequirementDictionary _itemPlacementRequirements;
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
    /// <param name="alternativeRequirements">
    ///     The <see cref="IAlternativeRequirementDictionary"/>.
    /// </param>
    /// <param name="bigKeyShuffleRequirements">
    ///     The <see cref="IBigKeyShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="guaranteedBossItemsRequirements">
    ///     The <see cref="IGuaranteedBossItemsRequirementDictionary"/>.
    /// </param>
    /// <param name="itemPlacementRequirements">
    ///     The <see cref="IItemPlacementRequirementDictionary"/>.
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
    public IPKeyLayoutFactory(
        IAggregateRequirementDictionary aggregateRequirements,
        IAlternativeRequirementDictionary alternativeRequirements,
        IBigKeyShuffleRequirementDictionary bigKeyShuffleRequirements,
        IGuaranteedBossItemsRequirementDictionary guaranteedBossItemsRequirements,
        IItemPlacementRequirementDictionary itemPlacementRequirements,
        IKeyDropShuffleRequirementDictionary keyDropShuffleRequirements,
        ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements,
        IBigKeyLayout.Factory bigKeyFactory, IEndKeyLayout.Factory endFactory,
        ISmallKeyLayout.Factory smallKeyFactory)
    {
        _aggregateRequirements = aggregateRequirements;
        _alternativeRequirements = alternativeRequirements;
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
                2, new List<DungeonItemID>
                {
                    DungeonItemID.IPCompassChest,
                    DungeonItemID.IPSpikeRoom,
                    DungeonItemID.IPMapChest,
                    DungeonItemID.IPBigKeyChest,
                    DungeonItemID.IPFreezorChest,
                    DungeonItemID.IPBigChest,
                    DungeonItemID.IPIcedTRoom
                },
                false, new List<IKeyLayout> {_endFactory()}, dungeon,
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _guaranteedBossItemsRequirements[true],
                        _itemPlacementRequirements[ItemPlacement.Basic]
                    }],
                    _bigKeyShuffleRequirements[true],
                    _keyDropShuffleRequirements[false],
                    _smallKeyShuffleRequirements[false]
                }]),
            _smallKeyFactory(
                2, new List<DungeonItemID>
                {
                    DungeonItemID.IPCompassChest,
                    DungeonItemID.IPSpikeRoom,
                    DungeonItemID.IPMapChest,
                    DungeonItemID.IPBigKeyChest,
                    DungeonItemID.IPFreezorChest,
                    DungeonItemID.IPBigChest,
                    DungeonItemID.IPIcedTRoom,
                    DungeonItemID.IPBoss
                },
                false, new List<IKeyLayout> {_endFactory()}, dungeon,
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[true],
                    _guaranteedBossItemsRequirements[false],
                    _itemPlacementRequirements[ItemPlacement.Advanced],
                    _keyDropShuffleRequirements[false],
                    _smallKeyShuffleRequirements[false]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.IPCompassChest,
                    DungeonItemID.IPSpikeRoom,
                    DungeonItemID.IPMapChest,
                    DungeonItemID.IPBigKeyChest,
                    DungeonItemID.IPFreezorChest,
                    DungeonItemID.IPIcedTRoom
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPBigChest,
                            DungeonItemID.IPIcedTRoom
                        },
                        true, new List<IKeyLayout> {_endFactory()}, dungeon,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _alternativeRequirements[new HashSet<IRequirement>
                            {
                                _guaranteedBossItemsRequirements[true],
                                _itemPlacementRequirements[ItemPlacement.Basic]
                            }],
                            _smallKeyShuffleRequirements[false]
                        }]),
                    _smallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPBigChest,
                            DungeonItemID.IPIcedTRoom,
                            DungeonItemID.IPBoss
                        },
                        true, new List<IKeyLayout> {_endFactory()}, dungeon,
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _guaranteedBossItemsRequirements[false],
                            _itemPlacementRequirements[ItemPlacement.Advanced],
                            _smallKeyShuffleRequirements[false]
                        }])
                },
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[false],
                    _keyDropShuffleRequirements[false]
                }]),
            _smallKeyFactory(
                1, new List<DungeonItemID> {DungeonItemID.IPJellyDrop},
                false, new List<IKeyLayout>
                {
                    _smallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPJellyDrop,
                            DungeonItemID.IPConveyerDrop
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                6, new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPSpikeRoom,
                                    DungeonItemID.IPMapChest,
                                    DungeonItemID.IPBigKeyChest,
                                    DungeonItemID.IPFreezorChest,
                                    DungeonItemID.IPBigChest,
                                    DungeonItemID.IPIcedTRoom,
                                    DungeonItemID.IPJellyDrop,
                                    DungeonItemID.IPConveyerDrop,
                                    DungeonItemID.IPHammerBlockDrop,
                                    DungeonItemID.IPManyPotsPot
                                },
                                false, new List<IKeyLayout> {_endFactory()}, dungeon,
                                _alternativeRequirements[new HashSet<IRequirement>
                                {
                                    _guaranteedBossItemsRequirements[true],
                                    _itemPlacementRequirements[ItemPlacement.Basic]
                                }]),
                            _smallKeyFactory(5,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPSpikeRoom,
                                    DungeonItemID.IPMapChest,
                                    DungeonItemID.IPBigKeyChest,
                                    DungeonItemID.IPFreezorChest,
                                    DungeonItemID.IPBigChest,
                                    DungeonItemID.IPIcedTRoom,
                                    DungeonItemID.IPJellyDrop,
                                    DungeonItemID.IPConveyerDrop,
                                    DungeonItemID.IPHammerBlockDrop,
                                    DungeonItemID.IPManyPotsPot
                                },
                                false, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        6, new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPSpikeRoom,
                                            DungeonItemID.IPMapChest,
                                            DungeonItemID.IPBigKeyChest,
                                            DungeonItemID.IPFreezorChest,
                                            DungeonItemID.IPBigChest,
                                            DungeonItemID.IPIcedTRoom,
                                            DungeonItemID.IPBoss,
                                            DungeonItemID.IPJellyDrop,
                                            DungeonItemID.IPConveyerDrop,
                                            DungeonItemID.IPHammerBlockDrop,
                                            DungeonItemID.IPManyPotsPot
                                        },
                                        false, new List<IKeyLayout> {_endFactory()},
                                        dungeon)
                                },
                                dungeon, _aggregateRequirements[new HashSet<IRequirement>
                                {
                                    _guaranteedBossItemsRequirements[false],
                                    _itemPlacementRequirements[ItemPlacement.Advanced]
                                }])
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
                    DungeonItemID.IPCompassChest,
                    DungeonItemID.IPConveyerDrop
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.IPJellyDrop},
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPJellyDrop,
                                    DungeonItemID.IPConveyerDrop
                                },
                                true, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        6, new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPSpikeRoom,
                                            DungeonItemID.IPMapChest,
                                            DungeonItemID.IPBigKeyChest,
                                            DungeonItemID.IPFreezorChest,
                                            DungeonItemID.IPBigChest,
                                            DungeonItemID.IPIcedTRoom,
                                            DungeonItemID.IPJellyDrop,
                                            DungeonItemID.IPConveyerDrop,
                                            DungeonItemID.IPHammerBlockDrop,
                                            DungeonItemID.IPManyPotsPot
                                        },
                                        true, new List<IKeyLayout> {_endFactory()},
                                        dungeon, _alternativeRequirements[new HashSet<IRequirement>
                                        {
                                            _guaranteedBossItemsRequirements[true],
                                            _itemPlacementRequirements[ItemPlacement.Basic]
                                        }]),
                                    _smallKeyFactory(
                                        5, new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPSpikeRoom,
                                            DungeonItemID.IPMapChest,
                                            DungeonItemID.IPBigKeyChest,
                                            DungeonItemID.IPFreezorChest,
                                            DungeonItemID.IPBigChest,
                                            DungeonItemID.IPIcedTRoom,
                                            DungeonItemID.IPJellyDrop,
                                            DungeonItemID.IPConveyerDrop,
                                            DungeonItemID.IPHammerBlockDrop,
                                            DungeonItemID.IPManyPotsPot
                                        },
                                        true, new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                6, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.IPCompassChest,
                                                    DungeonItemID.IPSpikeRoom,
                                                    DungeonItemID.IPMapChest,
                                                    DungeonItemID.IPBigKeyChest,
                                                    DungeonItemID.IPFreezorChest,
                                                    DungeonItemID.IPBigChest,
                                                    DungeonItemID.IPIcedTRoom,
                                                    DungeonItemID.IPBoss,
                                                    DungeonItemID.IPJellyDrop,
                                                    DungeonItemID.IPConveyerDrop,
                                                    DungeonItemID.IPHammerBlockDrop,
                                                    DungeonItemID.IPManyPotsPot
                                                },
                                                true,
                                                new List<IKeyLayout> {_endFactory()}, dungeon)
                                        },
                                        dungeon, _aggregateRequirements[new HashSet<IRequirement>
                                        {
                                            _guaranteedBossItemsRequirements[false],
                                            _itemPlacementRequirements[ItemPlacement.Advanced]
                                        }])
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
                    DungeonItemID.IPSpikeRoom,
                    DungeonItemID.IPMapChest,
                    DungeonItemID.IPBigKeyChest,
                    DungeonItemID.IPFreezorChest,
                    DungeonItemID.IPIcedTRoom,
                    DungeonItemID.IPHammerBlockDrop,
                    DungeonItemID.IPManyPotsPot
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.IPJellyDrop},
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPJellyDrop,
                                    DungeonItemID.IPConveyerDrop
                                },
                                false, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        6, new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPSpikeRoom,
                                            DungeonItemID.IPMapChest,
                                            DungeonItemID.IPBigKeyChest,
                                            DungeonItemID.IPFreezorChest,
                                            DungeonItemID.IPBigChest,
                                            DungeonItemID.IPIcedTRoom,
                                            DungeonItemID.IPJellyDrop,
                                            DungeonItemID.IPConveyerDrop,
                                            DungeonItemID.IPHammerBlockDrop,
                                            DungeonItemID.IPManyPotsPot
                                        },
                                        true, new List<IKeyLayout> {_endFactory()},
                                        dungeon, _alternativeRequirements[new HashSet<IRequirement>
                                        {
                                            _guaranteedBossItemsRequirements[true],
                                            _itemPlacementRequirements[ItemPlacement.Basic]
                                        }]),
                                    _smallKeyFactory(
                                        5, new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPSpikeRoom,
                                            DungeonItemID.IPMapChest,
                                            DungeonItemID.IPBigKeyChest,
                                            DungeonItemID.IPFreezorChest,
                                            DungeonItemID.IPBigChest,
                                            DungeonItemID.IPIcedTRoom,
                                            DungeonItemID.IPJellyDrop,
                                            DungeonItemID.IPConveyerDrop,
                                            DungeonItemID.IPHammerBlockDrop,
                                            DungeonItemID.IPManyPotsPot
                                        },
                                        true, new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                6, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.IPCompassChest,
                                                    DungeonItemID.IPSpikeRoom,
                                                    DungeonItemID.IPMapChest,
                                                    DungeonItemID.IPBigKeyChest,
                                                    DungeonItemID.IPFreezorChest,
                                                    DungeonItemID.IPBigChest,
                                                    DungeonItemID.IPIcedTRoom,
                                                    DungeonItemID.IPBoss,
                                                    DungeonItemID.IPJellyDrop,
                                                    DungeonItemID.IPConveyerDrop,
                                                    DungeonItemID.IPHammerBlockDrop,
                                                    DungeonItemID.IPManyPotsPot
                                                },
                                                true,
                                                new List<IKeyLayout> {_endFactory()}, dungeon)
                                        },
                                        dungeon,
                                        _aggregateRequirements[new HashSet<IRequirement>
                                        {
                                            _guaranteedBossItemsRequirements[false],
                                            _itemPlacementRequirements[ItemPlacement.Advanced]
                                        }])
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