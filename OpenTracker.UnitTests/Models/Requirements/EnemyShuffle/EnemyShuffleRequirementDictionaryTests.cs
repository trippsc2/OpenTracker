using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.EnemyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.EnemyShuffle
{
    public class EnemyShuffleRequirementDictionaryTests
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly EnemyShuffleRequirementDictionary _sut;

        public EnemyShuffleRequirementDictionaryTests()
        {
            static IEnemyShuffleRequirement Factory(bool expectedValue)
            {
                return Substitute.For<IEnemyShuffleRequirement>();
            }

            _sut = new EnemyShuffleRequirementDictionary(Factory);
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
            var sut = scope.Resolve<IEnemyShuffleRequirementDictionary>();
            
            Assert.NotNull(sut as EnemyShuffleRequirementDictionary);
        }
    }
}