using System;
using System.Collections.Generic;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class ItemFactoryTests
    {
        private readonly IItemAutoTrackValueFactory _autoTrackValueFactory =
            Substitute.For<IItemAutoTrackValueFactory>();
        private readonly IItemDictionary _items = Substitute.For<IItemDictionary>();
        private readonly IMode _mode = Substitute.For<IMode>();
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();

        private readonly IAddItem.Factory _addItemFactory = item => Substitute.For<IAddItem>();
        private readonly IRemoveItem.Factory _removeItemFactory = item => Substitute.For<IRemoveItem>();
        private readonly ICycleItem.Factory _cycleItemFactory = item => Substitute.For<ICycleItem>();
        
        private readonly ItemFactory _sut;
        
        private static readonly Dictionary<
            ItemType, (Type type, int starting, int? maximum, int? keyDropMaximum)> ExpectedValues =
                new Dictionary<ItemType, (Type type, int starting, int? maximum, int? keyDropMaximum)>();

        public ItemFactoryTests()
        {
            _autoTrackValueFactory.GetAutoTrackValue(ItemType.Arrows).ReturnsNullForAnyArgs();
            _sut = new ItemFactory(
                () => _items, () => _autoTrackValueFactory,
                (starting, autoTrackValue) => new Item(
                    _saveLoadManager, _addItemFactory, _removeItemFactory, starting, autoTrackValue),
                (starting, maximum, autoTrackValue) => new CappedItem(
                    _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, starting, maximum,
                    autoTrackValue),
                () => new CrystalRequirementItem(
                    _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory),
                (genericKey, nonKeyDropMaximum, keyDropMaximum, autoTrackValue) => new KeyItem(
                    _mode, _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, genericKey,
                    nonKeyDropMaximum, keyDropMaximum, autoTrackValue));
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
                    {
                        type = typeof(CappedItem);
                        starting = 1;
                        maximum = 5;
                    }
                        break;
                    case ItemType.Shield:
                    case ItemType.BombosDungeons:
                    case ItemType.EtherDungeons:
                    case ItemType.QuakeDungeons:
                    {
                        type = typeof(CappedItem);
                        maximum = 3;
                    }
                        break;
                    case ItemType.TowerCrystals:
                    case ItemType.GanonCrystals:
                    {
                        type = typeof(CrystalRequirementItem);
                        maximum = 7;
                    }
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
                    {
                        type = typeof(CappedItem);
                        maximum = 1;
                    }
                        break;
                    case ItemType.Mail:
                    case ItemType.Arrows:
                    case ItemType.Mushroom:
                    case ItemType.Gloves:
                    {
                        type = typeof(CappedItem);
                        maximum = 2;
                    }
                        break;
                    case ItemType.SmallKey:
                    {
                        type = typeof(CappedItem);
                        maximum = 29;
                    }
                        break;
                    case ItemType.Bottle:
                    {
                        type = typeof(CappedItem);
                        maximum = 4;
                    }
                        break;
                    case ItemType.HCSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 1;
                        keyDropMaximum = 4;
                    }
                        break;
                    case ItemType.EPSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 0;
                        keyDropMaximum = 2;
                    }
                        break;
                    case ItemType.DPSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 1;
                        keyDropMaximum = 4;
                    }
                        break;
                    case ItemType.ToHSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 1;
                        keyDropMaximum = 1;
                    }
                        break;
                    case ItemType.ATSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 2;
                        keyDropMaximum = 4;
                    }
                        break;
                    case ItemType.PoDSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 6;
                        keyDropMaximum = 6;
                    }
                        break;
                    case ItemType.SPSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 1;
                        keyDropMaximum = 6;
                    }
                        break;
                    case ItemType.SWSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 3;
                        keyDropMaximum = 5;
                    }
                        break;
                    case ItemType.TTSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 1;
                        keyDropMaximum = 3;
                    }
                        break;
                    case ItemType.IPSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 2;
                        keyDropMaximum = 6;
                    }
                        break;
                    case ItemType.MMSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 3;
                        keyDropMaximum = 6;
                    }
                        break;
                    case ItemType.TRSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 4;
                        keyDropMaximum = 6;
                    }
                        break;
                    case ItemType.GTSmallKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 4;
                        keyDropMaximum = 8;
                    }
                        break;
                    case ItemType.HCBigKey:
                    {
                        type = typeof(KeyItem);
                        maximum = 0;
                        keyDropMaximum = 1;
                    }
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
                var expected = ExpectedValues[itemType].starting;
                
                result.Add(new object[] { expected, itemType });
            }

            return result;
        }

        [Theory]
        [MemberData(nameof(GetItem_MaximumShouldEqualExpectedData))]
        public void GetItem_MaximumShouldEqualExpected(int expected, ItemType type)
        {
            var item = (ICappedItem)_sut.GetItem(type);
            
            Assert.Equal(expected, item.Maximum);
        }
        
        public static IEnumerable<object[]> GetItem_MaximumShouldEqualExpectedData()
        {
            PopulateExpectedValues();

            var result = new List<object[]>();

            for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                var itemType = (ItemType)i;
                var expected = ExpectedValues[itemType].maximum;

                if (expected is null)
                {
                    continue;
                }
                
                result.Add(new object[] { expected.Value, itemType });
            }

            return result;
        }

        [Theory]
        [MemberData(nameof(GetItem_KeyDropMaximumShouldEqualExpectedData))]
        public void GetItem_KeyDropMaximumShouldEqualExpected(int expected, ItemType type)
        {
            _mode.KeyDropShuffle.Returns(true);
            var item = (ICappedItem)_sut.GetItem(type);
            
            Assert.Equal(expected, item.Maximum);
        }
        
        public static IEnumerable<object[]> GetItem_KeyDropMaximumShouldEqualExpectedData()
        {
            PopulateExpectedValues();

            var result = new List<object[]>();

            for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                var itemType = (ItemType)i;
                var expected = ExpectedValues[itemType].keyDropMaximum;

                if (expected is null)
                {
                    continue;
                }
                
                result.Add(new object[] { expected.Value, itemType });
            }

            return result;
        }
    }
}