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
        public void Tests(
            ModeSaveData mode, RequirementNodeID[] accessibleEntryNodes,
            DungeonNodeID[] accessibleNodes, KeyDoorID[] unlockedDoors, (ItemType, int)[] items,
            (SequenceBreakType, bool)[] sequenceBreaks, LocationID dungeonID, DungeonNodeID id,
            AccessibilityLevel expected)
        {
            RequirementNodeDictionary.Instance.Reset();
            ((IDungeon)LocationDictionary.Instance[dungeonID]).DungeonDataQueue.TryPeek(
                out var dungeon);
            dungeon.Reset();
            ItemDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();
            Mode.Instance.Load(mode);

            foreach (var node in accessibleEntryNodes)
            {
                RequirementNodeDictionary.Instance[node].AlwaysAccessible = true;
            }

            foreach (var node in accessibleNodes)
            {
                dungeon.Nodes[node].AlwaysAccessible = true;
            }

            foreach (var door in unlockedDoors)
            {
                dungeon.KeyDoorDictionary[door].Unlocked = true;
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

            Assert.Equal(expected, dungeon.Nodes[id].Accessibility);
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
    }
}