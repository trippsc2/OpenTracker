using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.Locations;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This class contains the creation logic for dungeons.
    /// </summary>
    public class DungeonFactory : IDungeonFactory
    {
        private readonly IItemDictionary _items;
        private readonly IRequirementNodeDictionary _requirementNodes;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">
        /// The item dictionary.
        /// </param>
        /// <param name="requirementNodes">
        /// The requirement node dictionary.
        /// </param>
        public DungeonFactory(
            IItemDictionary items, IRequirementNodeDictionary requirementNodes)
        {
            _items = items;
            _requirementNodes = requirementNodes;
        }

        /// <summary>
        /// Returns the number of maps in the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer representing the number of maps.
        /// </returns>
        public int GetDungeonMapCount(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
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
                case LocationID.GanonsTower:
                    {
                        return 1;
                    }
                case LocationID.AgahnimTower:
                    {
                        return 0;
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the number of compasses in the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer representing the number of compasses.
        /// </returns>
        public int GetDungeonCompassCount(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                case LocationID.AgahnimTower:
                    {
                        return 0;
                    }
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
                case LocationID.GanonsTower:
                    {
                        return 1;
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the number of small keys in the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer representing the number of small keys.
        /// </returns>
        public int GetDungeonSmallKeyCount(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                case LocationID.DesertPalace:
                case LocationID.TowerOfHera:
                case LocationID.SwampPalace:
                case LocationID.ThievesTown:
                    {
                        return 1;
                    }
                case LocationID.AgahnimTower:
                case LocationID.IcePalace:
                    {
                        return 2;
                    }
                case LocationID.EasternPalace:
                    {
                        return 0;
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return 6;
                    }
                case LocationID.SkullWoods:
                case LocationID.MiseryMire:
                    {
                        return 3;
                    }
                case LocationID.TurtleRock:
                case LocationID.GanonsTower:
                    {
                        return 4;
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the number of big keys in the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer representing the number of big keys.
        /// </returns>
        public int GetDungeonBigKeyCount(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                case LocationID.AgahnimTower:
                    {
                        return 0;
                    }
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
                case LocationID.GanonsTower:
                    {
                        return 1;
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the map item for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// The map item.
        /// </returns>
        public IItem? GetDungeonMapItem(LocationID id)
        {
            return id switch
            {
                LocationID.HyruleCastle => _items[ItemType.HCMap],
                LocationID.AgahnimTower => null,
                LocationID.EasternPalace => _items[ItemType.EPMap],
                LocationID.DesertPalace => _items[ItemType.DPMap],
                LocationID.TowerOfHera => _items[ItemType.ToHMap],
                LocationID.PalaceOfDarkness => _items[ItemType.PoDMap],
                LocationID.SwampPalace => _items[ItemType.SPMap],
                LocationID.SkullWoods => _items[ItemType.SWMap],
                LocationID.ThievesTown => _items[ItemType.TTMap],
                LocationID.IcePalace => _items[ItemType.IPMap],
                LocationID.MiseryMire => _items[ItemType.MMMap],
                LocationID.TurtleRock => _items[ItemType.TRMap],
                LocationID.GanonsTower => _items[ItemType.GTMap],
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        /// <summary>
        /// Returns the compass item for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// The compass item.
        /// </returns>
        public IItem? GetDungeonCompassItem(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                case LocationID.AgahnimTower:
                    {
                        return null;
                    }
                case LocationID.EasternPalace:
                    {
                        return _items[ItemType.EPCompass];
                    }
                case LocationID.DesertPalace:
                    {
                        return _items[ItemType.DPCompass];
                    }
                case LocationID.TowerOfHera:
                    {
                        return _items[ItemType.ToHCompass];
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return _items[ItemType.PoDCompass];
                    }
                case LocationID.SwampPalace:
                    {
                        return _items[ItemType.SPCompass];
                    }
                case LocationID.SkullWoods:
                    {
                        return _items[ItemType.SWCompass];
                    }
                case LocationID.ThievesTown:
                    {
                        return _items[ItemType.TTCompass];
                    }
                case LocationID.IcePalace:
                    {
                        return _items[ItemType.IPCompass];
                    }
                case LocationID.MiseryMire:
                    {
                        return _items[ItemType.MMCompass];
                    }
                case LocationID.TurtleRock:
                    {
                        return _items[ItemType.TRCompass];
                    }
                case LocationID.GanonsTower:
                    {
                        return _items[ItemType.GTCompass];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the small key item for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// The small key item.
        /// </returns>
        public IItem GetDungeonSmallKeyItem(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                    {
                        return _items[ItemType.HCSmallKey];
                    }
                case LocationID.AgahnimTower:
                    {
                        return _items[ItemType.ATSmallKey];
                    }
                case LocationID.EasternPalace:
                    {
                        return _items[ItemType.EPSmallKey];
                    }
                case LocationID.DesertPalace:
                    {
                        return _items[ItemType.DPSmallKey];
                    }
                case LocationID.TowerOfHera:
                    {
                        return _items[ItemType.ToHSmallKey];
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return _items[ItemType.PoDSmallKey];
                    }
                case LocationID.SwampPalace:
                    {
                        return _items[ItemType.SPSmallKey];
                    }
                case LocationID.SkullWoods:
                    {
                        return _items[ItemType.SWSmallKey];
                    }
                case LocationID.ThievesTown:
                    {
                        return _items[ItemType.TTSmallKey];
                    }
                case LocationID.IcePalace:
                    {
                        return _items[ItemType.IPSmallKey];
                    }
                case LocationID.MiseryMire:
                    {
                        return _items[ItemType.MMSmallKey];
                    }
                case LocationID.TurtleRock:
                    {
                        return _items[ItemType.TRSmallKey];
                    }
                case LocationID.GanonsTower:
                    {
                        return _items[ItemType.GTSmallKey];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the big key item for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// The big key item.
        /// </returns>
        public IItem? GetDungeonBigKeyItem(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                    {
                        return _items[ItemType.HCBigKey];
                    }
                case LocationID.AgahnimTower:
                    {
                        return null;
                    }
                case LocationID.EasternPalace:
                    {
                        return _items[ItemType.EPBigKey];
                    }
                case LocationID.DesertPalace:
                    {
                        return _items[ItemType.DPBigKey];
                    }
                case LocationID.TowerOfHera:
                    {
                        return _items[ItemType.ToHBigKey];
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return _items[ItemType.PoDBigKey];
                    }
                case LocationID.SwampPalace:
                    {
                        return _items[ItemType.SPBigKey];
                    }
                case LocationID.SkullWoods:
                    {
                        return _items[ItemType.SWBigKey];
                    }
                case LocationID.ThievesTown:
                    {
                        return _items[ItemType.TTBigKey];
                    }
                case LocationID.IcePalace:
                    {
                        return _items[ItemType.IPBigKey];
                    }
                case LocationID.MiseryMire:
                    {
                        return _items[ItemType.MMBigKey];
                    }
                case LocationID.TurtleRock:
                    {
                        return _items[ItemType.TRBigKey];
                    }
                case LocationID.GanonsTower:
                    {
                        return _items[ItemType.GTBigKey];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a list of dungeon node IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// A list of dungeon node IDs.
        /// </returns>
        public List<DungeonNodeID> GetDungeonNodes(LocationID id)
        {
            return id switch
            {
                LocationID.HyruleCastle => new List<DungeonNodeID>
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
                LocationID.AgahnimTower => new List<DungeonNodeID>
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
                LocationID.EasternPalace => new List<DungeonNodeID>
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
                LocationID.DesertPalace => new List<DungeonNodeID>
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
                LocationID.TowerOfHera => new List<DungeonNodeID>
                {
                    DungeonNodeID.ToH,
                    DungeonNodeID.ToHPastKeyDoor,
                    DungeonNodeID.ToHBasementTorchRoom,
                    DungeonNodeID.ToHPastBigKeyDoor,
                    DungeonNodeID.ToHBigChest,
                    DungeonNodeID.ToHBoss
                },
                LocationID.PalaceOfDarkness => new List<DungeonNodeID>
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
                LocationID.SwampPalace => new List<DungeonNodeID>
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
                LocationID.SkullWoods => new List<DungeonNodeID>
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
                LocationID.ThievesTown => new List<DungeonNodeID>
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
                LocationID.IcePalace => new List<DungeonNodeID>
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
                LocationID.MiseryMire => new List<DungeonNodeID>
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
                LocationID.TurtleRock => new List<DungeonNodeID>
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
                    DungeonNodeID.TRPastB1toB2KeyDoor,
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
                LocationID.GanonsTower => new List<DungeonNodeID>
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
        /// Returns a list of dungeon item IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// A list of dungeon item IDs.
        /// </returns>
        public List<DungeonItemID> GetDungeonItems(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.HCSanctuary,
                            DungeonItemID.HCMapChest,
                            DungeonItemID.HCBoomerangChest,
                            DungeonItemID.HCZeldasCell,
                            DungeonItemID.HCDarkCross,
                            DungeonItemID.HCSecretRoomLeft,
                            DungeonItemID.HCSecretRoomMiddle,
                            DungeonItemID.HCSecretRoomRight
                        };
                    }
                case LocationID.AgahnimTower:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.ATRoom03,
                            DungeonItemID.ATDarkMaze
                        };
                    }
                case LocationID.EasternPalace:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.EPCannonballChest,
                            DungeonItemID.EPMapChest,
                            DungeonItemID.EPCompassChest,
                            DungeonItemID.EPBigChest,
                            DungeonItemID.EPBigKeyChest,
                            DungeonItemID.EPBoss
                        };
                    }
                case LocationID.DesertPalace:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPBigChest,
                            DungeonItemID.DPCompassChest,
                            DungeonItemID.DPBigKeyChest,
                            DungeonItemID.DPBoss
                        };
                    }
                case LocationID.TowerOfHera:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBasementCage,
                            DungeonItemID.ToHMapChest,
                            DungeonItemID.ToHBigKeyChest,
                            DungeonItemID.ToHCompassChest,
                            DungeonItemID.ToHBigChest,
                            DungeonItemID.ToHBoss
                        };
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return new List<DungeonItemID>
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
                        };
                    }
                case LocationID.SwampPalace:
                    {
                        return new List<DungeonItemID>
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
                        };
                    }
                case LocationID.SkullWoods:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWBigChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom,
                            DungeonItemID.SWBoss
                        };
                    }
                case LocationID.ThievesTown:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.TTMapChest,
                            DungeonItemID.TTAmbushChest,
                            DungeonItemID.TTCompassChest,
                            DungeonItemID.TTBigKeyChest,
                            DungeonItemID.TTAttic,
                            DungeonItemID.TTBlindsCell,
                            DungeonItemID.TTBigChest,
                            DungeonItemID.TTBoss
                        };
                    }
                case LocationID.IcePalace:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.IPCompassChest,
                            DungeonItemID.IPSpikeRoom,
                            DungeonItemID.IPMapChest,
                            DungeonItemID.IPBigKeyChest,
                            DungeonItemID.IPFreezorChest,
                            DungeonItemID.IPBigChest,
                            DungeonItemID.IPIcedTRoom,
                            DungeonItemID.IPBoss
                        };
                    }
                case LocationID.MiseryMire:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMMainLobby,
                            DungeonItemID.MMCompassChest,
                            DungeonItemID.MMBigKeyChest,
                            DungeonItemID.MMBigChest,
                            DungeonItemID.MMMapChest,
                            DungeonItemID.MMBoss
                        };
                    }
                case LocationID.TurtleRock:
                    {
                        return new List<DungeonItemID>
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
                            DungeonItemID.TRLaserBrdigeBottomRight,
                            DungeonItemID.TRBoss
                        };
                    }
                case LocationID.GanonsTower:
                    {
                        return new List<DungeonItemID>
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
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a list of dungeon boss item IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// A list of dungeon boss item IDs.
        /// </returns>
        public List<DungeonItemID> GetDungeonBosses(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                    {
                        return new List<DungeonItemID>(0);
                    }
                case LocationID.AgahnimTower:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.ATBoss
                        };
                    }
                case LocationID.EasternPalace:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.EPBoss
                        };
                    }
                case LocationID.DesertPalace:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.DPBoss
                        };
                    }
                case LocationID.TowerOfHera:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBoss
                        };
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.PoDBoss
                        };
                    }
                case LocationID.SwampPalace:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.SPBoss
                        };
                    }
                case LocationID.SkullWoods:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.SWBoss
                        };
                    }
                case LocationID.ThievesTown:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.TTBoss
                        };
                    }
                case LocationID.IcePalace:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.IPBoss
                        };
                    }
                case LocationID.MiseryMire:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.MMBoss
                        };
                    }
                case LocationID.TurtleRock:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.TRBoss
                        };
                    }
                case LocationID.GanonsTower:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.GTBoss1,
                            DungeonItemID.GTBoss2,
                            DungeonItemID.GTBoss3,
                            DungeonItemID.GTFinalBoss
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a list of dungeon small key drop item IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// A list of dungeon small key drop item IDs.
        /// </returns>
        public List<DungeonItemID> GetDungeonSmallKeyDrops(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.HCBoomerangGuardDrop,
                            DungeonItemID.HCMapGuardDrop,
                            DungeonItemID.HCKeyRatDrop
                        };
                    }
                case LocationID.AgahnimTower:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.ATDarkArcherDrop,
                            DungeonItemID.ATCircleOfPotsDrop
                        };
                    }
                case LocationID.EasternPalace:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.EPDarkSquarePot,
                            DungeonItemID.EPDarkEyegoreDrop
                        };
                    }
                case LocationID.DesertPalace:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.DPTiles1Pot,
                            DungeonItemID.DPBeamosHallPot,
                            DungeonItemID.DPTiles2Pot
                        };
                    }
                case LocationID.TowerOfHera:
                case LocationID.PalaceOfDarkness:
                    {
                        return new List<DungeonItemID>(0);
                    }
                case LocationID.SwampPalace:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.SPPotRowPot,
                            DungeonItemID.SPTrench1Pot,
                            DungeonItemID.SPHookshotPot,
                            DungeonItemID.SPTrench2Pot,
                            DungeonItemID.SPWaterwayPot
                        };
                    }
                case LocationID.SkullWoods:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.SWWestLobbyPot,
                            DungeonItemID.SWSpikeCornerDrop
                        };
                    }
                case LocationID.ThievesTown:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.TTHallwayPot,
                            DungeonItemID.TTSpikeSwitchPot
                        };
                    }
                case LocationID.IcePalace:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.IPJellyDrop,
                            DungeonItemID.IPConveyerDrop,
                            DungeonItemID.IPHammerBlockDrop,
                            DungeonItemID.IPManyPotsPot
                        };
                    }
                case LocationID.MiseryMire:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.MMSpikesPot,
                            DungeonItemID.MMFishbonePot,
                            DungeonItemID.MMConveyerCrystalDrop
                        };
                    }
                case LocationID.TurtleRock:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.TRPokey1Drop,
                            DungeonItemID.TRPokey2Drop
                        };
                    }
                case LocationID.GanonsTower:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.GTConveyorCrossPot,
                            DungeonItemID.GTDoubleSwitchPot,
                            DungeonItemID.GTConveyorStarPitsPot,
                            DungeonItemID.GTMiniHelmasaurDrop
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a list of dungeon small key drop item IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// A list of dungeon small key drop item IDs.
        /// </returns>
        public List<DungeonItemID> GetDungeonBigKeyDrops(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                    {
                        return new List<DungeonItemID>
                        {
                            DungeonItemID.HCBigKeyDrop
                        };
                    }
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
                case LocationID.GanonsTower:
                    {
                        return new List<DungeonItemID>(0);
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a list of small key door IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// A list of small key door IDs.
        /// </returns>
        public List<KeyDoorID> GetDungeonSmallKeyDoors(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.HCEscapeFirstKeyDoor,
                            KeyDoorID.HCEscapeSecondKeyDoor,
                            KeyDoorID.HCDarkCrossKeyDoor,
                            KeyDoorID.HCSewerRatRoomKeyDoor
                        };
                    }
                case LocationID.AgahnimTower:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.ATFirstKeyDoor,
                            KeyDoorID.ATSecondKeyDoor,
                            KeyDoorID.ATThirdKeyDoor,
                            KeyDoorID.ATFourthKeyDoor
                        };
                    }
                case LocationID.EasternPalace:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.EPRightKeyDoor,
                            KeyDoorID.EPBackKeyDoor
                        };
                    }
                case LocationID.DesertPalace:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.DPRightKeyDoor,
                            KeyDoorID.DP1FKeyDoor,
                            KeyDoorID.DP2FFirstKeyDoor,
                            KeyDoorID.DP2FSecondKeyDoor
                        };
                    }
                case LocationID.TowerOfHera:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.ToHKeyDoor
                        };
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.PoDFrontKeyDoor,
                            KeyDoorID.PoDBigKeyChestKeyDoor,
                            KeyDoorID.PoDCollapsingWalkwayKeyDoor,
                            KeyDoorID.PoDDarkMazeKeyDoor,
                            KeyDoorID.PoDHarmlessHellwayKeyDoor,
                            KeyDoorID.PoDBossAreaKeyDoor
                        };
                    }
                case LocationID.SwampPalace:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.SP1FKeyDoor,
                            KeyDoorID.SPB1FirstRightKeyDoor,
                            KeyDoorID.SPB1SecondRightKeyDoor,
                            KeyDoorID.SPB1LeftKeyDoor,
                            KeyDoorID.SPB1BackFirstKeyDoor,
                            KeyDoorID.SPBossRoomKeyDoor
                        };
                    }
                case LocationID.SkullWoods:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.SWFrontLeftKeyDoor,
                            KeyDoorID.SWFrontRightKeyDoor,
                            KeyDoorID.SWWorthlessKeyDoor,
                            KeyDoorID.SWBackFirstKeyDoor,
                            KeyDoorID.SWBackSecondKeyDoor
                        };
                    }
                case LocationID.ThievesTown:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.TTFirstKeyDoor,
                            KeyDoorID.TTSecondKeyDoor,
                            KeyDoorID.TTBigChestKeyDoor
                        };
                    }
                case LocationID.IcePalace:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.IP1FKeyDoor,
                            KeyDoorID.IPB2KeyDoor,
                            KeyDoorID.IPB3KeyDoor,
                            KeyDoorID.IPB4KeyDoor,
                            KeyDoorID.IPB5KeyDoor,
                            KeyDoorID.IPB6KeyDoor
                        };
                    }
                case LocationID.MiseryMire:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.MMB1TopRightKeyDoor,
                            KeyDoorID.MMB1TopLeftKeyDoor,
                            KeyDoorID.MMB1LeftSideFirstKeyDoor,
                            KeyDoorID.MMB1LeftSideSecondKeyDoor,
                            KeyDoorID.MMB1RightSideKeyDoor,
                            KeyDoorID.MMB2WorthlessKeyDoor
                        };
                    }
                case LocationID.TurtleRock:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.TR1FFirstKeyDoor,
                            KeyDoorID.TR1FSecondKeyDoor,
                            KeyDoorID.TR1FThirdKeyDoor,
                            KeyDoorID.TRB1BigKeyChestKeyDoor,
                            KeyDoorID.TRB1toB2KeyDoor,
                            KeyDoorID.TRB2KeyDoor
                        };
                    }
                case LocationID.GanonsTower:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.GT1FLeftToRightKeyDoor,
                            KeyDoorID.GT1FMapChestRoomKeyDoor,
                            KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor,
                            KeyDoorID.GT1FFiresnakeRoomKeyDoor,
                            KeyDoorID.GT1FTileRoomKeyDoor,
                            KeyDoorID.GT1FCollapsingWalkwayKeyDoor,
                            KeyDoorID.GT6FFirstKeyDoor,
                            KeyDoorID.GT6FSecondKeyDoor
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a list of big key door IDs for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <returns>
        /// A list of big key door IDs.
        /// </returns>
        public List<KeyDoorID> GetDungeonBigKeyDoors(LocationID id)
        {
            switch (id)
            {
                case LocationID.HyruleCastle:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.HCZeldasCellDoor
                        };
                    }
                case LocationID.AgahnimTower:
                    {
                        return new List<KeyDoorID>(0);
                    }
                case LocationID.EasternPalace:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.EPBigChest,
                            KeyDoorID.EPBigKeyDoor
                        };
                    }
                case LocationID.DesertPalace:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.DPBigChest,
                            KeyDoorID.DPBigKeyDoor
                        };
                    }
                case LocationID.TowerOfHera:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.ToHBigKeyDoor,
                            KeyDoorID.ToHBigChest
                        };
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.PoDBigChest,
                            KeyDoorID.PoDBigKeyDoor
                        };
                    }
                case LocationID.SwampPalace:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.SPBigChest
                        };
                    }
                case LocationID.SkullWoods:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.SWBigChest
                        };
                    }
                case LocationID.ThievesTown:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.TTBigKeyDoor,
                            KeyDoorID.TTBigChest
                        };
                    }
                case LocationID.IcePalace:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.IPBigKeyDoor,
                            KeyDoorID.IPBigChest
                        };
                    }
                case LocationID.MiseryMire:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.MMBigChest,
                            KeyDoorID.MMPortalBigKeyDoor,
                            KeyDoorID.MMBridgeBigKeyDoor,
                            KeyDoorID.MMBossRoomBigKeyDoor
                        };
                    }
                case LocationID.TurtleRock:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.TRBigChest,
                            KeyDoorID.TRB1BigKeyDoor,
                            KeyDoorID.TRBossRoomBigKeyDoor
                        };
                    }
                case LocationID.GanonsTower:
                    {
                        return new List<KeyDoorID>
                        {
                            KeyDoorID.GTBigChest,
                            KeyDoorID.GT3FBigKeyDoor,
                            KeyDoorID.GT7FBigKeyDoor
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a list of dungeon entry nodes for the specified dungeon.
        /// </summary>
        /// <param name="id">
        /// The location ID of the dungeon.
        /// </param>
        /// <returns>
        /// A list of dungeon entry nodes.
        /// </returns>
        public List<IRequirementNode> GetDungeonEntryNodes(LocationID id)
        {
            return id switch
            {
                LocationID.HyruleCastle => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.HCSanctuaryEntry],
                    _requirementNodes[RequirementNodeID.HCFrontEntry],
                    _requirementNodes[RequirementNodeID.HCBackEntry]
                },
                LocationID.AgahnimTower => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.ATEntry]
                },
                LocationID.EasternPalace => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.EPEntry]
                },
                LocationID.DesertPalace => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.DPFrontEntry],
                    _requirementNodes[RequirementNodeID.DPLeftEntry],
                    _requirementNodes[RequirementNodeID.DPBackEntry]
                },
                LocationID.TowerOfHera => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.ToHEntry]
                },
                LocationID.PalaceOfDarkness => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.PoDEntry]
                },
                LocationID.SwampPalace => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.SPEntry]
                },
                LocationID.SkullWoods => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.SWFrontEntry],
                    _requirementNodes[RequirementNodeID.SWBackEntry]
                },
                LocationID.ThievesTown => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.TTEntry]
                },
                LocationID.IcePalace => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.IPEntry]
                },
                LocationID.MiseryMire => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.MMEntry]
                },
                LocationID.TurtleRock => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.TRFrontEntry],
                    _requirementNodes[RequirementNodeID.TRMiddleEntry],
                    _requirementNodes[RequirementNodeID.TRBackEntry]
                },
                LocationID.GanonsTower => new List<IRequirementNode>
                {
                    _requirementNodes[RequirementNodeID.GTEntry]
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }
    }
}
