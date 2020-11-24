using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.RequirementNodes
{
    [Collection("Tests")]
    public class DungeonEntryTests
    {
        [Theory]
        [MemberData(nameof(EntranceDungeon_To_HCSanctuaryEntry))]
        [MemberData(nameof(LightWorldNotBunnyOrSuperBunnyMirror_To_HCSanctuaryEntry))]
        [MemberData(nameof(EntranceDungeon_To_HCFrontEntry))]
        [MemberData(nameof(LightWorldNotBunnyOrDungeonRevive_To_HCFrontEntry))]
        [MemberData(nameof(EntranceDungeon_To_HCBackEntry))]
        [MemberData(nameof(EscapeGrave_To_HCBackEntry))]
        [MemberData(nameof(EntranceDungeon_To_ATEntry))]
        [MemberData(nameof(AgahnimTowerEntrance_To_ATEntry))]
        [MemberData(nameof(GanonsTowerEntrance_To_ATEntry))]
        [MemberData(nameof(EntranceDungeon_To_EPEntry))]
        [MemberData(nameof(LightWorldNotBunnyOrDungeonRevive_To_EPEntry))]
        [MemberData(nameof(EntranceDungeon_To_DPFrontEntry))]
        [MemberData(nameof(DesertPalaceFrontEntrance_To_DPFrontEntry))]
        [MemberData(nameof(EntranceDungeon_To_DPLeftEntry))]
        [MemberData(nameof(DesertLedge_To_DPLeftEntry))]
        [MemberData(nameof(EntranceDungeon_To_DPBackEntry))]
        [MemberData(nameof(DesertBack_To_DPBackEntry))]
        [MemberData(nameof(EntranceDungeon_To_ToHEntry))]
        [MemberData(nameof(DeathMountainWestTopNotBunny_To_ToHEntry))]
        [MemberData(nameof(DeathMountainWestTop_To_ToHEntry))]
        [MemberData(nameof(EntranceDungeon_To_PoDEntry))]
        [MemberData(nameof(DarkWorldEastNotBunny_To_PoDEntry))]
        [MemberData(nameof(EntranceDungeon_To_SPEntry))]
        [MemberData(nameof(LightWorldInvertedNotBunny_To_SPEntry))]
        [MemberData(nameof(DarkWorldSouthStandardOpenNotBunny_To_SPEntry))]
        [MemberData(nameof(DarkWorldWestNotBunny_To_SWFrontEntry))]
        [MemberData(nameof(DarkWorldWest_To_SWFrontEntry))]
        [MemberData(nameof(EntranceDungeon_To_SWBackEntry))]
        [MemberData(nameof(SkullWoodsBack_To_SWBackEntry))]
        [MemberData(nameof(EntranceDungeon_To_TTEntry))]
        [MemberData(nameof(DarkWorldWestNotBunny_To_TTEntry))]
        [MemberData(nameof(EntranceDungeon_To_IPEntry))]
        [MemberData(nameof(IcePalaceIsland_To_IPEntry))]
        [MemberData(nameof(EntranceDungeon_To_MMEntry))]
        [MemberData(nameof(MiseryMireEntrance_To_MMEntry))]
        [MemberData(nameof(EntranceDungeon_To_TRFrontEntry))]
        [MemberData(nameof(TurtleRockFrontEntrance_To_TRFrontEntry))]
        [MemberData(nameof(TRFrontEntry_To_TRFrontEntryStandardOpen))]
        [MemberData(nameof(TRFrontEntryStandardOpen_To_TRFrontEntryStandardOpenNonEntrance))]
        [MemberData(nameof(TRFrontEntryStandardOpenNonEntrance_To_TRFrontToKeyDoors))]
        [MemberData(nameof(TRFrontToKeyDoors_To_TRKeyDoorsToMiddleExit))]
        [MemberData(nameof(EntranceDungeon_To_TRMiddleEntry))]
        [MemberData(nameof(TurtleRockTunnel_To_TRMiddleEntry))]
        [MemberData(nameof(EntranceDungeon_To_TRBackEntry))]
        [MemberData(nameof(TurtleRockSafetyDoor_To_TRBackEntry))]
        [MemberData(nameof(EntranceDungeon_To_GTEntry))]
        [MemberData(nameof(AgahnimTowerEntrance_To_GTEntry))]
        [MemberData(nameof(GanonsTowerEntranceStandardOpen_To_GTEntry))]
        public void Tests(
            ModeSaveData mode, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            (LocationID, int)[] smallKeys, RequirementNodeID id, AccessibilityLevel expected)
        {
            ItemDictionary.Instance.Reset();
            PrizeDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();
            RequirementNodeDictionary.Instance.Reset();
            LocationDictionary.Instance.Reset();
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

            foreach (var smallKey in smallKeys)
            {
                ((IDungeon)LocationDictionary.Instance[smallKey.Item1]).SmallKeyItem.Current =
                    smallKey.Item2;
            }

            Assert.Equal(expected, RequirementNodeDictionary.Instance[id].Accessibility);
        }

        public static IEnumerable<object[]> EntranceDungeon_To_HCSanctuaryEntry =>
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
                    new (LocationID, int)[0],
                    RequirementNodeID.HCSanctuaryEntry,
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
                    new (LocationID, int)[0],
                    RequirementNodeID.HCSanctuaryEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.HCSanctuaryEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldNotBunnyOrSuperBunnyMirror_To_HCSanctuaryEntry =>
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
                    new (LocationID, int)[0],
                    RequirementNodeID.HCSanctuaryEntry,
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
                        RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.HCSanctuaryEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_HCFrontEntry =>
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
                    new (LocationID, int)[0],
                    RequirementNodeID.HCFrontEntry,
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
                    new (LocationID, int)[0],
                    RequirementNodeID.HCFrontEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.HCFrontEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldNotBunnyOrDungeonRevive_To_HCFrontEntry =>
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
                    new (LocationID, int)[0],
                    RequirementNodeID.HCFrontEntry,
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
                        RequirementNodeID.LightWorldNotBunnyOrDungeonRevive
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.HCFrontEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_HCBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.HCBackEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.HCBackEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EscapeGrave_To_HCBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.HCBackEntry,
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
                        RequirementNodeID.EscapeGrave
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.HCBackEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_ATEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.ATEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.ATEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> AgahnimTowerEntrance_To_ATEntry =>
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
                        RequirementNodeID.AgahnimTowerEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.ATEntry,
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
                        RequirementNodeID.AgahnimTowerEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.ATEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> GanonsTowerEntrance_To_ATEntry =>
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
                        RequirementNodeID.GanonsTowerEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.ATEntry,
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
                        RequirementNodeID.GanonsTowerEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.ATEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_EPEntry =>
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
                    new (LocationID, int)[0],
                    RequirementNodeID.EPEntry,
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
                    new (LocationID, int)[0],
                    RequirementNodeID.EPEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.EPEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldNotBunnyOrDungeonRevive_To_EPEntry =>
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
                    new (LocationID, int)[0],
                    RequirementNodeID.EPEntry,
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
                        RequirementNodeID.LightWorldNotBunnyOrDungeonRevive
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.EPEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_DPFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.DPFrontEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.DPFrontEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DesertPalaceFrontEntrance_To_DPFrontEntry =>
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
                    new (LocationID, int)[0],
                    RequirementNodeID.DPFrontEntry,
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
                        RequirementNodeID.DesertPalaceFrontEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.DPFrontEntry,
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
                        RequirementNodeID.DesertPalaceFrontEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.DPFrontEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertPalaceFrontEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.DPFrontEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DesertPalaceFrontEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.DPFrontEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_DPLeftEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.DPLeftEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.DPLeftEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DesertLedge_To_DPLeftEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.DPLeftEntry,
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
                        RequirementNodeID.DesertLedge
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.DPLeftEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_DPBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.DPBackEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.DPBackEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DesertBack_To_DPBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.DPBackEntry,
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
                        RequirementNodeID.DesertBack
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.DPBackEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_ToHEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.ToHEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.ToHEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainWestTopNotBunny_To_ToHEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.ToHEntry,
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
                        RequirementNodeID.DeathMountainWestTopNotBunny
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.ToHEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DeathMountainWestTop_To_ToHEntry =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestTop
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.ToHEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestTop
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.ToHEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DeathMountainWestTop
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.ToHEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_PoDEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.PoDEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.PoDEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldEastNotBunny_To_PoDEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.PoDEntry,
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
                        RequirementNodeID.DarkWorldEastNotBunny
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.PoDEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_SPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.SPEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.SPEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LightWorldInvertedNotBunny_To_SPEntry =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldInvertedNotBunny
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.SPEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.LightWorldInvertedNotBunny
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.SPEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldSouthStandardOpenNotBunny_To_SPEntry =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthStandardOpenNotBunny
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.SPEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldSouthStandardOpenNotBunny
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.SPEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_SWFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.SWFrontEntry,
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
                    new (LocationID, int)[0],
                    RequirementNodeID.SWFrontEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWest_To_SWFrontEntry =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWest
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.SWFrontEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.DarkWorldWest
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.SWFrontEntry,
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_SWBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.SWBackEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.SWBackEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> SkullWoodsBack_To_SWBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.SWBackEntry,
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
                        RequirementNodeID.SkullWoodsBack
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.SWBackEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_TTEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.TTEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TTEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DarkWorldWestNotBunny_To_TTEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.TTEntry,
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
                    new (LocationID, int)[0],
                    RequirementNodeID.TTEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_IPEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.IPEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.IPEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> IcePalaceIsland_To_IPEntry =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.IPEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.IPEntry,
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
                    new (LocationID, int)[0],
                    RequirementNodeID.IPEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.IPEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.IcePalaceIsland
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.IPEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_MMEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.MMEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.MMEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MiseryMireEntrance_To_MMEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.MMEntry,
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
                        RequirementNodeID.MiseryMireEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.MMEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_TRFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.TRFrontEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRFrontEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TurtleRockFrontEntrance_To_TRFrontEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.TRFrontEntry,
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
                        RequirementNodeID.TurtleRockFrontEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRFrontEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TRFrontEntry_To_TRFrontEntryStandardOpen =>
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
                        RequirementNodeID.TRFrontEntry
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRFrontEntryStandardOpen,
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
                        RequirementNodeID.TRFrontEntry
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRFrontEntryStandardOpen,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TRFrontEntryStandardOpen_To_TRFrontEntryStandardOpenNonEntrance =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontEntryStandardOpen
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRFrontEntryStandardOpenEntranceNone,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontEntryStandardOpen
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRFrontEntryStandardOpenEntranceNone,
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
                        RequirementNodeID.TRFrontEntryStandardOpen
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRFrontEntryStandardOpenEntranceNone,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TRFrontEntryStandardOpenNonEntrance_To_TRFrontToKeyDoors =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontEntryStandardOpenEntranceNone
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRFrontToKeyDoors,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontEntryStandardOpenEntranceNone
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRFrontToKeyDoors,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TRFrontToKeyDoors_To_TRKeyDoorsToMiddleExit =>
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 0)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 0)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 0)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 1)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 0)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 0)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 1)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 0)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 2)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 0)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 2)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 0)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 1)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.TRFrontToKeyDoors
                    },
                    new (LocationID, int)[]
                    {
                        (LocationID.TurtleRock, 2)
                    },
                    RequirementNodeID.TRKeyDoorsToMiddleExit,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_TRMiddleEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.TRMiddleEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRMiddleEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TurtleRockTunnel_To_TRMiddleEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.TRMiddleEntry,
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
                        RequirementNodeID.TurtleRockTunnel
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRMiddleEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_TRBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.TRBackEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRBackEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> TurtleRockSafetyDoor_To_TRBackEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.TRBackEntry,
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
                        RequirementNodeID.TurtleRockSafetyDoor
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.TRBackEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EntranceDungeon_To_GTEntry =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    new (LocationID, int)[0],
                    RequirementNodeID.GTEntry,
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
                        RequirementNodeID.EntranceDungeonAllInsanity
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.GTEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> AgahnimTowerEntrance_To_GTEntry =>
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
                        RequirementNodeID.AgahnimTowerEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.GTEntry,
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
                        RequirementNodeID.AgahnimTowerEntrance
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.GTEntry,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> GanonsTowerEntranceStandardOpen_To_GTEntry =>
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
                    new (LocationID, int)[0],
                    RequirementNodeID.GTEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.GanonsTowerEntranceStandardOpen
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.GTEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.GanonsTowerEntranceStandardOpen
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.GTEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.GanonsTowerEntranceStandardOpen
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.GTEntry,
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
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.GanonsTowerEntranceStandardOpen
                    },
                    new (LocationID, int)[0],
                    RequirementNodeID.GTEntry,
                    AccessibilityLevel.Normal
                }
            };
    }
}
