using OpenTracker.Models.Items;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OpenTracker.UnitTests.Items
{
    public class ItemTests
    {
        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 0)]
        [InlineData(1, 2, 1)]
        [InlineData(2, 2, 2)]
        public void Ctor_CurrentTests(int starting, int maximum, int expected)
        {
            if (starting > maximum)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    new Item(ItemType.Sword, starting, maximum);
                });
            }
            else
            {
                var item = new Item(ItemType.Sword, starting, maximum);
                int actual = item.Current;
                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData(0, 1, 1, false, 1)]
        [InlineData(0, 1, 2, false, 1)]
        [InlineData(0, 1, 2, true, 2)]
        [InlineData(4, 5, 1, false, 5)]
        [InlineData(4, 5, 2, false, 5)]
        [InlineData(4, 5, 2, true, 6)]
        public void Change_Tests(
            int starting, int maximum, int delta, bool ignoreMaximum, int expected)
        {
            var item = new Item(ItemType.Sword, starting, maximum);
            item.Change(delta, ignoreMaximum);
            int actual = item.Current;
            Assert.Equal(expected, actual);
        }
    }
}
