using System.Collections.Generic;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.KeyLayouts.Factories;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories
{
    public class TRKeyLayoutFactoryTests
    {
        private static readonly IAggregateRequirementDictionary AggregateRequirements =
            new AggregateRequirementDictionary(requirements =>
                new AggregateRequirement(requirements));
        private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
            new AlternativeRequirementDictionary(requirements =>
                new AlternativeRequirement(requirements));
        private static readonly IBigKeyShuffleRequirementDictionary BigKeyShuffleRequirements =
            Substitute.For<IBigKeyShuffleRequirementDictionary>();
        private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
            Substitute.For<IEntranceShuffleRequirementDictionary>();
        private static readonly IKeyDropShuffleRequirementDictionary KeyDropShuffleRequirements =
            Substitute.For<IKeyDropShuffleRequirementDictionary>();
        private static readonly ISmallKeyShuffleRequirementDictionary SmallKeyShuffleRequirements =
            Substitute.For<ISmallKeyShuffleRequirementDictionary>();
        private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
            Substitute.For<IWorldStateRequirementDictionary>();

        private static readonly IBigKeyLayout.Factory BigKeyFactory = (bigKeyLocations, children, requirement) =>
            new BigKeyLayout(bigKeyLocations, children, requirement);
        private static readonly IEndKeyLayout.Factory EndFactory = requirement => new EndKeyLayout(requirement);
        private static readonly ISmallKeyLayout.Factory SmallKeyFactory =
            (count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement) => new SmallKeyLayout(
                count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement);

        private readonly TRKeyLayoutFactory _sut = new(
            AggregateRequirements, AlternativeRequirements, BigKeyShuffleRequirements,
            EntranceShuffleRequirements, KeyDropShuffleRequirements,
            SmallKeyShuffleRequirements, WorldStateRequirements, BigKeyFactory,
            EndFactory, SmallKeyFactory);

        [Fact]
        public void GetDungeonKeyLayouts_ShouldReturnExpected()
        {
            var dungeon = Substitute.For<IDungeon>();
            
            var expected = (new List<IKeyLayout>
            {
                EndFactory(AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    SmallKeyShuffleRequirements[true]
                }]),
                SmallKeyFactory(
                    2, new List<DungeonItemID>
                    {
                        DungeonItemID.TRCompassChest,
                        DungeonItemID.TRRollerRoomLeft,
                        DungeonItemID.TRRollerRoomRight,
                        DungeonItemID.TRChainChomps
                    },
                    false, new List<IKeyLayout>
                    {
                        SmallKeyFactory(
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
                                SmallKeyFactory(
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
                                    false, new List<IKeyLayout> {EndFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    dungeon, AggregateRequirements[new HashSet<IRequirement>
                    {
                        BigKeyShuffleRequirements[true],
                        EntranceShuffleRequirements[EntranceShuffle.None],
                        KeyDropShuffleRequirements[false],
                        WorldStateRequirements[WorldState.StandardOpen]
                    }]),
                SmallKeyFactory(
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
                    false, new List<IKeyLayout> {EndFactory()}, dungeon,
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon],
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity],
                            WorldStateRequirements[WorldState.Inverted]
                        }],
                        BigKeyShuffleRequirements[true],
                        KeyDropShuffleRequirements[false]
                    }]),
                BigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.TRCompassChest,
                        DungeonItemID.TRRollerRoomLeft,
                        DungeonItemID.TRRollerRoomRight,
                        DungeonItemID.TRChainChomps
                    },
                    new List<IKeyLayout>
                    {
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
                            2, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps
                            },
                            true, new List<IKeyLayout>
                            {
                                SmallKeyFactory(
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
                                        SmallKeyFactory(
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
                                            true, new List<IKeyLayout> {EndFactory()}, dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        BigKeyShuffleRequirements[false],
                        EntranceShuffleRequirements[EntranceShuffle.None],
                        KeyDropShuffleRequirements[false],
                        WorldStateRequirements[WorldState.StandardOpen]
                    }]),
                BigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.TRBigKeyChest}, new List<IKeyLayout>
                    {
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
                            2, new List<DungeonItemID>
                            {
                                DungeonItemID.TRCompassChest,
                                DungeonItemID.TRRollerRoomLeft,
                                DungeonItemID.TRRollerRoomRight,
                                DungeonItemID.TRChainChomps
                            },
                            false, new List<IKeyLayout>
                            {
                                SmallKeyFactory(
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
                                        SmallKeyFactory(
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
                                            false, new List<IKeyLayout> {EndFactory()}, dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        BigKeyShuffleRequirements[false],
                        EntranceShuffleRequirements[EntranceShuffle.None],
                        KeyDropShuffleRequirements[false],
                        WorldStateRequirements[WorldState.StandardOpen]
                    }]),
                BigKeyFactory(
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
                        EndFactory(SmallKeyShuffleRequirements[false]),
                        SmallKeyFactory(
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
                            true, new List<IKeyLayout> {EndFactory()}, dungeon)
                    },
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon],
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity],
                            WorldStateRequirements[WorldState.Inverted]
                        }],
                        BigKeyShuffleRequirements[false],
                        KeyDropShuffleRequirements[false]
                    }]),
                BigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.TRBigKeyChest},
                    new List<IKeyLayout>
                    {
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
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
                            false, new List<IKeyLayout> {EndFactory()}, dungeon)
                    },
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon],
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity],
                            WorldStateRequirements[WorldState.Inverted]
                        }],
                        BigKeyShuffleRequirements[false],
                        KeyDropShuffleRequirements[false]
                    }]),
                SmallKeyFactory(
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
                        SmallKeyFactory(
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
                                SmallKeyFactory(
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
                                    false, new List<IKeyLayout> {EndFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    dungeon,
                   AggregateRequirements[new HashSet<IRequirement>
                   {
                       BigKeyShuffleRequirements[true],
                       EntranceShuffleRequirements[EntranceShuffle.None],
                       KeyDropShuffleRequirements[true],
                       WorldStateRequirements[WorldState.StandardOpen]
                   }]),
                SmallKeyFactory(
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
                    false, new List<IKeyLayout> {EndFactory()}, dungeon,
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon],
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity],
                            WorldStateRequirements[WorldState.Inverted]
                        }],
                        BigKeyShuffleRequirements[true],
                        KeyDropShuffleRequirements[true]
                    }]),
                BigKeyFactory(
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
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
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
                                SmallKeyFactory(
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
                                        SmallKeyFactory(
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
                                            true, new List<IKeyLayout> {EndFactory()},
                                            dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        BigKeyShuffleRequirements[false],
                        EntranceShuffleRequirements[EntranceShuffle.None],
                        KeyDropShuffleRequirements[true],
                        WorldStateRequirements[WorldState.StandardOpen]
                    }]),
                BigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.TRPokey2Drop}, new List<IKeyLayout>
                    {
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
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
                                SmallKeyFactory(
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
                                        SmallKeyFactory(
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
                                            false, new List<IKeyLayout> {EndFactory()}, dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        BigKeyShuffleRequirements[false],
                        EntranceShuffleRequirements[EntranceShuffle.None],
                        KeyDropShuffleRequirements[true],
                        WorldStateRequirements[WorldState.StandardOpen]
                    }]),
                BigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.TRBigKeyChest}, new List<IKeyLayout>
                    {
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
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
                                SmallKeyFactory(
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
                                        SmallKeyFactory(6,
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
                                            false, new List<IKeyLayout> {EndFactory()}, dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        BigKeyShuffleRequirements[false],
                        EntranceShuffleRequirements[EntranceShuffle.None],
                        KeyDropShuffleRequirements[true],
                        WorldStateRequirements[WorldState.StandardOpen]
                    }]),
                BigKeyFactory(
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
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
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
                            true, new List<IKeyLayout> {EndFactory()}, dungeon)
                    },
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon],
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity],
                            WorldStateRequirements[WorldState.Inverted]
                        }],
                        BigKeyShuffleRequirements[false],
                        KeyDropShuffleRequirements[true]
                    }]),
                BigKeyFactory(new List<DungeonItemID> {DungeonItemID.TRBigKeyChest},
                    new List<IKeyLayout>
                    {
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
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
                            false, new List<IKeyLayout> {EndFactory()}, dungeon)
                    },
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.Dungeon],
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity],
                            WorldStateRequirements[WorldState.Inverted]
                        }],
                        BigKeyShuffleRequirements[false],
                        KeyDropShuffleRequirements[true]
                    }]),
            }).ToExpectedObject();
            
            expected.ShouldEqual(_sut.GetDungeonKeyLayouts(dungeon));
        }
    }
}