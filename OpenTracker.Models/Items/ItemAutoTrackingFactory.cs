using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.AutotrackValues;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This is the class for creating item auto tracking.
    /// </summary>
    public static class ItemAutoTrackingFactory
    {
        /// <summary>
        /// Returns the autotracking value for the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// The autotracking value for the specified item.
        /// </returns>
        public static IAutoTrackValue GetAutoTrackValue(ItemType type)
        {
            return type switch
            {
                ItemType.Sword => new AutoTrackAddressValue(
                    AutoTracker.Instance.MemoryAddresses[0x7ef359], 5, 1),
                ItemType.Shield => new AutoTrackAddressValue(
                    AutoTracker.Instance.MemoryAddresses[0x7ef35a], 3, 0),
                ItemType.Mail => new AutoTrackAddressValue(
                    AutoTracker.Instance.MemoryAddresses[0x7ef35b], 3, 0),
                ItemType.Bow => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef340], 0, 1),
                ItemType.Arrows => new AutoTrackMultipleOverride(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef38e], 0x40), 2),
                        new AutoTrackAddressBool(
                            AutoTracker.Instance.MemoryAddresses[0x7ef377], 0, 1)
                    }),
                ItemType.Boomerang => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef38c], 0x80), 1),
                ItemType.RedBoomerang => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef38c], 0x40), 1),
                ItemType.Hookshot => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef342], 0, 1),
                ItemType.Bomb => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef343], 0, 1),
                ItemType.BigBomb => new AutoTrackMultipleOverride(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef22c], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef22c], 0x20), 1)
                    }),
                ItemType.Powder => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef38c], 0x10), 1),
                ItemType.MagicBat => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef411], 0x80), 1),
                ItemType.Mushroom => new AutoTrackMultipleOverride(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef411], 0x20), 2),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef38c], 0x20), 1)
                    }),
                ItemType.Boots => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef355], 0, 1),
                ItemType.FireRod => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef345], 0, 1),
                ItemType.IceRod => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef346], 0, 1),
                ItemType.Bombos => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef347], 0, 1),
                ItemType.Ether => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef348], 0, 1),
                ItemType.Quake => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef349], 0, 1),
                ItemType.SmallKey => new AutoTrackAddressValue(
                    AutoTracker.Instance.MemoryAddresses[0x7ef397], 29, 0),
                ItemType.Gloves => new AutoTrackAddressValue(
                    AutoTracker.Instance.MemoryAddresses[0x7ef354], 2, 0),
                ItemType.Lamp => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef34a], 0, 1),
                ItemType.Hammer => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef34b], 0, 1),
                ItemType.Flute => new AutoTrackMultipleOverride(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef38c], 0x01), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef38c], 0x02), 1)
                    }),
                ItemType.FluteActivated => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef38c], 0x01), 1),
                ItemType.Net => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef34d], 0, 1),
                ItemType.Book => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef34e], 0, 1),
                ItemType.Shovel => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef38c], 0x04), 1),
                ItemType.Flippers => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef356], 0, 1),
                ItemType.Bottle => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackAddressBool(
                            AutoTracker.Instance.MemoryAddresses[0x7ef35c], 0, 1),
                        new AutoTrackAddressBool(
                            AutoTracker.Instance.MemoryAddresses[0x7ef35d], 0, 1),
                        new AutoTrackAddressBool(
                            AutoTracker.Instance.MemoryAddresses[0x7ef35e], 0, 1),
                        new AutoTrackAddressBool(
                            AutoTracker.Instance.MemoryAddresses[0x7ef35f], 0, 1)
                    }),
                ItemType.CaneOfSomaria => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef350], 0, 1),
                ItemType.CaneOfByrna => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef351], 0, 1),
                ItemType.Cape => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef352], 0, 1),
                ItemType.Mirror => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef353], 1, 1),
                ItemType.HalfMagic => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef37b], 0, 1),
                ItemType.MoonPearl => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef357], 0, 1),
                ItemType.HCSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackMultipleDifference(
                            new AutoTrackItemValue(ItemDictionary.Instance[ItemType.HCUnlockedDoor]),
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOff],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.HCFreeKey]),
                                new AutoTrackStaticValue(0))), null),
                    new AutoTrackMultipleOverride(
                        new List<IAutoTrackValue>
                        {
                            new AutoTrackAddressValue(
                                AutoTracker.Instance.MemoryAddresses[0x7ef4e0], 4, 0),
                            new AutoTrackAddressValue(
                                AutoTracker.Instance.MemoryAddresses[0x7ef4e1], 4, 0)
                        })),
                ItemType.EPSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackMultipleDifference(
                            new AutoTrackItemValue(ItemDictionary.Instance[ItemType.EPUnlockedDoor]),
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOff],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.EPFreeKey]),
                                new AutoTrackStaticValue(0))), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4e2], 2, 0)),
                ItemType.DPSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackMultipleDifference(
                            new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPUnlockedDoor]),
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOff],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPFreeKey]),
                                new AutoTrackStaticValue(0))), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4e3], 4, 0)),
                ItemType.ToHSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ToHUnlockedDoor]), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4ea], 1, 0)),
                ItemType.ATSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackMultipleDifference(
                            new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ATUnlockedDoor]),
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOff],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ATFreeKey]),
                                new AutoTrackStaticValue(0))), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4e4], 4, 0)),
                ItemType.PoDSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.PoDUnlockedDoor]), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4e6], 6, 0)),
                ItemType.SPSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackMultipleDifference(
                            new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SPUnlockedDoor]),
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOff],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SPFreeKey]),
                                new AutoTrackStaticValue(0))), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4e5], 6, 0)),
                ItemType.SWSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackMultipleDifference(
                            new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWUnlockedDoor]),
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOff],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWFreeKey]),
                                new AutoTrackStaticValue(0))), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4e8], 5, 0)),
                ItemType.TTSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackMultipleDifference(
                            new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTUnlockedDoor]),
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOff],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTFreeKey]),
                                new AutoTrackStaticValue(0))), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4eb], 3, 0)),
                ItemType.IPSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackMultipleDifference(
                            new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPUnlockedDoor]),
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOff],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPFreeKey]),
                                new AutoTrackStaticValue(0))), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4e9], 6, 0)),
                ItemType.MMSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackMultipleDifference(
                            new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMUnlockedDoor]),
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOff],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMFreeKey]),
                                new AutoTrackStaticValue(0))), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4e7], 6, 0)),
                ItemType.TRSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackMultipleDifference(
                            new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRUnlockedDoor]),
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOff],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRFreeKey]),
                                new AutoTrackStaticValue(0))), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4ec], 6, 0)),
                ItemType.GTSmallKey => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.GenericKeys],
                    new AutoTrackConditionalValue(
                        RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                        new AutoTrackMultipleDifference(
                            new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTUnlockedDoor]),
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOff],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTFreeKey]),
                                new AutoTrackStaticValue(0))), null),
                    new AutoTrackAddressValue(
                        AutoTracker.Instance.MemoryAddresses[0x7ef4ed], 8, 0)),
                ItemType.HCBigKey => new AutoTrackMultipleOverride(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef367], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef367], 0x40), 1)
                    }),
                ItemType.EPBigKey => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef367], 0x20), 1),
                ItemType.DPBigKey => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef367], 0x10), 1),
                ItemType.ToHBigKey => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef366], 0x20), 1),
                ItemType.PoDBigKey => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef367], 0x02), 1),
                ItemType.SPBigKey => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef367], 0x04), 1),
                ItemType.SWBigKey => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef366], 0x80), 1),
                ItemType.TTBigKey => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef366], 0x10), 1),
                ItemType.IPBigKey => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef366], 0x40), 1),
                ItemType.MMBigKey => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef367], 0x01), 1),
                ItemType.TRBigKey => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef366], 0x08), 1),
                ItemType.GTBigKey => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef366], 0x04), 1),
                ItemType.HCMap => new AutoTrackMultipleOverride(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef369], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef369], 0x40), 1)
                    }),
                ItemType.EPMap => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef369], 0x20), 1),
                ItemType.DPMap => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef369], 0x10), 1),
                ItemType.ToHMap => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef368], 0x20), 1),
                ItemType.PoDMap => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef369], 0x02), 1),
                ItemType.SPMap => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef369], 0x04), 1),
                ItemType.SWMap => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef368], 0x80), 1),
                ItemType.TTMap => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef368], 0x10), 1),
                ItemType.IPMap => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef368], 0x40), 1),
                ItemType.MMMap => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef369], 0x01), 1),
                ItemType.TRMap => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef368], 0x08), 1),
                ItemType.GTMap => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef368], 0x04), 1),
                ItemType.EPCompass => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef365], 0x20), 1),
                ItemType.DPCompass => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef365], 0x10), 1),
                ItemType.ToHCompass => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef364], 0x20), 1),
                ItemType.PoDCompass => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef365], 0x02), 1),
                ItemType.SPCompass => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef365], 0x04), 1),
                ItemType.SWCompass => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef364], 0x80), 1),
                ItemType.TTCompass => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef364], 0x10), 1),
                ItemType.IPCompass => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef364], 0x40), 1),
                ItemType.MMCompass => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef365], 0x01), 1),
                ItemType.TRCompass => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef364], 0x08), 1),
                ItemType.GTCompass => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef364], 0x04), 1),
                ItemType.HCFreeKey => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0e5], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0e3], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef043], 0x04), 1)
                        }),
                ItemType.ATFreeKey => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef181], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef161], 0x04), 1)
                    }),
                ItemType.EPFreeKey => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef175], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef133], 0x04), 1)
                    }),
                ItemType.DPFreeKey => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0c7], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0a7], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef087], 0x04), 1)
                    }),
                ItemType.SPFreeKey => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef071], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef06f], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef06d], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef06b], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef02d], 0x04), 1)
                    }),
                ItemType.SWFreeKey => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0ad], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef073], 0x04), 1)
                    }),
                ItemType.TTFreeKey => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef179], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef157], 0x04), 1)
                    }),
                ItemType.IPFreeKey => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef01d], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef07d], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef07f], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef13f], 0x04), 1)
                    }),
                ItemType.MMFreeKey => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef167], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef183], 0x04), 1)
                    }),
                ItemType.TRFreeKey => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef16d], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef027], 0x04), 1)
                    }),
                ItemType.GTFreeKey => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef117], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef137], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0f7], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef07b], 0x04), 1)
                    }),
                ItemType.HCUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0e5], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0e3], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef065], 0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef045], 0x80), 1)
                            }),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef043], 0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef023], 0x20), 1)
                            })
                    }),
                ItemType.ATUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1c1], 0x20), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1a1], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef181], 0x20), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef161], 0x20), 1)
                    }),
                ItemType.EPUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef175], 0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef173], 0x80), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef133], 0x80), 1)
                    }),
                ItemType.DPUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef10b], 0x40), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0c7], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0a7], 0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef087], 0x20), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef087], 0x40), 1)
                    }),
                ItemType.ToHUnlockedDoor => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0ef], 0x80), 1),
                ItemType.PoDUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef095], 0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef075], 0x80), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef015], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef055], 0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef035], 0x10), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef035], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef035], 0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef033], 0x40), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef017], 0x20), 1)
                    }),
                ItemType.SPUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef051], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef071], 0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef06f], 0x10), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef06f], 0x20), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef06d], 0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef06b], 0x80), 1)
                            }),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef06d], 0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef04d], 0x80), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef02d], 0x40), 1)
                    }),
                ItemType.SWUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0b1], 0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0af], 0x20), 1)
                            }),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0d1], 0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0b1], 0x20), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0ad], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0b3], 0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef093], 0x20), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef073], 0x40), 1)
                    }),
                ItemType.TTUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef179], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef157], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef089], 0x40), 1)
                    }),
                ItemType.IPUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef01d], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef07d], 0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef09d], 0x40), 1)
                            }),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0bd], 0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0bf], 0x80), 1)
                            }),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef11d], 0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0fd], 0x80), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef13d], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef17d], 0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef17f], 0x80), 1)
                            })
                    }),
                ItemType.MMUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef167], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef185], 0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef183], 0x40), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef183], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef185], 0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef187], 0x80), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef127], 0x40), 1)
                    }),
                ItemType.TRUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef18d], 0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef16d], 0x20), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef16d], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef16d], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef027], 0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef029], 0x40), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef009], 0x80), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef18d], 0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef189], 0x80), 1)
                            })
                    }),
                ItemType.GTUnlockedDoor => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef119], 0x20), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef117], 0x40), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef137], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0fb], 0x20), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef11b], 0x40), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0f7], 0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0f9], 0x20), 1)
                            }),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef07b], 0x40), 1),
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef07b], 0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef09b], 0x80), 1)
                            })
                    }),
                _ => null
            };
        }
    }
}
