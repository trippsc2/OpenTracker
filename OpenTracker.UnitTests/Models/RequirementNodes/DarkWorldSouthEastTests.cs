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
    public class DarkWorldSouthEastTests : RequirementNodeTestBase
    {
        [Theory]
        [MemberData(nameof(DarkWorldWestNotBunnyToDWLakeHyliaFlippers))]
        [MemberData(nameof(DarkWorldSouthNotBunnyToDWLakeHyliaFlippers))]
        [MemberData(nameof(DWWitchAreaNotBunnyToDWLakeHyliaFlippers))]
        [MemberData(nameof(DarkWorldEastNotBunnyToDWLakeHyliaFlippers))]
        [MemberData(nameof(DarkWorldSouthEastNotBunnyToDWLakeHyliaFlippers))]
        [MemberData(nameof(IcePalaceIslandInvertedToDWLakeHyliaFlippers))]
        [MemberData(nameof(DarkWorldWestNotBunnyToDWLakeHyliaFakeFlippers))]
        [MemberData(nameof(DarkWorldSouthNotBunnyToDWLakeHyliaFakeFlippers))]
        [MemberData(nameof(DWWitchAreaNotBunnyToDWLakeHyliaFakeFlippers))]
        [MemberData(nameof(DarkWorldEastNotBunnyToDWLakeHyliaFakeFlippers))]
        [MemberData(nameof(DarkWorldSouthEastNotBunnyToDWLakeHyliaFakeFlippers))]
        [MemberData(nameof(IcePalaceIslandInvertedToDWLakeHyliaFakeFlippers))]
        [MemberData(nameof(DarkWorldWestNotBunnyToDWLakeHyliaWaterWalk))]
        [MemberData(nameof(LakeHyliaFairyIslandToIcePalaceIsland))]
        [MemberData(nameof(LakeHyliaFairyIslandStandardOpenToIcePalaceIsland))]
        [MemberData(nameof(DWLakeHyliaFlippersToIcePalaceIsland))]
        [MemberData(nameof(DWLakeHyliaFakeFlippersToIcePalaceIsland))]
        [MemberData(nameof(DWLakeHyliaWaterWalkToIcePalaceIsland))]
        [MemberData(nameof(IcePalaceIslandToIcePalaceIslandInverted))]
        [MemberData(nameof(LightWorldMirrorToDarkWorldSouthEast))]
        [MemberData(nameof(DWLakeHyliaFlippersToDarkWorldSouthEast))]
        [MemberData(nameof(DWLakeHyliaFakeFlippersToDarkWorldSouthEast))]
        [MemberData(nameof(DWLakeHyliaWaterWalkToDarkWorldSouthEast))]
        [MemberData(nameof(DarkWorldSouthEastToDarkWorldSouthEastNotBunny))]
        [MemberData(nameof(DarkWorldSouthEastNotBunnyToDarkWorldSouthEastLift1))]
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
        }
    
        public static IEnumerable<object[]> DarkWorldWestNotBunnyToDWLakeHyliaFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthNotBunnyToDWLakeHyliaFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWWitchAreaNotBunnyToDWLakeHyliaFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldEastNotBunnyToDWLakeHyliaFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthEastNotBunnyToDWLakeHyliaFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> IcePalaceIslandInvertedToDWLakeHyliaFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldWestNotBunnyToDWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthNotBunnyToDWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> DWWitchAreaNotBunnyToDWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> DarkWorldEastNotBunnyToDWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthEastNotBunnyToDWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> IcePalaceIslandInvertedToDWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> DarkWorldWestNotBunnyToDWLakeHyliaWaterWalk =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> LakeHyliaFairyIslandToIcePalaceIsland =>
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
                        RequirementNodeID.LakeHyliaFairyIsland
                    },
                    RequirementNodeID.IcePalaceIsland,
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
                        RequirementNodeID.LakeHyliaFairyIsland
                    },
                    RequirementNodeID.IcePalaceIsland,
                    false,
                    AccessibilityLevel.SequenceBreak
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
                        RequirementNodeID.LakeHyliaFairyIsland
                    },
                    RequirementNodeID.IcePalaceIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LakeHyliaFairyIslandStandardOpenToIcePalaceIsland =>
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
                        RequirementNodeID.LakeHyliaFairyIslandStandardOpen
                    },
                    RequirementNodeID.IcePalaceIsland,
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
                        RequirementNodeID.LakeHyliaFairyIslandStandardOpen
                    },
                    RequirementNodeID.IcePalaceIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaFlippersToIcePalaceIsland =>
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
                        RequirementNodeID.DWLakeHyliaFlippers
                    },
                    RequirementNodeID.IcePalaceIsland,
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
                        RequirementNodeID.DWLakeHyliaFlippers
                    },
                    RequirementNodeID.IcePalaceIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaFakeFlippersToIcePalaceIsland =>
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
                        RequirementNodeID.DWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.IcePalaceIsland,
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
                        RequirementNodeID.DWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.IcePalaceIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaWaterWalkToIcePalaceIsland =>
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
                        RequirementNodeID.DWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.IcePalaceIsland,
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
                        RequirementNodeID.DWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.IcePalaceIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> IcePalaceIslandToIcePalaceIslandInverted =>
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
                        RequirementNodeID.IcePalaceIsland
                    },
                    RequirementNodeID.IcePalaceIslandInverted,
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
                        RequirementNodeID.IcePalaceIsland
                    },
                    RequirementNodeID.IcePalaceIslandInverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldMirrorToDarkWorldSouthEast =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldSouthEast,
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
                        RequirementNodeID.LightWorldMirror
                    },
                    RequirementNodeID.DarkWorldSouthEast,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaFlippersToDarkWorldSouthEast =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldSouthEast,
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
                        RequirementNodeID.DWLakeHyliaFlippers
                    },
                    RequirementNodeID.DarkWorldSouthEast,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaFakeFlippersToDarkWorldSouthEast =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldSouthEast,
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
                        RequirementNodeID.DWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.DarkWorldSouthEast,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaWaterWalkToDarkWorldSouthEast =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldSouthEast,
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
                        RequirementNodeID.DWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.DarkWorldSouthEast,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthEastToDarkWorldSouthEastNotBunny =>
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
                        RequirementNodeID.DarkWorldSouthEast
                    },
                    RequirementNodeID.DarkWorldSouthEastNotBunny,
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
                    RequirementNodeID.DarkWorldSouthEastNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthEast
                    },
                    RequirementNodeID.DarkWorldSouthEastNotBunny,
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
                        RequirementNodeID.DarkWorldSouthEast
                    },
                    RequirementNodeID.DarkWorldSouthEastNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthEastNotBunnyToDarkWorldSouthEastLift1 =>
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
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DarkWorldSouthEastLift1,
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
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DarkWorldSouthEastLift1,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    }
}
