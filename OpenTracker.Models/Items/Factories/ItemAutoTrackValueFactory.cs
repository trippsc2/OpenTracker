using System.Collections.Generic;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Multiple;
using OpenTracker.Models.AutoTracking.Values.Single;
using OpenTracker.Models.AutoTracking.Values.Static;
using OpenTracker.Models.Requirements.AutoTracking;
using OpenTracker.Models.Requirements.GenericKeys;
using OpenTracker.Models.Requirements.KeyDropShuffle;

namespace OpenTracker.Models.Items.Factories;

/// <summary>
/// This class contains creation logic for item <see cref="IAutoTrackValue"/> objects.
/// </summary>
public class ItemAutoTrackValueFactory : IItemAutoTrackValueFactory
{
    private readonly IItemDictionary _items;
    private readonly IMemoryAddressProvider _memoryAddressProvider;

    private readonly IGenericKeysRequirementDictionary _genericKeysRequirements;
    private readonly IKeyDropShuffleRequirementDictionary _keyDropShuffleRequirements;

    private readonly IRaceIllegalTrackingRequirement _raceIllegalTrackingRequirement;

    private readonly IMemoryFlag.Factory _memoryFlagFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="items">
    ///     The <see cref="IItemDictionary"/>.
    /// </param>
    /// <param name="memoryAddressProvider">
    ///     The <see cref="IMemoryAddressProvider"/>.
    /// </param>
    /// <param name="genericKeysRequirements">
    ///     The <see cref="IGenericKeysRequirementDictionary"/>.
    /// </param>
    /// <param name="keyDropShuffleRequirements">
    ///     The <see cref="IKeyDropShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="raceIllegalTrackingRequirement">
    ///     The <see cref="IRaceIllegalTrackingRequirement"/>.
    /// </param>
    /// <param name="memoryFlagFactory">
    ///     An Autofac factory for creating new <see cref="IMemoryFlag"/> objects.
    /// </param>
    public ItemAutoTrackValueFactory(
        IItemDictionary items,
        IMemoryAddressProvider memoryAddressProvider,
        IGenericKeysRequirementDictionary genericKeysRequirements,
        IKeyDropShuffleRequirementDictionary keyDropShuffleRequirements,
        IRaceIllegalTrackingRequirement raceIllegalTrackingRequirement,
        IMemoryFlag.Factory memoryFlagFactory)
    {
        _memoryAddressProvider = memoryAddressProvider;
        _items = items;

        _genericKeysRequirements = genericKeysRequirements;
        _keyDropShuffleRequirements = keyDropShuffleRequirements;
            
        _raceIllegalTrackingRequirement = raceIllegalTrackingRequirement;

        _memoryFlagFactory = memoryFlagFactory;
    }

    public IAutoTrackValue? GetAutoTrackValue(ItemType type)
    {
        return type switch
        {
            ItemType.Sword => new AutoTrackAddressValue(
                _memoryAddressProvider.MemoryAddresses[0x7ef359], 5, 1),
            ItemType.Shield => new AutoTrackAddressValue(
                _memoryAddressProvider.MemoryAddresses[0x7ef35a], 3),
            ItemType.Mail => new AutoTrackAddressValue(
                _memoryAddressProvider.MemoryAddresses[0x7ef35b], 3),
            ItemType.Bow => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef340], 0, 1),
            ItemType.Arrows => new AutoTrackMultipleOverride(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38e], 0x40), 2),
                new AutoTrackAddressBool(
                    _memoryAddressProvider.MemoryAddresses[0x7ef377], 0, 1)
            }),
            ItemType.Boomerang => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x80), 1),
            ItemType.RedBoomerang => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x40), 1),
            ItemType.Hookshot => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef342], 0, 1),
            ItemType.Bomb => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef343], 0, 1),
            ItemType.BigBomb => new AutoTrackMultipleOverride(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef22c], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef22c], 0x20), 1)
            }),
            ItemType.Powder => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x10), 1),
            ItemType.MagicBat => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x80), 1),
            ItemType.Mushroom => new AutoTrackMultipleOverride(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef411], 0x20), 2),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x20), 1)
            }),
            ItemType.Boots => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef355], 0, 1),
            ItemType.FireRod => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef345], 0, 1),
            ItemType.IceRod => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef346], 0, 1),
            ItemType.Bombos => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef347], 0, 1),
            ItemType.Ether => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef348], 0, 1),
            ItemType.Quake => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef349], 0, 1),
            ItemType.SmallKey => new AutoTrackAddressValue(
                _memoryAddressProvider.MemoryAddresses[0x7ef397], 29),
            ItemType.Gloves => new AutoTrackAddressValue(
                _memoryAddressProvider.MemoryAddresses[0x7ef354], 2),
            ItemType.Lamp => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef34a], 0, 1),
            ItemType.Hammer => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef34b], 0, 1),
            ItemType.Flute => new AutoTrackMultipleOverride(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x01), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x02), 1)
            }),
            ItemType.FluteActivated => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x01), 1),
            ItemType.Net => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef34d], 0, 1),
            ItemType.Book => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef34e], 0, 1),
            ItemType.Shovel => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef38c], 0x04), 1),
            ItemType.Flippers => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef356], 0, 1),
            ItemType.Bottle => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackAddressBool(
                    _memoryAddressProvider.MemoryAddresses[0x7ef35c], 0, 1),
                new AutoTrackAddressBool(
                    _memoryAddressProvider.MemoryAddresses[0x7ef35d], 0, 1),
                new AutoTrackAddressBool(
                    _memoryAddressProvider.MemoryAddresses[0x7ef35e], 0, 1),
                new AutoTrackAddressBool(
                    _memoryAddressProvider.MemoryAddresses[0x7ef35f], 0, 1)
            }),
            ItemType.CaneOfSomaria => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef350], 0, 1),
            ItemType.CaneOfByrna => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef351], 0, 1),
            ItemType.Cape => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef352], 0, 1),
            ItemType.Mirror => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef353], 1, 1),
            ItemType.HalfMagic => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef37b], 0, 1),
            ItemType.MoonPearl => new AutoTrackAddressBool(
                _memoryAddressProvider.MemoryAddresses[0x7ef357], 0, 1),
            ItemType.HCSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackMultipleDifference(
                        new AutoTrackItemValue(_items[ItemType.HCUnlockedDoor]),
                        new AutoTrackConditionalValue(
                            _keyDropShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.HCFreeKey]),
                            new AutoTrackStaticValue(0))), null),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackAddressValue(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4e0], 4),
                    new AutoTrackAddressValue(
                        _memoryAddressProvider.MemoryAddresses[0x7ef4e1], 4)
                })),
            ItemType.EPSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackMultipleDifference(
                        new AutoTrackItemValue(_items[ItemType.EPUnlockedDoor]),
                        new AutoTrackConditionalValue(
                            _keyDropShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.EPFreeKey]),
                            new AutoTrackStaticValue(0))), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4e2], 2)),
            ItemType.DPSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackMultipleDifference(
                        new AutoTrackItemValue(_items[ItemType.DPUnlockedDoor]),
                        new AutoTrackConditionalValue(
                            _keyDropShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.DPFreeKey]),
                            new AutoTrackStaticValue(0))), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4e3], 4)),
            ItemType.ToHSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackItemValue(_items[ItemType.ToHUnlockedDoor]), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4ea], 1)),
            ItemType.ATSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackMultipleDifference(
                        new AutoTrackItemValue(_items[ItemType.ATUnlockedDoor]),
                        new AutoTrackConditionalValue(
                            _keyDropShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.ATFreeKey]),
                            new AutoTrackStaticValue(0))), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4e4], 4)),
            ItemType.PoDSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackItemValue(_items[ItemType.PoDUnlockedDoor]), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4e6], 6)),
            ItemType.SPSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackMultipleDifference(
                        new AutoTrackItemValue(_items[ItemType.SPUnlockedDoor]),
                        new AutoTrackConditionalValue(
                            _keyDropShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.SPFreeKey]),
                            new AutoTrackStaticValue(0))), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4e5], 6)),
            ItemType.SWSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackMultipleDifference(
                        new AutoTrackItemValue(_items[ItemType.SWUnlockedDoor]),
                        new AutoTrackConditionalValue(
                            _keyDropShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.SWFreeKey]),
                            new AutoTrackStaticValue(0))), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4e8], 5)),
            ItemType.TTSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackMultipleDifference(
                        new AutoTrackItemValue(_items[ItemType.TTUnlockedDoor]),
                        new AutoTrackConditionalValue(
                            _keyDropShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.TTFreeKey]),
                            new AutoTrackStaticValue(0))), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4eb], 3)),
            ItemType.IPSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackMultipleDifference(
                        new AutoTrackItemValue(_items[ItemType.IPUnlockedDoor]),
                        new AutoTrackConditionalValue(
                            _keyDropShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.IPFreeKey]),
                            new AutoTrackStaticValue(0))), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4e9], 6)),
            ItemType.MMSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackMultipleDifference(
                        new AutoTrackItemValue(_items[ItemType.MMUnlockedDoor]),
                        new AutoTrackConditionalValue(
                            _keyDropShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.MMFreeKey]),
                            new AutoTrackStaticValue(0))), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4e7], 6)),
            ItemType.TRSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackMultipleDifference(
                        new AutoTrackItemValue(_items[ItemType.TRUnlockedDoor]),
                        new AutoTrackConditionalValue(
                            _keyDropShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.TRFreeKey]),
                            new AutoTrackStaticValue(0))), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4ec], 6)),
            ItemType.GTSmallKey => new AutoTrackConditionalValue(
                _genericKeysRequirements[true],
                new AutoTrackConditionalValue(
                    _raceIllegalTrackingRequirement,
                    new AutoTrackMultipleDifference(
                        new AutoTrackItemValue(_items[ItemType.GTUnlockedDoor]),
                        new AutoTrackConditionalValue(
                            _keyDropShuffleRequirements[false],
                            new AutoTrackItemValue(_items[ItemType.GTFreeKey]),
                            new AutoTrackStaticValue(0))), null),
                new AutoTrackAddressValue(
                    _memoryAddressProvider.MemoryAddresses[0x7ef4ed], 8)),
            ItemType.HCBigKey => new AutoTrackMultipleOverride(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x40), 1)
            }),
            ItemType.EPBigKey => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x20), 1),
            ItemType.DPBigKey => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x10), 1),
            ItemType.ToHBigKey => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x20), 1),
            ItemType.PoDBigKey => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x02), 1),
            ItemType.SPBigKey => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x04), 1),
            ItemType.SWBigKey => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x80), 1),
            ItemType.TTBigKey => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x10), 1),
            ItemType.IPBigKey => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x40), 1),
            ItemType.MMBigKey => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef367], 0x01), 1),
            ItemType.TRBigKey => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x08), 1),
            ItemType.GTBigKey => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef366], 0x04), 1),
            ItemType.HCMap => new AutoTrackMultipleOverride(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x40), 1)
            }),
            ItemType.EPMap => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x20), 1),
            ItemType.DPMap => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x10), 1),
            ItemType.ToHMap => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x20), 1),
            ItemType.PoDMap => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x02), 1),
            ItemType.SPMap => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x04), 1),
            ItemType.SWMap => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x80), 1),
            ItemType.TTMap => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x10), 1),
            ItemType.IPMap => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x40), 1),
            ItemType.MMMap => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef369], 0x01), 1),
            ItemType.TRMap => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x08), 1),
            ItemType.GTMap => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef368], 0x04), 1),
            ItemType.EPCompass => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef365], 0x20), 1),
            ItemType.DPCompass => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef365], 0x10), 1),
            ItemType.ToHCompass => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x20), 1),
            ItemType.PoDCompass => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef365], 0x02), 1),
            ItemType.SPCompass => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef365], 0x04), 1),
            ItemType.SWCompass => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x80), 1),
            ItemType.TTCompass => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x10), 1),
            ItemType.IPCompass => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x40), 1),
            ItemType.MMCompass => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef365], 0x01), 1),
            ItemType.TRCompass => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x08), 1),
            ItemType.GTCompass => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef364], 0x04), 1),
            ItemType.HCFreeKey => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0e5], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0e3], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef043], 0x04), 1)
            }),
            ItemType.ATFreeKey => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef181], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef161], 0x04), 1)
            }),
            ItemType.EPFreeKey => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef175], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef133], 0x04), 1)
            }),
            ItemType.DPFreeKey => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0c7], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0a7], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef087], 0x04), 1)
            }),
            ItemType.SPFreeKey => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef071], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06f], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06d], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06b], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef02d], 0x04), 1)
            }),
            ItemType.SWFreeKey => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0ad], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef073], 0x04), 1)
            }),
            ItemType.TTFreeKey => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef179], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef157], 0x04), 1)
            }),
            ItemType.IPFreeKey => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef01d], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07d], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07f], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef13f], 0x04), 1)
            }),
            ItemType.MMFreeKey => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef167], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef183], 0x04), 1)
            }),
            ItemType.TRFreeKey => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef16d], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef027], 0x04), 1)
            }),
            ItemType.GTFreeKey => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef117], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef137], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0f7], 0x04), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07b], 0x04), 1)
            }),
            ItemType.HCUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0e5], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0e3], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef065], 0x80), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef045], 0x80), 1)
                }),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef043], 0x80), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef023], 0x20), 1)
                })
            }),
            ItemType.ATUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1c1], 0x20), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef1a1], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef181], 0x20), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef161], 0x20), 1)
            }),
            ItemType.EPUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef175], 0x80), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef173], 0x80), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef133], 0x80), 1)
            }),
            ItemType.DPUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef10b], 0x40), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0c7], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0a7], 0x20), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef087], 0x20), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef087], 0x40), 1)
            }),
            ItemType.ToHUnlockedDoor => new AutoTrackFlagBool(
                _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0ef], 0x80), 1),
            ItemType.PoDUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef095], 0x20), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef075], 0x80), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef015], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef055], 0x40), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef035], 0x10), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef035], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef035], 0x40), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef033], 0x40), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef017], 0x20), 1)
            }),
            ItemType.SPUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef051], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef071], 0x40), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06f], 0x10), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06f], 0x20), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06d], 0x20), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06b], 0x80), 1)
                }),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef06d], 0x40), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef04d], 0x80), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef02d], 0x40), 1)
            }),
            ItemType.SWUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0b1], 0x40), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0af], 0x20), 1)
                }),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0d1], 0x40), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0b1], 0x20), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0ad], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0b3], 0x80), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef093], 0x20), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef073], 0x40), 1)
            }),
            ItemType.TTUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef179], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef157], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef089], 0x40), 1)
            }),
            ItemType.IPUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef01d], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07d], 0x40), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef09d], 0x40), 1)
                }),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0bd], 0x80), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0bf], 0x80), 1)
                }),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef11d], 0x80), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0fd], 0x80), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef13d], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef17d], 0x40), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef17f], 0x80), 1)
                })
            }),
            ItemType.MMUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef167], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef185], 0x40), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef183], 0x40), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef183], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef185], 0x80), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef187], 0x80), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef127], 0x40), 1)
            }),
            ItemType.TRUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef18d], 0x80), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef16d], 0x20), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef16d], 0x10), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef16d], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef027], 0x80), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef029], 0x40), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef009], 0x80), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef18d], 0x80), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef189], 0x80), 1)
                })
            }),
            ItemType.GTUnlockedDoor => new AutoTrackMultipleSum(new List<IAutoTrackValue>
            {
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef119], 0x20), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef117], 0x40), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef137], 0x80), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0fb], 0x20), 1),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef11b], 0x40), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0f7], 0x40), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef0f9], 0x20), 1)
                }),
                new AutoTrackFlagBool(
                    _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07b], 0x40), 1),
                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                {
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef07b], 0x20), 1),
                    new AutoTrackFlagBool(
                        _memoryFlagFactory(_memoryAddressProvider.MemoryAddresses[0x7ef09b], 0x80), 1)
                })
            }),
            _ => null
        };
    }
}