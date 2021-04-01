using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Items
{
    public class DungeonItemDictionaryTests
    {
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IDungeonItemDictionary.Factory>();
            var sut = factory(Substitute.For<IMutableDungeon>());
            
            Assert.NotNull(sut as DungeonItemDictionary);
        }
    }
}