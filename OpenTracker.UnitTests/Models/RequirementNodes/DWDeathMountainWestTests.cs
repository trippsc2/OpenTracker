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
    public class DWDeathMountainWestTests : RequirementNodeTestBase
    {
        [Theory]
        [MemberData(nameof(FluteInvertedToDarkDeathMountainWestBottom))]
        [MemberData(nameof(DeathMountainWestBottomToDarkDeathMountainWestBottom))]
        [MemberData(nameof(DarkDeathMountainTopToDarkDeathMountainWestBottom))]
        [MemberData(nameof(DarkDeathMountainWestBottomToDarkDeathMountainWestBottomInverted))]
        [MemberData(nameof(DarkDeathMountainWestBottomToDarkDeathMountainWestBottomNonEntrance))]
        [MemberData(nameof(DarkDeathMountainWestBottomToDarkDeathMountainWestBottomMirror))]
        [MemberData(nameof(DarkDeathMountainWestBottomToDarkDeathMountainWestBottomNotBunny))]
        [MemberData(nameof(DarkDeathMountainWestBottomNotBunnyToSpikeCavePastHammerBlocks))]
        [MemberData(nameof(SpikeCavePastHammerBlocksToSpikeCavePastSpikes))]
        [MemberData(nameof(SpikeCavePastSpikesToSpikeCaveChest))]
        [MemberData(nameof(DeathMountainWestTopToDarkDeathMountainTop))]
        [MemberData(nameof(DeathMountainEastTopToDarkDeathMountainTop))]
        [MemberData(nameof(DarkDeathMountainWestBottomInvertedToDarkDeathMountainTop))]
        [MemberData(nameof(SuperBunnyCaveToDarkDeathMountainTop))]
        [MemberData(nameof(DWFloatingIslandToDarkDeathMountainTop))]
        [MemberData(nameof(DWTurtleRockTopToDarkDeathMountainTop))]
        [MemberData(nameof(DarkDeathMountainTopToDarkDeathMountainTopInverted))]
        [MemberData(nameof(DarkDeathMountainTopToDarkDeathMountainTopStandardOpen))]
        [MemberData(nameof(DarkDeathMountainTopToDarkDeathMountainTopMirror))]
        [MemberData(nameof(DarkDeathMountainTopToDarkDeathMountainTopNotBunny))]
        [MemberData(nameof(DarkDeathMountainTopInvertedToGanonsTowerEntrance))]
        [MemberData(nameof(DarkDeathMountainTopStandardOpenToGanonsTowerEntrance))]
        [MemberData(nameof(GanonsTowerEntranceToGanonsTowerEntranceStandardOpen))]
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
        }
    
        public static IEnumerable<object[]> FluteInvertedToDarkDeathMountainWestBottom =>
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
                    new[]
                    {
                        RequirementNodeID.FluteInverted
                    },
                    RequirementNodeID.DarkDeathMountainWestBottom,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainWestBottomToDarkDeathMountainWestBottom =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottom,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTopToDarkDeathMountainWestBottom =>
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainWestBottom,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottomToDarkDeathMountainWestBottomInverted =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomInverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottomToDarkDeathMountainWestBottomNonEntrance =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomNonEntrance,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottomToDarkDeathMountainWestBottomMirror =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomMirror,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottomToDarkDeathMountainWestBottomNotBunny =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottom
                    },
                    RequirementNodeID.DarkDeathMountainWestBottomNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottomNotBunnyToSpikeCavePastHammerBlocks =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottomNotBunny
                    },
                    RequirementNodeID.SpikeCavePastHammerBlocks,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> SpikeCavePastHammerBlocksToSpikeCavePastSpikes =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
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
                    new[]
                    {
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Cape, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.SpikeCavePastHammerBlocks
                    },
                    RequirementNodeID.SpikeCavePastSpikes,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> SpikeCavePastSpikesToSpikeCaveChest =>
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
                    new[]
                    {
                        RequirementNodeID.SpikeCavePastSpikes
                    },
                    RequirementNodeID.SpikeCaveChest,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainWestTopToDarkDeathMountainTop =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DeathMountainWestTop
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainEastTopToDarkDeathMountainTop =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DeathMountainEastTop
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottomInvertedToDarkDeathMountainTop =>
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainWestBottomInverted
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> SuperBunnyCaveToDarkDeathMountainTop =>
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
                    new[]
                    {
                        RequirementNodeID.SuperBunnyCave
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWFloatingIslandToDarkDeathMountainTop =>
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
                    new[]
                    {
                        RequirementNodeID.DWFloatingIsland
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWTurtleRockTopToDarkDeathMountainTop =>
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
                    new[]
                    {
                        RequirementNodeID.DWTurtleRockTop
                    },
                    RequirementNodeID.DarkDeathMountainTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTopToDarkDeathMountainTopInverted =>
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopInverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTopToDarkDeathMountainTopStandardOpen =>
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTopToDarkDeathMountainTopMirror =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopMirror,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTopToDarkDeathMountainTopNotBunny =>
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
                    new[]
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
                    new[]
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainTop
                    },
                    RequirementNodeID.DarkDeathMountainTopNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTopInvertedToGanonsTowerEntrance =>
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
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainTopInverted
                    },
                    RequirementNodeID.GanonsTowerEntrance,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainTopStandardOpenToGanonsTowerEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.TowerCrystals, 6)
                    },
                    new[]
                    {
                        (PrizeType.RedCrystal, 0),
                        (PrizeType.Crystal, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (PrizeType.RedCrystal, 1),
                        (PrizeType.Crystal, 5)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (PrizeType.RedCrystal, 2),
                        (PrizeType.Crystal, 4)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (PrizeType.RedCrystal, 1),
                        (PrizeType.Crystal, 5)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (PrizeType.RedCrystal, 2),
                        (PrizeType.Crystal, 4)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (PrizeType.RedCrystal, 2),
                        (PrizeType.Crystal, 5)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
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
                    new[]
                    {
                        (PrizeType.RedCrystal, 0),
                        (PrizeType.Crystal, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkDeathMountainTopStandardOpen
                    },
                    RequirementNodeID.GanonsTowerEntrance,
                    true,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> GanonsTowerEntranceToGanonsTowerEntranceStandardOpen =>
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
                    new[]
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
