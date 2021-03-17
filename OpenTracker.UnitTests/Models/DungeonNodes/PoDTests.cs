using System.Collections.Generic;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.DungeonNodes
{
    public class PoDTests : DungeonNodeTestBase
    {
        [Theory]
        [MemberData(nameof(PoDEntryToPoD))]
        [MemberData(nameof(PoDLobbyArenaToPoD))]
        [MemberData(nameof(PoDToPoDPastFirstRedGoriyaRoom))]
        [MemberData(nameof(PoDToPoDFrontKeyDoor))]
        [MemberData(nameof(PoDLobbyArenaToPoDFrontKeyDoor))]
        [MemberData(nameof(PoDToPoDLobbyArena))]
        [MemberData(nameof(PoDPastFirstRedGoriyaRoomToPoDLobbyArena))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoorToPoDLobbyArena))]
        [MemberData(nameof(PoDLobbyArenaToPoDBigKeyChestArea))]
        [MemberData(nameof(PoDLobbyArenaToPoDCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoorToPoDCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(PoDLobbyArenaToPoDPastCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(PoDPastDarkMazeKeyDoorToPoDPastCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(PoDHarmlessHellwayRoomToPoDPastCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoorToPoDDarkBasement))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoorToPoDHarmlessHellwayKeyDoor))]
        [MemberData(nameof(PoDHarmlessHellwayRoomToPoDHarmlessHellwayKeyDoor))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoorToPoDHarmlessHellwayRoom))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoorToPoDDarkMazeKeyDoor))]
        [MemberData(nameof(PoDPastDarkMazeKeyDoorToPoDDarkMazeKeyDoor))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoorToPoDPastDarkMazeKeyDoor))]
        [MemberData(nameof(PoDDarkMazeToPoDPastDarkMazeKeyDoor))]
        [MemberData(nameof(PoDPastDarkMazeKeyDoorToPoDDarkMaze))]
        [MemberData(nameof(PoDBigChestLedgeToPoDDarkMaze))]
        [MemberData(nameof(PoDDarkMazeToPoDBigChestLedge))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoorToPoDBigChestLedge))]
        [MemberData(nameof(PoDBigChestLedgeToPoDBigChest))]
        [MemberData(nameof(PoDLobbyArenaToPoDPastSecondRedGoriyaRoom))]
        [MemberData(nameof(PoDPastSecondRedGoriyaRoomToPoDPastBowStatue))]
        [MemberData(nameof(PoDPastBowStatueToPoDBossAreaDarkRooms))]
        [MemberData(nameof(PoDBossAreaDarkRoomsToPoDPastHammerBlocks))]
        [MemberData(nameof(PoDPastBossAreaKeyDoorToPoDPastHammerBlocks))]
        [MemberData(nameof(PoDPastHammerBlocksToPoDBossAreaKeyDoor))]
        [MemberData(nameof(PoDPastBossAreaKeyDoorToPoDBossAreaKeyDoor))]
        [MemberData(nameof(PoDPastHammerBlocksToPoDPastBossAreaKeyDoor))]
        [MemberData(nameof(PoDPastBossAreaKeyDoorToPoDBossRoom))]
        public override void Tests(
            ModeSaveData modeData, RequirementNodeID[] accessibleEntryNodes,
            DungeonNodeID[] accessibleNodes, KeyDoorID[] unlockedDoors, (ItemType, int)[] items,
            (SequenceBreakType, bool)[] sequenceBreaks, LocationID dungeonID, DungeonNodeID id,
            AccessibilityLevel expected)
        {
            base.Tests(
                modeData, accessibleEntryNodes, accessibleNodes, unlockedDoors, items,
                sequenceBreaks, dungeonID, id, expected);
        }
        
        public static IEnumerable<object[]> PoDEntryToPoD =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoD,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.PoDEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoD,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDLobbyArenaToPoD =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoD,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new[]
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoD,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDToPoDPastFirstRedGoriyaRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.Bottle, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.CameraUnlock, false),
                        (SequenceBreakType.MimicClip, false)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.Bottle, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.CameraUnlock, false),
                        (SequenceBreakType.MimicClip, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.Bottle, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.CameraUnlock, true),
                        (SequenceBreakType.MimicClip, false)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.Bottle, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.CameraUnlock, true),
                        (SequenceBreakType.MimicClip, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = true
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.Bottle, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.CameraUnlock, true),
                        (SequenceBreakType.MimicClip, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDToPoDFrontKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDFrontKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDFrontKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDLobbyArenaToPoDFrontKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDFrontKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDFrontKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDToPoDLobbyArena =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDLobbyArena,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoD
                    },
                    new[]
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDLobbyArena,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastFirstRedGoriyaRoomToPoDLobbyArena =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastFirstRedGoriyaRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDLobbyArena,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastFirstRedGoriyaRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDLobbyArena,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoorToPoDLobbyArena =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDLobbyArena,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.PoDCollapsingWalkwayKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDLobbyArena,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDLobbyArenaToPoDBigKeyChestArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBigKeyChestArea,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new[]
                    {
                        KeyDoorID.PoDBigKeyChestKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBigKeyChestArea,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDLobbyArenaToPoDCollapsingWalkwayKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoorToPoDCollapsingWalkwayKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDLobbyArenaToPoDPastCollapsingWalkwayKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new[]
                    {
                        KeyDoorID.PoDCollapsingWalkwayKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastDarkMazeKeyDoorToPoDPastCollapsingWalkwayKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastDarkMazeKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastDarkMazeKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.PoDDarkMazeKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDHarmlessHellwayRoomToPoDPastCollapsingWalkwayKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDHarmlessHellwayRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDHarmlessHellwayRoom
                    },
                    new[]
                    {
                        KeyDoorID.PoDHarmlessHellwayKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoorToPoDDarkBasement =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkBasement, false)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkBasement,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkBasement, false)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkBasement,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkBasement, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkBasement,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkBasement, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkBasement,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkBasement, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkBasement,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkBasement, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkBasement,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkBasement, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkBasement,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoorToPoDHarmlessHellwayKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDHarmlessHellwayKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDHarmlessHellwayKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDHarmlessHellwayRoomToPoDHarmlessHellwayKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDHarmlessHellwayKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDHarmlessHellwayRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDHarmlessHellwayKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoorToPoDHarmlessHellwayRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDHarmlessHellwayRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.PoDHarmlessHellwayKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDHarmlessHellwayRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoorToPoDDarkMazeKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkMazeKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkMazeKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastDarkMazeKeyDoorToPoDDarkMazeKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkMazeKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastDarkMazeKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkMazeKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoorToPoDPastDarkMazeKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpPoDHammerJump, false)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastDarkMazeKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.PoDDarkMazeKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpPoDHammerJump, false)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastDarkMazeKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDDarkMazeToPoDPastDarkMazeKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastDarkMazeKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastDarkMazeKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastDarkMazeKeyDoorToPoDDarkMaze =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastDarkMazeKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkMaze, false)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkMaze,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastDarkMazeKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkMaze, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkMaze,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastDarkMazeKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkMaze, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkMaze,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDBigChestLedgeToPoDDarkMaze =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDBigChestLedge
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkMaze, false)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkMaze,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDBigChestLedge
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkMaze, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkMaze,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDBigChestLedge
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDDarkMaze, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDDarkMaze,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDDarkMazeToPoDBigChestLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBigChestLedge,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBigChestLedge,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoorToPoDBigChestLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpPoDHammerJump, false)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBigChestLedge,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpPoDHammerJump, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBigChestLedge,
                    AccessibilityLevel.SequenceBreak
                },
            };
        
        public static IEnumerable<object[]> PoDBigChestLedgeToPoDBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDBigChestLedge
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDBigChestLedge
                    },
                    new[]
                    {
                        KeyDoorID.PoDBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBigChest,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDLobbyArenaToPoDPastSecondRedGoriyaRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, false)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastSecondRedGoriyaRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastSecondRedGoriyaRoom,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastSecondRedGoriyaRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = true
                    },
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MimicClip, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastSecondRedGoriyaRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastSecondRedGoriyaRoomToPoDPastBowStatue =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastSecondRedGoriyaRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastBowStatue,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastSecondRedGoriyaRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastBowStatue,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastBowStatueToPoDBossAreaDarkRooms =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastBowStatue
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDBossArea, false)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBossAreaDarkRooms,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastBowStatue
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDBossArea, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBossAreaDarkRooms,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastBowStatue
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomPoDBossArea, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBossAreaDarkRooms,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDBossAreaDarkRoomsToPoDPastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDBossAreaDarkRooms
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastHammerBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDBossAreaDarkRooms
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastHammerBlocks,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastBossAreaKeyDoorToPoDPastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastBossAreaKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastHammerBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastBossAreaKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.PoDBossAreaKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastHammerBlocks,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastHammerBlocksToPoDBossAreaKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBossAreaKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBossAreaKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastBossAreaKeyDoorToPoDBossAreaKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBossAreaKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastBossAreaKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBossAreaKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastHammerBlocksToPoDPastBossAreaKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastBossAreaKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastHammerBlocks
                    },
                    new[]
                    {
                        KeyDoorID.PoDBossAreaKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastBossAreaKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> PoDPastBossAreaKeyDoorToPoDBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastBossAreaKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.PoDPastBossAreaKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.PoDBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
