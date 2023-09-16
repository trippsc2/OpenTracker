using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items.Keys;

public class BigKeyItemTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();
        
    private readonly IAddItem.Factory _addItemFactory = _ => Substitute.For<IAddItem>();
    private readonly IRemoveItem.Factory _removeItemFactory = _ => Substitute.For<IRemoveItem>();
    private readonly ICycleItem.Factory _cycleItemFactory = _ => Substitute.For<ICycleItem>();
        
    private readonly IAutoTrackValue _autoTrackValue = Substitute.For<IAutoTrackValue>();
        
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
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, nonKeyDropMaximum,
            keyDropMaximum, _autoTrackValue);
            
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
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, nonKeyDropMaximum,
            keyDropMaximum, _autoTrackValue);
            
        Assert.Equal(expected, sut.Maximum);
    }

    [Fact]
    public void Current_ShouldSetCurrentToMaximum_WhenCurrentIsGreaterThanMaximum()
    {
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 3,
            2, _autoTrackValue);
        
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
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 3,
            3, _autoTrackValue);
            
        Assert.PropertyChanged(sut, nameof(ICappedItem.Maximum), () =>
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.KeyDropShuffle))));
    }

    [Fact]
    public void ModeChanged_ShouldDoNothing_WhenKeyDropShuffleDidNotChange()
    {
        var maximumPropertyChangedRaised = 0;
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 1,
            1, _autoTrackValue);
        sut.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(IBigKeyItem.Maximum))
            {
                maximumPropertyChangedRaised++;
            }
        };
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.ItemPlacement)));
            
        Assert.Equal(0, maximumPropertyChangedRaised);
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
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, nonKeyDropMaximum,
            keyDropMaximum, _autoTrackValue);
        
        for (var i = 0; i < current; i++)
        {
            sut.Add();
        }
            
        Assert.Equal(expected, sut.CanAdd());
    }
        
    [Fact]
    public void Add_ShouldThrowException_WhenCurrentIsEqualToMaximum()
    {
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 0,
            0, null);
        
        Assert.Throws<Exception>(() => sut.Add());
    }
        
    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    [InlineData(3, 2)]
    public void Add_ShouldAdd1ToCurrent_WhenCurrentIsLessThanMaximum(int expected, int starting)
    {
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 3,
            3, _autoTrackValue);
        
        for (var i = 0; i <= starting; i++)
        {
            sut.Add();
        }
        
        Assert.Equal(expected, sut.Current);
    }
        
    [Fact]
    public void Remove_ShouldThrowException_WhenCurrentEquals0()
    {
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 1,
            1, _autoTrackValue);
        
        Assert.Throws<Exception>(() => sut.Remove());
    }
        
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public void Remove_ShouldSubtract1FromCurrent_WhenCurrentIsGreaterThan0(int expected, int starting)
    {
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 3,
            3, _autoTrackValue);
        
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
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 0,
            0, _autoTrackValue);
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
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 
            maximum, maximum, _autoTrackValue);
        
        for (var i = 0; i < current; i++)
        {
            sut.Add();
        }
            
        sut.Cycle(reverse);
            
        Assert.Equal(expected, sut.Current);
    }

    [Fact]
    public void GetKeyValues_ShouldReturnListContainingOnlyFalse_WhenMaximumIs0()
    {
        _mode.KeyDropShuffle.Returns(false);
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 0,
            1, _autoTrackValue);
        var keyValues = sut.GetKeyValues();

        Assert.Single(keyValues);
        Assert.Contains(false, keyValues);
    }

    [Theory]
    [InlineData(false, 0)]
    [InlineData(true, 1)]
    public void GetKeyValues_ShouldReturnCurrentOnly_WhenBigKeyShuffleIsTrue(bool expected, int current)
    {
        _mode.BigKeyShuffle.Returns(true);
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 1,
            1, _autoTrackValue);

        for (var i = 0; i < current; i++)
        {
            sut.Add();
        }

        var keyValues = sut.GetKeyValues();

        Assert.Single(keyValues);
        Assert.Contains(expected, keyValues);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void
        GetKeyValues_ShouldReturnListContainingTrueAndFalse_WhenMaximumIsGreaterThan0AndBigKeyShuffleIsFalse(
            int current)
    {
        _mode.BigKeyShuffle.Returns(false);
        var sut = new BigKeyItem(
            _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 1,
            1, _autoTrackValue);

        for (var i = 0; i < current; i++)
        {
            sut.Add();
        }

        var keyValues = sut.GetKeyValues();

        Assert.Equal(2, keyValues.Count);
        Assert.Contains(false, keyValues);
        Assert.Contains(true, keyValues);
    }
        
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IBigKeyItem.Factory>();
        var sut = factory(0, 0, _autoTrackValue);
            
        Assert.NotNull(sut as BigKeyItem);
    }
}