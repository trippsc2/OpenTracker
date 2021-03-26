using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
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
        [MemberData(nameof(LightWorldInvertedToSouthOfGroveLedge))]
        [MemberData(nameof(SouthOfGroveLedgeToSouthOfGrove))]
        [MemberData(nameof(LightWorldNotBunnyToGroveDiggingSpot))]
        [MemberData(nameof(DesertBackNotBunnyToDesertLedge))]
        [MemberData(nameof(MireAreaMirrorToDesertLedge))]
        [MemberData(nameof(DPFrontEntryToDesertLedge))]
        [MemberData(nameof(DesertLedgeToDesertLedgeNotBunny))]
        [MemberData(nameof(DesertLedgeNotBunnyToDesertBack))]
        [MemberData(nameof(MireAreaMirrorToDesertBack))]
        [MemberData(nameof(DesertBackToDesertBackNotBunny))]
        [MemberData(nameof(LightWorldInvertedToCheckerboardLedge))]
        [MemberData(nameof(MireAreaMirrorToCheckerboardLedge))]
        [MemberData(nameof(CheckerboardLedgeToCheckerboardLedgeNotBunny))]
        [MemberData(nameof(CheckerboardLedgeNotBunnyToCheckerboardCave))]
        [MemberData(nameof(LightWorldToDesertPalaceFrontEntrance))]
        [MemberData(nameof(MireAreaMirrorToDesertPalaceFrontEntrance))]
        [MemberData(nameof(LightWorldInvertedToBombosTabletLedge))]
        [MemberData(nameof(DarkWorldSouthMirrorToBombosTabletLedge))]
        [MemberData(nameof(BombosTabletLedgeToBombosTablet))]
        [MemberData(nameof(FluteStandardOpenToLWMirePortal))]
        [MemberData(nameof(DWMirePortalInvertedToLWMirePortal))]
        [MemberData(nameof(LWMirePortalToLWMirePortalStandardOpen))]
        [MemberData(nameof(LightWorldHammerToLWSouthPortal))]
        [MemberData(nameof(DWSouthPortalInvertedToLWSouthPortal))]
        [MemberData(nameof(LWSouthPortalToLWSouthPortalStandardOpen))]
        [MemberData(nameof(LWSouthPortalToLWSouthPortalNotBunny))]
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
        }
    
        public static IEnumerable<object[]> LightWorldInvertedToSouthOfGroveLedge =>
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
                    new[]
                    {
                        RequirementNodeID.LightWorldInverted
                    },
                    RequirementNodeID.SouthOfGroveLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> SouthOfGroveLedgeToSouthOfGrove =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
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
                    new[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new[]
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
                    new[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, false)
                    },
                    new[]
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
                    new[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new[]
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
                    new[]
                    {
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new[]
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
                    new[]
                    {
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new[]
                    {
                        RequirementNodeID.SouthOfGroveLedge
                    },
                    RequirementNodeID.SouthOfGrove,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunnyToGroveDiggingSpot =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.GroveDiggingSpot,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DesertBackNotBunnyToDesertLedge =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DesertBackNotBunny
                    },
                    RequirementNodeID.DesertLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> MireAreaMirrorToDesertLedge =>
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
                    new[]
                    {
                        RequirementNodeID.MireAreaMirror
                    },
                    RequirementNodeID.DesertLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DPFrontEntryToDesertLedge =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DPFrontEntry
                    },
                    RequirementNodeID.DesertLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DesertLedgeToDesertLedgeNotBunny =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DesertLedge
                    },
                    RequirementNodeID.DesertLedgeNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DesertLedgeNotBunnyToDesertBack =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DesertLedgeNotBunny
                    },
                    RequirementNodeID.DesertBack,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> MireAreaMirrorToDesertBack =>
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
                    new[]
                    {
                        RequirementNodeID.MireAreaMirror
                    },
                    RequirementNodeID.DesertBack,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DesertBackToDesertBackNotBunny =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DesertBack
                    },
                    RequirementNodeID.DesertBackNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldInvertedToCheckerboardLedge =>
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
                    new[]
                    {
                        RequirementNodeID.LightWorldInverted
                    },
                    RequirementNodeID.CheckerboardLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> MireAreaMirrorToCheckerboardLedge =>
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
                    new[]
                    {
                        RequirementNodeID.MireAreaMirror
                    },
                    RequirementNodeID.CheckerboardLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> CheckerboardLedgeToCheckerboardLedgeNotBunny =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.CheckerboardLedge
                    },
                    RequirementNodeID.CheckerboardLedgeNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> CheckerboardLedgeNotBunnyToCheckerboardCave =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.CheckerboardLedgeNotBunny
                    },
                    RequirementNodeID.CheckerboardCave,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldToDesertPalaceFrontEntrance =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> MireAreaMirrorToDesertPalaceFrontEntrance =>
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
                    new[]
                    {
                        RequirementNodeID.MireAreaMirror
                    },
                    RequirementNodeID.DesertPalaceFrontEntrance,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldInvertedToBombosTabletLedge =>
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
                    new[]
                    {
                        RequirementNodeID.LightWorldInverted
                    },
                    RequirementNodeID.BombosTabletLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthMirrorToBombosTabletLedge =>
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
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthMirror
                    },
                    RequirementNodeID.BombosTabletLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> BombosTabletLedgeToBombosTablet =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Book, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (ItemType.Book, 0),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (ItemType.Book, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (ItemType.Book, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (ItemType.Book, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (ItemType.Book, 1),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.BombosTabletLedge
                    },
                    RequirementNodeID.BombosTablet,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> FluteStandardOpenToLWMirePortal =>
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
                    new[]
                    {
                        RequirementNodeID.FluteStandardOpen
                    },
                    RequirementNodeID.LWMirePortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWMirePortalInvertedToLWMirePortal =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DWMirePortalInverted
                    },
                    RequirementNodeID.LWMirePortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWMirePortalToLWMirePortalStandardOpen =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.LWMirePortal
                    },
                    RequirementNodeID.LWMirePortalStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldHammerToLWSouthPortal =>
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
                    new[]
                    {
                        RequirementNodeID.LightWorldHammer
                    },
                    RequirementNodeID.LWSouthPortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWSouthPortalInvertedToLWSouthPortal =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DWSouthPortalInverted
                    },
                    RequirementNodeID.LWSouthPortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWSouthPortalToLWSouthPortalStandardOpen =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.LWSouthPortal
                    },
                    RequirementNodeID.LWSouthPortalStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWSouthPortalToLWSouthPortalNotBunny =>
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
                    new[]
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
                    new[]
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
                    new[]
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
