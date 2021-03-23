using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class KeyItemTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();

        private readonly IAddItem.Factory _addItemFactory = _ => Substitute.For<IAddItem>();
        private readonly IRemoveItem.Factory _removeItemFactory = _ => Substitute.For<IRemoveItem>();
        private readonly ICycleItem.Factory _cycleItemFactory = _ => Substitute.For<ICycleItem>();

        private readonly IItem _genericKey = Substitute.For<IItem>();
        
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
        public void Maximum_ShouldEqualNonKeyDropMaximum_WhenKeyDropShuffleEqualsFalse(
            int expected, int nonKeyDropMaximum, int keyDropMaximum)
        {
            _mode.KeyDropShuffle.Returns(false);
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                nonKeyDropMaximum, keyDropMaximum, null);
            
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
        public void Maximum_ShouldEqualKeyDropMaximum_WhenKeyDropShuffleEqualsTrue(
            int expected, int nonKeyDropMaximum, int keyDropMaximum)
        {
            _mode.KeyDropShuffle.Returns(true);
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                nonKeyDropMaximum, keyDropMaximum, null);
            
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
        public void EffectiveCurrent_ShouldEqualCurrent_WhenGenericKeysEqualsFalse(
            int expected, int current, int genericKeyCurrent)
        {
            _mode.GenericKeys.Returns(false);
            _genericKey.Current.Returns(genericKeyCurrent);
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                6, 6, null)
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
        public void EffectiveCurrent_ShouldEqualCurrentPlusGenericKeyCurrent_WhenGenericKeysEqualsTrue(
            int expected, int current, int genericKeyCurrent)
        {
            _mode.GenericKeys.Returns(true);
            _genericKey.Current.Returns(genericKeyCurrent);
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                6, 6, null)
            {
                Current = current
            };

            Assert.Equal(expected, sut.EffectiveCurrent);
        }

        [Fact]
        public void EffectiveCurrent_ShouldUpdate_WhenGenericKeysChanges()
        {
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                3, 3, null);
            _genericKey.Current.Returns(1);
            _mode.GenericKeys.Returns(true);
            
            Assert.PropertyChanged(sut, nameof(IKeyItem.EffectiveCurrent), () =>
                _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _mode, new PropertyChangedEventArgs(nameof(IMode.GenericKeys))));
        }

        [Fact]
        public void EffectiveCurrent_ShouldUpdate_WhenGenericKeyCurrentChanged()
        {
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                3, 3, null);
            _genericKey.Current.Returns(1);
            _mode.GenericKeys.Returns(true);
            
            Assert.PropertyChanged(sut, nameof(IKeyItem.EffectiveCurrent), () =>
                _genericKey.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _genericKey, new PropertyChangedEventArgs(nameof(IItem.Current))));
        }

        [Fact]
        public void Current_ShouldSetCurrentToMaximum_WhenCurrentIsGreaterThanMaximum()
        {
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                3, 2, null);

            for (var i = 0; i < 3; i++)
            {
                sut.Add();
            }

            _mode.KeyDropShuffle.Returns(true);
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.KeyDropShuffle)));
            
            Assert.Equal(2, sut.Current);
        }

        [Fact]
        public void PropertyChanged_ShouldRaise_WhenKeyDropShuffleChanges()
        {
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                3, 3, null);
            
            Assert.PropertyChanged(sut, nameof(ICappedItem.Maximum), () =>
                _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _mode, new PropertyChangedEventArgs(nameof(IMode.KeyDropShuffle))));
        }

        [Theory]
        [InlineData(false, 0, 0, 0, false)]
        [InlineData(false, 0, 0, 1, false)]
        [InlineData(false, 0, 0, 2, false)]
        [InlineData(true, 0, 1, 0, false)]
        [InlineData(false, 1, 1, 0, false)]
        [InlineData(true, 0, 1, 1, false)]
        [InlineData(false, 1, 1, 1, false)]
        [InlineData(true, 0, 1, 2, false)]
        [InlineData(false, 1, 1, 2, false)]
        [InlineData(true, 0, 2, 0, false)]
        [InlineData(true, 1, 2, 0, false)]
        [InlineData(false, 2, 2, 0, false)]
        [InlineData(true, 0, 2, 1, false)]
        [InlineData(true, 1, 2, 1, false)]
        [InlineData(false, 2, 2, 1, false)]
        [InlineData(true, 0, 2, 2, false)]
        [InlineData(true, 1, 2, 2, false)]
        [InlineData(false, 2, 2, 2, false)]
        [InlineData(false, 0, 0, 0, true)]
        [InlineData(true, 0, 0, 1, true)]
        [InlineData(false, 1, 0, 1, true)]
        [InlineData(true, 0, 0, 2, true)]
        [InlineData(true, 1, 0, 2, true)]
        [InlineData(false, 2, 0, 2, true)]
        [InlineData(false, 0, 1, 0, true)]
        [InlineData(true, 0, 1, 1, true)]
        [InlineData(false, 1, 1, 1, true)]
        [InlineData(true, 0, 1, 2, true)]
        [InlineData(true, 1, 1, 2, true)]
        [InlineData(false, 2, 1, 2, true)]
        [InlineData(false, 0, 2, 0, true)]
        [InlineData(true, 0, 2, 1, true)]
        [InlineData(false, 1, 2, 1, true)]
        [InlineData(true, 0, 2, 2, true)]
        [InlineData(true, 1, 2, 2, true)]
        [InlineData(false, 2, 2, 2, true)]
        public void CanAdd_ShouldReturnTrue_WhenCurrentIsLessThanMaximum(
            bool expected, int current, int nonKeyDropMaximum, int keyDropMaximum, bool keyDropShuffle)
        {
            _mode.KeyDropShuffle.Returns(keyDropShuffle);
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                nonKeyDropMaximum, keyDropMaximum, null);

            for (var i = 0; i < current; i++)
            {
                sut.Add();
            }
            
            Assert.Equal(expected, sut.CanAdd());
        }

        [Fact]
        public void Add_ShouldThrowException_WhenCurrentIsEqualToMaximum()
        {
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                0, 0, null);

            Assert.Throws<Exception>(() => sut.Add());
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Add_ShouldAdd1ToCurrent_WhenCurrentIsLessThanMaximum(int expected, int starting)
        {
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                3, 3, null);

            for (var i = 0; i <= starting; i++)
            {
                sut.Add();
            }

            Assert.Equal(expected, sut.Current);
        }

        [Fact]
        public void Remove_ShouldThrowException_WhenCurrentEquals0()
        {
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                1, 1, null);

            Assert.Throws<Exception>(() => sut.Remove());
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public void Remove_ShouldSubtract1FromCurrent_WhenCurrentIsGreaterThan0(int expected, int starting)
        {
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                3, 3, null);

            for (var i = 0; i < starting; i++)
            {
                sut.Add();
            }
            
            sut.Remove();
            
            Assert.Equal(expected, sut.Current);
        }

        [Fact]
        public void CreateCycleItemAction_ShouldReturnNewCycleItemAction()
        {
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                0, 0, null);
            var cycleItem = sut.CreateCycleItemAction();
            
            Assert.NotNull(cycleItem);
        }

        [Theory]
        [InlineData(0, 0, 0, false)]
        [InlineData(1, 0, 1, false)]
        [InlineData(0, 1, 1, false)]
        [InlineData(1, 0, 2, false)]
        [InlineData(2, 1, 2, false)]
        [InlineData(0, 2, 2, false)]
        [InlineData(1, 0, 3, false)]
        [InlineData(2, 1, 3, false)]
        [InlineData(3, 2, 3, false)]
        [InlineData(0, 3, 3, false)]
        [InlineData(0, 0, 0, true)]
        [InlineData(1, 0, 1, true)]
        [InlineData(0, 1, 1, true)]
        [InlineData(2, 0, 2, true)]
        [InlineData(0, 1, 2, true)]
        [InlineData(1, 2, 2, true)]
        [InlineData(3, 0, 3, true)]
        [InlineData(0, 1, 3, true)]
        [InlineData(1, 2, 3, true)]
        [InlineData(2, 3, 3, true)]
        public void Cycle_ShouldSetCurrent(int expected, int current, int maximum, bool reverse)
        {
            var sut = new KeyItem(
                _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, _genericKey,
                maximum, maximum, null);

            for (var i = 0; i < current; i++)
            {
                sut.Add();
            }
            
            sut.Cycle(reverse);
            
            Assert.Equal(expected, sut.Current);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IKeyItem.Factory>();
            var sut = factory(_genericKey, 0, 0, null);
            
            Assert.NotNull(sut as KeyItem);
        }
    }
}