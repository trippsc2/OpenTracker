using System.Collections.Generic;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Multiple;
using OpenTracker.Models.AutoTracking.Values.Single;
using OpenTracker.Models.AutoTracking.Values.Static;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Items.Factories
{
    /// <summary>
    ///     This class contains creation logic for item auto-track values.
    /// </summary>
    public class ItemAutoTrackValueFactory : IItemAutoTrackValueFactory
    {
        private readonly IItemDictionary _items;
        private readonly IMemoryAddressProvider _memoryAddressProvider;
        private readonly IRequirementDictionary _requirements;
        
        private readonly IAutoTrackAddressBool.Factory _boolFactory;
        private readonly IAutoTrackAddressValue.Factory _valueFactory;
        private readonly IAutoTrackConditionalValue.Factory _conditionalFactory;
        private readonly IAutoTrackFlagBool.Factory _flagBoolFactory;
        private readonly IAutoTrackItemValue.Factory _itemValueFactory;
        private readonly IAutoTrackMultipleDifference.Factory _differenceFactory;
        private readonly IAutoTrackMultipleOverride.Factory _overrideFactory;
        private readonly IAutoTrackMultipleSum.Factory _sumFactory;
        private readonly IAutoTrackStaticValue.Factory _staticFactory;
        private readonly IMemoryFlag.Factory _memoryFlagFactory;

        public ItemAutoTrackValueFactory(
            IItemDictionary items, IMemoryAddressProvider memoryAddressProvider, IRequirementDictionary requirements,
            IAutoTrackAddressBool.Factory boolFactory, IAutoTrackAddressValue.Factory valueFactory,
            IAutoTrackConditionalValue.Factory conditionalFactory, IAutoTrackFlagBool.Factory flagBoolFactory,
            IAutoTrackItemValue.Factory itemValueFactory, IAutoTrackMultipleDifference.Factory differenceFactory,
            IAutoTrackMultipleOverride.Factory overrideFactory, IAutoTrackMultipleSum.Factory sumFactory,
            IAutoTrackStaticValue.Factory staticFactory, IMemoryFlag.Factory memoryFlagFactory)
        {
            _memoryAddressProvider = memoryAddressProvider;
            _items = items;
            _requirements = requirements;
            
            _boolFactory = boolFactory;
            _valueFactory = valueFactory;
            _conditionalFactory = conditionalFactory;
            _flagBoolFactory = flagBoolFactory;
            _itemValueFactory = itemValueFactory;
            _differenceFactory = differenceFactory;
            _overrideFactory = overrideFactory;
            _sumFactory = sumFactory;
            _staticFactory = staticFactory;
            _memoryFlagFactory = memoryFlagFactory;
        }

        public IAutoTrackValue? GetAutoTrackValue(ItemType type)
        {
            return type switch
            {
                ItemType.Sword => _valueFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef359], 5, 1),
                ItemType.Shield => _valueFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef35a], 3, 0),
                ItemType.Mail => _valueFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef35b], 3, 0),
                ItemType.Bow => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef340], 0, 1),
                ItemType.Arrows => _overrideFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38e], 0x40), 2),
                    _boolFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef377], 0, 1)
                }),
                ItemType.Boomerang => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x80), 1),
                ItemType.RedBoomerang => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x40), 1),
                ItemType.Hookshot => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef342], 0, 1),
                ItemType.Bomb => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef343], 0, 1),
                ItemType.BigBomb => _overrideFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef22c], 0x10), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef22c], 0x20), 1)
                }),
                ItemType.Powder => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x10), 1),
                ItemType.MagicBat => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x80), 1),
                ItemType.Mushroom => _overrideFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x20), 2),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x20), 1)
                }),
                ItemType.Boots => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef355], 0, 1),
                ItemType.FireRod => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef345], 0, 1),
                ItemType.IceRod => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef346], 0, 1),
                ItemType.Bombos => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef347], 0, 1),
                ItemType.Ether => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef348], 0, 1),
                ItemType.Quake => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef349], 0, 1),
                ItemType.SmallKey => _valueFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef397], 29, 0),
                ItemType.Gloves => _valueFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef354], 2, 0),
                ItemType.Lamp => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef34a], 0, 1),
                ItemType.Hammer => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef34b], 0, 1),
                ItemType.Flute => _overrideFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x01), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x02), 1)
                }),
                ItemType.FluteActivated => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x01), 1),
                ItemType.Net => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef34d], 0, 1),
                ItemType.Book => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef34e], 0, 1),
                ItemType.Shovel => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x04), 1),
                ItemType.Flippers => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef356], 0, 1),
                ItemType.Bottle => _sumFactory(new List<IAutoTrackValue>
                {
                    _boolFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef35c], 0, 1),
                    _boolFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef35d], 0, 1),
                    _boolFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef35e], 0, 1),
                    _boolFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef35f], 0, 1)
                }),
                ItemType.CaneOfSomaria => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef350], 0, 1),
                ItemType.CaneOfByrna => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef351], 0, 1),
                ItemType.Cape => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef352], 0, 1),
                ItemType.Mirror => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef353], 1, 1),
                ItemType.HalfMagic => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef37b], 0, 1),
                ItemType.MoonPearl => _boolFactory(
                    _memoryAddressProvider.MemoryAddresses[0x7ef357], 0, 1),
                ItemType.HCSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _differenceFactory(
                            _itemValueFactory(_items[ItemType.HCUnlockedDoor]),
                            _conditionalFactory(
                                _requirements[RequirementType.KeyDropShuffleOff],
                                _itemValueFactory(_items[ItemType.HCFreeKey]),
                                _staticFactory(0))), null),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _valueFactory(
                            _memoryAddressProvider.MemoryAddresses[0x7ef4e0], 4, 0),
                        _valueFactory(
                            _memoryAddressProvider.MemoryAddresses[0x7ef4e1], 4, 0)
                    })),
                ItemType.EPSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _differenceFactory(
                            _itemValueFactory(_items[ItemType.EPUnlockedDoor]),
                            _conditionalFactory(
                                _requirements[RequirementType.KeyDropShuffleOff],
                                _itemValueFactory(_items[ItemType.EPFreeKey]),
                                _staticFactory(0))), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4e2], 2, 0)),
                ItemType.DPSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _differenceFactory(
                            _itemValueFactory(_items[ItemType.DPUnlockedDoor]),
                            _conditionalFactory(
                                _requirements[RequirementType.KeyDropShuffleOff],
                                _itemValueFactory(_items[ItemType.DPFreeKey]),
                                _staticFactory(0))), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4e3], 4, 0)),
                ItemType.ToHSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _itemValueFactory(_items[ItemType.ToHUnlockedDoor]), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4ea], 1, 0)),
                ItemType.ATSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _differenceFactory(
                            _itemValueFactory(_items[ItemType.ATUnlockedDoor]),
                            _conditionalFactory(
                                _requirements[RequirementType.KeyDropShuffleOff],
                                _itemValueFactory(_items[ItemType.ATFreeKey]),
                                _staticFactory(0))), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4e4], 4, 0)),
                ItemType.PoDSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _itemValueFactory(_items[ItemType.PoDUnlockedDoor]), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4e6], 6, 0)),
                ItemType.SPSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _differenceFactory(
                            _itemValueFactory(_items[ItemType.SPUnlockedDoor]),
                            _conditionalFactory(
                                _requirements[RequirementType.KeyDropShuffleOff],
                                _itemValueFactory(_items[ItemType.SPFreeKey]),
                                _staticFactory(0))), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4e5], 6, 0)),
                ItemType.SWSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _differenceFactory(
                            _itemValueFactory(_items[ItemType.SWUnlockedDoor]),
                            _conditionalFactory(
                                _requirements[RequirementType.KeyDropShuffleOff],
                                _itemValueFactory(_items[ItemType.SWFreeKey]),
                                _staticFactory(0))), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4e8], 5, 0)),
                ItemType.TTSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _differenceFactory(
                            _itemValueFactory(_items[ItemType.TTUnlockedDoor]),
                            _conditionalFactory(
                                _requirements[RequirementType.KeyDropShuffleOff],
                                _itemValueFactory(_items[ItemType.TTFreeKey]),
                                _staticFactory(0))), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4eb], 3, 0)),
                ItemType.IPSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _differenceFactory(
                            _itemValueFactory(_items[ItemType.IPUnlockedDoor]),
                            _conditionalFactory(
                                _requirements[RequirementType.KeyDropShuffleOff],
                                _itemValueFactory(_items[ItemType.IPFreeKey]),
                                _staticFactory(0))), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4e9], 6, 0)),
                ItemType.MMSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _differenceFactory(
                            _itemValueFactory(_items[ItemType.MMUnlockedDoor]),
                            _conditionalFactory(
                                _requirements[RequirementType.KeyDropShuffleOff],
                                _itemValueFactory(_items[ItemType.MMFreeKey]),
                                _staticFactory(0))), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4e7], 6, 0)),
                ItemType.TRSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _differenceFactory(
                            _itemValueFactory(_items[ItemType.TRUnlockedDoor]),
                            _conditionalFactory(
                                _requirements[RequirementType.KeyDropShuffleOff],
                                _itemValueFactory(_items[ItemType.TRFreeKey]),
                                _staticFactory(0))), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4ec], 6, 0)),
                ItemType.GTSmallKey => _conditionalFactory(
                    _requirements[RequirementType.GenericKeys],
                    _conditionalFactory(
                        _requirements[RequirementType.RaceIllegalTracking],
                        _differenceFactory(
                            _itemValueFactory(_items[ItemType.GTUnlockedDoor]),
                            _conditionalFactory(
                                _requirements[RequirementType.KeyDropShuffleOff],
                                _itemValueFactory(_items[ItemType.GTFreeKey]),
                                _staticFactory(0))), null),
                    _valueFactory(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4ed], 8, 0)),
                ItemType.HCBigKey => _overrideFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x80), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x40), 1)
                }),
                ItemType.EPBigKey => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x20), 1),
                ItemType.DPBigKey => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x10), 1),
                ItemType.ToHBigKey => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x20), 1),
                ItemType.PoDBigKey => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x02), 1),
                ItemType.SPBigKey => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x04), 1),
                ItemType.SWBigKey => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x80), 1),
                ItemType.TTBigKey => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x10), 1),
                ItemType.IPBigKey => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x40), 1),
                ItemType.MMBigKey => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x01), 1),
                ItemType.TRBigKey => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x08), 1),
                ItemType.GTBigKey => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x04), 1),
                ItemType.HCMap => _overrideFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x80), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x40), 1)
                }),
                ItemType.EPMap => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x20), 1),
                ItemType.DPMap => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x10), 1),
                ItemType.ToHMap => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x20), 1),
                ItemType.PoDMap => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x02), 1),
                ItemType.SPMap => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x04), 1),
                ItemType.SWMap => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x80), 1),
                ItemType.TTMap => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x10), 1),
                ItemType.IPMap => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x40), 1),
                ItemType.MMMap => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x01), 1),
                ItemType.TRMap => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x08), 1),
                ItemType.GTMap => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x04), 1),
                ItemType.EPCompass => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef365], 0x20), 1),
                ItemType.DPCompass => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef365], 0x10), 1),
                ItemType.ToHCompass => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x20), 1),
                ItemType.PoDCompass => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef365], 0x02), 1),
                ItemType.SPCompass => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef365], 0x04), 1),
                ItemType.SWCompass => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x80), 1),
                ItemType.TTCompass => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x10), 1),
                ItemType.IPCompass => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x40), 1),
                ItemType.MMCompass => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef365], 0x01), 1),
                ItemType.TRCompass => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x08), 1),
                ItemType.GTCompass => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x04), 1),
                ItemType.HCFreeKey => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0e5], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0e3], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef043], 0x04), 1)
                }),
                ItemType.ATFreeKey => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef181], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef161], 0x04), 1)
                }),
                ItemType.EPFreeKey => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef175], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef133], 0x04), 1)
                }),
                ItemType.DPFreeKey => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0c7], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0a7], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef087], 0x04), 1)
                }),
                ItemType.SPFreeKey => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef071], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06f], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06d], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06b], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef02d], 0x04), 1)
                }),
                ItemType.SWFreeKey => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0ad], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef073], 0x04), 1)
                }),
                ItemType.TTFreeKey => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef179], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef157], 0x04), 1)
                }),
                ItemType.IPFreeKey => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef01d], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07d], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07f], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef13f], 0x04), 1)
                }),
                ItemType.MMFreeKey => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef167], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef183], 0x04), 1)
                }),
                ItemType.TRFreeKey => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef16d], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef027], 0x04), 1)
                }),
                ItemType.GTFreeKey => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef117], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef137], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0f7], 0x04), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07b], 0x04), 1)
                }),
                ItemType.HCUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0e5], 0x80), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0e3], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef065], 0x80), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef045], 0x80), 1)
                    }),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef043], 0x80), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef023], 0x20), 1)
                    })
                }),
                ItemType.ATUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1c1], 0x20), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1a1], 0x80), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef181], 0x20), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef161], 0x20), 1)
                }),
                ItemType.EPUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef175], 0x80), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef173], 0x80), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef133], 0x80), 1)
                }),
                ItemType.DPUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef10b], 0x40), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0c7], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0a7], 0x20), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef087], 0x20), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef087], 0x40), 1)
                }),
                ItemType.ToHUnlockedDoor => _flagBoolFactory(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0ef], 0x80), 1),
                ItemType.PoDUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef095], 0x20), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef075], 0x80), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef015], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef055], 0x40), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef035], 0x10), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef035], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef035], 0x40), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef033], 0x40), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef017], 0x20), 1)
                }),
                ItemType.SPUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef051], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef071], 0x40), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06f], 0x10), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06f], 0x20), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06d], 0x20), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06b], 0x80), 1)
                    }),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06d], 0x40), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef04d], 0x80), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef02d], 0x40), 1)
                }),
                ItemType.SWUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0b1], 0x40), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0af], 0x20), 1)
                    }),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0d1], 0x40), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0b1], 0x20), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0ad], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0b3], 0x80), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef093], 0x20), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef073], 0x40), 1)
                }),
                ItemType.TTUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef179], 0x80), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef157], 0x80), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef089], 0x40), 1)
                }),
                ItemType.IPUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef01d], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07d], 0x40), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef09d], 0x40), 1)
                    }),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0bd], 0x80), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0bf], 0x80), 1)
                    }),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef11d], 0x80), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0fd], 0x80), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef13d], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef17d], 0x40), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef17f], 0x80), 1)
                    })
                }),
                ItemType.MMUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef167], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef185], 0x40), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef183], 0x40), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef183], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef185], 0x80), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef187], 0x80), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef127], 0x40), 1)
                }),
                ItemType.TRUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef18d], 0x80), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef16d], 0x20), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef16d], 0x10), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef16d], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef027], 0x80), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef029], 0x40), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef009], 0x80), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef18d], 0x80), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef189], 0x80), 1)
                    })
                }),
                ItemType.GTUnlockedDoor => _sumFactory(new List<IAutoTrackValue>
                {
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef119], 0x20), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef117], 0x40), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef137], 0x80), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0fb], 0x20), 1),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef11b], 0x40), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0f7], 0x40), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0f9], 0x20), 1)
                    }),
                    _flagBoolFactory(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07b], 0x40), 1),
                    _overrideFactory(new List<IAutoTrackValue>
                    {
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07b], 0x20), 1),
                        _flagBoolFactory(
                            _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef09b], 0x80), 1)
                    })
                }),
                _ => null
            };
        }
    }
}
