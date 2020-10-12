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
                    {
                        return MemorySegmentType.Item;
                    }
                case ItemType.MagicBat:
                case ItemType.Mushroom:
                    {
                        return MemorySegmentType.NPCItem;
                    }
                case ItemType.BigBomb:
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
                case ItemType.Mirror:
                case ItemType.HalfMagic:
                case ItemType.MoonPearl:
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
                    {
                        return 64;
                    }
                case ItemType.Boomerang:
                case ItemType.MagicBat:
                case ItemType.SWBigKey:
                    {
                        return 128;
                    }
                case ItemType.BigBomb when index == 0:
                case ItemType.Powder:
                case ItemType.DPBigKey:
                case ItemType.TTBigKey:
                    {
                        return 16;
                    }
                case ItemType.BigBomb:
                case ItemType.Mushroom:
                case ItemType.EPBigKey:
                case ItemType.ToHBigKey:
                    {
                        return 32;
                    }
                case ItemType.Flute when index == 0:
                case ItemType.FluteActivated:
                case ItemType.MMBigKey:
                    {
                        return 1;
                    }
                case ItemType.Flute:
                case ItemType.PoDBigKey:
                    {
                        return 2;
                    }
                case ItemType.Shovel:
                case ItemType.SPBigKey:
                case ItemType.GTBigKey:
                    {
                        return 4;
                    }
                case ItemType.TRBigKey:
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
                        {
                            values.Add(GetFlagBool(type, i));
                        }
                        break;
                    case ItemType.Arrows:
                    case ItemType.Bottle:
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
        private static IAutoTrackValue GetMultipleDifference(ItemType type)
        {
            switch (type)
            {
                case ItemType.HCSmallKey:
                    {
                        return new AutoTrackMultipleDifference(
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 229),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 227),
                                        0x80), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 101),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 69),
                                            0x80), 1)
                                }),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 67),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 35),
                                            0x20), 1)
                                }),
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 229),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 227),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 67),
                                        4), 1)
                            }));
                    }
                case ItemType.DPSmallKey:
                    {
                        return new AutoTrackMultipleDifference(
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 267),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 199),
                                        0x80), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 167),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 135),
                                            0x20), 1)
                                }),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 135),
                                        0x40), 1)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 199),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 167),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 135),
                                        4), 1)
                            }));
                    }
                case ItemType.ToHSmallKey:
                    {
                        return new AutoTrackFlagBool(
                            new MemoryFlag(
                                AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 239),
                                0x80), 1);
                    }
                case ItemType.ATSmallKey:
                    {
                        return new AutoTrackMultipleDifference(
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 449),
                                        0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 417),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 385),
                                        0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 353),
                                        0x20), 1)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 385),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 353),
                                        4), 1)
                            }));
                    }
                case ItemType.PoDSmallKey:
                    {
                        return new AutoTrackMultipleSum(new List<IAutoTrackValue>
                        {
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 149),
                                        0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 117),
                                        0x80), 1)
                                }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 21),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 85),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 53),
                                        0x10), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 53),
                                    0x80), 1),
                            new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 53),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 51),
                                        0x40), 1)
                            }),
                            new AutoTrackFlagBool(
                                new MemoryFlag(
                                    AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 17),
                                    0x20), 1)
                            });
                    }
                case ItemType.SPSmallKey:
                    {
                        return new AutoTrackFlagBool(
                            new MemoryFlag(
                                AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 81),
                                0x80), 1);
                    }
                case ItemType.SWSmallKey:
                    {
                        return new AutoTrackMultipleDifference(
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 177),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 175),
                                            0x20), 1)
                                }),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 209),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 177),
                                            0x20), 1)
                                }),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 173),
                                        0x80), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 179),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 147),
                                            0x20), 1)
                                }),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 73),
                                        0x40), 1)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 173),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 115),
                                        4), 1)
                            }));
                    }
                case ItemType.TTSmallKey:
                    {
                        return new AutoTrackMultipleDifference(
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 377),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 343),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 137),
                                        0x40), 1)
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 377),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 343),
                                        4), 1)
                            }));
                    }
                case ItemType.IPSmallKey:
                    {
                        return new AutoTrackMultipleDifference(
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 29),
                                        0x80), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 125),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 157),
                                            0x40), 1)
                                }),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 189),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 191),
                                            0x80), 1)
                                }),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 285),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 253),
                                            0x80), 1)
                                }),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 317),
                                        0x80), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 381),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 383),
                                            0x80), 1)
                                })
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 29),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 125),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 127),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 319),
                                        4), 1)
                            }));
                    }
                case ItemType.MMSmallKey:
                    {
                        return new AutoTrackMultipleDifference(
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 359),
                                        0x80), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 389),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 387),
                                            0x40), 1)
                                }),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 387),
                                        0x80), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 389),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 391),
                                            0x80), 1)
                                }),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 295),
                                        0x40), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 395),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 393),
                                            0x80), 1)
                                })
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 359),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 387),
                                        4), 1)
                            }));
                    }
                case ItemType.TRSmallKey:
                    {
                        return new AutoTrackMultipleDifference(
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 397),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 365),
                                            0x20), 1)
                                }),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 365),
                                        0x10), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 365),
                                        0x80), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 39),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 41),
                                            0x40), 1)
                                }),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 9),
                                        0x80), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 395),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 393),
                                            0x80), 1)
                                })
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 365),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 39),
                                        4), 1)
                            }));
                    }
                case ItemType.GTSmallKey:
                    {
                        return new AutoTrackMultipleDifference(
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 281),
                                        0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 279),
                                        0x40), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 311),
                                        0x80), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 251),
                                        0x20), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 283),
                                        0x40), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 247),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 249),
                                            0x20), 1)
                                }),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 123),
                                        0x40), 1),
                                new AutoTrackMultipleOverride(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 123),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 155),
                                            0x80), 1)
                                })
                            }),
                            new AutoTrackMultipleSum(new List<IAutoTrackValue>
                            {
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 279),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 311),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 247),
                                        4), 1),
                                new AutoTrackFlagBool(
                                    new MemoryFlag(
                                        AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 123),
                                        4), 1)
                            }));
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
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.GenericKeys],
                            new AutoTrackConditionalValue(
                                RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                                GetMultipleDifference(type), null),
                            GetAddressValue(type));
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
