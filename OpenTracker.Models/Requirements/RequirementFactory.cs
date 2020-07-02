using OpenTracker.Models.Enums;
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
                RequirementType.None => new StaticRequirement(AccessibilityLevel.Normal),
                RequirementType.Inspect => new StaticRequirement(AccessibilityLevel.Inspect),
                RequirementType.SequenceBreak => new StaticRequirement(AccessibilityLevel.SequenceBreak),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a world state requirement.
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A world state requirement.
        /// </returns>
        private static IRequirement GetWorldStateRequirement(Game game, RequirementType type)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            return type switch
            {
                RequirementType.WorldStateStandardOpen => new WorldStateRequirement(
                    game.Mode, WorldState.StandardOpen),
                RequirementType.WorldStateInverted => new WorldStateRequirement(
                    game.Mode, WorldState.Inverted),
                RequirementType.WorldStateRetro => new WorldStateRequirement(
                    game.Mode, WorldState.Retro),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a item placement requirement.
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A item placement requirement.
        /// </returns>
        private static IRequirement GetItemPlacementRequirement(Game game, RequirementType type)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            return type switch
            {
                RequirementType.ItemPlacementBasic => new ItemPlacementRequirement(
                    game.Mode, ItemPlacement.Basic),
                RequirementType.ItemPlacementAdvanced => new ItemPlacementRequirement(
                    game.Mode, ItemPlacement.Advanced),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a dungeon item shuffle requirement.
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A dungeon item shuffle requirement.
        /// </returns>
        private static IRequirement GetDungeonItemShuffleRequirement(Game game, RequirementType type)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            return type switch
            {
                RequirementType.DungeonItemShuffleStandard => new DungeonItemShuffleRequirement(
                    game.Mode, DungeonItemShuffle.Standard),
                RequirementType.DungeonItemShuffleMapsCompasses => new DungeonItemShuffleRequirement(
                    game.Mode, DungeonItemShuffle.MapsCompasses),
                RequirementType.DungeonItemShuffleMapsCompassesSmallKeys => new DungeonItemShuffleRequirement(
                    game.Mode, DungeonItemShuffle.MapsCompassesSmallKeys),
                RequirementType.DungeonItemShuffleKeysanity => new DungeonItemShuffleRequirement(
                    game.Mode, DungeonItemShuffle.Keysanity),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns a enemy shuffle requirement.
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// A enemy shuffle requirement.
        /// </returns>
        private static IRequirement GetEnemyShuffleRequirement(Game game, RequirementType type)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            return type switch
            {
                RequirementType.EnemyShuffleOff => new EnemyShuffleRequirement(game.Mode, false),
                RequirementType.EnemyShuffleOn => new EnemyShuffleRequirement(game.Mode, true),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns an item exact amount requirement.
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// An item exact amount requirement.
        /// </returns>
        private static IRequirement GetItemExactRequirement(Game game, RequirementType type)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            return type switch
            {
                RequirementType.Swordless => new ItemExactRequirement(game.Items[ItemType.Sword], 0),
                RequirementType.Mushroom => new ItemExactRequirement(game.Items[ItemType.Mushroom], 1),
                RequirementType.BombosMM => new ItemExactRequirement(game.Items[ItemType.BombosDungeons], 1),
                RequirementType.BombosTR => new ItemExactRequirement(game.Items[ItemType.BombosDungeons], 2),
                RequirementType.BombosBoth => new ItemExactRequirement(game.Items[ItemType.BombosDungeons], 3),
                RequirementType.EtherMM => new ItemExactRequirement(game.Items[ItemType.EtherDungeons], 1),
                RequirementType.EtherTR => new ItemExactRequirement(game.Items[ItemType.EtherDungeons], 2),
                RequirementType.EtherBoth => new ItemExactRequirement(game.Items[ItemType.EtherDungeons], 3),
                RequirementType.QuakeMM => new ItemExactRequirement(game.Items[ItemType.QuakeDungeons], 1),
                RequirementType.QuakeTR => new ItemExactRequirement(game.Items[ItemType.QuakeDungeons], 2),
                RequirementType.QuakeBoth => new ItemExactRequirement(game.Items[ItemType.QuakeDungeons], 3),
                RequirementType.NoFlippers => new ItemExactRequirement(game.Items[ItemType.Flippers], 0),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Returns an item requirement.
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="type">
        /// The requirement type.
        /// </param>
        /// <returns>
        /// An item requirement.
        /// </returns>
        private static IRequirement GetItemRequirement(Game game, RequirementType type)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            return type switch
            {
                RequirementType.Sword1 => new ItemRequirement(game.Items[ItemType.Sword], 2),
                RequirementType.Sword2 => new ItemRequirement(game.Items[ItemType.Sword], 3),
                RequirementType.Sword3 => new ItemRequirement(game.Items[ItemType.Sword], 4),
                RequirementType.Shield3 => new ItemRequirement(game.Items[ItemType.Shield], 3),
                RequirementType.Aga => new ItemRequirement(game.Items[ItemType.Aga]),
                RequirementType.Bow => new ItemRequirement(game.Items[ItemType.Bow]),
                RequirementType.Boomerang => new ItemRequirement(game.Items[ItemType.Boomerang]),
                RequirementType.RedBoomerang => new ItemRequirement(game.Items[ItemType.RedBoomerang]),
                RequirementType.Hookshot => new ItemRequirement(game.Items[ItemType.Hookshot]),
                RequirementType.Powder => new ItemRequirement(game.Items[ItemType.Powder]),
                RequirementType.Boots => new ItemRequirement(game.Items[ItemType.Boots]),
                RequirementType.FireRod => new ItemRequirement(game.Items[ItemType.FireRod]),
                RequirementType.IceRod => new ItemRequirement(game.Items[ItemType.IceRod]),
                RequirementType.Bombos => new ItemRequirement(game.Items[ItemType.Bombos]),
                RequirementType.Ether => new ItemRequirement(game.Items[ItemType.Ether]),
                RequirementType.Quake => new ItemRequirement(game.Items[ItemType.Quake]),
                RequirementType.Gloves1 => new ItemRequirement(game.Items[ItemType.Gloves]),
                RequirementType.Gloves2 => new ItemRequirement(game.Items[ItemType.Gloves], 2),
                RequirementType.Lamp => new ItemRequirement(game.Items[ItemType.Lamp]),
                RequirementType.Hammer => new ItemRequirement(game.Items[ItemType.Hammer]),
                RequirementType.Flute => new ItemRequirement(game.Items[ItemType.Flute]),
                RequirementType.Net => new ItemRequirement(game.Items[ItemType.Net]),
                RequirementType.Book => new ItemRequirement(game.Items[ItemType.Book]),
                RequirementType.Shovel => new ItemRequirement(game.Items[ItemType.Shovel]),
                RequirementType.Flippers => new ItemRequirement(game.Items[ItemType.Flippers]),
                RequirementType.Bottle => new ItemRequirement(game.Items[ItemType.Bottle]),
                RequirementType.CaneOfSomaria => new ItemRequirement(game.Items[ItemType.CaneOfSomaria]),
                RequirementType.CaneOfByrna => new ItemRequirement(game.Items[ItemType.CaneOfByrna]),
                RequirementType.Cape => new ItemRequirement(game.Items[ItemType.Cape]),
                RequirementType.Mirror => new ItemRequirement(game.Items[ItemType.Mirror]),
                RequirementType.HalfMagic => new ItemRequirement(game.Items[ItemType.HalfMagic]),
                RequirementType.MoonPearl => new ItemRequirement(game.Items[ItemType.MoonPearl]),
                RequirementType.Aga2 => new ItemRequirement(game.Items[ItemType.Aga2]),
                RequirementType.RedCrystal => new ItemRequirement(game.Items[ItemType.RedCrystal], 2),
                RequirementType.Pendant => new ItemRequirement(game.Items[ItemType.Pendant], 2),
                RequirementType.GreenPendant => new ItemRequirement(game.Items[ItemType.GreenPendant]),
                RequirementType.TRSmallKey2 => new ItemRequirement(game.Items[ItemType.TRSmallKey], 2),
                RequirementType.LightWorldAccess => new ItemRequirement(game.Items[ItemType.LightWorldAccess]),
                RequirementType.DeathMountainEntryAccess => new ItemRequirement(
                    game.Items[ItemType.DeathMountainEntryAccess]),
                RequirementType.DeathMountainExitAccess => new ItemRequirement(
                    game.Items[ItemType.DeathMountainExitAccess]),
                RequirementType.GrassHouseAccess => new ItemRequirement(game.Items[ItemType.GrassHouseAccess]),
                RequirementType.BombHutAccess => new ItemRequirement(game.Items[ItemType.BombHutAccess]),
                RequirementType.RaceGameLedgeAccess => new ItemRequirement(game.Items[ItemType.RaceGameLedgeAccess]),
                RequirementType.SouthOfGroveLedgeAccess => new ItemRequirement(
                    game.Items[ItemType.SouthOfGroveLedgeAccess]),
                RequirementType.DesertLedgeAccess => new ItemRequirement(game.Items[ItemType.DesertLedgeAccess]),
                RequirementType.DesertPalaceBackEntranceAccess => new ItemRequirement(
                    game.Items[ItemType.DesertPalaceBackEntranceAccess]),
                RequirementType.CheckerboardLedgeAccess => new ItemRequirement(
                    game.Items[ItemType.CheckerboardLedgeAccess]),
                RequirementType.LWGraveyardLedgeAccess => new ItemRequirement(
                    game.Items[ItemType.LWGraveyardLedgeAccess]),
                RequirementType.LWKingsTombAccess => new ItemRequirement(
                    game.Items[ItemType.LWKingsTombAccess]),
                RequirementType.HyruleCastleTopAccess => new ItemRequirement(
                    game.Items[ItemType.HyruleCastleTopAccess]),
                RequirementType.WaterfallFairyAccess => new ItemRequirement(
                    game.Items[ItemType.WaterfallFairyAccess]),
                RequirementType.LWWitchAreaAccess => new ItemRequirement(game.Items[ItemType.LWWitchAreaAccess]),
                RequirementType.LakeHyliaFairyIslandAccess => new ItemRequirement(
                    game.Items[ItemType.LakeHyliaFairyIslandAccess]),
                RequirementType.DeathMountainWestBottomAccess => new ItemRequirement(
                    game.Items[ItemType.DeathMountainWestBottomAccess]),
                RequirementType.DeathMountainWestTopAccess => new ItemRequirement(
                    game.Items[ItemType.DeathMountainWestTopAccess]),
                RequirementType.DeathMountainEastBottomAccess => new ItemRequirement(
                    game.Items[ItemType.DeathMountainEastBottomAccess]),
                RequirementType.DeathMountainEastTopConnectorAccess => new ItemRequirement(
                    game.Items[ItemType.DeathMountainEastTopConnectorAccess]),
                RequirementType.SpiralCaveAccess => new ItemRequirement(game.Items[ItemType.SpiralCaveAccess]),
                RequirementType.MimicCaveAccess => new ItemRequirement(game.Items[ItemType.MimicCaveAccess]),
                RequirementType.DeathMountainEastTopAccess => new ItemRequirement(
                    game.Items[ItemType.DeathMountainEastTopAccess]),
                RequirementType.DarkWorldWestAccess => new ItemRequirement(
                    game.Items[ItemType.DarkWorldWestAccess]),
                RequirementType.BumperCaveAccess => new ItemRequirement(game.Items[ItemType.BumperCaveAccess]),
                RequirementType.BumperCaveTopAccess => new ItemRequirement(
                    game.Items[ItemType.BumperCaveTopAccess]),
                RequirementType.HammerHouseAccess => new ItemRequirement(game.Items[ItemType.HammerHouseAccess]),
                RequirementType.HammerPegsAreaAccess => new ItemRequirement(
                    game.Items[ItemType.HammerPegsAreaAccess]),
                RequirementType.DarkWorldSouthAccess => new ItemRequirement(
                    game.Items[ItemType.DarkWorldSouthAccess]),
                RequirementType.MireAreaAccess => new ItemRequirement(game.Items[ItemType.MireAreaAccess]),
                RequirementType.DWWitchAreaAccess => new ItemRequirement(game.Items[ItemType.DWWitchAreaAccess]),
                RequirementType.DarkWorldEastAccess => new ItemRequirement(game.Items[ItemType.DarkWorldEastAccess]),
                RequirementType.IcePalaceAccess => new ItemRequirement(game.Items[ItemType.IcePalaceAccess]),
                RequirementType.DarkWorldSouthEastAccess => new ItemRequirement(
                    game.Items[ItemType.DarkWorldSouthEastAccess]),
                RequirementType.DarkDeathMountainWestBottomAccess => new ItemRequirement(
                    game.Items[ItemType.DarkDeathMountainWestBottomAccess]),
                RequirementType.DarkDeathMountainTopAccess => new ItemRequirement(
                    game.Items[ItemType.DarkDeathMountainTopAccess]),
                RequirementType.DWFloatingIslandAccess => new ItemRequirement(
                    game.Items[ItemType.DWFloatingIslandAccess]),
                RequirementType.DarkDeathMountainEastBottomAccess => new ItemRequirement(
                    game.Items[ItemType.DarkDeathMountainEastBottomAccess]),
                RequirementType.TurtleRockTunnelAccess => new ItemRequirement(
                    game.Items[ItemType.TurtleRockTunnelAccess]),
                RequirementType.TurtleRockSafetyDoorAccess => new ItemRequirement(
                    game.Items[ItemType.TurtleRockSafetyDoorAccess]),
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
        private static IRequirement GetSequenceBreakRequirement(Game game, RequirementType type)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            return type switch
            {
                RequirementType.SBBlindPedestal => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.BlindPedestal]),
                RequirementType.SBBonkOverLedge => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.BonkOverLedge]),
                RequirementType.SBBumperCaveHookshot => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.BumperCaveHookshot]),
                RequirementType.SBTRLaserSkip => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.TRLaserSkip]),
                RequirementType.SBLanmolasBombs => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.LanmolasBombs]),
                RequirementType.SBArrghusBasic => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.ArrghusBasic]),
                RequirementType.SBMothulaBasic => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.MothulaBasic]),
                RequirementType.SBBlindBasic => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.BlindBasic]),
                RequirementType.SBKholdstareBasic => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.KholdstareBasic]),
                RequirementType.SBVitreousBasic => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.VitreousBasic]),
                RequirementType.SBTrinexxBasic => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.TrinexxBasic]),
                RequirementType.SBBombDuplicationAncillaOverload => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.BombDuplicationAncillaOverload]),
                RequirementType.SBBombDuplicationMirror => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.BombDuplicationMirror]),
                RequirementType.SBBombJumpPoDHammerJump => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.BombJumpPoDHammerJump]),
                RequirementType.SBBombJumpSWBigChest => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.BombJumpSWBigChest]),
                RequirementType.SBBombJumpIPBJ => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.BombJumpIPBJ]),
                RequirementType.SBBombJumpIPHookshotGap => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.BombJumpIPHookshotGap]),
                RequirementType.SBBombJumpIPFreezorRoomGap => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.BombJumpIPFreezorRoomGap]),
                RequirementType.SBDarkRoomDeathMountainEntry => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DarkRoomDeathMountainEntry]),
                RequirementType.SBDarkRoomDeathMountainExit => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DarkRoomDeathMountainExit]),
                RequirementType.SBDarkRoomHC => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DarkRoomHC]),
                RequirementType.SBDarkRoomAT => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DarkRoomAT]),
                RequirementType.SBDarkRoomEPRight => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DarkRoomEPRight]),
                RequirementType.SBDarkRoomEPBack => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DarkRoomEPBack]),
                RequirementType.SBDarkRoomPoDDarkBasement => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DarkRoomPoDDarkBasement]),
                RequirementType.SBDarkRoomPoDDarkMaze => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DarkRoomPoDDarkMaze]),
                RequirementType.SBDarkRoomPoDBossArea => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DarkRoomPoDBossArea]),
                RequirementType.SBDarkRoomMM => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DarkRoomMM]),
                RequirementType.SBDarkRoomTR => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DarkRoomTR]),
                RequirementType.SBFakeFlippersFairyRevival => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.FakeFlippersFairyRevival]),
                RequirementType.SBFakeFlippersQirnJump => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.FakeFlippersQirnJump]),
                RequirementType.SBFakeFlippersScreenTransition => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.FakeFlippersScreenTransition]),
                RequirementType.SBFakeFlippersSplashDeletion => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.FakeFlippersSplashDeletion]),
                RequirementType.SBWaterWalk => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.WaterWalk]),
                RequirementType.SBWaterWalkFromWaterfallCave => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.WaterWalkFromWaterfallCave]),
                RequirementType.SBSuperBunnyFallInHole => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.SuperBunnyFallInHole]),
                RequirementType.SBSuperBunnyMirror => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.SuperBunnyMirror]),
                RequirementType.SBCameraUnlock => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.CameraUnlock]),
                RequirementType.SBDungeonRevive => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.DungeonRevive]),
                RequirementType.SBFakePowder => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.FakePowder]),
                RequirementType.SBHover => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.Hover]),
                RequirementType.SBMimicClip => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.MimicClip]),
                RequirementType.SBSpikeCave => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.SpikeCave]),
                RequirementType.SBToHHerapot => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.ToHHerapot]),
                RequirementType.SBIPIceBreaker => new SequenceBreakRequirement(
                    game.SequenceBreaks[SequenceBreakType.IPIceBreaker]),
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
        private static IRequirement GetBossRequirement(Game game, RequirementType type)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            return type switch
            {
                RequirementType.ATBoss => new BossRequirement(game, game.BossPlacements[BossPlacementID.ATBoss]),
                RequirementType.EPBoss => new BossRequirement(game, game.BossPlacements[BossPlacementID.EPBoss]),
                RequirementType.DPBoss => new BossRequirement(game, game.BossPlacements[BossPlacementID.DPBoss]),
                RequirementType.ToHBoss => new BossRequirement(game, game.BossPlacements[BossPlacementID.ToHBoss]),
                RequirementType.PoDBoss => new BossRequirement(game, game.BossPlacements[BossPlacementID.PoDBoss]),
                RequirementType.SPBoss => new BossRequirement(game, game.BossPlacements[BossPlacementID.SPBoss]),
                RequirementType.SWBoss => new BossRequirement(game, game.BossPlacements[BossPlacementID.SWBoss]),
                RequirementType.TTBoss => new BossRequirement(game, game.BossPlacements[BossPlacementID.TTBoss]),
                RequirementType.IPBoss => new BossRequirement(game, game.BossPlacements[BossPlacementID.IPBoss]),
                RequirementType.MMBoss => new BossRequirement(game, game.BossPlacements[BossPlacementID.MMBoss]),
                RequirementType.TRBoss => new BossRequirement(game, game.BossPlacements[BossPlacementID.TRBoss]),
                RequirementType.GTBoss1 => new BossRequirement(game, game.BossPlacements[BossPlacementID.GTBoss1]),
                RequirementType.GTBoss2 => new BossRequirement(game, game.BossPlacements[BossPlacementID.GTBoss2]),
                RequirementType.GTBoss3 => new BossRequirement(game, game.BossPlacements[BossPlacementID.GTBoss3]),
                RequirementType.GTFinalBoss => new BossRequirement(
                    game, game.BossPlacements[BossPlacementID.GTFinalBoss]),
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
        private static IRequirement GetRequirement(Game game, RequirementType type)
        {
            switch (type)
            {
                case RequirementType.None:
                case RequirementType.Inspect:
                case RequirementType.SequenceBreak:
                    {
                        return GetStaticRequirement(type);
                    }
                case RequirementType.WorldStateStandardOpen:
                case RequirementType.WorldStateInverted:
                case RequirementType.WorldStateRetro:
                    {
                        return GetWorldStateRequirement(game, type);
                    }
                case RequirementType.DungeonItemShuffleStandard:
                case RequirementType.DungeonItemShuffleMapsCompasses:
                case RequirementType.DungeonItemShuffleMapsCompassesSmallKeys:
                case RequirementType.DungeonItemShuffleKeysanity:
                    {
                        return GetDungeonItemShuffleRequirement(game, type);
                    }
                case RequirementType.ItemPlacementBasic:
                case RequirementType.ItemPlacementAdvanced:
                    {
                        return GetItemPlacementRequirement(game, type);
                    }
                case RequirementType.EnemyShuffleOff:
                case RequirementType.EnemyShuffleOn:
                    {
                        return GetEnemyShuffleRequirement(game, type);
                    }
                case RequirementType.SmallKeyShuffleOff:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.WorldStateStandardOpen],
                                game.Requirements[RequirementType.WorldStateInverted]
                            }),
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.DungeonItemShuffleStandard],
                                game.Requirements[RequirementType.DungeonItemShuffleMapsCompasses]
                            })
                        });
                    }
                case RequirementType.SmallKeyShuffleOn:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.WorldStateRetro],
                            game.Requirements[RequirementType.DungeonItemShuffleMapsCompassesSmallKeys],
                            game.Requirements[RequirementType.DungeonItemShuffleKeysanity]
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
                        return GetItemExactRequirement(game, type);
                    }
                case RequirementType.Sword1:
                case RequirementType.Sword2:
                case RequirementType.Sword3:
                case RequirementType.Shield3:
                case RequirementType.Aga:
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
                case RequirementType.TRSmallKey2:
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
                case RequirementType.IcePalaceAccess:
                case RequirementType.DarkWorldSouthEastAccess:
                case RequirementType.DarkDeathMountainWestBottomAccess:
                case RequirementType.DarkDeathMountainTopAccess:
                case RequirementType.DWFloatingIslandAccess:
                case RequirementType.DarkDeathMountainEastBottomAccess:
                case RequirementType.TurtleRockTunnelAccess:
                case RequirementType.TurtleRockSafetyDoorAccess:
                    {
                        return GetItemRequirement(game, type);
                    }
                case RequirementType.SBBlindPedestal:
                case RequirementType.SBBonkOverLedge:
                case RequirementType.SBBumperCaveHookshot:
                case RequirementType.SBTRLaserSkip:
                case RequirementType.SBLanmolasBombs:
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
                        return GetSequenceBreakRequirement(game, type);
                    }
                case RequirementType.GTCrystal:
                    {
                        return new CrystalRequirement(game);
                    }
                case RequirementType.LightWorld:
                    {
                        return new RequirementNodeRequirement(game.RequirementNodes[RequirementNodeID.LightWorld]);
                    }
                case RequirementType.AllMedallions:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Bombos],
                            game.Requirements[RequirementType.Ether],
                            game.Requirements[RequirementType.Quake]
                        });
                    }
                case RequirementType.ExtendMagic1:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Bottle],
                                game.Requirements[RequirementType.HalfMagic]
                            });
                    }
                case RequirementType.ExtendMagic2:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Bottle],
                                game.Requirements[RequirementType.HalfMagic]
                            });
                    }
                case RequirementType.FireRodDarkRoom:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.FireRod],
                            game.Requirements[RequirementType.ItemPlacementAdvanced]
                        });
                    }
                case RequirementType.UseMedallion:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Swordless],
                            game.Requirements[RequirementType.Sword1]
                        });
                    }
                case RequirementType.MeltThings:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.FireRod],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Bombos],
                                game.Requirements[RequirementType.UseMedallion]
                            })
                        });
                    }
                case RequirementType.NotBunnyLW:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.WorldStateStandardOpen],
                            game.Requirements[RequirementType.WorldStateRetro],
                            game.Requirements[RequirementType.MoonPearl]
                        });
                    }
                case RequirementType.NotBunnyDW:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.WorldStateInverted],
                            game.Requirements[RequirementType.MoonPearl]
                        });
                    }
                case RequirementType.Armos:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Sword1],
                            game.Requirements[RequirementType.Hammer],
                            game.Requirements[RequirementType.Bow],
                            game.Requirements[RequirementType.Boomerang],
                            game.Requirements[RequirementType.RedBoomerang],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.ExtendMagic2],
                                new AlternativeRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.FireRod],
                                    game.Requirements[RequirementType.IceRod]
                                })
                            }),
                            new AggregateRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.ExtendMagic1],
                                new AlternativeRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.CaneOfByrna],
                                    game.Requirements[RequirementType.CaneOfSomaria]
                                })
                            })
                        });
                    }
                case RequirementType.Lanmolas:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Sword1],
                            game.Requirements[RequirementType.Hammer],
                            game.Requirements[RequirementType.Bow],
                            game.Requirements[RequirementType.FireRod],
                            game.Requirements[RequirementType.IceRod],
                            game.Requirements[RequirementType.CaneOfByrna],
                            game.Requirements[RequirementType.CaneOfSomaria],
                            game.Requirements[RequirementType.SBLanmolasBombs]
                        });
                    }
                case RequirementType.Moldorm:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Sword1],
                            game.Requirements[RequirementType.Hammer]
                        });
                    }
                case RequirementType.ArrghusSB:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Hookshot],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Hammer],
                                game.Requirements[RequirementType.Sword1],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        game.Requirements[RequirementType.ExtendMagic1],
                                        game.Requirements[RequirementType.Bow]
                                    }),
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        game.Requirements[RequirementType.FireRod],
                                        game.Requirements[RequirementType.IceRod]
                                    })
                                })
                            }),
                        });
                    }
                case RequirementType.Arrghus:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.ArrghusSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.SBArrghusBasic],
                                game.Requirements[RequirementType.ItemPlacementAdvanced],
                                game.Requirements[RequirementType.Swordless],
                                game.Requirements[RequirementType.Sword2]
                            })
                        });
                    }
                case RequirementType.MothulaSB:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Sword1],
                            game.Requirements[RequirementType.Hammer],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.ExtendMagic1],
                                game.Requirements[RequirementType.FireRod]
                            }),
                            game.Requirements[RequirementType.CaneOfSomaria],
                            game.Requirements[RequirementType.CaneOfByrna]
                        });
                    }
                case RequirementType.Mothula:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.MothulaSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.SBMothulaBasic],
                                game.Requirements[RequirementType.ItemPlacementAdvanced],
                                game.Requirements[RequirementType.Sword2],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.ExtendMagic1],
                                    game.Requirements[RequirementType.FireRod]
                                })
                            })
                        });
                    }
                case RequirementType.BlindSB:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Sword1],
                            game.Requirements[RequirementType.Hammer],
                            game.Requirements[RequirementType.CaneOfSomaria],
                            game.Requirements[RequirementType.CaneOfByrna]
                        });
                    }
                case RequirementType.Blind:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.BlindSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.SBBlindBasic],
                                game.Requirements[RequirementType.ItemPlacementAdvanced],
                                game.Requirements[RequirementType.Swordless],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.Sword1],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        game.Requirements[RequirementType.Cape],
                                        game.Requirements[RequirementType.CaneOfByrna]
                                    })
                                })
                            })
                        });
                    }
                case RequirementType.KholdstareSB:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.MeltThings],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Hammer],
                                game.Requirements[RequirementType.Sword1],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.ExtendMagic2],
                                    game.Requirements[RequirementType.FireRod]
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.ExtendMagic1],
                                    game.Requirements[RequirementType.FireRod],
                                    game.Requirements[RequirementType.Bombos],
                                    game.Requirements[RequirementType.Swordless]
                                })
                            })
                        });
                    }
                case RequirementType.Kholdstare:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.KholdstareSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.SBKholdstareBasic],
                                game.Requirements[RequirementType.Sword2],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.ExtendMagic2],
                                    game.Requirements[RequirementType.FireRod]
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.Bombos],
                                    game.Requirements[RequirementType.UseMedallion],
                                    game.Requirements[RequirementType.ExtendMagic1],
                                    game.Requirements[RequirementType.FireRod]
                                })
                            })
                        });
                    }
                case RequirementType.VitreousSB:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Hammer],
                            game.Requirements[RequirementType.Sword1],
                            game.Requirements[RequirementType.Bow]
                        });
                    }
                case RequirementType.Vitreous:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.VitreousSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.SBVitreousBasic],
                                game.Requirements[RequirementType.ItemPlacementAdvanced],
                                game.Requirements[RequirementType.Sword2],
                                game.Requirements[RequirementType.Bow]
                            })
                        });
                    }
                case RequirementType.TrinexxSB:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.FireRod],
                            game.Requirements[RequirementType.IceRod],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Sword3],
                                game.Requirements[RequirementType.Hammer],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.ExtendMagic1],
                                    game.Requirements[RequirementType.Sword2]
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.ExtendMagic2],
                                    game.Requirements[RequirementType.Sword1]
                                })
                            })
                        });
                    }
                case RequirementType.Trinexx:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.TrinexxSB],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.SBTrinexxBasic],
                                game.Requirements[RequirementType.ItemPlacementAdvanced],
                                game.Requirements[RequirementType.Swordless],
                                game.Requirements[RequirementType.Sword3],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.ExtendMagic1],
                                    game.Requirements[RequirementType.Sword2]
                                })
                            })
                        });
                    }
                case RequirementType.AgaBoss:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Sword1],
                            game.Requirements[RequirementType.Hammer],
                            game.Requirements[RequirementType.Net]
                        });
                    }
                case RequirementType.UnknownBoss:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SequenceBreak],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Armos],
                                game.Requirements[RequirementType.Lanmolas],
                                game.Requirements[RequirementType.Moldorm],
                                game.Requirements[RequirementType.Arrghus],
                                game.Requirements[RequirementType.Mothula],
                                game.Requirements[RequirementType.Blind],
                                game.Requirements[RequirementType.Kholdstare],
                                game.Requirements[RequirementType.Vitreous],
                                game.Requirements[RequirementType.Trinexx]
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
                        return GetBossRequirement(game, type);
                    }
                case RequirementType.ATBarrier:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Cape],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Swordless],
                                game.Requirements[RequirementType.Hammer]
                            }),
                            game.Requirements[RequirementType.Sword2]
                        });
                    }
                case RequirementType.BombDuplicationAncillaOverload:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBBombDuplicationAncillaOverload],
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.Bow],
                            game.Requirements[RequirementType.CaneOfSomaria],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Boomerang],
                                game.Requirements[RequirementType.RedBoomerang]
                            })
                        });
                    }
                case RequirementType.BombDuplicationMirror:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.Flippers],
                            game.Requirements[RequirementType.Mirror]
                        });
                    }
                case RequirementType.BombJumpPoDHammerJump:
                    {
                        return game.Requirements[RequirementType.SBBombJumpPoDHammerJump];
                    }
                case RequirementType.BombJumpSWBigChest:
                    {
                        return game.Requirements[RequirementType.SBBombJumpSWBigChest];
                    }
                case RequirementType.BombJumpIPBJ:
                    {
                        return game.Requirements[RequirementType.SBBombJumpIPBJ];
                    }
                case RequirementType.BombJumpIPHookshotGap:
                    {
                        return game.Requirements[RequirementType.SBBombJumpIPHookshotGap];
                    }
                case RequirementType.BombJumpIPFreezorRoomGap:
                    {
                        return game.Requirements[RequirementType.SBBombJumpIPFreezorRoomGap];
                    }
                case RequirementType.BonkOverLedge:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Boots],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.SBBonkOverLedge],
                                game.Requirements[RequirementType.ItemPlacementAdvanced]
                            })
                        });
                    }
                case RequirementType.BumperCave:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Cape],
                            game.Requirements[RequirementType.MoonPearl],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Hookshot],
                                game.Requirements[RequirementType.ItemPlacementAdvanced],
                                game.Requirements[RequirementType.SBBumperCaveHookshot]
                            })
                        });
                    }
                case RequirementType.CameraUnlock:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBCameraUnlock],
                            game.Requirements[RequirementType.Bottle]
                        });
                    }
                case RequirementType.Curtains:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Swordless],
                            game.Requirements[RequirementType.Sword1]
                        });
                    }
                case RequirementType.DarkRoomDeathMountainEntry:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBDarkRoomDeathMountainEntry],
                            game.Requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomDeathMountainExit:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBDarkRoomDeathMountainExit],
                            game.Requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomHC:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBDarkRoomHC],
                            game.Requirements[RequirementType.Lamp],
                            game.Requirements[RequirementType.FireRodDarkRoom]
                        });
                    }
                case RequirementType.DarkRoomAT:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBDarkRoomAT],
                            game.Requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomEPRight:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBDarkRoomEPRight],
                            game.Requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomEPBack:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBDarkRoomEPBack],
                            game.Requirements[RequirementType.Lamp],
                            game.Requirements[RequirementType.FireRodDarkRoom]
                        });
                    }
                case RequirementType.DarkRoomPoDDarkBasement:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBDarkRoomPoDDarkBasement],
                            game.Requirements[RequirementType.Lamp],
                            game.Requirements[RequirementType.FireRodDarkRoom]
                        });
                    }
                case RequirementType.DarkRoomPoDDarkMaze:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBDarkRoomPoDDarkMaze],
                            game.Requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomPoDBossArea:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBDarkRoomPoDBossArea],
                            game.Requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomMM:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBDarkRoomMM],
                            game.Requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DarkRoomTR:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBDarkRoomTR],
                            game.Requirements[RequirementType.Lamp]
                        });
                    }
                case RequirementType.DungeonRevive:
                    {
                        return game.Requirements[RequirementType.SBDungeonRevive];
                    }
                case RequirementType.FakeFlippersFairyRevival:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBFakeFlippersFairyRevival],
                            game.Requirements[RequirementType.Bottle]
                        });
                    }
                case RequirementType.FakeFlippersSplashDeletion:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Bow],
                                game.Requirements[RequirementType.RedBoomerang],
                                game.Requirements[RequirementType.CaneOfSomaria],
                                game.Requirements[RequirementType.IceRod]
                            }),
                            game.Requirements[RequirementType.SBFakeFlippersSplashDeletion]
                        });
                    }
                case RequirementType.FireSource:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Lamp],
                            game.Requirements[RequirementType.FireRod]
                        });
                    }
                case RequirementType.Hover:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBHover],
                            game.Requirements[RequirementType.Boots]
                        });
                    }
                case RequirementType.LaserBridge:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBTRLaserSkip],
                            game.Requirements[RequirementType.ItemPlacementAdvanced],
                            game.Requirements[RequirementType.Cape],
                            game.Requirements[RequirementType.CaneOfByrna],
                            game.Requirements[RequirementType.Shield3]
                        });
                    }
                case RequirementType.Pedestal:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Pendant],
                                game.Requirements[RequirementType.GreenPendant],
                                new AlternativeRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.SBBlindPedestal],
                                    game.Requirements[RequirementType.ItemPlacementAdvanced],
                                    game.Requirements[RequirementType.Book]
                                })
                            }),
                            new AggregateRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Book],
                                game.Requirements[RequirementType.Inspect]
                            })
                        });
                    }
                case RequirementType.RedEyegoreGoriya:
                    {
                        return new AlternativeRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.EnemyShuffleOn],
                            game.Requirements[RequirementType.Bow]
                        });
                    }
                case RequirementType.SPEntry:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.MoonPearl],
                            game.Requirements[RequirementType.Mirror],
                            game.Requirements[RequirementType.LightWorld]
                        });
                    }
                case RequirementType.SuperBunnyFallInHole:
                    {
                        return game.Requirements[RequirementType.SBSuperBunnyFallInHole];
                    }
                case RequirementType.SuperBunnyMirror:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBSuperBunnyMirror],
                            game.Requirements[RequirementType.Mirror]
                        });
                    }
                case RequirementType.Tablet:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.Book],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Inspect],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.Swordless],
                                    game.Requirements[RequirementType.Hammer]
                                }),
                                game.Requirements[RequirementType.Sword2]
                            })
                        });
                    }
                case RequirementType.ToHHerapot:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBToHHerapot],
                            game.Requirements[RequirementType.Hookshot]
                        });
                    }
                case RequirementType.IPIceBreaker:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBIPIceBreaker],
                            game.Requirements[RequirementType.CaneOfSomaria]
                        });
                    }
                case RequirementType.MMMedallion:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.UseMedallion],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.AllMedallions],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.Bombos],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        game.Requirements[RequirementType.BombosMM],
                                        game.Requirements[RequirementType.BombosBoth]
                                    })
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.Ether],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        game.Requirements[RequirementType.EtherMM],
                                        game.Requirements[RequirementType.EtherBoth]
                                    })
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.Quake],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        game.Requirements[RequirementType.QuakeMM],
                                        game.Requirements[RequirementType.QuakeBoth]
                                    })
                                }),
                            })
                        });
                    }
                case RequirementType.TRMedallion:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.UseMedallion],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.AllMedallions],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.Bombos],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        game.Requirements[RequirementType.BombosTR],
                                        game.Requirements[RequirementType.BombosBoth]
                                    })
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.Ether],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        game.Requirements[RequirementType.EtherTR],
                                        game.Requirements[RequirementType.EtherBoth]
                                    })
                                }),
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.Quake],
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        game.Requirements[RequirementType.QuakeTR],
                                        game.Requirements[RequirementType.QuakeBoth]
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
                                game.Requirements[RequirementType.SmallKeyShuffleOn],
                                game.Requirements[RequirementType.TRSmallKey2]
                            }),
                            new AggregateRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.SmallKeyShuffleOff],
                                new AlternativeRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.FireRod],
                                    game.Requirements[RequirementType.SequenceBreak]
                                })
                            })
                        });
                    }
                case RequirementType.WaterWalk:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBWaterWalk],
                            game.Requirements[RequirementType.Boots]
                        });
                    }
                case RequirementType.WaterWalkFromWaterfallCave:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBWaterWalkFromWaterfallCave],
                            game.Requirements[RequirementType.NoFlippers],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.MoonPearl],
                                game.Requirements[RequirementType.WaterfallFairyAccess]
                            })
                        });
                    }
                case RequirementType.LWDash:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyLW],
                            game.Requirements[RequirementType.Boots]
                        });
                    }
                case RequirementType.LWFakeFlippersFairyRevival:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyLW],
                            game.Requirements[RequirementType.FakeFlippersFairyRevival]
                        });
                    }
                case RequirementType.LWFakeFlippersScreenTransition:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.SBFakeFlippersScreenTransition],
                            game.Requirements[RequirementType.NotBunnyLW]
                        });
                    }
                case RequirementType.LWFakeFlippersSplashDeletion:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyLW],
                            game.Requirements[RequirementType.FakeFlippersSplashDeletion]
                        });
                    }
                case RequirementType.LWFlute:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.WorldStateStandardOpen],
                                game.Requirements[RequirementType.WorldStateRetro]
                            }),
                            game.Requirements[RequirementType.Flute]
                        });
                    }
                case RequirementType.LWHammer:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyLW],
                            game.Requirements[RequirementType.Hammer]
                        });
                    }
                case RequirementType.LWHookshot:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyLW],
                            game.Requirements[RequirementType.Hookshot]
                        });
                    }
                case RequirementType.LWLift1:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyLW],
                            game.Requirements[RequirementType.Gloves1]
                        });
                    }
                case RequirementType.LWLift2:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyLW],
                            game.Requirements[RequirementType.Gloves2]
                        });
                    }
                case RequirementType.LWPowder:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyLW],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.Powder],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.SBFakePowder],
                                    game.Requirements[RequirementType.Mushroom],
                                    game.Requirements[RequirementType.CaneOfSomaria]
                                })
                            })
                        });
                    }
                case RequirementType.LWShovel:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyLW],
                            game.Requirements[RequirementType.Shovel]
                        });
                    }
                case RequirementType.LWSwim:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyLW],
                            game.Requirements[RequirementType.Flippers]
                        });
                    }
                case RequirementType.LWWaterWalk:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyLW],
                            game.Requirements[RequirementType.WaterWalk]
                        });
                    }
                case RequirementType.DWDash:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.Boots]
                        });
                    }
                case RequirementType.DWFakeFlippersFairyRevival:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.FakeFlippersFairyRevival]
                        });
                    }
                case RequirementType.DWFakeFlippersQirnJump:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.SBFakeFlippersQirnJump]
                        });
                    }
                case RequirementType.DWFakeFlippersSplashDeletion:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.FakeFlippersSplashDeletion]
                        });
                    }
                case RequirementType.DWFireRod:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.FireRod]
                        });
                    }
                case RequirementType.DWFlute:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.WorldStateInverted],
                            game.Requirements[RequirementType.MoonPearl],
                            game.Requirements[RequirementType.Flute]
                        });
                    }
                case RequirementType.DWHammer:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.Hammer]
                        });
                    }
                case RequirementType.DWHookshot:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.Hookshot]
                        });
                    }
                case RequirementType.DWHover:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.Hover]
                        });
                    }
                case RequirementType.DWLift1:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.Gloves1]
                        });
                    }
                case RequirementType.DWLift2:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.Gloves2]
                        });
                    }
                case RequirementType.DWSpikeCave:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.Hammer],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                game.Requirements[RequirementType.SequenceBreak],
                                game.Requirements[RequirementType.CaneOfByrna],
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    game.Requirements[RequirementType.Cape],
                                    game.Requirements[RequirementType.ExtendMagic1]
                                })
                            })
                        });
                    }
                case RequirementType.DWSwim:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.Flippers]
                        });
                    }
                case RequirementType.DWWaterWalk:
                    {
                        return new AggregateRequirement(new List<IRequirement>
                        {
                            game.Requirements[RequirementType.NotBunnyDW],
                            game.Requirements[RequirementType.WaterWalk]
                        });
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }

        internal static void GetAllRequirements(
            Game game, Dictionary<RequirementType, IRequirement> requirements)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            if (requirements == null)
            {
                throw new ArgumentNullException(nameof(requirements));
            }

            foreach (RequirementType type in Enum.GetValues(typeof(RequirementType)))
            {
                requirements.Add(type, GetRequirement(game, type));
            }
        }
    }
}
