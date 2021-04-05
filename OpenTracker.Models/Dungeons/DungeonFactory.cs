using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Nodes;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    ///     This class contains the creation logic for dungeons.
    /// </summary>
    public class DungeonFactory : IDungeonFactory
    {
        private readonly IItemDictionary _items;
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly IDungeon.Factory _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="items">
        ///     The item dictionary.
        /// </param>
        /// <param name="overworldNodes">
        ///     The requirement node dictionary.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating dungeons.
        /// </param>
        public DungeonFactory(
            IItemDictionary items, IOverworldNodeDictionary overworldNodes, IDungeon.Factory factory)
        {
            _items = items;
            _overworldNodes = overworldNodes;

            _factory = factory;
        }

        public IDungeon GetDungeon(DungeonID id)
        {
            return _factory(
                id, GetDungeonMapItem(id), GetDungeonCompassItem(id), GetDungeonSmallKeyItem(id),
                GetDungeonBigKeyItem(id), GetDungeonItems(id), GetDungeonBosses(id),
                GetDungeonSmallKeyDrops(id), GetDungeonBigKeyDrops(id),
                GetDungeonSmallKeyDoors(id), GetDungeonBigKeyDoors(id), GetDungeonNodes(id),
                GetDungeonEntryNodes(id));
        }

        /// <summary>
        ///     Returns the map item for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     The map item.
        /// </returns>
        private ICappedItem? GetDungeonMapItem(DungeonID id)
        {
            var item = id switch
            {
                DungeonID.HyruleCastle => _items[ItemType.HCMap],
                DungeonID.AgahnimTower => null,
                DungeonID.EasternPalace => _items[ItemType.EPMap],
                DungeonID.DesertPalace => _items[ItemType.DPMap],
                DungeonID.TowerOfHera => _items[ItemType.ToHMap],
                DungeonID.PalaceOfDarkness => _items[ItemType.PoDMap],
                DungeonID.SwampPalace => _items[ItemType.SPMap],
                DungeonID.SkullWoods => _items[ItemType.SWMap],
                DungeonID.ThievesTown => _items[ItemType.TTMap],
                DungeonID.IcePalace => _items[ItemType.IPMap],
                DungeonID.MiseryMire => _items[ItemType.MMMap],
                DungeonID.TurtleRock => _items[ItemType.TRMap],
                DungeonID.GanonsTower => _items[ItemType.GTMap],
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };

            return item as ICappedItem;
        }

        /// <summary>
        ///     Returns the compass item for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     The compass item.
        /// </returns>
        private ICappedItem? GetDungeonCompassItem(DungeonID id)
        {
            var item = id switch
            {
                DungeonID.HyruleCastle => null,
                DungeonID.AgahnimTower => null,
                DungeonID.EasternPalace => _items[ItemType.EPCompass],
                DungeonID.DesertPalace => _items[ItemType.DPCompass],
                DungeonID.TowerOfHera => _items[ItemType.ToHCompass],
                DungeonID.PalaceOfDarkness => _items[ItemType.PoDCompass],
                DungeonID.SwampPalace => _items[ItemType.SPCompass],
                DungeonID.SkullWoods => _items[ItemType.SWCompass],
                DungeonID.ThievesTown => _items[ItemType.TTCompass],
                DungeonID.IcePalace => _items[ItemType.IPCompass],
                DungeonID.MiseryMire => _items[ItemType.MMCompass],
                DungeonID.TurtleRock => _items[ItemType.TRCompass],
                DungeonID.GanonsTower => _items[ItemType.GTCompass],
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };

            return item as ICappedItem;
        }

        /// <summary>
        ///     Returns the small key item for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     The small key item.
        /// </returns>
        private ISmallKeyItem GetDungeonSmallKeyItem(DungeonID id)
        {
            var item = id switch
            {
                DungeonID.HyruleCastle => _items[ItemType.HCSmallKey],
                DungeonID.AgahnimTower => _items[ItemType.ATSmallKey],
                DungeonID.EasternPalace => _items[ItemType.EPSmallKey],
                DungeonID.DesertPalace => _items[ItemType.DPSmallKey],
                DungeonID.TowerOfHera => _items[ItemType.ToHSmallKey],
                DungeonID.PalaceOfDarkness => _items[ItemType.PoDSmallKey],
                DungeonID.SwampPalace => _items[ItemType.SPSmallKey],
                DungeonID.SkullWoods => _items[ItemType.SWSmallKey],
                DungeonID.ThievesTown => _items[ItemType.TTSmallKey],
                DungeonID.IcePalace => _items[ItemType.IPSmallKey],
                DungeonID.MiseryMire => _items[ItemType.MMSmallKey],
                DungeonID.TurtleRock => _items[ItemType.TRSmallKey],
                DungeonID.GanonsTower => _items[ItemType.GTSmallKey],
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };

            return (ISmallKeyItem)item;
        }

        /// <summary>
        ///     Returns the big key item for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     The big key item.
        /// </returns>
        private IBigKeyItem? GetDungeonBigKeyItem(DungeonID id)
        {
            var item = id switch
            {
                DungeonID.HyruleCastle => _items[ItemType.HCBigKey],
                DungeonID.AgahnimTower => null,
                DungeonID.EasternPalace => _items[ItemType.EPBigKey],
                DungeonID.DesertPalace => _items[ItemType.DPBigKey],
                DungeonID.TowerOfHera => _items[ItemType.ToHBigKey],
                DungeonID.PalaceOfDarkness => _items[ItemType.PoDBigKey],
                DungeonID.SwampPalace => _items[ItemType.SPBigKey],
                DungeonID.SkullWoods => _items[ItemType.SWBigKey],
                DungeonID.ThievesTown => _items[ItemType.TTBigKey],
                DungeonID.IcePalace => _items[ItemType.IPBigKey],
                DungeonID.MiseryMire => _items[ItemType.MMBigKey],
                DungeonID.TurtleRock => _items[ItemType.TRBigKey],
                DungeonID.GanonsTower => _items[ItemType.GTBigKey],
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };

            return item as IBigKeyItem;
        }

        /// <summary>
        ///     Returns a list of dungeon node IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     A list of dungeon node IDs.
        /// </returns>
        private static IList<DungeonNodeID> GetDungeonNodes(DungeonID id)
        {
            return id switch
            {
                DungeonID.HyruleCastle => new List<DungeonNodeID>
                {
                    DungeonNodeID.HCSanctuary,
                    DungeonNodeID.HCFront,
                    DungeonNodeID.HCEscapeFirstKeyDoor,
                    DungeonNodeID.HCPastEscapeFirstKeyDoor,
                    DungeonNodeID.HCEscapeSecondKeyDoor,
                    DungeonNodeID.HCPastEscapeSecondKeyDoor,
                    DungeonNodeID.HCDarkRoomFront,
                    DungeonNodeID.HCDarkCrossKeyDoor,
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    DungeonNodeID.HCSewerRatRoomKeyDoor,
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    DungeonNodeID.HCDarkRoomBack,
                    DungeonNodeID.HCBack
                },
                DungeonID.AgahnimTower => new List<DungeonNodeID>
                {
                    DungeonNodeID.AT,
                    DungeonNodeID.ATDarkMaze,
                    DungeonNodeID.ATPastFirstKeyDoor,
                    DungeonNodeID.ATSecondKeyDoor,
                    DungeonNodeID.ATPastSecondKeyDoor,
                    DungeonNodeID.ATPastThirdKeyDoor,
                    DungeonNodeID.ATFourthKeyDoor,
                    DungeonNodeID.ATPastFourthKeyDoor,
                    DungeonNodeID.ATBossRoom,
                    DungeonNodeID.ATBoss
                },
                DungeonID.EasternPalace => new List<DungeonNodeID>
                {
                    DungeonNodeID.EP,
                    DungeonNodeID.EPBigChest,
                    DungeonNodeID.EPRightDarkRoom,
                    DungeonNodeID.EPRightKeyDoor,
                    DungeonNodeID.EPPastRightKeyDoor,
                    DungeonNodeID.EPBigKeyDoor,
                    DungeonNodeID.EPPastBigKeyDoor,
                    DungeonNodeID.EPBackDarkRoom,
                    DungeonNodeID.EPPastBackKeyDoor,
                    DungeonNodeID.EPBossRoom,
                    DungeonNodeID.EPBoss
                },
                DungeonID.DesertPalace => new List<DungeonNodeID>
                {
                    DungeonNodeID.DPFront,
                    DungeonNodeID.DPTorch,
                    DungeonNodeID.DPBigChest,
                    DungeonNodeID.DPRightKeyDoor,
                    DungeonNodeID.DPPastRightKeyDoor,
                    DungeonNodeID.DPBack,
                    DungeonNodeID.DP2F,
                    DungeonNodeID.DP2FFirstKeyDoor,
                    DungeonNodeID.DP2FPastFirstKeyDoor,
                    DungeonNodeID.DP2FSecondKeyDoor,
                    DungeonNodeID.DP2FPastSecondKeyDoor,
                    DungeonNodeID.DPPastFourTorchWall,
                    DungeonNodeID.DPBossRoom,
                    DungeonNodeID.DPBoss
                },
                DungeonID.TowerOfHera => new List<DungeonNodeID>
                {
                    DungeonNodeID.ToH,
                    DungeonNodeID.ToHPastKeyDoor,
                    DungeonNodeID.ToHBasementTorchRoom,
                    DungeonNodeID.ToHPastBigKeyDoor,
                    DungeonNodeID.ToHBigChest,
                    DungeonNodeID.ToHBoss
                },
                DungeonID.PalaceOfDarkness => new List<DungeonNodeID>
                {
                    DungeonNodeID.PoD,
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    DungeonNodeID.PoDFrontKeyDoor,
                    DungeonNodeID.PoDLobbyArena,
                    DungeonNodeID.PoDBigKeyChestArea,
                    DungeonNodeID.PoDCollapsingWalkwayKeyDoor,
                    DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                    DungeonNodeID.PoDDarkBasement,
                    DungeonNodeID.PoDHarmlessHellwayKeyDoor,
                    DungeonNodeID.PoDHarmlessHellwayRoom,
                    DungeonNodeID.PoDDarkMazeKeyDoor,
                    DungeonNodeID.PoDPastDarkMazeKeyDoor,
                    DungeonNodeID.PoDDarkMaze,
                    DungeonNodeID.PoDBigChestLedge,
                    DungeonNodeID.PoDBigChest,
                    DungeonNodeID.PoDPastSecondRedGoriyaRoom,
                    DungeonNodeID.PoDPastBowStatue,
                    DungeonNodeID.PoDBossAreaDarkRooms,
                    DungeonNodeID.PoDPastHammerBlocks,
                    DungeonNodeID.PoDBossAreaKeyDoor,
                    DungeonNodeID.PoDPastBossAreaKeyDoor,
                    DungeonNodeID.PoDBossRoom,
                    DungeonNodeID.PoDBoss
                },
                DungeonID.SwampPalace => new List<DungeonNodeID>
                {
                    DungeonNodeID.SP,
                    DungeonNodeID.SPAfterRiver,
                    DungeonNodeID.SPB1,
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    DungeonNodeID.SPB1PastSecondRightKeyDoor,
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    DungeonNodeID.SPB1KeyLedge,
                    DungeonNodeID.SPB1PastLeftKeyDoor,
                    DungeonNodeID.SPBigChest,
                    DungeonNodeID.SPB1Back,
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    DungeonNodeID.SPBossRoom,
                    DungeonNodeID.SPBoss
                },
                DungeonID.SkullWoods => new List<DungeonNodeID>
                {
                    DungeonNodeID.SWBigChestAreaBottom,
                    DungeonNodeID.SWBigChestAreaTop,
                    DungeonNodeID.SWBigChest,
                    DungeonNodeID.SWFrontLeftSide,
                    DungeonNodeID.SWFrontRightSide,
                    DungeonNodeID.SWFrontBackConnector,
                    DungeonNodeID.SWPastTheWorthlessKeyDoor,
                    DungeonNodeID.SWBack,
                    DungeonNodeID.SWBackPastFirstKeyDoor,
                    DungeonNodeID.SWBackPastFourTorchRoom,
                    DungeonNodeID.SWBackPastCurtains,
                    DungeonNodeID.SWBossRoom,
                    DungeonNodeID.SWBoss
                },
                DungeonID.ThievesTown => new List<DungeonNodeID>
                {
                    DungeonNodeID.TT,
                    DungeonNodeID.TTPastBigKeyDoor,
                    DungeonNodeID.TTPastFirstKeyDoor,
                    DungeonNodeID.TTPastSecondKeyDoor,
                    DungeonNodeID.TTPastBigChestRoomKeyDoor,
                    DungeonNodeID.TTPastHammerBlocks,
                    DungeonNodeID.TTBigChest,
                    DungeonNodeID.TTBossRoom,
                    DungeonNodeID.TTBoss
                },
                DungeonID.IcePalace => new List<DungeonNodeID>
                {
                    DungeonNodeID.IP,
                    DungeonNodeID.IPPastEntranceFreezorRoom,
                    DungeonNodeID.IPB1LeftSide,
                    DungeonNodeID.IPB1RightSide,
                    DungeonNodeID.IPB2LeftSide,
                    DungeonNodeID.IPB2PastKeyDoor,
                    DungeonNodeID.IPB2PastHammerBlocks,
                    DungeonNodeID.IPB2PastLiftBlock,
                    DungeonNodeID.IPSpikeRoom,
                    DungeonNodeID.IPB4RightSide,
                    DungeonNodeID.IPB4IceRoom,
                    DungeonNodeID.IPB4FreezorRoom,
                    DungeonNodeID.IPFreezorChest,
                    DungeonNodeID.IPB4PastKeyDoor,
                    DungeonNodeID.IPBigChestArea,
                    DungeonNodeID.IPBigChest,
                    DungeonNodeID.IPB5,
                    DungeonNodeID.IPB5PastBigKeyDoor,
                    DungeonNodeID.IPB6,
                    DungeonNodeID.IPB6PastKeyDoor,
                    DungeonNodeID.IPB6PreBossRoom,
                    DungeonNodeID.IPB6PastHammerBlocks,
                    DungeonNodeID.IPB6PastLiftBlock,
                    DungeonNodeID.IPBoss
                },
                DungeonID.MiseryMire => new List<DungeonNodeID>
                {
                    DungeonNodeID.MM,
                    DungeonNodeID.MMPastEntranceGap,
                    DungeonNodeID.MMBigChest,
                    DungeonNodeID.MMB1TopSide,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
                    DungeonNodeID.MMB1PastFourTorchRoom,
                    DungeonNodeID.MMF1PastFourTorchRoom,
                    DungeonNodeID.MMB1PastPortalBigKeyDoor,
                    DungeonNodeID.MMB1PastBridgeBigKeyDoor,
                    DungeonNodeID.MMDarkRoom,
                    DungeonNodeID.MMB2PastWorthlessKeyDoor,
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    DungeonNodeID.MMBossRoom,
                    DungeonNodeID.MMBoss
                },
                DungeonID.TurtleRock => new List<DungeonNodeID>
                {
                    DungeonNodeID.TRFront,
                    DungeonNodeID.TRF1SomariaTrack,
                    DungeonNodeID.TRF1CompassChestArea,
                    DungeonNodeID.TRF1FourTorchRoom,
                    DungeonNodeID.TRF1RollerRoom,
                    DungeonNodeID.TRF1FirstKeyDoorArea,
                    DungeonNodeID.TRF1PastFirstKeyDoor,
                    DungeonNodeID.TRF1PastSecondKeyDoor,
                    DungeonNodeID.TRB1,
                    DungeonNodeID.TRB1PastBigKeyChestKeyDoor,
                    DungeonNodeID.TRB1MiddleRightEntranceArea,
                    DungeonNodeID.TRB1BigChestArea,
                    DungeonNodeID.TRBigChest,
                    DungeonNodeID.TRB1RightSide,
                    DungeonNodeID.TRPastB1ToB2KeyDoor,
                    DungeonNodeID.TRB2DarkRoomTop,
                    DungeonNodeID.TRB2DarkRoomBottom,
                    DungeonNodeID.TRB2PastDarkMaze,
                    DungeonNodeID.TRLaserBridgeChests,
                    DungeonNodeID.TRB2PastKeyDoor,
                    DungeonNodeID.TRB3,
                    DungeonNodeID.TRB3BossRoomEntry,
                    DungeonNodeID.TRBossRoom,
                    DungeonNodeID.TRBoss
                },
                DungeonID.GanonsTower => new List<DungeonNodeID>
                {
                    DungeonNodeID.GT,
                    DungeonNodeID.GTBobsTorch,
                    DungeonNodeID.GT1FLeft,
                    DungeonNodeID.GT1FLeftPastHammerBlocks,
                    DungeonNodeID.GT1FLeftDMsRoom,
                    DungeonNodeID.GT1FLeftPastBonkableGaps,
                    DungeonNodeID.GT1FLeftMapChestRoom,
                    DungeonNodeID.GT1FLeftSpikeTrapPortalRoom,
                    DungeonNodeID.GT1FLeftFiresnakeRoom,
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomGap,
                    DungeonNodeID.GT1FLeftPastFiresnakeRoomKeyDoor,
                    DungeonNodeID.GT1FLeftRandomizerRoom,
                    DungeonNodeID.GT1FRight,
                    DungeonNodeID.GT1FRightTileRoom,
                    DungeonNodeID.GT1FRightFourTorchRoom,
                    DungeonNodeID.GT1FRightCompassRoom,
                    DungeonNodeID.GT1FRightPastCompassRoomPortal,
                    DungeonNodeID.GT1FRightCollapsingWalkway,
                    DungeonNodeID.GT1FBottomRoom,
                    DungeonNodeID.GTBoss1,
                    DungeonNodeID.GTB1BossChests,
                    DungeonNodeID.GTBigChest,
                    DungeonNodeID.GT3FPastRedGoriyaRooms,
                    DungeonNodeID.GT3FPastBigKeyDoor,
                    DungeonNodeID.GTBoss2,
                    DungeonNodeID.GT4FPastBoss2,
                    DungeonNodeID.GT5FPastFourTorchRooms,
                    DungeonNodeID.GT6FPastFirstKeyDoor,
                    DungeonNodeID.GT6FBossRoom,
                    DungeonNodeID.GTBoss3,
                    DungeonNodeID.GTBoss3Item,
                    DungeonNodeID.GT6FPastBossRoomGap,
                    DungeonNodeID.GTFinalBossRoom,
                    DungeonNodeID.GTFinalBoss
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        /// <summary>
        ///     Returns a list of dungeon item IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     A list of dungeon item IDs.
        /// </returns>
        private static IList<DungeonItemID> GetDungeonItems(DungeonID id)
        {
            return id switch
            {
                DungeonID.HyruleCastle => new List<DungeonItemID>
                {
                    DungeonItemID.HCSanctuary,
                    DungeonItemID.HCMapChest,
                    DungeonItemID.HCBoomerangChest,
                    DungeonItemID.HCZeldasCell,
                    DungeonItemID.HCDarkCross,
                    DungeonItemID.HCSecretRoomLeft,
                    DungeonItemID.HCSecretRoomMiddle,
                    DungeonItemID.HCSecretRoomRight
                },
                DungeonID.AgahnimTower => new List<DungeonItemID>
                {
                    DungeonItemID.ATRoom03,
                    DungeonItemID.ATDarkMaze
                },
                DungeonID.EasternPalace => new List<DungeonItemID>
                {
                    DungeonItemID.EPCannonballChest,
                    DungeonItemID.EPMapChest,
                    DungeonItemID.EPCompassChest,
                    DungeonItemID.EPBigChest,
                    DungeonItemID.EPBigKeyChest,
                    DungeonItemID.EPBoss
                },
                DungeonID.DesertPalace => new List<DungeonItemID>
                {
                    DungeonItemID.DPMapChest,
                    DungeonItemID.DPTorch,
                    DungeonItemID.DPBigChest,
                    DungeonItemID.DPCompassChest,
                    DungeonItemID.DPBigKeyChest,
                    DungeonItemID.DPBoss
                },
                DungeonID.TowerOfHera => new List<DungeonItemID>
                {
                    DungeonItemID.ToHBasementCage,
                    DungeonItemID.ToHMapChest,
                    DungeonItemID.ToHBigKeyChest,
                    DungeonItemID.ToHCompassChest,
                    DungeonItemID.ToHBigChest,
                    DungeonItemID.ToHBoss
                },
                DungeonID.PalaceOfDarkness => new List<DungeonItemID>
                {
                    DungeonItemID.PoDShooterRoom,
                    DungeonItemID.PoDMapChest,
                    DungeonItemID.PoDArenaLedge,
                    DungeonItemID.PoDBigKeyChest,
                    DungeonItemID.PoDStalfosBasement,
                    DungeonItemID.PoDArenaBridge,
                    DungeonItemID.PoDCompassChest,
                    DungeonItemID.PoDDarkBasementLeft,
                    DungeonItemID.PoDDarkBasementRight,
                    DungeonItemID.PoDHarmlessHellway,
                    DungeonItemID.PoDDarkMazeTop,
                    DungeonItemID.PoDDarkMazeBottom,
                    DungeonItemID.PoDBigChest,
                    DungeonItemID.PoDBoss
                },
                DungeonID.SwampPalace => new List<DungeonItemID>
                {
                    DungeonItemID.SPEntrance,
                    DungeonItemID.SPMapChest,
                    DungeonItemID.SPBigChest,
                    DungeonItemID.SPCompassChest,
                    DungeonItemID.SPWestChest,
                    DungeonItemID.SPBigKeyChest,
                    DungeonItemID.SPFloodedRoomLeft,
                    DungeonItemID.SPFloodedRoomRight,
                    DungeonItemID.SPWaterfallRoom,
                    DungeonItemID.SPBoss
                },
                DungeonID.SkullWoods => new List<DungeonItemID>
                {
                    DungeonItemID.SWBigKeyChest,
                    DungeonItemID.SWMapChest,
                    DungeonItemID.SWBigChest,
                    DungeonItemID.SWPotPrison,
                    DungeonItemID.SWCompassChest,
                    DungeonItemID.SWPinballRoom,
                    DungeonItemID.SWBridgeRoom,
                    DungeonItemID.SWBoss
                },
                DungeonID.ThievesTown => new List<DungeonItemID>
                {
                    DungeonItemID.TTMapChest,
                    DungeonItemID.TTAmbushChest,
                    DungeonItemID.TTCompassChest,
                    DungeonItemID.TTBigKeyChest,
                    DungeonItemID.TTAttic,
                    DungeonItemID.TTBlindsCell,
                    DungeonItemID.TTBigChest,
                    DungeonItemID.TTBoss
                },
                DungeonID.IcePalace => new List<DungeonItemID>
                {
                    DungeonItemID.IPCompassChest,
                    DungeonItemID.IPSpikeRoom,
                    DungeonItemID.IPMapChest,
                    DungeonItemID.IPBigKeyChest,
                    DungeonItemID.IPFreezorChest,
                    DungeonItemID.IPBigChest,
                    DungeonItemID.IPIcedTRoom,
                    DungeonItemID.IPBoss
                },
                DungeonID.MiseryMire => new List<DungeonItemID>
                {
                    DungeonItemID.MMBridgeChest,
                    DungeonItemID.MMSpikeChest,
                    DungeonItemID.MMMainLobby,
                    DungeonItemID.MMCompassChest,
                    DungeonItemID.MMBigKeyChest,
                    DungeonItemID.MMBigChest,
                    DungeonItemID.MMMapChest,
                    DungeonItemID.MMBoss
                },
                DungeonID.TurtleRock => new List<DungeonItemID>
                {
                    DungeonItemID.TRCompassChest,
                    DungeonItemID.TRRollerRoomLeft,
                    DungeonItemID.TRRollerRoomRight,
                    DungeonItemID.TRChainChomps,
                    DungeonItemID.TRBigKeyChest,
                    DungeonItemID.TRBigChest,
                    DungeonItemID.TRCrystarollerRoom,
                    DungeonItemID.TRLaserBridgeTopLeft,
                    DungeonItemID.TRLaserBridgeTopRight,
                    DungeonItemID.TRLaserBridgeBottomLeft,
                    DungeonItemID.TRLaserBridgeBottomRight,
                    DungeonItemID.TRBoss
                },
                DungeonID.GanonsTower => new List<DungeonItemID>
                {
                    DungeonItemID.GTHopeRoomLeft,
                    DungeonItemID.GTHopeRoomRight,
                    DungeonItemID.GTBobsTorch,
                    DungeonItemID.GTDMsRoomTopLeft,
                    DungeonItemID.GTDMsRoomTopRight,
                    DungeonItemID.GTDMsRoomBottomLeft,
                    DungeonItemID.GTDMsRoomBottomRight,
                    DungeonItemID.GTMapChest,
                    DungeonItemID.GTFiresnakeRoom,
                    DungeonItemID.GTRandomizerRoomTopLeft,
                    DungeonItemID.GTRandomizerRoomTopRight,
                    DungeonItemID.GTRandomizerRoomBottomLeft,
                    DungeonItemID.GTRandomizerRoomBottomRight,
                    DungeonItemID.GTTileRoom,
                    DungeonItemID.GTCompassRoomTopLeft,
                    DungeonItemID.GTCompassRoomTopRight,
                    DungeonItemID.GTCompassRoomBottomLeft,
                    DungeonItemID.GTCompassRoomBottomRight,
                    DungeonItemID.GTBobsChest,
                    DungeonItemID.GTBigKeyRoomTopLeft,
                    DungeonItemID.GTBigKeyRoomTopRight,
                    DungeonItemID.GTBigKeyChest,
                    DungeonItemID.GTBigChest,
                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                    DungeonItemID.GTMiniHelmasaurRoomRight,
                    DungeonItemID.GTPreMoldormChest,
                    DungeonItemID.GTMoldormChest
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        /// <summary>
        ///     Returns a list of dungeon boss item IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     A list of dungeon boss item IDs.
        /// </returns>
        private static IList<DungeonItemID> GetDungeonBosses(DungeonID id)
        {
            return id switch
            {
                DungeonID.HyruleCastle => new List<DungeonItemID>(0),
                DungeonID.AgahnimTower => new List<DungeonItemID> {DungeonItemID.ATBoss},
                DungeonID.EasternPalace => new List<DungeonItemID> {DungeonItemID.EPBoss},
                DungeonID.DesertPalace => new List<DungeonItemID> {DungeonItemID.DPBoss},
                DungeonID.TowerOfHera => new List<DungeonItemID> {DungeonItemID.ToHBoss},
                DungeonID.PalaceOfDarkness => new List<DungeonItemID> {DungeonItemID.PoDBoss},
                DungeonID.SwampPalace => new List<DungeonItemID> {DungeonItemID.SPBoss},
                DungeonID.SkullWoods => new List<DungeonItemID> {DungeonItemID.SWBoss},
                DungeonID.ThievesTown => new List<DungeonItemID> {DungeonItemID.TTBoss},
                DungeonID.IcePalace => new List<DungeonItemID> {DungeonItemID.IPBoss},
                DungeonID.MiseryMire => new List<DungeonItemID> {DungeonItemID.MMBoss},
                DungeonID.TurtleRock => new List<DungeonItemID> {DungeonItemID.TRBoss},
                DungeonID.GanonsTower => new List<DungeonItemID>
                {
                    DungeonItemID.GTBoss1,
                    DungeonItemID.GTBoss2,
                    DungeonItemID.GTBoss3,
                    DungeonItemID.GTFinalBoss
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        /// <summary>
        ///     Returns a list of dungeon small key drop item IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     A list of dungeon small key drop item IDs.
        /// </returns>
        private static IList<DungeonItemID> GetDungeonSmallKeyDrops(DungeonID id)
        {
            return id switch
            {
                DungeonID.HyruleCastle => new List<DungeonItemID>
                {
                    DungeonItemID.HCBoomerangGuardDrop,
                    DungeonItemID.HCMapGuardDrop,
                    DungeonItemID.HCKeyRatDrop
                },
                DungeonID.AgahnimTower => new List<DungeonItemID>
                {
                    DungeonItemID.ATDarkArcherDrop,
                    DungeonItemID.ATCircleOfPotsDrop
                },
                DungeonID.EasternPalace => new List<DungeonItemID>
                {
                    DungeonItemID.EPDarkSquarePot,
                    DungeonItemID.EPDarkEyegoreDrop
                },
                DungeonID.DesertPalace => new List<DungeonItemID>
                {
                    DungeonItemID.DPTiles1Pot,
                    DungeonItemID.DPBeamosHallPot,
                    DungeonItemID.DPTiles2Pot
                },
                DungeonID.TowerOfHera => new List<DungeonItemID>(0),
                DungeonID.PalaceOfDarkness => new List<DungeonItemID>(0),
                DungeonID.SwampPalace => new List<DungeonItemID>
                {
                    DungeonItemID.SPPotRowPot,
                    DungeonItemID.SPTrench1Pot,
                    DungeonItemID.SPHookshotPot,
                    DungeonItemID.SPTrench2Pot,
                    DungeonItemID.SPWaterwayPot
                },
                DungeonID.SkullWoods => new List<DungeonItemID>
                {
                    DungeonItemID.SWWestLobbyPot,
                    DungeonItemID.SWSpikeCornerDrop
                },
                DungeonID.ThievesTown => new List<DungeonItemID>
                {
                    DungeonItemID.TTHallwayPot,
                    DungeonItemID.TTSpikeSwitchPot
                },
                DungeonID.IcePalace => new List<DungeonItemID>
                {
                    DungeonItemID.IPJellyDrop,
                    DungeonItemID.IPConveyerDrop,
                    DungeonItemID.IPHammerBlockDrop,
                    DungeonItemID.IPManyPotsPot
                },
                DungeonID.MiseryMire => new List<DungeonItemID>
                {
                    DungeonItemID.MMSpikesPot,
                    DungeonItemID.MMFishbonePot,
                    DungeonItemID.MMConveyerCrystalDrop
                },
                DungeonID.TurtleRock => new List<DungeonItemID>
                {
                    DungeonItemID.TRPokey1Drop,
                    DungeonItemID.TRPokey2Drop
                },
                DungeonID.GanonsTower => new List<DungeonItemID>
                {
                    DungeonItemID.GTConveyorCrossPot,
                    DungeonItemID.GTDoubleSwitchPot,
                    DungeonItemID.GTConveyorStarPitsPot,
                    DungeonItemID.GTMiniHelmasaurDrop
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        /// <summary>
        ///     Returns a list of dungeon small key drop item IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     A list of dungeon small key drop item IDs.
        /// </returns>
        private static IList<DungeonItemID> GetDungeonBigKeyDrops(DungeonID id)
        {
            return id switch
            {
                DungeonID.HyruleCastle => new List<DungeonItemID> {DungeonItemID.HCBigKeyDrop},
                DungeonID.AgahnimTower => new List<DungeonItemID>(0),
                DungeonID.EasternPalace => new List<DungeonItemID>(0),
                DungeonID.DesertPalace => new List<DungeonItemID>(0),
                DungeonID.TowerOfHera => new List<DungeonItemID>(0),
                DungeonID.PalaceOfDarkness => new List<DungeonItemID>(0),
                DungeonID.SwampPalace => new List<DungeonItemID>(0),
                DungeonID.SkullWoods => new List<DungeonItemID>(0),
                DungeonID.ThievesTown => new List<DungeonItemID>(0),
                DungeonID.IcePalace => new List<DungeonItemID>(0),
                DungeonID.MiseryMire => new List<DungeonItemID>(0),
                DungeonID.TurtleRock => new List<DungeonItemID>(0),
                DungeonID.GanonsTower => new List<DungeonItemID>(0),
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        /// <summary>
        ///     Returns a list of small key door IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     A list of small key door IDs.
        /// </returns>
        private static IList<KeyDoorID> GetDungeonSmallKeyDoors(DungeonID id)
        {
            return id switch
            {
                DungeonID.HyruleCastle => new List<KeyDoorID>
                {
                    KeyDoorID.HCEscapeFirstKeyDoor,
                    KeyDoorID.HCEscapeSecondKeyDoor,
                    KeyDoorID.HCDarkCrossKeyDoor,
                    KeyDoorID.HCSewerRatRoomKeyDoor
                },
                DungeonID.AgahnimTower => new List<KeyDoorID>
                {
                    KeyDoorID.ATFirstKeyDoor,
                    KeyDoorID.ATSecondKeyDoor,
                    KeyDoorID.ATThirdKeyDoor,
                    KeyDoorID.ATFourthKeyDoor
                },
                DungeonID.EasternPalace => new List<KeyDoorID> {KeyDoorID.EPRightKeyDoor, KeyDoorID.EPBackKeyDoor},
                DungeonID.DesertPalace => new List<KeyDoorID>
                {
                    KeyDoorID.DPRightKeyDoor,
                    KeyDoorID.DP1FKeyDoor,
                    KeyDoorID.DP2FFirstKeyDoor,
                    KeyDoorID.DP2FSecondKeyDoor
                },
                DungeonID.TowerOfHera => new List<KeyDoorID> {KeyDoorID.ToHKeyDoor},
                DungeonID.PalaceOfDarkness => new List<KeyDoorID>
                {
                    KeyDoorID.PoDFrontKeyDoor,
                    KeyDoorID.PoDBigKeyChestKeyDoor,
                    KeyDoorID.PoDCollapsingWalkwayKeyDoor,
                    KeyDoorID.PoDDarkMazeKeyDoor,
                    KeyDoorID.PoDHarmlessHellwayKeyDoor,
                    KeyDoorID.PoDBossAreaKeyDoor
                },
                DungeonID.SwampPalace => new List<KeyDoorID>
                {
                    KeyDoorID.SP1FKeyDoor,
                    KeyDoorID.SPB1FirstRightKeyDoor,
                    KeyDoorID.SPB1SecondRightKeyDoor,
                    KeyDoorID.SPB1LeftKeyDoor,
                    KeyDoorID.SPB1BackFirstKeyDoor,
                    KeyDoorID.SPBossRoomKeyDoor
                },
                DungeonID.SkullWoods => new List<KeyDoorID>
                {
                    KeyDoorID.SWFrontLeftKeyDoor,
                    KeyDoorID.SWFrontRightKeyDoor,
                    KeyDoorID.SWWorthlessKeyDoor,
                    KeyDoorID.SWBackFirstKeyDoor,
                    KeyDoorID.SWBackSecondKeyDoor
                },
                DungeonID.ThievesTown => new List<KeyDoorID>
                {
                    KeyDoorID.TTFirstKeyDoor,
                    KeyDoorID.TTSecondKeyDoor,
                    KeyDoorID.TTBigChestKeyDoor
                },
                DungeonID.IcePalace => new List<KeyDoorID>
                {
                    KeyDoorID.IP1FKeyDoor,
                    KeyDoorID.IPB2KeyDoor,
                    KeyDoorID.IPB3KeyDoor,
                    KeyDoorID.IPB4KeyDoor,
                    KeyDoorID.IPB5KeyDoor,
                    KeyDoorID.IPB6KeyDoor
                },
                DungeonID.MiseryMire => new List<KeyDoorID>
                {
                    KeyDoorID.MMB1TopRightKeyDoor,
                    KeyDoorID.MMB1TopLeftKeyDoor,
                    KeyDoorID.MMB1LeftSideFirstKeyDoor,
                    KeyDoorID.MMB1LeftSideSecondKeyDoor,
                    KeyDoorID.MMB1RightSideKeyDoor,
                    KeyDoorID.MMB2WorthlessKeyDoor
                },
                DungeonID.TurtleRock => new List<KeyDoorID>
                {
                    KeyDoorID.TR1FFirstKeyDoor,
                    KeyDoorID.TR1FSecondKeyDoor,
                    KeyDoorID.TR1FThirdKeyDoor,
                    KeyDoorID.TRB1BigKeyChestKeyDoor,
                    KeyDoorID.TRB1ToB2KeyDoor,
                    KeyDoorID.TRB2KeyDoor
                },
                DungeonID.GanonsTower => new List<KeyDoorID>
                {
                    KeyDoorID.GT1FLeftToRightKeyDoor,
                    KeyDoorID.GT1FMapChestRoomKeyDoor,
                    KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor,
                    KeyDoorID.GT1FFiresnakeRoomKeyDoor,
                    KeyDoorID.GT1FTileRoomKeyDoor,
                    KeyDoorID.GT1FCollapsingWalkwayKeyDoor,
                    KeyDoorID.GT6FFirstKeyDoor,
                    KeyDoorID.GT6FSecondKeyDoor
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        /// <summary>
        ///     Returns a list of big key door IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <returns>
        ///     A list of big key door IDs.
        /// </returns>
        private static IList<KeyDoorID> GetDungeonBigKeyDoors(DungeonID id)
        {
            return id switch
            {
                DungeonID.HyruleCastle => new List<KeyDoorID> {KeyDoorID.HCZeldasCellDoor},
                DungeonID.AgahnimTower => new List<KeyDoorID>(0),
                DungeonID.EasternPalace => new List<KeyDoorID> {KeyDoorID.EPBigChest, KeyDoorID.EPBigKeyDoor},
                DungeonID.DesertPalace => new List<KeyDoorID> {KeyDoorID.DPBigChest, KeyDoorID.DPBigKeyDoor},
                DungeonID.TowerOfHera => new List<KeyDoorID> {KeyDoorID.ToHBigKeyDoor, KeyDoorID.ToHBigChest},
                DungeonID.PalaceOfDarkness => new List<KeyDoorID> {KeyDoorID.PoDBigChest, KeyDoorID.PoDBigKeyDoor},
                DungeonID.SwampPalace => new List<KeyDoorID> {KeyDoorID.SPBigChest},
                DungeonID.SkullWoods => new List<KeyDoorID> {KeyDoorID.SWBigChest},
                DungeonID.ThievesTown => new List<KeyDoorID> {KeyDoorID.TTBigKeyDoor, KeyDoorID.TTBigChest},
                DungeonID.IcePalace => new List<KeyDoorID> {KeyDoorID.IPBigKeyDoor, KeyDoorID.IPBigChest},
                DungeonID.MiseryMire => new List<KeyDoorID>
                {
                    KeyDoorID.MMBigChest,
                    KeyDoorID.MMPortalBigKeyDoor,
                    KeyDoorID.MMBridgeBigKeyDoor,
                    KeyDoorID.MMBossRoomBigKeyDoor
                },
                DungeonID.TurtleRock => new List<KeyDoorID>
                {
                    KeyDoorID.TRBigChest,
                    KeyDoorID.TRB1BigKeyDoor,
                    KeyDoorID.TRBossRoomBigKeyDoor
                },
                DungeonID.GanonsTower => new List<KeyDoorID>
                {
                    KeyDoorID.GTBigChest,
                    KeyDoorID.GT3FBigKeyDoor,
                    KeyDoorID.GT7FBigKeyDoor
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        /// <summary>
        ///     Returns a list of dungeon entry nodes for the specified dungeon.
        /// </summary>
        /// <param name="id">
        ///     The location ID of the dungeon.
        /// </param>
        /// <returns>
        ///     A list of dungeon entry nodes.
        /// </returns>
        private IList<INode> GetDungeonEntryNodes(DungeonID id)
        {
            return id switch
            {
                DungeonID.HyruleCastle => new List<INode>
                {
                    _overworldNodes[OverworldNodeID.HCSanctuaryEntry],
                    _overworldNodes[OverworldNodeID.HCFrontEntry],
                    _overworldNodes[OverworldNodeID.HCBackEntry]
                },
                DungeonID.AgahnimTower => new List<INode> {_overworldNodes[OverworldNodeID.ATEntry]},
                DungeonID.EasternPalace => new List<INode> {_overworldNodes[OverworldNodeID.EPEntry]},
                DungeonID.DesertPalace => new List<INode>
                {
                    _overworldNodes[OverworldNodeID.DPFrontEntry],
                    _overworldNodes[OverworldNodeID.DPLeftEntry],
                    _overworldNodes[OverworldNodeID.DPBackEntry]
                },
                DungeonID.TowerOfHera => new List<INode> {_overworldNodes[OverworldNodeID.ToHEntry]},
                DungeonID.PalaceOfDarkness => new List<INode> {_overworldNodes[OverworldNodeID.PoDEntry]},
                DungeonID.SwampPalace => new List<INode> {_overworldNodes[OverworldNodeID.SPEntry]},
                DungeonID.SkullWoods => new List<INode>
                {
                    _overworldNodes[OverworldNodeID.SWFrontEntry],
                    _overworldNodes[OverworldNodeID.SWBackEntry]
                },
                DungeonID.ThievesTown => new List<INode> {_overworldNodes[OverworldNodeID.TTEntry]},
                DungeonID.IcePalace => new List<INode> {_overworldNodes[OverworldNodeID.IPEntry]},
                DungeonID.MiseryMire => new List<INode> {_overworldNodes[OverworldNodeID.MMEntry]},
                DungeonID.TurtleRock => new List<INode>
                {
                    _overworldNodes[OverworldNodeID.TRFrontEntry],
                    _overworldNodes[OverworldNodeID.TRMiddleEntry],
                    _overworldNodes[OverworldNodeID.TRBackEntry]
                },
                DungeonID.GanonsTower => new List<INode>
                {
                    _overworldNodes[OverworldNodeID.GTEntry]
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }
    }
}
