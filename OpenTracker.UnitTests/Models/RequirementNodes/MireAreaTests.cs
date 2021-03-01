using System.Collections.Generic;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.RequirementNodes
{
    public class MireAreaTests : RequirementNodeTestBase
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
        public override void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            base.Tests(
                modeData, items, prizes, sequenceBreaks, accessibleNodes, id, towerCrystalsKnown, expected);
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
                        RequirementNodeID.LightWorldMirror
                    },
                    RequirementNodeID.MireArea,
                    false,
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
                        RequirementNodeID.DWMirePortal
                    },
                    RequirementNodeID.MireArea,
                    false,
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
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaMirror,
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
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaMirror,
                    false,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[0],
                    RequirementNodeID.MireAreaNotBunny,
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
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaNotBunny,
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
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    new (SequenceBreakType, bool)[0],
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaNotBunny,
                    false,
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
                        RequirementNodeID.MireAreaNotBunny
                    },
                    RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror,
                    false,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, false)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror,
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
                    new (SequenceBreakType, bool)[]
                    {
                        (SequenceBreakType.SuperBunnyMirror, true)
                    },
                    new RequirementNodeID[]
                    {
                        RequirementNodeID.MireArea
                    },
                    RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror,
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                    false,
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
                        RequirementNodeID.FluteInverted
                    },
                    RequirementNodeID.DWMirePortal,
                    false,
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
                        RequirementNodeID.LWMirePortalStandardOpen
                    },
                    RequirementNodeID.DWMirePortal,
                    false,
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
                        RequirementNodeID.DWMirePortal
                    },
                    RequirementNodeID.DWMirePortalInverted,
                    false,
                    AccessibilityLevel.Normal
                }
            };
    }
}
