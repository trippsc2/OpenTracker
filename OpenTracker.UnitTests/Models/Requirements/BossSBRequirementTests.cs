using System.Collections.Generic;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements
{
    public class BossSBRequirementTests : ComplexItemRequirementTestBase
    {
        [Theory]
        [MemberData(nameof(HelmasaurKingSBData))]
        [MemberData(nameof(ArrghusSBData))]
        [MemberData(nameof(MothulaSBData))]
        [MemberData(nameof(BlindSBData))]
        [MemberData(nameof(KholdstareSBData))]
        [MemberData(nameof(VitreousSBData))]
        [MemberData(nameof(TrinexxSBData))]
        [MemberData(nameof(UnknownBossData))]
        public override void AccessibilityTests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, (LocationID, int)[] smallKeys,
            (LocationID, int)[] bigKeys, RequirementNodeID[] accessibleNodes, RequirementType type,
            AccessibilityLevel expected)
        {
            base.AccessibilityTests(
                modeData, items, prizes, sequenceBreaks, smallKeys, bigKeys, accessibleNodes, type, expected);
        }

        public static IEnumerable<object[]> HelmasaurKingSBData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.HelmasaurKingSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.HelmasaurKingSB,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ArrghusSBData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ArrghusSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1),
                        (ItemType.Bow, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ArrghusSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ArrghusSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ArrghusSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ArrghusSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ArrghusSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ArrghusSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ArrghusSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ArrghusSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ArrghusSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.ArrghusSB,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MothulaSBData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.MothulaSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.MothulaSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.MothulaSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.MothulaSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.MothulaSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.MothulaSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.MothulaSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.MothulaSB,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BlindSBData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.BlindSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.BlindSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.BlindSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.BlindSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.BlindSB,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> KholdstareSBData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.KholdstareSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.KholdstareSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.KholdstareSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.KholdstareSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.KholdstareSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.KholdstareSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.KholdstareSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.KholdstareSB,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> VitreousSBData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.VitreousSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.VitreousSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.VitreousSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.VitreousSB,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TrinexxSBData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.TrinexxSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.TrinexxSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.TrinexxSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.TrinexxSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.TrinexxSB,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.TrinexxSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.TrinexxSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.TrinexxSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 4),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.TrinexxSB,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.TrinexxSB,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> UnknownBossData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Hookshot, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Bow, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 4),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Bow, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Bow, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Bow, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Cape, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.Cape, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 4),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Cape, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 4),
                        (ItemType.Hammer, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Hookshot, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Cape, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.UnknownBoss,
                    AccessibilityLevel.Normal
                }
            };
    }
}