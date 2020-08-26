using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Requirements
{
    [Collection("Tests")]
    public class SmallKeyRequirementTests
    {
        [Theory]
        [MemberData(nameof(TRSmallKey2))]
        public void AccessibilityTests(
            RequirementType type, ModeSaveData mode, LocationID dungeonID, int dungeonKeys,
            int genericKeys, AccessibilityLevel expected)
        {
            Mode.Instance.Load(mode);
            ItemDictionary.Instance.Reset();
            LocationDictionary.Instance.Reset();
            ((IDungeon)LocationDictionary.Instance[dungeonID]).SmallKeyItem.Current = dungeonKeys;
            ItemDictionary.Instance[ItemType.SmallKey].Current = genericKeys;

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<object[]> TRSmallKey2 =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    LocationID.TurtleRock,
                    0,
                    0,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    LocationID.TurtleRock,
                    1,
                    0,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    LocationID.TurtleRock,
                    1,
                    1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    LocationID.TurtleRock,
                    0,
                    2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Retro
                    },
                    LocationID.TurtleRock,
                    0,
                    0,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Retro
                    },
                    LocationID.TurtleRock,
                    1,
                    0,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Retro
                    },
                    LocationID.TurtleRock,
                    0,
                    1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    LocationID.TurtleRock,
                    0,
                    0,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    LocationID.TurtleRock,
                    1,
                    0,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    LocationID.TurtleRock,
                    1,
                    1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    LocationID.TurtleRock,
                    0,
                    2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    LocationID.TurtleRock,
                    2,
                    0,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Retro
                    },
                    LocationID.TurtleRock,
                    2,
                    0,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Retro
                    },
                    LocationID.TurtleRock,
                    1,
                    1,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Retro
                    },
                    LocationID.TurtleRock,
                    0,
                    2,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    RequirementType.TRSmallKey2,
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    LocationID.TurtleRock,
                    2,
                    0,
                    AccessibilityLevel.Normal
                }
            };
    }
}
