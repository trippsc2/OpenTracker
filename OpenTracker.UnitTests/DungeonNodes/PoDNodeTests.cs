using OpenTracker.Models;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.DungeonNodes
{
    public class PoDNodeTests
    {
        [Theory]
        [MemberData(nameof(PoD_AccessibilityData))]
        [MemberData(nameof(PastFirstRedGoriyaRoom_AccessibilityData))]
        [MemberData(nameof(LobbyArena_AccessibilityData))]
        [MemberData(nameof(BigKeyChestArea_AccessibilityData))]
        [MemberData(nameof(PastCollapsingWalkwayKeyDoor_AccessibilityData))]
        [MemberData(nameof(DarkBasement_AccessibilityData))]
        [MemberData(nameof(HarmlessHellway_AccessibilityData))]
        [MemberData(nameof(PastDarkMazeKeyDoor_AccessibilityData))]
        [MemberData(nameof(DarkMaze_AccessibilityData))]
        [MemberData(nameof(BigChestLedge_AccessibilityData))]
        [MemberData(nameof(BigChest_AccessibilityData))]
        [MemberData(nameof(PastSecondRedGoriyaRoom_AccessibilityData))]
        [MemberData(nameof(PastBowStatue_AccessibilityData))]
        [MemberData(nameof(BossAreaDarkRooms_AccessibilityData))]
        [MemberData(nameof(PastHammerBlocks_AccessibilityData))]
        [MemberData(nameof(PastBossAreaKeyDoor_AccessibilityData))]
        [MemberData(nameof(BossRoom_AccessibilityData))]
        public void AccessibilityTests(
            DungeonNodeID id, WorldState worldState, DungeonItemShuffle dungeonItemShuffle,
            ItemPlacement itemPlacement, bool entranceShuffle, bool enemyShuffle,
            (ItemType, int)[] items, SequenceBreakType[] sequenceBreaksDisabled, List<KeyDoorID> keyDoors,
            AccessibilityLevel expected)
        {
            ItemDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();

            Mode.Instance.WorldState = worldState;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.ItemPlacement = itemPlacement;
            Mode.Instance.EntranceShuffle = entranceShuffle;
            Mode.Instance.EnemyShuffle = enemyShuffle;

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].SetCurrent(item.Item2);
            }

            foreach (var sequenceBreak in sequenceBreaksDisabled)
            {
                SequenceBreakDictionary.Instance[sequenceBreak].Enabled = false;
            }

            ((IDungeon)LocationDictionary.Instance[LocationID.PalaceOfDarkness]).DungeonDataQueue
                .TryPeek(out IMutableDungeon dungeonData);

            foreach (var keyDoor in dungeonData.SmallKeyDoors.Values)
            {
                keyDoor.Unlocked = keyDoors.Contains(keyDoor.ID);
            }

            foreach (var keyDoor in dungeonData.BigKeyDoors.Values)
            {
                keyDoor.Unlocked = keyDoors.Contains(keyDoor.ID);
            }

            Assert.Equal(expected, dungeonData.RequirementNodes[id].Accessibility);
        }

        public static IEnumerable<object[]> PoD_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoD,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoD,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoD,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.PoD,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.PoD,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoD,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoD,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.PoD,
                    WorldState.Retro,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.PoD,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoD,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.PoD,
                    WorldState.Inverted,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastFirstRedGoriyaRoom_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.MimicClip
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bottle, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.MimicClip
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bottle, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.MimicClip,
                        SequenceBreakType.CameraUnlock
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.MimicClip,
                        SequenceBreakType.CameraUnlock
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.PoDPastFirstRedGoriyaRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    true,
                    new (ItemType, int)[0],
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.MimicClip,
                        SequenceBreakType.CameraUnlock
                    },
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LobbyArena_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDLobbyArena,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDLobbyArena,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.PoDLobbyArena,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID> { },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BigKeyChestArea_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDBigKeyChestArea,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDBigKeyChestArea,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDBigKeyChestKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastCollapsingWalkwayKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkBasement_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDDarkBasement,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.PoDDarkBasement,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomPoDDarkBasement
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDDarkBasement,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomPoDDarkBasement
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.PoDDarkBasement,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Basic,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomPoDDarkBasement
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDDarkBasement,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomPoDDarkBasement
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HarmlessHellway_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDHarmlessHellwayRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDHarmlessHellwayRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor,
                        KeyDoorID.PoDHarmlessHellwayKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastDarkMazeKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDPastDarkMazeKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDPastDarkMazeKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor,
                        KeyDoorID.PoDDarkMazeKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkMaze_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDDarkMaze,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.BombJumpPoDHammerJump
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor,
                        KeyDoorID.PoDDarkMazeKeyDoor
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.PoDDarkMaze,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.BombJumpPoDHammerJump,
                        SequenceBreakType.DarkRoomPoDDarkMaze
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor,
                        KeyDoorID.PoDDarkMazeKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDDarkMaze,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.BombJumpPoDHammerJump,
                        SequenceBreakType.DarkRoomPoDDarkMaze
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor,
                        KeyDoorID.PoDDarkMazeKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.PoDDarkMaze,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor
                    },
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> BigChestLedge_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDBigChestLedge,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.BombJumpPoDHammerJump,
                        SequenceBreakType.DarkRoomPoDDarkMaze
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor,
                        KeyDoorID.PoDDarkMazeKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.PoDBigChestLedge,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor
                    },
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> BigChest_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDBigChest,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.BombJumpPoDHammerJump,
                        SequenceBreakType.DarkRoomPoDDarkMaze
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor,
                        KeyDoorID.PoDDarkMazeKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDBigChest,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.BombJumpPoDHammerJump,
                        SequenceBreakType.DarkRoomPoDDarkMaze
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDCollapsingStairwayKeyDoor,
                        KeyDoorID.PoDDarkMazeKeyDoor,
                        KeyDoorID.PoDBigChest
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastSecondRedGoriyaRoom_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDPastSecondRedGoriyaRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDPastSecondRedGoriyaRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    DungeonNodeID.PoDPastSecondRedGoriyaRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    true,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastBowStatue_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDPastBowStatue,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    true,
                    new (ItemType, int)[0],
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDPastBowStatue,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    true,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BossAreaDarkRooms_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDBossAreaDarkRooms,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    DungeonNodeID.PoDBossAreaDarkRooms,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomPoDBossArea
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDBossAreaDarkRooms,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[]
                    {
                        SequenceBreakType.DarkRoomPoDBossArea
                    },
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastHammerBlocks_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDPastHammerBlocks,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.Lamp, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDPastHammerBlocks,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PastBossAreaKeyDoor_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDPastBossAreaKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDPastBossAreaKeyDoor,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDBossAreaKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BossRoom_AccessibilityData =>
            new List<object[]>
            {
                new object[]
                {
                    DungeonNodeID.PoDBossRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDBossAreaKeyDoor
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    DungeonNodeID.PoDBossRoom,
                    WorldState.StandardOpen,
                    DungeonItemShuffle.Standard,
                    ItemPlacement.Advanced,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.Lamp, 1),
                        (ItemType.Hammer, 1)
                    },
                    new SequenceBreakType[0],
                    new List<KeyDoorID>
                    {
                        KeyDoorID.PoDFrontKeyDoor,
                        KeyDoorID.PoDBossAreaKeyDoor,
                        KeyDoorID.PoDBigKeyDoor
                    },
                    AccessibilityLevel.Normal
                }
            };
    }
}
