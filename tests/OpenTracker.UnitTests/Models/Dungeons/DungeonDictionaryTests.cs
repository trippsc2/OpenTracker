using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons
{
    public class DungeonDictionaryTests
    {
        private readonly IDungeonFactory _factory = Substitute.For<IDungeonFactory>();
        
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly DungeonDictionary _sut;

        public DungeonDictionaryTests()
        {
            _sut = new DungeonDictionary(() => _factory);
        }

        [Fact]
        public void Index_ShouldCallGetDungeonOnFactory()
        {
            const DungeonID id = DungeonID.AgahnimTower;
            _ = _sut[id];

            _factory.Received().GetDungeon(id);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IDungeonDictionary>();
            
            Assert.NotNull(sut as DungeonDictionary);
        }
    }
}