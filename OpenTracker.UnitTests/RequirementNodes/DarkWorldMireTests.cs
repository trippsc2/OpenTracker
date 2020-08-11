using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    public class DarkWorldMireTests
    {
        [Theory]
        [MemberData(nameof(Start_To_MireArea))]
        [MemberData(nameof(LightWorld_To_MireArea))]
        [MemberData(nameof(DWMirePortal_To_MireArea))]
        [MemberData(nameof(LightWorld_To_DWMirePortal))]
        [MemberData(nameof(LWMirePortal_To_DWMirePortal))]
        [MemberData(nameof(MireArea_To_MiseryMireEntrance))]
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

        public static IEnumerable<object[]> Start_To_MireArea =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaAccess, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    true,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaAccess, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_MireArea =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWMirePortal_To_MireArea =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MireArea,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.DWMirePortalTest, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_DWMirePortal =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flute, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flute, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flute, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flute, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Flute, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LightWorldTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Flute, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWMirePortal_To_DWMirePortal =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 0),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 1),
                        (ItemType.Mirror, 0),
                        (ItemType.Gloves, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.DWMirePortal,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.LWMirePortalTest, 1),
                        (ItemType.Mirror, 1),
                        (ItemType.Gloves, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MireArea_To_MiseryMireEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 0),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.StandardOpen,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Retro,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementNodeID.MiseryMireEntrance,
                    ItemPlacement.Advanced,
                    DungeonItemShuffle.Standard,
                    WorldState.Inverted,
                    false,
                    false,
                    new (ItemType, int)[]
                    {
                        (ItemType.MireAreaTest, 1),
                        (ItemType.MoonPearl, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    AccessibilityLevel.Normal
                }
            };
    }
}
