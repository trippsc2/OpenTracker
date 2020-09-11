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
    public class DarkWorldNorthWestTests
    {
        [Theory]
        [MemberData(nameof(LWKakarikoPortalStandardOpen_To_DWKakarikoPortal))]
        [MemberData(nameof(DarkWorldWestNotBunny_To_DWKakarikoPortal))]
        [MemberData(nameof(DWKakarikoPortal_To_DWKakarikoPortalInverted))]
        [MemberData(nameof(NonEntranceInverted_To_DarkWorldWest))]
        [MemberData(nameof(LightWorldMirror_To_DarkWorldWest))]
        [MemberData(nameof(DWKakarikoPortal_To_DarkWorldWest))]
        [MemberData(nameof(BumperCaveEntry_To_DarkWorldWest))]
        [MemberData(nameof(BumperCaveTop_To_DarkWorldWest))]
        [MemberData(nameof(HammerHouseNotBunny_To_DarkWorldWest))]
        [MemberData(nameof(DarkWorldSouthNotBunny_To_DarkWorldWest))]
        [MemberData(nameof(DWWitchAreaNotBunny_To_DarkWorldWest))]
        [MemberData(nameof(DarkWorldWest_To_DarkWorldWestNotBunny))]
        [MemberData(nameof(DarkWorldWestNotBunny_To_DarkWorldWestNotBunnyOrSuperBunnyMirror))]
        [MemberData(nameof(DarkWorldWest_To_DarkWorldWestNotBunnyOrSuperBunnyMirror))]
        [MemberData(nameof(DarkWorldWestNotBunny_To_DarkWorldWestLift2))]
        [MemberData(nameof(DarkWorldWestNotBunny_To_SkullWoodsBack))]
        [MemberData(nameof(DarkWorldWestNotBunny_To_BumperCaveEntry))]
        [MemberData(nameof(DeathMountainEntry_To_BumperCaveEntry))]
        [MemberData(nameof(BumperCaveEntry_To_BumperCaveEntryNonEntrance))]
        [MemberData(nameof(DeathMountainEntryNonEntrance_To_BumperCaveFront))]
        [MemberData(nameof(BumperCaveEntryNonEntrance_To_BumperCaveFront))]
        [MemberData(nameof(BumperCaveFront_To_BumperCaveNotBunny))]
        [MemberData(nameof(BumperCaveNotBunny_To_BumperCavePastGap))]
        [MemberData(nameof(BumperCavePastGap_To_BumperCaveBack))]
        [MemberData(nameof(DeathMountainExit_To_BumperCaveTop))]
        [MemberData(nameof(BumperCaveBack_To_BumperCaveTop))]
        [MemberData(nameof(BumperCaveTop_To_BumperCaveItem))]
        [MemberData(nameof(DarkWorldWest_To_BumperCaveItem))]
        [MemberData(nameof(DarkWorldWestNotBunny_To_HammerHouse))]
        [MemberData(nameof(HammerHouse_To_HammerHouseNotBunny))]
        [MemberData(nameof(LightWorldMirror_To_HammerPegsArea))]
        [MemberData(nameof(DarkWorldWestLift2_To_HammerPegsArea))]
        [MemberData(nameof(HammerPegsArea_To_HammerPegs))]
        [MemberData(nameof(HammerPegsArea_To_PurpleChest))]
        [MemberData(nameof(LightWorldMirror_To_BlacksmithPrison))]
        [MemberData(nameof(DarkWorldWestLift2_To_BlacksmithPrison))]
        [MemberData(nameof(BlacksmithPrison_To_Blacksmith))]
        [MemberData(nameof(DarkWorldWestNotBunny_To_DWGraveyard))]
        [MemberData(nameof(DWGraveyard_To_DWGraveyardMirror))]
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

        public static IEnumerable<object[]> LWKakarikoPortalStandardOpen_To_DWKakarikoPortal =>
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
                        RequirementNodeID.LWKakarikoPortalStandardOpen
                    },
                    RequirementNodeID.DWKakarikoPortal,
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
                        RequirementNodeID.LWKakarikoPortalStandardOpen
                    },
                    RequirementNodeID.DWKakarikoPortal,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_DWKakarikoPortal =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DWKakarikoPortal,
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
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWKakarikoPortal,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWKakarikoPortal_To_DWKakarikoPortalInverted =>
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
                        RequirementNodeID.DWKakarikoPortal
                    },
                    RequirementNodeID.DWKakarikoPortalInverted,
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
                        RequirementNodeID.DWKakarikoPortal
                    },
                    RequirementNodeID.DWKakarikoPortalInverted,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> NonEntranceInverted_To_DarkWorldWest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldWest,
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
                        RequirementNodeID.NonEntranceInverted
                    },
                    RequirementNodeID.DarkWorldWest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldMirror_To_DarkWorldWest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldWest,
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
                        RequirementNodeID.LightWorldMirror
                    },
                    RequirementNodeID.DarkWorldWest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWKakarikoPortal_To_DarkWorldWest =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWKakarikoPortal
                    },
                    RequirementNodeID.DarkWorldWest,
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
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldWest,
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
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWKakarikoPortal
                    },
                    RequirementNodeID.DarkWorldWest,
                    AccessibilityLevel.Normal
                },
            };

        public static IEnumerable<object[]> BumperCaveEntry_To_DarkWorldWest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldWest,
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
                        RequirementNodeID.BumperCaveEntry
                    },
                    RequirementNodeID.DarkWorldWest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BumperCaveTop_To_DarkWorldWest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldWest,
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
                        RequirementNodeID.BumperCaveTop
                    },
                    RequirementNodeID.DarkWorldWest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HammerHouseNotBunny_To_DarkWorldWest =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HammerHouseNotBunny
                    },
                    RequirementNodeID.DarkWorldWest,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HammerHouseNotBunny
                    },
                    RequirementNodeID.DarkWorldWest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouthNotBunny_To_DarkWorldWest =>
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
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DarkWorldWest,
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
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DarkWorldWest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWWitchAreaNotBunny_To_DarkWorldWest =>
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
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DarkWorldWest,
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
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DWWitchAreaNotBunny
                    },
                    RequirementNodeID.DarkWorldWest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_DarkWorldWestNotBunny =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWest
                    },
                    RequirementNodeID.DarkWorldWestNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWest
                    },
                    RequirementNodeID.DarkWorldWestNotBunny,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldWestNotBunny,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_DarkWorldWestNotBunnyOrSuperBunnyMirror =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror,
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
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_DarkWorldWestNotBunnyOrSuperBunnyMirror =>
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWest
                    },
                    RequirementNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, false)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWest
                    },
                    RequirementNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWest
                    },
                    RequirementNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror,
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_DarkWorldWestLift2 =>
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
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DarkWorldWestLift2,
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
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DarkWorldWestLift2,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_SkullWoodsBack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.SkullWoodsBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.SkullWoodsBack,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_BumperCaveEntry =>
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
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.BumperCaveEntry,
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
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.BumperCaveEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainEntry_To_BumperCaveEntry =>
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEntry
                    },
                    RequirementNodeID.BumperCaveEntry,
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
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainEntry
                    },
                    RequirementNodeID.BumperCaveEntry,
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
                        RequirementNodeID.DeathMountainEntry
                    },
                    RequirementNodeID.BumperCaveEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BumperCaveEntry_To_BumperCaveEntryNonEntrance =>
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
                        RequirementNodeID.BumperCaveEntry
                    },
                    RequirementNodeID.BumperCaveEntryNonEntrance,
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
                        RequirementNodeID.BumperCaveEntry
                    },
                    RequirementNodeID.BumperCaveEntryNonEntrance,
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
                        RequirementNodeID.BumperCaveEntry
                    },
                    RequirementNodeID.BumperCaveEntryNonEntrance,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainEntryNonEntrance_To_BumperCaveFront =>
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
                        RequirementNodeID.DeathMountainEntryNonEntrance
                    },
                    RequirementNodeID.BumperCaveFront,
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
                        RequirementNodeID.DeathMountainEntryNonEntrance
                    },
                    RequirementNodeID.BumperCaveFront,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BumperCaveEntryNonEntrance_To_BumperCaveFront =>
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
                        RequirementNodeID.BumperCaveEntryNonEntrance
                    },
                    RequirementNodeID.BumperCaveFront,
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
                        RequirementNodeID.BumperCaveEntryNonEntrance
                    },
                    RequirementNodeID.BumperCaveFront,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BumperCaveFront_To_BumperCaveNotBunny =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCaveFront
                    },
                    RequirementNodeID.BumperCaveNotBunny,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCaveFront
                    },
                    RequirementNodeID.BumperCaveNotBunny,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BumperCaveNotBunny_To_BumperCavePastGap =>
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
                        (ItemType.Hookshot, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.BumperCavePastGap,
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
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.BumperCavePastGap,
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
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCaveNotBunny
                    },
                    RequirementNodeID.BumperCavePastGap,
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
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCaveNotBunny
                    },
                    RequirementNodeID.BumperCavePastGap,
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
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCaveNotBunny
                    },
                    RequirementNodeID.BumperCavePastGap,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BumperCavePastGap_To_BumperCaveBack =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCavePastGap
                    },
                    RequirementNodeID.BumperCaveBack,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Cape, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BumperCavePastGap
                    },
                    RequirementNodeID.BumperCaveBack,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainExit_To_BumperCaveTop =>
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
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExit
                    },
                    RequirementNodeID.BumperCaveTop,
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
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainExit
                    },
                    RequirementNodeID.BumperCaveTop,
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
                        RequirementNodeID.DeathMountainExit
                    },
                    RequirementNodeID.BumperCaveTop,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BumperCaveBack_To_BumperCaveTop =>
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
                        RequirementNodeID.BumperCaveBack
                    },
                    RequirementNodeID.BumperCaveTop,
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
                        RequirementNodeID.BumperCaveBack
                    },
                    RequirementNodeID.BumperCaveTop,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BumperCaveTop_To_BumperCaveItem =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.BumperCaveItem,
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
                        RequirementNodeID.BumperCaveTop
                    },
                    RequirementNodeID.BumperCaveItem,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_BumperCaveItem =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.BumperCaveItem,
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
                        RequirementNodeID.DarkWorldWest
                    },
                    RequirementNodeID.BumperCaveItem,
                    AccessibilityLevel.Inspect
                }
            };

        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_HammerHouse =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.HammerHouse,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.HammerHouse,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HammerHouse_To_HammerHouseNotBunny =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HammerHouse
                    },
                    RequirementNodeID.HammerHouseNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.HammerHouseNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HammerHouse
                    },
                    RequirementNodeID.HammerHouseNotBunny,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HammerHouse
                    },
                    RequirementNodeID.HammerHouseNotBunny,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldMirror_To_HammerPegsArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.HammerPegsArea,
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
                        RequirementNodeID.LightWorldMirror
                    },
                    RequirementNodeID.HammerPegsArea,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWestLift2_To_HammerPegsArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.HammerPegsArea,
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
                        RequirementNodeID.DarkWorldWestLift2
                    },
                    RequirementNodeID.HammerPegsArea,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HammerPegsArea_To_HammerPegs =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HammerPegsArea
                    },
                    RequirementNodeID.HammerPegs,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.HammerPegsArea
                    },
                    RequirementNodeID.HammerPegs,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HammerPegsArea_To_PurpleChest =>
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
                    RequirementNodeID.PurpleChest,
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
                        RequirementNodeID.HammerPegsArea
                    },
                    RequirementNodeID.PurpleChest,
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
                        RequirementNodeID.HammerPegsArea
                    },
                    RequirementNodeID.PurpleChest,
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
                        RequirementNodeID.HammerPegsArea,
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.PurpleChest,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldMirror_To_BlacksmithPrison =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.BlacksmithPrison,
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
                        RequirementNodeID.LightWorldMirror
                    },
                    RequirementNodeID.BlacksmithPrison,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWestLift2_To_BlacksmithPrison =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.BlacksmithPrison,
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
                        RequirementNodeID.DarkWorldWestLift2
                    },
                    RequirementNodeID.BlacksmithPrison,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BlacksmithPrison_To_Blacksmith =>
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
                    RequirementNodeID.Blacksmith,
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
                        RequirementNodeID.BlacksmithPrison
                    },
                    RequirementNodeID.Blacksmith,
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
                        RequirementNodeID.BlacksmithPrison
                    },
                    RequirementNodeID.Blacksmith,
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
                        RequirementNodeID.BlacksmithPrison,
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.Blacksmith,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_DWGraveyard =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DWGraveyard,
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
                        RequirementNodeID.DarkWorldWestNotBunny
                    },
                    RequirementNodeID.DWGraveyard,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWGraveyard_To_DWGraveyardMirror =>
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
                        RequirementNodeID.DWGraveyard
                    },
                    RequirementNodeID.DWGraveyardMirror,
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
                        RequirementNodeID.DWGraveyard
                    },
                    RequirementNodeID.DWGraveyardMirror,
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
                        RequirementNodeID.DWGraveyard
                    },
                    RequirementNodeID.DWGraveyardMirror,
                    AccessibilityLevel.Normal
                }
            };
    }
}
