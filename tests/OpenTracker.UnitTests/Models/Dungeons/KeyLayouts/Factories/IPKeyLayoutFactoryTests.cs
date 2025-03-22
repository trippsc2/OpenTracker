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
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories
{
    public class IPKeyLayoutFactoryTests
    {
        private static readonly IAggregateRequirementDictionary AggregateRequirements =
            new AggregateRequirementDictionary(requirements =>
                new AggregateRequirement(requirements));
        private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
            new AlternativeRequirementDictionary(requirements =>
                new AlternativeRequirement(requirements));
        private static readonly IBigKeyShuffleRequirementDictionary BigKeyShuffleRequirements =
            Substitute.For<IBigKeyShuffleRequirementDictionary>();
        private static readonly IGuaranteedBossItemsRequirementDictionary GuaranteedBossItemsRequirements =
            Substitute.For<IGuaranteedBossItemsRequirementDictionary>();
        private static readonly IItemPlacementRequirementDictionary ItemPlacementRequirements =
            Substitute.For<IItemPlacementRequirementDictionary>();
        private static readonly IKeyDropShuffleRequirementDictionary KeyDropShuffleRequirements =
            Substitute.For<IKeyDropShuffleRequirementDictionary>();
        private static readonly ISmallKeyShuffleRequirementDictionary SmallKeyShuffleRequirements =
            Substitute.For<ISmallKeyShuffleRequirementDictionary>();

        private static readonly IBigKeyLayout.Factory BigKeyFactory = (bigKeyLocations, children, requirement) =>
            new BigKeyLayout(bigKeyLocations, children, requirement);
        private static readonly IEndKeyLayout.Factory EndFactory = requirement => new EndKeyLayout(requirement);
        private static readonly ISmallKeyLayout.Factory SmallKeyFactory =
            (count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement) => new SmallKeyLayout(
                count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement);

        private readonly IPKeyLayoutFactory _sut = new(
            AggregateRequirements, AlternativeRequirements, BigKeyShuffleRequirements,
            GuaranteedBossItemsRequirements, ItemPlacementRequirements, KeyDropShuffleRequirements,
            SmallKeyShuffleRequirements, BigKeyFactory, EndFactory,
            SmallKeyFactory);

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
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPBigChest,
                            DungeonItemID.IPIcedTRoom
                        },
                        false, new List<IKeyLayout> {EndFactory()}, dungeon,
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                GuaranteedBossItemsRequirements[true],
                                ItemPlacementRequirements[ItemPlacement.Basic]
                            }],
                            BigKeyShuffleRequirements[true],
                            KeyDropShuffleRequirements[false],
                            SmallKeyShuffleRequirements[false]
                        }]),
                    SmallKeyFactory(
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
                        false, new List<IKeyLayout> {EndFactory()}, dungeon,
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            BigKeyShuffleRequirements[true],
                            GuaranteedBossItemsRequirements[false],
                            ItemPlacementRequirements[ItemPlacement.Advanced],
                            KeyDropShuffleRequirements[false],
                            SmallKeyShuffleRequirements[false]
                        }]),
                    BigKeyFactory(
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
                            EndFactory(SmallKeyShuffleRequirements[true]),
                            SmallKeyFactory(
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
                                true, new List<IKeyLayout> {EndFactory()}, dungeon,
                                AggregateRequirements[new HashSet<IRequirement>
                                {
                                    AlternativeRequirements[new HashSet<IRequirement>
                                    {
                                        GuaranteedBossItemsRequirements[true],
                                        ItemPlacementRequirements[ItemPlacement.Basic]
                                    }],
                                    SmallKeyShuffleRequirements[false]
                                }]),
                            SmallKeyFactory(
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
                                true, new List<IKeyLayout> {EndFactory()}, dungeon,
                                AggregateRequirements[new HashSet<IRequirement>
                                {
                                    GuaranteedBossItemsRequirements[false],
                                    ItemPlacementRequirements[ItemPlacement.Advanced],
                                    SmallKeyShuffleRequirements[false]
                                }])
                        },
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            BigKeyShuffleRequirements[false],
                            KeyDropShuffleRequirements[false]
                        }]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.IPJellyDrop},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPJellyDrop,
                                    DungeonItemID.IPConveyerDrop
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
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
                                        false, new List<IKeyLayout> {EndFactory()}, dungeon,
                                        AlternativeRequirements[new HashSet<IRequirement>
                                        {
                                            GuaranteedBossItemsRequirements[true],
                                            ItemPlacementRequirements[ItemPlacement.Basic]
                                        }]),
                                    SmallKeyFactory(5,
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
                                            SmallKeyFactory(
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
                                                false, new List<IKeyLayout> {EndFactory()},
                                                dungeon)
                                        },
                                        dungeon, AggregateRequirements[new HashSet<IRequirement>
                                        {
                                            GuaranteedBossItemsRequirements[false],
                                            ItemPlacementRequirements[ItemPlacement.Advanced]
                                        }])
                                },
                                dungeon)
                        },
                        dungeon, AggregateRequirements[new HashSet<IRequirement>
                        {
                            BigKeyShuffleRequirements[true],
                            KeyDropShuffleRequirements[true]
                        }]),
                    BigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPConveyerDrop
                        },
                        new List<IKeyLayout>
                        {
                            EndFactory(SmallKeyShuffleRequirements[true]),
                            SmallKeyFactory(
                                1, new List<DungeonItemID> {DungeonItemID.IPJellyDrop},
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        2, new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPJellyDrop,
                                            DungeonItemID.IPConveyerDrop
                                        },
                                        true, new List<IKeyLayout>
                                        {
                                            SmallKeyFactory(
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
                                                true, new List<IKeyLayout> {EndFactory()},
                                                dungeon, AlternativeRequirements[new HashSet<IRequirement>
                                                {
                                                    GuaranteedBossItemsRequirements[true],
                                                    ItemPlacementRequirements[ItemPlacement.Basic]
                                                }]),
                                            SmallKeyFactory(
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
                                                    SmallKeyFactory(
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
                                                        new List<IKeyLayout> {EndFactory()}, dungeon)
                                                },
                                                dungeon, AggregateRequirements[new HashSet<IRequirement>
                                                {
                                                    GuaranteedBossItemsRequirements[false],
                                                    ItemPlacementRequirements[ItemPlacement.Advanced]
                                                }])
                                        },
                                        dungeon)
                                },
                                dungeon)
                        },
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            BigKeyShuffleRequirements[false],
                            KeyDropShuffleRequirements[true]
                        }]),
                    BigKeyFactory(
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
                            EndFactory(SmallKeyShuffleRequirements[true]),
                            SmallKeyFactory(
                                1, new List<DungeonItemID> {DungeonItemID.IPJellyDrop},
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        2, new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPJellyDrop,
                                            DungeonItemID.IPConveyerDrop
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            SmallKeyFactory(
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
                                                true, new List<IKeyLayout> {EndFactory()},
                                                dungeon, AlternativeRequirements[new HashSet<IRequirement>
                                                {
                                                    GuaranteedBossItemsRequirements[true],
                                                    ItemPlacementRequirements[ItemPlacement.Basic]
                                                }]),
                                            SmallKeyFactory(
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
                                                    SmallKeyFactory(
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
                                                        new List<IKeyLayout> {EndFactory()}, dungeon)
                                                },
                                                dungeon,
                                                AggregateRequirements[new HashSet<IRequirement>
                                                {
                                                    GuaranteedBossItemsRequirements[false],
                                                    ItemPlacementRequirements[ItemPlacement.Advanced]
                                                }])
                                        },
                                        dungeon)
                                },
                                dungeon)
                        },
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            BigKeyShuffleRequirements[false],
                            KeyDropShuffleRequirements[true]
                        }])
                }).ToExpectedObject();
            
            expected.ShouldEqual(_sut.GetDungeonKeyLayouts(dungeon));
        }
    }
}