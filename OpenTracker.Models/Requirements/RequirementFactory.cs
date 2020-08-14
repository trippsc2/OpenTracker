using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SequenceBreaks;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for creating Requirement classes.
    /// </summary>
    public static class RequirementFactory
    {
        /// <summary>
        /// Returns a static requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A static requirement.
        /// </returns>
        private static IRequirement GetStaticRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.NoRequirement => new StaticRequirement(AccessibilityLevel.Normal),
                RequirementType.Inspect => new StaticRequirement(AccessibilityLevel.Inspect),
                RequirementType.SequenceBreak => new StaticRequirement(AccessibilityLevel.SequenceBreak),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a world state requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A world state requirement.
        /// </returns>
        private static IRequirement GetWorldStateRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.WorldStateStandardOpen => new WorldStateRequirement(
                    WorldState.StandardOpen),
                RequirementType.WorldStateInverted => new WorldStateRequirement(
                    WorldState.Inverted),
                RequirementType.WorldStateRetro => new WorldStateRequirement(
                    WorldState.Retro),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a item placement requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A item placement requirement.
        /// </returns>
        private static IRequirement GetItemPlacementRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.ItemPlacementBasic => new ItemPlacementRequirement(
                    ItemPlacement.Basic),
                RequirementType.ItemPlacementAdvanced => new ItemPlacementRequirement(
                    ItemPlacement.Advanced),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a dungeon item shuffle requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A dungeon item shuffle requirement.
        /// </returns>
        private static IRequirement GetDungeonItemShuffleRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.DungeonItemShuffleStandard => new DungeonItemShuffleRequirement(
                    DungeonItemShuffle.Standard),
                RequirementType.DungeonItemShuffleMapsCompasses => new DungeonItemShuffleRequirement(
                    DungeonItemShuffle.MapsCompasses),
                RequirementType.DungeonItemShuffleMapsCompassesSmallKeys => new DungeonItemShuffleRequirement(
                    DungeonItemShuffle.MapsCompassesSmallKeys),
                RequirementType.DungeonItemShuffleKeysanity => new DungeonItemShuffleRequirement(
                    DungeonItemShuffle.Keysanity),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a boss shuffle requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A boss shuffle requirement.
        /// </returns>
        private static IRequirement GetBossShuffleRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.BossShuffleOff => new BossShuffleRequirement(false),
                RequirementType.BossShuffleOn => new BossShuffleRequirement(true),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a enemy shuffle requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A enemy shuffle requirement.
        /// </returns>
        private static IRequirement GetEnemyShuffleRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.EnemyShuffleOff => new EnemyShuffleRequirement(false),
                RequirementType.EnemyShuffleOn => new EnemyShuffleRequirement(true),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a entrance shuffle requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A entrance shuffle requirement.
        /// </returns>
        private static IRequirement GetEntranceShuffleRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.EntranceShuffleOff => new EntranceShuffleRequirement(false),
                RequirementType.EntranceShuffleOn => new EntranceShuffleRequirement(true),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a guaranteed boss items requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A guaranteed boss items requirement.
        /// </returns>
        private static IRequirement GetGuaranteedBossItemsRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.GuaranteedBossItemsOff =>
                    new GuaranteedBossItemsRequirement(false),
                RequirementType.GuaranteedBossItemsOn =>
                    new GuaranteedBossItemsRequirement(true),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns an item exact amount requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// An item exact amount requirement.
        /// </returns>
        private static IRequirement GetItemExactRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.Swordless => new ItemExactRequirement(ItemDictionary.Instance[ItemType.Sword], 0),
                RequirementType.Mushroom => new ItemExactRequirement(ItemDictionary.Instance[ItemType.Mushroom], 1),
                RequirementType.BombosMM => new ItemExactRequirement(ItemDictionary.Instance[ItemType.BombosDungeons], 1),
                RequirementType.BombosTR => new ItemExactRequirement(ItemDictionary.Instance[ItemType.BombosDungeons], 2),
                RequirementType.BombosBoth => new ItemExactRequirement(ItemDictionary.Instance[ItemType.BombosDungeons], 3),
                RequirementType.EtherMM => new ItemExactRequirement(ItemDictionary.Instance[ItemType.EtherDungeons], 1),
                RequirementType.EtherTR => new ItemExactRequirement(ItemDictionary.Instance[ItemType.EtherDungeons], 2),
                RequirementType.EtherBoth => new ItemExactRequirement(ItemDictionary.Instance[ItemType.EtherDungeons], 3),
                RequirementType.QuakeMM => new ItemExactRequirement(ItemDictionary.Instance[ItemType.QuakeDungeons], 1),
                RequirementType.QuakeTR => new ItemExactRequirement(ItemDictionary.Instance[ItemType.QuakeDungeons], 2),
                RequirementType.QuakeBoth => new ItemExactRequirement(ItemDictionary.Instance[ItemType.QuakeDungeons], 3),
                RequirementType.NoFlippers => new ItemExactRequirement(ItemDictionary.Instance[ItemType.Flippers], 0),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns an item requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// An item requirement.
        /// </returns>
        private static IRequirement GetItemRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.Sword1 => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Sword], 2),
                RequirementType.Sword2 => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Sword], 3),
                RequirementType.Sword3 => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Sword], 4),
                RequirementType.Shield3 => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Shield], 3),
                RequirementType.Aga1 => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Aga1]),
                RequirementType.Bow => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Bow]),
                RequirementType.Boomerang => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Boomerang]),
                RequirementType.RedBoomerang => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.RedBoomerang]),
                RequirementType.Hookshot => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Hookshot]),
                RequirementType.Powder => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Powder]),
                RequirementType.Boots => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Boots]),
                RequirementType.FireRod => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.FireRod]),
                RequirementType.IceRod => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IceRod]),
                RequirementType.Bombos => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Bombos]),
                RequirementType.Ether => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Ether]),
                RequirementType.Quake => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Quake]),
                RequirementType.Gloves1 => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Gloves]),
                RequirementType.Gloves2 => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Gloves], 2),
                RequirementType.Lamp => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Lamp]),
                RequirementType.Hammer => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Hammer]),
                RequirementType.Flute => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Flute]),
                RequirementType.Net => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Net]),
                RequirementType.Book => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Book]),
                RequirementType.Shovel => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Shovel]),
                RequirementType.Flippers => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Flippers]),
                RequirementType.Bottle => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Bottle]),
                RequirementType.CaneOfSomaria => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.CaneOfSomaria]),
                RequirementType.CaneOfByrna => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.CaneOfByrna]),
                RequirementType.Cape => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Cape]),
                RequirementType.Mirror => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Mirror]),
                RequirementType.HalfMagic => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HalfMagic]),
                RequirementType.MoonPearl => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MoonPearl]),
                RequirementType.Aga2 => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Aga2]),
                RequirementType.RedCrystal => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.RedCrystal], 2),
                RequirementType.Pendant => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.Pendant], 2),
                RequirementType.GreenPendant => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GreenPendant]),
                RequirementType.TRSmallKey2 => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRSmallKey], 2),
                RequirementType.LightWorldAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LightWorldAccess]),
                RequirementType.DeathMountainEntryAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainEntryAccess]),
                RequirementType.DeathMountainExitAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainExitAccess]),
                RequirementType.GrassHouseAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GrassHouseAccess]),
                RequirementType.BombHutAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BombHutAccess]),
                RequirementType.RaceGameLedgeAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.RaceGameLedgeAccess]),
                RequirementType.SouthOfGroveLedgeAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SouthOfGroveLedgeAccess]),
                RequirementType.DesertLedgeAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DesertLedgeAccess]),
                RequirementType.DesertPalaceBackEntranceAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DesertPalaceBackEntranceAccess]),
                RequirementType.CheckerboardLedgeAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.CheckerboardLedgeAccess]),
                RequirementType.LWGraveyardLedgeAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWGraveyardLedgeAccess]),
                RequirementType.LWKingsTombAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWKingsTombAccess]),
                RequirementType.HyruleCastleTopAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HyruleCastleTopAccess]),
                RequirementType.WaterfallFairyAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.WaterfallFairyAccess]),
                RequirementType.LWWitchAreaAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWWitchAreaAccess]),
                RequirementType.LakeHyliaFairyIslandAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LakeHyliaFairyIslandAccess]),
                RequirementType.DeathMountainWestBottomAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainWestBottomAccess]),
                RequirementType.DeathMountainWestTopAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainWestTopAccess]),
                RequirementType.DeathMountainEastBottomAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainEastBottomAccess]),
                RequirementType.DeathMountainEastBottomConnectorAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainEastBottomConnectorAccess]),
                RequirementType.DeathMountainEastTopConnectorAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainEastTopConnectorAccess]),
                RequirementType.SpiralCaveAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SpiralCaveAccess]),
                RequirementType.MimicCaveAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MimicCaveAccess]),
                RequirementType.DeathMountainEastTopAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainEastTopAccess]),
                RequirementType.DarkWorldWestAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkWorldWestAccess]),
                RequirementType.BumperCaveAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BumperCaveAccess]),
                RequirementType.BumperCaveTopAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BumperCaveTopAccess]),
                RequirementType.HammerHouseAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HammerHouseAccess]),
                RequirementType.HammerPegsAreaAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HammerPegsAreaAccess]),
                RequirementType.DarkWorldSouthAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkWorldSouthAccess]),
                RequirementType.MireAreaAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MireAreaAccess]),
                RequirementType.DWWitchAreaAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWWitchAreaAccess]),
                RequirementType.DarkWorldEastAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkWorldEastAccess]),
                RequirementType.IcePalaceEntranceAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IcePalaceEntranceAccess]),
                RequirementType.DarkWorldSouthEastAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkWorldSouthEastAccess]),
                RequirementType.DarkDeathMountainWestBottomAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkDeathMountainWestBottomAccess]),
                RequirementType.DarkDeathMountainTopAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkDeathMountainTopAccess]),
                RequirementType.DWFloatingIslandAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWFloatingIslandAccess]),
                RequirementType.DarkDeathMountainEastBottomAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkDeathMountainEastBottomAccess]),
                RequirementType.TurtleRockTunnelAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TurtleRockTunnelAccess]),
                RequirementType.TurtleRockSafetyDoorAccess => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TurtleRockSafetyDoorAccess]),
                RequirementType.LightWorldTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LightWorldTest]),
                RequirementType.LumberjackTreeActiveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LumberjackTreeActiveTest]),
                RequirementType.LumberjackCaveEntranceTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LumberjackCaveEntranceTest]),
                RequirementType.ForestHideoutTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ForestHideoutTest]),
                RequirementType.DeathMountainEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainEntryTest]),
                RequirementType.DeathMountainExitTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainExitTest]),
                RequirementType.LWKakarikoPortalTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWKakarikoPortalTest]),
                RequirementType.GrassHouseTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GrassHouseTest]),
                RequirementType.BombHutTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BombHutTest]),
                RequirementType.MagicBatLedgeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MagicBatLedgeTest]),
                RequirementType.RaceGameLedgeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.RaceGameLedgeTest]),
                RequirementType.SouthOfGroveLedgeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SouthOfGroveLedgeTest]),
                RequirementType.DesertLedgeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DesertLedgeTest]),
                RequirementType.DesertPalaceBackEntranceTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DesertPalaceBackEntranceTest]),
                RequirementType.CheckerboardLedgeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.CheckerboardLedgeTest]),
                RequirementType.CheckerboardCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.CheckerboardCaveTest]),
                RequirementType.DesertPalaceFrontEntranceTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DesertPalaceFrontEntranceTest]),
                RequirementType.BombosTabletLedgeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BombosTabletLedgeTest]),
                RequirementType.RupeeCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.RupeeCaveTest]),
                RequirementType.LWMirePortalTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWMirePortalTest]),
                RequirementType.NorthBonkRocksTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.NorthBonkRocksTest]),
                RequirementType.LWGraveyardTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWGraveyardTest]),
                RequirementType.EscapeGraveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.EscapeGraveTest]),
                RequirementType.LWGraveyardLedgeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWGraveyardLedgeTest]),
                RequirementType.LWKingsTombTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWKingsTombTest]),
                RequirementType.KingsTombGraveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.KingsTombGraveTest]),
                RequirementType.HyruleCastleTopTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HyruleCastleTopTest]),
                RequirementType.AgahnimTowerEntranceTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.AgahnimTowerEntranceTest]),
                RequirementType.GanonHoleTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GanonHoleTest]),
                RequirementType.GanonHoleBackTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GanonHoleBackTest]),
                RequirementType.CastleSecretFrontTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.CastleSecretFrontTest]),
                RequirementType.CastleSecretBackTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.CastleSecretBackTest]),
                RequirementType.CentralBonkRocksTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.CentralBonkRocksTest]),
                RequirementType.LWSouthPortalTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWSouthPortalTest]),
                RequirementType.HypeFairyCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HypeFairyCaveTest]),
                RequirementType.MiniMoldormCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MiniMoldormCaveTest]),
                RequirementType.ZoraTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ZoraTest]),
                RequirementType.WaterfallFairyTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.WaterfallFairyTest]),
                RequirementType.LWWitchAreaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWWitchAreaTest]),
                RequirementType.LWEastPortalTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWEastPortalTest]),
                RequirementType.LWLakeHyliaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWLakeHyliaTest]),
                RequirementType.LWLakeHyliaWaterWalkTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWLakeHyliaWaterWalkTest]),
                RequirementType.LakeHyliaIslandTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LakeHyliaIslandTest]),
                RequirementType.LakeHyliaFairyIslandTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LakeHyliaFairyIslandTest]),
                RequirementType.IceRodCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IceRodCaveTest]),
                RequirementType.IceFairyCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IceFairyCaveTest]),
                RequirementType.DeathMountainWestBottomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainWestBottomTest]),
                RequirementType.SpectacleRockTopTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SpectacleRockTopTest]),
                RequirementType.DeathMountainWestTopTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainWestTopTest]),
                RequirementType.DeathMountainEastBottomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainEastBottomTest]),
                RequirementType.DeathMountainEastBottomConnectorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainEastBottomConnectorTest]),
                RequirementType.DeathMountainEastTopConnectorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainEastTopConnectorTest]),
                RequirementType.SpiralCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SpiralCaveTest]),
                RequirementType.MimicCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MimicCaveTest]),
                RequirementType.DeathMountainEastTopTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DeathMountainEastTopTest]),
                RequirementType.LWFloatingIslandTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWFloatingIslandTest]),
                RequirementType.LWTurtleRockTopTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.LWTurtleRockTopTest]),
                RequirementType.DWKakarikoPortalTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWKakarikoPortalTest]),
                RequirementType.DarkWorldWestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkWorldWestTest]),
                RequirementType.SkullWoodsBackEntranceTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SkullWoodsBackEntranceTest]),
                RequirementType.BumperCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BumperCaveTest]),
                RequirementType.BumperCaveTopTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BumperCaveTopTest]),
                RequirementType.ThievesTownEntranceTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ThievesTownEntranceTest]),
                RequirementType.BombableShackTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BombableShackTest]),
                RequirementType.HammerHouseTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HammerHouseTest]),
                RequirementType.HammerPegsAreaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HammerPegsAreaTest]),
                RequirementType.HammerPegsTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HammerPegsTest]),
                RequirementType.BlacksmithPrisonTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BlacksmithPrisonTest]),
                RequirementType.DarkWorldSouthTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkWorldSouthTest]),
                RequirementType.DWCentralBonkRocksTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWCentralBonkRocksTest]),
                RequirementType.BuyBigBombTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BuyBigBombTest]),
                RequirementType.BigBombToLightWorldTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BigBombToLightWorldTest]),
                RequirementType.BigBombToDWLakeHyliaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BigBombToDWLakeHyliaTest]),
                RequirementType.BigBombToWallTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.BigBombToWallTest]),
                RequirementType.DWSouthPortalTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWSouthPortalTest]),
                RequirementType.HypeCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HypeCaveTest]),
                RequirementType.MireAreaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MireAreaTest]),
                RequirementType.DWMirePortalTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWMirePortalTest]),
                RequirementType.MiseryMireEntranceTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MiseryMireEntranceTest]),
                RequirementType.DWGraveyardTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWGraveyardTest]),
                RequirementType.DWGraveyardLedgeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWGraveyardLedgeTest]),
                RequirementType.DWWitchAreaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWWitchAreaTest]),
                RequirementType.CatfishTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.CatfishTest]),
                RequirementType.DarkWorldEastTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkWorldEastTest]),
                RequirementType.FatFairyTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.FatFairyTest]),
                RequirementType.PalaceOfDarknessEntranceTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PalaceOfDarknessEntranceTest]),
                RequirementType.DWEastPortalTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWEastPortalTest]),
                RequirementType.DWLakeHyliaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWLakeHyliaTest]),
                RequirementType.DWLakeHyliaWaterWalkTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWLakeHyliaWaterWalkTest]),
                RequirementType.IcePalaceEntranceTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IcePalaceEntranceTest]),
                RequirementType.DarkWorldSouthEastTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkWorldSouthEastTest]),
                RequirementType.DWIceRodCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWIceRodCaveTest]),
                RequirementType.DWIceRodRockTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWIceRodRockTest]),
                RequirementType.DarkDeathMountainWestBottomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkDeathMountainWestBottomTest]),
                RequirementType.DarkDeathMountainTopTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkDeathMountainTopTest]),
                RequirementType.GanonsTowerEntranceTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GanonsTowerEntranceTest]),
                RequirementType.DWFloatingIslandTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWFloatingIslandTest]),
                RequirementType.HookshotCaveTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HookshotCaveTest]),
                RequirementType.DWTurtleRockTopTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DWTurtleRockTopTest]),
                RequirementType.TurtleRockFrontEntranceTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TurtleRockFrontEntranceTest]),
                RequirementType.DarkDeathMountainEastBottomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkDeathMountainEastBottomTest]),
                RequirementType.DarkDeathMountainEastBottomConnectorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DarkDeathMountainEastBottomConnectorTest]),
                RequirementType.TurtleRockTunnelTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TurtleRockTunnelTest]),
                RequirementType.TurtleRockSafetyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TurtleRockSafetyDoorTest]),
                RequirementType.HCSanctuaryEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCSanctuaryEntryTest]),
                RequirementType.HCFrontEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCFrontEntryTest]),
                RequirementType.HCBackEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCBackEntryTest]),
                RequirementType.ATEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ATEntryTest]),
                RequirementType.EPEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.EPEntryTest]),
                RequirementType.DPFrontEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DPFrontEntryTest]),
                RequirementType.DPLeftEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DPLeftEntryTest]),
                RequirementType.DPBackEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DPBackEntryTest]),
                RequirementType.ToHEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ToHEntryTest]),
                RequirementType.PoDEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDEntryTest]),
                RequirementType.SPEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPEntryTest]),
                RequirementType.SWFrontEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWFrontEntryTest]),
                RequirementType.SWFrontLeftDropEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWFrontLeftDropEntryTest]),
                RequirementType.SWPinballRoomEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWPinballRoomEntryTest]),
                RequirementType.SWFrontTopDropEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWFrontTopDropEntryTest]),
                RequirementType.SWFrontBackConnectorEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWFrontBackConnectorEntryTest]),
                RequirementType.SWBackEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWBackEntryTest]),
                RequirementType.TTEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TTEntryTest]),
                RequirementType.IPEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPEntryTest]),
                RequirementType.MMEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMEntryTest]),
                RequirementType.TRFrontEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRFrontEntryTest]),
                RequirementType.TRFrontToKeyDoorsTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRFrontToKeyDoorsTest]),
                RequirementType.TRKeyDoorsToMiddleExitTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRKeyDoorsToMiddleExitTest]),
                RequirementType.TRMiddleEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRMiddleEntryTest]),
                RequirementType.TRBackEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRBackEntryTest]),
                RequirementType.GTEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GTEntryTest]),
                RequirementType.HCSanctuaryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCSanctuaryTest]),
                RequirementType.HCFrontTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCFrontTest]),
                RequirementType.HCPastEscapeFirstKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCPastEscapeFirstKeyDoorTest]),
                RequirementType.HCPastEscapeSecondKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCPastEscapeSecondKeyDoorTest]),
                RequirementType.HCDarkRoomFrontTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCDarkRoomFrontTest]),
                RequirementType.HCPastDarkCrossKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCPastDarkCrossKeyDoorTest]),
                RequirementType.HCPastSewerRatRoomKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCPastSewerRatRoomKeyDoorTest]),
                RequirementType.HCDarkRoomBackTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCDarkRoomBackTest]),
                RequirementType.HCBackTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.HCBackTest]),
                RequirementType.ATTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ATTest]),
                RequirementType.ATDarkMazeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ATDarkMazeTest]),
                RequirementType.ATPastFirstKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ATPastFirstKeyDoorTest]),
                RequirementType.ATPastSecondKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ATPastSecondKeyDoorTest]),
                RequirementType.ATPastThirdKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ATPastThirdKeyDoorTest]),
                RequirementType.ATPastFourthKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ATPastFourthKeyDoorTest]),
                RequirementType.ATBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ATBossRoomTest]),
                RequirementType.ATBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ATBossTest]),
                RequirementType.EPTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.EPTest]),
                RequirementType.EPBigChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.EPBigChestTest]),
                RequirementType.EPRightDarkRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.EPRightDarkRoomTest]),
                RequirementType.EPPastRightKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.EPPastRightKeyDoorTest]),
                RequirementType.EPPastBigKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.EPPastBigKeyDoorTest]),
                RequirementType.EPBackDarkRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.EPBackDarkRoomTest]),
                RequirementType.EPPastBackKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.EPPastBackKeyDoorTest]),
                RequirementType.EPBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.EPBossRoomTest]),
                RequirementType.EPBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.EPBossTest]),
                RequirementType.DPFrontTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DPFrontTest]),
                RequirementType.DPTorchItemTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DPTorchItemTest]),
                RequirementType.DPBigChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DPBigChestTest]),
                RequirementType.DPPastRightKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DPPastRightKeyDoorTest]),
                RequirementType.DPBackTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DPBackTest]),
                RequirementType.DP2FTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DP2FTest]),
                RequirementType.DP2FPastFirstKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DP2FPastFirstKeyDoorTest]),
                RequirementType.DP2FPastSecondKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DP2FPastSecondKeyDoorTest]),
                RequirementType.DPPastFourTorchWallTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DPPastFourTorchWallTest]),
                RequirementType.DPBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DPBossRoomTest]),
                RequirementType.DPBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.DPBossTest]),
                RequirementType.ToHTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ToHTest]),
                RequirementType.ToHPastKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ToHPastKeyDoorTest]),
                RequirementType.ToHBasementTorchRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ToHBasementTorchRoomTest]),
                RequirementType.ToHPastBigKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ToHPastBigKeyDoorTest]),
                RequirementType.ToHBigChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ToHBigChestTest]),
                RequirementType.ToHBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.ToHBossTest]),
                RequirementType.PoDTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDTest]),
                RequirementType.PoDPastFirstRedGoriyaRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDPastFirstRedGoriyaRoomTest]),
                RequirementType.PoDLobbyArenaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDLobbyArenaTest]),
                RequirementType.PoDBigKeyChestAreaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDBigKeyChestAreaTest]),
                RequirementType.PoDPastCollapsingWalkwayKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDPastCollapsingWalkwayKeyDoorTest]),
                RequirementType.PoDDarkBasementTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDDarkBasementTest]),
                RequirementType.PoDHarmlessHellwayRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDHarmlessHellwayRoomTest]),
                RequirementType.PoDPastDarkMazeKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDPastDarkMazeKeyDoorTest]),
                RequirementType.PoDDarkMazeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDDarkMazeTest]),
                RequirementType.PoDBigChestLedgeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDBigChestLedgeTest]),
                RequirementType.PoDBigChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDBigChestTest]),
                RequirementType.PoDPastSecondRedGoriyaRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDPastSecondRedGoriyaRoomTest]),
                RequirementType.PoDPastBowStatueTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDPastBowStatueTest]),
                RequirementType.PoDBossAreaDarkRoomsTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDBossAreaDarkRoomsTest]),
                RequirementType.PoDPastHammerBlocksTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDPastHammerBlocksTest]),
                RequirementType.PoDPastBossAreaKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDPastBossAreaKeyDoorTest]),
                RequirementType.PoDBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDBossRoomTest]),
                RequirementType.PoDBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.PoDBossTest]),
                RequirementType.SPTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPTest]),
                RequirementType.SPAfterRiverTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPAfterRiverTest]),
                RequirementType.SPB1Test => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPB1Test]),
                RequirementType.SPB1PastFirstRightKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPB1PastFirstRightKeyDoorTest]),
                RequirementType.SPB1PastSecondRightKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPB1PastSecondRightKeyDoorTest]),
                RequirementType.SPB1PastRightHammerBlocksTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPB1PastRightHammerBlocksTest]),
                RequirementType.SPB1KeyLedgeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPB1KeyLedgeTest]),
                RequirementType.SPB1PastLeftKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPB1PastLeftKeyDoorTest]),
                RequirementType.SPBigChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPBigChestTest]),
                RequirementType.SPB1BackTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPB1BackTest]),
                RequirementType.SPB1PastBackFirstKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPB1PastBackFirstKeyDoorTest]),
                RequirementType.SPBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPBossRoomTest]),
                RequirementType.SPBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SPBossTest]),
                RequirementType.SWBigChestAreaBottomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWBigChestAreaBottomTest]),
                RequirementType.SWBigChestAreaTopTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWBigChestAreaTopTest]),
                RequirementType.SWBigChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWBigChestTest]),
                RequirementType.SWFrontLeftSideTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWFrontLeftSideTest]),
                RequirementType.SWFrontRightSideTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWFrontRightSideTest]),
                RequirementType.SWFrontBackConnectorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWFrontBackConnectorTest]),
                RequirementType.SWPastTheWorthlessKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWPastTheWorthlessKeyDoorTest]),
                RequirementType.SWBackTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWBackTest]),
                RequirementType.SWBackPastFirstKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWBackPastFirstKeyDoorTest]),
                RequirementType.SWBackPastFourTorchRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWBackPastFourTorchRoomTest]),
                RequirementType.SWBackPastCurtainsTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWBackPastCurtainsTest]),
                RequirementType.SWBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWBossRoomTest]),
                RequirementType.SWBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.SWBossTest]),
                RequirementType.TTTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TTTest]),
                RequirementType.TTPastBigKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TTPastBigKeyDoorTest]),
                RequirementType.TTPastFirstKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TTPastFirstKeyDoorTest]),
                RequirementType.TTPastSecondKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TTPastSecondKeyDoorTest]),
                RequirementType.TTPastBigChestRoomKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TTPastBigChestRoomKeyDoorTest]),
                RequirementType.TTPastHammerBlocksTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TTPastHammerBlocksTest]),
                RequirementType.TTBigChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TTBigChestTest]),
                RequirementType.TTBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TTBossRoomTest]),
                RequirementType.TTBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TTBossTest]),
                RequirementType.IPTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPTest]),
                RequirementType.IPPastEntranceFreezorRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPPastEntranceFreezorRoomTest]),
                RequirementType.IPB1LeftSideTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB1LeftSideTest]),
                RequirementType.IPB1RightSideTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB1RightSideTest]),
                RequirementType.IPB2LeftSideTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB2LeftSideTest]),
                RequirementType.IPB2PastKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB2PastKeyDoorTest]),
                RequirementType.IPB2PastHammerBlocksTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB2PastHammerBlocksTest]),
                RequirementType.IPB2PastLiftBlockTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB2PastLiftBlockTest]),
                RequirementType.IPSpikeRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPSpikeRoomTest]),
                RequirementType.IPB4RightSideTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB4RightSideTest]),
                RequirementType.IPB4IceRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB4IceRoomTest]),
                RequirementType.IPB4FreezorRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB4FreezorRoomTest]),
                RequirementType.IPFreezorChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPFreezorChestTest]),
                RequirementType.IPB4PastKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB4PastKeyDoorTest]),
                RequirementType.IPBigChestAreaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPBigChestAreaTest]),
                RequirementType.IPBigChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPBigChestTest]),
                RequirementType.IPB5Test => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB5Test]),
                RequirementType.IPB5PastBigKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB5PastBigKeyDoorTest]),
                RequirementType.IPB6Test => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB6Test]),
                RequirementType.IPB6PastKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB6PastKeyDoorTest]),
                RequirementType.IPB6PreBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB6PreBossRoomTest]),
                RequirementType.IPB6PastHammerBlocksTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB6PastHammerBlocksTest]),
                RequirementType.IPB6PastLiftBlockTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPB6PastLiftBlockTest]),
                RequirementType.IPBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.IPBossTest]),
                RequirementType.MMTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMTest]),
                RequirementType.MMPastEntranceGapTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMPastEntranceGapTest]),
                RequirementType.MMBigChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMBigChestTest]),
                RequirementType.MMB1TopSideTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMB1TopSideTest]),
                RequirementType.MMB1LobbyBeyondBlueBlocksTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMB1LobbyBeyondBlueBlocksTest]),
                RequirementType.MMB1RightSideBeyondBlueBlocksTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMB1RightSideBeyondBlueBlocksTest]),
                RequirementType.MMB1LeftSidePastFirstKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMB1LeftSidePastFirstKeyDoorTest]),
                RequirementType.MMB1LeftSidePastSecondKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMB1LeftSidePastSecondKeyDoorTest]),
                RequirementType.MMB1PastFourTorchRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMB1PastFourTorchRoomTest]),
                RequirementType.MMF1PastFourTorchRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMF1PastFourTorchRoomTest]),
                RequirementType.MMB1PastPortalBigKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMB1PastPortalBigKeyDoorTest]),
                RequirementType.MMB1PastBridgeBigKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMB1PastBridgeBigKeyDoorTest]),
                RequirementType.MMDarkRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMDarkRoomTest]),
                RequirementType.MMB2PastWorthlessKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMB2PastWorthlessKeyDoorTest]),
                RequirementType.MMB2PastCaneOfSomariaSwitchTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMB2PastCaneOfSomariaSwitchTest]),
                RequirementType.MMBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMBossRoomTest]),
                RequirementType.MMBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.MMBossTest]),
                RequirementType.TRFrontTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRFrontTest]),
                RequirementType.TRF1CompassChestAreaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRF1CompassChestAreaTest]),
                RequirementType.TRF1FourTorchRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRF1FourTorchRoomTest]),
                RequirementType.TRF1RollerRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRF1RollerRoomTest]),
                RequirementType.TRF1FirstKeyDoorAreaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRF1FirstKeyDoorAreaTest]),
                RequirementType.TRF1PastFirstKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRF1PastFirstKeyDoorTest]),
                RequirementType.TRF1PastSecondKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRF1PastSecondKeyDoorTest]),
                RequirementType.TRB1Test => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRB1Test]),
                RequirementType.TRB1PastBigKeyChestKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRB1PastBigKeyChestKeyDoorTest]),
                RequirementType.TRB1MiddleRightEntranceAreaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRB1MiddleRightEntranceAreaTest]),
                RequirementType.TRB1BigChestAreaTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRB1BigChestAreaTest]),
                RequirementType.TRBigChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRBigChestTest]),
                RequirementType.TRB1RightSideTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRB1RightSideTest]),
                RequirementType.TRPastB1toB2KeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRPastB1toB2KeyDoorTest]),
                RequirementType.TRB2DarkRoomTopTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRB2DarkRoomTopTest]),
                RequirementType.TRB2DarkRoomBottomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRB2DarkRoomBottomTest]),
                RequirementType.TRB2PastDarkMazeTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRB2PastDarkMazeTest]),
                RequirementType.TRLaserBridgeChestsTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRLaserBridgeChestsTest]),
                RequirementType.TRB2PastKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRB2PastKeyDoorTest]),
                RequirementType.TRB3Test => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRB3Test]),
                RequirementType.TRB3BossRoomEntryTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRB3BossRoomEntryTest]),
                RequirementType.TRBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRBossRoomTest]),
                RequirementType.TRBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.TRBossTest]),
                RequirementType.GTTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GTTest]),
                RequirementType.GTBobsTorchTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GTBobsTorchTest]),
                RequirementType.GT1FLeftTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FLeftTest]),
                RequirementType.GT1FLeftPastHammerBlocksTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FLeftPastHammerBlocksTest]),
                RequirementType.GT1FLeftDMsRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FLeftDMsRoomTest]),
                RequirementType.GT1FLeftPastBonkableGapsTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FLeftPastBonkableGapsTest]),
                RequirementType.GT1FLeftMapChestRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FLeftMapChestRoomTest]),
                RequirementType.GT1FLeftSpikeTrapPortalRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FLeftSpikeTrapPortalRoomTest]),
                RequirementType.GT1FLeftFiresnakeRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FLeftFiresnakeRoomTest]),
                RequirementType.GT1FLeftPastFiresnakeRoomGapTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FLeftPastFiresnakeRoomGapTest]),
                RequirementType.GT1FLeftPastFiresnakeRoomKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FLeftPastFiresnakeRoomKeyDoorTest]),
                RequirementType.GT1FLeftRandomizerRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FLeftRandomizerRoomTest]),
                RequirementType.GT1FRightTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FRightTest]),
                RequirementType.GT1FRightTileRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FRightTileRoomTest]),
                RequirementType.GT1FRightFourTorchRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FRightFourTorchRoomTest]),
                RequirementType.GT1FRightCompassRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FRightCompassRoomTest]),
                RequirementType.GT1FRightPastCompassRoomPortalTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FRightPastCompassRoomPortalTest]),
                RequirementType.GT1FRightCollapsingWalkwayTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FRightCollapsingWalkwayTest]),
                RequirementType.GT1FBottomRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT1FBottomRoomTest]),
                RequirementType.GTBoss1Test => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GTBoss1Test]),
                RequirementType.GTB1BossChestsTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GTB1BossChestsTest]),
                RequirementType.GTBigChestTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GTBigChestTest]),
                RequirementType.GT3FPastRedGoriyaRoomsTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT3FPastRedGoriyaRoomsTest]),
                RequirementType.GT3FPastBigKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT3FPastBigKeyDoorTest]),
                RequirementType.GTBoss2Test => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GTBoss2Test]),
                RequirementType.GT3FPastBoss2Test => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT3FPastBoss2Test]),
                RequirementType.GT5FPastFourTorchRoomsTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT5FPastFourTorchRoomsTest]),
                RequirementType.GT6FPastFirstKeyDoorTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT6FPastFirstKeyDoorTest]),
                RequirementType.GT6FBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT6FBossRoomTest]),
                RequirementType.GTBoss3Test => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GTBoss3Test]),
                RequirementType.GT6FPastBossRoomGapTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GT6FPastBossRoomGapTest]),
                RequirementType.GTFinalBossRoomTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GTFinalBossRoomTest]),
                RequirementType.GTFinalBossTest => new ItemRequirement(
                    ItemDictionary.Instance[ItemType.GTFinalBossTest]),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a sequence break requirement.
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A sequence break requirement.
        /// </returns>
        private static IRequirement GetSequenceBreakRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.SBBlindPedestal => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.BlindPedestal]),
                RequirementType.SBBonkOverLedge => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.BonkOverLedge]),
                RequirementType.SBBumperCaveHookshot => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.BumperCaveHookshot]),
                RequirementType.SBTRLaserSkip => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.TRLaserSkip]),
                RequirementType.SBHelmasaurKingBasic => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.HelmasaurKingBasic]),
                RequirementType.SBLanmolasBombs => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.LanmolasBombs]),
                RequirementType.SBArrghusBasic => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.ArrghusBasic]),
                RequirementType.SBMothulaBasic => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.MothulaBasic]),
                RequirementType.SBBlindBasic => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.BlindBasic]),
                RequirementType.SBKholdstareBasic => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.KholdstareBasic]),
                RequirementType.SBVitreousBasic => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.VitreousBasic]),
                RequirementType.SBTrinexxBasic => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.TrinexxBasic]),
                RequirementType.SBBombDuplicationAncillaOverload => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.BombDuplicationAncillaOverload]),
                RequirementType.SBBombDuplicationMirror => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.BombDuplicationMirror]),
                RequirementType.SBBombJumpPoDHammerJump => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.BombJumpPoDHammerJump]),
                RequirementType.SBBombJumpSWBigChest => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.BombJumpSWBigChest]),
                RequirementType.SBBombJumpIPBJ => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.BombJumpIPBJ]),
                RequirementType.SBBombJumpIPHookshotGap => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.BombJumpIPHookshotGap]),
                RequirementType.SBBombJumpIPFreezorRoomGap => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.BombJumpIPFreezorRoomGap]),
                RequirementType.SBDarkRoomDeathMountainEntry => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DarkRoomDeathMountainEntry]),
                RequirementType.SBDarkRoomDeathMountainExit => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DarkRoomDeathMountainExit]),
                RequirementType.SBDarkRoomHC => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DarkRoomHC]),
                RequirementType.SBDarkRoomAT => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DarkRoomAT]),
                RequirementType.SBDarkRoomEPRight => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DarkRoomEPRight]),
                RequirementType.SBDarkRoomEPBack => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DarkRoomEPBack]),
                RequirementType.SBDarkRoomPoDDarkBasement => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DarkRoomPoDDarkBasement]),
                RequirementType.SBDarkRoomPoDDarkMaze => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DarkRoomPoDDarkMaze]),
                RequirementType.SBDarkRoomPoDBossArea => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DarkRoomPoDBossArea]),
                RequirementType.SBDarkRoomMM => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DarkRoomMM]),
                RequirementType.SBDarkRoomTR => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DarkRoomTR]),
                RequirementType.SBFakeFlippersFairyRevival => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.FakeFlippersFairyRevival]),
                RequirementType.SBFakeFlippersQirnJump => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.FakeFlippersQirnJump]),
                RequirementType.SBFakeFlippersScreenTransition => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.FakeFlippersScreenTransition]),
                RequirementType.SBFakeFlippersSplashDeletion => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.FakeFlippersSplashDeletion]),
                RequirementType.SBWaterWalk => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.WaterWalk]),
                RequirementType.SBWaterWalkFromWaterfallCave => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.WaterWalkFromWaterfallCave]),
                RequirementType.SBSuperBunnyFallInHole => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.SuperBunnyFallInHole]),
                RequirementType.SBSuperBunnyMirror => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.SuperBunnyMirror]),
                RequirementType.SBCameraUnlock => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.CameraUnlock]),
                RequirementType.SBDungeonRevive => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.DungeonRevive]),
                RequirementType.SBFakePowder => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.FakePowder]),
                RequirementType.SBHover => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.Hover]),
                RequirementType.SBMimicClip => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.MimicClip]),
                RequirementType.SBSpikeCave => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.SpikeCave]),
                RequirementType.SBToHHerapot => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.ToHHerapot]),
                RequirementType.SBIPIceBreaker => new SequenceBreakRequirement(
                    SequenceBreakDictionary.Instance[SequenceBreakType.IPIceBreaker]),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a boss requirement.
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A boss requirement.
        /// </returns>
        private static IRequirement GetBossRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.ATBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.ATBoss]),
                RequirementType.EPBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.EPBoss]),
                RequirementType.DPBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.DPBoss]),
                RequirementType.ToHBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.ToHBoss]),
                RequirementType.PoDBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.PoDBoss]),
                RequirementType.SPBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.SPBoss]),
                RequirementType.SWBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.SWBoss]),
                RequirementType.TTBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.TTBoss]),
                RequirementType.IPBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.IPBoss]),
                RequirementType.MMBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.MMBoss]),
                RequirementType.TRBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.TRBoss]),
                RequirementType.GTBoss1 => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.GTBoss1]),
                RequirementType.GTBoss2 => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.GTBoss2]),
                RequirementType.GTBoss3 => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.GTBoss3]),
                RequirementType.GTFinalBoss => new BossRequirement(
                    BossPlacementDictionary.Instance[BossPlacementID.GTFinalBoss]),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a new requirement of the proper type.
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A requirement of the proper type.
        /// </returns>
        internal static IRequirement GetRequirement(RequirementType type)
        {
            switch (type)
            {
                case RequirementType.NoRequirement:
                case RequirementType.Inspect:
                case RequirementType.SequenceBreak:
                    {
                        return GetStaticRequirement(type);
                    }
                case RequirementType.WorldStateStandardOpen:
                case RequirementType.WorldStateInverted:
                case RequirementType.WorldStateRetro:
                    {
                        return GetWorldStateRequirement(type);
                    }
                case RequirementType.DungeonItemShuffleStandard:
                case RequirementType.DungeonItemShuffleMapsCompasses:
                case RequirementType.DungeonItemShuffleMapsCompassesSmallKeys:
                case RequirementType.DungeonItemShuffleKeysanity:
                    {
                        return GetDungeonItemShuffleRequirement(type);
                    }
                case RequirementType.ItemPlacementBasic:
                case RequirementType.ItemPlacementAdvanced:
                    {
                        return GetItemPlacementRequirement(type);
                    }
                case RequirementType.BossShuffleOff:
                case RequirementType.BossShuffleOn:
                    {
                        return GetBossShuffleRequirement(type);
                    }
                case RequirementType.EnemyShuffleOff:
                case RequirementType.EnemyShuffleOn:
                    {
                        return GetEnemyShuffleRequirement(type);
                    }
                case RequirementType.EntranceShuffleOff:
                case RequirementType.EntranceShuffleOn:
                    {
                        return GetEntranceShuffleRequirement(type);
                    }
                case RequirementType.GuaranteedBossItemsOff:
                case RequirementType.GuaranteedBossItemsOn:
                    {
                        return GetGuaranteedBossItemsRequirement(type);
                    }
                case RequirementType.WorldStateNonInverted:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen],
                            RequirementDictionary.Instance[RequirementType.WorldStateRetro]
                        });
                    }
                case RequirementType.SmallKeyShuffleOff:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen],
                                RequirementDictionary.Instance[RequirementType.WorldStateInverted]
                            }),
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.DungeonItemShuffleStandard],
                                RequirementDictionary.Instance[RequirementType.DungeonItemShuffleMapsCompasses]
                            })
                        });
                    }
                case RequirementType.SmallKeyShuffleOn:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateRetro],
                            RequirementDictionary.Instance[RequirementType.DungeonItemShuffleMapsCompassesSmallKeys],
                            RequirementDictionary.Instance[RequirementType.DungeonItemShuffleKeysanity]
                        });
                    }
                case RequirementType.WorldStateInvertedEntranceShuffleOff:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOff]
                        });
                    }
                case RequirementType.WorldStateInvertedEntranceShuffleOn:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]
                        });
                    }
                case RequirementType.WorldStateNonInvertedEntranceShuffleOff:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOff]
                        });
                    }
                case RequirementType.WorldStateRetroEntranceShuffleOff:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateRetro],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOff]
                        });
                    }
                case RequirementType.Swordless:
                case RequirementType.Mushroom:
                case RequirementType.BombosMM:
                case RequirementType.BombosTR:
                case RequirementType.BombosBoth:
                case RequirementType.EtherMM:
                case RequirementType.EtherTR:
                case RequirementType.EtherBoth:
                case RequirementType.QuakeMM:
                case RequirementType.QuakeTR:
                case RequirementType.QuakeBoth:
                case RequirementType.NoFlippers:
                    {
                        return GetItemExactRequirement(type);
                    }
                case RequirementType.Sword1:
                case RequirementType.Sword2:
                case RequirementType.Sword3:
                case RequirementType.Shield3:
                case RequirementType.Aga1:
                case RequirementType.Bow:
                case RequirementType.Boomerang:
                case RequirementType.RedBoomerang:
                case RequirementType.Hookshot:
                case RequirementType.Powder:
                case RequirementType.Boots:
                case RequirementType.FireRod:
                case RequirementType.IceRod:
                case RequirementType.Bombos:
                case RequirementType.Ether:
                case RequirementType.Quake:
                case RequirementType.Gloves1:
                case RequirementType.Gloves2:
                case RequirementType.Lamp:
                case RequirementType.Hammer:
                case RequirementType.Flute:
                case RequirementType.Net:
                case RequirementType.Book:
                case RequirementType.Shovel:
                case RequirementType.Flippers:
                case RequirementType.Bottle:
                case RequirementType.CaneOfSomaria:
                case RequirementType.CaneOfByrna:
                case RequirementType.Cape:
                case RequirementType.Mirror:
                case RequirementType.HalfMagic:
                case RequirementType.MoonPearl:
                case RequirementType.Aga2:
                case RequirementType.RedCrystal:
                case RequirementType.Pendant:
                case RequirementType.GreenPendant:
                case RequirementType.LightWorldTest:
                case RequirementType.LumberjackTreeActiveTest:
                case RequirementType.LumberjackCaveEntranceTest:
                case RequirementType.ForestHideoutTest:
                case RequirementType.DeathMountainEntryTest:
                case RequirementType.DeathMountainExitTest:
                case RequirementType.LWKakarikoPortalTest:
                case RequirementType.GrassHouseTest:
                case RequirementType.BombHutTest:
                case RequirementType.MagicBatLedgeTest:
                case RequirementType.RaceGameLedgeTest:
                case RequirementType.SouthOfGroveLedgeTest:
                case RequirementType.DesertLedgeTest:
                case RequirementType.DesertPalaceBackEntranceTest:
                case RequirementType.CheckerboardLedgeTest:
                case RequirementType.CheckerboardCaveTest:
                case RequirementType.DesertPalaceFrontEntranceTest:
                case RequirementType.BombosTabletLedgeTest:
                case RequirementType.RupeeCaveTest:
                case RequirementType.LWMirePortalTest:
                case RequirementType.NorthBonkRocksTest:
                case RequirementType.LWGraveyardTest:
                case RequirementType.EscapeGraveTest:
                case RequirementType.LWGraveyardLedgeTest:
                case RequirementType.LWKingsTombTest:
                case RequirementType.KingsTombGraveTest:
                case RequirementType.HyruleCastleTopTest:
                case RequirementType.AgahnimTowerEntranceTest:
                case RequirementType.GanonHoleTest:
                case RequirementType.GanonHoleBackTest:
                case RequirementType.CastleSecretFrontTest:
                case RequirementType.CastleSecretBackTest:
                case RequirementType.CentralBonkRocksTest:
                case RequirementType.LWSouthPortalTest:
                case RequirementType.HypeFairyCaveTest:
                case RequirementType.MiniMoldormCaveTest:
                case RequirementType.ZoraTest:
                case RequirementType.WaterfallFairyTest:
                case RequirementType.LWWitchAreaTest:
                case RequirementType.LWEastPortalTest:
                case RequirementType.LWLakeHyliaTest:
                case RequirementType.LWLakeHyliaWaterWalkTest:
                case RequirementType.LakeHyliaIslandTest:
                case RequirementType.LakeHyliaFairyIslandTest:
                case RequirementType.IceRodCaveTest:
                case RequirementType.IceFairyCaveTest:
                case RequirementType.DeathMountainWestBottomTest:
                case RequirementType.SpectacleRockTopTest:
                case RequirementType.DeathMountainWestTopTest:
                case RequirementType.DeathMountainEastBottomTest:
                case RequirementType.DeathMountainEastBottomConnectorTest:
                case RequirementType.DeathMountainEastTopConnectorTest:
                case RequirementType.SpiralCaveTest:
                case RequirementType.MimicCaveTest:
                case RequirementType.DeathMountainEastTopTest:
                case RequirementType.LWFloatingIslandTest:
                case RequirementType.LWTurtleRockTopTest:
                case RequirementType.DWKakarikoPortalTest:
                case RequirementType.DarkWorldWestTest:
                case RequirementType.SkullWoodsBackEntranceTest:
                case RequirementType.BumperCaveTest:
                case RequirementType.BumperCaveTopTest:
                case RequirementType.ThievesTownEntranceTest:
                case RequirementType.BombableShackTest:
                case RequirementType.HammerHouseTest:
                case RequirementType.HammerPegsAreaTest:
                case RequirementType.HammerPegsTest:
                case RequirementType.BlacksmithPrisonTest:
                case RequirementType.DarkWorldSouthTest:
                case RequirementType.DWCentralBonkRocksTest:
                case RequirementType.BuyBigBombTest:
                case RequirementType.BigBombToLightWorldTest:
                case RequirementType.BigBombToDWLakeHyliaTest:
                case RequirementType.BigBombToWallTest:
                case RequirementType.DWSouthPortalTest:
                case RequirementType.HypeCaveTest:
                case RequirementType.MireAreaTest:
                case RequirementType.DWMirePortalTest:
                case RequirementType.MiseryMireEntranceTest:
                case RequirementType.DWGraveyardTest:
                case RequirementType.DWGraveyardLedgeTest:
                case RequirementType.DWWitchAreaTest:
                case RequirementType.CatfishTest:
                case RequirementType.DarkWorldEastTest:
                case RequirementType.FatFairyTest:
                case RequirementType.PalaceOfDarknessEntranceTest:
                case RequirementType.DWEastPortalTest:
                case RequirementType.DWLakeHyliaTest:
                case RequirementType.DWLakeHyliaWaterWalkTest:
                case RequirementType.IcePalaceEntranceTest:
                case RequirementType.DarkWorldSouthEastTest:
                case RequirementType.DWIceRodCaveTest:
                case RequirementType.DWIceRodRockTest:
                case RequirementType.DarkDeathMountainWestBottomTest:
                case RequirementType.DarkDeathMountainTopTest:
                case RequirementType.GanonsTowerEntranceTest:
                case RequirementType.DWFloatingIslandTest:
                case RequirementType.HookshotCaveTest:
                case RequirementType.DWTurtleRockTopTest:
                case RequirementType.TurtleRockFrontEntranceTest:
                case RequirementType.DarkDeathMountainEastBottomTest:
                case RequirementType.DarkDeathMountainEastBottomConnectorTest:
                case RequirementType.TurtleRockTunnelTest:
                case RequirementType.TurtleRockSafetyDoorTest:
                case RequirementType.HCSanctuaryEntryTest:
                case RequirementType.HCFrontEntryTest:
                case RequirementType.HCBackEntryTest:
                case RequirementType.ATEntryTest:
                case RequirementType.EPEntryTest:
                case RequirementType.DPFrontEntryTest:
                case RequirementType.DPLeftEntryTest:
                case RequirementType.DPBackEntryTest:
                case RequirementType.ToHEntryTest:
                case RequirementType.PoDEntryTest:
                case RequirementType.SPEntryTest:
                case RequirementType.SWFrontEntryTest:
                case RequirementType.SWFrontLeftDropEntryTest:
                case RequirementType.SWPinballRoomEntryTest:
                case RequirementType.SWFrontTopDropEntryTest:
                case RequirementType.SWFrontBackConnectorEntryTest:
                case RequirementType.SWBackEntryTest:
                case RequirementType.TTEntryTest:
                case RequirementType.IPEntryTest:
                case RequirementType.MMEntryTest:
                case RequirementType.TRFrontEntryTest:
                case RequirementType.TRFrontToKeyDoorsTest:
                case RequirementType.TRKeyDoorsToMiddleExitTest:
                case RequirementType.TRMiddleEntryTest:
                case RequirementType.TRBackEntryTest:
                case RequirementType.GTEntryTest:
                case RequirementType.HCSanctuaryTest:
                case RequirementType.HCFrontTest:
                case RequirementType.HCPastEscapeFirstKeyDoorTest:
                case RequirementType.HCPastEscapeSecondKeyDoorTest:
                case RequirementType.HCDarkRoomFrontTest:
                case RequirementType.HCPastDarkCrossKeyDoorTest:
                case RequirementType.HCPastSewerRatRoomKeyDoorTest:
                case RequirementType.HCDarkRoomBackTest:
                case RequirementType.HCBackTest:
                case RequirementType.ATTest:
                case RequirementType.ATDarkMazeTest:
                case RequirementType.ATPastFirstKeyDoorTest:
                case RequirementType.ATPastSecondKeyDoorTest:
                case RequirementType.ATPastThirdKeyDoorTest:
                case RequirementType.ATPastFourthKeyDoorTest:
                case RequirementType.ATBossRoomTest:
                case RequirementType.ATBossTest:
                case RequirementType.EPTest:
                case RequirementType.EPBigChestTest:
                case RequirementType.EPRightDarkRoomTest:
                case RequirementType.EPPastRightKeyDoorTest:
                case RequirementType.EPPastBigKeyDoorTest:
                case RequirementType.EPBackDarkRoomTest:
                case RequirementType.EPPastBackKeyDoorTest:
                case RequirementType.EPBossRoomTest:
                case RequirementType.EPBossTest:
                case RequirementType.DPFrontTest:
                case RequirementType.DPTorchItemTest:
                case RequirementType.DPBigChestTest:
                case RequirementType.DPPastRightKeyDoorTest:
                case RequirementType.DPBackTest:
                case RequirementType.DP2FTest:
                case RequirementType.DP2FPastFirstKeyDoorTest:
                case RequirementType.DP2FPastSecondKeyDoorTest:
                case RequirementType.DPPastFourTorchWallTest:
                case RequirementType.DPBossRoomTest:
                case RequirementType.DPBossTest:
                case RequirementType.ToHTest:
                case RequirementType.ToHPastKeyDoorTest:
                case RequirementType.ToHBasementTorchRoomTest:
                case RequirementType.ToHPastBigKeyDoorTest:
                case RequirementType.ToHBigChestTest:
                case RequirementType.ToHBossTest:
                case RequirementType.PoDTest:
                case RequirementType.PoDPastFirstRedGoriyaRoomTest:
                case RequirementType.PoDLobbyArenaTest:
                case RequirementType.PoDBigKeyChestAreaTest:
                case RequirementType.PoDPastCollapsingWalkwayKeyDoorTest:
                case RequirementType.PoDDarkBasementTest:
                case RequirementType.PoDHarmlessHellwayRoomTest:
                case RequirementType.PoDPastDarkMazeKeyDoorTest:
                case RequirementType.PoDDarkMazeTest:
                case RequirementType.PoDBigChestLedgeTest:
                case RequirementType.PoDBigChestTest:
                case RequirementType.PoDPastSecondRedGoriyaRoomTest:
                case RequirementType.PoDPastBowStatueTest:
                case RequirementType.PoDBossAreaDarkRoomsTest:
                case RequirementType.PoDPastHammerBlocksTest:
                case RequirementType.PoDPastBossAreaKeyDoorTest:
                case RequirementType.PoDBossRoomTest:
                case RequirementType.PoDBossTest:
                case RequirementType.SPTest:
                case RequirementType.SPAfterRiverTest:
                case RequirementType.SPB1Test:
                case RequirementType.SPB1PastFirstRightKeyDoorTest:
                case RequirementType.SPB1PastSecondRightKeyDoorTest:
                case RequirementType.SPB1PastRightHammerBlocksTest:
                case RequirementType.SPB1KeyLedgeTest:
                case RequirementType.SPB1PastLeftKeyDoorTest:
                case RequirementType.SPBigChestTest:
                case RequirementType.SPB1BackTest:
                case RequirementType.SPB1PastBackFirstKeyDoorTest:
                case RequirementType.SPBossRoomTest:
                case RequirementType.SPBossTest:
                case RequirementType.SWBigChestAreaBottomTest:
                case RequirementType.SWBigChestAreaTopTest:
                case RequirementType.SWBigChestTest:
                case RequirementType.SWFrontLeftSideTest:
                case RequirementType.SWFrontRightSideTest:
                case RequirementType.SWFrontBackConnectorTest:
                case RequirementType.SWPastTheWorthlessKeyDoorTest:
                case RequirementType.SWBackTest:
                case RequirementType.SWBackPastFirstKeyDoorTest:
                case RequirementType.SWBackPastFourTorchRoomTest:
                case RequirementType.SWBackPastCurtainsTest:
                case RequirementType.SWBossRoomTest:
                case RequirementType.SWBossTest:
                case RequirementType.TTTest:
                case RequirementType.TTPastBigKeyDoorTest:
                case RequirementType.TTPastFirstKeyDoorTest:
                case RequirementType.TTPastSecondKeyDoorTest:
                case RequirementType.TTPastBigChestRoomKeyDoorTest:
                case RequirementType.TTPastHammerBlocksTest:
                case RequirementType.TTBigChestTest:
                case RequirementType.TTBossRoomTest:
                case RequirementType.TTBossTest:
                case RequirementType.IPTest:
                case RequirementType.IPPastEntranceFreezorRoomTest:
                case RequirementType.IPB1LeftSideTest:
                case RequirementType.IPB1RightSideTest:
                case RequirementType.IPB2LeftSideTest:
                case RequirementType.IPB2PastKeyDoorTest:
                case RequirementType.IPB2PastHammerBlocksTest:
                case RequirementType.IPB2PastLiftBlockTest:
                case RequirementType.IPSpikeRoomTest:
                case RequirementType.IPB4RightSideTest:
                case RequirementType.IPB4IceRoomTest:
                case RequirementType.IPB4FreezorRoomTest:
                case RequirementType.IPFreezorChestTest:
                case RequirementType.IPB4PastKeyDoorTest:
                case RequirementType.IPBigChestAreaTest:
                case RequirementType.IPBigChestTest:
                case RequirementType.IPB5Test:
                case RequirementType.IPB5PastBigKeyDoorTest:
                case RequirementType.IPB6Test:
                case RequirementType.IPB6PastKeyDoorTest:
                case RequirementType.IPB6PreBossRoomTest:
                case RequirementType.IPB6PastHammerBlocksTest:
                case RequirementType.IPB6PastLiftBlockTest:
                case RequirementType.IPBossTest:
                case RequirementType.MMTest:
                case RequirementType.MMPastEntranceGapTest:
                case RequirementType.MMBigChestTest:
                case RequirementType.MMB1TopSideTest:
                case RequirementType.MMB1LobbyBeyondBlueBlocksTest:
                case RequirementType.MMB1RightSideBeyondBlueBlocksTest:
                case RequirementType.MMB1LeftSidePastFirstKeyDoorTest:
                case RequirementType.MMB1LeftSidePastSecondKeyDoorTest:
                case RequirementType.MMB1PastFourTorchRoomTest:
                case RequirementType.MMF1PastFourTorchRoomTest:
                case RequirementType.MMB1PastPortalBigKeyDoorTest:
                case RequirementType.MMB1PastBridgeBigKeyDoorTest:
                case RequirementType.MMDarkRoomTest:
                case RequirementType.MMB2PastWorthlessKeyDoorTest:
                case RequirementType.MMB2PastCaneOfSomariaSwitchTest:
                case RequirementType.MMBossRoomTest:
                case RequirementType.MMBossTest:
                case RequirementType.TRFrontTest:
                case RequirementType.TRF1CompassChestAreaTest:
                case RequirementType.TRF1FourTorchRoomTest:
                case RequirementType.TRF1RollerRoomTest:
                case RequirementType.TRF1FirstKeyDoorAreaTest:
                case RequirementType.TRF1PastFirstKeyDoorTest:
                case RequirementType.TRF1PastSecondKeyDoorTest:
                case RequirementType.TRB1Test:
                case RequirementType.TRB1PastBigKeyChestKeyDoorTest:
                case RequirementType.TRB1MiddleRightEntranceAreaTest:
                case RequirementType.TRB1BigChestAreaTest:
                case RequirementType.TRBigChestTest:
                case RequirementType.TRB1RightSideTest:
                case RequirementType.TRPastB1toB2KeyDoorTest:
                case RequirementType.TRB2DarkRoomTopTest:
                case RequirementType.TRB2DarkRoomBottomTest:
                case RequirementType.TRB2PastDarkMazeTest:
                case RequirementType.TRLaserBridgeChestsTest:
                case RequirementType.TRB2PastKeyDoorTest:
                case RequirementType.TRB3Test:
                case RequirementType.TRB3BossRoomEntryTest:
                case RequirementType.TRBossRoomTest:
                case RequirementType.TRBossTest:
                case RequirementType.GTTest:
                case RequirementType.GTBobsTorchTest:
                case RequirementType.GT1FLeftTest:
                case RequirementType.GT1FLeftPastHammerBlocksTest:
                case RequirementType.GT1FLeftDMsRoomTest:
                case RequirementType.GT1FLeftPastBonkableGapsTest:
                case RequirementType.GT1FLeftMapChestRoomTest:
                case RequirementType.GT1FLeftSpikeTrapPortalRoomTest:
                case RequirementType.GT1FLeftFiresnakeRoomTest:
                case RequirementType.GT1FLeftPastFiresnakeRoomGapTest:
                case RequirementType.GT1FLeftPastFiresnakeRoomKeyDoorTest:
                case RequirementType.GT1FLeftRandomizerRoomTest:
                case RequirementType.GT1FRightTest:
                case RequirementType.GT1FRightTileRoomTest:
                case RequirementType.GT1FRightFourTorchRoomTest:
                case RequirementType.GT1FRightCompassRoomTest:
                case RequirementType.GT1FRightPastCompassRoomPortalTest:
                case RequirementType.GT1FRightCollapsingWalkwayTest:
                case RequirementType.GT1FBottomRoomTest:
                case RequirementType.GTBoss1Test:
                case RequirementType.GTB1BossChestsTest:
                case RequirementType.GTBigChestTest:
                case RequirementType.GT3FPastRedGoriyaRoomsTest:
                case RequirementType.GT3FPastBigKeyDoorTest:
                case RequirementType.GTBoss2Test:
                case RequirementType.GT3FPastBoss2Test:
                case RequirementType.GT5FPastFourTorchRoomsTest:
                case RequirementType.GT6FPastFirstKeyDoorTest:
                case RequirementType.GT6FBossRoomTest:
                case RequirementType.GTBoss3Test:
                case RequirementType.GT6FPastBossRoomGapTest:
                case RequirementType.GTFinalBossRoomTest:
                case RequirementType.GTFinalBossTest:
                    {
                        return GetItemRequirement(type);
                    }
                case RequirementType.LightWorldAccess:
                case RequirementType.DeathMountainEntryAccess:
                case RequirementType.DeathMountainExitAccess:
                case RequirementType.GrassHouseAccess:
                case RequirementType.BombHutAccess:
                case RequirementType.RaceGameLedgeAccess:
                case RequirementType.SouthOfGroveLedgeAccess:
                case RequirementType.DesertLedgeAccess:
                case RequirementType.DesertPalaceBackEntranceAccess:
                case RequirementType.CheckerboardLedgeAccess:
                case RequirementType.LWGraveyardLedgeAccess:
                case RequirementType.LWKingsTombAccess:
                case RequirementType.HyruleCastleTopAccess:
                case RequirementType.WaterfallFairyAccess:
                case RequirementType.LWWitchAreaAccess:
                case RequirementType.LakeHyliaFairyIslandAccess:
                case RequirementType.DeathMountainWestBottomAccess:
                case RequirementType.DeathMountainWestTopAccess:
                case RequirementType.DeathMountainEastBottomAccess:
                case RequirementType.DeathMountainEastBottomConnectorAccess:
                case RequirementType.DeathMountainEastTopConnectorAccess:
                case RequirementType.SpiralCaveAccess:
                case RequirementType.MimicCaveAccess:
                case RequirementType.DeathMountainEastTopAccess:
                case RequirementType.DarkWorldWestAccess:
                case RequirementType.BumperCaveAccess:
                case RequirementType.BumperCaveTopAccess:
                case RequirementType.HammerHouseAccess:
                case RequirementType.HammerPegsAreaAccess:
                case RequirementType.DarkWorldSouthAccess:
                case RequirementType.MireAreaAccess:
                case RequirementType.DWWitchAreaAccess:
                case RequirementType.DarkWorldEastAccess:
                case RequirementType.IcePalaceEntranceAccess:
                case RequirementType.DarkWorldSouthEastAccess:
                case RequirementType.DarkDeathMountainWestBottomAccess:
                case RequirementType.DarkDeathMountainTopAccess:
                case RequirementType.DWFloatingIslandAccess:
                case RequirementType.DarkDeathMountainEastBottomAccess:
                case RequirementType.TurtleRockTunnelAccess:
                case RequirementType.TurtleRockSafetyDoorAccess:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOn],
                            GetItemRequirement(type)
                        });
                    }
                case RequirementType.TRSmallKey2:
                    {
                        return new SmallKeyRequirement(
                            ItemDictionary.Instance[ItemType.TRSmallKey], 2);
                    }
                case RequirementType.SBBlindPedestal:
                case RequirementType.SBBonkOverLedge:
                case RequirementType.SBBumperCaveHookshot:
                case RequirementType.SBTRLaserSkip:
                case RequirementType.SBLanmolasBombs:
                case RequirementType.SBHelmasaurKingBasic:
                case RequirementType.SBArrghusBasic:
                case RequirementType.SBMothulaBasic:
                case RequirementType.SBBlindBasic:
                case RequirementType.SBKholdstareBasic:
                case RequirementType.SBVitreousBasic:
                case RequirementType.SBTrinexxBasic:
                case RequirementType.SBBombDuplicationAncillaOverload:
                case RequirementType.SBBombDuplicationMirror:
                case RequirementType.SBBombJumpPoDHammerJump:
                case RequirementType.SBBombJumpSWBigChest:
                case RequirementType.SBBombJumpIPBJ:
                case RequirementType.SBBombJumpIPHookshotGap:
                case RequirementType.SBBombJumpIPFreezorRoomGap:
                case RequirementType.SBDarkRoomDeathMountainEntry:
                case RequirementType.SBDarkRoomDeathMountainExit:
                case RequirementType.SBDarkRoomHC:
                case RequirementType.SBDarkRoomAT:
                case RequirementType.SBDarkRoomEPRight:
                case RequirementType.SBDarkRoomEPBack:
                case RequirementType.SBDarkRoomPoDDarkBasement:
                case RequirementType.SBDarkRoomPoDDarkMaze:
                case RequirementType.SBDarkRoomPoDBossArea:
                case RequirementType.SBDarkRoomMM:
                case RequirementType.SBDarkRoomTR:
                case RequirementType.SBFakeFlippersFairyRevival:
                case RequirementType.SBFakeFlippersQirnJump:
                case RequirementType.SBFakeFlippersScreenTransition:
                case RequirementType.SBFakeFlippersSplashDeletion:
                case RequirementType.SBWaterWalk:
                case RequirementType.SBWaterWalkFromWaterfallCave:
                case RequirementType.SBSuperBunnyFallInHole:
                case RequirementType.SBSuperBunnyMirror:
                case RequirementType.SBCameraUnlock:
                case RequirementType.SBDungeonRevive:
                case RequirementType.SBFakePowder:
                case RequirementType.SBHover:
                case RequirementType.SBMimicClip:
                case RequirementType.SBSpikeCave:
                case RequirementType.SBToHHerapot:
                case RequirementType.SBIPIceBreaker:
                    {
                        return GetSequenceBreakRequirement(type);
                    }
                case RequirementType.GTCrystal:
                    {
                        return new CrystalRequirement();
                    }
                case RequirementType.LightWorld:
                    {
                        return new RequirementNodeRequirement(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld]);
                    }
                case RequirementType.AllMedallions:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Bombos],
                            RequirementDictionary.Instance[RequirementType.Ether],
                            RequirementDictionary.Instance[RequirementType.Quake]
                        });
                    }
                case RequirementType.ExtendMagic1:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Bottle],
                                RequirementDictionary.Instance[RequirementType.HalfMagic]
                            });
                    }
                case RequirementType.ExtendMagic2:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Bottle],
                                RequirementDictionary.Instance[RequirementType.HalfMagic]
                            });
                    }
                case RequirementType.FireRodDarkRoom:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.FireRod],
                            RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced]
                        });
                    }
                case RequirementType.UseMedallion:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Swordless],
                            RequirementDictionary.Instance[RequirementType.Sword1]
                        });
                    }
                case RequirementType.MeltThings:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.FireRod],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Bombos],
                                RequirementDictionary.Instance[RequirementType.UseMedallion]
                            })
                        });
                    }
                case RequirementType.NotBunnyLW:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen],
                            RequirementDictionary.Instance[RequirementType.WorldStateRetro],
                            RequirementDictionary.Instance[RequirementType.MoonPearl]
                        });
                    }
                case RequirementType.NotBunnyDW:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.MoonPearl]
                        });
                    }
                case RequirementType.ATBarrier:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Cape],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Swordless],
                                RequirementDictionary.Instance[RequirementType.Hammer]
                            }),
                            RequirementDictionary.Instance[RequirementType.Sword2]
                        });
                    }
                case RequirementType.BombDuplicationAncillaOverload:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBBombDuplicationAncillaOverload],
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.Bow],
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Boomerang],
                                RequirementDictionary.Instance[RequirementType.RedBoomerang]
                            })
                        });
                    }
                case RequirementType.BombDuplicationMirror:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBBombDuplicationMirror],
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.Flippers],
                            RequirementDictionary.Instance[RequirementType.DWMirror]
                        });
                    }
                case RequirementType.BombJumpPoDHammerJump:
                    {
                        return RequirementDictionary.Instance[RequirementType.SBBombJumpPoDHammerJump];
                    }
                case RequirementType.BombJumpSWBigChest:
                    {
                        return RequirementDictionary.Instance[RequirementType.SBBombJumpSWBigChest];
                    }
                case RequirementType.BombJumpIPBJ:
                    {
                        return RequirementDictionary.Instance[RequirementType.SBBombJumpIPBJ];
                    }
                case RequirementType.BombJumpIPHookshotGap:
                    {
                        return RequirementDictionary.Instance[RequirementType.SBBombJumpIPHookshotGap];
                    }
                case RequirementType.BombJumpIPFreezorRoomGap:
                    {
                        return RequirementDictionary.Instance[RequirementType.SBBombJumpIPFreezorRoomGap];
                    }
                case RequirementType.BonkOverLedge:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Boots],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.SBBonkOverLedge],
                                RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced]
                            })
                        });
                    }
                case RequirementType.BumperCave:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Cape],
                            RequirementDictionary.Instance[RequirementType.MoonPearl],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Hookshot],
                                RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                RequirementDictionary.Instance[RequirementType.SBBumperCaveHookshot]
                            })
                        });
                    }
                case RequirementType.CameraUnlock:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBCameraUnlock],
                            RequirementDictionary.Instance[RequirementType.Bottle]
                        });
                    }
                case RequirementType.Curtains:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Swordless],
                            RequirementDictionary.Instance[RequirementType.Sword1]
                        });
                    }
                case RequirementType.DarkRoomDeathMountainEntry:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBDarkRoomDeathMountainEntry],
                            RequirementDictionary.Instance[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomDeathMountainExit:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBDarkRoomDeathMountainExit],
                            RequirementDictionary.Instance[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomHC:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBDarkRoomHC],
                            RequirementDictionary.Instance[RequirementType.Lamp],
                            RequirementDictionary.Instance[RequirementType.FireRodDarkRoom]
                        });
                    }
                case RequirementType.DarkRoomAT:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBDarkRoomAT],
                            RequirementDictionary.Instance[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomEPRight:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBDarkRoomEPRight],
                            RequirementDictionary.Instance[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomEPBack:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBDarkRoomEPBack],
                            RequirementDictionary.Instance[RequirementType.Lamp],
                            RequirementDictionary.Instance[RequirementType.FireRodDarkRoom]
                        });
                    }
                case RequirementType.DarkRoomPoDDarkBasement:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBDarkRoomPoDDarkBasement],
                            RequirementDictionary.Instance[RequirementType.Lamp],
                            RequirementDictionary.Instance[RequirementType.FireRodDarkRoom]
                        });
                    }
                case RequirementType.DarkRoomPoDDarkMaze:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBDarkRoomPoDDarkMaze],
                            RequirementDictionary.Instance[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomPoDBossArea:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBDarkRoomPoDBossArea],
                            RequirementDictionary.Instance[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomMM:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBDarkRoomMM],
                            RequirementDictionary.Instance[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomTR:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBDarkRoomTR],
                            RequirementDictionary.Instance[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DungeonRevive:
                    {
                        return RequirementDictionary.Instance[RequirementType.SBDungeonRevive];
                    }
                case RequirementType.FakeFlippersFairyRevival:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBFakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.Bottle]
                        });
                    }
                case RequirementType.FakeFlippersSplashDeletion:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Bow],
                                RequirementDictionary.Instance[RequirementType.RedBoomerang],
                                RequirementDictionary.Instance[RequirementType.CaneOfSomaria],
                                RequirementDictionary.Instance[RequirementType.IceRod]
                            }),
                            RequirementDictionary.Instance[RequirementType.SBFakeFlippersSplashDeletion]
                        });
                    }
                case RequirementType.FireSource:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Lamp],
                            RequirementDictionary.Instance[RequirementType.FireRod]
                        });
                    }
                case RequirementType.Hover:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBHover],
                            RequirementDictionary.Instance[RequirementType.Boots]
                        });
                    }
                case RequirementType.LaserBridge:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBTRLaserSkip],
                            RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                            RequirementDictionary.Instance[RequirementType.Cape],
                            RequirementDictionary.Instance[RequirementType.CaneOfByrna],
                            RequirementDictionary.Instance[RequirementType.Shield3]
                        });
                    }
                case RequirementType.Pedestal:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Pendant],
                                RequirementDictionary.Instance[RequirementType.GreenPendant],
                                new AlternativeRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.SBBlindPedestal],
                                    RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                    RequirementDictionary.Instance[RequirementType.Book]
                                })
                            }),
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Book],
                                RequirementDictionary.Instance[RequirementType.Inspect]
                            })
                        });
                    }
                case RequirementType.RedEyegoreGoriya:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.EnemyShuffleOn],
                            RequirementDictionary.Instance[RequirementType.Bow]
                        });
                    }
                case RequirementType.SPEntry:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.MoonPearl],
                            RequirementDictionary.Instance[RequirementType.Mirror]
                        });
                    }
                case RequirementType.SuperBunnyFallInHole:
                    {
                        return RequirementDictionary.Instance[RequirementType.SBSuperBunnyFallInHole];
                    }
                case RequirementType.SuperBunnyMirror:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBSuperBunnyMirror],
                            RequirementDictionary.Instance[RequirementType.Mirror]
                        });
                    }
                case RequirementType.Tablet:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Book],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Inspect],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.Swordless],
                                    RequirementDictionary.Instance[RequirementType.Hammer]
                                }),
                                RequirementDictionary.Instance[RequirementType.Sword2]
                            })
                        });
                    }
                case RequirementType.Torch:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Inspect],
                            RequirementDictionary.Instance[RequirementType.Boots]
                        });
                    }
                case RequirementType.ToHHerapot:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBToHHerapot],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Hookshot],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.Boots],
                                    RequirementDictionary.Instance[RequirementType.Sword1]
                                })
                            })
                        });
                    }
                case RequirementType.IPIceBreaker:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBIPIceBreaker],
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]
                        });
                    }
                case RequirementType.MMMedallion:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.UseMedallion],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.AllMedallions],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.Bombos],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.BombosMM],
                                        RequirementDictionary.Instance[RequirementType.BombosBoth]
                                    })
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.Ether],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.EtherMM],
                                        RequirementDictionary.Instance[RequirementType.EtherBoth]
                                    })
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.Quake],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.QuakeMM],
                                        RequirementDictionary.Instance[RequirementType.QuakeBoth]
                                    })
                                }),
                            })
                        });
                    }
                case RequirementType.TRMedallion:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.UseMedallion],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.AllMedallions],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.Bombos],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.BombosTR],
                                        RequirementDictionary.Instance[RequirementType.BombosBoth]
                                    })
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.Ether],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.EtherTR],
                                        RequirementDictionary.Instance[RequirementType.EtherBoth]
                                    })
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.Quake],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.QuakeTR],
                                        RequirementDictionary.Instance[RequirementType.QuakeBoth]
                                    })
                                }),
                            })
                        });
                    }
                case RequirementType.TRKeyDoorsToMiddleExit:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn],
                                RequirementDictionary.Instance[RequirementType.TRSmallKey2]
                            }),
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                new AlternativeRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.FireRod],
                                    RequirementDictionary.Instance[RequirementType.SequenceBreak]
                                })
                            })
                        });
                    }
                case RequirementType.WaterWalk:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBWaterWalk],
                            RequirementDictionary.Instance[RequirementType.Boots]
                        });
                    }
                case RequirementType.WaterWalkFromWaterfallCave:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBWaterWalkFromWaterfallCave],
                            RequirementDictionary.Instance[RequirementType.NoFlippers],
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.MoonPearl],
                                RequirementDictionary.Instance[RequirementType.WaterfallFairyAccess]
                            })
                        });
                    }
                case RequirementType.LWDash:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.Boots]
                        });
                    }
                case RequirementType.LWFakeFlippersFairyRevival:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival]
                        });
                    }
                case RequirementType.LWFakeFlippersScreenTransition:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBFakeFlippersScreenTransition],
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]
                        });
                    }
                case RequirementType.LWFakeFlippersSplashDeletion:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]
                        });
                    }
                case RequirementType.LWFlute:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.Flute]
                        });
                    }
                case RequirementType.LWHammer:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.Hammer]
                        });
                    }
                case RequirementType.LWHookshot:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.Hookshot]
                        });
                    }
                case RequirementType.LWLift1:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.Gloves1]
                        });
                    }
                case RequirementType.LWLift2:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.Gloves2]
                        });
                    }
                case RequirementType.LWMirror:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.Mirror]
                        });
                    }
                case RequirementType.LWPowder:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Powder],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.SBFakePowder],
                                    RequirementDictionary.Instance[RequirementType.Mushroom],
                                    RequirementDictionary.Instance[RequirementType.CaneOfSomaria]
                                })
                            })
                        });
                    }
                case RequirementType.LWShovel:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.Shovel]
                        });
                    }
                case RequirementType.LWSwim:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.Flippers]
                        });
                    }
                case RequirementType.LWWaterWalk:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.WaterWalk]
                        });
                    }
                case RequirementType.DWBonkOverLedge:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.BonkOverLedge]
                        });
                    }
                case RequirementType.DWDash:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.Boots]
                        });
                    }
                case RequirementType.DWFakeFlippersFairyRevival:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival]
                        });
                    }
                case RequirementType.DWFakeFlippersQirnJump:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.SBFakeFlippersQirnJump]
                        });
                    }
                case RequirementType.DWFakeFlippersSplashDeletion:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]
                        });
                    }
                case RequirementType.DWFireRod:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.FireRod]
                        });
                    }
                case RequirementType.DWFlute:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.MoonPearl],
                            RequirementDictionary.Instance[RequirementType.Flute]
                        });
                    }
                case RequirementType.DWHammer:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.Hammer]
                        });
                    }
                case RequirementType.DWHookshot:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.Hookshot]
                        });
                    }
                case RequirementType.DWHover:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.Hover]
                        });
                    }
                case RequirementType.DWLift1:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.Gloves1]
                        });
                    }
                case RequirementType.DWLift2:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.Gloves2]
                        });
                    }
                case RequirementType.DWMirror:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.Mirror]
                        });
                    }
                case RequirementType.DWSpikeCave:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.Gloves1],
                            RequirementDictionary.Instance[RequirementType.Hammer],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.SequenceBreak],
                                RequirementDictionary.Instance[RequirementType.CaneOfByrna],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.Cape],
                                    RequirementDictionary.Instance[RequirementType.ExtendMagic1]
                                })
                            })
                        });
                    }
                case RequirementType.DWSwim:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.Flippers]
                        });
                    }
                case RequirementType.DWWaterWalk:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.WaterWalk]
                        });
                    }
                case RequirementType.Armos:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Sword1],
                            RequirementDictionary.Instance[RequirementType.Hammer],
                            RequirementDictionary.Instance[RequirementType.Bow],
                            RequirementDictionary.Instance[RequirementType.Boomerang],
                            RequirementDictionary.Instance[RequirementType.RedBoomerang],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.ExtendMagic2],
                                new AlternativeRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.FireRod],
                                    RequirementDictionary.Instance[RequirementType.IceRod]
                                })
                            }),
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.ExtendMagic1],
                                new AlternativeRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.CaneOfByrna],
                                    RequirementDictionary.Instance[RequirementType.CaneOfSomaria]
                                })
                            })
                        });
                    }
                case RequirementType.Lanmolas:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Sword1],
                            RequirementDictionary.Instance[RequirementType.Hammer],
                            RequirementDictionary.Instance[RequirementType.Bow],
                            RequirementDictionary.Instance[RequirementType.FireRod],
                            RequirementDictionary.Instance[RequirementType.IceRod],
                            RequirementDictionary.Instance[RequirementType.CaneOfByrna],
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria],
                            RequirementDictionary.Instance[RequirementType.SBLanmolasBombs]
                        });
                    }
                case RequirementType.Moldorm:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Sword1],
                            RequirementDictionary.Instance[RequirementType.Hammer]
                        });
                    }
                case RequirementType.HelmasaurKingSB:
                    {
                        return RequirementDictionary.Instance[RequirementType.Sword1];
                    }
                case RequirementType.HelmasaurKing:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.HelmasaurKingSB],
                                new AlternativeRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.SBHelmasaurKingBasic],
                                    RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced]
                                })
                            }),
                            RequirementDictionary.Instance[RequirementType.Bow],
                            RequirementDictionary.Instance[RequirementType.Sword2]
                        });
                    }
                case RequirementType.ArrghusSB:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Hookshot],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Hammer],
                                RequirementDictionary.Instance[RequirementType.Sword1],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.ExtendMagic1],
                                        RequirementDictionary.Instance[RequirementType.Bow]
                                    }),
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.FireRod],
                                        RequirementDictionary.Instance[RequirementType.IceRod]
                                    })
                                })
                            }),
                        });
                    }
                case RequirementType.Arrghus:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.ArrghusSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.SBArrghusBasic],
                                RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                RequirementDictionary.Instance[RequirementType.Swordless],
                                RequirementDictionary.Instance[RequirementType.Sword2]
                            })
                        });
                    }
                case RequirementType.MothulaSB:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Sword1],
                            RequirementDictionary.Instance[RequirementType.Hammer],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.ExtendMagic1],
                                RequirementDictionary.Instance[RequirementType.FireRod]
                            }),
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria],
                            RequirementDictionary.Instance[RequirementType.CaneOfByrna]
                        });
                    }
                case RequirementType.Mothula:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.MothulaSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.SBMothulaBasic],
                                RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                RequirementDictionary.Instance[RequirementType.Sword2],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ExtendMagic1],
                                    RequirementDictionary.Instance[RequirementType.FireRod]
                                })
                            })
                        });
                    }
                case RequirementType.BlindSB:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Sword1],
                            RequirementDictionary.Instance[RequirementType.Hammer],
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria],
                            RequirementDictionary.Instance[RequirementType.CaneOfByrna]
                        });
                    }
                case RequirementType.Blind:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.BlindSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.SBBlindBasic],
                                RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                RequirementDictionary.Instance[RequirementType.Swordless],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.Sword1],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.Cape],
                                        RequirementDictionary.Instance[RequirementType.CaneOfByrna]
                                    })
                                })
                            })
                        });
                    }
                case RequirementType.KholdstareSB:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.MeltThings],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Hammer],
                                RequirementDictionary.Instance[RequirementType.Sword1],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ExtendMagic2],
                                    RequirementDictionary.Instance[RequirementType.FireRod]
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ExtendMagic1],
                                    RequirementDictionary.Instance[RequirementType.FireRod],
                                    RequirementDictionary.Instance[RequirementType.Bombos],
                                    RequirementDictionary.Instance[RequirementType.Swordless]
                                })
                            })
                        });
                    }
                case RequirementType.Kholdstare:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.KholdstareSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.SBKholdstareBasic],
                                RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                RequirementDictionary.Instance[RequirementType.Sword2],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ExtendMagic2],
                                    RequirementDictionary.Instance[RequirementType.FireRod]
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.Bombos],
                                    RequirementDictionary.Instance[RequirementType.UseMedallion],
                                    RequirementDictionary.Instance[RequirementType.ExtendMagic1],
                                    RequirementDictionary.Instance[RequirementType.FireRod]
                                })
                            })
                        });
                    }
                case RequirementType.VitreousSB:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Hammer],
                            RequirementDictionary.Instance[RequirementType.Sword1],
                            RequirementDictionary.Instance[RequirementType.Bow]
                        });
                    }
                case RequirementType.Vitreous:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.VitreousSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.SBVitreousBasic],
                                RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                RequirementDictionary.Instance[RequirementType.Sword2],
                                RequirementDictionary.Instance[RequirementType.Bow]
                            })
                        });
                    }
                case RequirementType.TrinexxSB:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.FireRod],
                            RequirementDictionary.Instance[RequirementType.IceRod],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Sword3],
                                RequirementDictionary.Instance[RequirementType.Hammer],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ExtendMagic1],
                                    RequirementDictionary.Instance[RequirementType.Sword2]
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ExtendMagic2],
                                    RequirementDictionary.Instance[RequirementType.Sword1]
                                })
                            })
                        });
                    }
                case RequirementType.Trinexx:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.TrinexxSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.SBTrinexxBasic],
                                RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                RequirementDictionary.Instance[RequirementType.Swordless],
                                RequirementDictionary.Instance[RequirementType.Sword3],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ExtendMagic1],
                                    RequirementDictionary.Instance[RequirementType.Sword2]
                                })
                            })
                        });
                    }
                case RequirementType.AgaBoss:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Sword1],
                            RequirementDictionary.Instance[RequirementType.Hammer],
                            RequirementDictionary.Instance[RequirementType.Net]
                        });
                    }
                case RequirementType.UnknownBoss:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SequenceBreak],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Armos],
                                RequirementDictionary.Instance[RequirementType.Lanmolas],
                                RequirementDictionary.Instance[RequirementType.Moldorm],
                                RequirementDictionary.Instance[RequirementType.Arrghus],
                                RequirementDictionary.Instance[RequirementType.Mothula],
                                RequirementDictionary.Instance[RequirementType.Blind],
                                RequirementDictionary.Instance[RequirementType.Kholdstare],
                                RequirementDictionary.Instance[RequirementType.Vitreous],
                                RequirementDictionary.Instance[RequirementType.Trinexx]
                            })
                        });
                    }
                case RequirementType.ATBoss:
                case RequirementType.EPBoss:
                case RequirementType.DPBoss:
                case RequirementType.ToHBoss:
                case RequirementType.PoDBoss:
                case RequirementType.SPBoss:
                case RequirementType.SWBoss:
                case RequirementType.TTBoss:
                case RequirementType.IPBoss:
                case RequirementType.MMBoss:
                case RequirementType.TRBoss:
                case RequirementType.GTBoss1:
                case RequirementType.GTBoss2:
                case RequirementType.GTBoss3:
                case RequirementType.GTFinalBoss:
                    {
                        return GetBossRequirement(type);
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}
