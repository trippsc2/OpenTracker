using System.Collections.Generic;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.DungeonNodes
{
    public class TTTests : DungeonNodeTestBase
    {
        [Theory]
        [MemberData(nameof(TTEntryToTT))]
        [MemberData(nameof(TTPastBigKeyDoorToTT))]
        [MemberData(nameof(TTToTTBigKeyDoor))]
        [MemberData(nameof(TTPastBigKeyDoorToTTBigKeyDoor))]
        [MemberData(nameof(TTToTTPastBigKeyDoor))]
        [MemberData(nameof(TTPastFirstKeyDoorToTTPastBigKeyDoor))]
        [MemberData(nameof(TTPastBigKeyDoorToTTFirstKeyDoor))]
        [MemberData(nameof(TTPastFirstKeyDoorToTTFirstKeyDoor))]
        [MemberData(nameof(TTPastBigKeyDoorToTTPastFirstKeyDoor))]
        [MemberData(nameof(TTPastBigChestRoomKeyDoorToTTPastFirstKeyDoor))]
        [MemberData(nameof(TTPastFirstKeyDoorToTTPastSecondKeyDoor))]
        [MemberData(nameof(TTPastFirstKeyDoorToTTBigChestKeyDoor))]
        [MemberData(nameof(TTPastBigChestRoomKeyDoorToTTBigChestKeyDoor))]
        [MemberData(nameof(TTPastFirstKeyDoorToTTPastBigChestRoomKeyDoor))]
        [MemberData(nameof(TTPastBigChestRoomKeyDoorToTTPastHammerBlocks))]
        [MemberData(nameof(TTPastHammerBlocksToTTBigChest))]
        [MemberData(nameof(TTPastBigKeyDoorToTTBossRoom))]
        [MemberData(nameof(TTPastSecondKeyDoorToTTBossRoom))]
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
        
        public static IEnumerable<object[]> TTEntryToTT =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TTPastBigKeyDoorToTT =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TTPastBigKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TTToTTBigKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TTPastBigKeyDoorToTTBigKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TTToTTPastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TT
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TTPastFirstKeyDoorToTTPastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TTPastFirstKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TTPastBigKeyDoorToTTFirstKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TTPastFirstKeyDoorToTTFirstKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TTPastBigKeyDoorToTTPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TTPastBigKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TTPastBigChestRoomKeyDoorToTTPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TTPastBigChestRoomKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TTPastFirstKeyDoorToTTPastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TTPastFirstKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TTPastFirstKeyDoorToTTBigChestKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TTPastBigChestRoomKeyDoorToTTBigChestKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> TTPastFirstKeyDoorToTTPastBigChestRoomKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TTPastFirstKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TTPastBigChestRoomKeyDoorToTTPastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> TTPastHammerBlocksToTTBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.TTPastHammerBlocks
                    },
                    new[]
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
        
        public static IEnumerable<object[]> TTPastBigKeyDoorToTTBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        BossShuffle = false
                    },
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> TTPastSecondKeyDoorToTTBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        BossShuffle = true
                    },
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
