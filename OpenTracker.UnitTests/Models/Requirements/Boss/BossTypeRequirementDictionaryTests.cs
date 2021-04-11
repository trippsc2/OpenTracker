using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Requirements.Boss;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Boss
{
    public class BossTypeRequirementDictionaryTests
    {
        private readonly IBossTypeRequirementFactory _factory = Substitute.For<IBossTypeRequirementFactory>();

        private readonly BossTypeRequirementDictionary _sut;

        public BossTypeRequirementDictionaryTests()
        {
            _sut = new BossTypeRequirementDictionary(() => _factory);
        }

        [Fact]
        public void NoBoss_ShouldCallGetBossTypeRequirement()
        {
            _ = _sut.NoBoss;
        }

        [Fact]
        public void Indexer_ShouldCallGetBossTypeRequirement()
        {
            const BossType bossType = BossType.Aga;
            _ = _sut[bossType];
            _ = _sut[bossType];

            _factory.Received(1).GetBossTypeRequirement(bossType);
        }
        
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IBossTypeRequirementDictionary>();
            
            Assert.NotNull(sut as BossTypeRequirementDictionary);
        }
    }
}