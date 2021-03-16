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
    public class DarkWorldSouthEastTests : RequirementNodeTestBase
    {
        [Theory]
        [MemberData(nameof(DarkWorldWestNotBunny_To_DWLakeHyliaFlippers))]
        [MemberData(nameof(DarkWorldSouthNotBunny_To_DWLakeHyliaFlippers))]
        [MemberData(nameof(DWWitchAreaNotBunny_To_DWLakeHyliaFlippers))]
        [MemberData(nameof(DarkWorldEastNotBunny_To_DWLakeHyliaFlippers))]
        [MemberData(nameof(DarkWorldSouthEastNotBunny_To_DWLakeHyliaFlippers))]
        [MemberData(nameof(IcePalaceIslandInverted_To_DWLakeHyliaFlippers))]
        [MemberData(nameof(DarkWorldWestNotBunny_To_DWLakeHyliaFakeFlippers))]
        [MemberData(nameof(DarkWorldSouthNotBunny_To_DWLakeHyliaFakeFlippers))]
        [MemberData(nameof(DWWitchAreaNotBunny_To_DWLakeHyliaFakeFlippers))]
        [MemberData(nameof(DarkWorldEastNotBunny_To_DWLakeHyliaFakeFlippers))]
        [MemberData(nameof(DarkWorldSouthEastNotBunny_To_DWLakeHyliaFakeFlippers))]
        [MemberData(nameof(IcePalaceIslandInverted_To_DWLakeHyliaFakeFlippers))]
        [MemberData(nameof(DarkWorldWestNotBunny_To_DWLakeHyliaWaterWalk))]
        [MemberData(nameof(LakeHyliaFairyIsland_To_IcePalaceIsland))]
        [MemberData(nameof(LakeHyliaFairyIslandStandardOpen_To_IcePalaceIsland))]
        [MemberData(nameof(DWLakeHyliaFlippers_To_IcePalaceIsland))]
        [MemberData(nameof(DWLakeHyliaFakeFlippers_To_IcePalaceIsland))]
        [MemberData(nameof(DWLakeHyliaWaterWalk_To_IcePalaceIsland))]
        [MemberData(nameof(IcePalaceIsland_To_IcePalaceIslandInverted))]
        [MemberData(nameof(LightWorldMirror_To_DarkWorldSouthEast))]
        [MemberData(nameof(DWLakeHyliaFlippers_To_DarkWorldSouthEast))]
        [MemberData(nameof(DWLakeHyliaFakeFlippers_To_DarkWorldSouthEast))]
        [MemberData(nameof(DWLakeHyliaWaterWalk_To_DarkWorldSouthEast))]
        [MemberData(nameof(DarkWorldSouthEast_To_DarkWorldSouthEastNotBunny))]
        [MemberData(nameof(DarkWorldSouthEastNotBunny_To_DarkWorldSouthEastLift1))]
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
        }
    
        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_DWLakeHyliaFlippers =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthNotBunny_To_DWLakeHyliaFlippers =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWWitchAreaNotBunny_To_DWLakeHyliaFlippers =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldEastNotBunny_To_DWLakeHyliaFlippers =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthEastNotBunny_To_DWLakeHyliaFlippers =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> IcePalaceIslandInverted_To_DWLakeHyliaFlippers =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFlippers,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_DWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersQirnJump, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthNotBunny_To_DWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> DWWitchAreaNotBunny_To_DWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> DarkWorldEastNotBunny_To_DWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthEastNotBunny_To_DWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthEastNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> IcePalaceIslandInverted_To_DWLakeHyliaFakeFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
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
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true),
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.IcePalaceIslandInverted
                    },
                    RequirementNodeID.DWLakeHyliaFakeFlippers,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_DWLakeHyliaWaterWalk =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWLakeHyliaWaterWalk,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> LakeHyliaFairyIsland_To_IcePalaceIsland =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LakeHyliaFairyIsland
                    },
                    RequirementNodeID.IcePalaceIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LakeHyliaFairyIslandStandardOpen_To_IcePalaceIsland =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LakeHyliaFairyIslandStandardOpen
                    },
                    RequirementNodeID.IcePalaceIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaFlippers_To_IcePalaceIsland =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWLakeHyliaFlippers
                    },
                    RequirementNodeID.IcePalaceIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaFakeFlippers_To_IcePalaceIsland =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.IcePalaceIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaWaterWalk_To_IcePalaceIsland =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.IcePalaceIsland,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> IcePalaceIsland_To_IcePalaceIslandInverted =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    RequirementNodeID.IcePalaceIslandInverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldMirror_To_DarkWorldSouthEast =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldMirror
                    },
                    RequirementNodeID.DarkWorldSouthEast,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaFlippers_To_DarkWorldSouthEast =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWLakeHyliaFlippers
                    },
                    RequirementNodeID.DarkWorldSouthEast,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaFakeFlippers_To_DarkWorldSouthEast =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.DarkWorldSouthEast,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWLakeHyliaWaterWalk_To_DarkWorldSouthEast =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.DarkWorldSouthEast,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthEast_To_DarkWorldSouthEastNotBunny =>
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
                    new RequirementNodeID[]
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthEast
                    },
                    RequirementNodeID.DarkWorldSouthEastNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthEastNotBunny_To_DarkWorldSouthEastLift1 =>
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
                    new RequirementNodeID[]
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
