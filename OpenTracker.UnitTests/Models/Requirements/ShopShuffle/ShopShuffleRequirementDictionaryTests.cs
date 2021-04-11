using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.ShopShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.ShopShuffle
{
    public class ShopShuffleRequirementDictionaryTests
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly ShopShuffleRequirementDictionary _sut;

        public ShopShuffleRequirementDictionaryTests()
        {
            static IShopShuffleRequirement Factory(bool expectedValue)
            {
                return Substitute.For<IShopShuffleRequirement>();
            }

            _sut = new ShopShuffleRequirementDictionary(Factory);
        }

        [Fact]
        public void Indexer_ShouldReturnTheSameInstance()
        {
            var requirement1 = _sut[false];
            var requirement2 = _sut[false];
            
            Assert.Equal(requirement1, requirement2);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IShopShuffleRequirementDictionary>();
            
            Assert.NotNull(sut as ShopShuffleRequirementDictionary);
        }
    }
}