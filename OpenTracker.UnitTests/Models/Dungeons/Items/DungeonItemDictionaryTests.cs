using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Items
{
    public class DungeonItemDictionaryTests
    {
        private readonly IDungeonItemFactory _factory = Substitute.For<IDungeonItemFactory>();
        private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();
        private readonly IDungeon _dungeon = Substitute.For<IDungeon>();
        private readonly IDungeonItem _dungeonItem = Substitute.For<IDungeonItem>();
        
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly IDungeonItemDictionary _sut;

        public DungeonItemDictionaryTests()
        {
            _factory.GetDungeonItem(Arg.Any<IMutableDungeon>(), Arg.Any<DungeonItemID>()).Returns(_dungeonItem);
            _sut = new DungeonItemDictionary(() => _factory, _dungeonData);
        }

        [Fact]
        public void Index_ShouldCreateAndReturnDungeonItem()
        {
            var dungeonItem = _sut[DungeonItemID.ATBoss];
            
            Assert.Equal(_dungeonItem, dungeonItem);
        }
    
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IDungeonItemDictionary.Factory>();
            var sut = factory(_dungeonData, _dungeon);
            
            Assert.NotNull(sut as DungeonItemDictionary);
        }
    }
}