using System;
using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values.Multiple;
using OpenTracker.Models.AutoTracking.Values.Single;
using OpenTracker.Models.AutoTracking.Values.Static;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Factories;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items.Factories
{
    public class ItemAutoTrackValueFactoryTests
    {
        private readonly IItemDictionary _items = Substitute.For<IItemDictionary>();
        private readonly IMemoryAddressProvider _memoryAddressProvider = new MemoryAddressProvider(() =>
            Substitute.For<IMemoryAddress>());
        private readonly IRequirementDictionary _requirements = Substitute.For<IRequirementDictionary>();
        
        private readonly IAutoTrackAddressBool.Factory _boolFactory = (address, comparison, trueValue) => 
            new AutoTrackAddressBool(address, comparison, trueValue);
        private readonly IAutoTrackAddressValue.Factory _valueFactory = (address, maximum, adjustment) => 
            new AutoTrackAddressValue(address, maximum, adjustment);
        private readonly IAutoTrackConditionalValue.Factory _conditionalFactory = (condition, trueValue, falseValue) => 
            new AutoTrackConditionalValue(condition, trueValue, falseValue);
        private readonly IAutoTrackFlagBool.Factory _flagBoolFactory = (flag, trueValue) => 
            new AutoTrackFlagBool(flag, trueValue);
        private readonly IAutoTrackItemValue.Factory _itemValueFactory = item => new AutoTrackItemValue(item);
        private readonly IAutoTrackMultipleDifference.Factory _differenceFactory = (value1, value2) => 
            new AutoTrackMultipleDifference(value1, value2);
        private readonly IAutoTrackMultipleOverride.Factory _overrideFactory = values => 
            new AutoTrackMultipleOverride(values);
        private readonly IAutoTrackMultipleSum.Factory _sumFactory = values => 
            new AutoTrackMultipleSum(values);
        private readonly IAutoTrackStaticValue.Factory _staticFactory = value => new AutoTrackStaticValue(value);
        private readonly IMemoryFlag.Factory _memoryFlagFactory = (_, _) => Substitute.For<IMemoryFlag>();

        private readonly ItemAutoTrackValueFactory _sut;

        public ItemAutoTrackValueFactoryTests()
        {
            _items[Arg.Any<ItemType>()].Returns(Substitute.For<IItem>());
            _requirements[Arg.Any<RequirementType>()].Returns(Substitute.For<IRequirement>());
            
            _sut = new ItemAutoTrackValueFactory(
                _items, _memoryAddressProvider, _requirements, _boolFactory, _valueFactory, _conditionalFactory,
                _flagBoolFactory, _itemValueFactory, _differenceFactory, _overrideFactory, _sumFactory, _staticFactory,
                _memoryFlagFactory);
        }

        [Theory]
        [MemberData(nameof(GetAutoTrackValue_ShouldReturnExpectedTypeData))]
        public void GetAutoTrackValue_ShouldReturnExpectedType(Type? expected, ItemType itemType)
        {
            var autoTrackValue = _sut.GetAutoTrackValue(itemType);

            if (expected is null)
            {
                Assert.Null(autoTrackValue);
                return;
            }
            
            Assert.Equal(expected, autoTrackValue!.GetType());
        }

        public static IEnumerable<object?[]> GetAutoTrackValue_ShouldReturnExpectedTypeData()
        {
            var expectedValues = new List<object?[]>();

            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                Type? type = null;

                switch (itemType)
                {
                    case ItemType.Sword:
                    case ItemType.Shield:
                    case ItemType.Mail:
                    case ItemType.SmallKey:
                    case ItemType.Gloves:
                        type = typeof(AutoTrackAddressValue);
                        break;
                    case ItemType.Bow:
                    case ItemType.Hookshot:
                    case ItemType.Bomb:
                    case ItemType.Boots:
                    case ItemType.FireRod:
                    case ItemType.IceRod:
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                    case ItemType.Lamp:
                    case ItemType.Hammer:
                    case ItemType.Net:
                    case ItemType.Book:
                    case ItemType.Flippers:
                    case ItemType.CaneOfSomaria:
                    case ItemType.CaneOfByrna:
                    case ItemType.Cape:
                    case ItemType.Mirror:
                    case ItemType.HalfMagic:
                    case ItemType.MoonPearl:
                        type = typeof(AutoTrackAddressBool);
                        break;
                    case ItemType.Arrows:
                    case ItemType.BigBomb:
                    case ItemType.Mushroom:
                    case ItemType.Flute:
                    case ItemType.HCBigKey:
                    case ItemType.HCMap:
                        type = typeof(AutoTrackMultipleOverride);
                        break;
                    case ItemType.Boomerang:
                    case ItemType.RedBoomerang:
                    case ItemType.Powder:
                    case ItemType.MagicBat:
                    case ItemType.FluteActivated:
                    case ItemType.Shovel:
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
                    case ItemType.ToHUnlockedDoor:
                        type = typeof(AutoTrackFlagBool);
                        break;
                    case ItemType.Bottle:
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
                    case ItemType.PoDUnlockedDoor:
                    case ItemType.SPUnlockedDoor:
                    case ItemType.SWUnlockedDoor:
                    case ItemType.TTUnlockedDoor:
                    case ItemType.IPUnlockedDoor:
                    case ItemType.MMUnlockedDoor:
                    case ItemType.TRUnlockedDoor:
                    case ItemType.GTUnlockedDoor:
                        type = typeof(AutoTrackMultipleSum);
                        break;
                    case ItemType.HCSmallKey:
                    case ItemType.EPSmallKey:
                    case ItemType.DPSmallKey:
                    case ItemType.ToHSmallKey:
                    case ItemType.ATSmallKey:
                    case ItemType.PoDSmallKey:
                    case ItemType.SPSmallKey:
                    case ItemType.SWSmallKey:
                    case ItemType.TTSmallKey:
                    case ItemType.IPSmallKey:
                    case ItemType.MMSmallKey:
                    case ItemType.TRSmallKey:
                    case ItemType.GTSmallKey:
                        type = typeof(AutoTrackConditionalValue);
                        break;
                }
                
                expectedValues.Add(new object?[] {type, itemType} );
            }

            return expectedValues;
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IItemAutoTrackValueFactory.Factory>();
            var sut = factory();
            
            Assert.NotNull(sut as ItemAutoTrackValueFactory);
        }
    }
}