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
    public class ATTests
    {
        [Theory]
        [MemberData(nameof(ATEntry_To_AT))]
        [MemberData(nameof(AT_To_ATDarkMaze))]
        [MemberData(nameof(ATDarkMaze_To_ATPastFirstKeyDoor))]
        [MemberData(nameof(ATPastSecondKeyDoor_To_ATPastFirstKeyDoor))]
        [MemberData(nameof(ATPastFirstKeyDoor_To_ATSecondKeyDoor))]
        [MemberData(nameof(ATPastSecondKeyDoor_To_ATSecondKeyDoor))]
        [MemberData(nameof(ATPastFirstKeyDoor_To_ATPastSecondKeyDoor))]
        [MemberData(nameof(ATPastSecondKeyDoor_To_ATPastThirdKeyDoor))]
        [MemberData(nameof(ATPastFourthKeyDoor_To_ATPastThirdKeyDoor))]
        [MemberData(nameof(ATPastThirdKeyDoor_To_ATFourthKeyDoor))]
        [MemberData(nameof(ATPastFourthKeyDoor_To_ATFourthKeyDoor))]
        [MemberData(nameof(ATPastThirdKeyDoor_To_ATPastFourthKeyDoor))]
        [MemberData(nameof(ATPastFourthKeyDoor_To_ATBossRoom))]
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
                dungeonData.KeyDoors[door].Unlocked = true;
            }

            Assert.Equal(expected, dungeonData.Nodes[id].Accessibility);
        }

        public static IEnumerable<object[]> ATEntry_To_AT =>
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
                    LocationID.AgahnimTower,
                    DungeonNodeID.AT,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.ATEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.AT,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> AT_To_ATDarkMaze =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.AT
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, false)
                    },
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATDarkMaze,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.AT
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATDarkMaze,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.AT
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomAT, true)
                    },
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATDarkMaze,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ATDarkMaze_To_ATPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATDarkMaze
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATDarkMaze
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.ATFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ATPastSecondKeyDoor_To_ATPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastSecondKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.ATSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ATPastFirstKeyDoor_To_ATSecondKeyDoor =>
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
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ATPastSecondKeyDoor_To_ATSecondKeyDoor =>
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
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ATPastFirstKeyDoor_To_ATPastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.ATSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ATPastSecondKeyDoor_To_ATPastThirdKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastThirdKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastSecondKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.ATThirdKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastThirdKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ATPastFourthKeyDoor_To_ATPastThirdKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastFourthKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastThirdKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastFourthKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.ATFourthKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastThirdKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ATPastThirdKeyDoor_To_ATFourthKeyDoor =>
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
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATFourthKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastThirdKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATFourthKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ATPastFourthKeyDoor_To_ATFourthKeyDoor =>
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
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATFourthKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastFourthKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATFourthKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ATPastThirdKeyDoor_To_ATPastFourthKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastThirdKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastFourthKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastThirdKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.ATFourthKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATPastFourthKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ATPastFourthKeyDoor_To_ATBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastFourthKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastFourthKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATBossRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.ATPastFourthKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.AgahnimTower,
                    DungeonNodeID.ATBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
