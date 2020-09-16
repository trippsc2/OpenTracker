using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.AutotrackValues
{
    /// <summary>
    /// This is the class containing creation logic for autotracking values.
    /// </summary>
    public static class AutoTrackValueFactory
    {
        /// <summary>
        /// Returns the specified memory address.
        /// </summary>
        /// <param name="memorySegment">
        /// The memory segment of the address.
        /// </param>
        /// <param name="index">
        /// The index of the address.
        /// </param>
        /// <returns>
        /// The memory address.
        /// </returns>
        private static MemoryAddress GetMemoryAddress(MemorySegmentType memorySegment, int index)
        {
            return memorySegment switch
            {
                MemorySegmentType.Room => AutoTracker.Instance.RoomMemory[index],
                MemorySegmentType.OverworldEvent =>
                    AutoTracker.Instance.OverworldEventMemory[index],
                MemorySegmentType.Item => AutoTracker.Instance.ItemMemory[index],
                MemorySegmentType.NPCItem => AutoTracker.Instance.NPCItemMemory[index],
                _ => throw new ArgumentOutOfRangeException(nameof(memorySegment)),
            };
        }

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
        private static MemorySegmentType GetItemMemorySegment(ItemType type, int index = 0)
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
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }
        }

        /// <summary>
        /// Returns the memory segment of the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID of the section.
        /// </param>
        /// <param name="index">
        /// The index of the section.
        /// </param>
        /// <returns>
        /// The memory segment of the specified section.
        /// </returns>
        private static MemorySegmentType GetSectionMemorySegment(
            LocationID id, int sectionIndex = 0)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                case LocationID.LumberjackCave:
                case LocationID.BlindsHouse:
                case LocationID.TheWell:
                case LocationID.ChickenHouse:
                case LocationID.Tavern:
                case LocationID.ForestHideout:
                case LocationID.CastleSecret when sectionIndex == 1:
                case LocationID.SahasrahlasHut when sectionIndex == 0:
                case LocationID.BonkRocks:
                case LocationID.KingsTomb:
                case LocationID.AginahsCave:
                case LocationID.Dam when sectionIndex == 0:
                case LocationID.MiniMoldormCave:
                case LocationID.IceRodCave:
                case LocationID.FatFairy:
                case LocationID.HypeCave:
                case LocationID.SouthOfGrove:
                case LocationID.WaterfallFairy:
                case LocationID.GraveyardLedge:
                case LocationID.CShapedHouse:
                case LocationID.TreasureGame:
                case LocationID.BombableShack:
                case LocationID.HammerPegs:
                case LocationID.MireShack:
                case LocationID.CheckerboardCave:
                case LocationID.SpectacleRock when sectionIndex == 1:
                case LocationID.SpikeCave:
                case LocationID.SpiralCave:
                case LocationID.ParadoxCave:
                case LocationID.SuperBunnyCave:
                case LocationID.HookshotCave:
                case LocationID.MimicCave:
                case LocationID.AgahnimTower when sectionIndex == 1:
                case LocationID.EasternPalace when sectionIndex == 1:
                case LocationID.DesertPalace when sectionIndex == 1:
                case LocationID.TowerOfHera when sectionIndex == 1:
                case LocationID.PalaceOfDarkness when sectionIndex == 1:
                case LocationID.SwampPalace when sectionIndex == 1:
                case LocationID.SkullWoods when sectionIndex == 1:
                case LocationID.ThievesTown when sectionIndex == 1:
                case LocationID.IcePalace when sectionIndex == 1:
                case LocationID.MiseryMire when sectionIndex == 1:
                case LocationID.TurtleRock when sectionIndex == 1:
                    {
                        return MemorySegmentType.Room;
                    }
                case LocationID.Pedestal:
                case LocationID.RaceGame:
                case LocationID.GroveDiggingSpot:
                case LocationID.Dam:
                case LocationID.PyramidLedge:
                case LocationID.DiggingGame:
                case LocationID.ZoraArea when sectionIndex == 0:
                case LocationID.DesertLedge:
                case LocationID.BumperCave:
                case LocationID.LakeHyliaIsland:
                case LocationID.SpectacleRock:
                case LocationID.FloatingIsland:
                    {
                        return MemorySegmentType.OverworldEvent;
                    }
                case LocationID.BottleVendor:
                case LocationID.CastleSecret:
                case LocationID.Hobo:
                case LocationID.PurpleChest:
                    {
                        return MemorySegmentType.Item;
                    }
                case LocationID.SickKid:
                case LocationID.MagicBat:
                case LocationID.Library:
                case LocationID.MushroomSpot:
                case LocationID.WitchsHut:
                case LocationID.SahasrahlasHut:
                case LocationID.HauntedGrove:
                case LocationID.BombosTablet:
                case LocationID.ZoraArea:
                case LocationID.Catfish:
                case LocationID.Blacksmith:
                case LocationID.OldMan:
                case LocationID.EtherTablet:
                    {
                        return MemorySegmentType.NPCItem;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(id));
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
        private static int GetItemMemoryIndex(ItemType type, int index = 0)
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
                    {
                        return 3;
                    }
                case ItemType.BigBomb:
                    {
                        return 556;
                    }
                case ItemType.MagicBat:
                case ItemType.Mushroom:
                    {
                        return 1;
                    }
                case ItemType.Boots:
                    {
                        return 21;
                    }
                case ItemType.FireRod:
                    {
                        return 5;
                    }
                case ItemType.IceRod:
                    {
                        return 6;
                    }
                case ItemType.Bombos:
                    {
                        return 7;
                    }
                case ItemType.Ether:
                    {
                        return 8;
                    }
                case ItemType.Quake:
                    {
                        return 9;
                    }
                case ItemType.Gloves:
                    {
                        return 20;
                    }
                case ItemType.Lamp:
                    {
                        return 10;
                    }
                case ItemType.Hammer:
                    {
                        return 11;
                    }
                case ItemType.Net:
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
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }
        }

        /// <summary>
        /// Returns the memory address index of the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID of the section.
        /// </param>
        /// <param name="sectionIndex">
        /// The index of the section.
        /// </param>
        /// <param name="index">
        /// The index of the memory address to be retrieved.
        /// </param>
        /// <returns>
        /// The memory address index.
        /// </returns>
        private static int GetSectionMemoryIndex(LocationID id, int sectionIndex = 0, int index = 0)
        {
            switch (id)
            {
                case LocationID.LinksHouse when index == 0:
                case LocationID.MagicBat:
                case LocationID.MushroomSpot:
                case LocationID.WitchsHut:
                case LocationID.BombosTablet:
                case LocationID.Blacksmith:
                case LocationID.EtherTablet:
                    {
                        return 1;
                    }
                case LocationID.LinksHouse:
                    {
                        return 520;
                    }
                case LocationID.Pedestal:
                    {
                        return 128;
                    }
                case LocationID.LumberjackCave:
                    {
                        return 453;
                    }
                case LocationID.BlindsHouse when sectionIndex == 0 && index == 3:
                    {
                        return 571;
                    }
                case LocationID.BlindsHouse:
                    {
                        return 570;
                    }
                case LocationID.TheWell when sectionIndex == 0 && index == 3:
                    {
                        return 95;
                    }
                case LocationID.TheWell:
                    {
                        return 94;
                    }
                case LocationID.BottleVendor:
                case LocationID.Hobo:
                case LocationID.PurpleChest:
                    {
                        return 137;
                    }
                case LocationID.ChickenHouse:
                    {
                        return 528;
                    }
                case LocationID.Tavern:
                    {
                        return 518;
                    }
                case LocationID.SickKid:
                case LocationID.Library:
                case LocationID.SahasrahlasHut when sectionIndex == 1:
                case LocationID.HauntedGrove:
                case LocationID.ZoraArea when sectionIndex == 1:
                case LocationID.Catfish:
                case LocationID.OldMan:
                    {
                        return 0;
                    }
                case LocationID.RaceGame:
                    {
                        return 40;
                    }
                case LocationID.ForestHideout:
                    {
                        return 451;
                    }
                case LocationID.CastleSecret when sectionIndex == 0:
                    {
                        return 134;
                    }
                case LocationID.CastleSecret:
                    {
                        return 170;
                    }
                case LocationID.SahasrahlasHut:
                    {
                        return 522;
                    }
                case LocationID.BonkRocks:
                    {
                        return 584;
                    }
                case LocationID.KingsTomb:
                    {
                        return 550;
                    }
                case LocationID.AginahsCave:
                    {
                        return 532;
                    }
                case LocationID.GroveDiggingSpot:
                    {
                        return 42;
                    }
                case LocationID.Dam when sectionIndex == 0:
                    {
                        return 534;
                    }
                case LocationID.Dam:
                    {
                        return 59;
                    }
                case LocationID.MiniMoldormCave when sectionIndex == 0 && index == 4:
                    {
                        return 583;
                    }
                case LocationID.MiniMoldormCave:
                    {
                        return 582;
                    }
                case LocationID.IceRodCave:
                    {
                        return 576;
                    }
                case LocationID.PyramidLedge:
                    {
                        return 91;
                    }
                case LocationID.FatFairy:
                    {
                        return 556;
                    }
                case LocationID.HypeCave when sectionIndex == 0 && index == 4:
                    {
                        return 573;
                    }
                case LocationID.HypeCave:
                    {
                        return 572;
                    }
                case LocationID.SouthOfGrove:
                case LocationID.GraveyardLedge:
                    {
                        return 567;
                    }
                case LocationID.DiggingGame:
                    {
                        return 104;
                    }
                case LocationID.WaterfallFairy:
                    {
                        return 552;
                    }
                case LocationID.ZoraArea:
                    {
                        return 129;
                    }
                case LocationID.DesertLedge:
                    {
                        return 48;
                    }
                case LocationID.CShapedHouse:
                    {
                        return 568;
                    }
                case LocationID.TreasureGame:
                    {
                        return 525;
                    }
                case LocationID.BombableShack:
                    {
                        return 524;
                    }
                case LocationID.HammerPegs:
                    {
                        return 591;
                    }
                case LocationID.BumperCave:
                    {
                        return 74;
                    }
                case LocationID.LakeHyliaIsland:
                    {
                        return 53;
                    }
                case LocationID.MireShack:
                    {
                        return 538;
                    }
                case LocationID.CheckerboardCave:
                    {
                        return 589;
                    }
                case LocationID.SpectacleRock when sectionIndex == 0:
                    {
                        return 3;
                    }
                case LocationID.SpectacleRock:
                    {
                        return 469;
                    }
                case LocationID.SpikeCave:
                    {
                        return 558;
                    }
                case LocationID.SpiralCave:
                    {
                        return 508;
                    }
                case LocationID.ParadoxCave when sectionIndex == 0:
                    {
                        return 510;
                    }
                case LocationID.ParadoxCave when sectionIndex == 1 && index == 4:
                    {
                        return 479;
                    }
                case LocationID.ParadoxCave:
                    {
                        return 478;
                    }
                case LocationID.SuperBunnyCave:
                    {
                        return 496;
                    }
                case LocationID.HookshotCave:
                    {
                        return 120;
                    }
                case LocationID.FloatingIsland:
                    {
                        return 5;
                    }
                case LocationID.MimicCave:
                    {
                        return 536;
                    }
                case LocationID.AgahnimTower when sectionIndex == 1:
                    {
                        return 133;
                    }
                case LocationID.EasternPalace when sectionIndex == 1:
                    {
                        return 401;
                    }
                case LocationID.DesertPalace when sectionIndex == 1:
                    {
                        return 103;
                    }
                case LocationID.TowerOfHera when sectionIndex == 1:
                    {
                        return 15;
                    }
                case LocationID.PalaceOfDarkness when sectionIndex == 1:
                    {
                        return 181;
                    }
                case LocationID.SwampPalace when sectionIndex == 1:
                    {
                        return 13;
                    }
                case LocationID.SkullWoods when sectionIndex == 1:
                    {
                        return 83;
                    }
                case LocationID.ThievesTown when sectionIndex == 1:
                    {
                        return 345;
                    }
                case LocationID.IcePalace when sectionIndex == 1:
                    {
                        return 445;
                    }
                case LocationID.MiseryMire when sectionIndex == 1:
                    {
                        return 289;
                    }
                case LocationID.TurtleRock when sectionIndex == 1:
                    {
                        return 329;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(id));
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
        private static byte GetItemMaximum(ItemType type)
        {
            switch (type)
            {
                case ItemType.Sword:
                    {
                        return 5;
                    }
                case ItemType.Shield:
                    {
                        return 3;
                    }
                case ItemType.Mail:
                case ItemType.Gloves:
                    {
                        return 2;
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
        private static int GetItemAdjustment(ItemType type)
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
        private static byte GetItemComparison(ItemType type)
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
        private static int GetItemTrueValue(ItemType type, int index = 0)
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
        private static byte GetItemFlag(ItemType type, int index = 0)
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
        /// Returns the memory flag for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID of the section.
        /// </param>
        /// <param name="sectionIndex">
        /// The index of the section.
        /// </param>
        /// <param name="index">
        /// The index of the memory flag to be retrieved.
        /// </param>
        /// <returns>
        /// The memory flag for the specified item.
        /// </returns>
        private static byte GetSectionFlag(LocationID id, int sectionIndex = 0, int index = 0)
        {
            switch (id)
            {
                case LocationID.LinksHouse when index == 0:
                case LocationID.SickKid:
                case LocationID.MiniMoldormCave when index == 4:
                case LocationID.HypeCave when index == 4:
                case LocationID.SouthOfGrove:
                case LocationID.TreasureGame:
                case LocationID.Blacksmith:
                case LocationID.HammerPegs:
                    {
                        return 4;
                    }
                case LocationID.LinksHouse:
                case LocationID.BlindsHouse when sectionIndex == 1 && index == 0:
                case LocationID.TheWell when sectionIndex == 1 && index == 0:
                case LocationID.ChickenHouse:
                case LocationID.Tavern:
                case LocationID.MushroomSpot:
                case LocationID.CastleSecret when sectionIndex == 1:
                case LocationID.SahasrahlasHut when sectionIndex == 0 && index == 0:
                case LocationID.SahasrahlasHut when sectionIndex == 1 && index == 0:
                case LocationID.BonkRocks:
                case LocationID.KingsTomb:
                case LocationID.AginahsCave:
                case LocationID.Dam when sectionIndex == 0:
                case LocationID.MiniMoldormCave when index == 0:
                case LocationID.IceRodCave:
                case LocationID.FatFairy when index == 0:
                case LocationID.HypeCave when index == 0:
                case LocationID.WaterfallFairy when index == 0:
                case LocationID.CShapedHouse:
                case LocationID.BombableShack:
                case LocationID.PurpleChest:
                case LocationID.MireShack when index == 0:
                case LocationID.SpectacleRock when sectionIndex == 1:
                case LocationID.SpikeCave:
                case LocationID.SpiralCave:
                case LocationID.ParadoxCave when index == 0:
                case LocationID.SuperBunnyCave when index == 0:
                case LocationID.HookshotCave when sectionIndex == 1 && index == 0:
                case LocationID.MimicCave:
                    {
                        return 16;
                    }
                case LocationID.Pedestal:
                case LocationID.BlindsHouse when sectionIndex == 0 && index == 1:
                case LocationID.TheWell when sectionIndex == 0 && index == 1:
                case LocationID.RaceGame:
                case LocationID.SahasrahlasHut when sectionIndex == 0 && index == 2:
                case LocationID.GroveDiggingSpot:
                case LocationID.Dam:
                case LocationID.MiniMoldormCave when index == 2:
                case LocationID.PyramidLedge:
                case LocationID.HypeCave when index == 2:
                case LocationID.DiggingGame:
                case LocationID.ZoraArea when sectionIndex == 0:
                case LocationID.DesertLedge:
                case LocationID.BumperCave:
                case LocationID.LakeHyliaIsland:
                case LocationID.SpectacleRock:
                case LocationID.ParadoxCave when index == 2:
                case LocationID.HookshotCave when sectionIndex == 1 && index == 2:
                case LocationID.FloatingIsland:
                    {
                        return 64;
                    }
                case LocationID.LumberjackCave:
                case LocationID.BottleVendor:
                case LocationID.ForestHideout:
                case LocationID.BombosTablet:
                case LocationID.ZoraArea:
                case LocationID.GraveyardLedge:
                case LocationID.CheckerboardCave:
                    {
                        return 2;
                    }
                case LocationID.BlindsHouse when sectionIndex == 0 && index == 0:
                case LocationID.TheWell when sectionIndex == 0 && index == 0:
                case LocationID.WitchsHut:
                case LocationID.SahasrahlasHut:
                case LocationID.MiniMoldormCave when index == 1:
                case LocationID.FatFairy:
                case LocationID.HypeCave when index == 1:
                case LocationID.WaterfallFairy:
                case LocationID.Catfish:
                case LocationID.MireShack:
                case LocationID.ParadoxCave when index == 1:
                case LocationID.SuperBunnyCave:
                case LocationID.HookshotCave when sectionIndex == 1 && index == 1:
                    {
                        return 32;
                    }
                case LocationID.BlindsHouse when sectionIndex == 0 && index == 2:
                case LocationID.TheWell when sectionIndex == 0 && index == 2:
                case LocationID.MagicBat:
                case LocationID.Library:
                case LocationID.MiniMoldormCave:
                case LocationID.HypeCave:
                case LocationID.ParadoxCave when index == 3:
                case LocationID.HookshotCave:
                    {
                        return 128;
                    }
                case LocationID.BlindsHouse:
                case LocationID.TheWell:
                case LocationID.CastleSecret:
                case LocationID.Hobo:
                case LocationID.OldMan:
                case LocationID.EtherTablet:
                case LocationID.ParadoxCave:
                    {
                        return 1;
                    }
                case LocationID.HauntedGrove:
                case LocationID.AgahnimTower when sectionIndex == 1:
                case LocationID.EasternPalace when sectionIndex == 1:
                case LocationID.DesertPalace when sectionIndex == 1:
                case LocationID.TowerOfHera when sectionIndex == 1:
                case LocationID.PalaceOfDarkness when sectionIndex == 1:
                case LocationID.SwampPalace when sectionIndex == 1:
                case LocationID.SkullWoods when sectionIndex == 1:
                case LocationID.ThievesTown when sectionIndex == 1:
                case LocationID.IcePalace when sectionIndex == 1:
                case LocationID.MiseryMire when sectionIndex == 1:
                case LocationID.TurtleRock when sectionIndex == 1:
                case LocationID.GanonsTower when sectionIndex == 1:
                    {
                        return 8;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(id));
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
        private static IAutoTrackValue GetItemAddressValue(ItemType type)
        {
            return new AutoTrackAddressValue(
                GetMemoryAddress(GetItemMemorySegment(type), GetItemMemoryIndex(type)),
                GetItemMaximum(type), GetItemAdjustment(type));
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
        private static IAutoTrackValue GetItemAddressBool(ItemType type, int index = 0)
        {
            return new AutoTrackAddressBool(
                GetMemoryAddress(
                    GetItemMemorySegment(type, index), GetItemMemoryIndex(type, index)),
                GetItemComparison(type), GetItemTrueValue(type, index));
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
        private static IAutoTrackValue GetItemFlagBool(ItemType type, int index = 0)
        {
            return new AutoTrackFlagBool(
                new MemoryFlag(
                    GetMemoryAddress(
                        GetItemMemorySegment(type, index), GetItemMemoryIndex(type, index)),
                    GetItemFlag(type, index)), GetItemTrueValue(type, index));
        }

        /// <summary>
        /// Returns the autotracking value for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID of the section.
        /// </param>
        /// <param name="sectionIndex">
        /// The index of the section.
        /// </param>
        /// <param name="index">
        /// The index of the memory address.
        /// </param>
        /// <returns>
        /// The autotracking value for the specified section.
        /// </returns>
        private static IAutoTrackValue GetSectionFlagBool(
            LocationID id, int sectionIndex, int index = 0)
        {
            return new AutoTrackFlagBool(
                new MemoryFlag(
                    GetMemoryAddress(
                        GetSectionMemorySegment(id, sectionIndex),
                        GetSectionMemoryIndex(id, sectionIndex, index)),
                    GetSectionFlag(id, sectionIndex, index)), 1);
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
        private static List<IAutoTrackValue> GetItemValues(ItemType type)
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
                            values.Add(GetItemFlagBool(type, i));
                        }
                        break;
                    case ItemType.Arrows:
                    case ItemType.Bottle:
                        {
                            values.Add(GetItemAddressBool(type, i));
                        }
                        break;
                }
            }

            return values;
        }

        /// <summary>
        /// Returns the list of autotracking values for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID for the section.
        /// </param>
        /// <param name="sectionIndex">
        /// The index of the section.
        /// </param>
        /// <returns>
        /// The list of autotracking values for the specified section.
        /// </returns>
        private static List<IAutoTrackValue> GetSectionValues(LocationID id, int sectionIndex)
        {
            var values = new List<IAutoTrackValue>();
            int count;

            switch (id)
            {
                case LocationID.BlindsHouse when sectionIndex == 0:
                case LocationID.TheWell when sectionIndex == 0:
                    {
                        count = 4;
                    }
                    break;
                case LocationID.SahasrahlasHut when sectionIndex == 0:
                case LocationID.HookshotCave when sectionIndex == 1:
                    {
                        count = 3;
                    }
                    break;
                case LocationID.MiniMoldormCave:
                case LocationID.HypeCave:
                case LocationID.ParadoxCave when sectionIndex == 1:
                    {
                        count = 5;
                    }
                    break;
                case LocationID.LinksHouse:
                case LocationID.FatFairy:
                case LocationID.WaterfallFairy:
                case LocationID.ParadoxCave when sectionIndex == 0:
                case LocationID.SuperBunnyCave:
                case LocationID.MireShack:
                    {
                        count = 2;
                    }
                    break;
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(id));
                    }
            }

            for (int i = 0; i < count; i++)
            {
                values.Add(GetSectionFlagBool(id, sectionIndex, i));
            }

            return values;
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
        private static IAutoTrackValue GetItemMultipleOverride(ItemType type)
        {
            return new AutoTrackMultipleOverride(GetItemValues(type));
        }

        /// <summary>
        /// Returns the autotracking override value for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID of the section.
        /// </param>
        /// <param name="sectionIndex">
        /// The index of the section.
        /// </param>
        /// <returns>
        /// The autotracking override value for the specified section.
        /// </returns>
        private static IAutoTrackValue GetSectionMultipleOverride(LocationID id, int sectionIndex)
        {
            return new AutoTrackMultipleOverride(GetSectionValues(id, sectionIndex));
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
        private static IAutoTrackValue GetItemMultipleSum(ItemType type)
        {
            return new AutoTrackMultipleSum(GetItemValues(type));
        }

        /// <summary>
        /// Returns the autotracking sum value for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID of the section.
        /// </param>
        /// <param name="sectionIndex">
        /// The index of the section.
        /// </param>
        /// <returns>
        /// The autotracking sum value for the specified section.
        /// </returns>
        private static IAutoTrackValue GetSectionMultipleSum(LocationID id, int sectionIndex)
        {
            return new AutoTrackMultipleSum(GetSectionValues(id, sectionIndex));
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
        public static IAutoTrackValue GetItemAutoTrackValue(ItemType type)
        {
            switch (type)
            {
                case ItemType.Sword:
                case ItemType.Shield:
                case ItemType.Mail:
                case ItemType.Gloves:
                    {
                        return GetItemAddressValue(type);
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
                        return GetItemAddressBool(type);
                    }
                case ItemType.Arrows:
                case ItemType.BigBomb:
                case ItemType.Mushroom:
                case ItemType.Flute:
                    {
                        return GetItemMultipleOverride(type);
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
                        return GetItemFlagBool(type);
                    }
                case ItemType.Bottle:
                    {
                        return GetItemMultipleSum(type);
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        /// <summary>
        /// Returns the autotracking value for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID of the section.
        /// </param>
        /// <param name="sectionIndex">
        /// The index of the section.
        /// </param>
        /// <returns>
        /// The autotracking value for the specified section.
        /// </returns>
        public static IAutoTrackValue GetSectionAutoTrackValue(LocationID id, int sectionIndex)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    {
                        return GetSectionMultipleOverride(id, sectionIndex);
                    }
                case LocationID.Pedestal:
                case LocationID.LumberjackCave:
                case LocationID.BlindsHouse when sectionIndex == 1:
                case LocationID.TheWell when sectionIndex == 1:
                case LocationID.BottleVendor:
                case LocationID.ChickenHouse:
                case LocationID.Tavern:
                case LocationID.SickKid:
                case LocationID.MagicBat:
                case LocationID.RaceGame:
                case LocationID.Library:
                case LocationID.MushroomSpot:
                case LocationID.ForestHideout:
                case LocationID.CastleSecret:
                case LocationID.WitchsHut:
                case LocationID.SahasrahlasHut when sectionIndex == 1:
                case LocationID.BonkRocks:
                case LocationID.KingsTomb:
                case LocationID.AginahsCave:
                case LocationID.GroveDiggingSpot:
                case LocationID.Dam:
                case LocationID.IceRodCave:
                case LocationID.Hobo:
                case LocationID.PyramidLedge:
                case LocationID.HauntedGrove:
                case LocationID.BombosTablet:
                case LocationID.SouthOfGrove:
                case LocationID.DiggingGame:
                case LocationID.ZoraArea:
                case LocationID.Catfish:
                case LocationID.GraveyardLedge:
                case LocationID.DesertLedge:
                case LocationID.CShapedHouse:
                case LocationID.TreasureGame:
                case LocationID.BombableShack:
                case LocationID.Blacksmith:
                case LocationID.PurpleChest:
                case LocationID.HammerPegs:
                case LocationID.BumperCave:
                case LocationID.LakeHyliaIsland:
                case LocationID.CheckerboardCave:
                case LocationID.OldMan:
                case LocationID.SpectacleRock:
                case LocationID.EtherTablet:
                case LocationID.SpikeCave:
                case LocationID.SpiralCave:
                case LocationID.HookshotCave when sectionIndex == 0:
                case LocationID.FloatingIsland:
                case LocationID.MimicCave:
                case LocationID.AgahnimTower when sectionIndex == 1:
                case LocationID.EasternPalace when sectionIndex == 1:
                case LocationID.DesertPalace when sectionIndex == 1:
                case LocationID.TowerOfHera when sectionIndex == 1:
                case LocationID.PalaceOfDarkness when sectionIndex == 1:
                case LocationID.SwampPalace when sectionIndex == 1:
                case LocationID.SkullWoods when sectionIndex == 1:
                case LocationID.ThievesTown when sectionIndex == 1:
                case LocationID.IcePalace when sectionIndex == 1:
                case LocationID.MiseryMire when sectionIndex == 1:
                case LocationID.TurtleRock when sectionIndex == 1:
                    {
                        return GetSectionFlagBool(id, sectionIndex);
                    }
                case LocationID.BlindsHouse:
                case LocationID.TheWell:
                case LocationID.SahasrahlasHut:
                case LocationID.MiniMoldormCave:
                case LocationID.FatFairy:
                case LocationID.HypeCave:
                case LocationID.WaterfallFairy:
                case LocationID.MireShack:
                case LocationID.ParadoxCave:
                case LocationID.SuperBunnyCave:
                case LocationID.HookshotCave:
                    {
                        return GetSectionMultipleSum(id, sectionIndex);
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
