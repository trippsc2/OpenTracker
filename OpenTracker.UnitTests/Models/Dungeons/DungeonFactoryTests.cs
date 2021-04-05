using System;
using System.Collections.Generic;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Factories;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Nodes;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons
{
    public class DungeonFactoryTests
    {
        private readonly IItemDictionary _items;
        private readonly IOverworldNodeDictionary _overworldNodes;
        
        private 
            (ICappedItem? map, ICappedItem? compass, ISmallKeyItem smallKey, IBigKeyItem? bigKey,
            IList<DungeonItemID> dungeonItems, IList<DungeonItemID> bosses, IList<DungeonItemID> smallKeyDrops,
            IList<DungeonItemID> bigKeyDrops, IList<KeyDoorID> smallKeyDoors, IList<KeyDoorID> bigKeyDoors,
            IList<DungeonNodeID> nodes, IList<INode> entryNodes)? _factoryCall;
        
        private readonly DungeonFactory _sut;

        private static readonly Dictionary<DungeonID,
            (ItemType? map, ItemType? compass, ItemType smallKey, ItemType? bigKey, List<DungeonItemID> dungeonItems,
            List<DungeonItemID> bosses, List<DungeonItemID> smallKeyDrops, List<DungeonItemID> bigKeyDrops,
            List<KeyDoorID> smallKeyDoors, List<KeyDoorID> bigKeyDoors, List<DungeonNodeID> nodes,
            List<OverworldNodeID> entryNodes)> ExpectedValues = new();

        public DungeonFactoryTests()
        {
            static IItem GetItem(ItemType type)
            {
                return type switch
                {
                    ItemType.HCSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.EPSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.DPSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.ToHSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.ATSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.PoDSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.SPSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.SWSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.TTSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.IPSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.MMSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.TRSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.GTSmallKey => Substitute.For<ISmallKeyItem>(),
                    ItemType.HCBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.EPBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.DPBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.ToHBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.PoDBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.SPBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.SWBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.TTBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.IPBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.MMBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.TRBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.GTBigKey => Substitute.For<IBigKeyItem>(),
                    ItemType.HCMap => Substitute.For<ICappedItem>(),
                    ItemType.EPMap => Substitute.For<ICappedItem>(),
                    ItemType.DPMap => Substitute.For<ICappedItem>(),
                    ItemType.ToHMap => Substitute.For<ICappedItem>(),
                    ItemType.PoDMap => Substitute.For<ICappedItem>(),
                    ItemType.SPMap => Substitute.For<ICappedItem>(),
                    ItemType.SWMap => Substitute.For<ICappedItem>(),
                    ItemType.TTMap => Substitute.For<ICappedItem>(),
                    ItemType.IPMap => Substitute.For<ICappedItem>(),
                    ItemType.MMMap => Substitute.For<ICappedItem>(),
                    ItemType.TRMap => Substitute.For<ICappedItem>(),
                    ItemType.GTMap => Substitute.For<ICappedItem>(),
                    ItemType.EPCompass => Substitute.For<ICappedItem>(),
                    ItemType.DPCompass => Substitute.For<ICappedItem>(),
                    ItemType.ToHCompass => Substitute.For<ICappedItem>(),
                    ItemType.PoDCompass => Substitute.For<ICappedItem>(),
                    ItemType.SPCompass => Substitute.For<ICappedItem>(),
                    ItemType.SWCompass => Substitute.For<ICappedItem>(),
                    ItemType.TTCompass => Substitute.For<ICappedItem>(),
                    ItemType.IPCompass => Substitute.For<ICappedItem>(),
                    ItemType.MMCompass => Substitute.For<ICappedItem>(),
                    ItemType.TRCompass => Substitute.For<ICappedItem>(),
                    ItemType.GTCompass => Substitute.For<ICappedItem>(),
                    _ => Substitute.For<IItem>()
                };
            }
            
            var itemsFactory = Substitute.For<IItemFactory>();
            itemsFactory.GetItem(Arg.Any<ItemType>()).Returns(x => GetItem((ItemType)x[0]));
            _items = new ItemDictionary(() => itemsFactory);

            static INode GetOverworldNode()
            {
                return Substitute.For<INode>();
            }
            
            var overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
            overworldNodeFactory.GetOverworldNode(Arg.Any<OverworldNodeID>()).Returns(x =>
                GetOverworldNode());
            _overworldNodes = new OverworldNodeDictionary(() => overworldNodeFactory);

            IDungeon Factory(
                DungeonID id, ICappedItem? map, ICappedItem? compass, ISmallKeyItem smallKey, IBigKeyItem? bigKey,
                IList<DungeonItemID> dungeonItems, IList<DungeonItemID> bosses, IList<DungeonItemID> smallKeyDrops,
                IList<DungeonItemID> bigKeyDrops, IList<KeyDoorID> smallKeyDoors, IList<KeyDoorID> bigKeyDoors,
                IList<DungeonNodeID> nodes, IList<INode> entryNodes)
            {
                _factoryCall = (map, compass, smallKey, bigKey, dungeonItems, bosses, smallKeyDrops, bigKeyDrops,
                    smallKeyDoors, bigKeyDoors, nodes, entryNodes);
                
                return Substitute.For<IDungeon>();
            }

            _sut = new DungeonFactory(_items, _overworldNodes, Factory);
        }

        private static void PopulateExpectedValues()
        {
            foreach (DungeonID id in Enum.GetValues(typeof(DungeonID)))
            {
                ItemType? map = null;
                ItemType? compass = null;
                var smallKey = ItemType.HCSmallKey;
                ItemType? bigKey = null;

                List<DungeonItemID> dungeonItems = new();
                List<DungeonItemID> bosses = new();
                List<DungeonItemID> smallKeyDrops = new();
                List<DungeonItemID> bigKeyDrops = new();
                
                List<KeyDoorID> smallKeyDoors = new();
                List<KeyDoorID> bigKeyDoors = new();

                List<DungeonNodeID> nodes = new();
                List<OverworldNodeID> entryNodes = new();

                switch (id)
                {
                    case DungeonID.HyruleCastle:
                        map = ItemType.HCMap;
                        bigKey = ItemType.HCBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
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
                        smallKeyDrops = new List<DungeonItemID>
                        {
                            DungeonItemID.HCBoomerangGuardDrop,
                            DungeonItemID.HCMapGuardDrop,
                            DungeonItemID.HCKeyRatDrop
                        };
                        bigKeyDrops = new List<DungeonItemID> {DungeonItemID.HCBigKeyDrop};

                        smallKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.HCEscapeFirstKeyDoor,
                            KeyDoorID.HCEscapeSecondKeyDoor,
                            KeyDoorID.HCDarkCrossKeyDoor,
                            KeyDoorID.HCSewerRatRoomKeyDoor
                        };
                        bigKeyDoors = new List<KeyDoorID> {KeyDoorID.HCZeldasCellDoor};

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID>
                        {
                            OverworldNodeID.HCSanctuaryEntry,
                            OverworldNodeID.HCFrontEntry,
                            OverworldNodeID.HCBackEntry
                        };
                        break;
                    case DungeonID.AgahnimTower:
                        smallKey = ItemType.ATSmallKey;
                        
                        dungeonItems = new List<DungeonItemID>
                        {
                            DungeonItemID.ATRoom03,
                            DungeonItemID.ATDarkMaze
                        };
                        bosses = new List<DungeonItemID> {DungeonItemID.ATBoss};
                        smallKeyDrops = new List<DungeonItemID>
                        {
                            DungeonItemID.ATDarkArcherDrop,
                            DungeonItemID.ATCircleOfPotsDrop
                        };

                        smallKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.ATFirstKeyDoor,
                            KeyDoorID.ATSecondKeyDoor,
                            KeyDoorID.ATThirdKeyDoor,
                            KeyDoorID.ATFourthKeyDoor
                        };

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID> {OverworldNodeID.ATEntry};
                        break;
                    case DungeonID.EasternPalace:
                        map = ItemType.EPMap;
                        compass = ItemType.EPCompass;
                        smallKey = ItemType.EPSmallKey;
                        bigKey = ItemType.EPBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
                        {
                            DungeonItemID.EPCannonballChest,
                            DungeonItemID.EPMapChest,
                            DungeonItemID.EPCompassChest,
                            DungeonItemID.EPBigChest,
                            DungeonItemID.EPBigKeyChest,
                            DungeonItemID.EPBoss
                        };
                        bosses = new List<DungeonItemID> {DungeonItemID.EPBoss};
                        smallKeyDrops = new List<DungeonItemID>
                        {
                            DungeonItemID.EPDarkSquarePot,
                            DungeonItemID.EPDarkEyegoreDrop
                        };

                        smallKeyDoors = new List<KeyDoorID> {KeyDoorID.EPRightKeyDoor, KeyDoorID.EPBackKeyDoor};
                        bigKeyDoors = new List<KeyDoorID> {KeyDoorID.EPBigChest, KeyDoorID.EPBigKeyDoor};

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID> {OverworldNodeID.EPEntry};
                        break;
                    case DungeonID.DesertPalace:
                        map = ItemType.DPMap;
                        compass = ItemType.DPCompass;
                        smallKey = ItemType.DPSmallKey;
                        bigKey = ItemType.DPBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPBigChest,
                            DungeonItemID.DPCompassChest,
                            DungeonItemID.DPBigKeyChest,
                            DungeonItemID.DPBoss
                        };
                        bosses = new List<DungeonItemID> {DungeonItemID.DPBoss};
                        smallKeyDrops = new List<DungeonItemID>
                        {
                            DungeonItemID.DPTiles1Pot,
                            DungeonItemID.DPBeamosHallPot,
                            DungeonItemID.DPTiles2Pot
                        };

                        smallKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.DPRightKeyDoor,
                            KeyDoorID.DP1FKeyDoor,
                            KeyDoorID.DP2FFirstKeyDoor,
                            KeyDoorID.DP2FSecondKeyDoor
                        };
                        bigKeyDoors = new List<KeyDoorID> {KeyDoorID.DPBigChest, KeyDoorID.DPBigKeyDoor};

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID>
                        {
                            OverworldNodeID.DPFrontEntry,
                            OverworldNodeID.DPLeftEntry,
                            OverworldNodeID.DPBackEntry
                        };
                        break;
                    case DungeonID.TowerOfHera:
                        map = ItemType.ToHMap;
                        compass = ItemType.ToHCompass;
                        smallKey = ItemType.ToHSmallKey;
                        bigKey = ItemType.ToHBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBasementCage,
                            DungeonItemID.ToHMapChest,
                            DungeonItemID.ToHBigKeyChest,
                            DungeonItemID.ToHCompassChest,
                            DungeonItemID.ToHBigChest,
                            DungeonItemID.ToHBoss
                        };
                        bosses = new List<DungeonItemID> {DungeonItemID.ToHBoss};

                        smallKeyDoors = new List<KeyDoorID> {KeyDoorID.ToHKeyDoor};
                        bigKeyDoors = new List<KeyDoorID> {KeyDoorID.ToHBigKeyDoor, KeyDoorID.ToHBigChest};

                        nodes = new List<DungeonNodeID>
                        {
                            DungeonNodeID.ToH,
                            DungeonNodeID.ToHPastKeyDoor,
                            DungeonNodeID.ToHBasementTorchRoom,
                            DungeonNodeID.ToHPastBigKeyDoor,
                            DungeonNodeID.ToHBigChest,
                            DungeonNodeID.ToHBoss
                        };
                        entryNodes = new List<OverworldNodeID> {OverworldNodeID.ToHEntry};
                        break;
                    case DungeonID.PalaceOfDarkness:
                        map = ItemType.PoDMap;
                        compass = ItemType.PoDCompass;
                        smallKey = ItemType.PoDSmallKey;
                        bigKey = ItemType.PoDBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
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
                        bosses = new List<DungeonItemID> {DungeonItemID.PoDBoss};

                        smallKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.PoDFrontKeyDoor,
                            KeyDoorID.PoDBigKeyChestKeyDoor,
                            KeyDoorID.PoDCollapsingWalkwayKeyDoor,
                            KeyDoorID.PoDDarkMazeKeyDoor,
                            KeyDoorID.PoDHarmlessHellwayKeyDoor,
                            KeyDoorID.PoDBossAreaKeyDoor
                        };
                        bigKeyDoors = new List<KeyDoorID> {KeyDoorID.PoDBigChest, KeyDoorID.PoDBigKeyDoor};

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID> {OverworldNodeID.PoDEntry};
                        break;
                    case DungeonID.SwampPalace:
                        map = ItemType.SPMap;
                        compass = ItemType.SPCompass;
                        smallKey = ItemType.SPSmallKey;
                        bigKey = ItemType.SPBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
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
                        bosses = new List<DungeonItemID> {DungeonItemID.SPBoss};
                        smallKeyDrops = new List<DungeonItemID>
                        {
                            DungeonItemID.SPPotRowPot,
                            DungeonItemID.SPTrench1Pot,
                            DungeonItemID.SPHookshotPot,
                            DungeonItemID.SPTrench2Pot,
                            DungeonItemID.SPWaterwayPot
                        };

                        smallKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.SP1FKeyDoor,
                            KeyDoorID.SPB1FirstRightKeyDoor,
                            KeyDoorID.SPB1SecondRightKeyDoor,
                            KeyDoorID.SPB1LeftKeyDoor,
                            KeyDoorID.SPB1BackFirstKeyDoor,
                            KeyDoorID.SPBossRoomKeyDoor
                        };
                        bigKeyDoors = new List<KeyDoorID> {KeyDoorID.SPBigChest};

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID> {OverworldNodeID.SPEntry};
                        break;
                    case DungeonID.SkullWoods:
                        map = ItemType.SWMap;
                        compass = ItemType.SWCompass;
                        smallKey = ItemType.SWSmallKey;
                        bigKey = ItemType.SWBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
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
                        bosses = new List<DungeonItemID> {DungeonItemID.SWBoss};
                        smallKeyDrops = new List<DungeonItemID>
                        {
                            DungeonItemID.SWWestLobbyPot,
                            DungeonItemID.SWSpikeCornerDrop
                        };

                        smallKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.SWFrontLeftKeyDoor,
                            KeyDoorID.SWFrontRightKeyDoor,
                            KeyDoorID.SWWorthlessKeyDoor,
                            KeyDoorID.SWBackFirstKeyDoor,
                            KeyDoorID.SWBackSecondKeyDoor
                        };
                        bigKeyDoors = new List<KeyDoorID> {KeyDoorID.SWBigChest};

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID>
                        {
                            OverworldNodeID.SWFrontEntry,
                            OverworldNodeID.SWBackEntry
                        };
                        break;
                    case DungeonID.ThievesTown:
                        map = ItemType.TTMap;
                        compass = ItemType.TTCompass;
                        smallKey = ItemType.TTSmallKey;
                        bigKey = ItemType.TTBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
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
                        bosses = new List<DungeonItemID> {DungeonItemID.TTBoss};
                        smallKeyDrops = new List<DungeonItemID>
                        {
                            DungeonItemID.TTHallwayPot,
                            DungeonItemID.TTSpikeSwitchPot
                        };

                        smallKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.TTFirstKeyDoor,
                            KeyDoorID.TTSecondKeyDoor,
                            KeyDoorID.TTBigChestKeyDoor
                        };
                        bigKeyDoors = new List<KeyDoorID> {KeyDoorID.TTBigKeyDoor, KeyDoorID.TTBigChest};

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID> {OverworldNodeID.TTEntry};
                        break;
                    case DungeonID.IcePalace:
                        map = ItemType.IPMap;
                        compass = ItemType.IPCompass;
                        smallKey = ItemType.IPSmallKey;
                        bigKey = ItemType.IPBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
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
                        bosses = new List<DungeonItemID> {DungeonItemID.IPBoss};
                        smallKeyDrops = new List<DungeonItemID>
                        {
                            DungeonItemID.IPJellyDrop,
                            DungeonItemID.IPConveyerDrop,
                            DungeonItemID.IPHammerBlockDrop,
                            DungeonItemID.IPManyPotsPot
                        };

                        smallKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.IP1FKeyDoor,
                            KeyDoorID.IPB2KeyDoor,
                            KeyDoorID.IPB3KeyDoor,
                            KeyDoorID.IPB4KeyDoor,
                            KeyDoorID.IPB5KeyDoor,
                            KeyDoorID.IPB6KeyDoor
                        };
                        bigKeyDoors = new List<KeyDoorID> {KeyDoorID.IPBigKeyDoor, KeyDoorID.IPBigChest};

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID> {OverworldNodeID.IPEntry};
                        break;
                    case DungeonID.MiseryMire:
                        map = ItemType.MMMap;
                        compass = ItemType.MMCompass;
                        smallKey = ItemType.MMSmallKey;
                        bigKey = ItemType.MMBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
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
                        bosses = new List<DungeonItemID> {DungeonItemID.MMBoss};
                        smallKeyDrops = new List<DungeonItemID>
                        {
                            DungeonItemID.MMSpikesPot,
                            DungeonItemID.MMFishbonePot,
                            DungeonItemID.MMConveyerCrystalDrop
                        };

                        smallKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.MMB1TopRightKeyDoor,
                            KeyDoorID.MMB1TopLeftKeyDoor,
                            KeyDoorID.MMB1LeftSideFirstKeyDoor,
                            KeyDoorID.MMB1LeftSideSecondKeyDoor,
                            KeyDoorID.MMB1RightSideKeyDoor,
                            KeyDoorID.MMB2WorthlessKeyDoor
                        };
                        bigKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.MMBigChest,
                            KeyDoorID.MMPortalBigKeyDoor,
                            KeyDoorID.MMBridgeBigKeyDoor,
                            KeyDoorID.MMBossRoomBigKeyDoor
                        };

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID> {OverworldNodeID.MMEntry};
                        break;
                    case DungeonID.TurtleRock:
                        map = ItemType.TRMap;
                        compass = ItemType.TRCompass;
                        smallKey = ItemType.TRSmallKey;
                        bigKey = ItemType.TRBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
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
                        };
                        bosses = new List<DungeonItemID> {DungeonItemID.TRBoss};
                        smallKeyDrops = new List<DungeonItemID>
                        {
                            DungeonItemID.TRPokey1Drop,
                            DungeonItemID.TRPokey2Drop
                        };

                        smallKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.TR1FFirstKeyDoor,
                            KeyDoorID.TR1FSecondKeyDoor,
                            KeyDoorID.TR1FThirdKeyDoor,
                            KeyDoorID.TRB1BigKeyChestKeyDoor,
                            KeyDoorID.TRB1ToB2KeyDoor,
                            KeyDoorID.TRB2KeyDoor
                        };
                        bigKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.TRBigChest,
                            KeyDoorID.TRB1BigKeyDoor,
                            KeyDoorID.TRBossRoomBigKeyDoor
                        };

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID>
                        {
                            OverworldNodeID.TRFrontEntry,
                            OverworldNodeID.TRMiddleEntry,
                            OverworldNodeID.TRBackEntry
                        };
                        break;
                    case DungeonID.GanonsTower:
                        map = ItemType.GTMap;
                        compass = ItemType.GTCompass;
                        smallKey = ItemType.GTSmallKey;
                        bigKey = ItemType.GTBigKey;
                        
                        dungeonItems = new List<DungeonItemID>
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
                        bosses = new List<DungeonItemID>
                        {
                            DungeonItemID.GTBoss1,
                            DungeonItemID.GTBoss2,
                            DungeonItemID.GTBoss3,
                            DungeonItemID.GTFinalBoss
                        };
                        smallKeyDrops = new List<DungeonItemID>
                        {
                            DungeonItemID.GTConveyorCrossPot,
                            DungeonItemID.GTDoubleSwitchPot,
                            DungeonItemID.GTConveyorStarPitsPot,
                            DungeonItemID.GTMiniHelmasaurDrop
                        };

                        smallKeyDoors = new List<KeyDoorID>
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
                        bigKeyDoors = new List<KeyDoorID>
                        {
                            KeyDoorID.GTBigChest,
                            KeyDoorID.GT3FBigKeyDoor,
                            KeyDoorID.GT7FBigKeyDoor
                        };

                        nodes = new List<DungeonNodeID>
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
                        };
                        entryNodes = new List<OverworldNodeID> {OverworldNodeID.GTEntry};
                        break;
                }
                
                ExpectedValues.Add(id,
                    (map, compass, smallKey, bigKey, dungeonItems, bosses, smallKeyDrops, bigKeyDrops, smallKeyDoors,
                        bigKeyDoors, nodes, entryNodes));
            }
        }

        [Fact]
        public void GetDungeon_ShouldThrowException_WhenDungeonIDIsUnexpected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.GetDungeon((DungeonID) int.MaxValue));
        }

        [Theory]
        [MemberData(nameof(GetDungeon_ShouldReturnExpectedMapData))]
        public void GetDungeon_ShouldReturnExpectedMap(ItemType? expected, DungeonID id)
        {
            _ = _sut.GetDungeon(id);

            var item = expected is not null ? (ICappedItem?)_items[expected.Value] : null; 
            
            Assert.Equal(item, _factoryCall!.Value.map);
        }

        public static IEnumerable<object?[]> GetDungeon_ShouldReturnExpectedMapData()
        {
            PopulateExpectedValues();
            
            var results = new List<object?[]>();
            
            foreach (var id in ExpectedValues.Keys)
            {
                if (ExpectedValues[id].map is null)
                {
                    results.Add(new object?[] {null, id});
                    continue;
                }
                
                results.Add(new object?[] {ExpectedValues[id].map, id});
            }

            return results;
        }
    }
}