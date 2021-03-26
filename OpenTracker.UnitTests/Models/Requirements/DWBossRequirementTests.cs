using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
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
    public class DWBossRequirementTests : ComplexItemRequirementTestBase
    {
        [Theory]
        [MemberData(nameof(HelmasaurKingData))]
        [MemberData(nameof(ArrghusData))]
        [MemberData(nameof(MothulaData))]
        [MemberData(nameof(BlindData))]
        [MemberData(nameof(KholdstareData))]
        [MemberData(nameof(VitreousData))]
        [MemberData(nameof(TrinexxData))]
        public override void AccessibilityTests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, (LocationID, int)[] smallKeys,
            (LocationID, int)[] bigKeys, RequirementNodeID[] accessibleNodes, RequirementType type,
            AccessibilityLevel expected)
        {
            base.AccessibilityTests(
                modeData, items, prizes, sequenceBreaks, smallKeys, bigKeys, accessibleNodes, type, expected);
        }

        public static IEnumerable<object[]> HelmasaurKingData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.HelmasaurKingBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.HelmasaurKing,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.HelmasaurKingBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.HelmasaurKing,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.HelmasaurKingBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.HelmasaurKing,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.HelmasaurKingBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.HelmasaurKing,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.HelmasaurKingBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.HelmasaurKing,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.HelmasaurKingBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.HelmasaurKing,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.HelmasaurKingBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.HelmasaurKing,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.HelmasaurKingBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.HelmasaurKing,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> ArrghusData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 1),
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 1),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hookshot, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 3),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.Bow, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.ArrghusBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Arrghus,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MothulaData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0),
                        (ItemType.FireRod, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.MothulaBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Mothula,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BlindData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 1),
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.CaneOfByrna, 0),
                        (ItemType.Cape, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Blind,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> KholdstareData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.Sword, 0),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.KholdstareBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Kholdstare,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> VitreousData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.VitreousBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Vitreous,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.VitreousBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Vitreous,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hammer, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.VitreousBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Vitreous,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.VitreousBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Vitreous,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hammer, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.VitreousBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Vitreous,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.VitreousBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Vitreous,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Hammer, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.VitreousBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Vitreous,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 2),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.VitreousBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Vitreous,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.VitreousBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Vitreous,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 3),
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.VitreousBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Vitreous,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.Hammer, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Bow, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.VitreousBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Vitreous,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TrinexxData =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, false)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 4),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 1),
                        (ItemType.Hammer, 1),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 1),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 3),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.IceRod, 1),
                        (ItemType.Sword, 4),
                        (ItemType.Hammer, 0),
                        (ItemType.Bottle, 0),
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.TrinexxBasic, true)
                    },
                    new (LocationID, int)[0],
                    new (LocationID, int)[0],
                    new RequirementNodeID[0],
                    RequirementType.Trinexx,
                    AccessibilityLevel.Normal
                }
            };
    }
}