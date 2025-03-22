using System.Collections.Generic;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.KeyLayouts.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories
{
    public class PoDKeyLayoutFactoryTests
    {
        private static readonly IAggregateRequirementDictionary AggregateRequirements =
            new AggregateRequirementDictionary(requirements =>
                new AggregateRequirement(requirements));
        private static readonly IBigKeyShuffleRequirementDictionary BigKeyShuffleRequirements =
            Substitute.For<IBigKeyShuffleRequirementDictionary>();
        private static readonly ISmallKeyShuffleRequirementDictionary SmallKeyShuffleRequirements =
            Substitute.For<ISmallKeyShuffleRequirementDictionary>();

        private static readonly IBigKeyLayout.Factory BigKeyFactory = (bigKeyLocations, children, requirement) =>
            new BigKeyLayout(bigKeyLocations, children, requirement);
        private static readonly IEndKeyLayout.Factory EndFactory = requirement => new EndKeyLayout(requirement);
        private static readonly ISmallKeyLayout.Factory SmallKeyFactory =
            (count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement) => new SmallKeyLayout(
                count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement);

        private readonly PoDKeyLayoutFactory _sut = new(
            AggregateRequirements, BigKeyShuffleRequirements, SmallKeyShuffleRequirements, BigKeyFactory, EndFactory,
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
                    4, new List<DungeonItemID>
                    {
                        DungeonItemID.PoDShooterRoom,
                        DungeonItemID.PoDMapChest,
                        DungeonItemID.PoDArenaLedge,
                        DungeonItemID.PoDBigKeyChest,
                        DungeonItemID.PoDStalfosBasement,
                        DungeonItemID.PoDArenaBridge
                    },
                    false, new List<IKeyLayout>
                    {
                        SmallKeyFactory(
                            6, new List<DungeonItemID>
                            {
                                DungeonItemID.PoDShooterRoom,
                                DungeonItemID.PoDMapChest,
                                DungeonItemID.PoDArenaLedge,
                                DungeonItemID.PoDBigKeyChest,
                                DungeonItemID.PoDStalfosBasement,
                                DungeonItemID.PoDArenaBridge,
                                DungeonItemID.PoDCompassChest,
                                DungeonItemID.PoDDarkBasementLeft,
                                DungeonItemID.PoDDarkBasementRight,
                                DungeonItemID.PoDHarmlessHellway
                            },
                            false, new List<IKeyLayout> {EndFactory()}, dungeon)
                    },
                    dungeon, BigKeyShuffleRequirements[true]),
                BigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.PoDShooterRoom,
                        DungeonItemID.PoDMapChest,
                        DungeonItemID.PoDArenaLedge,
                        DungeonItemID.PoDBigKeyChest,
                        DungeonItemID.PoDStalfosBasement,
                        DungeonItemID.PoDArenaBridge
                    },
                    new List<IKeyLayout>
                    {
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.PoDShooterRoom,
                                DungeonItemID.PoDMapChest,
                                DungeonItemID.PoDArenaLedge,
                                DungeonItemID.PoDBigKeyChest,
                                DungeonItemID.PoDStalfosBasement,
                                DungeonItemID.PoDArenaBridge
                            },
                            true, new List<IKeyLayout>
                            {
                                SmallKeyFactory(
                                    6, new List<DungeonItemID>
                                    {
                                        DungeonItemID.PoDShooterRoom,
                                        DungeonItemID.PoDMapChest,
                                        DungeonItemID.PoDArenaLedge,
                                        DungeonItemID.PoDBigKeyChest,
                                        DungeonItemID.PoDStalfosBasement,
                                        DungeonItemID.PoDArenaBridge,
                                        DungeonItemID.PoDCompassChest,
                                        DungeonItemID.PoDDarkBasementLeft,
                                        DungeonItemID.PoDDarkBasementRight,
                                        DungeonItemID.PoDHarmlessHellway
                                    },
                                    true, new List<IKeyLayout> {EndFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    BigKeyShuffleRequirements[false]),
                BigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.PoDCompassChest,
                        DungeonItemID.PoDDarkBasementLeft,
                        DungeonItemID.PoDDarkBasementRight,
                        DungeonItemID.PoDHarmlessHellway
                    },
                    new List<IKeyLayout>
                    {
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.PoDShooterRoom,
                                DungeonItemID.PoDMapChest,
                                DungeonItemID.PoDArenaLedge,
                                DungeonItemID.PoDBigKeyChest,
                                DungeonItemID.PoDStalfosBasement,
                                DungeonItemID.PoDArenaBridge
                            },
                            false, new List<IKeyLayout>
                            {
                                SmallKeyFactory(
                                    6, new List<DungeonItemID>
                                    {
                                        DungeonItemID.PoDShooterRoom,
                                        DungeonItemID.PoDMapChest,
                                        DungeonItemID.PoDArenaLedge,
                                        DungeonItemID.PoDBigKeyChest,
                                        DungeonItemID.PoDStalfosBasement,
                                        DungeonItemID.PoDArenaBridge,
                                        DungeonItemID.PoDCompassChest,
                                        DungeonItemID.PoDDarkBasementLeft,
                                        DungeonItemID.PoDDarkBasementRight,
                                        DungeonItemID.PoDHarmlessHellway
                                    },
                                    true, new List<IKeyLayout> {EndFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    BigKeyShuffleRequirements[false]),
                BigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.PoDDarkMazeTop,
                        DungeonItemID.PoDDarkMazeBottom
                    },
                    new List<IKeyLayout>
                    {
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.PoDShooterRoom,
                                DungeonItemID.PoDMapChest,
                                DungeonItemID.PoDArenaLedge,
                                DungeonItemID.PoDBigKeyChest,
                                DungeonItemID.PoDStalfosBasement,
                                DungeonItemID.PoDArenaBridge
                            },
                            false, new List<IKeyLayout>
                            {
                                SmallKeyFactory(
                                    6, new List<DungeonItemID>
                                    {
                                        DungeonItemID.PoDShooterRoom,
                                        DungeonItemID.PoDMapChest,
                                        DungeonItemID.PoDArenaLedge,
                                        DungeonItemID.PoDBigKeyChest,
                                        DungeonItemID.PoDStalfosBasement,
                                        DungeonItemID.PoDArenaBridge,
                                        DungeonItemID.PoDCompassChest,
                                        DungeonItemID.PoDDarkBasementLeft,
                                        DungeonItemID.PoDDarkBasementRight,
                                        DungeonItemID.PoDHarmlessHellway
                                    },
                                    false, new List<IKeyLayout> {EndFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    BigKeyShuffleRequirements[false])
            }).ToExpectedObject();
            
            expected.ShouldEqual(_sut.GetDungeonKeyLayouts(dungeon));
        }
    }
}