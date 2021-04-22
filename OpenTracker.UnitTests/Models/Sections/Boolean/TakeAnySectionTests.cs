using Autofac;
using NSubstitute;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections.Boolean;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Boolean
{
    public class TakeAnySectionTests
    {
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ITakeAnySection.Factory>();
            var sut = factory(Substitute.For<INode>(), Substitute.For<IRequirement>());
            
            Assert.NotNull(sut as TakeAnySection);
        }
    }
}