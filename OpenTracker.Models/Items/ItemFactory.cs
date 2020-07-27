using OpenTracker.Models.AutoTracking;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This is the class for creating items.
    /// </summary>
    public static class ItemFactory
    {
        /// <summary>
        /// Returns the starting amount of the item.
        /// </summary>
        /// <param name="type">
        /// The type of item.
        /// </param>
        /// <returns>
        /// A 32-bit integer representing the starting amount of the item.
        /// </returns>
        private static int GetItemStarting(ItemType type)
        {
            return type == ItemType.Sword ? 1 : 0;
        }

        /// <summary>
        /// Returns the maximum amount of the item.
        /// </summary>
        /// <param name="type">
        /// The type of item.
        /// </param>
        /// <returns>
        /// A 32-bit integer representing the maximum amount of the item.
        /// </returns>
        private static int GetItemMaximum(ItemType type)
        {
            switch (type)
            {
                case ItemType.Sword:
                case ItemType.Crystal:
                    {
                        return 5;
                    }
                case ItemType.Shield:
                case ItemType.BombosDungeons:
                case ItemType.EtherDungeons:
                case ItemType.QuakeDungeons:
                case ItemType.SWSmallKey:
                case ItemType.MMSmallKey:
                    {
                        return 3;
                    }
                case ItemType.Mail:
                case ItemType.Arrows:
                case ItemType.Mushroom:
                case ItemType.Gloves:
                case ItemType.RedCrystal:
                case ItemType.Pendant:
                case ItemType.ATSmallKey:
                case ItemType.IPSmallKey:
                    {
                        return 2;
                    }
                case ItemType.Aga1:
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
                case ItemType.Aga2:
                case ItemType.GreenPendant:
                case ItemType.HCSmallKey:
                case ItemType.DPSmallKey:
                case ItemType.ToHSmallKey:
                case ItemType.SPSmallKey:
                case ItemType.TTSmallKey:
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
                case ItemType.TowerCrystals:
                case ItemType.GanonCrystals:
                    {
                        return 7;
                    }
                case ItemType.SmallKey:
                    {
                        return 29;
                    }
                case ItemType.Bottle:
                case ItemType.TRSmallKey:
                case ItemType.GTSmallKey:
                    {
                        return 4;
                    }
                case ItemType.PoDSmallKey:
                    {
                        return 6;
                    }
            }

            return 0;
        }

        /// <summary>
        /// Returns the base item without any autotracking or other wrappings.
        /// </summary>
        /// <param name="type">
        /// The type of the item.
        /// </param>
        /// <returns></returns>
        private static IItem GetBaseItem(ItemType type)
        {
            return new Item(type, GetItemStarting(type), GetItemMaximum(type));
        }

        /// <summary>
        /// Returns a list of memory addresses to which to subscribe.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// A list of memory addresses to which to subscribe.
        /// </returns>
        private static List<(MemorySegmentType, int)> GetMemoryAddresses(ItemType type)
        {
            return type switch
            {
                ItemType.Sword => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 25)
                },
                ItemType.Shield => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 26)
                },
                ItemType.Mail => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 27)
                },
                ItemType.Bow => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 0)
                },
                ItemType.Arrows => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 55),
                    (MemorySegmentType.Item, 78)
                },
                ItemType.Boomerang => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 76)
                },
                ItemType.RedBoomerang => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 76)
                },
                ItemType.Hookshot => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 2)
                },
                ItemType.Bomb => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 3)
                },
                ItemType.BigBomb => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Room, 556)
                },
                ItemType.Powder => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 76)
                },
                ItemType.MagicBat => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.NPCItem, 1)
                },
                ItemType.Mushroom => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.NPCItem, 1),
                    (MemorySegmentType.Item, 76)
                },
                ItemType.Boots => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 21)
                },
                ItemType.FireRod => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 5)
                },
                ItemType.IceRod => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 6)
                },
                ItemType.Bombos => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 7)
                },
                ItemType.Ether => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 8)
                },
                ItemType.Quake => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 9)
                },
                ItemType.Gloves => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 20)
                },
                ItemType.Lamp => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 10)
                },
                ItemType.Hammer => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 11)
                },
                ItemType.Flute => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 76)
                },
                ItemType.FluteActivated => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 76)
                },
                ItemType.Net => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 13)
                },
                ItemType.Book => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 14)
                },
                ItemType.Shovel => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 76)
                },
                ItemType.Flippers => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 22)
                },
                ItemType.Bottle => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 28),
                    (MemorySegmentType.Item, 29),
                    (MemorySegmentType.Item, 30),
                    (MemorySegmentType.Item, 31)
                },
                ItemType.CaneOfSomaria => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 16)
                },
                ItemType.CaneOfByrna => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 17)
                },
                ItemType.Cape => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 18)
                },
                ItemType.Mirror => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 19)
                },
                ItemType.HalfMagic => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 59)
                },
                ItemType.MoonPearl => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 23)
                },
                ItemType.EPBigKey => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 39)
                },
                ItemType.DPBigKey => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 39)
                },
                ItemType.ToHBigKey => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 38)
                },
                ItemType.PoDBigKey => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 39)
                },
                ItemType.SPBigKey => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 39)
                },
                ItemType.SWBigKey => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 38)
                },
                ItemType.TTBigKey => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 38)
                },
                ItemType.IPBigKey => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 38)
                },
                ItemType.MMBigKey => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 39)
                },
                ItemType.TRBigKey => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 38)
                },
                ItemType.GTBigKey => new List<(MemorySegmentType, int)>
                {
                    (MemorySegmentType.Item, 38)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns the function for autotracking the specified item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// A function for autotracking the specified item.
        /// </returns>
        private static Func<int, int?> GetAutoTrackFunction(ItemType type)
        {
            return type switch
            {
                ItemType.Sword => (current) =>
                {
                    if (current > 0)
                    {
                        byte? result = AutoTracker.Instance.CheckMemoryByteValue(
                            MemorySegmentType.Item, 25);

                        if (result.HasValue && result.Value != 255)
                        {
                            return result.Value + 1;
                        }
                    }

                    return null;
                },
                ItemType.Shield => (current) =>
                {
                    return AutoTracker.Instance.CheckMemoryByteValue(MemorySegmentType.Item, 26);
                },
                ItemType.Mail => (current) =>
                {
                    return AutoTracker.Instance.CheckMemoryByteValue(MemorySegmentType.Item, 27);
                },
                ItemType.Bow => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 0);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Arrows => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryFlag(
                        MemorySegmentType.Item, 78, 64);

                    if (result.HasValue && result.Value)
                    {
                        return 2;
                    }
                    else
                    {
                        result = AutoTracker.Instance.CheckMemoryByte(MemorySegmentType.Item, 55);

                        if (result.HasValue)
                        {
                            return result.Value ? 1 : 0;
                        }
                    }

                    return null;
                },
                ItemType.Boomerang => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryFlag(
                        MemorySegmentType.Item, 76, 128);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.RedBoomerang => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryFlag(
                        MemorySegmentType.Item, 76, 64);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Hookshot => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryByte(
                        MemorySegmentType.Item, 2);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Bomb => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryByte(
                        MemorySegmentType.Item, 3);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.BigBomb => (current) =>
                {
                    return AutoTracker.Instance.CheckMemoryFlagArray(
                        new (MemorySegmentType, int, byte)[2]
                        {
                            (MemorySegmentType.Room, 556, 16),
                            (MemorySegmentType.Room, 556, 32)
                        });
                },
                ItemType.Powder => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryFlag(
                        MemorySegmentType.Item, 76, 16);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.MagicBat => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryFlag(
                        MemorySegmentType.NPCItem, 1, 128);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Mushroom => (current) =>
                {
                    bool? witchTurnIn = AutoTracker.Instance.CheckMemoryFlag(
                        MemorySegmentType.NPCItem, 1, 32);
                    bool? mushroom = AutoTracker.Instance.CheckMemoryFlag(
                        MemorySegmentType.Item, 76, 32);

                    if (witchTurnIn.HasValue || mushroom.HasValue)
                    {
                        if (witchTurnIn.HasValue && witchTurnIn.Value)
                        {
                            return 2;
                        }

                        if (mushroom.HasValue && mushroom.Value)
                        {
                            return 1;
                        }
                        
                        return 0;
                    }

                    return null;
                },
                ItemType.Boots => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryByte(
                        MemorySegmentType.Item, 21);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.FireRod => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 5);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.IceRod => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryByte(
                        MemorySegmentType.Item, 6);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Bombos => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryByte(
                        MemorySegmentType.Item, 7);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Ether => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryByte(
                        MemorySegmentType.Item, 8);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Quake => (current) =>
                {
                    bool? result = AutoTracker.Instance.CheckMemoryByte(
                        MemorySegmentType.Item, 9);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Gloves => (current) =>
                {
                    int? result = AutoTracker.Instance
                        .CheckMemoryByteValue(MemorySegmentType.Item, 20);

                    if (result.HasValue)
                    {
                        return result.Value;
                    }

                    return null;
                },
                ItemType.Lamp => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 10);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Hammer => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 11);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Flute => (current) =>
                {
                    int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                        new (MemorySegmentType, int, byte)[2]
                        {
                            (MemorySegmentType.Item, 76, 1),
                            (MemorySegmentType.Item, 76, 2)
                        });

                    if (result.HasValue)
                    {
                        return result.Value > 0 ? 1 : 0;
                    }

                    return null;
                },
                ItemType.FluteActivated => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 76, 1);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Net => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 13);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Book => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 14);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Shovel => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 76, 4);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Flippers => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 22);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Bottle => (current) =>
                {
                    int? result = AutoTracker.Instance.CheckMemoryByteArray(
                        new (MemorySegmentType, int)[4]
                        {
                            (MemorySegmentType.Item, 28),
                            (MemorySegmentType.Item, 29),
                            (MemorySegmentType.Item, 30),
                            (MemorySegmentType.Item, 31)
                        });

                    if (result.HasValue)
                    {
                        return result.Value;
                    }

                    return null;
                },
                ItemType.CaneOfSomaria => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 16);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.CaneOfByrna => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 17);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Cape => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 18);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.Mirror => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 19);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.HalfMagic => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 59);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.MoonPearl => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryByte(MemorySegmentType.Item, 23);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.EPBigKey => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 39, 32);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.DPBigKey => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 39, 16);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.ToHBigKey => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 32);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.PoDBigKey => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 39, 2);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.SPBigKey => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 39, 4);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.SWBigKey => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 128);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.TTBigKey => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 16);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.IPBigKey => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 64);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.MMBigKey => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 39, 1);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.TRBigKey => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 8);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                ItemType.GTBigKey => (current) =>
                {
                    bool? result = AutoTracker.Instance
                        .CheckMemoryFlag(MemorySegmentType.Item, 38, 4);

                    if (result.HasValue)
                    {
                        return result.Value ? 1 : 0;
                    }

                    return null;
                },
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a finished item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// A finished item.
        /// </returns>
        internal static IItem GetItem(ItemType type)
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
                    {
                        return new AutoTrackedItem(
                            GetBaseItem(type), GetAutoTrackFunction(type),
                            GetMemoryAddresses(type));
                    }
                default:
                    {
                        return GetBaseItem(type);
                    }
            }
    }
    }
}
