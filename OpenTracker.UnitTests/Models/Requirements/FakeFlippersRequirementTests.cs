using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements
{
    public class FakeFlippersRequirementTests : ComplexItemRequirementTestBase
    {
        [Theory]
        [MemberData(nameof(FakeFlippersFairyRevivalData))]
        [MemberData(nameof(FakeFlippersQirnJumpData))]
        [MemberData(nameof(FakeFlippersScreenTransitionData))]
        [MemberData(nameof(FakeFlippersSplashDeletionData))]
        public override void AccessibilityTests(ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, (LocationID, int)[] smallKeys, (LocationID, int)[] bigKeys,
            RequirementNodeID[] accessibleNodes, RequirementType type, AccessibilityLevel expected)
        {
            base.AccessibilityTests(
                modeData, items, prizes, sequenceBreaks, smallKeys, bigKeys, accessibleNodes, type, expected);
        }

        public static IEnumerable<object[]> FakeFlippersFairyRevivalData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersFairyRevival,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersFairyRevival,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersFairyRevival, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersFairyRevival,
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> FakeFlippersQirnJumpData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersQirnJump, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersQirnJump,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersQirnJump, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersQirnJump,
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> FakeFlippersScreenTransitionData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersScreenTransition, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersScreenTransition,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersScreenTransition, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersScreenTransition,
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> FakeFlippersSplashDeletionData =>
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
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersSplashDeletion,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersSplashDeletion,
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
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersSplashDeletion,
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
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersSplashDeletion,
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
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersSplashDeletion,
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
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.FakeFlippersSplashDeletion,
                    AccessibilityLevel.SequenceBreak
                }
            };
    }
}