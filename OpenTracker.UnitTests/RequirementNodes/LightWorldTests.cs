using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    [Collection("Tests")]
    public class LightWorldTests
    {
        [Theory]
        [MemberData(nameof(Start_To_LightWorld))]
        [MemberData(nameof(DeathMountainEntry_To_LightWorld))]
        [MemberData(nameof(DeathMountainExit_To_LightWorld))]
        [MemberData(nameof(LWKakarikoPortalNotBunny_To_LightWorld))]
        [MemberData(nameof(DesertLedge_To_LightWorld))]
        [MemberData(nameof(GrassHouse_To_LightWorld))]
        [MemberData(nameof(BombHut_To_LightWorld))]
        [MemberData(nameof(RaceGameLedge_To_LightWorld))]
        [MemberData(nameof(SouthOfGroveLedge_To_LightWorld))]
        [MemberData(nameof(CheckerboardLedge_To_LightWorld))]
        [MemberData(nameof(LWMirePortal_To_LightWorld))]
        [MemberData(nameof(LWSouthPortalNotBunny_To_LightWorld))]
        [MemberData(nameof(LWWitchAreaNotBunny_To_LightWorld))]
        [MemberData(nameof(LWEastPortalNotBunny_To_LightWorld))]
        [MemberData(nameof(DarkWorldSouthInverted_To_LightWorld))]
        [MemberData(nameof(LightWorld_To_LightWorldInverted))]
        [MemberData(nameof(LightWorldInverted_To_LightWorldInvertedNotBunny))]
        [MemberData(nameof(LightWorld_To_LightWorldStandardOpen))]
        [MemberData(nameof(LightWorld_To_LightWorldMirror))]
        [MemberData(nameof(LightWorld_To_LightWorldNotBunny))]
        [MemberData(nameof(LightWorld_To_LightWorldNotBunnyOrDungeonRevive))]
        [MemberData(nameof(LightWorld_To_LightWorldNotBunnyOrSuperBunnyFallInHole))]
        [MemberData(nameof(LightWorld_To_LightWorldNotBunnyOrSuperBunnyMirror))]
        [MemberData(nameof(LightWorldNotBunny_To_LightWorldDash))]
        [MemberData(nameof(LightWorldNotBunny_To_LightWorldHammer))]
        [MemberData(nameof(LightWorldNotBunny_To_LightWorldLift1))]
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

        public static IEnumerable<object[]> Start_To_LightWorld =>
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
                    RequirementNodeID.LightWorld,
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
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainEntry_To_LightWorld =>
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
                    RequirementNodeID.LightWorld,
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
                        RequirementNodeID.DeathMountainEntry
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainExit_To_LightWorld =>
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
                    RequirementNodeID.LightWorld,
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
                        RequirementNodeID.DeathMountainExit
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWKakarikoPortalNotBunny_To_LightWorld =>
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
                        (ItemType.Gloves, 1),
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWKakarikoPortalNotBunny
                    },
                    RequirementNodeID.LightWorld,
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
                        (ItemType.Gloves, 1),
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWKakarikoPortalNotBunny
                    },
                    RequirementNodeID.LightWorld,
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
                        (ItemType.Gloves, 2),
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWKakarikoPortalNotBunny
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DesertLedge_To_LightWorld =>
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
                    RequirementNodeID.LightWorld,
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
                        RequirementNodeID.DesertLedge
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> GrassHouse_To_LightWorld =>
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
                        RequirementNodeID.GrassHouse
                    },
                    RequirementNodeID.LightWorld,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.GrassHouse
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BombHut_To_LightWorld =>
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
                        RequirementNodeID.BombHut
                    },
                    RequirementNodeID.LightWorld,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.BombHut
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> RaceGameLedge_To_LightWorld =>
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
                    RequirementNodeID.LightWorld,
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
                        RequirementNodeID.RaceGameLedge
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SouthOfGroveLedge_To_LightWorld =>
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
                    RequirementNodeID.LightWorld,
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
                        RequirementNodeID.SouthOfGroveLedge
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> CheckerboardLedge_To_LightWorld =>
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
                    RequirementNodeID.LightWorld,
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
                        RequirementNodeID.CheckerboardLedge
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWMirePortal_To_LightWorld =>
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
                    RequirementNodeID.LightWorld,
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
                        RequirementNodeID.LWMirePortal
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWSouthPortalNotBunny_To_LightWorld =>
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
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWSouthPortalNotBunny
                    },
                    RequirementNodeID.LightWorld,
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
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWSouthPortalNotBunny
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWWitchAreaNotBunny_To_LightWorld =>
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
                    RequirementNodeID.LightWorld,
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
                        RequirementNodeID.LWWitchAreaNotBunny
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWEastPortalNotBunny_To_LightWorld =>
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
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWEastPortalNotBunny
                    },
                    RequirementNodeID.LightWorld,
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
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LWEastPortalNotBunny
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouthInverted_To_LightWorld =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga1, 0)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthInverted
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga1, 1)
                    },
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthInverted
                    },
                    RequirementNodeID.LightWorld,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_LightWorldInverted =>
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldInverted,
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
                    RequirementNodeID.LightWorldInverted,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldInverted_To_LightWorldInvertedNotBunny =>
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
                        RequirementNodeID.LightWorldInverted
                    },
                    RequirementNodeID.LightWorldInvertedNotBunny,
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
                        RequirementNodeID.LightWorldInverted
                    },
                    RequirementNodeID.LightWorldInvertedNotBunny,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_LightWorldStandardOpen =>
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldStandardOpen,
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldStandardOpen,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_LightWorldMirror =>
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldMirror,
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldMirror,
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldMirror,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_LightWorldNotBunny =>
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
                    RequirementNodeID.LightWorldNotBunny,
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
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunny,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunny,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_LightWorldNotBunnyOrDungeonRevive =>
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
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.LightWorldNotBunnyOrDungeonRevive,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.DungeonRevive, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.LightWorldNotBunnyOrDungeonRevive,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunnyOrDungeonRevive,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunnyOrDungeonRevive,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunnyOrDungeonRevive,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_LightWorldNotBunnyOrSuperBunnyFallInHole =>
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
                        (SequenceBreakType.SuperBunnyFallInHole, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyFallInHole, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole,
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
                        (SequenceBreakType.SuperBunnyFallInHole, false)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole,
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
                        (SequenceBreakType.SuperBunnyFallInHole, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole,
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
                        (SequenceBreakType.SuperBunnyFallInHole, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorld_To_LightWorldNotBunnyOrSuperBunnyMirror =>
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
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[0],
                    RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror,
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
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror,
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
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, false)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror,
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
                        (ItemType.MoonPearl, 0),
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror,
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
                        (ItemType.MoonPearl, 1),
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorld
                    },
                    RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldNotBunny_To_LightWorldDash =>
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
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LightWorldDash,
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
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LightWorldDash,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldNotBunny_To_LightWorldHammer =>
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
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LightWorldHammer,
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
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LightWorldHammer,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldNotBunny_To_LightWorldLift1 =>
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
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LightWorldLift1,
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
                        RequirementNodeID.LightWorldNotBunny
                    },
                    RequirementNodeID.LightWorldLift1,
                    AccessibilityLevel.Normal
                }
            };
    }
}
