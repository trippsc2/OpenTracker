using System;
using Autofac;
using OpenTracker.Models.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class ItemTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_CurrentTests(int starting, int expected)
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<Item.Factory>();

            var item = factory(starting, null);

            Assert.Equal(expected, item.Current);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        public void CanAdd_Tests(int starting, bool expected)
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<Item.Factory>();

            var item = factory(starting, null);

            Assert.Equal(expected, item.CanAdd());
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public void Add_Tests(int starting, int expected)
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<Item.Factory>();

            var item = factory(starting, null);
            item.Add();

            Assert.Equal(expected, item.Current);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        public void CanRemove_Tests(int starting, bool expected)
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<Item.Factory>();
            
            var item = factory(starting, null);

            Assert.Equal(expected, item.CanRemove());
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Remove_Tests(int starting, int expected)
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<Item.Factory>();

            var item = factory(starting, null);
            item.Remove();

            Assert.Equal(expected, item.Current);
        }

        [Fact]
        public void Remove_ExceptionTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<Item.Factory>();

            var item = factory(0, null);
            Assert.Throws<Exception>(() => { item.Remove(); });
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Reset_Tests(int starting, int expected)
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<Item.Factory>();

            var item = factory(starting, null);
            item.Add();
            item.Reset();

            Assert.Equal(expected, item.Current);
        }

        [Fact]
        public void PropertyChanged_Tests()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<Item.Factory>();

            var item = factory(0, null);

            Assert.PropertyChanged(item, nameof(IItem.Current), () => { item.Add(); });
        }
    }
}
