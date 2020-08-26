using OpenTracker.Models.Items;
using System;
using Xunit;

namespace OpenTracker.UnitTests.Items
{
    public class CappedItemTests
    {
        [Theory]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Ctor_ExceptionTests(int starting, int maximum)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var item = new CappedItem(starting, maximum);
            });
        }
    }
}
