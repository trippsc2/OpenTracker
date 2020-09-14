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
    public class TTTests
    {
        [Theory]
        [MemberData(nameof(TTEntry_To_TT))]
        [MemberData(nameof(TTPastBigKeyDoor_To_TT))]
        [MemberData(nameof(TT_To_TTBigKeyDoor))]
        [MemberData(nameof(TTPastBigKeyDoor_To_TTBigKeyDoor))]
        [MemberData(nameof(TT_To_TTPastBigKeyDoor))]
        [MemberData(nameof(TTPastFirstKeyDoor_To_TTPastBigKeyDoor))]
        [MemberData(nameof(TTPastBigKeyDoor_To_TTFirstKeyDoor))]
        [MemberData(nameof(TTPastFirstKeyDoor_To_TTFirstKeyDoor))]
        [MemberData(nameof(TTPastBigKeyDoor_To_TTPastFirstKeyDoor))]
        [MemberData(nameof(TTPastBigChestRoomKeyDoor_To_TTPastFirstKeyDoor))]
        [MemberData(nameof(TTPastFirstKeyDoor_To_TTPastSecondKeyDoor))]
        [MemberData(nameof(TTPastFirstKeyDoor_To_TTBigChestKeyDoor))]
        [MemberData(nameof(TTPastBigChestRoomKeyDoor_To_TTBigChestKeyDoor))]
        [MemberData(nameof(TTPastFirstKeyDoor_To_TTPastBigChestRoomKeyDoor))]
        [MemberData(nameof(TTPastBigChestRoomKeyDoor_To_TTPastHammerBlocks))]
        [MemberData(nameof(TTPastHammerBlocks_To_TTBigChest))]
        [MemberData(nameof(TTPastBigKeyDoor_To_TTBossRoom))]
        [MemberData(nameof(TTPastSecondKeyDoor_To_TTBossRoom))]
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

        public static IEnumerable<object[]> TTEntry_To_TT =>
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
                    LocationID.ThievesTown,
                    DungeonNodeID.TT,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TTEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TT,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastBigKeyDoor_To_TT =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TT,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TTBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TT,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TT_To_TTBigKeyDoor =>
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
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TT
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastBigKeyDoor_To_TTBigKeyDoor =>
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
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TT_To_TTPastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TT
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TT
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TTBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastFirstKeyDoor_To_TTPastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TTFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastBigKeyDoor_To_TTFirstKeyDoor =>
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
                    LocationID.ThievesTown,
                    DungeonNodeID.TTFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastFirstKeyDoor_To_TTFirstKeyDoor =>
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
                    LocationID.ThievesTown,
                    DungeonNodeID.TTFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastBigKeyDoor_To_TTPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TTFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastBigChestRoomKeyDoor_To_TTPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigChestRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigChestRoomKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TTBigChestKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastFirstKeyDoor_To_TTPastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TTSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastFirstKeyDoor_To_TTBigChestKeyDoor =>
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
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBigChestKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBigChestKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastBigChestRoomKeyDoor_To_TTBigChestKeyDoor =>
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
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBigChestKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigChestRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBigChestKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastFirstKeyDoor_To_TTPastBigChestRoomKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastBigChestRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TTBigChestKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastBigChestRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastBigChestRoomKeyDoor_To_TTPastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigChestRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastHammerBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigChestRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTPastHammerBlocks,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastHammerBlocks_To_TTBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastHammerBlocks
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.TTBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBigChest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastBigKeyDoor_To_TTBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        BossShuffle = false
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        BossShuffle = true
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBossRoom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TTPastSecondKeyDoor_To_TTBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        BossShuffle = true
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        BossShuffle = false
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.TTPastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.ThievesTown,
                    DungeonNodeID.TTBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
