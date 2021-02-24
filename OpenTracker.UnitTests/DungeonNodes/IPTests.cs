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
    public class IPTests
    {
        [Theory]
        [MemberData(nameof(IPEntry_To_IP))]
        [MemberData(nameof(IP_To_IPPastEntranceFreezorRoom))]
        [MemberData(nameof(IPPastEntranceFreezorRoom_To_IPB1LeftSide))]
        [MemberData(nameof(IPB2PastLiftBlock_To_IPB1RightSide))]
        [MemberData(nameof(IPB1LeftSide_To_IPB1RightSide))]
        [MemberData(nameof(IPB1LeftSide_To_IPB2LeftSide))]
        [MemberData(nameof(IPB2PastKeyDoor_To_IPB2LeftSide))]
        [MemberData(nameof(IPB2LeftSide_To_IPB2KeyDoor))]
        [MemberData(nameof(IPB2PastKeyDoor_To_IPB2KeyDoor))]
        [MemberData(nameof(IPB2LeftSide_To_IPB2PastKeyDoor))]
        [MemberData(nameof(IPSpikeRoom_To_IPB2PastKeyDoor))]
        [MemberData(nameof(IPB4FreezorRoom_To_IPB2PastKeyDoor))]
        [MemberData(nameof(IPB2PastLiftBlock_To_IPB2PastHammerBlocks))]
        [MemberData(nameof(IPSpikeRoom_To_IPB2PastHammerBlocks))]
        [MemberData(nameof(IPB2PastHammerBlocks_To_IPB2PastLiftBlock))]
        [MemberData(nameof(IPB2PastKeyDoor_To_IPB3KeyDoor))]
        [MemberData(nameof(IPSpikeRoom_To_IPB3KeyDoor))]
        [MemberData(nameof(IPB1RightSide_To_IPSpikeRoom))]
        [MemberData(nameof(IPB2PastHammerBlocks_To_IPSpikeRoom))]
        [MemberData(nameof(IPB2PastKeyDoor_To_IPSpikeRoom))]
        [MemberData(nameof(IPB4RightSide_To_IPSpikeRoom))]
        [MemberData(nameof(IPSpikeRoom_To_IPB4RightSide))]
        [MemberData(nameof(IPB4IceRoom_To_IPB4RightSide))]
        [MemberData(nameof(IPB2PastKeyDoor_To_IPB4IceRoom))]
        [MemberData(nameof(IPB4PastKeyDoor_To_IPB4IceRoom))]
        [MemberData(nameof(IPB2PastKeyDoor_To_IPB4FreezorRoom))]
        [MemberData(nameof(IPB4FreezorRoom_To_IPFreezorChest))]
        [MemberData(nameof(IPB4PastKeyDoor_To_IPB4KeyDoor))]
        [MemberData(nameof(IPB4IceRoom_To_IPB4KeyDoor))]
        [MemberData(nameof(IPB4IceRoom_To_IPB4PastKeyDoor))]
        [MemberData(nameof(IPB5_To_IPB4PastKeyDoor))]
        [MemberData(nameof(IPB4FreezorRoom_To_IPBigChestArea))]
        [MemberData(nameof(IPBigChestArea_To_IPBigChest))]
        [MemberData(nameof(IPB4FreezorRoom_To_IPB5))]
        [MemberData(nameof(IPB4PastKeyDoor_To_IPB5))]
        [MemberData(nameof(IPB5_To_IPB5PastBigKeyDoor))]
        [MemberData(nameof(IPB5PastBigKeyDoor_To_IPB6))]
        [MemberData(nameof(IPB6PastKeyDoor_To_IPB6))]
        [MemberData(nameof(IPB5_To_IPB6))]
        [MemberData(nameof(IPB6_To_IPB6KeyDoor))]
        [MemberData(nameof(IPB6PastKeyDoor_To_IPB6KeyDoor))]
        [MemberData(nameof(IPB6_To_IPB6PastKeyDoor))]
        [MemberData(nameof(IPB6_To_IPB6PreBossRoom))]
        [MemberData(nameof(IPB6PastKeyDoor_To_IPB6PreBossRoom))]
        [MemberData(nameof(IPB6PreBossRoom_To_IPB6PastHammerBlocks))]
        [MemberData(nameof(IPB6PastHammerBlocks_To_IPB6PastLiftBlock))]
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

        public static IEnumerable<object[]> IPEntry_To_IP =>
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
                    new RequirementNodeID[]
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

        public static IEnumerable<object[]> IP_To_IPPastEntranceFreezorRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IP
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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

        public static IEnumerable<object[]> IPPastEntranceFreezorRoom_To_IPB1LeftSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPPastEntranceFreezorRoom
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> IPB2PastLiftBlock_To_IPB1RightSide =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB1LeftSide_To_IPB1RightSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB1LeftSide_To_IPB2LeftSide =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB2PastKeyDoor_To_IPB2LeftSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> IPB2LeftSide_To_IPB2KeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB2PastKeyDoor_To_IPB2KeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB2LeftSide_To_IPB2PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB2LeftSide
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> IPSpikeRoom_To_IPB2PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPSpikeRoom
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> IPB4FreezorRoom_To_IPB2PastKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB2PastLiftBlock_To_IPB2PastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB2PastLiftBlock
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB2PastLiftBlock
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPB2PastHammerBlocks,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> IPSpikeRoom_To_IPB2PastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB2PastHammerBlocks_To_IPB2PastLiftBlock =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB2PastKeyDoor_To_IPB3KeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPSpikeRoom_To_IPB3KeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB1RightSide_To_IPSpikeRoom =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB2PastHammerBlocks_To_IPSpikeRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB2PastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPSpikeRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB2PastHammerBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.IcePalace,
                    DungeonNodeID.IPSpikeRoom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> IPB2PastKeyDoor_To_IPSpikeRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB2PastKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> IPB4RightSide_To_IPSpikeRoom =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPSpikeRoom_To_IPB4RightSide =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB4IceRoom_To_IPB4RightSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.Hover, true),
                        (SequenceBreakType.BombJumpIPHookshotGap, true)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4RightSide,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> IPB2PastKeyDoor_To_IPB4IceRoom =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB4PastKeyDoor_To_IPB4IceRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB4PastKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> IPB2PastKeyDoor_To_IPB4FreezorRoom =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB4FreezorRoom_To_IPFreezorChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB4FreezorRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
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

        public static IEnumerable<object[]> IPB4PastKeyDoor_To_IPB4KeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB4IceRoom_To_IPB4KeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB4IceRoom_To_IPB4PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB4IceRoom
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.IPB4KeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpIPHookshotGap, false),
                        (SequenceBreakType.BombJumpIPFreezorRoomGap, false)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB4PastKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> IPB5_To_IPB4PastKeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB4FreezorRoom_To_IPBigChestArea =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPBigChestArea_To_IPBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPBigChestArea
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> IPB4FreezorRoom_To_IPB5 =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB4PastKeyDoor_To_IPB5 =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB5_To_IPB5PastBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB5
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> IPB5PastBigKeyDoor_To_IPB6 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB5PastBigKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> IPB6PastKeyDoor_To_IPB6 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB6PastKeyDoor
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> IPB5_To_IPB6 =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB5
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB5
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB5
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombJumpIPBJ, false),
                        (SequenceBreakType.IPIceBreaker, true)
                    },
                    LocationID.IcePalace,
                    DungeonNodeID.IPB6,
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> IPB6_To_IPB6KeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB6PastKeyDoor_To_IPB6KeyDoor =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB6_To_IPB6PastKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.IPB6
                    },
                    new KeyDoorID[]
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

        public static IEnumerable<object[]> IPB6_To_IPB6PreBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB6PastKeyDoor_To_IPB6PreBossRoom =>
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB6PreBossRoom_To_IPB6PastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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

        public static IEnumerable<object[]> IPB6PastHammerBlocks_To_IPB6PastLiftBlock =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
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
                    new DungeonNodeID[]
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
