using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.CompassShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.CompassShuffle
{
    public class CompassShuffleRequirementDictionaryTests
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly CompassShuffleRequirementDictionary _sut;

        public CompassShuffleRequirementDictionaryTests()
        {
            static ICompassShuffleRequirement Factory(bool expectedValue)
            {
                return Substitute.For<ICompassShuffleRequirement>();
            }

            _sut = new CompassShuffleRequirementDictionary(Factory);
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
            var sut = scope.Resolve<ICompassShuffleRequirementDictionary>();
            
            Assert.NotNull(sut as CompassShuffleRequirementDictionary);
        }
    }
}