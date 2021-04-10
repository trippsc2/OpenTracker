using Autofac;
using OpenTracker.Models.Requirements.Boss;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Boss
{
    public class BossTypeRequirementDictionaryTests
    {
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IBossTypeRequirementDictionary>();
            
            Assert.NotNull(sut as BossTypeRequirementDictionary);
        }
    }
}