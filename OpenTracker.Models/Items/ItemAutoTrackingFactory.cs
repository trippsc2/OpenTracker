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
        /// Returns the memory segment of the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <param name="index">
        /// The index of the memory address to be retrieved.
        /// </param>
        /// <returns>
        /// The memory segment of the specified item.
        /// </returns>
        private static MemorySegmentType GetMemorySegment(ItemType type, int index = 0)
        {
            switch (type)
            {
                case ItemType.Sword:
                case ItemType.Shield:
                case ItemType.Mail:
                case ItemType.Bow:
                case ItemType.Arrows:
                case ItemType.Boomerang:
                case ItemType.RedBoomerang:
                case ItemType.Hookshot:
                case ItemType.Bomb:
                case ItemType.Powder:
                case ItemType.Mushroom when index == 1:
                case ItemType.Boots:
                case ItemType.FireRod:
                case ItemType.IceRod:
                case ItemType.Bombos:
                case ItemType.Ether:
                case ItemType.Quake:
                case ItemType.Gloves:
                case ItemType.Lamp:
                case ItemType.Hammer:
                case ItemType.Flute:
                case ItemType.FluteActivated:
                case ItemType.Net:
                case ItemType.Book:
                case ItemType.Shovel:
                case ItemType.Flippers:
                case ItemType.Bottle:
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
                case ItemType.SmallKey:
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
                        return MemorySegmentType.Item;
                    }
                case ItemType.MagicBat:
                case ItemType.Mushroom:
                    {
                        return MemorySegmentType.NPCItem;
                    }
                case ItemType.BigBomb:
                case ItemType.HCFreeKey:
                case ItemType.ATFreeKey:
                case ItemType.DPFreeKey:
                case ItemType.SWFreeKey:
                case ItemType.TTFreeKey:
                case ItemType.IPFreeKey:
                case ItemType.MMFreeKey:
                case ItemType.TRFreeKey:
                case ItemType.GTFreeKey:
                case ItemType.HCUnlockedDoor:
                case ItemType.ATUnlockedDoor:
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
                    {
                        return MemorySegmentType.Room;
                    }
                case ItemType.HCSmallKey:
                case ItemType.ATSmallKey:
                case ItemType.DPSmallKey:
                case ItemType.ToHSmallKey:
                case ItemType.PoDSmallKey:
                case ItemType.SPSmallKey:
                case ItemType.SWSmallKey:
                case ItemType.TTSmallKey:
                case ItemType.IPSmallKey:
                case ItemType.MMSmallKey:
                case ItemType.TRSmallKey:
                case ItemType.GTSmallKey:
                    {
                        return MemorySegmentType.SmallKey;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }
        }

        /// <summary>
        /// Returns the memory address index of the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <param name="index">
        /// The index of the memory address to be retrieved.
        /// </param>
        /// <returns>
        /// The memory address index.
        /// </returns>
        private static int GetMemoryIndex(ItemType type, int index = 0)
        {
            switch (type)
            {
                case ItemType.Sword:
                    {
                        return 25;
                    }
                case ItemType.Shield:
                    {
                        return 26;
                    }
                case ItemType.Mail:
                    {
                        return 27;
                    }
                case ItemType.Bow:
                case ItemType.HCSmallKey when index == 0:
                    {
                        return 0;
                    }
                case ItemType.Arrows when index == 0:
                    {
                        return 78;
                    }
                case ItemType.Arrows:
                    {
                        return 55;
                    }
                case ItemType.Boomerang:
                case ItemType.RedBoomerang:
                case ItemType.Powder:
                case ItemType.Mushroom when index == 1:
                case ItemType.Flute:
                case ItemType.FluteActivated:
                case ItemType.Shovel:
                    {
                        return 76;
                    }
                case ItemType.Hookshot:
                    {
                        return 2;
                    }
                case ItemType.Bomb:
                case ItemType.DPSmallKey:
                    {
                        return 3;
                    }
                case ItemType.BigBomb:
                    {
                        return 556;
                    }
                case ItemType.MagicBat:
                case ItemType.Mushroom:
                case ItemType.HCSmallKey:
                    {
                        return 1;
                    }
                case ItemType.Boots:
                    {
                        return 21;
                    }
                case ItemType.FireRod:
                case ItemType.SPSmallKey:
                    {
                        return 5;
                    }
                case ItemType.IceRod:
                case ItemType.PoDSmallKey:
                    {
                        return 6;
                    }
                case ItemType.Bombos:
                case ItemType.MMSmallKey:
                    {
                        return 7;
                    }
                case ItemType.Ether:
                case ItemType.SWSmallKey:
                    {
                        return 8;
                    }
                case ItemType.Quake:
                case ItemType.IPSmallKey:
                    {
                        return 9;
                    }
                case ItemType.Gloves:
                    {
                        return 20;
                    }
                case ItemType.Lamp:
                case ItemType.ToHSmallKey:
                    {
                        return 10;
                    }
                case ItemType.Hammer:
                case ItemType.TTSmallKey:
                    {
                        return 11;
                    }
                case ItemType.Net:
                case ItemType.GTSmallKey:
                    {
                        return 13;
                    }
                case ItemType.Book:
                    {
                        return 14;
                    }
                case ItemType.Flippers:
                    {
                        return 22;
                    }
                case ItemType.Bottle when index == 0:
                    {
                        return 28;
                    }
                case ItemType.Bottle when index == 1:
                    {
                        return 29;
                    }
                case ItemType.Bottle when index == 2:
                    {
                        return 30;
                    }
                case ItemType.Bottle:
                    {
                        return 31;
                    }
                case ItemType.CaneOfSomaria:
                    {
                        return 16;
                    }
                case ItemType.CaneOfByrna:
                    {
                        return 17;
                    }
                case ItemType.Cape:
                    {
                        return 18;
                    }
                case ItemType.Mirror:
                    {
                        return 19;
                    }
                case ItemType.HalfMagic:
                    {
                        return 59;
                    }
                case ItemType.MoonPearl:
                    {
                        return 23;
                    }
                case ItemType.EPBigKey:
                case ItemType.DPBigKey:
                case ItemType.PoDBigKey:
                case ItemType.SPBigKey:
                case ItemType.MMBigKey:
                    {
                        return 39;
                    }
                case ItemType.ToHBigKey:
                case ItemType.SWBigKey:
                case ItemType.TTBigKey:
                case ItemType.IPBigKey:
                case ItemType.TRBigKey:
                case ItemType.GTBigKey:
                    {
                        return 38;
                    }
                case ItemType.SmallKey:
                    {
                        return 47;
                    }
                case ItemType.ATSmallKey:
                    {
                        return 4;
                    }
                case ItemType.TRSmallKey:
                    {
                        return 12;
                    }
                case ItemType.HCMap:
                case ItemType.EPMap:
                case ItemType.DPMap:
                case ItemType.PoDMap:
                case ItemType.SPMap:
                case ItemType.MMMap:
                    {
                        return 41;
                    }
                case ItemType.ToHMap:
                case ItemType.SWMap:
                case ItemType.TTMap:
                case ItemType.IPMap:
                case ItemType.TRMap:
                case ItemType.GTMap:
                    {
                        return 40;
                    }
                case ItemType.EPCompass:
                case ItemType.DPCompass:
                case ItemType.PoDCompass:
                case ItemType.SPCompass:
                case ItemType.MMCompass:
                    {
                        return 37;
                    }
                case ItemType.ToHCompass:
                case ItemType.SWCompass:
                case ItemType.TTCompass:
                case ItemType.IPCompass:
                case ItemType.TRCompass:
                case ItemType.GTCompass:
                    {
                        return 36;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }
        }

        /// <summary>
        /// Returns the maximum value of the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// The maximum autotracking value.
        /// </returns>
        private static byte GetMaximum(ItemType type)
        {
            switch (type)
            {
                case ItemType.Sword:
                    {
                        return 5;
                    }
                case ItemType.Shield:
                case ItemType.SWSmallKey:
                case ItemType.MMSmallKey:
                    {
                        return 3;
                    }
                case ItemType.Mail:
                case ItemType.Gloves:
                case ItemType.ATSmallKey:
                case ItemType.IPSmallKey:
                    {
                        return 2;
                    }
                case ItemType.SmallKey:
                    {
                        return 99;
                    }
                case ItemType.HCSmallKey:
                case ItemType.DPSmallKey:
                case ItemType.ToHSmallKey:
                case ItemType.SPSmallKey:
                case ItemType.TTSmallKey:
                    {
                        return 1;
                    }
                case ItemType.PoDSmallKey:
                    {
                        return 6;
                    }
                case ItemType.TRSmallKey:
                case ItemType.GTSmallKey:
                    {
                        return 4;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }
        }

        /// <summary>
        /// Returns the adjustment value of the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// The adjustment autotracking value.
        /// </returns>
        private static int GetAdjustment(ItemType type)
        {
            switch (type)
            {
                case ItemType.Sword:
                    {
                        return 1;
                    }
                case ItemType.Shield:
                case ItemType.Mail:
                case ItemType.Gloves:
                case ItemType.SmallKey:
                case ItemType.HCSmallKey:
                case ItemType.ATSmallKey:
                case ItemType.DPSmallKey:
                case ItemType.ToHSmallKey:
                case ItemType.PoDSmallKey:
                case ItemType.SPSmallKey:
                case ItemType.SWSmallKey:
                case ItemType.TTSmallKey:
                case ItemType.IPSmallKey:
                case ItemType.MMSmallKey:
                case ItemType.TRSmallKey:
                case ItemType.GTSmallKey:
                    {
                        return 0;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }
        }

        /// <summary>
        /// Returns the comparison value of the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// The comparison autotracking value.
        /// </returns>
        private static byte GetComparison(ItemType type)
        {
            switch (type)
            {
                case ItemType.Bow:
                case ItemType.Arrows:
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
                case ItemType.Bottle:
                case ItemType.CaneOfSomaria:
                case ItemType.CaneOfByrna:
                case ItemType.Cape:
                case ItemType.HalfMagic:
                case ItemType.MoonPearl:
                case ItemType.HCSmallKey:
                    {
                        return 0;
                    }
                case ItemType.Mirror:
                    {
                        return 1;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }
        }

        /// <summary>
        /// Returns the memory flag for the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <param name="index">
        /// The index of the memory address to be retrieved.
        /// </param>
        /// <returns>
        /// The memory flag for the specified item.
        /// </returns>
        private static byte GetFlag(ItemType type, int index = 0)
        {
            switch (type)
            {
                case ItemType.Arrows:
                case ItemType.RedBoomerang:
                case ItemType.IPBigKey:
                case ItemType.HCMap when index == 0:
                case ItemType.IPMap:
                case ItemType.IPCompass:
                    {
                        return 64;
                    }
                case ItemType.Boomerang:
                case ItemType.MagicBat:
                case ItemType.SWBigKey:
                case ItemType.SWMap:
                case ItemType.SWCompass:
                case ItemType.HCMap:
                    {
                        return 128;
                    }
                case ItemType.BigBomb when index == 0:
                case ItemType.Powder:
                case ItemType.DPBigKey:
                case ItemType.TTBigKey:
                case ItemType.DPMap:
                case ItemType.TTMap:
                case ItemType.DPCompass:
                case ItemType.TTCompass:
                    {
                        return 16;
                    }
                case ItemType.BigBomb:
                case ItemType.Mushroom:
                case ItemType.EPBigKey:
                case ItemType.ToHBigKey:
                case ItemType.EPMap:
                case ItemType.ToHMap:
                case ItemType.EPCompass:
                case ItemType.ToHCompass:
                    {
                        return 32;
                    }
                case ItemType.Flute when index == 0:
                case ItemType.FluteActivated:
                case ItemType.MMBigKey:
                case ItemType.MMMap:
                case ItemType.MMCompass:
                    {
                        return 1;
                    }
                case ItemType.Flute:
                case ItemType.PoDBigKey:
                case ItemType.PoDMap:
                case ItemType.PoDCompass:
                    {
                        return 2;
                    }
                case ItemType.Shovel:
                case ItemType.SPBigKey:
                case ItemType.GTBigKey:
                case ItemType.SPMap:
                case ItemType.GTMap:
                case ItemType.SPCompass:
                case ItemType.GTCompass:
                    {
                        return 4;
                    }
                case ItemType.TRBigKey:
                case ItemType.TRMap:
                case ItemType.TRCompass:
                    {
                        return 8;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }
        }

        /// <summary>
        /// Returns the value returned when the comparison is true for the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <param name="index">
        /// The index of the memory address to be retrieved.
        /// </param>
        /// <returns>
        /// The value returned when the comparison is true.
        /// </returns>
        private static int GetTrueValue(ItemType type, int index = 0)
        {
            switch (type)
            {
                case ItemType.Arrows when index == 0:
                case ItemType.Mushroom when index == 0:
                    {
                        return 2;
                    }
                case ItemType.Bow:
                case ItemType.Boomerang:
                case ItemType.RedBoomerang:
                case ItemType.Arrows:
                case ItemType.Hookshot:
                case ItemType.Bomb:
                case ItemType.BigBomb:
                case ItemType.Powder:
                case ItemType.MagicBat:
                case ItemType.Mushroom:
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
                case ItemType.Bottle:
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
                case ItemType.HCSmallKey:
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
                        return 1;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }
        }

        /// <summary>
        /// Returns the autotracking flag value for the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <param name="index">
        /// The index of the memory address.
        /// </param>
        /// <returns>
        /// The autotracking flag value for the specified item.
        /// </returns>
        private static IAutoTrackValue GetFlagBool(ItemType type, int index = 0)
        {
            return new AutoTrackFlagBool(
                new MemoryFlag(
                    AutoTracker.GetMemoryAddress(
                        GetMemorySegment(type, index), GetMemoryIndex(type, index)),
                    GetFlag(type, index)), GetTrueValue(type, index));
        }

        /// <summary>
        /// Returns the list of autotracking values for the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// The list of autotracking values for the specified item.
        /// </returns>
        private static List<IAutoTrackValue> GetValues(ItemType type)
        {
            var values = new List<IAutoTrackValue>();
            int count;

            switch (type)
            {
                case ItemType.Arrows:
                case ItemType.BigBomb:
                case ItemType.Mushroom:
                case ItemType.Flute:
                case ItemType.HCMap:
                case ItemType.HCSmallKey:
                    {
                        count = 2;
                    }
                    break;
                case ItemType.Bottle:
                    {
                        count = 4;
                    }
                    break;
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }

            for (int i = 0; i < count; i++)
            {
                switch (type)
                {
                    case ItemType.Arrows when i == 0:
                    case ItemType.BigBomb:
                    case ItemType.Mushroom:
                    case ItemType.Flute:
                    case ItemType.HCMap:
                        {
                            values.Add(GetFlagBool(type, i));
                        }
                        break;
                    case ItemType.Arrows:
                    case ItemType.Bottle:
                    case ItemType.HCSmallKey:
                        {
                            values.Add(GetAddressBool(type, i));
                        }
                        break;
                }
            }

            return values;
        }

        /// <summary>
        /// Returns the autotracking value for the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// The autotracking value for the specified item.
        /// </returns>
        private static IAutoTrackValue GetAddressValue(ItemType type)
        {
            return new AutoTrackAddressValue(
                AutoTracker.GetMemoryAddress(GetMemorySegment(type), GetMemoryIndex(type)),
                GetMaximum(type), GetAdjustment(type));
        }

        /// <summary>
        /// Returns the autotracking boolean value for the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <param name="index">
        /// The index of the memory address.
        /// </param>
        /// <returns>
        /// The autotracking boolean value for the specified item.
        /// </returns>
        private static IAutoTrackValue GetAddressBool(ItemType type, int index = 0)
        {
            return new AutoTrackAddressBool(
                AutoTracker.GetMemoryAddress(
                    GetMemorySegment(type, index), GetMemoryIndex(type, index)),
                GetComparison(type), GetTrueValue(type, index));
        }

        /// <summary>
        /// Returns the autotracking override value for the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// The autotracking override value for the specified item.
        /// </returns>
        private static IAutoTrackValue GetMultipleOverride(ItemType type)
        {
            return new AutoTrackMultipleOverride(GetValues(type));
        }

        /// <summary>
        /// Returns the autotracking sum value for the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// The autotracking sum value for the specified item.
        /// </returns>
        private static IAutoTrackValue GetMultipleSum(ItemType type)
        {
            return new AutoTrackMultipleSum(GetValues(type));
        }

        /// <summary>
        /// Returns the autotracking difference value for the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// The autotracking difference value for the specified item.
        /// </returns>
        private static IAutoTrackValue GetSmallKeyValue(ItemType type)
        {
            switch (type)
            {
                case ItemType.HCSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackMultipleDifference(
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.HCUnlockedDoor]),
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.HCFreeKey])), null),
                            GetMultipleOverride(type));
                    }
                case ItemType.DPSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackMultipleDifference(
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPUnlockedDoor]),
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPFreeKey])), null),
                            GetAddressValue(type));
                    }
                case ItemType.ToHSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ToHUnlockedDoor]), null),
                            GetAddressValue(type));
                    }
                case ItemType.ATSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackMultipleDifference(
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ATUnlockedDoor]),
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ATFreeKey])), null),
                            GetAddressValue(type));
                    }
                case ItemType.PoDSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.PoDUnlockedDoor]), null),
                            GetAddressValue(type));
                    }
                case ItemType.SPSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SPUnlockedDoor]), null),
                            GetAddressValue(type));
                    }
                case ItemType.SWSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackMultipleDifference(
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWUnlockedDoor]),
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWFreeKey])), null),
                            GetAddressValue(type));
                    }
                case ItemType.TTSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackMultipleDifference(
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTUnlockedDoor]),
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTFreeKey])), null),
                            GetAddressValue(type));
                    }
                case ItemType.IPSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackMultipleDifference(
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPUnlockedDoor]),
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPFreeKey])), null),
                            GetAddressValue(type));
                    }
                case ItemType.MMSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackMultipleDifference(
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMUnlockedDoor]),
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMFreeKey])), null),
                            GetAddressValue(type));
                    }
                case ItemType.TRSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackMultipleDifference(
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRUnlockedDoor]),
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRFreeKey])), null),
                            GetAddressValue(type));
                    }
                case ItemType.GTSmallKey:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                new AutoTrackMultipleDifference(
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTUnlockedDoor]),
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTFreeKey])), null),
                            GetAddressValue(type));
                    }
                case ItemType.HCFreeKey:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xe5),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xe3),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x43),
                                    0x4), 1)
                        });
                    }
                case ItemType.ATFreeKey:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x181),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x161),
                                    0x4), 1)
                        });
                    }
                case ItemType.DPFreeKey:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xc7),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xa7),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x87),
                                    0x4), 1)
                        });
                    }
                case ItemType.SWFreeKey:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xad),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x73),
                                    0x4), 1)
                        });
                    }
                case ItemType.TTFreeKey:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x179),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x157),
                                    0x4), 1)
                        });
                    }
                case ItemType.IPFreeKey:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1d),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x7d),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x7f),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x13f),
                                    0x4), 1)
                        });
                    }
                case ItemType.MMFreeKey:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x167),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x183),
                                    0x4), 1)
                        });
                    }
                case ItemType.TRFreeKey:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x16d),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x27),
                                    0x4), 1)
                        });
                    }
                case ItemType.GTFreeKey:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x117),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x137),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf7),
                                    0x4), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x7b),
                                    0x4), 1)
                        });
                    }
                case ItemType.HCUnlockedDoor:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xe5),
                                    0x80), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xe3),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x65),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x45),
                                        0x80), 1)
                            }),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x43),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x23),
                                        0x20), 1)
                            })
                        });
                    }
                case ItemType.ATUnlockedDoor:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1c1),
                                    0x20), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1a1),
                                    0x80), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x181),
                                    0x20), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x161),
                                    0x20), 1)
                        });
                    }
                case ItemType.DPUnlockedDoor:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x10b),
                                    0x40), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xc7),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xa7),
                                        0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x87),
                                        0x20), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x87),
                                    0x40), 1)
                        });
                    }
                case ItemType.ToHUnlockedDoor:
                    {
                        return new AutoTrackFlagBool(
                            new MemoryFlag(
                                AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xef),
                                0x80), 1);
                    }
                case ItemType.PoDUnlockedDoor:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x95),
                                        0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x75),
                                        0x80), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x15),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x55),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x35),
                                        0x10), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x35),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x35),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x33),
                                        0x40), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x17),
                                    0x20), 1)
                        });
                    }
                case ItemType.SPUnlockedDoor:
                    {
                        return new AutoTrackFlagBool(
                            new MemoryFlag(
                                AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x51),
                                0x80), 1);
                    }
                case ItemType.SWUnlockedDoor:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xb1),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xaf),
                                        0x20), 1)
                            }),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xd1),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xb1),
                                        0x20), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xad),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xb3),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x93),
                                        0x20), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x73),
                                    0x40), 1)
                        });
                    }
                case ItemType.TTUnlockedDoor:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x179),
                                    0x80), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x157),
                                    0x80), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x89),
                                    0x40), 1)
                        });
                    }
                case ItemType.IPUnlockedDoor:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1d),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x7d),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x9d),
                                        0x40), 1)
                            }),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xbd),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xbf),
                                        0x80), 1)
                            }),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x11d),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xfd),
                                        0x80), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x13d),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x17d),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x17f),
                                        0x80), 1)
                            })
                        });
                    }
                case ItemType.MMUnlockedDoor:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x167),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x185),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x183),
                                        0x40), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x183),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x185),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x187),
                                        0x80), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x127),
                                    0x40), 1)
                        });
                    }
                case ItemType.TRUnlockedDoor:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x18d),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x16d),
                                        0x20), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x16d),
                                    0x10), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x16d),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x27),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x29),
                                        0x40), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x9),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x18d),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x189),
                                        0x80), 1)
                            })
                        });
                    }
                case ItemType.GTUnlockedDoor:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x119),
                                    0x20), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x117),
                                    0x40), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x137),
                                    0x80), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xfb),
                                    0x20), 1),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x11b),
                                    0x40), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf7),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf9),
                                        0x20), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x7b),
                                    0x40), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x7b),
                                        0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x9b),
                                        0x80), 1)
                            })
                        });
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }
        }

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
            switch (type)
            {
                case ItemType.Sword:
                case ItemType.Shield:
                case ItemType.Mail:
                case ItemType.Gloves:
                case ItemType.SmallKey:
                    {
                        return GetAddressValue(type);
                    }
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
                    {
                        return GetAddressBool(type);
                    }
                case ItemType.Arrows:
                case ItemType.BigBomb:
                case ItemType.Mushroom:
                case ItemType.Flute:
                case ItemType.HCMap:
                    {
                        return GetMultipleOverride(type);
                    }
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
                    {
                        return GetFlagBool(type);
                    }
                case ItemType.Bottle:
                    {
                        return GetMultipleSum(type);
                    }
                case ItemType.HCSmallKey:
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
                case ItemType.HCFreeKey:
                case ItemType.ATFreeKey:
                case ItemType.DPFreeKey:
                case ItemType.SWFreeKey:
                case ItemType.TTFreeKey:
                case ItemType.IPFreeKey:
                case ItemType.MMFreeKey:
                case ItemType.TRFreeKey:
                case ItemType.GTFreeKey:
                case ItemType.HCUnlockedDoor:
                case ItemType.ATUnlockedDoor:
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
                    {
                        return GetSmallKeyValue(type);
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
