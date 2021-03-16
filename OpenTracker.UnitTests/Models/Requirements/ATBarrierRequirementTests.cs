using System.Collections.Generic;
using OpenTracker.Models.AccessibilityLevels;
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
    public class ATBarrierRequirementTests : ComplexItemRequirementTestBase
    {
        [Theory]
        [MemberData(nameof(ATBarrierData))]
        public override void AccessibilityTests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, (LocationID, int)[] smallKeys,
            (LocationID, int)[] bigKeys, RequirementNodeID[] accessibleNodes, RequirementType type,
            AccessibilityLevel expected)
        {
            base.AccessibilityTests(
                modeData, items, prizes, sequenceBreaks, smallKeys, bigKeys, accessibleNodes, type, expected);
        }

        public static IEnumerable<object[]> ATBarrierData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 0),
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ATBarrier,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 0),
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ATBarrier,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Cape, 0),
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ATBarrier,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 0),
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ATBarrier,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Cape, 0),
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ATBarrier,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Cape, 1),
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ATBarrier,
                    AccessibilityLevel.Normal
                }
            };
    }
}