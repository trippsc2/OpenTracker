using Autofac;
using Autofac.Core;
using OpenTracker.Models.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class CappedItemTests
    {
        [Theory]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Ctor_ExceptionTests(int starting, int maximum)
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<CappedItem.Factory>();
            
            Assert.Throws<DependencyResolutionException>(() =>
            {
                factory(starting, maximum, null);
            });
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(0, 1, true)]
        [InlineData(1, 1, false)]
        [InlineData(1, 2, true)]
        public void CanAdd_Tests(int starting, int maximum, bool expected)
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<CappedItem.Factory>();

            var item = factory(starting, maximum, null);

            Assert.Equal(expected, item.CanAdd());
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 2, 2)]
        public void Add_Tests(int starting, int maximum, int expected)
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<CappedItem.Factory>();

            var item = factory(starting, maximum, null);
            item.Add();

            Assert.Equal(expected, item.Current);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(0, 2, 2)]
        [InlineData(1, 2, 0)]
        public void Remove_Tests(int starting, int maximum, int expected)
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<CappedItem.Factory>();

            var item = factory(starting, maximum, null);
            item.Remove();

            Assert.Equal(expected, item.Current);
        }
    }
}
