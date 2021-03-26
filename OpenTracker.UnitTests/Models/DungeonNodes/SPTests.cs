using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
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
    public class SPTests : DungeonNodeTestBase
    {
        [Theory]
        [MemberData(nameof(SPEntryToSP))]
        [MemberData(nameof(SPToSPAfterRiver))]
        [MemberData(nameof(SPAfterRiverToSPB1))]
        [MemberData(nameof(SPB1PastFirstRightKeyDoorToSPB1))]
        [MemberData(nameof(SPB1ToSPB1FirstRightKeyDoor))]
        [MemberData(nameof(SPB1PastFirstRightKeyDoorToSPB1FirstRightKeyDoor))]
        [MemberData(nameof(SPB1ToSPB1PastFirstRightKeyDoor))]
        [MemberData(nameof(SPB1PastSecondRightKeyDoorToSPB1PastFirstRightKeyDoor))]
        [MemberData(nameof(SPB1PastFirstRightKeyDoorToSPB1SecondRightKeyDoor))]
        [MemberData(nameof(SPB1PastSecondRightKeyDoorToSPB1SecondRightKeyDoor))]
        [MemberData(nameof(SPB1PastFirstRightKeyDoorToSPB1PastSecondRightKeyDoor))]
        [MemberData(nameof(SPB1PastSecondRightKeyDoorToSPB1PastRightHammerBlocks))]
        [MemberData(nameof(SPB1PastLeftKeyDoorToSPB1PastRightHammerBlocks))]
        [MemberData(nameof(SPB1PastRightHammerBlocksToSPB1KeyLedge))]
        [MemberData(nameof(SPB1PastRightHammerBlocksToSPB1LeftKeyDoor))]
        [MemberData(nameof(SPB1PastLeftKeyDoorToSPB1LeftKeyDoor))]
        [MemberData(nameof(SPB1PastRightHammerBlocksToSPB1PastLeftKeyDoor))]
        [MemberData(nameof(SPB1PastRightHammerBlocksToSPBigChest))]
        [MemberData(nameof(SPB1PastRightHammerBlocksToSPB1Back))]
        [MemberData(nameof(SPB1PastBackFirstKeyDoorToSPB1Back))]
        [MemberData(nameof(SPB1BackToSPB1BackFirstKeyDoor))]
        [MemberData(nameof(SPB1PastBackFirstKeyDoorToSPB1BackFirstKeyDoor))]
        [MemberData(nameof(SPB1BackToSPB1PastBackFirstKeyDoor))]
        [MemberData(nameof(SPBossRoomToSPB1PastBackFirstKeyDoor))]
        [MemberData(nameof(SPB1PastBackFirstKeyDoorToSPBossRoomKeyDoor))]
        [MemberData(nameof(SPBossRoomToSPBossRoomKeyDoor))]
        [MemberData(nameof(SPB1PastBackFirstKeyDoorToSPBossRoom))]
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
        
        public static IEnumerable<object[]> SPEntryToSP =>
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
                    new[]
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
        
        public static IEnumerable<object[]> SPToSPAfterRiver =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> SPAfterRiverToSPB1 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPAfterRiver
                    },
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastFirstRightKeyDoorToSPB1 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPB1PastFirstRightKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> SPB1ToSPB1FirstRightKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastFirstRightKeyDoorToSPB1FirstRightKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1ToSPB1PastFirstRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPB1
                    },
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastSecondRightKeyDoorToSPB1PastFirstRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPB1PastSecondRightKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastFirstRightKeyDoorToSPB1SecondRightKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastSecondRightKeyDoorToSPB1SecondRightKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastFirstRightKeyDoorToSPB1PastSecondRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPB1PastFirstRightKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastSecondRightKeyDoorToSPB1PastRightHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastLeftKeyDoorToSPB1PastRightHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPB1PastLeftKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastRightHammerBlocksToSPB1KeyLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastRightHammerBlocksToSPB1LeftKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastLeftKeyDoorToSPB1LeftKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastRightHammerBlocksToSPB1PastLeftKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPB1PastRightHammerBlocks
                    },
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastRightHammerBlocksToSPBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPB1PastRightHammerBlocks
                    },
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastRightHammerBlocksToSPB1Back =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastBackFirstKeyDoorToSPB1Back =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPB1PastBackFirstKeyDoor
                    },
                    new[]
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
        
        public static IEnumerable<object[]> SPB1BackToSPB1BackFirstKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastBackFirstKeyDoorToSPB1BackFirstKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1BackToSPB1PastBackFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPB1Back
                    },
                    new[]
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
        
        public static IEnumerable<object[]> SPBossRoomToSPB1PastBackFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPBossRoom
                    },
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastBackFirstKeyDoorToSPBossRoomKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> SPBossRoomToSPBossRoomKeyDoor =>
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
                    new[]
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
        
        public static IEnumerable<object[]> SPB1PastBackFirstKeyDoorToSPBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
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
                    new[]
                    {
                        DungeonNodeID.SPB1PastBackFirstKeyDoor
                    },
                    new[]
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
