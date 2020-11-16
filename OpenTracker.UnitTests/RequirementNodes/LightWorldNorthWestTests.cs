using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    [Collection("Tests")]
    public class LightWorldNorthWestTests
    {
        [Theory]
        [MemberData(nameof(LightWorld_To_Pedestal))]
        [MemberData(nameof(LightWorldDash_To_LumberjackCaveHole))]
        [MemberData(nameof(LumberjackCaveHole_To_LumberjackCave))]
        [MemberData(nameof(LightWorldInspect_To_LumberjackCave))]
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
        [MemberData(nameof(MagicBatLedge_To_MagicBatEntrance))]
        [MemberData(nameof(LightWorldInspect_To_MagicBatEntrance))]
        [MemberData(nameof(LightWorldDash_To_Library))]
        [MemberData(nameof(LightWorldInspect_To_Library))]
        [MemberData(nameof(LightWorldNotBunny_To_RaceGameLedge))]
        [MemberData(nameof(DarkWorldSouthMirror_To_RaceGameLedge))]
        [MemberData(nameof(RaceGameLedge_To_RaceGame))]
        [MemberData(nameof(LightWorldInspect_To_RaceGame))]
        [MemberData(nameof(LightWorldNotBunny_To_LWGraveyard))]
        [MemberData(nameof(LWGraveyardLedge_To_LWGraveyard))]
        [MemberData(nameof(KingsTombNotBunny_To_LWGraveyard))]
        [MemberData(nameof(LWGraveyard_To_LWGraveyardNotBunny))]
        [MemberData(nameof(LWGraveyardNotBunny_To_LWGraveyardLedge))]
        [MemberData(nameof(DWGraveyardMirror_To_LWGraveyardLedge))]
        [MemberData(nameof(LWGraveyardNotBunny_To_EscapeGrave))]
        [MemberData(nameof(EscapeGrave_To_SanctuaryGraveEntrance))]
        [MemberData(nameof(LightWorldInspect_To_SanctuaryGraveEntrance))]
        [MemberData(nameof(LWGraveyardNotBunny_To_KingsTomb))]
        [MemberData(nameof(DWGraveyardMirror_To_KingsTomb))]
        [MemberData(nameof(KingsTomb_To_KingsTombNotBunny))]
        [MemberData(nameof(KingsTombNotBunny_To_KingsTombGrave))]
        public void Tests(
            ModeSaveData mode, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, AccessibilityLevel expected)
        {
            ItemDictionary.Instance.Reset();
            PrizeDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();
            RequirementNodeDictionary.Instance.Reset();
            Mode.Instance.Load(mode);

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            foreach (var prize in prizes)
            {
                PrizeDictionary.Instance[prize.Item1].Current = prize.Item2;
            }

            foreach (var sequenceBreak in sequenceBreaks)
            {
                SequenceBreakDictionary.Instance[sequenceBreak.Item1].Enabled =
                    sequenceBreak.Item2;
            }

            foreach (var node in accessibleNodes)
            {
                RequirementNodeDictionary.Instance[node].AlwaysAccessible = true;
            }

            Assert.Equal(expected, RequirementNodeDictionary.Instance[id].Accessibility);
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
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LumberjackCaveHole_To_LumberjackCave =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.LumberjackCave,
                    AccessibilityLevel.Inspect
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LumberjackCaveHole
                    },
                    RequirementNodeID.LumberjackCave,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldInspect_To_LumberjackCave =>
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
                    RequirementNodeID.LumberjackCave,
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
                    RequirementNodeID.LumberjackCave,
                    AccessibilityLevel.Inspect
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LumberjackCave,
                    AccessibilityLevel.Inspect
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
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MagicBatLedge_To_MagicBatEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.MagicBatEntrance,
                    AccessibilityLevel.Inspect
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MagicBatLedge
                    },
                    RequirementNodeID.MagicBatEntrance,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldInspect_To_MagicBatEntrance =>
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
                    RequirementNodeID.MagicBatEntrance,
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
                    RequirementNodeID.MagicBatEntrance,
                    AccessibilityLevel.Inspect
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.MagicBatEntrance,
                    AccessibilityLevel.Inspect
                }
            };

        public static IEnumerable<object[]> LightWorldDash_To_Library =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.Library,
                    AccessibilityLevel.Inspect
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldDash
                    },
                    RequirementNodeID.Library,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldInspect_To_Library =>
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
                    RequirementNodeID.Library,
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
                    RequirementNodeID.Library,
                    AccessibilityLevel.Inspect
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Library,
                    AccessibilityLevel.Inspect
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
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> RaceGameLedge_To_RaceGame =>
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
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.RaceGameLegdeNotBunny,
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
                        RequirementNodeID.RaceGameLedge
                    },
                    RequirementNodeID.RaceGameLegdeNotBunny,
                    AccessibilityLevel.Inspect
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
                    RequirementNodeID.RaceGameLegdeNotBunny,
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
                    RequirementNodeID.RaceGameLegdeNotBunny,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldInspect_To_RaceGame =>
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
                    RequirementNodeID.RaceGameLegdeNotBunny,
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.RaceGameLegdeNotBunny,
                    AccessibilityLevel.Inspect
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
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EscapeGrave_To_SanctuaryGraveEntrance =>
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
                    RequirementNodeID.SanctuaryGraveEntrance,
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
                    RequirementNodeID.SanctuaryGraveEntrance,
                    AccessibilityLevel.Inspect
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
                        RequirementNodeID.EscapeGrave
                    },
                    RequirementNodeID.SanctuaryGraveEntrance,
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
                        RequirementNodeID.EscapeGrave
                    },
                    RequirementNodeID.SanctuaryGraveEntrance,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldInspect_To_SanctuaryGraveEntrance =>
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
                    RequirementNodeID.SanctuaryGraveEntrance,
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
                    RequirementNodeID.SanctuaryGraveEntrance,
                    AccessibilityLevel.Inspect
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.SanctuaryGraveEntrance,
                    AccessibilityLevel.Inspect
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
                    AccessibilityLevel.Normal
                }
            };
    }
}
