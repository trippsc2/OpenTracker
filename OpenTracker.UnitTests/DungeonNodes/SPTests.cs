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
    public class SPTests
    {
        [Theory]
        [MemberData(nameof(SPEntry_To_SP))]
        [MemberData(nameof(SP_To_SPAfterRiver))]
        [MemberData(nameof(SPAfterRiver_To_SPB1))]
        [MemberData(nameof(SPB1PastFirstRightKeyDoor_To_SPB1))]
        [MemberData(nameof(SPB1_To_SPB1FirstRightKeyDoor))]
        [MemberData(nameof(SPB1PastFirstRightKeyDoor_To_SPB1FirstRightKeyDoor))]
        [MemberData(nameof(SPB1_To_SPB1PastFirstRightKeyDoor))]
        [MemberData(nameof(SPB1PastSecondRightKeyDoor_To_SPB1PastFirstRightKeyDoor))]
        [MemberData(nameof(SPB1PastFirstRightKeyDoor_To_SPB1SecondRightKeyDoor))]
        [MemberData(nameof(SPB1PastSecondRightKeyDoor_To_SPB1SecondRightKeyDoor))]
        [MemberData(nameof(SPB1PastFirstRightKeyDoor_To_SPB1PastSecondRightKeyDoor))]
        [MemberData(nameof(SPB1PastSecondRightKeyDoor_To_SPB1PastRightHammerBlocks))]
        [MemberData(nameof(SPB1PastLeftKeyDoor_To_SPB1PastRightHammerBlocks))]
        [MemberData(nameof(SPB1PastRightHammerBlocks_To_SPB1KeyLedge))]
        [MemberData(nameof(SPB1PastRightHammerBlocks_To_SPB1LeftKeyDoor))]
        [MemberData(nameof(SPB1PastLeftKeyDoor_To_SPB1LeftKeyDoor))]
        [MemberData(nameof(SPB1PastRightHammerBlocks_To_SPB1PastLeftKeyDoor))]
        [MemberData(nameof(SPB1PastRightHammerBlocks_To_SPBigChest))]
        [MemberData(nameof(SPB1PastRightHammerBlocks_To_SPB1Back))]
        [MemberData(nameof(SPB1PastBackFirstKeyDoor_To_SPB1Back))]
        [MemberData(nameof(SPB1Back_To_SPB1BackFirstKeyDoor))]
        [MemberData(nameof(SPB1PastBackFirstKeyDoor_To_SPB1BackFirstKeyDoor))]
        [MemberData(nameof(SPB1Back_To_SPB1PastBackFirstKeyDoor))]
        [MemberData(nameof(SPBossRoom_To_SPB1PastBackFirstKeyDoor))]
        [MemberData(nameof(SPB1PastBackFirstKeyDoor_To_SPBossRoomKeyDoor))]
        [MemberData(nameof(SPBossRoom_To_SPBossRoomKeyDoor))]
        [MemberData(nameof(SPB1PastBackFirstKeyDoor_To_SPBossRoom))]
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

        public static IEnumerable<object[]> SPEntry_To_SP =>
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
                    LocationID.SwampPalace,
                    DungeonNodeID.SP,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SPEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SP,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SP_To_SPAfterRiver =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPAfterRiver,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPAfterRiver,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPAfterRiver_To_SPB1 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPAfterRiver
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPAfterRiver
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SP1FKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastFirstRightKeyDoor_To_SPB1 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastFirstRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastFirstRightKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1FirstRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1_To_SPB1FirstRightKeyDoor =>
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
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1FirstRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1FirstRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastFirstRightKeyDoor_To_SPB1FirstRightKeyDoor =>
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
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1FirstRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastFirstRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1FirstRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1_To_SPB1PastFirstRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1FirstRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastSecondRightKeyDoor_To_SPB1PastFirstRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastSecondRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastSecondRightKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastFirstRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastFirstRightKeyDoor_To_SPB1SecondRightKeyDoor =>
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
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1SecondRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastFirstRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1SecondRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastSecondRightKeyDoor_To_SPB1SecondRightKeyDoor =>
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
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1SecondRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastSecondRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1SecondRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastFirstRightKeyDoor_To_SPB1PastSecondRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastFirstRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastSecondRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastFirstRightKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1SecondRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastSecondRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastSecondRightKeyDoor_To_SPB1PastRightHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastSecondRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastSecondRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastLeftKeyDoor_To_SPB1PastRightHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastLeftKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastLeftKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1LeftKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastRightHammerBlocks,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastRightHammerBlocks_To_SPB1KeyLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastRightHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1KeyLedge,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastRightHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1KeyLedge,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastRightHammerBlocks_To_SPB1LeftKeyDoor =>
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
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1LeftKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastRightHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1LeftKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastLeftKeyDoor_To_SPB1LeftKeyDoor =>
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
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1LeftKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastLeftKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1LeftKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastRightHammerBlocks_To_SPB1PastLeftKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastRightHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastLeftKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastRightHammerBlocks
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1LeftKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastLeftKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastRightHammerBlocks_To_SPBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastRightHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastRightHammerBlocks
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPBigChest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastRightHammerBlocks_To_SPB1Back =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastRightHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1Back,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastRightHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1Back,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastBackFirstKeyDoor_To_SPB1Back =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastBackFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1Back,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastBackFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1BackFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1Back,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1Back_To_SPB1BackFirstKeyDoor =>
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
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1BackFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1Back
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1BackFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastBackFirstKeyDoor_To_SPB1BackFirstKeyDoor =>
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
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1BackFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastBackFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1BackFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1Back_To_SPB1PastBackFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1Back
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1Back
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPB1BackFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPBossRoom_To_SPB1PastBackFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPBossRoom
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPBossRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPB1PastBackFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastBackFirstKeyDoor_To_SPBossRoomKeyDoor =>
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
                    LocationID.SwampPalace,
                    DungeonNodeID.SPBossRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastBackFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPBossRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPBossRoom_To_SPBossRoomKeyDoor =>
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
                    LocationID.SwampPalace,
                    DungeonNodeID.SPBossRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPBossRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SPB1PastBackFirstKeyDoor_To_SPBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastBackFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.SPB1PastBackFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.SPBossRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.SwampPalace,
                    DungeonNodeID.SPBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
