using System;
using System.Collections.Generic;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Factories;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items.Factories
{
    public class ItemFactoryTests
    {
        private readonly IItemAutoTrackValueFactory _autoTrackValueFactory =
            Substitute.For<IItemAutoTrackValueFactory>();

        private readonly ICappedItemFactory _cappedItemFactory = Substitute.For<ICappedItemFactory>();
        private readonly IKeyItemFactory _keyItemFactory = Substitute.For<IKeyItemFactory>();

        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();

        private readonly IAddItem.Factory _addItemFactory = _ => Substitute.For<IAddItem>();
        private readonly IRemoveItem.Factory _removeItemFactory = _ => Substitute.For<IRemoveItem>();
        private readonly ICycleItem.Factory _cycleItemFactory = _ => Substitute.For<ICycleItem>();
        
        private readonly ItemFactory _sut;
        
        private static readonly Dictionary<
            ItemType, (Type type, int starting, int? maximum, int? keyDropMaximum)> ExpectedValues = new();

        public ItemFactoryTests()
        {
            _sut = new ItemFactory(
                () => _autoTrackValueFactory, _cappedItemFactory, _keyItemFactory,
                (starting, autoTrackValue) => new Item(
                    _saveLoadManager, _addItemFactory, _removeItemFactory, starting, autoTrackValue),
                () => new CrystalRequirementItem(
                    _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory));
        }

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();
            
            for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                var itemType = (ItemType) i;

                Type type = typeof(Item);
                var starting = 0;
                int? maximum = null;
                int? keyDropMaximum = null;

                switch (itemType)
                {
                    case ItemType.Sword:
                        type = typeof(CappedItem);
                        starting = 1;
                        maximum = 5;
                        break;
                    case ItemType.Shield:
                    case ItemType.BombosDungeons:
                    case ItemType.EtherDungeons:
                    case ItemType.QuakeDungeons:
                        type = typeof(CappedItem);
                        maximum = 3;
                        break;
                    case ItemType.TowerCrystals:
                    case ItemType.GanonCrystals:
                        type = typeof(CrystalRequirementItem);
                        maximum = 7;
                        break;
                    case ItemType.Bow:
                    case ItemType.Boomerang:
                    case ItemType.RedBoomerang:
                    case ItemType.Hookshot:
                    case ItemType.Bomb:
                    case ItemType.BigBomb:
                    case ItemType.Powder:
                    case ItemType.MagicBat:
                    case ItemType.Boots:
                    case ItemType.FireRod:
                    case ItemType.IceRod:
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                    case ItemType.Lamp:
                    case ItemType.Hammer:
                    case ItemType.Flute:
                    case ItemType.FluteActivated:
                    case ItemType.Net:
                    case ItemType.Book:
                    case ItemType.Shovel:
                    case ItemType.Flippers:
                    case ItemType.CaneOfSomaria:
                    case ItemType.CaneOfByrna:
                    case ItemType.Cape:
                    case ItemType.Mirror:
                    case ItemType.HalfMagic:
                    case ItemType.MoonPearl:
                    case ItemType.HCMap:
                    case ItemType.EPMap:
                    case ItemType.DPMap:
                    case ItemType.ToHMap:
                    case ItemType.PoDMap:
                    case ItemType.SPMap:
                    case ItemType.SWMap:
                    case ItemType.TTMap:
                    case ItemType.IPMap:
                    case ItemType.MMMap:
                    case ItemType.TRMap:
                    case ItemType.GTMap:
                    case ItemType.EPCompass:
                    case ItemType.DPCompass:
                    case ItemType.ToHCompass:
                    case ItemType.PoDCompass:
                    case ItemType.SPCompass:
                    case ItemType.SWCompass:
                    case ItemType.TTCompass:
                    case ItemType.IPCompass:
                    case ItemType.MMCompass:
                    case ItemType.TRCompass:
                    case ItemType.GTCompass:
                        type = typeof(CappedItem);
                        maximum = 1;
                        break;
                    case ItemType.Mail:
                    case ItemType.Arrows:
                    case ItemType.Mushroom:
                    case ItemType.Gloves:
                        type = typeof(CappedItem);
                        maximum = 2;
                        break;
                    case ItemType.SmallKey:
                        type = typeof(CappedItem);
                        maximum = 29;
                        break;
                    case ItemType.Bottle:
                        type = typeof(CappedItem);
                        maximum = 4;
                        break;
                    case ItemType.HCSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 1;
                        keyDropMaximum = 4;
                        break;
                    case ItemType.EPSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 0;
                        keyDropMaximum = 2;
                        break;
                    case ItemType.DPSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 1;
                        keyDropMaximum = 4;
                        break;
                    case ItemType.ToHSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 1;
                        keyDropMaximum = 1;
                        break;
                    case ItemType.ATSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 2;
                        keyDropMaximum = 4;
                        break;
                    case ItemType.PoDSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 6;
                        keyDropMaximum = 6;
                        break;
                    case ItemType.SPSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 1;
                        keyDropMaximum = 6;
                        break;
                    case ItemType.SWSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 3;
                        keyDropMaximum = 5;
                        break;
                    case ItemType.TTSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 1;
                        keyDropMaximum = 3;
                        break;
                    case ItemType.IPSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 2;
                        keyDropMaximum = 6;
                        break;
                    case ItemType.MMSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 3;
                        keyDropMaximum = 6;
                        break;
                    case ItemType.TRSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 4;
                        keyDropMaximum = 6;
                        break;
                    case ItemType.GTSmallKey:
                        type = typeof(SmallKeyItem);
                        maximum = 4;
                        keyDropMaximum = 8;
                        break;
                    case ItemType.HCBigKey:
                        type = typeof(BigKeyItem);
                        maximum = 0;
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
                        maximum = 1;
                        keyDropMaximum = 1;
                        break;
                    case ItemType.HCFreeKey:
                    case ItemType.ATFreeKey:
                    case ItemType.EPFreeKey:
                    case ItemType.DPFreeKey:
                    case ItemType.SPFreeKey:
                    case ItemType.SWFreeKey:
                    case ItemType.TTFreeKey:
                    case ItemType.IPFreeKey:
                    case ItemType.MMFreeKey:
                    case ItemType.TRFreeKey:
                    case ItemType.GTFreeKey:
                    case ItemType.HCUnlockedDoor:
                    case ItemType.ATUnlockedDoor:
                    case ItemType.EPUnlockedDoor:
                    case ItemType.DPUnlockedDoor:
                    case ItemType.ToHUnlockedDoor:
                    case ItemType.PoDUnlockedDoor:
                    case ItemType.SPUnlockedDoor:
                    case ItemType.SWUnlockedDoor:
                    case ItemType.TTUnlockedDoor:
                    case ItemType.IPUnlockedDoor:
                    case ItemType.MMUnlockedDoor:
                    case ItemType.TRUnlockedDoor:
                    case ItemType.GTUnlockedDoor:
                        type = typeof(Item);
                        break;
                }
                
                ExpectedValues.Add(itemType, (type, starting, maximum, keyDropMaximum));
            }
        }

        [Fact]
        public void GetItem_ShouldThrowException_WhenTypeIsOutsideOfExpected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.GetItem((ItemType) int.MaxValue));
        }
        
        [Theory]
        [MemberData(nameof(GetItem_ShouldCallGetItemOnKeyItemFactory_WhenKeyItemTypeData))]
        public void GetItem_ShouldCallGetItemOnKeyItemFactory_WhenKeyItemType(ItemType type)
        {
            _ = _sut.GetItem(type);

            _keyItemFactory.Received().GetItem(type, Arg.Any<IAutoTrackValue?>());
        }
        
        public static IEnumerable<object[]> GetItem_ShouldCallGetItemOnKeyItemFactory_WhenKeyItemTypeData()
        {
            PopulateExpectedValues();

            var result = new List<object[]>();

            for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                var itemType = (ItemType)i;
                var type = ExpectedValues[itemType].type;

                if (type != typeof(SmallKeyItem) && type != typeof(BigKeyItem))
                {
                    continue;
                }
                
                result.Add(new object[] { itemType });
            }

            return result;
        }

        [Theory]
        [MemberData(nameof(GetItem_TypeShouldMatchExpectedData))]
        public void GetItem_TypeShouldMatchExpected(Type expected, ItemType type)
        {
            var item = _sut.GetItem(type);
            
            Assert.Equal(expected, item.GetType());
        }

        public static IEnumerable<object[]> GetItem_TypeShouldMatchExpectedData()
        {
            PopulateExpectedValues();

            var result = new List<object[]>();

            for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                var itemType = (ItemType)i;
                var expected = ExpectedValues[itemType].type;

                if (expected == typeof(CappedItem) || expected == typeof(SmallKeyItem) ||
                    expected == typeof(BigKeyItem))
                {
                    continue;
                }
                
                result.Add(new object[] { expected, itemType });
            }

            return result;
        }
        
        [Theory]
        [MemberData(nameof(GetItem_StartingShouldEqualExpectedData))]
        public void GetItem_StartingShouldEqualExpected(int expected, ItemType type)
        {
            var item = _sut.GetItem(type);
            
            Assert.Equal(expected, item.Current);
        }
        
        public static IEnumerable<object[]> GetItem_StartingShouldEqualExpectedData()
        {
            PopulateExpectedValues();

            var result = new List<object[]>();

            for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                var itemType = (ItemType)i;
                var type = ExpectedValues[itemType].type;

                if (type == typeof(CappedItem) || type == typeof(SmallKeyItem) || type == typeof(BigKeyItem))
                {
                    continue;
                }

                var expected = ExpectedValues[itemType].starting;
                
                result.Add(new object[] { expected, itemType });
            }

            return result;
        }
        
        [Theory]
        [MemberData(nameof(GetItem_ShouldCallGetItemOnCappedItemFactory_WhenCappedItemTypeData))]
        public void GetItem_ShouldCallGetItemOnCappedItemFactory_WhenCappedItemType(int expected, ItemType type)
        {
            _ = _sut.GetItem(type);

            _cappedItemFactory.Received().GetItem(type, expected, Arg.Any<IAutoTrackValue?>());
        }
        
        public static IEnumerable<object[]> GetItem_ShouldCallGetItemOnCappedItemFactory_WhenCappedItemTypeData()
        {
            PopulateExpectedValues();

            var result = new List<object[]>();

            for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                var itemType = (ItemType)i;
                var type = ExpectedValues[itemType].type;

                if (type != typeof(CappedItem))
                {
                    continue;
                }

                var expected = ExpectedValues[itemType].starting;
                
                result.Add(new object[] { expected, itemType });
            }

            return result;
        }
    }
}