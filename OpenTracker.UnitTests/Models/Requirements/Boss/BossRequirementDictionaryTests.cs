using Autofac;
using NSubstitute;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Requirements.Boss;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Boss
{
    public class BossRequirementDictionaryTests
    {
        private readonly IBossPlacementDictionary _bossPlacements = Substitute.For<IBossPlacementDictionary>();

        private readonly BossRequirementDictionary _sut;

        public BossRequirementDictionaryTests()
        {
            _sut = new BossRequirementDictionary(_bossPlacements, _ => Substitute.For<IBossRequirement>());
        }

        [Fact]
        public void Indexer_ShouldReturnTheSameInstance()
        {
            var requirement1 = _sut[BossPlacementID.ATBoss];
            var requirement2 = _sut[BossPlacementID.ATBoss];
            
            Assert.Equal(requirement1, requirement2);
        }
        
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IBossRequirementDictionary>();
            
            Assert.NotNull(sut as BossRequirementDictionary);
        }
    }
}