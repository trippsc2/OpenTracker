using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Mode
{
    public class ItemPlacementRequirementDictionaryTests
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly ItemPlacementRequirementDictionary _sut;

        public ItemPlacementRequirementDictionaryTests()
        {
            static IItemPlacementRequirement Factory(ItemPlacement expectedValue)
            {
                return Substitute.For<IItemPlacementRequirement>();
            }

            _sut = new ItemPlacementRequirementDictionary(Factory);
        }

        [Fact]
        public void Indexer_ShouldReturnTheSameInstance()
        {
            var requirement1 = _sut[ItemPlacement.Basic];
            var requirement2 = _sut[ItemPlacement.Basic];
            
            Assert.Equal(requirement1, requirement2);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IItemPlacementRequirementDictionary>();
            
            Assert.NotNull(sut as ItemPlacementRequirementDictionary);
        }
    }
}