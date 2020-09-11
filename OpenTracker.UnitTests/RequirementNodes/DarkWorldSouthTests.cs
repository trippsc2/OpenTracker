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
    public class DarkWorldSouthTests
    {
        [Theory]
        [MemberData(nameof(NonEntranceInverted_To_DarkWorldSouth))]
        [MemberData(nameof(LightWorldMirror_To_DarkWorldSouth))]
        [MemberData(nameof(DWSouthPortalNotBunny_To_DarkWorldSouth))]
        [MemberData(nameof(DarkWorldWest_To_DarkWorldSouth))]
        [MemberData(nameof(DarkWorldEastHammer_To_DarkWorldSouth))]
        [MemberData(nameof(DarkWorldSouth_To_DarkWorldSouthInverted))]
        [MemberData(nameof(DarkWorldSouth_To_DarkWorldSouthStandardOpen))]
        [MemberData(nameof(DarkWorldSouthStandardOpen_To_DarkWorldSouthStandardOpenNotBunny))]
        [MemberData(nameof(DarkWorldSouth_To_DarkWorldSouthMirror))]
        [MemberData(nameof(DarkWorldSouth_To_DarkWorldSouthNotBunny))]
        [MemberData(nameof(DarkWorldSouthNotBunny_To_DarkWorldSouthDash))]
        [MemberData(nameof(DarkWorldSouthNotBunny_To_DarkWorldSouthHammer))]
        [MemberData(nameof(LightWorldInvertedNotBunny_To_BuyBigBomb))]
        [MemberData(nameof(DarkWorldSouthStandardOpenNotBunny_To_BuyBigBomb))]
        [MemberData(nameof(BuyBigBomb_To_BuyBigBombStandardOpen))]
        [MemberData(nameof(BuyBigBomb_To_BigBombToLightWorld))]
        [MemberData(nameof(BigBombToLightWorld_To_BigBombToLightWorldStandardOpen))]
        [MemberData(nameof(BuyBigBombStandardOpen_To_BigBombToDWLakeHylia))]
        [MemberData(nameof(BuyBigBombStandardOpen_To_BigBombToWall))]
        [MemberData(nameof(BigBombToLightWorld_To_BigBombToWall))]
        [MemberData(nameof(BigBombToLightWorldStandardOpen_To_BigBombToWall))]
        [MemberData(nameof(BigBombToDWLakeHylia_To_BigBombToWall))]
        [MemberData(nameof(LWSouthPortalStandardOpen_To_DWSouthPortal))]
        [MemberData(nameof(DarkWorldSouthHammer_To_DWSouthPortal))]
        [MemberData(nameof(DWSouthPortal_To_DWSouthPortalInverted))]
        [MemberData(nameof(DWSouthPortal_To_DWSouthPortalNotBunny))]
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

        public static IEnumerable<object[]> NonEntranceInverted_To_DarkWorldSouth =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldSouth,
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
                    RequirementNodeID.DarkWorldSouth,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldMirror_To_DarkWorldSouth =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldSouth,
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
                    RequirementNodeID.DarkWorldSouth,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWSouthPortalNotBunny_To_DarkWorldSouth =>
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
                        RequirementNodeID.DWSouthPortalNotBunny
                    },
                    RequirementNodeID.DarkWorldSouth,
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
                        RequirementNodeID.DWSouthPortalNotBunny
                    },
                    RequirementNodeID.DarkWorldSouth,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_DarkWorldSouth =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldSouth,
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
                    RequirementNodeID.DarkWorldSouth,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldEastHammer_To_DarkWorldSouth =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DarkWorldSouth,
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
                        RequirementNodeID.DarkWorldEastHammer
                    },
                    RequirementNodeID.DarkWorldSouth,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_DarkWorldSouthInverted =>
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
                        RequirementNodeID.DarkWorldSouth
                    },
                    RequirementNodeID.DarkWorldSouthInverted,
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
                        RequirementNodeID.DarkWorldSouth
                    },
                    RequirementNodeID.DarkWorldSouthInverted,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_DarkWorldSouthStandardOpen =>
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
                        RequirementNodeID.DarkWorldSouth
                    },
                    RequirementNodeID.DarkWorldSouthStandardOpen,
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
                        RequirementNodeID.DarkWorldSouth
                    },
                    RequirementNodeID.DarkWorldSouthStandardOpen,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouthStandardOpen_To_DarkWorldSouthStandardOpenNotBunny =>
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
                        RequirementNodeID.DarkWorldSouthStandardOpen
                    },
                    RequirementNodeID.DarkWorldSouthStandardOpenNotBunny,
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
                        RequirementNodeID.DarkWorldSouthStandardOpen
                    },
                    RequirementNodeID.DarkWorldSouthStandardOpenNotBunny,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_DarkWorldSouthMirror =>
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
                        RequirementNodeID.DarkWorldSouth
                    },
                    RequirementNodeID.DarkWorldSouthMirror,
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
                        RequirementNodeID.DarkWorldSouth
                    },
                    RequirementNodeID.DarkWorldSouthMirror,
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
                        RequirementNodeID.DarkWorldSouth
                    },
                    RequirementNodeID.DarkWorldSouthMirror,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouth_To_DarkWorldSouthNotBunny =>
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
                        RequirementNodeID.DarkWorldSouth
                    },
                    RequirementNodeID.DarkWorldSouthNotBunny,
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
                        RequirementNodeID.DarkWorldSouth
                    },
                    RequirementNodeID.DarkWorldSouthNotBunny,
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
                    RequirementNodeID.DarkWorldSouthNotBunny,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouthNotBunny_To_DarkWorldSouthDash =>
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
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DarkWorldSouthDash,
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
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DarkWorldSouthDash,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouthNotBunny_To_DarkWorldSouthHammer =>
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
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DarkWorldSouthHammer,
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
                        RequirementNodeID.DarkWorldSouthNotBunny
                    },
                    RequirementNodeID.DarkWorldSouthHammer,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldInvertedNotBunny_To_BuyBigBomb =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldInvertedNotBunny
                    },
                    RequirementNodeID.BuyBigBomb,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldInvertedNotBunny
                    },
                    RequirementNodeID.BuyBigBomb,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouthStandardOpenNotBunny_To_BuyBigBomb =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthStandardOpenNotBunny
                    },
                    RequirementNodeID.BuyBigBomb,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 2)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthStandardOpenNotBunny
                    },
                    RequirementNodeID.BuyBigBomb,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BuyBigBomb_To_BuyBigBombStandardOpen =>
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
                        RequirementNodeID.BuyBigBomb
                    },
                    RequirementNodeID.BuyBigBombStandardOpen,
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
                        RequirementNodeID.BuyBigBomb
                    },
                    RequirementNodeID.BuyBigBombStandardOpen,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BuyBigBomb_To_BigBombToLightWorld =>
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
                        RequirementNodeID.BuyBigBomb
                    },
                    RequirementNodeID.BigBombToLightWorld,
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
                    new RequirementNodeID[0],
                    RequirementNodeID.BigBombToLightWorld,
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
                        RequirementNodeID.BuyBigBomb
                    },
                    RequirementNodeID.BigBombToLightWorld,
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
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BuyBigBomb
                    },
                    RequirementNodeID.BigBombToLightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BigBombToLightWorld_To_BigBombToLightWorldStandardOpen =>
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
                        RequirementNodeID.BigBombToLightWorld
                    },
                    RequirementNodeID.BigBombToLightWorldStandardOpen,
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
                        RequirementNodeID.BigBombToLightWorld
                    },
                    RequirementNodeID.BigBombToLightWorldStandardOpen,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BuyBigBombStandardOpen_To_BigBombToDWLakeHylia =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BuyBigBombStandardOpen
                    },
                    RequirementNodeID.BigBombToDWLakeHylia,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BuyBigBombStandardOpen
                    },
                    RequirementNodeID.BigBombToDWLakeHylia,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BuyBigBombStandardOpen
                    },
                    RequirementNodeID.BigBombToDWLakeHylia,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, false),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BuyBigBombStandardOpen
                    },
                    RequirementNodeID.BigBombToDWLakeHylia,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, false),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BuyBigBombStandardOpen
                    },
                    RequirementNodeID.BigBombToDWLakeHylia,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, false)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BuyBigBombStandardOpen
                    },
                    RequirementNodeID.BigBombToDWLakeHylia,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 1),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BuyBigBombStandardOpen
                    },
                    RequirementNodeID.BigBombToDWLakeHylia,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1),
                        (ItemType.CaneOfSomaria, 1),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 1),
                        (ItemType.Flippers, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BuyBigBombStandardOpen
                    },
                    RequirementNodeID.BigBombToDWLakeHylia,
                    AccessibilityLevel.SequenceBreak
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0),
                        (ItemType.CaneOfSomaria, 0),
                        (ItemType.Boomerang, 0),
                        (ItemType.RedBoomerang, 0),
                        (ItemType.Flippers, 1),
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.BombDuplicationAncillaOverload, true),
                        (SequenceBreakType.BombDuplicationMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BuyBigBombStandardOpen
                    },
                    RequirementNodeID.BigBombToDWLakeHylia,
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> BuyBigBombStandardOpen_To_BigBombToWall =>
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
                        RequirementNodeID.BuyBigBombStandardOpen
                    },
                    RequirementNodeID.BigBombToWall,
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
                        RequirementNodeID.BuyBigBombStandardOpen
                    },
                    RequirementNodeID.BigBombToWall,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BigBombToLightWorld_To_BigBombToWall =>
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
                        RequirementNodeID.BigBombToLightWorld
                    },
                    RequirementNodeID.BigBombToWall,
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
                        RequirementNodeID.BigBombToLightWorld
                    },
                    RequirementNodeID.BigBombToWall,
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
                        RequirementNodeID.BigBombToLightWorld
                    },
                    RequirementNodeID.BigBombToWall,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BigBombToLightWorldStandardOpen_To_BigBombToWall =>
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
                        RequirementNodeID.BigBombToLightWorldStandardOpen
                    },
                    RequirementNodeID.BigBombToWall,
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
                        RequirementNodeID.BigBombToLightWorldStandardOpen
                    },
                    RequirementNodeID.BigBombToWall,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BigBombToDWLakeHylia_To_BigBombToWall =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.BigBombToWall,
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
                        RequirementNodeID.BigBombToDWLakeHylia
                    },
                    RequirementNodeID.BigBombToWall,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWSouthPortalStandardOpen_To_DWSouthPortal =>
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
                        RequirementNodeID.LWSouthPortalStandardOpen
                    },
                    RequirementNodeID.DWSouthPortal,
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
                        RequirementNodeID.LWSouthPortalStandardOpen
                    },
                    RequirementNodeID.DWSouthPortal,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouthHammer_To_DWSouthPortal =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DWSouthPortal,
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
                        RequirementNodeID.DarkWorldSouthHammer
                    },
                    RequirementNodeID.DWSouthPortal,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWSouthPortal_To_DWSouthPortalInverted =>
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
                        RequirementNodeID.DWSouthPortal
                    },
                    RequirementNodeID.DWSouthPortalInverted,
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
                        RequirementNodeID.DWSouthPortal
                    },
                    RequirementNodeID.DWSouthPortalInverted,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWSouthPortal_To_DWSouthPortalNotBunny =>
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
                        RequirementNodeID.DWSouthPortal
                    },
                    RequirementNodeID.DWSouthPortalNotBunny,
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
                    RequirementNodeID.DWSouthPortalNotBunny,
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
                        RequirementNodeID.DWSouthPortal
                    },
                    RequirementNodeID.DWSouthPortalNotBunny,
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
                        RequirementNodeID.DWSouthPortal
                    },
                    RequirementNodeID.DWSouthPortalNotBunny,
                    AccessibilityLevel.Normal
                }
            };
    }
}
