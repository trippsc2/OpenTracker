using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts
{
    public class SmallKeyLayoutTests
    {
        private readonly IList<DungeonItemID> _smallKeyLocations = new List<DungeonItemID>();
        private readonly IList<IKeyLayout> _children = new List<IKeyLayout>();
        private readonly IRequirement _requirement = Substitute.For<IRequirement>();
        private readonly IDungeon _dungeon = Substitute.For<IDungeon>();
        
        [Fact]
        public void CanBeTrue_ShouldReturnFalse_WhenRequirementMetReturnsFalse()
        {
            var sut = new SmallKeyLayout(
                1, _smallKeyLocations, false, _children, _dungeon, _requirement);

            _requirement.Met.Returns(false);
            var state = Substitute.For<IDungeonState>();

            Assert.False(sut.CanBeTrue(new List<DungeonItemID>(), new List<DungeonItemID>(), state));
        }

        [Fact]
        public void CanBeTrue_ShouldReturnFalse_WhenNoChildReturnsFalse()
        {
            const DungeonItemID id = DungeonItemID.HCSanctuary;
            
            var sut = new SmallKeyLayout(
                1, _smallKeyLocations, false, _children, _dungeon, _requirement);

            _smallKeyLocations.Add(id);
            _requirement.Met.Returns(true);
            var state = Substitute.For<IDungeonState>();

            _dungeon.SmallKey.Maximum.Returns(1);
            
            _children.Add(Substitute.For<IKeyLayout>());
            _children[0].CanBeTrue(
                Arg.Any<IList<DungeonItemID>>(), Arg.Any<IList<DungeonItemID>>(),
                Arg.Any<IDungeonState>()).Returns(false);

            Assert.False(sut.CanBeTrue(
                new List<DungeonItemID> {id}, new List<DungeonItemID>(), state));
        }

        [Theory]
        [InlineData(true, false, 0, false, false, false, false)]
        [InlineData(true, false, 0, false, false, false, true)]
        [InlineData(true, false, 0, false, false, true, false)]
        [InlineData(true, false, 0, false, false, true, true)]
        [InlineData(true, false, 0, false, true, false, false)]
        [InlineData(true, false, 0, false, true, false, true)]
        [InlineData(true, false, 0, false, true, true, false)]
        [InlineData(false, false, 0, false, true, true, true)]
        [InlineData(true, false, 0, true, false, false, false)]
        [InlineData(true, false, 0, true, false, false, true)]
        [InlineData(true, false, 0, true, false, true, false)]
        [InlineData(true, false, 0, true, false, true, true)]
        [InlineData(true, false, 0, true, true, false, false)]
        [InlineData(true, false, 0, true, true, false, true)]
        [InlineData(true, false, 0, true, true, true, false)]
        [InlineData(false, false, 0, true, true, true, true)]
        [InlineData(false, false, 1, false, false, false, false)]
        [InlineData(true, false, 1, false, false, false, true)]
        [InlineData(true, false, 1, false, false, true, false)]
        [InlineData(true, false, 1, false, false, true, true)]
        [InlineData(true, false, 1, false, true, false, false)]
        [InlineData(true, false, 1, false, true, false, true)]
        [InlineData(true, false, 1, false, true, true, false)]
        [InlineData(true, false, 1, false, true, true, true)]
        [InlineData(false, false, 1, true, false, false, false)]
        [InlineData(true, false, 1, true, false, false, true)]
        [InlineData(true, false, 1, true, false, true, false)]
        [InlineData(true, false, 1, true, false, true, true)]
        [InlineData(true, false, 1, true, true, false, false)]
        [InlineData(true, false, 1, true, true, false, true)]
        [InlineData(true, false, 1, true, true, true, false)]
        [InlineData(true, false, 1, true, true, true, true)]
        [InlineData(true, true, 0, false, false, false, false)]
        [InlineData(true, true, 0, false, false, false, true)]
        [InlineData(true, true, 0, false, false, true, false)]
        [InlineData(false, true, 0, false, false, true, true)]
        [InlineData(true, true, 0, false, true, false, false)]
        [InlineData(false, true, 0, false, true, false, true)]
        [InlineData(false, true, 0, false, true, true, false)]
        [InlineData(false, true, 0, false, true, true, true)]
        [InlineData(true, true, 0, true, false, false, true)]
        [InlineData(true, true, 0, true, false, true, false)]
        [InlineData(true, true, 0, true, false, true, true)]
        [InlineData(true, true, 0, true, true, false, false)]
        [InlineData(true, true, 0, true, true, false, true)]
        [InlineData(true, true, 0, true, true, true, false)]
        [InlineData(false, true, 0, true, true, true, true)]
        [InlineData(false, true, 1, false, false, false, false)]
        [InlineData(true, true, 1, false, false, false, true)]
        [InlineData(true, true, 1, false, false, true, false)]
        [InlineData(true, true, 1, false, false, true, true)]
        [InlineData(true, true, 1, false, true, false, false)]
        [InlineData(true, true, 1, false, true, false, true)]
        [InlineData(true, true, 1, false, true, true, false)]
        [InlineData(false, true, 1, true, false, false, false)]
        [InlineData(false, true, 1, true, false, false, true)]
        [InlineData(false, true, 1, true, false, true, false)]
        [InlineData(true, true, 1, true, false, true, true)]
        [InlineData(false, true, 1, true, true, false, false)]
        [InlineData(true, true, 1, true, true, false, true)]
        [InlineData(true, true, 1, true, true, true, false)]
        [InlineData(true, true, 1, true, true, true, true)]
        public void CanBeTrue_ShouldReturnExpected(
            bool expected, bool bigKeyInLocations, int keysCollected, bool bigKeyCollected, bool item1Accessible,
            bool item2Accessible, bool item3Accessible)
        {
            _dungeon.SmallKey.Maximum.Returns(1);
            _requirement.Met.Returns(true);
            _children.Add(Substitute.For<IKeyLayout>());
            _children[0].CanBeTrue(
                Arg.Any<IList<DungeonItemID>>(), Arg.Any<IList<DungeonItemID>>(),
                Arg.Any<IDungeonState>()).Returns(true);

            var sut = new SmallKeyLayout(
                1, _smallKeyLocations, bigKeyInLocations, _children, _dungeon, _requirement);

            const DungeonItemID id1 = DungeonItemID.HCSanctuary;
            const DungeonItemID id2 = DungeonItemID.HCMapChest;
            const DungeonItemID id3 = DungeonItemID.HCBoomerangChest;
            
            _smallKeyLocations.Add(id1);
            _smallKeyLocations.Add(id2);
            _smallKeyLocations.Add(id3);

            var inaccessible = new List<DungeonItemID>();
            var accessible = new List<DungeonItemID>();

            if (item1Accessible)
            {
                accessible.Add(id1);
            }
            else
            {
                inaccessible.Add(id1);
            }

            if (item2Accessible)
            {
                accessible.Add(id2);
            }
            else
            {
                inaccessible.Add(id2);
            }

            if (item3Accessible)
            {
                accessible.Add(id3);
            }
            else
            {
                inaccessible.Add(id3);
            }

            var state = Substitute.For<IDungeonState>();
            state.KeysCollected.Returns(keysCollected);
            state.BigKeyCollected.Returns(bigKeyCollected);
            
            Assert.Equal(expected, sut.CanBeTrue(inaccessible, accessible, state));
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ISmallKeyLayout.Factory>();
            var sut = factory(
                1, _smallKeyLocations, false, _children, _dungeon, _requirement);
            
            Assert.NotNull(sut as SmallKeyLayout);
        }
    }
}