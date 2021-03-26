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
    public class LWBossRequirementTests : ComplexItemRequirementTestBase
    {
        [Theory]
        [MemberData(nameof(ArmosData))]
        [MemberData(nameof(LanmolasData))]
        [MemberData(nameof(MoldormData))]
        [MemberData(nameof(AgaBossData))]
        public override void AccessibilityTests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, (LocationID, int)[] smallKeys,
            (LocationID, int)[] bigKeys, RequirementNodeID[] accessibleNodes, RequirementType type,
            AccessibilityLevel expected)
        {
            base.AccessibilityTests(
                modeData, items, prizes, sequenceBreaks, smallKeys, bigKeys, accessibleNodes, type, expected);
        }

        public static IEnumerable<object[]> ArmosData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 1),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Armos,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LanmolasData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Lanmolas,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Lanmolas,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Lanmolas,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Lanmolas,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Lanmolas,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Lanmolas,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Lanmolas,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Lanmolas,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MoldormData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Moldorm,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Moldorm,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Moldorm,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> AgaBossData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Net, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.AgaBoss,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Net, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.AgaBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Net, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.AgaBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Net, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.AgaBoss,
                    AccessibilityLevel.Normal
                }
            };
    }
}