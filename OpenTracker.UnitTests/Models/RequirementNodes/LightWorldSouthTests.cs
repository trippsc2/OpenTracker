using System.Collections.Generic;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.RequirementNodes
{
    public class LightWorldSouthTests : RequirementNodeTestBase
    {
        [Theory]
        [MemberData(nameof(LightWorldInverted_To_SouthOfGroveLedge))]
        [MemberData(nameof(SouthOfGroveLedge_To_SouthOfGrove))]
        [MemberData(nameof(LightWorldNotBunny_To_GroveDiggingSpot))]
        [MemberData(nameof(DesertBackNotBunny_To_DesertLedge))]
        [MemberData(nameof(MireAreaMirror_To_DesertLedge))]
        [MemberData(nameof(DPFrontEntry_To_DesertLedge))]
        [MemberData(nameof(DesertLedge_To_DesertLedgeNotBunny))]
        [MemberData(nameof(DesertLedgeNotBunny_To_DesertBack))]
        [MemberData(nameof(MireAreaMirror_To_DesertBack))]
        [MemberData(nameof(DesertBack_To_DesertBackNotBunny))]
        [MemberData(nameof(LightWorldInverted_To_CheckerboardLedge))]
        [MemberData(nameof(MireAreaMirror_To_CheckerboardLedge))]
        [MemberData(nameof(CheckerboardLedge_To_CheckerboardLedgeNotBunny))]
        [MemberData(nameof(CheckerboardLedgeNotBunny_To_CheckerboardCave))]
        [MemberData(nameof(LightWorld_To_DesertPalaceFrontEntrance))]
        [MemberData(nameof(MireAreaMirror_To_DesertPalaceFrontEntrance))]
        [MemberData(nameof(LightWorldInverted_To_BombosTabletLedge))]
        [MemberData(nameof(DarkWorldSouthMirror_To_BombosTabletLedge))]
        [MemberData(nameof(BombosTabletLedge_To_BombosTablet))]
        [MemberData(nameof(FluteStandardOpen_To_LWMirePortal))]
        [MemberData(nameof(DWMirePortalInverted_To_LWMirePortal))]
        [MemberData(nameof(LWMirePortal_To_LWMirePortalStandardOpen))]
        [MemberData(nameof(LightWorldHammer_To_LWSouthPortal))]
        [MemberData(nameof(DWSouthPortalInverted_To_LWSouthPortal))]
        [MemberData(nameof(LWSouthPortal_To_LWSouthPortalStandardOpen))]
        [MemberData(nameof(LWSouthPortal_To_LWSouthPortalNotBunny))]
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
        }
    
        public static IEnumerable<object[]> LightWorldInverted_To_SouthOfGroveLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.SouthOfGroveLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldInverted
                    },
                    RequirementNodeID.SouthOfGroveLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> SouthOfGroveLedge_To_SouthOfGrove =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.SouthOfGrove,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SouthOfGroveLedge
                    },
                    RequirementNodeID.SouthOfGrove,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, false)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SouthOfGroveLedge
                    },
                    RequirementNodeID.SouthOfGrove,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SouthOfGroveLedge
                    },
                    RequirementNodeID.SouthOfGrove,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SouthOfGroveLedge
                    },
                    RequirementNodeID.SouthOfGrove,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SouthOfGroveLedge
                    },
                    RequirementNodeID.SouthOfGrove,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunny_To_GroveDiggingSpot =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Shovel, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.GroveDiggingSpot,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Shovel, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.GroveDiggingSpot,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DesertBackNotBunny_To_DesertLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertBackNotBunny
                    },
                    RequirementNodeID.DesertLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertBackNotBunny
                    },
                    RequirementNodeID.DesertLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> MireAreaMirror_To_DesertLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DesertLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaMirror
                    },
                    RequirementNodeID.DesertLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DPFrontEntry_To_DesertLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DPFrontEntry
                    },
                    RequirementNodeID.DesertLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DPFrontEntry
                    },
                    RequirementNodeID.DesertLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DPFrontEntry
                    },
                    RequirementNodeID.DesertLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DesertLedge_To_DesertLedgeNotBunny =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DesertLedgeNotBunny,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertLedge
                    },
                    RequirementNodeID.DesertLedgeNotBunny,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertLedge
                    },
                    RequirementNodeID.DesertLedgeNotBunny,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertLedge
                    },
                    RequirementNodeID.DesertLedgeNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DesertLedgeNotBunny_To_DesertBack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertLedgeNotBunny
                    },
                    RequirementNodeID.DesertBack,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertLedgeNotBunny
                    },
                    RequirementNodeID.DesertBack,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> MireAreaMirror_To_DesertBack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DesertBack,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaMirror
                    },
                    RequirementNodeID.DesertBack,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DesertBack_To_DesertBackNotBunny =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DesertBackNotBunny,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertBack
                    },
                    RequirementNodeID.DesertBackNotBunny,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertBack
                    },
                    RequirementNodeID.DesertBackNotBunny,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertBack
                    },
                    RequirementNodeID.DesertBackNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldInverted_To_CheckerboardLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.CheckerboardLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldInverted
                    },
                    RequirementNodeID.CheckerboardLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> MireAreaMirror_To_CheckerboardLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.CheckerboardLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaMirror
                    },
                    RequirementNodeID.CheckerboardLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> CheckerboardLedge_To_CheckerboardLedgeNotBunny =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.CheckerboardLedgeNotBunny,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.CheckerboardLedge
                    },
                    RequirementNodeID.CheckerboardLedgeNotBunny,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.CheckerboardLedge
                    },
                    RequirementNodeID.CheckerboardLedgeNotBunny,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.CheckerboardLedge
                    },
                    RequirementNodeID.CheckerboardLedgeNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> CheckerboardLedgeNotBunny_To_CheckerboardCave =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.CheckerboardLedgeNotBunny
                    },
                    RequirementNodeID.CheckerboardCave,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.CheckerboardLedgeNotBunny
                    },
                    RequirementNodeID.CheckerboardCave,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorld_To_DesertPalaceFrontEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> MireAreaMirror_To_DesertPalaceFrontEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaMirror
                    },
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldInverted_To_BombosTabletLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.BombosTabletLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldInverted
                    },
                    RequirementNodeID.BombosTabletLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthMirror_To_BombosTabletLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.BombosTabletLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthMirror
                    },
                    RequirementNodeID.BombosTabletLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> BombosTabletLedge_To_BombosTablet =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BombosTabletLedge
                    },
                    RequirementNodeID.BombosTablet,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 0),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BombosTabletLedge
                    },
                    RequirementNodeID.BombosTablet,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BombosTabletLedge
                    },
                    RequirementNodeID.BombosTablet,
                    false,
                    AccessibilityLevel.Inspect
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BombosTabletLedge
                    },
                    RequirementNodeID.BombosTablet,
                    false,
                    AccessibilityLevel.Inspect
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BombosTabletLedge
                    },
                    RequirementNodeID.BombosTablet,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 1),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BombosTabletLedge
                    },
                    RequirementNodeID.BombosTablet,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> FluteStandardOpen_To_LWMirePortal =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LWMirePortal,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.FluteStandardOpen
                    },
                    RequirementNodeID.LWMirePortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWMirePortalInverted_To_LWMirePortal =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWMirePortalInverted
                    },
                    RequirementNodeID.LWMirePortal,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWMirePortalInverted
                    },
                    RequirementNodeID.LWMirePortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWMirePortal_To_LWMirePortalStandardOpen =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWMirePortal
                    },
                    RequirementNodeID.LWMirePortalStandardOpen,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWMirePortal
                    },
                    RequirementNodeID.LWMirePortalStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldHammer_To_LWSouthPortal =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LWSouthPortal,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldHammer
                    },
                    RequirementNodeID.LWSouthPortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWSouthPortalInverted_To_LWSouthPortal =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWSouthPortalInverted
                    },
                    RequirementNodeID.LWSouthPortal,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWSouthPortalInverted
                    },
                    RequirementNodeID.LWSouthPortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWSouthPortal_To_LWSouthPortalStandardOpen =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWSouthPortal
                    },
                    RequirementNodeID.LWSouthPortalStandardOpen,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWSouthPortal
                    },
                    RequirementNodeID.LWSouthPortalStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWSouthPortal_To_LWSouthPortalNotBunny =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LWSouthPortalNotBunny,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWSouthPortal
                    },
                    RequirementNodeID.LWSouthPortalNotBunny,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWSouthPortal
                    },
                    RequirementNodeID.LWSouthPortalNotBunny,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWSouthPortal
                    },
                    RequirementNodeID.LWSouthPortalNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    }
}
