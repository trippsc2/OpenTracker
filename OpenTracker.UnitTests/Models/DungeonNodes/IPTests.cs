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
    public class IPTests : DungeonNodeTestBase
    {
        [Theory]
        [MemberData(nameof(IPEntryToIP))]
        [MemberData(nameof(IPToIPPastEntranceFreezorRoom))]
        [MemberData(nameof(IPPastEntranceFreezorRoomToIPB1LeftSide))]
        [MemberData(nameof(IPB2PastLiftBlockToIPB1RightSide))]
        [MemberData(nameof(IPB1LeftSideToIPB1RightSide))]
        [MemberData(nameof(IPB1LeftSideToIPB2LeftSide))]
        [MemberData(nameof(IPB2PastKeyDoorToIPB2LeftSide))]
        [MemberData(nameof(IPB2LeftSideToIPB2KeyDoor))]
        [MemberData(nameof(IPB2PastKeyDoorToIPB2KeyDoor))]
        [MemberData(nameof(IPB2LeftSideToIPB2PastKeyDoor))]
        [MemberData(nameof(IPSpikeRoomToIPB2PastKeyDoor))]
        [MemberData(nameof(IPB4FreezorRoomToIPB2PastKeyDoor))]
        [MemberData(nameof(IPSpikeRoomToIPB2PastHammerBlocks))]
        [MemberData(nameof(IPB2PastHammerBlocksToIPB2PastLiftBlock))]
        [MemberData(nameof(IPB2PastKeyDoorToIPB3KeyDoor))]
        [MemberData(nameof(IPSpikeRoomToIPB3KeyDoor))]
        [MemberData(nameof(IPB1RightSideToIPSpikeRoom))]
        [MemberData(nameof(IPB2PastKeyDoorToIPSpikeRoom))]
        [MemberData(nameof(IPB4RightSideToIPSpikeRoom))]
        [MemberData(nameof(IPSpikeRoomToIPB4RightSide))]
        [MemberData(nameof(IPB4IceRoomToIPB4RightSide))]
        [MemberData(nameof(IPB2PastKeyDoorToIPB4IceRoom))]
        [MemberData(nameof(IPB4PastKeyDoorToIPB4IceRoom))]
        [MemberData(nameof(IPB2PastKeyDoorToIPB4FreezorRoom))]
        [MemberData(nameof(IPB4FreezorRoomToIPFreezorChest))]
        [MemberData(nameof(IPB4PastKeyDoorToIPB4KeyDoor))]
        [MemberData(nameof(IPB4IceRoomToIPB4KeyDoor))]
        [MemberData(nameof(IPB4IceRoomToIPB4PastKeyDoor))]
        [MemberData(nameof(IPB5ToIPB4PastKeyDoor))]
        [MemberData(nameof(IPB4FreezorRoomToIPBigChestArea))]
        [MemberData(nameof(IPBigChestAreaToIPBigChest))]
        [MemberData(nameof(IPB4FreezorRoomToIPB5))]
        [MemberData(nameof(IPB4PastKeyDoorToIPB5))]
        [MemberData(nameof(IPB5ToIPB5PastBigKeyDoor))]
        [MemberData(nameof(IPB5PastBigKeyDoorToIPB6))]
        [MemberData(nameof(IPB6PastKeyDoorToIPB6))]
        [MemberData(nameof(IPB5ToIPB6))]
        [MemberData(nameof(IPB6ToIPB6KeyDoor))]
        [MemberData(nameof(IPB6PastKeyDoorToIPB6KeyDoor))]
        [MemberData(nameof(IPB6ToIPB6PastKeyDoor))]
        [MemberData(nameof(IPB6ToIPB6PreBossRoom))]
        [MemberData(nameof(IPB6PastKeyDoorToIPB6PreBossRoom))]
        [MemberData(nameof(IPB6PreBossRoomToIPB6PastHammerBlocks))]
        [MemberData(nameof(IPB6PastHammerBlocksToIPB6PastLiftBlock))]
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
        
        public static IEnumerable<object[]> IPEntryToIP =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IP,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        RequirementNodeID.IPEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IP,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPToIPPastEntranceFreezorRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IP
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPPastEntranceFreezorRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IP
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPPastEntranceFreezorRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IP
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPPastEntranceFreezorRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IP
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPPastEntranceFreezorRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPPastEntranceFreezorRoomToIPB1LeftSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPPastEntranceFreezorRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB1LeftSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPPastEntranceFreezorRoom
                    },
                    new[]
                    {
                        KeyDoorID.IP1FKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB1LeftSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB2PastLiftBlockToIPB1RightSide =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB1RightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2PastLiftBlock
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB1RightSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB1LeftSideToIPB1RightSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB1LeftSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.IPIceBreaker, false)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB1RightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB1LeftSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.IPIceBreaker, true)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB1RightSide,
                    AccessibilityLevel.SequenceBreak
                }
            };
        
        public static IEnumerable<object[]> IPB1LeftSideToIPB2LeftSide =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2LeftSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB1LeftSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2LeftSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB2PastKeyDoorToIPB2LeftSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2LeftSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.IPB2KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2LeftSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB2LeftSideToIPB2KeyDoor =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2LeftSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB2PastKeyDoorToIPB2KeyDoor =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB2LeftSideToIPB2PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2LeftSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2PastKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2LeftSide
                    },
                    new[]
                    {
                        KeyDoorID.IPB2KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2PastKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPSpikeRoomToIPB2PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPSpikeRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2PastKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPSpikeRoom
                    },
                    new[]
                    {
                        KeyDoorID.IPB3KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2PastKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB4FreezorRoomToIPB2PastKeyDoor =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2PastKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2PastKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPSpikeRoomToIPB2PastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPSpikeRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2PastHammerBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPSpikeRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2PastHammerBlocks,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB2PastHammerBlocksToIPB2PastLiftBlock =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2PastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2PastLiftBlock,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2PastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2PastLiftBlock,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB2PastKeyDoorToIPB3KeyDoor =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB3KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB3KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPSpikeRoomToIPB3KeyDoor =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB3KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPSpikeRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB3KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB1RightSideToIPSpikeRoom =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPSpikeRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB1RightSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPSpikeRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB2PastKeyDoorToIPSpikeRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpIPHookshotGap, false)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPSpikeRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.IPB3KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpIPHookshotGap, false)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPSpikeRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB4RightSideToIPSpikeRoom =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPSpikeRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4RightSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPSpikeRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPSpikeRoomToIPB4RightSide =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4RightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPSpikeRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4RightSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB4IceRoomToIPB4RightSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.Hover, false),
                        (SequenceBreakType.BombJumpIPHookshotGap, false)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4RightSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.Hover, false),
                        (SequenceBreakType.BombJumpIPHookshotGap, true)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4RightSide,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.Hover, true),
                        (SequenceBreakType.BombJumpIPHookshotGap, false)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4RightSide,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.Hover, true),
                        (SequenceBreakType.BombJumpIPHookshotGap, true)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4RightSide,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB2PastKeyDoorToIPB4IceRoom =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4IceRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4IceRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB4PastKeyDoorToIPB4IceRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4IceRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4PastKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.IPB4KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4IceRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB2PastKeyDoorToIPB4FreezorRoom =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4FreezorRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4FreezorRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB4FreezorRoomToIPFreezorChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPFreezorChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPFreezorChest,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPFreezorChest,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    },
                    new KeyDoorID[0],
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPFreezorChest,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB4PastKeyDoorToIPB4KeyDoor =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB4IceRoomToIPB4KeyDoor =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB4IceRoomToIPB4PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.BombJumpIPHookshotGap, false),
                        (SequenceBreakType.BombJumpIPFreezorRoomGap, false)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4PastKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new[]
                    {
                        KeyDoorID.IPB4KeyDoor
                    },
                    new (ItemType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.BombJumpIPHookshotGap, false),
                        (SequenceBreakType.BombJumpIPFreezorRoomGap, false)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4PastKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB5ToIPB4PastKeyDoor =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4PastKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB5
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4PastKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB4FreezorRoomToIPBigChestArea =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPBigChestArea,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPBigChestArea,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPBigChestAreaToIPBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPBigChestArea
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPBigChestArea
                    },
                    new[]
                    {
                        KeyDoorID.IPBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPBigChest,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB4FreezorRoomToIPB5 =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB5,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB5,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB4PastKeyDoorToIPB5 =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB5,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB4PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB5,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB5ToIPB5PastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB5
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB5PastBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB5
                    },
                    new[]
                    {
                        KeyDoorID.IPBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB5PastBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB5PastBigKeyDoorToIPB6 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB5PastBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB5PastBigKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.IPB5KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB6PastKeyDoorToIPB6 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6PastKeyDoor
                    },
                    new[]
                    {
                        KeyDoorID.IPB6KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB5ToIPB6 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB5
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.BombJumpIPBJ, false),
                        (SequenceBreakType.IPIceBreaker, false)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB5
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new[]
                    {
                        (SequenceBreakType.BombJumpIPBJ, true),
                        (SequenceBreakType.IPIceBreaker, false)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB5
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new[]
                    {
                        (SequenceBreakType.BombJumpIPBJ, false),
                        (SequenceBreakType.IPIceBreaker, true)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6,
                    AccessibilityLevel.SequenceBreak
                }
            };
        
        public static IEnumerable<object[]> IPB6ToIPB6KeyDoor =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB6PastKeyDoorToIPB6KeyDoor =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6KeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6KeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB6ToIPB6PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6PastKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6
                    },
                    new[]
                    {
                        KeyDoorID.IPB6KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6PastKeyDoor,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB6ToIPB6PreBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpIPBJ, false)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6PreBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpIPBJ, true)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6PreBossRoom,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpIPBJ, true)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6PreBossRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB6PastKeyDoorToIPB6PreBossRoom =>
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
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6PreBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6PastKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6PreBossRoom,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB6PreBossRoomToIPB6PastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6PreBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6PastHammerBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6PreBossRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6PastHammerBlocks,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> IPB6PastHammerBlocksToIPB6PastLiftBlock =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6PastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6PastLiftBlock,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new[]
                    {
                        DungeonNodeID.IPB6PastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6PastLiftBlock,
                    AccessibilityLevel.Normal
                }
            };
    }
}
