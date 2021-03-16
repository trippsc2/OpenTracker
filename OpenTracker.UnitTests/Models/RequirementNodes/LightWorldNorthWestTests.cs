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
    public class LightWorldNorthWestTests : RequirementNodeTestBase
    {
        [Theory]
        [MemberData(nameof(LightWorld_To_Pedestal))]
        [MemberData(nameof(LightWorldDash_To_LumberjackCaveHole))]
        [MemberData(nameof(LightWorldLift1_To_DeathMountainEntry))]
        [MemberData(nameof(BumperCaveEntry_To_DeathMountainEntry))]
        [MemberData(nameof(DeathMountainEntry_To_DeathMountainEntryNonEntrance))]
        [MemberData(nameof(DeathMountainEntryNonEntrance_To_DeathMountainEntryCave))]
        [MemberData(nameof(DeathMountainWestBottomNonEntrance_To_DeathMountainEntryCave))]
        [MemberData(nameof(BumperCaveEntryNonEntrance_To_DeathMountainEntryCave))]
        [MemberData(nameof(DarkDeathMountainWestBottomNonEntrance_To_DeathMountainEntryCave))]
        [MemberData(nameof(DeathMountainEntryCave_To_DeathMountainEntryCaveDark))]
        [MemberData(nameof(DeathMountainExitCaveDark_To_DeathMountainExit))]
        [MemberData(nameof(BumperCaveBack_To_DeathMountainExit))]
        [MemberData(nameof(BumperCaveTop_To_DeathMountainExit))]
        [MemberData(nameof(DeathMountainExit_To_DeathMountainExitNonEntrance))]
        [MemberData(nameof(DeathMountainExitNonEntrance_To_DeathMountainExitCave))]
        [MemberData(nameof(DeathMountainWestBottomNonEntrance_To_DeathMountainExitCave))]
        [MemberData(nameof(DeathMountainExitCave_To_DeathMountainExitCaveDark))]
        [MemberData(nameof(LightWorldNotBunny_To_LWKakarikoPortal))]
        [MemberData(nameof(LightWorldHammer_To_LWKakarikoPortal))]
        [MemberData(nameof(DWKakarikoPortalInverted_To_LWKakarikoPortal))]
        [MemberData(nameof(LWKakarikoPortal_To_LWKakarikoPortalStandardOpen))]
        [MemberData(nameof(LWKakarikoPortal_To_LWKakarikoPortalNotBunny))]
        [MemberData(nameof(LightWorld_To_SickKid))]
        [MemberData(nameof(LightWorldNotBunny_To_GrassHouse))]
        [MemberData(nameof(LightWorldNotBunny_To_BombHut))]
        [MemberData(nameof(LightWorldHammer_To_MagicBatLedge))]
        [MemberData(nameof(HammerPegsArea_To_MagicBatLedge))]
        [MemberData(nameof(MagicBatLedge_To_MagicBat))]
        [MemberData(nameof(LightWorldNotBunny_To_RaceGameLedge))]
        [MemberData(nameof(DarkWorldSouthMirror_To_RaceGameLedge))]
        [MemberData(nameof(RaceGameLedge_To_RaceGameLedgeNotBunny))]
        [MemberData(nameof(LightWorldNotBunny_To_LWGraveyard))]
        [MemberData(nameof(LWGraveyardLedge_To_LWGraveyard))]
        [MemberData(nameof(KingsTombNotBunny_To_LWGraveyard))]
        [MemberData(nameof(LWGraveyard_To_LWGraveyardNotBunny))]
        [MemberData(nameof(LWGraveyardNotBunny_To_LWGraveyardLedge))]
        [MemberData(nameof(DWGraveyardMirror_To_LWGraveyardLedge))]
        [MemberData(nameof(LWGraveyardNotBunny_To_EscapeGrave))]
        [MemberData(nameof(LWGraveyardNotBunny_To_KingsTomb))]
        [MemberData(nameof(DWGraveyardMirror_To_KingsTomb))]
        [MemberData(nameof(KingsTomb_To_KingsTombNotBunny))]
        [MemberData(nameof(KingsTombNotBunny_To_KingsTombGrave))]
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
        }
    
        public static IEnumerable<object[]> LightWorld_To_Pedestal =>
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
                        (PrizeType.GreenPendant, 1),
                        (PrizeType.Pendant, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindPedestal, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Pedestal,
                    false,
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
                        (PrizeType.GreenPendant, 1),
                        (PrizeType.Pendant, 1)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindPedestal, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Pedestal,
                    false,
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
                        (PrizeType.GreenPendant, 1),
                        (PrizeType.Pendant, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindPedestal, false)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Pedestal,
                    false,
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
                        (PrizeType.GreenPendant, 0),
                        (PrizeType.Pendant, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindPedestal, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Pedestal,
                    false,
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
                        (PrizeType.GreenPendant, 0),
                        (PrizeType.Pendant, 0)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindPedestal, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Pedestal,
                    false,
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
                        (PrizeType.GreenPendant, 1),
                        (PrizeType.Pendant, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindPedestal, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Pedestal,
                    false,
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
                        (PrizeType.GreenPendant, 1),
                        (PrizeType.Pendant, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindPedestal, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Pedestal,
                    false,
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
                        (PrizeType.GreenPendant, 1),
                        (PrizeType.Pendant, 2)
                    },
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BlindPedestal, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Pedestal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldDash_To_LumberjackCaveHole =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga1, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldDash
                    },
                    RequirementNodeID.LumberjackCaveHole,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga1, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldDash
                    },
                    RequirementNodeID.LumberjackCaveHole,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldLift1_To_DeathMountainEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DeathMountainEntry,
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
                        RequirementNodeID.LightWorldLift1
                    },
                    RequirementNodeID.DeathMountainEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> BumperCaveEntry_To_DeathMountainEntry =>
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
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCaveEntry
                    },
                    RequirementNodeID.DeathMountainEntry,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCaveEntry
                    },
                    RequirementNodeID.DeathMountainEntry,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCaveEntry
                    },
                    RequirementNodeID.DeathMountainEntry,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainEntry_To_DeathMountainEntryNonEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEntry
                    },
                    RequirementNodeID.DeathMountainEntryNonEntrance,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEntry
                    },
                    RequirementNodeID.DeathMountainEntryNonEntrance,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEntry
                    },
                    RequirementNodeID.DeathMountainEntryNonEntrance,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainEntryNonEntrance_To_DeathMountainEntryCave =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEntryNonEntrance
                    },
                    RequirementNodeID.DeathMountainEntryCave,
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
                    RequirementNodeID.DeathMountainEntryCave,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEntryNonEntrance
                    },
                    RequirementNodeID.DeathMountainEntryCave,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainWestBottomNonEntrance_To_DeathMountainEntryCave =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestBottomNonEntrance
                    },
                    RequirementNodeID.DeathMountainEntryCave,
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
                    RequirementNodeID.DeathMountainEntryCave,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestBottomNonEntrance
                    },
                    RequirementNodeID.DeathMountainEntryCave,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> BumperCaveEntryNonEntrance_To_DeathMountainEntryCave =>
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
                        RequirementNodeID.BumperCaveEntryNonEntrance
                    },
                    RequirementNodeID.DeathMountainEntryCave,
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
                    new RequirementNodeID[0],
                    RequirementNodeID.DeathMountainEntryCave,
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
                        RequirementNodeID.BumperCaveEntryNonEntrance
                    },
                    RequirementNodeID.DeathMountainEntryCave,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkDeathMountainWestBottomNonEntrance_To_DeathMountainEntryCave =>
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
                        RequirementNodeID.DarkDeathMountainWestBottomNonEntrance
                    },
                    RequirementNodeID.DeathMountainEntryCave,
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
                    new RequirementNodeID[0],
                    RequirementNodeID.DeathMountainEntryCave,
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
                        RequirementNodeID.DarkDeathMountainWestBottomNonEntrance
                    },
                    RequirementNodeID.DeathMountainEntryCave,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainEntryCave_To_DeathMountainEntryCaveDark =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.DeathMountainEntryCaveDark,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, false)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEntryCave
                    },
                    RequirementNodeID.DeathMountainEntryCaveDark,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEntryCave
                    },
                    RequirementNodeID.DeathMountainEntryCaveDark,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainEntry, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEntryCave
                    },
                    RequirementNodeID.DeathMountainEntryCaveDark,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainExitCaveDark_To_DeathMountainExit =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExitCaveDark
                    },
                    RequirementNodeID.DeathMountainExit,
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
                    RequirementNodeID.DeathMountainExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExitCaveDark
                    },
                    RequirementNodeID.DeathMountainExit,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> BumperCaveBack_To_DeathMountainExit =>
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
                        RequirementNodeID.BumperCaveBack
                    },
                    RequirementNodeID.DeathMountainExit,
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
                    new RequirementNodeID[0],
                    RequirementNodeID.DeathMountainExit,
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
                        RequirementNodeID.BumperCaveBack
                    },
                    RequirementNodeID.DeathMountainExit,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> BumperCaveTop_To_DeathMountainExit =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCaveTop
                    },
                    RequirementNodeID.DeathMountainExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCaveTop
                    },
                    RequirementNodeID.DeathMountainExit,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCaveTop
                    },
                    RequirementNodeID.DeathMountainExit,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainExit_To_DeathMountainExitNonEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExit
                    },
                    RequirementNodeID.DeathMountainExitNonEntrance,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExit
                    },
                    RequirementNodeID.DeathMountainExitNonEntrance,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExit
                    },
                    RequirementNodeID.DeathMountainExitNonEntrance,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainExitNonEntrance_To_DeathMountainExitCave =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExitNonEntrance
                    },
                    RequirementNodeID.DeathMountainExitCave,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExitNonEntrance
                    },
                    RequirementNodeID.DeathMountainExitCave,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainWestBottomNonEntrance_To_DeathMountainExitCave =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestBottomNonEntrance
                    },
                    RequirementNodeID.DeathMountainExitCave,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestBottomNonEntrance
                    },
                    RequirementNodeID.DeathMountainExitCave,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DeathMountainExitCave_To_DeathMountainExitCaveDark =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.DeathMountainExitCaveDark,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, false)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExitCave
                    },
                    RequirementNodeID.DeathMountainExitCaveDark,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExitCave
                    },
                    RequirementNodeID.DeathMountainExitCaveDark,
                    false,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DarkRoomDeathMountainExit, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExitCave
                    },
                    RequirementNodeID.DeathMountainExitCaveDark,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunny_To_LWKakarikoPortal =>
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
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWKakarikoPortal,
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
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWKakarikoPortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldHammer_To_LWKakarikoPortal =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LWKakarikoPortal,
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
                        RequirementNodeID.LightWorldHammer
                    },
                    RequirementNodeID.LWKakarikoPortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWKakarikoPortalInverted_To_LWKakarikoPortal =>
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
                        RequirementNodeID.DWKakarikoPortalInverted
                    },
                    RequirementNodeID.LWKakarikoPortal,
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
                        RequirementNodeID.DWKakarikoPortalInverted
                    },
                    RequirementNodeID.LWKakarikoPortal,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWKakarikoPortal_To_LWKakarikoPortalStandardOpen =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWKakarikoPortal
                    },
                    RequirementNodeID.LWKakarikoPortalStandardOpen,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWKakarikoPortal
                    },
                    RequirementNodeID.LWKakarikoPortalStandardOpen,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWKakarikoPortal_To_LWKakarikoPortalNotBunny =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWKakarikoPortal
                    },
                    RequirementNodeID.LWKakarikoPortalNotBunny,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWKakarikoPortal
                    },
                    RequirementNodeID.LWKakarikoPortalNotBunny,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWKakarikoPortal
                    },
                    RequirementNodeID.LWKakarikoPortalNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorld_To_SickKid =>
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
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.SickKid,
                    false,
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
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.SickKid,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunny_To_GrassHouse =>
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
                    RequirementNodeID.GrassHouse,
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
                    RequirementNodeID.GrassHouse,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.GrassHouse,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunny_To_BombHut =>
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
                    RequirementNodeID.BombHut,
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
                    RequirementNodeID.BombHut,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.BombHut,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldHammer_To_MagicBatLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.MagicBatLedge,
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
                        RequirementNodeID.LightWorldHammer
                    },
                    RequirementNodeID.MagicBatLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> HammerPegsArea_To_MagicBatLedge =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HammerPegsArea
                    },
                    RequirementNodeID.MagicBatLedge,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HammerPegsArea
                    },
                    RequirementNodeID.MagicBatLedge,
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HammerPegsArea
                    },
                    RequirementNodeID.MagicBatLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> MagicBatLedge_To_MagicBat =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MagicBatLedge
                    },
                    RequirementNodeID.MagicBat,
                    false,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MagicBatLedge
                    },
                    RequirementNodeID.MagicBat,
                    false,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MagicBatLedge
                    },
                    RequirementNodeID.MagicBat,
                    false,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MagicBatLedge
                    },
                    RequirementNodeID.MagicBat,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunny_To_RaceGameLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.RaceGameLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.RaceGameLedge,
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
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.RaceGameLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.RaceGameLedge,
                    false,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.RaceGameLedge,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.RaceGameLedge,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.RaceGameLedge,
                    false,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.RaceGameLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DarkWorldSouthMirror_To_RaceGameLedge =>
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
                    RequirementNodeID.RaceGameLedge,
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
                        RequirementNodeID.DarkWorldSouthMirror
                    },
                    RequirementNodeID.RaceGameLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> RaceGameLedge_To_RaceGameLedgeNotBunny =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.RaceGameLedge
                    },
                    RequirementNodeID.RaceGameLedgeNotBunny,
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
                    RequirementNodeID.RaceGameLedgeNotBunny,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.RaceGameLedge
                    },
                    RequirementNodeID.RaceGameLedgeNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LightWorldNotBunny_To_LWGraveyard =>
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
                    RequirementNodeID.LWGraveyard,
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
                    RequirementNodeID.LWGraveyard,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LWGraveyard,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWGraveyardLedge_To_LWGraveyard =>
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
                    RequirementNodeID.LWGraveyard,
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
                    RequirementNodeID.LWGraveyard,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWGraveyardLedge
                    },
                    RequirementNodeID.LWGraveyard,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> KingsTombNotBunny_To_LWGraveyard =>
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
                        (ItemType.Gloves, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.KingsTombNotBunny
                    },
                    RequirementNodeID.LWGraveyard,
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
                    RequirementNodeID.LWGraveyard,
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
                        (ItemType.Gloves, 2)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.KingsTombNotBunny
                    },
                    RequirementNodeID.LWGraveyard,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWGraveyard_To_LWGraveyardNotBunny =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWGraveyard
                    },
                    RequirementNodeID.LWGraveyardNotBunny,
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
                    RequirementNodeID.LWGraveyardNotBunny,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWGraveyard
                    },
                    RequirementNodeID.LWGraveyardNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWGraveyardNotBunny_To_LWGraveyardLedge =>
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
                        RequirementNodeID.LWGraveyardNotBunny
                    },
                    RequirementNodeID.LWGraveyardLedge,
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
                        RequirementNodeID.LWGraveyardNotBunny
                    },
                    RequirementNodeID.LWGraveyardLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWGraveyardMirror_To_LWGraveyardLedge =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LWGraveyardLedge,
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
                        RequirementNodeID.DWGraveyardMirror
                    },
                    RequirementNodeID.LWGraveyardLedge,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWGraveyardNotBunny_To_EscapeGrave =>
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
                        RequirementNodeID.LWGraveyardNotBunny
                    },
                    RequirementNodeID.EscapeGrave,
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
                        RequirementNodeID.LWGraveyardNotBunny
                    },
                    RequirementNodeID.EscapeGrave,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> LWGraveyardNotBunny_To_KingsTomb =>
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
                        RequirementNodeID.LWGraveyardNotBunny
                    },
                    RequirementNodeID.KingsTomb,
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
                        RequirementNodeID.LWGraveyardNotBunny
                    },
                    RequirementNodeID.KingsTomb,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DWGraveyardMirror_To_KingsTomb =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWGraveyardNotBunny
                    },
                    RequirementNodeID.KingsTomb,
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
                        RequirementNodeID.DWGraveyardMirror
                    },
                    RequirementNodeID.KingsTomb,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> KingsTomb_To_KingsTombNotBunny =>
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
                    RequirementNodeID.KingsTombNotBunny,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.KingsTomb
                    },
                    RequirementNodeID.KingsTombNotBunny,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.KingsTomb
                    },
                    RequirementNodeID.KingsTombNotBunny,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.KingsTomb
                    },
                    RequirementNodeID.KingsTombNotBunny,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> KingsTombNotBunny_To_KingsTombGrave =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.KingsTombNotBunny
                    },
                    RequirementNodeID.KingsTombGrave,
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
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.KingsTombNotBunny
                    },
                    RequirementNodeID.KingsTombGrave,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    }
}
