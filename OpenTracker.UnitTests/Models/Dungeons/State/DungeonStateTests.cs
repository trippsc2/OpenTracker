using System.Collections.Generic;
using NSubstitute;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.KeyDoors;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.State
{
    public class DungeonStateTests
    {
        [Fact]
        public void Ctor_ShouldSetUnlockedDoors()
        {
            var unlockedDoors = Substitute.For<IList<KeyDoorID>>();
            var sut = new DungeonState(unlockedDoors, 0, false, false);
            
            Assert.Equal(unlockedDoors, sut.UnlockedDoors);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void Ctor_ShouldSetKeysCollected(int expected, int keysCollected)
        {
            var sut = new DungeonState(
                new List<KeyDoorID>(), keysCollected, false, false);
            
            Assert.Equal(expected, sut.KeysCollected);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Ctor_ShouldSetBigKeyCollected(bool expected, bool bigKeyCollected)
        {
            var sut = new DungeonState(
                new List<KeyDoorID>(), 0, bigKeyCollected, false);
            
            Assert.Equal(expected, sut.BigKeyCollected);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Ctor_ShouldSetSequenceBreak(bool expected, bool sequenceBreak)
        {
            var sut = new DungeonState(
                new List<KeyDoorID>(), 0, false, sequenceBreak);
            
            Assert.Equal(expected, sut.SequenceBreak);
        }
    }
}