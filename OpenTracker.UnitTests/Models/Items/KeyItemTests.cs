using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class KeyItemTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 2, 0)]
        [InlineData(3, 3, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 1)]
        [InlineData(3, 3, 1)]
        [InlineData(0, 0, 2)]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 2)]
        [InlineData(3, 3, 2)]
        [InlineData(0, 0, 3)]
        [InlineData(1, 1, 3)]
        [InlineData(2, 2, 3)]
        [InlineData(3, 3, 3)]
        public void Maximum_ShouldEqualNonKeyDropMaximumWhenKeyDropShuffleEqualsFalse(
            int expected, int nonKeyDropMaximum, int keyDropMaximum)
        {
            var mode = new Mode();
            var sut = new KeyItem(
                Substitute.For<ISaveLoadManager>(), mode, Substitute.For<IItem>(),
                nonKeyDropMaximum, keyDropMaximum, null);
            mode.KeyDropShuffle = false;
            
            Assert.Equal(expected, sut.Maximum);
        }
        
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 0)]
        [InlineData(0, 2, 0)]
        [InlineData(0, 3, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 1)]
        [InlineData(1, 3, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(2, 1, 2)]
        [InlineData(2, 2, 2)]
        [InlineData(2, 3, 2)]
        [InlineData(3, 0, 3)]
        [InlineData(3, 1, 3)]
        [InlineData(3, 2, 3)]
        [InlineData(3, 3, 3)]
        public void Maximum_ShouldEqualKeyDropMaximumWhenKeyDropShuffleEqualsTrue(
            int expected, int nonKeyDropMaximum, int keyDropMaximum)
        {
            var mode = new Mode();
            var sut = new KeyItem(
                Substitute.For<ISaveLoadManager>(), mode, Substitute.For<IItem>(),
                nonKeyDropMaximum, keyDropMaximum, null);
            mode.KeyDropShuffle = true;
            
            Assert.Equal(expected, sut.Maximum);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 2, 0)]
        [InlineData(3, 3, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 1)]
        [InlineData(3, 3, 1)]
        [InlineData(0, 0, 2)]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 2)]
        [InlineData(3, 3, 2)]
        [InlineData(0, 0, 3)]
        [InlineData(1, 1, 3)]
        [InlineData(2, 2, 3)]
        [InlineData(3, 3, 3)]
        public void EffectiveCurrent_ShouldEqualCurrentIfGenericKeysEqualsFalse(
            int expected, int current, int genericKeyCurrent)
        {
            var mode = new Mode { GenericKeys = false };
            var genericKey = Substitute.For<IItem>();
            genericKey.Current.Returns(genericKeyCurrent);
            var sut = new KeyItem(
                Substitute.For<ISaveLoadManager>(), mode, genericKey, 6,
                6, null)
            {
                Current = current
            };

            Assert.Equal(expected, sut.EffectiveCurrent);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 2, 0)]
        [InlineData(3, 3, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(2, 1, 1)]
        [InlineData(3, 2, 1)]
        [InlineData(4, 3, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(3, 1, 2)]
        [InlineData(4, 2, 2)]
        [InlineData(5, 3, 2)]
        [InlineData(3, 0, 3)]
        [InlineData(4, 1, 3)]
        [InlineData(5, 2, 3)]
        [InlineData(6, 3, 3)]
        [InlineData(6, 4, 4)]
        public void EffectiveCurrent_ShouldEqualCurrentPlusGenericKeyCurrentIfGenericKeysEqualsTrue(
            int expected, int current, int genericKeyCurrent)
        {
            var mode = new Mode { GenericKeys = true };
            var genericKey = Substitute.For<IItem>();
            genericKey.Current.Returns(genericKeyCurrent);
            var sut = new KeyItem(
                Substitute.For<ISaveLoadManager>(), mode, genericKey, 6,
                6, null)
            {
                Current = current
            };

            Assert.Equal(expected, sut.EffectiveCurrent);
        }
    }
}