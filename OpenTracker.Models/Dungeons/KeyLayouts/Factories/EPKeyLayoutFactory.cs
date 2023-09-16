using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories;

/// <summary>
/// This class contains the creation logic for Eastern Palace key layouts.
/// </summary>
public class EPKeyLayoutFactory : IEPKeyLayoutFactory
{
    private readonly IAggregateRequirementDictionary _aggregateRequirements;
    private readonly IAlternativeRequirementDictionary _alternativeRequirements;
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
    /// <param name="alternativeRequirements">
    ///     The <see cref="IAlternativeRequirementDictionary"/>.
    /// </param>
    /// <param name="bigKeyShuffleRequirements">
    ///     The <see cref="IBigKeyShuffleRequirementDictionary"/>.
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
    public EPKeyLayoutFactory(
        IAggregateRequirementDictionary aggregateRequirements,
        IAlternativeRequirementDictionary alternativeRequirements,
        IBigKeyShuffleRequirementDictionary bigKeyShuffleRequirements,
        IKeyDropShuffleRequirementDictionary keyDropShuffleRequirements,
        ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements, IBigKeyLayout.Factory bigKeyFactory,
        IEndKeyLayout.Factory endFactory, ISmallKeyLayout.Factory smallKeyFactory)
    {
        _aggregateRequirements = aggregateRequirements;
        _alternativeRequirements = alternativeRequirements;
        _bigKeyShuffleRequirements = bigKeyShuffleRequirements;
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
            _endFactory(
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[true],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _keyDropShuffleRequirements[false],
                        _smallKeyShuffleRequirements[true]
                    }]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.EPCannonballChest,
                    DungeonItemID.EPMapChest,
                    DungeonItemID.EPCompassChest,
                    DungeonItemID.EPBigKeyChest
                },
                new List<IKeyLayout> {_endFactory()},
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[false],
                    _keyDropShuffleRequirements[false]
                }]),
            _smallKeyFactory(
                1, new List<DungeonItemID>
                {
                    DungeonItemID.EPCannonballChest,
                    DungeonItemID.EPMapChest,
                    DungeonItemID.EPCompassChest,
                    DungeonItemID.EPBigChest,
                    DungeonItemID.EPDarkSquarePot
                },
                false, new List<IKeyLayout>
                {
                    _smallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.EPCannonballChest,
                            DungeonItemID.EPMapChest,
                            DungeonItemID.EPCompassChest,
                            DungeonItemID.EPBigChest,
                            DungeonItemID.EPBigKeyChest,
                            DungeonItemID.EPDarkSquarePot,
                            DungeonItemID.EPDarkEyegoreDrop
                        },
                        false, new List<IKeyLayout> {_endFactory()}, dungeon)
                },
                dungeon, _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[true],
                    _keyDropShuffleRequirements[true]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.EPCannonballChest,
                    DungeonItemID.EPMapChest,
                    DungeonItemID.EPCompassChest,
                    DungeonItemID.EPDarkSquarePot
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.EPCannonballChest,
                            DungeonItemID.EPMapChest,
                            DungeonItemID.EPCompassChest,
                            DungeonItemID.EPDarkSquarePot,
                            DungeonItemID.EPBigChest,
                            DungeonItemID.EPDarkEyegoreDrop
                        },
                        true, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.EPCannonballChest,
                                    DungeonItemID.EPMapChest,
                                    DungeonItemID.EPCompassChest,
                                    DungeonItemID.EPBigChest,
                                    DungeonItemID.EPBigKeyChest,
                                    DungeonItemID.EPDarkSquarePot,
                                    DungeonItemID.EPDarkEyegoreDrop
                                },
                                true, new List<IKeyLayout> {_endFactory()}, dungeon),
                        },
                        dungeon)
                },
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[false],
                    _keyDropShuffleRequirements[true]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.EPBigKeyChest}, new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.EPCannonballChest,
                            DungeonItemID.EPMapChest,
                            DungeonItemID.EPCompassChest,
                            DungeonItemID.EPDarkSquarePot
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.EPCannonballChest,
                                    DungeonItemID.EPMapChest,
                                    DungeonItemID.EPCompassChest,
                                    DungeonItemID.EPDarkSquarePot,
                                    DungeonItemID.EPBigChest,
                                    DungeonItemID.EPDarkEyegoreDrop
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