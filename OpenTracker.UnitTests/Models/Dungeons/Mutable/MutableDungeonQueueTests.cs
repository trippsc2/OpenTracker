using System;
using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.KeyDoors;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Mutable
{
    public class MutableDungeonQueueTests
    {
        private readonly IDungeon _dungeon = Substitute.For<IDungeon>();
        private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_ShouldCreateExpectedNumberOfInstances(int expected, int count)
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var sut = new MutableDungeonQueue(_ => _dungeonData, _dungeon, count);
            
            Assert.Equal(expected, sut.Count);
        }

        [Fact]
        public void Ctor_ShouldCreateProcessorCountMinusOne()
        {
            var expected = Math.Max(1, Environment.ProcessorCount - 1);
            // ReSharper disable once CollectionNeverUpdated.Local
            var sut = new MutableDungeonQueue(_ => _dungeonData, _dungeon);
            
            Assert.Equal(expected, sut.Count);
        }

        [Fact]
        public void AutofacTests()
        {
            _dungeon.SmallKeyDoors.Returns(new List<KeyDoorID>());
            _dungeon.BigKeyDoors.Returns(new List<KeyDoorID>());
            _dungeon.DungeonItems.Returns(new List<DungeonItemID>());
            _dungeon.Bosses.Returns(new List<DungeonItemID>());
            _dungeon.SmallKeyDrops.Returns(new List<DungeonItemID>());
            _dungeon.BigKeyDrops.Returns(new List<DungeonItemID>());
            _dungeon.Nodes.Returns(new List<DungeonNodeID>());

            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IMutableDungeonQueue.Factory>();
            var sut = factory(_dungeon);
            
            Assert.NotNull(sut as MutableDungeonQueue);
        }
    }
}