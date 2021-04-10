using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.GuaranteedBossItems
{
    public class GuaranteedBossItemsRequirementDictionaryTests
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly GuaranteedBossItemsRequirementDictionary _sut;

        public GuaranteedBossItemsRequirementDictionaryTests()
        {
            static IGuaranteedBossItemsRequirement Factory(bool expectedValue)
            {
                return Substitute.For<IGuaranteedBossItemsRequirement>();
            }

            _sut = new GuaranteedBossItemsRequirementDictionary(Factory);
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
            var sut = scope.Resolve<IGuaranteedBossItemsRequirementDictionary>();
            
            Assert.NotNull(sut as GuaranteedBossItemsRequirementDictionary);
        }
    }
}