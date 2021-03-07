using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements
{
     public class ComplexItemRequirementTests : ComplexItemRequirementTestBase
     {
         [Theory]
         [MemberData(nameof(BonkOverLedgeData))]
         [MemberData(nameof(BumperCaveGapData))]
         [MemberData(nameof(CameraUnlockData))]
         [MemberData(nameof(CurtainsData))]
         [MemberData(nameof(DungeonReviveData))]
         [MemberData(nameof(HoverData))]
         [MemberData(nameof(LaserBridgeData))]
         [MemberData(nameof(MagicBatData))]
         [MemberData(nameof(PedestalData))]
         [MemberData(nameof(RedEyegoreGoriyaData))]
         [MemberData(nameof(TabletData))]
         [MemberData(nameof(TorchData))]
         [MemberData(nameof(ToHHerapotData))]
         [MemberData(nameof(IPIceBreakerData))]
         [MemberData(nameof(TRKeyDoorsToMiddleExitData))]
         [MemberData(nameof(SpikeCaveData))]
         public override void AccessibilityTests(
             ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
             (SequenceBreakType, bool)[] sequenceBreaks, (LocationID, int)[] smallKeys,
             (LocationID, int)[] bigKeys, RequirementNodeID[] accessibleNodes, RequirementType type,
             AccessibilityLevel expected)
         {
             base.AccessibilityTests(
                 modeData, items, prizes, sequenceBreaks, smallKeys, bigKeys, accessibleNodes, type, expected);
         }

         public static IEnumerable<object[]> BonkOverLedgeData =>
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
                         (ItemType.Boots, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BonkOverLedge, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.BonkOverLedge,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Boots, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BonkOverLedge, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.BonkOverLedge,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Boots, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BonkOverLedge, false)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.BonkOverLedge,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Boots, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BonkOverLedge, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.BonkOverLedge,
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
                         (ItemType.Boots, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BonkOverLedge, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.BonkOverLedge,
                     AccessibilityLevel.Normal
                 }
             };

         public static IEnumerable<object[]> BumperCaveGapData =>
             new List<object[]>
             {
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Hookshot, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BumperCaveHookshot, false)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.BumperCaveGap,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Hookshot, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BumperCaveHookshot, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.BumperCaveGap,
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
                         (ItemType.Hookshot, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BumperCaveHookshot, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.BumperCaveGap,
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
                         (ItemType.Hookshot, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BumperCaveHookshot, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.BumperCaveGap,
                     AccessibilityLevel.Normal
                 }
             };

         public static IEnumerable<object[]> CameraUnlockData =>
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
                         (SequenceBreakType.CameraUnlock, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.CameraUnlock,
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
                         (SequenceBreakType.CameraUnlock, false)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.CameraUnlock,
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
                         (SequenceBreakType.CameraUnlock, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.CameraUnlock,
                     AccessibilityLevel.SequenceBreak
                 }
             };

         public static IEnumerable<object[]> CurtainsData =>
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
                     RequirementType.Curtains,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Sword, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Curtains,
                     AccessibilityLevel.Normal
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
                     RequirementType.Curtains,
                     AccessibilityLevel.Normal
                 }
             };

         public static IEnumerable<object[]> DungeonReviveData =>
             new List<object[]>
             {
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[0],
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.DungeonRevive, false)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.DungeonRevive,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[0],
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.DungeonRevive, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.DungeonRevive,
                     AccessibilityLevel.SequenceBreak
                 }
             };

         public static IEnumerable<object[]> HoverData =>
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
                         (SequenceBreakType.Hover, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Hover,
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
                         (SequenceBreakType.Hover, false)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Hover,
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
                         (SequenceBreakType.Hover, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Hover,
                     AccessibilityLevel.SequenceBreak
                 }
             };

         public static IEnumerable<object[]> LaserBridgeData =>
             new List<object[]>
             {
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Cape, 0),
                         (ItemType.CaneOfByrna, 0),
                         (ItemType.Shield, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.TRLaserSkip, false)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.LaserBridge,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Cape, 0),
                         (ItemType.CaneOfByrna, 0),
                         (ItemType.Shield, 2)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.TRLaserSkip, false)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.LaserBridge,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Cape, 0),
                         (ItemType.CaneOfByrna, 0),
                         (ItemType.Shield, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.TRLaserSkip, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.LaserBridge,
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
                         (ItemType.Cape, 0),
                         (ItemType.CaneOfByrna, 0),
                         (ItemType.Shield, 2)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.TRLaserSkip, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.LaserBridge,
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
                         (ItemType.Cape, 0),
                         (ItemType.CaneOfByrna, 0),
                         (ItemType.Shield, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.TRLaserSkip, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.LaserBridge,
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
                         (ItemType.Cape, 1),
                         (ItemType.CaneOfByrna, 0),
                         (ItemType.Shield, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.TRLaserSkip, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.LaserBridge,
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
                         (ItemType.Cape, 0),
                         (ItemType.CaneOfByrna, 1),
                         (ItemType.Shield, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.TRLaserSkip, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.LaserBridge,
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
                         (ItemType.Cape, 0),
                         (ItemType.CaneOfByrna, 0),
                         (ItemType.Shield, 3)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.TRLaserSkip, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.LaserBridge,
                     AccessibilityLevel.Normal
                 }
             };

         public static IEnumerable<object[]> MagicBatData =>
             new List<object[]>
             {
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Mushroom, 1),
                         (ItemType.CaneOfSomaria, 0),
                         (ItemType.Powder, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.FakePowder, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.MagicBat,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Mushroom, 1),
                         (ItemType.CaneOfSomaria, 1),
                         (ItemType.Powder, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.FakePowder, false)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.MagicBat,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Mushroom, 1),
                         (ItemType.CaneOfSomaria, 1),
                         (ItemType.Powder, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.FakePowder, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.MagicBat,
                     AccessibilityLevel.SequenceBreak
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Mushroom, 0),
                         (ItemType.CaneOfSomaria, 0),
                         (ItemType.Powder, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.FakePowder, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.MagicBat,
                     AccessibilityLevel.Normal
                 }
             };

         public static IEnumerable<object[]> PedestalData =>
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
                         (ItemType.Book, 0)
                     },
                     new (PrizeType, int)[]
                     {
                         (PrizeType.Pendant, 0),
                         (PrizeType.GreenPendant, 0)
                     },
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BlindPedestal, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Pedestal,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Advanced
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 0)
                     },
                     new (PrizeType, int)[]
                     {
                         (PrizeType.Pendant, 1),
                         (PrizeType.GreenPendant, 1)
                     },
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BlindPedestal, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Pedestal,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 0)
                     },
                     new (PrizeType, int)[]
                     {
                         (PrizeType.Pendant, 0),
                         (PrizeType.GreenPendant, 0)
                     },
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BlindPedestal, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Pedestal,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 0)
                     },
                     new (PrizeType, int)[]
                     {
                         (PrizeType.Pendant, 1),
                         (PrizeType.GreenPendant, 1)
                     },
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BlindPedestal, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Pedestal,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 0)
                     },
                     new (PrizeType, int)[]
                     {
                         (PrizeType.Pendant, 2),
                         (PrizeType.GreenPendant, 1)
                     },
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BlindPedestal, false)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Pedestal,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Advanced
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 1)
                     },
                     new (PrizeType, int)[]
                     {
                         (PrizeType.Pendant, 0),
                         (PrizeType.GreenPendant, 0)
                     },
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BlindPedestal, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Pedestal,
                     AccessibilityLevel.Inspect
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 1)
                     },
                     new (PrizeType, int)[]
                     {
                         (PrizeType.Pendant, 0),
                         (PrizeType.GreenPendant, 0)
                     },
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BlindPedestal, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Pedestal,
                     AccessibilityLevel.Inspect
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         ItemPlacement = ItemPlacement.Basic
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 0)
                     },
                     new (PrizeType, int)[]
                     {
                         (PrizeType.Pendant, 2),
                         (PrizeType.GreenPendant, 1)
                     },
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BlindPedestal, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Pedestal,
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
                         (ItemType.Book, 0)
                     },
                     new (PrizeType, int)[]
                     {
                         (PrizeType.Pendant, 2),
                         (PrizeType.GreenPendant, 1)
                     },
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BlindPedestal, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Pedestal,
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
                         (ItemType.Book, 1)
                     },
                     new (PrizeType, int)[]
                     {
                         (PrizeType.Pendant, 2),
                         (PrizeType.GreenPendant, 1)
                     },
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.BlindPedestal, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Pedestal,
                     AccessibilityLevel.Normal
                 }
             };

         public static IEnumerable<object[]> RedEyegoreGoriyaData =>
             new List<object[]>
             {
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         EnemyShuffle = false
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Bow, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.RedEyegoreGoriya,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         EnemyShuffle = false
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Bow, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.RedEyegoreGoriya,
                     AccessibilityLevel.Normal
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         EnemyShuffle = true
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.Bow, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.RedEyegoreGoriya,
                     AccessibilityLevel.Normal
                 }
             };

         public static IEnumerable<object[]> TabletData =>
             new List<object[]>
             {
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 0),
                         (ItemType.Sword, 1),
                         (ItemType.Hammer, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Tablet,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 0),
                         (ItemType.Sword, 0),
                         (ItemType.Hammer, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Tablet,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 0),
                         (ItemType.Sword, 3),
                         (ItemType.Hammer, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Tablet,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 1),
                         (ItemType.Sword, 1),
                         (ItemType.Hammer, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Tablet,
                     AccessibilityLevel.Inspect
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 1),
                         (ItemType.Sword, 1),
                         (ItemType.Hammer, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Tablet,
                     AccessibilityLevel.Inspect
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 1),
                         (ItemType.Sword, 0),
                         (ItemType.Hammer, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Tablet,
                     AccessibilityLevel.Inspect
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 1),
                         (ItemType.Sword, 2),
                         (ItemType.Hammer, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Tablet,
                     AccessibilityLevel.Inspect
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 1),
                         (ItemType.Sword, 0),
                         (ItemType.Hammer, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Tablet,
                     AccessibilityLevel.Normal
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Book, 1),
                         (ItemType.Sword, 3),
                         (ItemType.Hammer, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Tablet,
                     AccessibilityLevel.Normal
                 }
             };

         public static IEnumerable<object[]> TorchData =>
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
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Torch,
                     AccessibilityLevel.Inspect
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Boots, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.Torch,
                     AccessibilityLevel.Normal
                 }
             };

         public static IEnumerable<object[]> ToHHerapotData =>
             new List<object[]>
             {
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Hookshot, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.ToHHerapot, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.ToHHerapot,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Hookshot, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.ToHHerapot, false)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.ToHHerapot,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.Hookshot, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.ToHHerapot, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.ToHHerapot,
                     AccessibilityLevel.SequenceBreak
                 }
             };

         public static IEnumerable<object[]> IPIceBreakerData =>
             new List<object[]>
             {
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.CaneOfSomaria, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.IPIceBreaker, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.IPIceBreaker,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.CaneOfSomaria, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.IPIceBreaker, false)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.IPIceBreaker,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData(),
                     new (ItemType, int)[]
                     {
                         (ItemType.CaneOfSomaria, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[]
                     {
                         (SequenceBreakType.IPIceBreaker, true)
                     },
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.IPIceBreaker,
                     AccessibilityLevel.SequenceBreak
                 }
             };

         public static IEnumerable<object[]> TRKeyDoorsToMiddleExitData =>
             new List<object[]>
             {
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = true,
                         GenericKeys = false
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 0),
                         (ItemType.SmallKey, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 0)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = true,
                         GenericKeys = false
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 1),
                         (ItemType.SmallKey, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 0)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = true,
                         GenericKeys = false
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 0),
                         (ItemType.SmallKey, 2)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 0)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = true,
                         GenericKeys = false
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 0),
                         (ItemType.SmallKey, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 1)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = true,
                         GenericKeys = true
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 0),
                         (ItemType.SmallKey, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 0)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = true,
                         GenericKeys = true
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 1),
                         (ItemType.SmallKey, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 0)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = true,
                         GenericKeys = true
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 0),
                         (ItemType.SmallKey, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 1)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.None
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = false,
                         GenericKeys = false
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 0),
                         (ItemType.SmallKey, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 0)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.SequenceBreak
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = false,
                         GenericKeys = false
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 0),
                         (ItemType.SmallKey, 2)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 2)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.SequenceBreak
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = false,
                         GenericKeys = false
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 1),
                         (ItemType.SmallKey, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 0)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.Normal
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = true,
                         GenericKeys = false
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 0),
                         (ItemType.SmallKey, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 2)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.Normal
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = true,
                         GenericKeys = true
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 0),
                         (ItemType.SmallKey, 2)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 0)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.Normal
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = true,
                         GenericKeys = true
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 0),
                         (ItemType.SmallKey, 1)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 1)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.Normal
                 },
                 new object[]
                 {
                     new ModeSaveData()
                     {
                         SmallKeyShuffle = true,
                         GenericKeys = true
                     },
                     new (ItemType, int)[]
                     {
                         (ItemType.FireRod, 0),
                         (ItemType.SmallKey, 0)
                     },
                     new (PrizeType, int)[0],
                     new (SequenceBreakType, bool)[0],
                     new (LocationID, int)[]
                     {
                         (LocationID.TurtleRock, 2)
                     },
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.TRKeyDoorsToMiddleExit,
                     AccessibilityLevel.Normal
                 }
             };

         public static IEnumerable<object[]> SpikeCaveData =>
             new List<object[]>
             {
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
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.SpikeCave,
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
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.SpikeCave,
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
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.SpikeCave,
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
                     new (LocationID, int)[0],
                     new (LocationID, int)[0],
                     new RequirementNodeID[0],
                     RequirementType.SpikeCave,
                     AccessibilityLevel.Normal
                 }
             };
     }
}
