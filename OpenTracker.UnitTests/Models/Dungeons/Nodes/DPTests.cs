using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes
{
    public class DPTests : DungeonNodeTestBase
    {
        [Theory]
        [MemberData(nameof(DPFrontEntryToDPFront))]
        [MemberData(nameof(DPLeftEntryToDPFront))]
        [MemberData(nameof(DPPastRightKeyDoorToDPFront))]
        [MemberData(nameof(DPFrontToDPTorch))]
        [MemberData(nameof(DPFrontToDPBigChest))]
        [MemberData(nameof(DPFrontToDPRightKeyDoor))]
        [MemberData(nameof(DPPastRightKeyDoorToDPRightKeyDoor))]
        [MemberData(nameof(DPFrontToDPPastRightKeyDoor))]
        [MemberData(nameof(DPBackEntryToDPBack))]
        [MemberData(nameof(DPBackToDP2F))]
        [MemberData(nameof(DP2FPastFirstKeyDoorToDP2F))]
        [MemberData(nameof(DP2FToDP2FFirstKeyDoor))]
        [MemberData(nameof(DP2FPastFirstKeyDoorToDP2FFirstKeyDoor))]
        [MemberData(nameof(DP2FToDP2FPastFirstKeyDoor))]
        [MemberData(nameof(DP2FPastSecondKeyDoorToDP2FPastFirstKeyDoor))]
        [MemberData(nameof(DP2FToDP2FSecondKeyDoor))]
        [MemberData(nameof(DP2FPastFirstKeyDoorToDP2FSecondKeyDoor))]
        [MemberData(nameof(DP2FPastFirstKeyDoorToDP2FPastSecondKeyDoor))]
        [MemberData(nameof(DP2FPastSecondKeyDoorToDPPastFourTorchWall))]
        [MemberData(nameof(DPPastFourTorchWallToDPBossRoom))]
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
        
        public static IEnumerable<object[]> DPFrontEntryToDPFront =>
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
                    LocationID.DesertPalace,
                    DungeonNodeID.DPFront,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.DPFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPFront,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DPLeftEntryToDPFront =>
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
                    LocationID.DesertPalace,
                    DungeonNodeID.DPFront,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.DPLeftEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPFront,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DPPastRightKeyDoorToDPFront =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPPastRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPFront,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPPastRightKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.DPRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPFront,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DPFrontToDPTorch =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPTorch,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPTorch,
                    AccessibilityLevel.Inspect
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPTorch,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DPFrontToDPBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPFront
                    },
                    new[]
                    {
                        KeyDoorID.DPBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPBigChest,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DPFrontToDPRightKeyDoor =>
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
                    LocationID.DesertPalace,
                    DungeonNodeID.DPRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DPPastRightKeyDoorToDPRightKeyDoor =>
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
                    LocationID.DesertPalace,
                    DungeonNodeID.DPRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPPastRightKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DPFrontToDPPastRightKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPPastRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPFront
                    },
                    new[]
                    {
                        KeyDoorID.DPRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPPastRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DPBackEntryToDPBack =>
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
                    LocationID.DesertPalace,
                    DungeonNodeID.DPBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.DPBackEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPBack,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DPBackToDP2F =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2F,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPBack
                    },
                    new[]
                    {
                        KeyDoorID.DP1FKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2F,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DP2FPastFirstKeyDoorToDP2F =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2F,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastFirstKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.DP2FFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2F,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DP2FToDP2FFirstKeyDoor =>
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
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2F
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DP2FPastFirstKeyDoorToDP2FFirstKeyDoor =>
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
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DP2FToDP2FPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2F
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FPastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2F
                    },
                    new[]
                    {
                        KeyDoorID.DP2FFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FPastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DP2FPastSecondKeyDoorToDP2FPastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FPastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastSecondKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.DP2FSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FPastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DP2FToDP2FSecondKeyDoor =>
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
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DP2FPastFirstKeyDoorToDP2FSecondKeyDoor =>
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
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DP2FPastFirstKeyDoorToDP2FPastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FPastSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastFirstKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.DP2FSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DP2FPastSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DP2FPastSecondKeyDoorToDPPastFourTorchWall =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPPastFourTorchWall,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPPastFourTorchWall,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DP2FPastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPPastFourTorchWall,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> DPPastFourTorchWallToDPBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPPastFourTorchWall
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.DPPastFourTorchWall
                    },
                    new[]
                    {
                        KeyDoorID.DPBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.DesertPalace,
                    DungeonNodeID.DPBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
