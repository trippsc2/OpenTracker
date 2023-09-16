using System;
using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Factories;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items.Factories;

public class KeyItemFactoryItems
{
    private readonly IItemDictionary _items = Substitute.For<IItemDictionary>();
    private readonly IMode _mode = Substitute.For<IMode>();
    private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();

    private readonly IAutoTrackValue _autoTrackValue = Substitute.For<IAutoTrackValue>();
    private readonly IItem _genericKey = Substitute.For<IItem>();
        
    private readonly KeyItemFactory _sut;
        
    private static readonly Dictionary<
        ItemType, (Type type, int nonKeyDropMaximum, int keyDropMaximum)> ExpectedValues = new();

    public KeyItemFactoryItems()
    {
        _items[ItemType.SmallKey].Returns(_genericKey);
            
        _sut = new KeyItemFactory(_items,
            (genericKey, nonKeyDropMaximum, keyDropMaximum, autoTrackValue) =>
                new SmallKeyItem(_mode, _saveLoadManager, _ => Substitute.For<IAddItem>(),
                    _ => Substitute.For<IRemoveItem>(),
                    _ => Substitute.For<ICycleItem>(), genericKey, nonKeyDropMaximum, keyDropMaximum,
                    autoTrackValue), (nonKeyDropMaximum, keyDropMaximum, autoTrackValue) => 
                new BigKeyItem(_mode, _saveLoadManager, _ => Substitute.For<IAddItem>(),
                    _ => Substitute.For<IRemoveItem>(),
                    _ => Substitute.For<ICycleItem>(), nonKeyDropMaximum, keyDropMaximum,
                    autoTrackValue));
    }
        
    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();
            
        for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
        {
            var itemType = (ItemType) i;

            Type type;
            int nonKeyDropMaximum;
            int keyDropMaximum;

            switch (itemType)
            {
                case ItemType.HCSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 1;
                    keyDropMaximum = 4;
                    break;
                case ItemType.EPSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 0;
                    keyDropMaximum = 2;
                    break;
                case ItemType.DPSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 1;
                    keyDropMaximum = 4;
                    break;
                case ItemType.ToHSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 1;
                    keyDropMaximum = 1;
                    break;
                case ItemType.ATSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 2;
                    keyDropMaximum = 4;
                    break;
                case ItemType.PoDSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 6;
                    keyDropMaximum = 6;
                    break;
                case ItemType.SPSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 1;
                    keyDropMaximum = 6;
                    break;
                case ItemType.SWSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 3;
                    keyDropMaximum = 5;
                    break;
                case ItemType.TTSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 1;
                    keyDropMaximum = 3;
                    break;
                case ItemType.IPSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 2;
                    keyDropMaximum = 6;
                    break;
                case ItemType.MMSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 3;
                    keyDropMaximum = 6;
                    break;
                case ItemType.TRSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 4;
                    keyDropMaximum = 6;
                    break;
                case ItemType.GTSmallKey:
                    type = typeof(SmallKeyItem);
                    nonKeyDropMaximum = 4;
                    keyDropMaximum = 8;
                    break;
                case ItemType.HCBigKey:
                    type = typeof(BigKeyItem);
                    nonKeyDropMaximum = 0;
                    keyDropMaximum = 1;
                    break;
                case ItemType.EPBigKey:
                case ItemType.DPBigKey:
                case ItemType.ToHBigKey:
                case ItemType.PoDBigKey:
                case ItemType.SPBigKey:
                case ItemType.SWBigKey:
                case ItemType.TTBigKey:
                case ItemType.IPBigKey:
                case ItemType.MMBigKey:
                case ItemType.TRBigKey:
                case ItemType.GTBigKey:
                    type = typeof(BigKeyItem);
                    nonKeyDropMaximum = 1;
                    keyDropMaximum = 1;
                    break;
                default:
                    continue;
            }
                
            ExpectedValues.Add(itemType, (type, nonKeyDropMaximum, keyDropMaximum));
        }
    }
        
    [Fact]
    public void GetItem_ShouldThrowException_WhenTypeIsOutsideOfExpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.GetItem((ItemType) int.MaxValue, _autoTrackValue));
    }

    [Theory]
    [MemberData(nameof(GetItem_TypeShouldMatchExpectedData))]
    public void GetItem_TypeShouldMatchExpected(Type expected, ItemType type)
    {
        var item = _sut.GetItem(type, _autoTrackValue);
            
        Assert.Equal(expected, item.GetType());
    }

    public static IEnumerable<object[]> GetItem_TypeShouldMatchExpectedData()
    {
        PopulateExpectedValues();

        var result = new List<object[]>();

        for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
        {
            var itemType = (ItemType)i;
                
            if (!ExpectedValues.TryGetValue(itemType, out var expectedValue))
            {
                continue;
            }
                
            var expected = expectedValue.type;

            result.Add(new object[] { expected, itemType });
        }

        return result;
    }

    [Theory]
    [MemberData(nameof(GetItem_MaximumShouldReturnExpectedData))]
    public void GetItem_MaximumShouldReturnExpected(int expected, bool keyDropShuffle, ItemType type)
    {
        _mode.KeyDropShuffle.Returns(keyDropShuffle);
        var item = (ICappedItem)_sut.GetItem(type, _autoTrackValue);
            
        Assert.Equal(expected, item.Maximum);
    }

    public static IEnumerable<object[]> GetItem_MaximumShouldReturnExpectedData()
    {
        PopulateExpectedValues();

        var result = new List<object[]>();

        for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
        {
            var itemType = (ItemType)i;
                
            if (!ExpectedValues.TryGetValue(itemType, out var expectedValue))
            {
                continue;
            }
                
            result.Add(new object[] { expectedValue.nonKeyDropMaximum, false, itemType });
            result.Add(new object[] { expectedValue.keyDropMaximum, true, itemType });
        }

        return result;
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IKeyItemFactory>();
            
        Assert.NotNull(sut as KeyItemFactory);
    }
}