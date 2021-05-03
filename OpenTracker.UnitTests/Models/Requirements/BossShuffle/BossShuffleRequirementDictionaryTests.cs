using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.BossShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.BossShuffle
{
    public class BossShuffleRequirementDictionaryTests
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly BossShuffleRequirementDictionary _sut;

        public BossShuffleRequirementDictionaryTests()
        {
            static IBossShuffleRequirement Factory(bool expectedValue)
            {
                return Substitute.For<IBossShuffleRequirement>();
            }

            _sut = new BossShuffleRequirementDictionary(Factory);
        }

        [Fact]
        public void Indexer_ShouldReturnTheSameInstance()
        {
            var requirement1 = _sut[false];
            var requirement2 = _sut[false];
            
            Assert.Equal(requirement1, requirement2);
        }

        [Fact]
        public void Indexer_ShouldReturnTheDifferentInstances()
        {
            var requirement1 = _sut[false];
            var requirement2 = _sut[true];
            
            Assert.NotEqual(requirement1, requirement2);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IBossShuffleRequirementDictionary>();
            
            Assert.NotNull(sut as BossShuffleRequirementDictionary);
        }
    }
}