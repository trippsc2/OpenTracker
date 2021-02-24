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
    public class MMTests
    {
        [Theory]
        [MemberData(nameof(MMEntry_To_MM))]
        [MemberData(nameof(MM_To_MMPastEntranceGap))]
        [MemberData(nameof(MMB1TopSide_To_MMPastEntranceGap))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoor_To_MMPastEntranceGap))]
        [MemberData(nameof(MMPastEntranceGap_To_MMBigChest))]
        [MemberData(nameof(MMPastEntranceGap_To_MMB1TopLeftKeyDoor))]
        [MemberData(nameof(MMB1TopSide_To_MMB1TopLeftKeyDoor))]
        [MemberData(nameof(MMPastEntranceGap_To_MMB1TopRightKeyDoor))]
        [MemberData(nameof(MMB1TopSide_To_MMB1TopRightKeyDoor))]
        [MemberData(nameof(MMPastEntranceGap_To_MMB1TopSide))]
        [MemberData(nameof(MMB1PastPortalBigKeyDoor_To_MMB1TopSide))]
        [MemberData(nameof(MMB1PastBridgeBigKeyDoor_To_MMB1TopSide))]
        [MemberData(nameof(MMB1TopSide_To_MMB1LobbyBeyondBlueBlocks))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoor_To_MMB1LobbyBeyondBlueBlocks))]
        [MemberData(nameof(MMB1RightSideBeyondBlueBlocks_To_MMB1LobbyBeyondBlueBlocks))]
        [MemberData(nameof(MMB1LobbyBeyondBlueBlocks_To_MMB1RightSideKeyDoor))]
        [MemberData(nameof(MMB1RightSideBeyondBlueBlocks_To_MMB1RightSideKeyDoor))]
        [MemberData(nameof(MMB1TopSide_To_MMB1RightSideBeyondBlueBlocks))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoor_To_MMB1RightSideBeyondBlueBlocks))]
        [MemberData(nameof(MMB1LobbyBeyondBlueBlocks_To_MMB1RightSideBeyondBlueBlocks))]
        [MemberData(nameof(MMPastEntranceGap_To_MMB1LeftSideFirstKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoor_To_MMB1LeftSideFirstKeyDoor))]
        [MemberData(nameof(MMPastEntranceGap_To_MMB1LeftSidePastFirstKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastSecondKeyDoor_To_MMB1LeftSidePastFirstKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoor_To_MMB1LeftSideSecondKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastSecondKeyDoor_To_MMB1LeftSideSecondKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastFirstKeyDoor_To_MMB1LeftSidePastSecondKeyDoor))]
        [MemberData(nameof(MMB1LeftSidePastSecondKeyDoor_To_MMB1PastFourTorchRoom))]
        [MemberData(nameof(MMB1LeftSidePastSecondKeyDoor_To_MMF1PastFourTorchRoom))]
        [MemberData(nameof(MMPastEntranceGap_To_MMB1PastPortalBigKeyDoor))]
        [MemberData(nameof(MMB1TopSide_To_MMBridgeBigKeyDoor))]
        [MemberData(nameof(MMB1PastBridgeBigKeyDoor_To_MMBridgeBigKeyDoor))]
        [MemberData(nameof(MMB1TopSide_To_MMB1PastBridgeBigKeyDoor))]
        [MemberData(nameof(MMB2PastWorthlessKeyDoor_To_MMDarkRoom))]
        [MemberData(nameof(MMB1PastBridgeBigKeyDoor_To_MMDarkRoom))]
        [MemberData(nameof(MMDarkRoom_To_MMB2WorthlessKeyDoor))]
        [MemberData(nameof(MMB2PastWorthlessKeyDoor_To_MMB2WorthlessKeyDoor))]
        [MemberData(nameof(MMDarkRoom_To_MMB2PastWorthlessKeyDoor))]
        [MemberData(nameof(MMDarkRoom_To_MMB2PastCaneOfSomariaSwitch))]
        [MemberData(nameof(MMB2PastCaneOfSomariaSwitch_To_MMBossRoom))]
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

        public static IEnumerable<object[]> MMEntry_To_MM =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MMEntry
                    },
                    new DungeonNodeID[0],
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MM,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MM_To_MMPastEntranceGap =>
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true),
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, false),
                        (SequenceBreakType.Hover, false)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true),
                        (SequenceBreakType.Hover, false)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, false),
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true),
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Boots, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true),
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
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
                        DungeonNodeID.MM
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Boots, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BonkOverLedge, true),
                        (SequenceBreakType.Hover, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1TopSide_To_MMPastEntranceGap =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1TopLeftKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1TopRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoor_To_MMPastEntranceGap =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1LeftSideFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMPastEntranceGap,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMPastEntranceGap_To_MMBigChest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBigChest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMBigChest
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBigChest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMPastEntranceGap_To_MMB1TopLeftKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopLeftKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopLeftKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1TopSide_To_MMB1TopLeftKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopLeftKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopLeftKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMPastEntranceGap_To_MMB1TopRightKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1TopSide_To_MMB1TopRightKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopRightKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopRightKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMPastEntranceGap_To_MMB1TopSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1TopLeftKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1TopRightKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1PastPortalBigKeyDoor_To_MMB1TopSide =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1PastPortalBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1PastBridgeBigKeyDoor_To_MMB1TopSide =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMBridgeBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1TopSide,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1TopSide_To_MMB1LobbyBeyondBlueBlocks =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoor_To_MMB1LobbyBeyondBlueBlocks =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1RightSideBeyondBlueBlocks_To_MMB1LobbyBeyondBlueBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1RightSideBeyondBlueBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1RightSideBeyondBlueBlocks
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1RightSideKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LobbyBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LobbyBeyondBlueBlocks_To_MMB1RightSideKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LobbyBeyondBlueBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1RightSideBeyondBlueBlocks_To_MMB1RightSideKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1RightSideBeyondBlueBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1TopSide_To_MMB1RightSideBeyondBlueBlocks =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoor_To_MMB1RightSideBeyondBlueBlocks =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LobbyBeyondBlueBlocks_To_MMB1RightSideBeyondBlueBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LobbyBeyondBlueBlocks
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LobbyBeyondBlueBlocks
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1RightSideKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1RightSideBeyondBlueBlocks,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMPastEntranceGap_To_MMB1LeftSideFirstKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoor_To_MMB1LeftSideFirstKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMPastEntranceGap_To_MMB1LeftSidePastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1LeftSideFirstKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LeftSidePastSecondKeyDoor_To_MMB1LeftSidePastFirstKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1LeftSideSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastFirstKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoor_To_MMB1LeftSideSecondKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LeftSidePastSecondKeyDoor_To_MMB1LeftSideSecondKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSideSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LeftSidePastFirstKeyDoor_To_MMB1LeftSidePastSecondKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastFirstKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB1LeftSideSecondKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1LeftSidePastSecondKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LeftSidePastSecondKeyDoor_To_MMB1PastFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastFourTorchRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastFourTorchRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastFourTorchRoom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1LeftSidePastSecondKeyDoor_To_MMF1PastFourTorchRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMF1PastFourTorchRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1),
                        (ItemType.FireRod, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMF1PastFourTorchRoom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1LeftSidePastSecondKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0),
                        (ItemType.FireRod, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMF1PastFourTorchRoom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMPastEntranceGap_To_MMB1PastPortalBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastPortalBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMPastEntranceGap
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMPortalBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastPortalBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1TopSide_To_MMBridgeBigKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBridgeBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBridgeBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1PastBridgeBigKeyDoor_To_MMBridgeBigKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBridgeBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBridgeBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1TopSide_To_MMB1PastBridgeBigKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastBridgeBigKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1TopSide
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMBridgeBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB1PastBridgeBigKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB2PastWorthlessKeyDoor_To_MMDarkRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB2PastWorthlessKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMDarkRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB2PastWorthlessKeyDoor
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB2WorthlessKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMDarkRoom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB1PastBridgeBigKeyDoor_To_MMDarkRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomMM, false)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMDarkRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomMM, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMDarkRoom,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB1PastBridgeBigKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomMM, true)
                    },
                    LocationID.MiseryMire,
                    DungeonNodeID.MMDarkRoom,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMDarkRoom_To_MMB2WorthlessKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2WorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMDarkRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2WorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB2PastWorthlessKeyDoor_To_MMB2WorthlessKeyDoor =>
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
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2WorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB2PastWorthlessKeyDoor
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2WorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMDarkRoom_To_MMB2PastWorthlessKeyDoor =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMDarkRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2PastWorthlessKeyDoor,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMDarkRoom
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMB2WorthlessKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2PastWorthlessKeyDoor,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMDarkRoom_To_MMB2PastCaneOfSomariaSwitch =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMDarkRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMDarkRoom
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MMB2PastCaneOfSomariaSwitch_To_MMBossRoom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB2PastCaneOfSomariaSwitch
                    },
                    new KeyDoorID[0],
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBossRoom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new RequirementNodeID[0],
                    new DungeonNodeID[]
                    {
                        DungeonNodeID.MMB2PastCaneOfSomariaSwitch
                    },
                    new KeyDoorID[]
                    {
                        KeyDoorID.MMBossRoomBigKeyDoor
                    },
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    LocationID.MiseryMire,
                    DungeonNodeID.MMBossRoom,
                    AccessibilityLevel.Normal
                }
            };
    }
}
