using System.Collections.Generic;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.KeyLayouts.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories
{
    public class ATKeyLayoutFactoryTests
    {
        private static readonly IAggregateRequirementDictionary AggregateRequirements =
            new AggregateRequirementDictionary(requirements =>
                new AggregateRequirement(requirements));
        private static readonly IKeyDropShuffleRequirementDictionary KeyDropShuffleRequirements =
            Substitute.For<IKeyDropShuffleRequirementDictionary>();
        private static readonly ISmallKeyShuffleRequirementDictionary SmallKeyShuffleRequirements =
            Substitute.For<ISmallKeyShuffleRequirementDictionary>();

        private static readonly IEndKeyLayout.Factory EndFactory = requirement => new EndKeyLayout(requirement);
        private static readonly ISmallKeyLayout.Factory SmallKeyFactory =
            (count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement) => new SmallKeyLayout(
                count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement);

        private readonly ATKeyLayoutFactory _sut = new(
            AggregateRequirements, KeyDropShuffleRequirements, SmallKeyShuffleRequirements,
            EndFactory, SmallKeyFactory);

        [Fact]
        public void GetDungeonKeyLayouts_ShouldReturnExpected()
        {
            var dungeon = Substitute.For<IDungeon>();
            
            var expected = (new List<IKeyLayout>
            {
                EndFactory(SmallKeyShuffleRequirements[true]),
                SmallKeyFactory(
                    2, new List<DungeonItemID> {DungeonItemID.ATRoom03, DungeonItemID.ATDarkMaze},
                    false, new List<IKeyLayout> {EndFactory()},
                    dungeon,
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        KeyDropShuffleRequirements[false],
                        SmallKeyShuffleRequirements[false]
                    }]),
                SmallKeyFactory(
                    4, new List<DungeonItemID>
                    {
                        DungeonItemID.ATRoom03,
                        DungeonItemID.ATDarkMaze,
                        DungeonItemID.ATDarkArcherDrop,
                        DungeonItemID.ATCircleOfPotsDrop
                    },
                    false, new List<IKeyLayout> {EndFactory()}, dungeon,
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        KeyDropShuffleRequirements[true],
                        SmallKeyShuffleRequirements[false]
                    }])
            }).ToExpectedObject();
            
            expected.ShouldEqual(_sut.GetDungeonKeyLayouts(dungeon));
        }
    }
}