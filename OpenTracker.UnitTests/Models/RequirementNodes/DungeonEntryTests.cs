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
    public class DungeonEntryTests : RequirementNodeTestBase
    {
        [Theory]
        [MemberData(nameof(EntranceDungeonToHCSanctuaryEntry))]
        [MemberData(nameof(LightWorldNotBunnyOrSuperBunnyMirrorToHCSanctuaryEntry))]
        [MemberData(nameof(EntranceDungeonToHCFrontEntry))]
        [MemberData(nameof(LightWorldNotBunnyOrDungeonReviveToHCFrontEntry))]
        [MemberData(nameof(EntranceDungeonToHCBackEntry))]
        [MemberData(nameof(EscapeGraveToHCBackEntry))]
        [MemberData(nameof(EntranceDungeonToATEntry))]
        [MemberData(nameof(AgahnimTowerEntranceToATEntry))]
        [MemberData(nameof(GanonsTowerEntranceToATEntry))]
        [MemberData(nameof(EntranceDungeonToEPEntry))]
        [MemberData(nameof(LightWorldNotBunnyOrDungeonReviveToEPEntry))]
        [MemberData(nameof(EntranceDungeonToDPFrontEntry))]
        [MemberData(nameof(DesertPalaceFrontEntranceToDPFrontEntry))]
        [MemberData(nameof(EntranceDungeonToDPLeftEntry))]
        [MemberData(nameof(DesertLedgeToDPLeftEntry))]
        [MemberData(nameof(EntranceDungeonToDPBackEntry))]
        [MemberData(nameof(DesertBackToDPBackEntry))]
        [MemberData(nameof(EntranceDungeonToToHEntry))]
        [MemberData(nameof(DeathMountainWestTopNotBunnyToToHEntry))]
        [MemberData(nameof(DeathMountainWestTopToToHEntry))]
        [MemberData(nameof(EntranceDungeonToPoDEntry))]
        [MemberData(nameof(DarkWorldEastNotBunnyToPoDEntry))]
        [MemberData(nameof(EntranceDungeonToSPEntry))]
        [MemberData(nameof(LightWorldInvertedNotBunnyToSPEntry))]
        [MemberData(nameof(DarkWorldSouthStandardOpenNotBunnyToSPEntry))]
        [MemberData(nameof(DarkWorldWestNotBunnyToSWFrontEntry))]
        [MemberData(nameof(DarkWorldWestToSWFrontEntry))]
        [MemberData(nameof(EntranceDungeonToSWBackEntry))]
        [MemberData(nameof(SkullWoodsBackToSWBackEntry))]
        [MemberData(nameof(EntranceDungeonToTTEntry))]
        [MemberData(nameof(DarkWorldWestNotBunnyToTTEntry))]
        [MemberData(nameof(EntranceDungeonToIPEntry))]
        [MemberData(nameof(IcePalaceIslandToIPEntry))]
        [MemberData(nameof(EntranceDungeonToMMEntry))]
        [MemberData(nameof(MiseryMireEntranceToMMEntry))]
        [MemberData(nameof(EntranceDungeonToTRFrontEntry))]
        [MemberData(nameof(TurtleRockFrontEntranceToTRFrontEntry))]
        [MemberData(nameof(TRFrontEntryToTRFrontEntryStandardOpen))]
        [MemberData(nameof(TRFrontEntryStandardOpenToTRFrontEntryStandardOpenNonEntrance))]
        [MemberData(nameof(TRFrontEntryStandardOpenNonEntranceToTRFrontToKeyDoors))]
        [MemberData(nameof(TRFrontToKeyDoorsToTRKeyDoorsToMiddleExit))]
        [MemberData(nameof(EntranceDungeonToTRMiddleEntry))]
        [MemberData(nameof(TurtleRockTunnelToTRMiddleEntry))]
        [MemberData(nameof(EntranceDungeonToTRBackEntry))]
        [MemberData(nameof(TurtleRockSafetyDoorToTRBackEntry))]
        [MemberData(nameof(EntranceDungeonToGTEntry))]
        [MemberData(nameof(AgahnimTowerEntranceToGTEntry))]
        [MemberData(nameof(GanonsTowerEntranceStandardOpenToGTEntry))]
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
        }
    
        public static IEnumerable<object[]> EntranceDungeonToHCSanctuaryEntry =>
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
                    new RequirementNodeID[0],
                    RequirementNodeID.HCSanctuaryEntry,
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
                    new RequirementNodeID[0],
                    RequirementNodeID.HCSanctuaryEntry,
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
                    new[]
                    {
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.HCSanctuaryEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunnyOrSuperBunnyMirrorToHCSanctuaryEntry =>
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
                    new RequirementNodeID[0],
                    RequirementNodeID.HCSanctuaryEntry,
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
                        RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror
                    },
                    RequirementNodeID.HCSanctuaryEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToHCFrontEntry =>
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
                    new RequirementNodeID[0],
                    RequirementNodeID.HCFrontEntry,
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
                    new RequirementNodeID[0],
                    RequirementNodeID.HCFrontEntry,
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
                    new[]
                    {
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.HCFrontEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunnyOrDungeonReviveToHCFrontEntry =>
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
                    new RequirementNodeID[0],
                    RequirementNodeID.HCFrontEntry,
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
                        RequirementNodeID.LightWorldNotBunnyOrDungeonRevive
                    },
                    RequirementNodeID.HCFrontEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToHCBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.HCBackEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.HCBackEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EscapeGraveToHCBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.HCBackEntry,
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
                        RequirementNodeID.EscapeGrave
                    },
                    RequirementNodeID.HCBackEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToATEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.ATEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.ATEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> AgahnimTowerEntranceToATEntry =>
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
                        RequirementNodeID.AgahnimTowerEntrance
                    },
                    RequirementNodeID.ATEntry,
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
                        RequirementNodeID.AgahnimTowerEntrance
                    },
                    RequirementNodeID.ATEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> GanonsTowerEntranceToATEntry =>
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
                        RequirementNodeID.GanonsTowerEntrance
                    },
                    RequirementNodeID.ATEntry,
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
                        RequirementNodeID.GanonsTowerEntrance
                    },
                    RequirementNodeID.ATEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToEPEntry =>
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
                    new RequirementNodeID[0],
                    RequirementNodeID.EPEntry,
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
                    new RequirementNodeID[0],
                    RequirementNodeID.EPEntry,
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
                    new[]
                    {
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.EPEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunnyOrDungeonReviveToEPEntry =>
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
                    new RequirementNodeID[0],
                    RequirementNodeID.EPEntry,
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
                        RequirementNodeID.LightWorldNotBunnyOrDungeonRevive
                    },
                    RequirementNodeID.EPEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToDPFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DPFrontEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.DPFrontEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DesertPalaceFrontEntranceToDPFrontEntry =>
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.DPFrontEntry,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DesertPalaceFrontEntrance
                    },
                    RequirementNodeID.DPFrontEntry,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DesertPalaceFrontEntrance
                    },
                    RequirementNodeID.DPFrontEntry,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DesertPalaceFrontEntrance
                    },
                    RequirementNodeID.DPFrontEntry,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DesertPalaceFrontEntrance
                    },
                    RequirementNodeID.DPFrontEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToDPLeftEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DPLeftEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.DPLeftEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DesertLedgeToDPLeftEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DPLeftEntry,
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
                        RequirementNodeID.DesertLedge
                    },
                    RequirementNodeID.DPLeftEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToDPBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DPBackEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.DPBackEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DesertBackToDPBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DPBackEntry,
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
                        RequirementNodeID.DesertBack
                    },
                    RequirementNodeID.DPBackEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToToHEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.ToHEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.ToHEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainWestTopNotBunnyToToHEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.ToHEntry,
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
                        RequirementNodeID.DeathMountainWestTopNotBunny
                    },
                    RequirementNodeID.ToHEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainWestTopToToHEntry =>
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false)
                    },
                    new[]
                    {
                        RequirementNodeID.DeathMountainWestTop
                    },
                    RequirementNodeID.ToHEntry,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DeathMountainWestTop
                    },
                    RequirementNodeID.ToHEntry,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new[]
                    {
                        RequirementNodeID.DeathMountainWestTop
                    },
                    RequirementNodeID.ToHEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToPoDEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.PoDEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.PoDEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldEastNotBunnyToPoDEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.PoDEntry,
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
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    RequirementNodeID.PoDEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToSPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.SPEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.SPEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldInvertedNotBunnyToSPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LightWorldInvertedNotBunny
                    },
                    RequirementNodeID.SPEntry,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.LightWorldInvertedNotBunny
                    },
                    RequirementNodeID.SPEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthStandardOpenNotBunnyToSPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthStandardOpenNotBunny
                    },
                    RequirementNodeID.SPEntry,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.DarkWorldSouthStandardOpenNotBunny
                    },
                    RequirementNodeID.SPEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldWestNotBunnyToSWFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.SWFrontEntry,
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
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.SWFrontEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldWestToSWFrontEntry =>
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
                    new[]
                    {
                        RequirementNodeID.DarkWorldWest
                    },
                    RequirementNodeID.SWFrontEntry,
                    false,
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
                    new[]
                    {
                        RequirementNodeID.DarkWorldWest
                    },
                    RequirementNodeID.SWFrontEntry,
                    false,
                    AccessibilityLevel.SequenceBreak
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToSWBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.SWBackEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.SWBackEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> SkullWoodsBackToSWBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.SWBackEntry,
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
                        RequirementNodeID.SkullWoodsBack
                    },
                    RequirementNodeID.SWBackEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToTTEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.TTEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.TTEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldWestNotBunnyToTTEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.TTEntry,
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
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.TTEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToIPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.IPEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.IPEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> IcePalaceIslandToIPEntry =>
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    RequirementNodeID.IPEntry,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    RequirementNodeID.IPEntry,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.IPEntry,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    RequirementNodeID.IPEntry,
                    false,
                    AccessibilityLevel.Normal
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    RequirementNodeID.IPEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToMMEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.MMEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.MMEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> MiseryMireEntranceToMMEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.MMEntry,
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
                        RequirementNodeID.MiseryMireEntrance
                    },
                    RequirementNodeID.MMEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToTRFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.TRFrontEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.TRFrontEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> TurtleRockFrontEntranceToTRFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.TRFrontEntry,
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
                        RequirementNodeID.TurtleRockFrontEntrance
                    },
                    RequirementNodeID.TRFrontEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> TRFrontEntryToTRFrontEntryStandardOpen =>
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
                        RequirementNodeID.TRFrontEntry
                    },
                    RequirementNodeID.TRFrontEntryStandardOpen,
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
                        RequirementNodeID.TRFrontEntry
                    },
                    RequirementNodeID.TRFrontEntryStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> TRFrontEntryStandardOpenToTRFrontEntryStandardOpenNonEntrance =>
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
                        RequirementNodeID.TRFrontEntryStandardOpen
                    },
                    RequirementNodeID.TRFrontEntryStandardOpenEntranceNone,
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
                        RequirementNodeID.TRFrontEntryStandardOpen
                    },
                    RequirementNodeID.TRFrontEntryStandardOpenEntranceNone,
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
                        RequirementNodeID.TRFrontEntryStandardOpen
                    },
                    RequirementNodeID.TRFrontEntryStandardOpenEntranceNone,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> TRFrontEntryStandardOpenNonEntranceToTRFrontToKeyDoors =>
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
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontEntryStandardOpenEntranceNone
                    },
                    RequirementNodeID.TRFrontToKeyDoors,
                    false,
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
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontEntryStandardOpenEntranceNone
                    },
                    RequirementNodeID.TRFrontToKeyDoors,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> TRFrontToKeyDoorsToTRKeyDoorsToMiddleExit =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = false,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = false,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = false,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 2),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = false,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = false,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = false,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = false,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 3),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = false,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 2)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 2)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        GenericKeys = false,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        GenericKeys = false,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 2),
                        (ItemType.TRSmallKey, 2)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        GenericKeys = false,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        GenericKeys = false,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 3),
                        (ItemType.TRSmallKey, 3)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        GenericKeys = false,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = false,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 2)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 2),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.TRSmallKey, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = false
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 2)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        GenericKeys = false,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 1),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = false,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 3)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 3),
                        (ItemType.TRSmallKey, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 2),
                        (ItemType.TRSmallKey, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 1),
                        (ItemType.TRSmallKey, 2)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        GenericKeys = true,
                        KeyDropShuffle = true
                    },
                    new[]
                    {
                        (ItemType.FireRod, 0),
                        (ItemType.SmallKey, 0),
                        (ItemType.TRSmallKey, 3)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToTRMiddleEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.TRMiddleEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.TRMiddleEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> TurtleRockTunnelToTRMiddleEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.TRMiddleEntry,
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
                        RequirementNodeID.TurtleRockTunnel
                    },
                    RequirementNodeID.TRMiddleEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToTRBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.TRBackEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.TRBackEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> TurtleRockSafetyDoorToTRBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.TRBackEntry,
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
                        RequirementNodeID.TurtleRockSafetyDoor
                    },
                    RequirementNodeID.TRBackEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceDungeonToGTEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.GTEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    RequirementNodeID.GTEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> AgahnimTowerEntranceToGTEntry =>
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
                        RequirementNodeID.AgahnimTowerEntrance
                    },
                    RequirementNodeID.GTEntry,
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
                        RequirementNodeID.AgahnimTowerEntrance
                    },
                    RequirementNodeID.GTEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> GanonsTowerEntranceStandardOpenToGTEntry =>
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.GTEntry,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, false)
                    },
                    new[]
                    {
                        RequirementNodeID.GanonsTowerEntranceStandardOpen
                    },
                    RequirementNodeID.GTEntry,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new[]
                    {
                        RequirementNodeID.GanonsTowerEntranceStandardOpen
                    },
                    RequirementNodeID.GTEntry,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new[]
                    {
                        RequirementNodeID.GanonsTowerEntranceStandardOpen
                    },
                    RequirementNodeID.GTEntry,
                    false,
                    AccessibilityLevel.Normal
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new[]
                    {
                        RequirementNodeID.GanonsTowerEntranceStandardOpen
                    },
                    RequirementNodeID.GTEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    }
}
