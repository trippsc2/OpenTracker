using OpenTracker.Models.Items;
using System;
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
    }
}
