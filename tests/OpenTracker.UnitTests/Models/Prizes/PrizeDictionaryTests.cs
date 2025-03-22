using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Prizes;
using Xunit;

namespace OpenTracker.UnitTests.Models.Prizes
{
    public class PrizeDictionaryTests
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly PrizeDictionary _sut = new((_, _) => Substitute.For<IItem>());
        
        [Fact]
        public void Indexer_ShouldReturnTheSameInstance()
        {
            var prize1 = _sut[PrizeType.Aga1];
            var prize2 = _sut[PrizeType.Aga1];
            
            Assert.Equal(prize1, prize2);
        }

        [Fact]
        public void Indexer_ShouldReturnTheDifferentInstances()
        {
            var prize1 = _sut[PrizeType.Aga1];
            var prize2 = _sut[PrizeType.Aga2];
            
            Assert.NotEqual(prize1, prize2);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IPrizeDictionary>();
            
            Assert.NotNull(sut as PrizeDictionary);
        }
    }
}