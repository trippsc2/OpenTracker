using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items;

public class ItemTests
{
    private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();

    private readonly IAddItem.Factory _addItemFactory = _ => Substitute.For<IAddItem>();
    private readonly IRemoveItem.Factory _removeItemFactory = _ => Substitute.For<IRemoveItem>();

    private readonly IAutoTrackValue _autoTrackValue = Substitute.For<IAutoTrackValue>();
        
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void Ctor_ShouldSetCurrent(int expected, int starting)
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, starting, _autoTrackValue);

        Assert.Equal(expected, sut.Current);
    }

    [Fact]
    public void PropertyChanged_ShouldRaise_WhenCurrentIsChanged()
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, 0, _autoTrackValue);

        Assert.PropertyChanged(sut, nameof(IItem.Current), () => { sut.Add(); });
    }

    [Theory]
    [InlineData(3, null)]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    public void AutoTrackUpdate_ShouldSetCurrentToAutoTrackCurrentValue(int expected, int? autoTrackValue)
    {
        var sut = new Item(
            _saveLoadManager, _addItemFactory, _removeItemFactory, 3, _autoTrackValue);

        _autoTrackValue.CurrentValue.Returns(autoTrackValue);
        _autoTrackValue.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _autoTrackValue, new PropertyChangedEventArgs(nameof(IAutoTrackValue.CurrentValue)));
            
        Assert.Equal(expected, sut.Current);
    }

    [Fact]
    public void CreateAddItemAction_ShouldReturnNewAddItem()
    {
        var sut = new Item(
            _saveLoadManager, _addItemFactory, _removeItemFactory, 0, null);
        var addItem = sut.CreateAddItemAction();
            
        Assert.NotNull(addItem);
    }

    [Fact]
    public void CanAdd_ShouldAlwaysReturnTrue()
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, 0, _autoTrackValue);

        Assert.True(sut.CanAdd());
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    [InlineData(3, 2)]
    public void Add_ShouldAddOneToCurrent(int expected, int starting)
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, starting, _autoTrackValue);
        sut.Add();

        Assert.Equal(expected, sut.Current);
    }

    [Fact]
    public void CreateRemoveItemAction_ShouldReturnNewRemoveItem()
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, 0, _autoTrackValue);
        var removeItem = sut.CreateRemoveItemAction();
            
        Assert.NotNull(removeItem);
    }

    [Theory]
    [InlineData(false, 0)]
    [InlineData(true, 1)]
    [InlineData(true, 2)]
    public void CanRemove_ShouldReturnTrue_WhenCurrentGreaterThanZero(bool expected, int starting)
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, starting, _autoTrackValue);

        Assert.Equal(expected, sut.CanRemove());
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public void Remove_ShouldSubtractOneFromCurrent_WhenCurrentIsGreaterThan0(int expected, int starting)
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, starting, _autoTrackValue);
        sut.Remove();

        Assert.Equal(expected, sut.Current);
    }

    [Fact]
    public void Remove_ShouldThrowException_WhenCurrentEquals0()
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, 0,
            null);
        Assert.Throws<Exception>(() => { sut.Remove(); });
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void Reset_ShouldSetCurrentToStarting(int expected, int starting)
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, starting, null);
        sut.Add();
        sut.Reset();

        Assert.Equal(expected, sut.Current);
    }

    [Fact]
    public void Save_ShouldReturnItemSaveData()
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, 0, _autoTrackValue);
        var saveData = sut.Save();
            
        Assert.Equal(typeof(ItemSaveData), saveData.GetType());
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void Save_ShouldSetCurrentToSaveDataValue(int expected, int current)
    {
        var sut = new Item(
            _saveLoadManager, _addItemFactory, _removeItemFactory, 0, null)
        {
            Current = current
        };
        var saveData = sut.Save();
            
        Assert.Equal(expected, saveData.Current);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void Load_SetCurrentToSaveDataValue(int expected, int current)
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, 3, _autoTrackValue);
        var saveData = new ItemSaveData
        {
            Current = current
        };
        sut.Load(saveData);
            
        Assert.Equal(expected, sut.Current);
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        var sut = new Item(_saveLoadManager, _addItemFactory, _removeItemFactory, 0, _autoTrackValue);
        ItemSaveData? saveData = null;
        sut.Load(saveData);
            
        Assert.Equal(0, sut.Current);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IItem.Factory>();
        var sut = factory(0, null);
            
        Assert.NotNull(sut as Item);
    }
}