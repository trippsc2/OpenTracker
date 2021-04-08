using System.Collections.Generic;
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
    }
}