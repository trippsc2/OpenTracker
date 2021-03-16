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
    public class DWDeathMountainWestTests : RequirementNodeTestBase
    {
        [Theory]
        [MemberData(nameof(FluteInverted_To_DarkDeathMountainWestBottom))]
        [MemberData(nameof(DeathMountainWestBottom_To_DarkDeathMountainWestBottom))]
        [MemberData(nameof(DarkDeathMountainTop_To_DarkDeathMountainWestBottom))]
        [MemberData(nameof(DarkDeathMountainWestBottom_To_DarkDeathMountainWestBottomInverted))]
        [MemberData(nameof(DarkDeathMountainWestBottom_To_DarkDeathMountainWestBottomNonEntrance))]
        [MemberData(nameof(DarkDeathMountainWestBottom_To_DarkDeathMountainWestBottomMirror))]
        [MemberData(nameof(DarkDeathMountainWestBottom_To_DarkDeathMountainWestBottomNotBunny))]
        [MemberData(nameof(DarkDeathMountainWestBottomNotBunny_To_SpikeCavePastHammerBlocks))]
        [MemberData(nameof(SpikeCavePastHammerBlocks_To_SpikeCavePastSpikes))]
        [MemberData(nameof(SpikeCavePastSpikes_To_SpikeCaveChest))]
        [MemberData(nameof(DeathMountainWestTop_To_DarkDeathMountainTop))]
        [MemberData(nameof(DeathMountainEastTop_To_DarkDeathMountainTop))]
        [MemberData(nameof(DarkDeathMountainWestBottomInverted_To_DarkDeathMountainTop))]
        [MemberData(nameof(SuperBunnyCave_To_DarkDeathMountainTop))]
        [MemberData(nameof(DWFloatingIsland_To_DarkDeathMountainTop))]
        [MemberData(nameof(DWTurtleRockTop_To_DarkDeathMountainTop))]
        [MemberData(nameof(DarkDeathMountainTop_To_DarkDeathMountainTopInverted))]
        [MemberData(nameof(DarkDeathMountainTop_To_DarkDeathMountainTopStandardOpen))]
        [MemberData(nameof(DarkDeathMountainTop_To_DarkDeathMountainTopMirror))]
        [MemberData(nameof(DarkDeathMountainTop_To_DarkDeathMountainTopNotBunny))]
        [MemberData(nameof(DarkDeathMountainTopInverted_To_GanonsTowerEntrance))]
        [MemberData(nameof(DarkDeathMountainTopStandardOpen_To_GanonsTowerEntrance))]
        [MemberData(nameof(GanonsTowerEntrance_To_GanonsTowerEntranceStandardOpen))]
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
        }
    
        public static IEnumerable<object[]> FluteInverted_To_DarkDeathMountainWestBottom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkDeathMountainWestBottom,
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
                        RequirementNodeID.FluteInverted
                    },
                    RequirementNodeID.DarkDeathMountainWestBottom,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainWestBottom_To_DarkDeathMountainWestBottom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottom,
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
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottom,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottom,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTop_To_DarkDeathMountainWestBottom =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkDeathMountainWestBottom,
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
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainWestBottom,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottom_To_DarkDeathMountainWestBottomInverted =>
            new List<object[]>
            {
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
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomInverted,
                    false,
                    AccessibilityLevel.None
                },
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
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomInverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottom_To_DarkDeathMountainWestBottomNonEntrance =>
            new List<object[]>
            {
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
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomNonEntrance,
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
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomNonEntrance,
                    false,
                    AccessibilityLevel.Normal
                },
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
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomNonEntrance,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottom_To_DarkDeathMountainWestBottomMirror =>
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
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomMirror,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomMirror,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomMirror,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottom_To_DarkDeathMountainWestBottomNotBunny =>
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkDeathMountainWestBottomNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomNotBunny,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottomNotBunny_To_SpikeCavePastHammerBlocks =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottomNotBunny
                    },
                    RequirementNodeID.SpikeCavePastHammerBlocks,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottomNotBunny
                    },
                    RequirementNodeID.SpikeCavePastHammerBlocks,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> SpikeCavePastHammerBlocks_To_SpikeCavePastSpikes =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Cape, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.SpikeCavePastSpikes,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SpikeCavePastHammerBlocks
                    },
                    RequirementNodeID.SpikeCavePastSpikes,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Cape, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SpikeCavePastHammerBlocks
                    },
                    RequirementNodeID.SpikeCavePastSpikes,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SpikeCavePastHammerBlocks
                    },
                    RequirementNodeID.SpikeCavePastSpikes,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.SpikeCavePastHammerBlocks
                    },
                    RequirementNodeID.SpikeCavePastSpikes,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> SpikeCavePastSpikes_To_SpikeCaveChest =>
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
                        RequirementNodeID.SpikeCavePastSpikes
                    },
                    RequirementNodeID.SpikeCaveChest,
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
                        RequirementNodeID.SpikeCavePastSpikes
                    },
                    RequirementNodeID.SpikeCaveChest,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainWestTop_To_DarkDeathMountainTop =>
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
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestTop
                    },
                    RequirementNodeID.DarkDeathMountainTop,
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
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestTop
                    },
                    RequirementNodeID.DarkDeathMountainTop,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestTop
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainEastTop_To_DarkDeathMountainTop =>
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEastTop
                    },
                    RequirementNodeID.DarkDeathMountainTop,
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
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEastTop
                    },
                    RequirementNodeID.DarkDeathMountainTop,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEastTop
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottomInverted_To_DarkDeathMountainTop =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkDeathMountainTop,
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
                        RequirementNodeID.DarkDeathMountainWestBottomInverted
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> SuperBunnyCave_To_DarkDeathMountainTop =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkDeathMountainTop,
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
                        RequirementNodeID.SuperBunnyCave
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWFloatingIsland_To_DarkDeathMountainTop =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkDeathMountainTop,
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
                        RequirementNodeID.DWFloatingIsland
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWTurtleRockTop_To_DarkDeathMountainTop =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkDeathMountainTop,
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
                        RequirementNodeID.DWTurtleRockTop
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTop_To_DarkDeathMountainTopInverted =>
            new List<object[]>
            {
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
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopInverted,
                    false,
                    AccessibilityLevel.None
                },
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
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopInverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTop_To_DarkDeathMountainTopStandardOpen =>
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
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopStandardOpen,
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
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTop_To_DarkDeathMountainTopMirror =>
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
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopMirror,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopMirror,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopMirror,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTop_To_DarkDeathMountainTopNotBunny =>
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkDeathMountainTopNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopNotBunny,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTopInverted_To_GanonsTowerEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.GanonsTowerEntrance,
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
                        RequirementNodeID.DarkDeathMountainTopInverted
                    },
                    RequirementNodeID.GanonsTowerEntrance,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTopStandardOpen_To_GanonsTowerEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.TowerCrystals, 6)
                    },
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 0),
                        (PrizeType.Crystal, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTopStandardOpen
                    },
                    RequirementNodeID.GanonsTowerEntrance,
                    true,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.TowerCrystals, 0)
                    },
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 1),
                        (PrizeType.Crystal, 5)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTopStandardOpen
                    },
                    RequirementNodeID.GanonsTowerEntrance,
                    true,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.TowerCrystals, 0)
                    },
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 2),
                        (PrizeType.Crystal, 4)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTopStandardOpen
                    },
                    RequirementNodeID.GanonsTowerEntrance,
                    true,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.TowerCrystals, 7)
                    },
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 1),
                        (PrizeType.Crystal, 5)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTopStandardOpen
                    },
                    RequirementNodeID.GanonsTowerEntrance,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.TowerCrystals, 7)
                    },
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 2),
                        (PrizeType.Crystal, 4)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTopStandardOpen
                    },
                    RequirementNodeID.GanonsTowerEntrance,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.TowerCrystals, 7)
                    },
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 2),
                        (PrizeType.Crystal, 5)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTopStandardOpen
                    },
                    RequirementNodeID.GanonsTowerEntrance,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.TowerCrystals, 7)
                    },
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 0),
                        (PrizeType.Crystal, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkDeathMountainTopStandardOpen
                    },
                    RequirementNodeID.GanonsTowerEntrance,
                    true,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> GanonsTowerEntrance_To_GanonsTowerEntranceStandardOpen =>
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
                        RequirementNodeID.GanonsTowerEntrance
                    },
                    RequirementNodeID.GanonsTowerEntranceStandardOpen,
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
                        RequirementNodeID.GanonsTowerEntrance
                    },
                    RequirementNodeID.GanonsTowerEntranceStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    }
}
