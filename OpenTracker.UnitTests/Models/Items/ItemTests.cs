using System;
using OpenTracker.Models.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    [Collection("Tests")]
    public class ItemTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_CurrentTests(int starting, int expected)
        {
            var item = new Item(starting, null);

            Assert.Equal(expected, item.Current);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        public void CanAdd_Tests(int starting, bool expected)
        {
            var item = new Item(starting, null);

            Assert.Equal(expected, item.CanAdd());
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public void Add_Tests(int starting, int expected)
        {
            var item = new Item(starting, null);
            item.Add();

            Assert.Equal(expected, item.Current);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        public void CanRemove_Tests(int starting, bool expected)
        {
            var item = new Item(starting, null);

            Assert.Equal(expected, item.CanRemove());
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Remove_Tests(int starting, int expected)
        {
            var item = new Item(starting, null);
            item.Remove();

            Assert.Equal(expected, item.Current);
        }

        [Fact]
        public void Remove_ExceptionTest()
        {
            var item = new Item(0, null);
            Assert.Throws<Exception>(() => { item.Remove(); });
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Reset_Tests(int starting, int expected)
        {
            var item = new Item(starting, null);
            item.Add();
            item.Reset();

            Assert.Equal(expected, item.Current);
        }

        [Fact]
        public void PropertyChanged_Tests()
        {
            var item = new Item(0, null);

            Assert.PropertyChanged(item, nameof(IItem.Current), () => { item.Add(); });
        }
    }
}
