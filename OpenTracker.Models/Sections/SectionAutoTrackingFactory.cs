using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.AutotrackValues;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class for creating section auto tracking.
    /// </summary>
    public static class SectionAutoTrackingFactory
    {
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
        private static MemorySegmentType GetMemorySegment(LocationID id, int sectionIndex = 0)
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
        private static int GetMemoryIndex(LocationID id, int sectionIndex = 0, int index = 0)
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
        private static byte GetFlag(LocationID id, int sectionIndex = 0, int index = 0)
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
                case LocationID.SpectacleRock when sectionIndex == 1:
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
        private static IAutoTrackValue GetFlagBool(LocationID id, int sectionIndex, int index = 0)
        {
            return new AutoTrackFlagBool(
                new MemoryFlag(
                    AutoTracker.GetMemoryAddress(
                        GetMemorySegment(id, sectionIndex),
                        GetMemoryIndex(id, sectionIndex, index)),
                    GetFlag(id, sectionIndex, index)), 1);
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
        private static List<IAutoTrackValue> GetValues(LocationID id, int sectionIndex)
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
                values.Add(GetFlagBool(id, sectionIndex, i));
            }

            return values;
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
        private static IAutoTrackValue GetMultipleOverride(LocationID id, int sectionIndex)
        {
            return new AutoTrackMultipleOverride(GetValues(id, sectionIndex));
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
        private static IAutoTrackValue GetMultipleSum(LocationID id, int sectionIndex)
        {
            return new AutoTrackMultipleSum(GetValues(id, sectionIndex));
        }

        /// <summary>
        /// Returns the autotracking value for the specified dungeon section.
        /// </summary>
        /// <param name="id">
        /// The location ID of the dungeon.
        /// </param>
        /// <returns>
        /// The autotracking value for the specified dungeon section.
        /// </returns>
        private static IAutoTrackValue GetDungeonValue(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x24),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x22),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x22),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x22),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xe4),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xe2),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x100),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x64),
                                            0x10), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.HCMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.HCSmallKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.AgahnimTower:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1c0),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1a0),
                                            0x10), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ATSmallKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.EasternPalace:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x172),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x154),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x150),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x152),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x170),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x191),
                                            0x8), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.EPMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.EPCompass]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.EPBigKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.DesertPalace:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xe7),
                                            0x4), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xe6),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xe8),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x10a),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xea),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x67),
                                            0x8), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPCompass]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPSmallKey]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPBigKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.TowerOfHera:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x10f),
                                            0x4), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xee),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x10e),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x4e),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x4e),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf),
                                            0x8), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ToHMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ToHCompass]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ToHSmallKey]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ToHBigKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x12),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x54),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x74),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x14),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x56),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x54),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x34),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x32),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x32),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x34),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xd4),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xd4),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x34),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xb5),
                                            0x8), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.PoDMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.PoDCompass]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.PoDSmallKey]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.PoDBigKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.SwampPalace:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x50),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x6e),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x6c),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x8c),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x68),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x6a),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xec),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xec),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xcc),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xd),
                                            0x8), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SPMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SPCompass]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SPSmallKey]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SPBigKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.SkullWoods:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xae),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xb2),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xb0),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xb0),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xae),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xce),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xd0),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x53),
                                            0x8), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWCompass]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWSmallKey]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWBigKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.ThievesTown:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1b6),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x196),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1b8),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1b6),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xca),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x8a),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x88),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x159),
                                            0x8), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTCompass]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTSmallKey]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTBigKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.IcePalace:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x5c),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x3e),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x7e),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xbe),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xfc),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x15c),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x13c),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1bd),
                                            0x8), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPCompass]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPSmallKey]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPBigKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.MiseryMire:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x182),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1a2),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x184),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x186),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x186),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x166),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x144),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x121),
                                            0x8), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMCompass]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMSmallKey]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMBigKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.TurtleRock:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x163),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x163),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1ac),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x16c),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x28),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x48),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x8),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1aa),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1aa),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1aa),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x1aa),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x149),
                                            0x8), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRCompass]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRSmallKey]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRBigKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                case LocationID.GanonsTower:
                    {
                        return new AutoTrackConditionalValue(
                            RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                            new AutoTrackMultipleDifference(
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x119),
                                            0x4), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf6),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf6),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf6),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf6),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x116),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xfa),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf8),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf8),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf8),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0xf8),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x118),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x118),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x11a),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x13a),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x13a),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x13a),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x13a),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x118),
                                            0x80), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x38),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x38),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x38),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x118),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x7a),
                                            0x10), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x7a),
                                            0x20), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x7a),
                                            0x40), 1),
                                    new AutoTrackFlagBool(
                                        new MemoryFlag(
                                            AutoTracker.GetMemoryAddress(MemorySegmentType.Room, 0x9a),
                                            0x10), 1)
                                }),
                                new AutoTrackMultipleSum(new List<IAutoTrackValue>
                                {
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTMap]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTCompass]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTSmallKey]),
                                        new AutoTrackStaticValue(0)),
                                    new AutoTrackConditionalValue(
                                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                        new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTBigKey]),
                                        new AutoTrackStaticValue(0))
                                })), null);
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(id));
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
        public static IAutoTrackValue GetAutoTrackValue(LocationID id, int sectionIndex)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    {
                        return GetMultipleOverride(id, sectionIndex);
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
                        return GetFlagBool(id, sectionIndex);
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
                        return GetMultipleSum(id, sectionIndex);
                    }
                case LocationID.HyruleCastle:
                case LocationID.AgahnimTower:
                case LocationID.EasternPalace:
                case LocationID.DesertPalace:
                case LocationID.TowerOfHera:
                case LocationID.PalaceOfDarkness:
                case LocationID.SwampPalace:
                case LocationID.SkullWoods:
                case LocationID.ThievesTown:
                case LocationID.IcePalace:
                case LocationID.MiseryMire:
                case LocationID.TurtleRock:
                case LocationID.GanonsTower when sectionIndex == 0:
                    {
                        return GetDungeonValue(id);
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
