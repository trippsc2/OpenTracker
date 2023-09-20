using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories;

/// <summary>
/// This class contains the creation logic for Desert Palace key layouts.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public class DPKeyLayoutFactory : IDPKeyLayoutFactory
{
    private readonly IAggregateRequirementDictionary _aggregateRequirements;
    private readonly IBigKeyShuffleRequirementDictionary _bigKeyShuffleRequirements;
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
    /// <param name="keyDropShuffleRequirements">
    ///     The <see cref="IKeyDropShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="smallKeyRequirements">
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
    public DPKeyLayoutFactory(
        IAggregateRequirementDictionary aggregateRequirements,
        IBigKeyShuffleRequirementDictionary bigKeyShuffleRequirements,
        IKeyDropShuffleRequirementDictionary keyDropShuffleRequirements,
        ISmallKeyShuffleRequirementDictionary smallKeyRequirements, IBigKeyLayout.Factory bigKeyFactory,
        IEndKeyLayout.Factory endFactory, ISmallKeyLayout.Factory smallKeyFactory)
    {
        _aggregateRequirements = aggregateRequirements;
        _bigKeyShuffleRequirements = bigKeyShuffleRequirements;
        _keyDropShuffleRequirements = keyDropShuffleRequirements;
        _smallKeyShuffleRequirements = smallKeyRequirements;

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
                1, new List<DungeonItemID>
                {
                    DungeonItemID.DPMapChest,
                    DungeonItemID.DPTorch,
                    DungeonItemID.DPBigChest
                },
                false, new List<IKeyLayout> {_endFactory()}, dungeon,
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[true],
                    _keyDropShuffleRequirements[false]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.DPMapChest, DungeonItemID.DPTorch},
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPBigChest
                        },
                        true, new List<IKeyLayout> {_endFactory()}, dungeon)
                },
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[false],
                    _keyDropShuffleRequirements[false]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.DPCompassChest, DungeonItemID.DPBigKeyChest},
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1,
                        new List<DungeonItemID> {DungeonItemID.DPMapChest, DungeonItemID.DPTorch},
                        false, new List<IKeyLayout> {_endFactory()}, dungeon)
                },
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[false],
                    _keyDropShuffleRequirements[false]
                }]),
            _smallKeyFactory(
                2, new List<DungeonItemID>
                {
                    DungeonItemID.DPMapChest,
                    DungeonItemID.DPTorch,
                    DungeonItemID.DPBigChest,
                    DungeonItemID.DPTiles1Pot
                },
                false, new List<IKeyLayout>
                {
                    _smallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPBigChest,
                            DungeonItemID.DPTiles1Pot,
                            DungeonItemID.DPBeamosHallPot
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest,
                                    DungeonItemID.DPTiles1Pot,
                                    DungeonItemID.DPBeamosHallPot,
                                    DungeonItemID.DPTiles2Pot
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
                    DungeonItemID.DPMapChest,
                    DungeonItemID.DPTorch,
                    DungeonItemID.DPCompassChest,
                    DungeonItemID.DPBigKeyChest,
                    DungeonItemID.DPTiles1Pot,
                    DungeonItemID.DPBeamosHallPot,
                    DungeonItemID.DPTiles2Pot
                },
                new List<IKeyLayout> {_endFactory()}, _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[false],
                    _keyDropShuffleRequirements[true],
                    _smallKeyShuffleRequirements[true]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.DPMapChest,
                    DungeonItemID.DPTorch,
                    DungeonItemID.DPTiles1Pot
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPBigChest,
                            DungeonItemID.DPTiles1Pot
                        },
                        true, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest,
                                    DungeonItemID.DPTiles1Pot,
                                    DungeonItemID.DPBeamosHallPot
                                },
                                true, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        4, new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPBigChest,
                                            DungeonItemID.DPTiles1Pot,
                                            DungeonItemID.DPBeamosHallPot,
                                            DungeonItemID.DPTiles2Pot
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
                    _keyDropShuffleRequirements[true]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.DPBeamosHallPot},
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPTiles1Pot
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest,
                                    DungeonItemID.DPTiles1Pot
                                },
                                false, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        4, new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPBigChest,
                                            DungeonItemID.DPTiles1Pot,
                                            DungeonItemID.DPTiles2Pot
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
                    _keyDropShuffleRequirements[true]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.DPTiles2Pot},
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPTiles1Pot
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest,
                                    DungeonItemID.DPTiles1Pot,
                                    DungeonItemID.DPBeamosHallPot
                                },
                                false, new List<IKeyLayout> {_endFactory()}, dungeon)
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