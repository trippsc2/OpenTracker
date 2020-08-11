using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    public class DungeonEntranceTests
    {
        [Theory]
        [MemberData(nameof(Start_To_HCSanctuaryEntry))]
        [MemberData(nameof(LightWorld_To_HCSanctuaryEntry))]
        [MemberData(nameof(Start_To_HCFrontEntry))]
        [MemberData(nameof(LightWorld_To_HCFrontEntry))]
        [MemberData(nameof(Start_To_HCBackEntry))]
        [MemberData(nameof(EscapeGrave_To_HCBackEntry))]
        [MemberData(nameof(Start_To_ATEntry))]
        [MemberData(nameof(AgahnimTowerEntrance_To_ATEntry))]
        [MemberData(nameof(GanonsTowerEntrance_To_ATEntry))]
        [MemberData(nameof(Start_To_EPEntry))]
        [MemberData(nameof(LightWorld_To_EPEntry))]
        [MemberData(nameof(Start_To_DPFrontEntry))]
        [MemberData(nameof(DesertPalaceFrontEntrance_To_DPFrontEntry))]
        [MemberData(nameof(Start_To_DPLeftEntry))]
        [MemberData(nameof(DesertLedge_To_DPLeftEntry))]
        [MemberData(nameof(Start_To_DPBackEntry))]
        [MemberData(nameof(DesertPalaceBackEntrance_To_DPBackEntry))]
        [MemberData(nameof(Start_To_ToHEntry))]
        [MemberData(nameof(DeathMountainWestTop_To_ToHEntry))]
        [MemberData(nameof(Start_To_PoDEntry))]
        [MemberData(nameof(PalaceOfDarknessEntrance_To_PoDEntry))]
        [MemberData(nameof(Start_To_SPEntry))]
        [MemberData(nameof(DarkWorldSouth_To_SPEntry))]
        [MemberData(nameof(LightWorld_To_SPEntry))]
        [MemberData(nameof(DarkWorldWest_To_SWFrontEntry))]
        [MemberData(nameof(DarkWorldWest_To_SWFrontLeftDropEntry))]
        [MemberData(nameof(DarkWorldWest_To_SWPinballRoomEntry))]
        [MemberData(nameof(DarkWorldWest_To_SWFrontTopDropEntry))]
        [MemberData(nameof(DarkWorldWest_To_SWFrontBackConnectorEntry))]
        [MemberData(nameof(Start_To_SWBackEntry))]
        [MemberData(nameof(SkullWoodsBackEntrance_To_SWBackEntry))]
        [MemberData(nameof(Start_To_TTEntry))]
        [MemberData(nameof(ThievesTownEntrance_To_TTEntry))]
        [MemberData(nameof(Start_To_IPEntry))]
        [MemberData(nameof(IcePalaceEntrance_To_IPEntry))]
        [MemberData(nameof(Start_To_MMEntry))]
        [MemberData(nameof(MiseryMireEntrance_To_MMEntry))]
        [MemberData(nameof(Start_To_TRFrontEntry))]
        [MemberData(nameof(TurtleRockFrontEntrance_To_TRFrontEntry))]
        [MemberData(nameof(TRFrontEntry_To_TRFrontToKeyDoors))]
        [MemberData(nameof(TRFrontToKeyDoors_To_TRKeyDoorsToMiddleExit))]
        [MemberData(nameof(Start_To_TRMiddleEntry))]
        [MemberData(nameof(TurtleRockTunnel_To_TRMiddleEntry))]
        [MemberData(nameof(Start_To_TRBackEntry))]
        [MemberData(nameof(TurtleRockSafetyDoor_To_TRBackEntry))]
        [MemberData(nameof(Start_To_GTEntry))]
        [MemberData(nameof(GanonsTowerEntrance_To_GTEntry))]
        [MemberData(nameof(AgahnimTowerEntrance_To_GTEntry))]
        public void AccessibilityTests(
            RequirementNodeID id, ItemPlacement itemPlacement,
            DungeonItemShuffle dungeonItemShuffle, WorldState worldState,
            bool entranceShuffle, bool enemyShuffle, (ItemType, int)[] items,
            (SequenceBreakType, bool)[] sequenceBreaks, AccessibilityLevel expected)
        {
            Mode.Instance.ItemPlacement = itemPlacement;
            Mode.Instance.DungeonItemShuffle = dungeonItemShuffle;
            Mode.Instance.WorldState = worldState;
            Mode.Instance.EntranceShuffle = entranceShuffle;
            Mode.Instance.EnemyShuffle = enemyShuffle;
            ItemDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled =
                    sequenceBreak.Item2;
            }

            Assert.Equal(expected, RequirementNodeDictionary.Instance[id].Accessibility);
        }

        public static IEnumerable<object[]> Start_To_HCSanctuaryEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.HCSanctuaryEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HCSanctuaryEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HCSanctuaryEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HCSanctuaryEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HCSanctuaryEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HCSanctuaryEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_HCSanctuaryEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.HCSanctuaryEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HCSanctuaryEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HCSanctuaryEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HCSanctuaryEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.HCSanctuaryEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_HCFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.HCFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HCFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HCFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HCFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HCFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HCFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_HCFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.HCFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HCFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HCFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.HCFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_HCBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.HCBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HCBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HCBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HCBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HCBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.HCBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EscapeGrave_To_HCBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.HCBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.EscapeGraveTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.HCBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.EscapeGraveTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_ATEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.ATEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ATEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> AgahnimTowerEntrance_To_ATEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.ATEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.AgahnimTowerEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ATEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.AgahnimTowerEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ATEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.AgahnimTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ATEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.AgahnimTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.ATEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.AgahnimTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> GanonsTowerEntrance_To_ATEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.ATEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GanonsTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ATEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GanonsTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ATEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GanonsTowerEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ATEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GanonsTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_EPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.EPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.EPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.EPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.EPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.EPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.EPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_EPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.EPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.EPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.EPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.EPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_DPFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DPFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DesertPalaceFrontEntrance_To_DPFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DPFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceFrontEntranceTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceFrontEntranceTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceFrontEntranceTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceFrontEntranceTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceFrontEntranceTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DPFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceFrontEntranceTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DPFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceFrontEntranceTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DPFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceFrontEntranceTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_DPLeftEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DPLeftEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPLeftEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DesertLedge_To_DPLeftEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DPLeftEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPLeftEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPLeftEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPLeftEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPLeftEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DPLeftEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertLedgeTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_DPBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DPBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DesertPalaceBackEntrance_To_DPBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DPBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DPBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.DPBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DPBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DPBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DesertPalaceBackEntranceTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_ToHEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.ToHEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ToHEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainWestTop_To_ToHEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.ToHEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ToHEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ToHEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 0),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ToHEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.ToHEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.ToHEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.ToHEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 1),
                        (ItemType.MoonPearl, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.ToHEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DeathMountainWestTopTest, 1),
                        (ItemType.MoonPearl, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_PoDEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.PoDEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.PoDEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> PalaceOfDarknessEntrance_To_PoDEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.PoDEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.PalaceOfDarknessEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.PoDEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.PalaceOfDarknessEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_SPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_SPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldSouthTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_SPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_SWFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SWFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SWFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_SWFrontLeftDropEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SWFrontLeftDropEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SWFrontLeftDropEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_SWPinballRoomEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SWPinballRoomEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SWPinballRoomEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_SWFrontTopDropEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SWFrontTopDropEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SWFrontTopDropEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_SWFrontBackConnectorEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SWFrontBackConnectorEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SWFrontBackConnectorEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DarkWorldWestTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_SWBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SWBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SWBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SkullWoodsBackEntrance_To_SWBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.SWBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SkullWoodsBackEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.SWBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.SkullWoodsBackEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_TTEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.TTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ThievesTownEntrance_To_TTEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.TTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.ThievesTownEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.ThievesTownEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_IPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.IPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> IcePalaceEntrance_To_IPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.IPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.IPEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.IcePalaceEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_MMEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.MMEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MMEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MiseryMireEntrance_To_MMEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.MMEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MiseryMireEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MMEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MiseryMireEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_TRFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.TRFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TurtleRockFrontEntrance_To_TRFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.TRFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TurtleRockFrontEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRFrontEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TurtleRockFrontEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TRFrontEntry_To_TRFrontToKeyDoors =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.TRFrontToKeyDoors,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRFrontToKeyDoors,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRFrontToKeyDoors,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRFrontToKeyDoors,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRFrontToKeyDoors,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRFrontToKeyDoors,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRFrontToKeyDoors,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRFrontToKeyDoors,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRFrontToKeyDoors,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRFrontToKeyDoors,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontEntryTest, 1),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TRFrontToKeyDoors_To_TRKeyDoorsToMiddleExit =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompasses,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.MapsCompassesSmallKeys,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 2),
                        (ItemType.SmallKey, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 1),
                        (ItemType.SmallKey, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Keysanity,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TRFrontToKeyDoorsTest, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.TRSmallKey, 0),
                        (ItemType.SmallKey, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
           };

        public static IEnumerable<object[]> Start_To_TRMiddleEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.TRMiddleEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRMiddleEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TurtleRockTunnel_To_TRMiddleEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.TRMiddleEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TurtleRockTunnelTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRMiddleEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TurtleRockTunnelTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_TRBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.TRBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TurtleRockSafetyDoor_To_TRBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.TRBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TurtleRockSafetyDoorTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.TRBackEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.TurtleRockSafetyDoorTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Start_To_GTEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.GTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.GTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> GanonsTowerEntrance_To_GTEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.GTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GanonsTowerEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.GTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GanonsTowerEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.GTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GanonsTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.GTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GanonsTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.GTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.GanonsTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> AgahnimTowerEntrance_To_GTEntry =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.GTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.AgahnimTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.GTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.AgahnimTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.GTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.AgahnimTowerEntranceTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.GTEntry,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.AgahnimTowerEntranceTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };
    }
}
