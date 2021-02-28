using System;
using OpenTracker.Models.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    [Collection("Tests")]
    public class CappedItemTests
    {
        [Theory]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Ctor_ExceptionTests(int starting, int maximum)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var item = new CappedItem(starting, maximum, null);
            });
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(0, 1, true)]
        [InlineData(1, 1, false)]
        [InlineData(1, 2, true)]
        public void CanAdd_Tests(int starting, int maximum, bool expected)
        {
            var item = new CappedItem(starting, maximum, null);

            Assert.Equal(expected, item.CanAdd());
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 2, 2)]
        public void Add_Tests(int starting, int maximum, int expected)
        {
            var item = new CappedItem(starting, maximum, null);
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
            var item = new CappedItem(starting, maximum, null);
            item.Remove();

            Assert.Equal(expected, item.Current);
        }
    }
}
