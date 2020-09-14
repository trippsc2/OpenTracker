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
    public class SWTests
    {
        [Theory]
        [MemberData(nameof(SWFrontEntry_To_SWBigChestAreaBottom))]
        [MemberData(nameof(SWBigChestAreaTop_To_SWBigChestAreaBottom))]
        [MemberData(nameof(SWFrontLeftSide_To_SWBigChestAreaBottom))]
        [MemberData(nameof(SWFrontRightSide_To_SWBigChestAreaBottom))]
        [MemberData(nameof(SWFrontEntry_To_SWBigChestAreaTop))]
        [MemberData(nameof(SWBigChestAreaBottom_To_SWBigChestAreaTop))]
        [MemberData(nameof(SWBigChestAreaTop_To_SWBigChest))]
        [MemberData(nameof(SWFrontLeftSide_To_SWFrontLeftKeyDoor))]
        [MemberData(nameof(SWBigChestAreaBottom_To_SWFrontLeftKeyDoor))]
        [MemberData(nameof(SWFrontEntry_To_SWFrontLeftSide))]
        [MemberData(nameof(SWBigChestAreaBottom_To_SWFrontLeftSide))]
        [MemberData(nameof(SWFrontRightSide_To_SWFrontRightKeyDoor))]
        [MemberData(nameof(SWBigChestAreaBottom_To_SWFrontRightKeyDoor))]
        [MemberData(nameof(SWFrontEntry_To_SWFrontRightSide))]
        [MemberData(nameof(SWFrontLeftSide_To_SWFrontRightSide))]
        [MemberData(nameof(SWBigChestAreaBottom_To_SWFrontRightSide))]
        [MemberData(nameof(SWFrontEntry_To_SWFrontBackConnector))]
        [MemberData(nameof(SWPastTheWorthlessKeyDoor_To_SWFrontBackConnector))]
        [MemberData(nameof(SWFrontBackConnector_To_SWWorthlessKeyDoor))]
        [MemberData(nameof(SWPastTheWorthlessKeyDoor_To_SWWorthlessKeyDoor))]
        [MemberData(nameof(SWFrontBackConnector_To_SWPastTheWorthlessKeyDoor))]
        [MemberData(nameof(SWBackEntry_To_SWBack))]
        [MemberData(nameof(SWBackFirstKeyDoor_To_SWBack))]
        [MemberData(nameof(SWBack_To_SWBackFirstKeyDoor))]
        [MemberData(nameof(SWBackPastFirstKeyDoor_To_SWBackFirstKeyDoor))]
        [MemberData(nameof(SWBack_To_SWBackPastFirstKeyDoor))]
        [MemberData(nameof(SWBackPastFirstKeyDoor_To_SWBackPastFourTorchRoom))]
        [MemberData(nameof(SWBackPastFourTorchRoom_To_SWBackPastCurtains))]
        [MemberData(nameof(SWBossRoom_To_SWBackPastCurtains))]
        [MemberData(nameof(SWBackPastCurtains_To_SWBackSecondKeyDoor))]
        [MemberData(nameof(SWBossRoom_To_SWBackSecondKeyDoor))]
        [MemberData(nameof(SWBackPastCurtains_To_SWBossRoom))]
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

        public static IEnumerable<object[]> SWFrontEntry_To_SWBigChestAreaBottom =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SWFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBigChestAreaTop_To_SWBigChestAreaBottom =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaTop
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWFrontLeftSide_To_SWBigChestAreaBottom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWFrontLeftSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWFrontLeftSide
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SWFrontLeftKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWFrontRightSide_To_SWBigChestAreaBottom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWFrontRightSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWFrontRightSide
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SWFrontRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaBottom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWFrontEntry_To_SWBigChestAreaTop =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaTop,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SWFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaTop,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBigChestAreaBottom_To_SWBigChestAreaTop =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpSWBigChest, false)
                    },
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaTop,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpSWBigChest, true)
                    },
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaTop,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpSWBigChest, true)
                    },
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChestAreaTop,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBigChestAreaTop_To_SWBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaTop
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaTop
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SWBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBigChest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWFrontLeftSide_To_SWFrontLeftKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWFrontLeftSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBigChestAreaBottom_To_SWFrontLeftKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWFrontEntry_To_SWFrontLeftSide =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SWFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftSide,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBigChestAreaBottom_To_SWFrontLeftSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SWFrontLeftKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontLeftSide,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWFrontRightSide_To_SWFrontRightKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWFrontRightSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBigChestAreaBottom_To_SWFrontRightKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWFrontEntry_To_SWFrontRightSide =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SWFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWFrontLeftSide_To_SWFrontRightSide =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWFrontLeftSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBigChestAreaBottom_To_SWFrontRightSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBigChestAreaBottom
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SWFrontRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontRightSide,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWFrontEntry_To_SWFrontBackConnector =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontBackConnector,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SWFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontBackConnector,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWPastTheWorthlessKeyDoor_To_SWFrontBackConnector =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWPastTheWorthlessKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontBackConnector,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWPastTheWorthlessKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SWWorthlessKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWFrontBackConnector,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWFrontBackConnector_To_SWWorthlessKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWWorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWFrontBackConnector
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWWorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWPastTheWorthlessKeyDoor_To_SWWorthlessKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWWorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWPastTheWorthlessKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWWorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWFrontBackConnector_To_SWPastTheWorthlessKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWFrontBackConnector
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWPastTheWorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWFrontBackConnector
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SWWorthlessKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWPastTheWorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBackEntry_To_SWBack =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SWBackEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBack,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBackFirstKeyDoor_To_SWBack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBackFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBackFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SWBackFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBack,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBack_To_SWBackFirstKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBackPastFirstKeyDoor_To_SWBackFirstKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBackPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBack_To_SWBackPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBack
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SWBackFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBackPastFirstKeyDoor_To_SWBackPastFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBackPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastFourTorchRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBackPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastFourTorchRoom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBackPastFourTorchRoom_To_SWBackPastCurtains =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBackPastFourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastCurtains,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBackPastFourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastCurtains,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBackPastFourTorchRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastCurtains,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBossRoom_To_SWBackPastCurtains =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastCurtains,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBossRoom
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SWBackSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackPastCurtains,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBackPastCurtains_To_SWBackSecondKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBackPastCurtains
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBossRoom_To_SWBackSecondKeyDoor =>
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
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBackSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SWBackPastCurtains_To_SWBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBackPastCurtains
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SWBackPastCurtains
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SWBackSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SkullWoods,
                    DungeonNodeID.SWBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
