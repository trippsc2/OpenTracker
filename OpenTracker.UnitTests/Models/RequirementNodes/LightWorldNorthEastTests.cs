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
    public class LightWorldNorthEastTests : RequirementNodeTestBase
    {
        [Theory]
        [MemberData(nameof(LightWorldToHyruleCastleTop))]
        [MemberData(nameof(DarkWorldEastToHyruleCastleTop))]
        [MemberData(nameof(HyruleCastleTopToHyruleCastleTopInverted))]
        [MemberData(nameof(HyruleCastleTopToHyruleCastleTopStandardOpen))]
        [MemberData(nameof(HyruleCastleTopInvertedToAgahnimTowerEntrance))]
        [MemberData(nameof(HyruleCastleTopStandardOpenToAgahnimTowerEntrance))]
        [MemberData(nameof(HyruleCastleTopInvertedToGanonHole))]
        [MemberData(nameof(DarkWorldEastStandardOpenToGanonHole))]
        [MemberData(nameof(LWLakeHyliaFlippersToZoraArea))]
        [MemberData(nameof(LWWitchAreaNotBunnyToZoraArea))]
        [MemberData(nameof(CatfishAreaToZoraArea))]
        [MemberData(nameof(LWLakeHyliaFakeFlippersToZoraArea))]
        [MemberData(nameof(LWLakeHyliaWaterWalkToZoraArea))]
        [MemberData(nameof(ZoraAreaToZoraLedge))]
        [MemberData(nameof(LWLakeHyliaFlippersToWaterfallFairy))]
        [MemberData(nameof(LWLakeHyliaFakeFlippersToWaterfallFairy))]
        [MemberData(nameof(LWLakeHyliaWaterWalkToWaterfallFairy))]
        [MemberData(nameof(WaterfallFairyToWaterfallFairyNotBunny))]
        [MemberData(nameof(LightWorldNotBunnyToLWWitchArea))]
        [MemberData(nameof(ZoraAreaToLWWitchArea))]
        [MemberData(nameof(LWWitchAreaToLWWitchAreaNotBunny))]
        [MemberData(nameof(LWWitchAreaToWitchsHut))]
        [MemberData(nameof(LightWorldToSahasrahla))]
        [MemberData(nameof(LightWorldHammerToLWEastPortal))]
        [MemberData(nameof(DWEastPortalInvertedToLWEastPortal))]
        [MemberData(nameof(LWEastPortalToLWEastPortalStandardOpen))]
        [MemberData(nameof(LWEastPortalToLWEastPortalNotBunny))]
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
        }
    
        public static IEnumerable<object[]> LightWorldToHyruleCastleTop =>
            new List<object[]>
            {
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.HyruleCastleTop,
                    false,
                    AccessibilityLevel.None
                },
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.HyruleCastleTop,
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.HyruleCastleTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldEastToHyruleCastleTop =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldEast
                    },
                    RequirementNodeID.HyruleCastleTop,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldEast
                    },
                    RequirementNodeID.HyruleCastleTop,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldEast
                    },
                    RequirementNodeID.HyruleCastleTop,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> HyruleCastleTopToHyruleCastleTopInverted =>
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
                        RequirementNodeID.HyruleCastleTop
                    },
                    RequirementNodeID.HyruleCastleTopInverted,
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
                        RequirementNodeID.HyruleCastleTop
                    },
                    RequirementNodeID.HyruleCastleTopInverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> HyruleCastleTopToHyruleCastleTopStandardOpen =>
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
                        RequirementNodeID.HyruleCastleTop
                    },
                    RequirementNodeID.HyruleCastleTopStandardOpen,
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
                        RequirementNodeID.HyruleCastleTop
                    },
                    RequirementNodeID.HyruleCastleTopStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> HyruleCastleTopInvertedToAgahnimTowerEntrance =>
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
                        RequirementNodeID.HyruleCastleTopInverted
                    },
                    RequirementNodeID.AgahnimTowerEntrance,
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
                        RequirementNodeID.HyruleCastleTopInverted
                    },
                    RequirementNodeID.AgahnimTowerEntrance,
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
                        RequirementNodeID.HyruleCastleTopInverted
                    },
                    RequirementNodeID.AgahnimTowerEntrance,
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
                        RequirementNodeID.HyruleCastleTopInverted
                    },
                    RequirementNodeID.AgahnimTowerEntrance,
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
                        RequirementNodeID.HyruleCastleTopInverted
                    },
                    RequirementNodeID.AgahnimTowerEntrance,
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
                        RequirementNodeID.HyruleCastleTopInverted
                    },
                    RequirementNodeID.AgahnimTowerEntrance,
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
                        RequirementNodeID.HyruleCastleTopInverted
                    },
                    RequirementNodeID.AgahnimTowerEntrance,
                    true,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> HyruleCastleTopStandardOpenToAgahnimTowerEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Hammer, 1),
                        (ItemType.Cape, 0),
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.HyruleCastleTopStandardOpen
                    },
                    RequirementNodeID.AgahnimTowerEntrance,
                    false,
                    AccessibilityLevel.None
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
                    new[]
                    {
                        RequirementNodeID.HyruleCastleTopStandardOpen
                    },
                    RequirementNodeID.AgahnimTowerEntrance,
                    false,
                    AccessibilityLevel.Normal
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
                    new[]
                    {
                        RequirementNodeID.HyruleCastleTopStandardOpen
                    },
                    RequirementNodeID.AgahnimTowerEntrance,
                    false,
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
                    new[]
                    {
                        RequirementNodeID.HyruleCastleTopStandardOpen
                    },
                    RequirementNodeID.AgahnimTowerEntrance,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> HyruleCastleTopInvertedToGanonHole =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga2, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.HyruleCastleTopInverted
                    },
                    RequirementNodeID.GanonHole,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga2, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.HyruleCastleTopInverted
                    },
                    RequirementNodeID.GanonHole,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldEastStandardOpenToGanonHole =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga2, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastStandardOpen
                    },
                    RequirementNodeID.GanonHole,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga2, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldEastStandardOpen
                    },
                    RequirementNodeID.GanonHole,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFlippersToZoraArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.ZoraArea,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFlippers
                    },
                    RequirementNodeID.ZoraArea,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWWitchAreaNotBunnyToZoraArea =>
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
                        RequirementNodeID.LWWitchAreaNotBunny
                    },
                    RequirementNodeID.ZoraArea,
                    false,
                    AccessibilityLevel.SequenceBreak
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
                        RequirementNodeID.LWWitchAreaNotBunny
                    },
                    RequirementNodeID.ZoraArea,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> CatfishAreaToZoraArea =>
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.CatfishArea
                    },
                    RequirementNodeID.ZoraArea,
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
                        RequirementNodeID.CatfishArea
                    },
                    RequirementNodeID.ZoraArea,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.CatfishArea
                    },
                    RequirementNodeID.ZoraArea,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFakeFlippersToZoraArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.ZoraArea,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.ZoraArea,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaWaterWalkToZoraArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.ZoraArea,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.ZoraArea,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> ZoraAreaToZoraLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, false)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, false)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, false),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 1),
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new[]
                    {
                        (ItemType.Flippers, 0),
                        (ItemType.Bow, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.IceRod, 0),
                        (ItemType.Boots, 1)
                    },
                    new (PrizeType, int)[0],
                    new[]
                    {
                        (SequenceBreakType.FakeFlippersSplashDeletion, true),
                        (SequenceBreakType.WaterWalk, true)
                    },
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.ZoraLedge,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFlippersToWaterfallFairy =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.WaterfallFairy,
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
                        RequirementNodeID.LWLakeHyliaFlippers
                    },
                    RequirementNodeID.WaterfallFairy,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaFakeFlippersToWaterfallFairy =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.WaterfallFairy,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWLakeHyliaFakeFlippers
                    },
                    RequirementNodeID.WaterfallFairy,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWLakeHyliaWaterWalkToWaterfallFairy =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.WaterfallFairy,
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
                        RequirementNodeID.LWLakeHyliaWaterWalk
                    },
                    RequirementNodeID.WaterfallFairy,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> WaterfallFairyToWaterfallFairyNotBunny =>
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
                    new RequirementNodeID[0],
                    RequirementNodeID.WaterfallFairyNotBunny,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.WaterfallFairy
                    },
                    RequirementNodeID.WaterfallFairyNotBunny,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.WaterfallFairy
                    },
                    RequirementNodeID.WaterfallFairyNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.WaterfallFairy
                    },
                    RequirementNodeID.WaterfallFairyNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunnyToLWWitchArea =>
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
                    new RequirementNodeID[0],
                    RequirementNodeID.LWWitchArea,
                    false,
                    AccessibilityLevel.Normal
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
                    new RequirementNodeID[0],
                    RequirementNodeID.LWWitchArea,
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
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWWitchArea,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> ZoraAreaToLWWitchArea =>
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
                        (ItemType.Gloves, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.LWWitchArea,
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
                        (ItemType.Gloves, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.ZoraArea
                    },
                    RequirementNodeID.LWWitchArea,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWWitchAreaToLWWitchAreaNotBunny =>
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWWitchArea
                    },
                    RequirementNodeID.LWWitchAreaNotBunny,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LWWitchAreaNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWWitchArea
                    },
                    RequirementNodeID.LWWitchAreaNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWWitchAreaToWitchsHut =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Mushroom, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWWitchArea
                    },
                    RequirementNodeID.WitchsHut,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Mushroom, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWWitchArea
                    },
                    RequirementNodeID.WitchsHut,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldToSahasrahla =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.GreenPendant, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Sahasrahla,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.GreenPendant, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Sahasrahla,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldHammerToLWEastPortal =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LWEastPortal,
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
                        RequirementNodeID.LightWorldHammer
                    },
                    RequirementNodeID.LWEastPortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWEastPortalInvertedToLWEastPortal =>
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
                        RequirementNodeID.DWEastPortalInverted
                    },
                    RequirementNodeID.LWEastPortal,
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
                        RequirementNodeID.DWEastPortalInverted
                    },
                    RequirementNodeID.LWEastPortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWEastPortalToLWEastPortalStandardOpen =>
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
                        RequirementNodeID.LWEastPortal
                    },
                    RequirementNodeID.LWEastPortalStandardOpen,
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
                        RequirementNodeID.LWEastPortal
                    },
                    RequirementNodeID.LWEastPortalStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWEastPortalToLWEastPortalNotBunny =>
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LWEastPortalNotBunny,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWEastPortal
                    },
                    RequirementNodeID.LWEastPortalNotBunny,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWEastPortal
                    },
                    RequirementNodeID.LWEastPortalNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LWEastPortal
                    },
                    RequirementNodeID.LWEastPortalNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    }
}
