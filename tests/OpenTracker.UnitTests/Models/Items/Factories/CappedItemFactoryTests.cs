using System;
using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Factories;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items.Factories
{
    public class CappedItemFactoryTests
    {
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();
        private readonly IAutoTrackValue _autoTrackValue = Substitute.For<IAutoTrackValue>();
        
        private readonly CappedItemFactory _sut;
        
        private static readonly Dictionary<ItemType, (int starting, int maximum)> ExpectedValues = new();

        public CappedItemFactoryTests()
        {
            _sut = new CappedItemFactory((starting, maximum, autoTrackValue) => new CappedItem(
                _saveLoadManager, _ => Substitute.For<IAddItem>(),
                _ => Substitute.For<IRemoveItem>(), _ => Substitute.For<ICycleItem>(),
                starting, maximum, autoTrackValue));
        }

        private static void PopulateExpectedValues()
        {
            ExpectedValues.Clear();
            
            for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                var itemType = (ItemType)i;

                var starting = 0;
                int maximum;

                switch (itemType)
                {
                    case ItemType.Sword:
                        starting = 1;
                        maximum = 5;
                        break;
                    case ItemType.Shield:
                    case ItemType.BombosDungeons:
                    case ItemType.EtherDungeons:
                    case ItemType.QuakeDungeons:
                        maximum = 3;
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
                        maximum = 1;
                        break;
                    case ItemType.Mail:
                    case ItemType.Arrows:
                    case ItemType.Mushroom:
                    case ItemType.Gloves:
                        maximum = 2;
                        break;
                    case ItemType.SmallKey:
                        maximum = 29;
                        break;
                    case ItemType.Bottle:
                        maximum = 4;
                        break;
                    default:
                        continue;
                }
                
                ExpectedValues.Add(itemType, (starting, maximum));
            }
        }

        [Fact]
        public void GetItem_ShouldThrowException_WhenItemTypeNotSupported()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.GetItem(
                ItemType.ATUnlockedDoor, 0, _autoTrackValue));
        }
        
        [Theory]
        [MemberData(nameof(GetItem_MaximumShouldEqualExpectedData))]
        public void GetItem_MaximumShouldEqualExpected(int expected, ItemType type, int starting)
        {
            var item = (ICappedItem)_sut.GetItem(type, starting, _autoTrackValue);
            
            Assert.Equal(expected, item.Maximum);
        }
        
        public static IEnumerable<object[]> GetItem_MaximumShouldEqualExpectedData()
        {
            PopulateExpectedValues();
        
            var result = new List<object[]>();
        
            for (var i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                var itemType = (ItemType)i;

                if (!ExpectedValues.TryGetValue(itemType, out var expectedValues))
                {
                    continue;
                }
                
                var expected = expectedValues.maximum;
                var starting = expectedValues.starting;
                
                result.Add(new object[] { expected, itemType, starting });
            }
        
            return result;
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<ICappedItemFactory>();
            
            Assert.NotNull(sut as CappedItemFactory);
        }
    }
}