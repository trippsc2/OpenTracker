using Autofac;
using OpenTracker.Models.Requirements.Boss;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Boss
{
    public class BossRequirementDictionaryTests
    {
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IBossRequirementDictionary>();
            
            Assert.NotNull(sut as BossRequirementDictionary);
        }
    }
}