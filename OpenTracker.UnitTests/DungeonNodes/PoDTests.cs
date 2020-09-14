using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.DungeonNodes
{
    [Collection("Tests")]
    public class PoDTests
    {
        [Theory]
        [MemberData(nameof(PoDEntry_To_PoD))]
        [MemberData(nameof(PoDLobbyArena_To_PoD))]
        [MemberData(nameof(PoD_To_PoDPastFirstRedGoriyaRoom))]
        [MemberData(nameof(PoD_To_PoDFrontKeyDoor))]
        [MemberData(nameof(PoDLobbyArena_To_PoDFrontKeyDoor))]
        [MemberData(nameof(PoD_To_PoDLobbyArena))]
        [MemberData(nameof(PoDPastFirstRedGoriyaRoom_To_PoDLobbyArena))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoor_To_PoDLobbyArena))]
        [MemberData(nameof(PoDLobbyArena_To_PoDBigKeyChestArea))]
        [MemberData(nameof(PoDLobbyArena_To_PoDCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoor_To_PoDCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(PoDLobbyArena_To_PoDPastCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(PoDPastDarkMazeKeyDoor_To_PoDPastCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(PoDHarmlessHellwayRoom_To_PoDPastCollapsingWalkwayKeyDoor))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoor_To_PoDDarkBasement))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoor_To_PoDHarmlessHellwayKeyDoor))]
        [MemberData(nameof(PoDHarmlessHellwayRoom_To_PoDHarmlessHellwayKeyDoor))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoor_To_PoDHarmlessHellwayRoom))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoor_To_PoDDarkMazeKeyDoor))]
        [MemberData(nameof(PoDPastDarkMazeKeyDoor_To_PoDDarkMazeKeyDoor))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoor_To_PoDPastDarkMazeKeyDoor))]
        [MemberData(nameof(PoDDarkMaze_To_PoDPastDarkMazeKeyDoor))]
        [MemberData(nameof(PoDPastDarkMazeKeyDoor_To_PoDDarkMaze))]
        [MemberData(nameof(PoDBigChestLedge_To_PoDDarkMaze))]
        [MemberData(nameof(PoDDarkMaze_To_PoDBigChestLedge))]
        [MemberData(nameof(PoDPastCollapsingWalkwayKeyDoor_To_PoDBigChestLedge))]
        [MemberData(nameof(PoDBigChestLedge_To_PoDBigChest))]
        [MemberData(nameof(PoDLobbyArena_To_PoDPastSecondRedGoriyaRoom))]
        [MemberData(nameof(PoDPastSecondRedGoriyaRoom_To_PoDPastBowStatue))]
        [MemberData(nameof(PoDPastBowStatue_To_PoDBossAreaDarkRooms))]
        [MemberData(nameof(PoDBossAreaDarkRooms_To_PoDPastHammerBlocks))]
        [MemberData(nameof(PoDPastBossAreaKeyDoor_To_PoDPastHammerBlocks))]
        [MemberData(nameof(PoDPastHammerBlocks_To_PoDBossAreaKeyDoor))]
        [MemberData(nameof(PoDPastBossAreaKeyDoor_To_PoDBossAreaKeyDoor))]
        [MemberData(nameof(PoDPastHammerBlocks_To_PoDPastBossAreaKeyDoor))]
        [MemberData(nameof(PoDPastBossAreaKeyDoor_To_PoDBossRoom))]
        public void Tests(
            ModeSaveData mode, RequirementNodeID[] accessibleEntryNodes,
            DungeonNodeID[] accessibleNodes, KeyDoorID[] unlockedDoors, (ItemType, int)[] items,
            (SequenceBreakType, bool)[] sequenceBreaks, LocationID dungeonID, DungeonNodeID id,
            AccessibilityLevel expected)
        {
            RequirementNodeDictionary.Instance.Reset();
            var dungeon = (IDungeon)LocationDictionary.Instance[dungeonID];
            var dungeonData = MutableDungeonFactory.GetMutableDungeon(dungeon);
            dungeon.FinishMutableDungeonCreation(dungeonData);
            dungeonData.Reset();
            ItemDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();
            Mode.Instance.Load(mode);

            foreach (var node in accessibleEntryNodes)
            {
                RequirementNodeDictionary.Instance[node].AlwaysAccessible = true;
            }

            foreach (var node in accessibleNodes)
            {
                dungeonData.Nodes[node].AlwaysAccessible = true;
            }

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled =
                    sequenceBreak.Item2;
            }

            foreach (var door in unlockedDoors)
            {
                dungeonData.KeyDoorDictionary[door].Unlocked = true;
            }

            Assert.Equal(expected, dungeonData.Nodes[id].Accessibility);
        }

        public static IEnumerable<object[]> PoDEntry_To_PoD =>
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
                    new RequirementNodeID[]
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

        public static IEnumerable<object[]> PoDLobbyArena_To_PoD =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoD_To_PoDPastFirstRedGoriyaRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.CameraUnlock, true),
                        (SequenceBreakType.MimicClip, true)
                    },
                    LocationID.PalaceOfDarkness,
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PoD_To_PoDFrontKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDLobbyArena_To_PoDFrontKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoD_To_PoDLobbyArena =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoD
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoDPastFirstRedGoriyaRoom_To_PoDLobbyArena =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoor_To_PoDLobbyArena =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoDLobbyArena_To_PoDBigKeyChestArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoDLobbyArena_To_PoDCollapsingWalkwayKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoor_To_PoDCollapsingWalkwayKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDLobbyArena_To_PoDPastCollapsingWalkwayKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDLobbyArena
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoDPastDarkMazeKeyDoor_To_PoDPastCollapsingWalkwayKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastDarkMazeKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoDHarmlessHellwayRoom_To_PoDPastCollapsingWalkwayKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDHarmlessHellwayRoom
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoor_To_PoDDarkBasement =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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

        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoor_To_PoDHarmlessHellwayKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDHarmlessHellwayRoom_To_PoDHarmlessHellwayKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoor_To_PoDHarmlessHellwayRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoor_To_PoDDarkMazeKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastDarkMazeKeyDoor_To_PoDDarkMazeKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoor_To_PoDPastDarkMazeKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoDDarkMaze_To_PoDPastDarkMazeKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastDarkMazeKeyDoor_To_PoDDarkMaze =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDBigChestLedge_To_PoDDarkMaze =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDDarkMaze_To_PoDBigChestLedge =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastCollapsingWalkwayKeyDoor_To_PoDBigChestLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDBigChestLedge_To_PoDBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDBigChestLedge
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoDLobbyArena_To_PoDPastSecondRedGoriyaRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastSecondRedGoriyaRoom_To_PoDPastBowStatue =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastBowStatue_To_PoDBossAreaDarkRooms =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDBossAreaDarkRooms_To_PoDPastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastBossAreaKeyDoor_To_PoDPastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastBossAreaKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoDPastHammerBlocks_To_PoDBossAreaKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastBossAreaKeyDoor_To_PoDBossAreaKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> PoDPastHammerBlocks_To_PoDPastBossAreaKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastHammerBlocks
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> PoDPastBossAreaKeyDoor_To_PoDBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.PoDPastBossAreaKeyDoor
                    },
                    new KeyDoorID[]
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
