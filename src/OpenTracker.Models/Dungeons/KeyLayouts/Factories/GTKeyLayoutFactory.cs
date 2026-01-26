using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories;

/// <summary>
/// This class contains the creation logic for Ganon's Tower key layouts.
/// </summary>
public class GTKeyLayoutFactory : IGTKeyLayoutFactory
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
    public GTKeyLayoutFactory(
        IAggregateRequirementDictionary aggregateRequirements,
        IBigKeyShuffleRequirementDictionary bigKeyShuffleRequirements,
        IKeyDropShuffleRequirementDictionary keyDropShuffleRequirements,
        ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements, IBigKeyLayout.Factory bigKeyFactory,
        IEndKeyLayout.Factory endFactory, ISmallKeyLayout.Factory smallKeyFactory)
    {
        _aggregateRequirements = aggregateRequirements;
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
            _endFactory(_aggregateRequirements[new HashSet<IRequirement>
            {
                _bigKeyShuffleRequirements[true],
                _smallKeyShuffleRequirements[true]
            }]),
            _smallKeyFactory(
                3, new List<DungeonItemID>
                {
                    DungeonItemID.GTHopeRoomLeft,
                    DungeonItemID.GTHopeRoomRight,
                    DungeonItemID.GTBobsTorch,
                    DungeonItemID.GTDMsRoomTopLeft,
                    DungeonItemID.GTDMsRoomTopRight,
                    DungeonItemID.GTDMsRoomBottomLeft,
                    DungeonItemID.GTDMsRoomBottomRight,
                    DungeonItemID.GTTileRoom
                },
                false, new List<IKeyLayout>
                {
                    _smallKeyFactory(
                        4, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTMapChest,
                            DungeonItemID.GTFiresnakeRoom,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTBobsChest,
                            DungeonItemID.GTBigKeyRoomTopLeft,
                            DungeonItemID.GTBigKeyRoomTopRight,
                            DungeonItemID.GTBigKeyChest,
                            DungeonItemID.GTBigChest,
                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                            DungeonItemID.GTMiniHelmasaurRoomRight,
                            DungeonItemID.GTPreMoldormChest
                        },
                        false, new List<IKeyLayout> {_endFactory()}, dungeon)
                },
                dungeon, _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[true],
                    _keyDropShuffleRequirements[false]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.GTHopeRoomLeft,
                    DungeonItemID.GTHopeRoomRight,
                    DungeonItemID.GTBobsTorch,
                    DungeonItemID.GTDMsRoomTopLeft,
                    DungeonItemID.GTDMsRoomTopRight,
                    DungeonItemID.GTDMsRoomBottomLeft,
                    DungeonItemID.GTDMsRoomBottomRight,
                    DungeonItemID.GTTileRoom
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        },
                        true, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTMapChest,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTBigChest,
                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                    DungeonItemID.GTPreMoldormChest
                                },
                                true, new List<IKeyLayout> {_endFactory()}, dungeon)
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
                    DungeonItemID.GTFiresnakeRoom,
                    DungeonItemID.GTBobsChest,
                    DungeonItemID.GTBigKeyRoomTopLeft,
                    DungeonItemID.GTBigKeyRoomTopRight,
                    DungeonItemID.GTBigKeyChest
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTMapChest,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTBigChest,
                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                    DungeonItemID.GTPreMoldormChest
                                },
                                true, new List<IKeyLayout> {_endFactory()}, dungeon)
                        },
                        dungeon, _smallKeyShuffleRequirements[false])
                },
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[false],
                    _keyDropShuffleRequirements[false]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.GTMapChest}, new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTBigChest,
                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                    DungeonItemID.GTPreMoldormChest
                                },
                                false, new List<IKeyLayout> {_endFactory()}, dungeon)
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
                    DungeonItemID.GTRandomizerRoomTopLeft,
                    DungeonItemID.GTRandomizerRoomTopRight,
                    DungeonItemID.GTRandomizerRoomBottomLeft,
                    DungeonItemID.GTRandomizerRoomBottomRight
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom
                                },
                                false, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        4, new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTMapChest,
                                            DungeonItemID.GTFiresnakeRoom,
                                            DungeonItemID.GTRandomizerRoomTopLeft,
                                            DungeonItemID.GTRandomizerRoomTopRight,
                                            DungeonItemID.GTRandomizerRoomBottomLeft,
                                            DungeonItemID.GTRandomizerRoomBottomRight,
                                            DungeonItemID.GTTileRoom,
                                            DungeonItemID.GTBobsChest,
                                            DungeonItemID.GTBigKeyRoomTopLeft,
                                            DungeonItemID.GTBigKeyRoomTopRight,
                                            DungeonItemID.GTBigKeyChest
                                        },
                                        true, new List<IKeyLayout> {_endFactory()},
                                        dungeon)
                                },
                                dungeon)
                        }, dungeon)
                },
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[false],
                    _keyDropShuffleRequirements[false]
                }]),
            _bigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.GTCompassRoomTopLeft,
                    DungeonItemID.GTCompassRoomTopRight,
                    DungeonItemID.GTCompassRoomBottomLeft,
                    DungeonItemID.GTCompassRoomBottomRight
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom
                                },
                                false, new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        4, new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTMapChest,
                                            DungeonItemID.GTFiresnakeRoom,
                                            DungeonItemID.GTTileRoom,
                                            DungeonItemID.GTCompassRoomTopLeft,
                                            DungeonItemID.GTCompassRoomTopRight,
                                            DungeonItemID.GTCompassRoomBottomLeft,
                                            DungeonItemID.GTCompassRoomBottomRight,
                                            DungeonItemID.GTBobsChest,
                                            DungeonItemID.GTBigKeyRoomTopLeft,
                                            DungeonItemID.GTBigKeyRoomTopRight,
                                            DungeonItemID.GTBigKeyChest
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
                    _keyDropShuffleRequirements[false]
                }]),
            _smallKeyFactory(
                7, new List<DungeonItemID>
                {
                    DungeonItemID.GTHopeRoomLeft,
                    DungeonItemID.GTHopeRoomRight,
                    DungeonItemID.GTBobsTorch,
                    DungeonItemID.GTDMsRoomTopLeft,
                    DungeonItemID.GTDMsRoomTopRight,
                    DungeonItemID.GTDMsRoomBottomLeft,
                    DungeonItemID.GTDMsRoomBottomRight,
                    DungeonItemID.GTTileRoom,
                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                    DungeonItemID.GTMiniHelmasaurRoomRight,
                    DungeonItemID.GTConveyorCrossPot,
                    DungeonItemID.GTDoubleSwitchPot,
                    DungeonItemID.GTMiniHelmasaurDrop
                },
                false, new List<IKeyLayout>
                {
                    _smallKeyFactory(
                        8, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTFiresnakeRoom,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTCompassRoomTopLeft,
                            DungeonItemID.GTCompassRoomTopRight,
                            DungeonItemID.GTCompassRoomBottomLeft,
                            DungeonItemID.GTCompassRoomBottomRight,
                            DungeonItemID.GTBobsChest,
                            DungeonItemID.GTBigKeyRoomTopLeft,
                            DungeonItemID.GTBigKeyRoomTopRight,
                            DungeonItemID.GTBigKeyChest,
                            DungeonItemID.GTBigChest,
                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                            DungeonItemID.GTMiniHelmasaurRoomRight,
                            DungeonItemID.GTPreMoldormChest,
                            DungeonItemID.GTConveyorCrossPot,
                            DungeonItemID.GTDoubleSwitchPot,
                            DungeonItemID.GTConveyorStarPitsPot,
                            DungeonItemID.GTMiniHelmasaurDrop
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
                    DungeonItemID.GTHopeRoomLeft,
                    DungeonItemID.GTHopeRoomRight,
                    DungeonItemID.GTBobsTorch,
                    DungeonItemID.GTDMsRoomTopLeft,
                    DungeonItemID.GTDMsRoomTopRight,
                    DungeonItemID.GTDMsRoomBottomLeft,
                    DungeonItemID.GTDMsRoomBottomRight,
                    DungeonItemID.GTTileRoom,
                    DungeonItemID.GTConveyorCrossPot,
                    DungeonItemID.GTDoubleSwitchPot
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        7, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                            DungeonItemID.GTMiniHelmasaurRoomRight,
                            DungeonItemID.GTConveyorCrossPot,
                            DungeonItemID.GTDoubleSwitchPot,
                            DungeonItemID.GTMiniHelmasaurDrop
                        },
                        true, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                8, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTBigChest,
                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                    DungeonItemID.GTPreMoldormChest,
                                    DungeonItemID.GTConveyorCrossPot,
                                    DungeonItemID.GTDoubleSwitchPot,
                                    DungeonItemID.GTConveyorStarPitsPot,
                                    DungeonItemID.GTMiniHelmasaurDrop
                                },
                                true, new List<IKeyLayout> {_endFactory()}, dungeon)
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
                    DungeonItemID.GTFiresnakeRoom,
                    DungeonItemID.GTCompassRoomTopLeft,
                    DungeonItemID.GTCompassRoomTopRight,
                    DungeonItemID.GTCompassRoomBottomLeft,
                    DungeonItemID.GTCompassRoomBottomRight,
                    DungeonItemID.GTBobsChest,
                    DungeonItemID.GTBigKeyRoomTopLeft,
                    DungeonItemID.GTBigKeyRoomTopRight,
                    DungeonItemID.GTBigKeyChest,
                    DungeonItemID.GTConveyorStarPitsPot
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        7, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTConveyorCrossPot,
                            DungeonItemID.GTDoubleSwitchPot
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                8, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTBigChest,
                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                    DungeonItemID.GTPreMoldormChest,
                                    DungeonItemID.GTConveyorCrossPot,
                                    DungeonItemID.GTDoubleSwitchPot,
                                    DungeonItemID.GTConveyorStarPitsPot,
                                    DungeonItemID.GTMiniHelmasaurDrop
                                },
                                true, new List<IKeyLayout> {_endFactory()}, dungeon)
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
                    DungeonItemID.GTRandomizerRoomTopLeft,
                    DungeonItemID.GTRandomizerRoomTopRight,
                    DungeonItemID.GTRandomizerRoomBottomLeft,
                    DungeonItemID.GTRandomizerRoomBottomRight
                },
                new List<IKeyLayout>
                {
                    _endFactory(_smallKeyShuffleRequirements[true]),
                    _smallKeyFactory(
                        7, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTConveyorCrossPot,
                            DungeonItemID.GTDoubleSwitchPot
                        },
                        false, new List<IKeyLayout>
                        {
                            _smallKeyFactory(
                                8, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTConveyorCrossPot,
                                    DungeonItemID.GTDoubleSwitchPot,
                                    DungeonItemID.GTConveyorStarPitsPot
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