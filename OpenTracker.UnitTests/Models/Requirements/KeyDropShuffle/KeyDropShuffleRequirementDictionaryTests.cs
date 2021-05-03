using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.KeyDropShuffle
{
    public class KeyDropShuffleRequirementDictionaryTests
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly KeyDropShuffleRequirementDictionary _sut;

        public KeyDropShuffleRequirementDictionaryTests()
        {
            static IKeyDropShuffleRequirement Factory(bool expectedValue)
            {
                return Substitute.For<IKeyDropShuffleRequirement>();
            }

            _sut = new KeyDropShuffleRequirementDictionary(Factory);
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
            var sut = scope.Resolve<IKeyDropShuffleRequirementDictionary>();
            
            Assert.NotNull(sut as KeyDropShuffleRequirementDictionary);
        }
    }
}