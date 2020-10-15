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
        /// Returns a map requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A map requirement.
        /// </returns>
        private static IRequirement GetMapShuffleRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.MapShuffleOff => new MapShuffleRequirement(false),
                RequirementType.MapShuffleOn => new MapShuffleRequirement(true),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a compass requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A compass requirement.
        /// </returns>
        private static IRequirement GetCompassShuffleRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.CompassShuffleOff => new CompassShuffleRequirement(false),
                RequirementType.CompassShuffleOn => new CompassShuffleRequirement(true),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a small key requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A small key requirement.
        /// </returns>
        private static IRequirement GetSmallKeyShuffleRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.SmallKeyShuffleOff => new SmallKeyShuffleRequirement(false),
                RequirementType.SmallKeyShuffleOn => new SmallKeyShuffleRequirement(true),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a big key requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A big key requirement.
        /// </returns>
        private static IRequirement GetBigKeyShuffleRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.BigKeyShuffleOff => new BigKeyShuffleRequirement(false),
                RequirementType.BigKeyShuffleOn => new BigKeyShuffleRequirement(true),
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
                RequirementType.EntranceShuffleNone =>
                    new EntranceShuffleRequirement(EntranceShuffle.None),
                RequirementType.EntranceShuffleDungeon =>
                    new EntranceShuffleRequirement(EntranceShuffle.Dungeon),
                RequirementType.EntranceShuffleAll =>
                    new EntranceShuffleRequirement(EntranceShuffle.All),
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
        /// Returns a generic keys requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A generic keys requirement.
        /// </returns>
        private static IRequirement GetGenericKeysRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.GenericKeys =>
                    new GenericKeysRequirement(true),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a take any locations requirement.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A take any locations requirement.
        /// </returns>
        private static IRequirement GetTakeAnyLocationsRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.TakeAnyLocations =>
                    new TakeAnyLocationsRequirement(true),
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
                RequirementType.Aga1 => new ItemRequirement(
                    PrizeDictionary.Instance[PrizeType.Aga1]),
                RequirementType.Aga2 => new ItemRequirement(
                    PrizeDictionary.Instance[PrizeType.Aga2]),
                RequirementType.RedCrystal => new ItemRequirement(
                    PrizeDictionary.Instance[PrizeType.RedCrystal], 2),
                RequirementType.Pendant => new ItemRequirement(
                    PrizeDictionary.Instance[PrizeType.Pendant], 2),
                RequirementType.GreenPendant => new ItemRequirement(
                    PrizeDictionary.Instance[PrizeType.GreenPendant]),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a new small key requirement instance.
        /// </summary>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A new small key requirement instance.
        /// </returns>
        private static IRequirement GetSmallKeyRequirement(RequirementType type)
        {
            return type switch
            {
                RequirementType.TRSmallKey2 => new SmallKeyRequirement(
                    ItemDictionary.Instance[ItemType.TRSmallKey], 2),
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
        public static IRequirement GetRequirement(RequirementType type)
        {
            switch (type)
            {
                case RequirementType.NoRequirement:
                case RequirementType.Inspect:
                case RequirementType.SequenceBreak:
                    {
                        return GetStaticRequirement(type);
                    }
                case RequirementType.ItemPlacementBasic:
                case RequirementType.ItemPlacementAdvanced:
                    {
                        return GetItemPlacementRequirement(type);
                    }
                case RequirementType.MapShuffleOff:
                case RequirementType.MapShuffleOn:
                    {
                        return GetMapShuffleRequirement(type);
                    }
                case RequirementType.CompassShuffleOff:
                case RequirementType.CompassShuffleOn:
                    {
                        return GetCompassShuffleRequirement(type);
                    }
                case RequirementType.SmallKeyShuffleOff:
                case RequirementType.SmallKeyShuffleOn:
                    {
                        return GetSmallKeyShuffleRequirement(type);
                    }
                case RequirementType.BigKeyShuffleOff:
                case RequirementType.BigKeyShuffleOn:
                    {
                        return GetBigKeyShuffleRequirement(type);
                    }
                case RequirementType.WorldStateStandardOpen:
                case RequirementType.WorldStateInverted:
                    {
                        return GetWorldStateRequirement(type);
                    }
                case RequirementType.EntranceShuffleNone:
                case RequirementType.EntranceShuffleDungeon:
                case RequirementType.EntranceShuffleAll:
                    {
                        return GetEntranceShuffleRequirement(type);
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
                case RequirementType.GuaranteedBossItemsOff:
                case RequirementType.GuaranteedBossItemsOn:
                    {
                        return GetGuaranteedBossItemsRequirement(type);
                    }
                case RequirementType.GenericKeys:
                    {
                        return GetGenericKeysRequirement(type);
                    }
                case RequirementType.TakeAnyLocations:
                    {
                        return GetTakeAnyLocationsRequirement(type);
                    }
                case RequirementType.NoKeyShuffle:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                            RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff]
                        });
                    }
                case RequirementType.SmallKeyShuffleOnly:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn],
                            RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff]
                        });
                    }
                case RequirementType.BigKeyShuffleOnly:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                            RequirementDictionary.Instance[RequirementType.BigKeyShuffleOn]
                        });
                    }
                case RequirementType.AllKeyShuffle:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn],
                            RequirementDictionary.Instance[RequirementType.BigKeyShuffleOn]
                        });
                    }
                case RequirementType.EntranceShuffleNotAll:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNone],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeon]
                        });
                    }
                case RequirementType.EntranceShuffleDungeonOn:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeon],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleAll]
                        });
                    }
                case RequirementType.WorldStateStandardOpenEntranceShuffleNone:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]
                        });
                    }
                case RequirementType.WorldStateStandardOpenEntranceShuffleNotAll:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]
                        });
                    }
                case RequirementType.WorldStateInvertedEntranceShuffleNone:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]
                        });
                    }
                case RequirementType.WorldStateInvertedEntranceShuffleNotAll:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]
                        });
                    }
                case RequirementType.WorldStateInvertedEntranceShuffleAll:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleAll]
                        });
                    }
                case RequirementType.TakeAnyLocationsEntranceShuffleNotAll:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.TakeAnyLocations],
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]
                        });
                    }
                case RequirementType.RaceIllegalTracking:
                    {
                        return new RaceIllegalTrackingRequirement(true);
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
                    {
                        return GetItemRequirement(type);
                    }
                case RequirementType.TRSmallKey2:
                    {
                        return GetSmallKeyRequirement(type);
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
                            RequirementDictionary.Instance[RequirementType.Flippers],
                            RequirementDictionary.Instance[RequirementType.Mirror]
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
                case RequirementType.BumperCaveGap:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Hookshot],
                            RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                            RequirementDictionary.Instance[RequirementType.SBBumperCaveHookshot]
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
                case RequirementType.FakeFlippersQirnJump:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBFakeFlippersQirnJump]
                        });
                    }
                case RequirementType.FakeFlippersScreenTransition:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SBFakeFlippersScreenTransition]
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
                case RequirementType.MagicBat:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.SBFakePowder],
                                RequirementDictionary.Instance[RequirementType.Mushroom],
                                RequirementDictionary.Instance[RequirementType.CaneOfSomaria]
                            }),
                            RequirementDictionary.Instance[RequirementType.Powder]
                        });
                    }
                case RequirementType.MimicClip:
                    {
                        return RequirementDictionary.Instance[RequirementType.SBMimicClip];
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
                case RequirementType.SpikeCave:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.SequenceBreak],
                            RequirementDictionary.Instance[RequirementType.CaneOfByrna],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.Cape],
                                RequirementDictionary.Instance[RequirementType.ExtendMagic1]
                            })
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
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.MoonPearl],
                                RequirementDictionary.Instance[RequirementType.EntranceShuffleAll]
                            })
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
                case RequirementType.DWMirror:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen],
                            RequirementDictionary.Instance[RequirementType.Mirror]
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
