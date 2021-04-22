using Autofac;
using OpenTracker.Models.Sections.Factories;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Factories
{
    public class ShopSectionFactoryTests
    {
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IShopSectionFactory>();
            
            Assert.NotNull(sut as ShopSectionFactory);
        }
    }
}