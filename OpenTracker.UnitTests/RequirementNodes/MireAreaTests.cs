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
    public class MireAreaTests
    {
        [Theory]
        [MemberData(nameof(LightWorldMirror_To_MireArea))]
        [MemberData(nameof(DWMirePortal_To_MireArea))]
        [MemberData(nameof(MireArea_To_MireAreaMirror))]
        [MemberData(nameof(MireArea_To_MireAreaNotBunny))]
        [MemberData(nameof(MireAreaNotBunny_To_MireAreaNotBunnyOrSuperBunnyMirror))]
        [MemberData(nameof(MireArea_To_MireAreaNotBunnyOrSuperBunnyMirror))]
        [MemberData(nameof(MireAreaNotBunny_To_MiseryMireEntrance))]
        [MemberData(nameof(FluteInverted_To_DWMirePortal))]
        [MemberData(nameof(LWMirePortalStandardOpen_To_DWMirePortal))]
        [MemberData(nameof(DWMirePortal_To_DWMirePortalInverted))]
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

        public static IEnumerable<object[]> LightWorldMirror_To_MireArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.MireArea,
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
                    RequirementNodeID.MireArea,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWMirePortal_To_MireArea =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.MireArea,
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
                        RequirementNodeID.DWMirePortal
                    },
                    RequirementNodeID.MireArea,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MireArea_To_MireAreaMirror =>
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
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaMirror,
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
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaMirror,
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
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaMirror,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MireArea_To_MireAreaNotBunny =>
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
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaNotBunny,
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
                    RequirementNodeID.MireAreaNotBunny,
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
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaNotBunny,
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
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaNotBunny,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MireAreaNotBunny_To_MireAreaNotBunnyOrSuperBunnyMirror =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror,
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
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MireArea_To_MireAreaNotBunnyOrSuperBunnyMirror =>
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
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror,
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
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror,
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
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror,
                    AccessibilityLevel.SequenceBreak
                }
            };

        public static IEnumerable<object[]> MireAreaNotBunny_To_MiseryMireEntrance =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 2),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 2),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 2)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 2),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 2),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 2)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 1),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 3),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 1),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 3),
                        (ItemType.Quake, 0),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 0),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 0),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2),
                        (ItemType.Bombos, 1),
                        (ItemType.BombosDungeons, 0),
                        (ItemType.Ether, 1),
                        (ItemType.EtherDungeons, 0),
                        (ItemType.Quake, 1),
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MiseryMireEntrance,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> FluteInverted_To_DWMirePortal =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData(),
                    new (ItemType, int)[0],
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.DWMirePortal,
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
                        RequirementNodeID.FluteInverted
                    },
                    RequirementNodeID.DWMirePortal,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> LWMirePortalStandardOpen_To_DWMirePortal =>
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
                        RequirementNodeID.LWMirePortalStandardOpen
                    },
                    RequirementNodeID.DWMirePortal,
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
                        RequirementNodeID.LWMirePortalStandardOpen
                    },
                    RequirementNodeID.DWMirePortal,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> DWMirePortal_To_DWMirePortalInverted =>
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
                        RequirementNodeID.DWMirePortal
                    },
                    RequirementNodeID.DWMirePortalInverted,
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
                        RequirementNodeID.DWMirePortal
                    },
                    RequirementNodeID.DWMirePortalInverted,
                    AccessibilityLevel.Normal
                }
            };
    }
}
