using System.Collections.Generic;
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
using Xunit;

namespace OpenTracker.UnitTests.DungeonNodes
{
    [Collection("Tests")]
    public class HCTests
    {
        [Theory]
        [MemberData(nameof(HCSanctuaryEntry_To_HCSanctuary))]
        [MemberData(nameof(HCBack_To_HCSanctuary))]
        [MemberData(nameof(HCFrontEntry_To_HCFront))]
        [MemberData(nameof(HCPastEscapeFirstKeyDoor_To_HCFront))]
        [MemberData(nameof(HCDarkRoomFront_To_HCFront))]
        [MemberData(nameof(HCFront_To_HCEscapeFirstKeyDoor))]
        [MemberData(nameof(HCPastEscapeFirstKeyDoor_To_HCEscapeFirstKeyDoor))]
        [MemberData(nameof(HCFront_To_HCPastEscapeFirstKeyDoor))]
        [MemberData(nameof(HCPastEscapeSecondKeyDoor_To_HCPastEscapeFirstKeyDoor))]
        [MemberData(nameof(HCPastEscapeFirstKeyDoor_To_HCEscapeSecondKeyDoor))]
        [MemberData(nameof(HCPastEscapeSecondKeyDoor_To_HCEscapeSecondKeyDoor))]
        [MemberData(nameof(HCPastEscapeFirstKeyDoor_To_HCPastEscapeSecondKeyDoor))]
        [MemberData(nameof(HCFront_To_HCDarkRoomFront))]
        [MemberData(nameof(HCPastDarkCrossKeyDoor_To_HCDarkRoomFront))]
        [MemberData(nameof(HCDarkRoomFront_To_HCDarkCrossKeyDoor))]
        [MemberData(nameof(HCPastDarkCrossKeyDoor_To_HCDarkCrossKeyDoor))]
        [MemberData(nameof(HCDarkRoomFront_To_HCPastDarkCrossKeyDoor))]
        [MemberData(nameof(HCPastSewerRatRoomKeyDoor_To_HCPastDarkCrossKeyDoor))]
        [MemberData(nameof(HCPastDarkCrossKeyDoor_To_HCSewerRatRoomKeyDoor))]
        [MemberData(nameof(HCPastSewerRatRoomKeyDoor_To_HCSewerRatRoomKeyDoor))]
        [MemberData(nameof(HCDarkRoomBack_To_HCPastSewerRatRoomKeyDoor))]
        [MemberData(nameof(HCPastDarkCrossKeyDoor_To_HCPastSewerRatRoomKeyDoor))]
        [MemberData(nameof(HCPastSewerRatRoomKeyDoor_To_HCDarkRoomBack))]
        [MemberData(nameof(HCBack_To_HCDarkRoomBack))]
        [MemberData(nameof(HCBackEntry_To_HCBack))]
        [MemberData(nameof(HCDarkRoomBack_To_HCBack))]
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

        public static IEnumerable<object[]> HCSanctuaryEntry_To_HCSanctuary =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSanctuary,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSanctuary,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HCSanctuaryEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSanctuary,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCBack_To_HCSanctuary =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSanctuary,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSanctuary,
                    AccessibilityLevel.Normal
                },
            };

        public static IEnumerable<object[]> HCFrontEntry_To_HCFront =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
                    AccessibilityLevel.Normal
                },
                 new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HCFrontEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
                    AccessibilityLevel.Normal
                }
           };

        public static IEnumerable<object[]> HCPastEscapeFirstKeyDoor_To_HCFront =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
                    AccessibilityLevel.None
                },
                 new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.HCEscapeFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCDarkRoomFront_To_HCFront =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCDarkRoomFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCFront,
                    AccessibilityLevel.Normal
                }
            };
        
        public static IEnumerable<object[]> HCFront_To_HCEscapeFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeFirstKeyDoor,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeFirstKeyDoor,
                    AccessibilityLevel.Normal
                },
            };

        public static IEnumerable<object[]> HCPastEscapeFirstKeyDoor_To_HCEscapeFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCFront_To_HCPastEscapeFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.HCEscapeFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCPastEscapeSecondKeyDoor_To_HCPastEscapeFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastEscapeSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastEscapeSecondKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.HCEscapeSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCPastEscapeFirstKeyDoor_To_HCEscapeSecondKeyDoor =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCPastEscapeSecondKeyDoor_To_HCEscapeSecondKeyDoor =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastEscapeSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCEscapeSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCPastEscapeFirstKeyDoor_To_HCPastEscapeSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastEscapeFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.HCEscapeSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastEscapeSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCFront_To_HCDarkRoomFront =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCPastDarkCrossKeyDoor_To_HCDarkRoomFront =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.HCDarkCrossKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomFront,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCDarkRoomFront_To_HCDarkCrossKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkCrossKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCDarkRoomFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkCrossKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCPastDarkCrossKeyDoor_To_HCDarkCrossKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkCrossKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkCrossKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCDarkRoomFront_To_HCPastDarkCrossKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCDarkRoomFront
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCDarkRoomFront
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.HCDarkCrossKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCPastSewerRatRoomKeyDoor_To_HCPastDarkCrossKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastSewerRatRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastSewerRatRoomKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastDarkCrossKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCPastDarkCrossKeyDoor_To_HCSewerRatRoomKeyDoor =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSewerRatRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSewerRatRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCPastSewerRatRoomKeyDoor_To_HCSewerRatRoomKeyDoor =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSewerRatRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastSewerRatRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCSewerRatRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCDarkRoomBack_To_HCPastSewerRatRoomKeyDoor =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCDarkRoomBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCPastDarkCrossKeyDoor_To_HCPastSewerRatRoomKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastDarkCrossKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.HCSewerRatRoomKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCPastSewerRatRoomKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCPastSewerRatRoomKeyDoor_To_HCDarkRoomBack =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCPastSewerRatRoomKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCBack_To_HCDarkRoomBack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, false)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomHC, true)
                    },
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCDarkRoomBack,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCBackEntry_To_HCBack =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HCBackEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCBack,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HCDarkRoomBack_To_HCBack =>
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
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.HCDarkRoomBack
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.HyruleCastle,
                    DungeonNodeID.HCBack,
                    AccessibilityLevel.Normal
                }
             };
    }
}