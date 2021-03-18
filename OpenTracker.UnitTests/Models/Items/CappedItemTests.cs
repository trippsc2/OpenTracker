using System;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class CappedItemTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_MaximumShouldEqualParameter(int expected, int maximum)
        {
            var sut = new CappedItem(
                Substitute.For<ISaveLoadManager>(), 0, maximum, null);
            
            Assert.Equal(expected, sut.Maximum);
        }
        
        [Theory]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Ctor_ShouldThrowExceptionIfStartingIsGreaterThanMaximum(int starting, int maximum)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new CappedItem(
                    Substitute.For<ISaveLoadManager>(), starting, maximum, null);
            });
        }

        [Theory]
        [InlineData(false, 0, 0)]
        [InlineData(true, 0, 1)]
        [InlineData(false, 1, 1)]
        [InlineData(true, 1, 2)]
        public void CanAdd_ShouldReturnTrueIfCurrentIsLessThanMaximum(bool expected, int starting, int maximum)
        {
            var sut = new CappedItem(
                Substitute.For<ISaveLoadManager>(), starting, maximum, null);

            Assert.Equal(expected, sut.CanAdd());
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(2, 1, 2)]
        public void Add_ShouldAddOneToCurrentIfLessThanMaximumOtherwiseSetToZero(
            int expected, int starting, int maximum)
        {
            var sut = new CappedItem(
                Substitute.For<ISaveLoadManager>(), starting, maximum, null);
            sut.Add();

            Assert.Equal(expected, sut.Current);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(0, 1, 2)]
        public void Remove_ShouldSubtractOneFromCurrentIfGreaterThanZeroOtherwiseSetToMaximum(
            int expected, int starting, int maximum)
        {
            var sut = new CappedItem(
                Substitute.For<ISaveLoadManager>(), starting, maximum, null);
            sut.Remove();

            Assert.Equal(expected, sut.Current);
        }
    }
}
