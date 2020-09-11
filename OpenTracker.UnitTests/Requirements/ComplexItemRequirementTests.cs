using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Requirements
{
    [Collection("Tests")]
    public class ComplexItemRequirementTests
    {
        [Theory]
        [MemberData(nameof(ComplexItem_Data))]
        public void AccessibilityTests(
            ModeSaveData mode, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, (LocationID, int)[] smallKeys,
            (LocationID, int)[] bigKeys, RequirementNodeID[] accessibleNodes, RequirementType type,
            AccessibilityLevel expected)
        {
            Mode.Instance.Load(mode);
            ItemDictionary.Instance.Reset();
            LocationDictionary.Instance.Reset();
            PrizeDictionary.Instance.Reset();
            SequenceBreakDictionary.Instance.Reset();
            RequirementNodeDictionary.Instance.Reset();

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

            foreach (var smallKey in smallKeys)
            {
                ((IDungeon)LocationDictionary.Instance[smallKey.Item1]).SmallKeyItem.Current =
                    smallKey.Item2;
            }

            foreach (var bigKey in bigKeys)
            {
                ((IDungeon)LocationDictionary.Instance[bigKey.Item1]).BigKeyItem.Current =
                    bigKey.Item2;
            }

            foreach (var accessibleNode in accessibleNodes)
            {
                RequirementNodeDictionary.Instance[accessibleNode].AlwaysAccessible = true;
            }

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<object[]> GetRequirementTests(RequirementType type)
        {
            switch (type)
            {
                case RequirementType.AllMedallions:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bombos, 0),
                                    (ItemType.Ether, 0),
                                    (ItemType.Quake, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bombos, 1),
                                    (ItemType.Ether, 0),
                                    (ItemType.Quake, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bombos, 0),
                                    (ItemType.Ether, 1),
                                    (ItemType.Quake, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bombos, 0),
                                    (ItemType.Ether, 0),
                                    (ItemType.Quake, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bombos, 1),
                                    (ItemType.Ether, 1),
                                    (ItemType.Quake, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bombos, 1),
                                    (ItemType.Ether, 0),
                                    (ItemType.Quake, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bombos, 0),
                                    (ItemType.Ether, 1),
                                    (ItemType.Quake, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bombos, 1),
                                    (ItemType.Ether, 1),
                                    (ItemType.Quake, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.ExtendMagic1:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.ExtendMagic2:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.FireRodDarkRoom:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.UseMedallion:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 2)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.MeltThings:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bombos, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Sword, 0),
                                    (ItemType.Bombos, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Bombos, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.NotBunnyLW:
                    {
                        return new List<object[]>
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.NotBunnyDW:
                    {
                        return new List<object[]>
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.ATBarrier:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Cape, 0),
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Cape, 0),
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Cape, 0),
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Cape, 0),
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Cape, 0),
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Cape, 1),
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.BombDuplicationAncillaOverload:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bow, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombDuplicationAncillaOverload, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.RedBoomerang, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombDuplicationAncillaOverload, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.RedBoomerang, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombDuplicationAncillaOverload, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.RedBoomerang, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombDuplicationAncillaOverload, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.RedBoomerang, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombDuplicationAncillaOverload, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.BombDuplicationMirror:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 0),
                                    (ItemType.Mirror, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombDuplicationMirror, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 1),
                                    (ItemType.Mirror, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombDuplicationMirror, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 0),
                                    (ItemType.Mirror, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombDuplicationMirror, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 1),
                                    (ItemType.Mirror, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombDuplicationMirror, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 1),
                                    (ItemType.Mirror, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombDuplicationMirror, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.BombJumpPoDHammerJump:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombJumpPoDHammerJump, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombJumpPoDHammerJump, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.BombJumpSWBigChest:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombJumpSWBigChest, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombJumpSWBigChest, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.BombJumpIPBJ:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombJumpIPBJ, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombJumpIPBJ, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.BombJumpIPHookshotGap:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombJumpIPHookshotGap, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombJumpIPHookshotGap, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.BombJumpIPFreezorRoomGap:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombJumpIPFreezorRoomGap, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BombJumpIPFreezorRoomGap, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.BonkOverLedge:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Boots, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BonkOverLedge, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Boots, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BonkOverLedge, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Boots, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BonkOverLedge, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Boots, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BonkOverLedge, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Boots, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BonkOverLedge, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.BumperCaveGap:
                    {
                        return new List<object[]>
                        {
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BumperCaveHookshot, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BumperCaveHookshot, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BumperCaveHookshot, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BumperCaveHookshot, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.CameraUnlock:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bottle, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.CameraUnlock, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.CameraUnlock, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.CameraUnlock, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.Curtains:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 2)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DarkRoomDeathMountainEntry:
                    {
                        return new List<object[]>
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
                                    (SequenceBreakType.DarkRoomDeathMountainEntry, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DarkRoomDeathMountainExit:
                    {
                        return new List<object[]>
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
                                    (SequenceBreakType.DarkRoomDeathMountainExit, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DarkRoomHC:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomHC, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomHC, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomHC, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomHC, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomHC, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomHC, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 1),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomHC, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomHC, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 1),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomHC, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DarkRoomAT:
                    {
                        return new List<object[]>
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
                                    (SequenceBreakType.DarkRoomAT, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomAT, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomAT, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DarkRoomEPRight:
                    {
                        return new List<object[]>
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
                                    (SequenceBreakType.DarkRoomEPRight, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomEPRight, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomEPRight, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DarkRoomEPBack:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomEPBack, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomEPBack, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomEPBack, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomEPBack, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomEPBack, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomEPBack, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 1),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomEPBack, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomEPBack, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 1),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomEPBack, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DarkRoomPoDDarkBasement:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomPoDDarkBasement, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomPoDDarkBasement, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomPoDDarkBasement, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomPoDDarkBasement, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomPoDDarkBasement, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomPoDDarkBasement, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 1),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomPoDDarkBasement, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomPoDDarkBasement, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Lamp, 1),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.DarkRoomPoDDarkBasement, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DarkRoomPoDDarkMaze:
                    {
                        return new List<object[]>
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
                                    (SequenceBreakType.DarkRoomPoDDarkMaze, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomPoDDarkMaze, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomPoDDarkMaze, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DarkRoomPoDBossArea:
                    {
                        return new List<object[]>
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
                                    (SequenceBreakType.DarkRoomPoDBossArea, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomPoDBossArea, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomPoDBossArea, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DarkRoomMM:
                    {
                        return new List<object[]>
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
                                    (SequenceBreakType.DarkRoomMM, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomMM, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomMM, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DarkRoomTR:
                    {
                        return new List<object[]>
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
                                    (SequenceBreakType.DarkRoomTR, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomTR, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (SequenceBreakType.DarkRoomTR, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DungeonRevive:
                    {
                        return new List<object[]>
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.FakeFlippersFairyRevival:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bottle, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersFairyRevival, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersFairyRevival, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersFairyRevival, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.FakeFlippersQirnJump:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersQirnJump, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersQirnJump, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.FakeFlippersScreenTransition:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersScreenTransition, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersScreenTransition, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.FakeFlippersSplashDeletion:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bow, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersSplashDeletion, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bow, 1),
                                    (ItemType.RedBoomerang, 1),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersSplashDeletion, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bow, 1),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersSplashDeletion, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bow, 0),
                                    (ItemType.RedBoomerang, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersSplashDeletion, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bow, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersSplashDeletion, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bow, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.FakeFlippersSplashDeletion, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.FireSource:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 1),
                                    (ItemType.FireRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Lamp, 0),
                                    (ItemType.FireRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Hover:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Boots, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.Hover, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.Hover, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.Hover, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.LaserBridge:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Shield, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TRLaserSkip, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Shield, 2)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TRLaserSkip, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Shield, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TRLaserSkip, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Shield, 2)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TRLaserSkip, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Shield, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TRLaserSkip, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Cape, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Shield, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TRLaserSkip, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.Shield, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TRLaserSkip, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Shield, 3)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TRLaserSkip, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.MagicBat:
                    {
                        return new List<object[]>
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Pedestal:
                    {
                        return new List<object[]>
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
                                    (PrizeType.Pendant, 0),
                                    (PrizeType.GreenPendant, 0)
                                },
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindPedestal, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Book, 0)
                                },
                                new (PrizeType, int)[]
                                {
                                    (PrizeType.Pendant, 1),
                                    (PrizeType.GreenPendant, 1)
                                },
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindPedestal, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (PrizeType.Pendant, 0),
                                    (PrizeType.GreenPendant, 0)
                                },
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindPedestal, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (PrizeType.Pendant, 1),
                                    (PrizeType.GreenPendant, 1)
                                },
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindPedestal, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (PrizeType.Pendant, 2),
                                    (PrizeType.GreenPendant, 1)
                                },
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindPedestal, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (PrizeType.Pendant, 0),
                                    (PrizeType.GreenPendant, 0)
                                },
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindPedestal, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (PrizeType.Pendant, 0),
                                    (PrizeType.GreenPendant, 0)
                                },
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindPedestal, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (PrizeType.Pendant, 2),
                                    (PrizeType.GreenPendant, 1)
                                },
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindPedestal, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (PrizeType.Pendant, 2),
                                    (PrizeType.GreenPendant, 1)
                                },
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindPedestal, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (PrizeType.Pendant, 2),
                                    (PrizeType.GreenPendant, 1)
                                },
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindPedestal, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.RedEyegoreGoriya:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EnemyShuffle = false
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EnemyShuffle = false
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bow, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EnemyShuffle = true
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.SuperBunnyFallInHole:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.SuperBunnyFallInHole, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[0],
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.SuperBunnyFallInHole, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.SuperBunnyMirror:
                    {
                        return new List<object[]>
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.Tablet:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Book, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Book, 0),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Book, 0),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Book, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Inspect
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Book, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Inspect
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Book, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Inspect
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Book, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Inspect
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Book, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Book, 1),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Torch:
                    {
                        return new List<object[]>
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Inspect
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.ToHHerapot:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ToHHerapot, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ToHHerapot, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ToHHerapot, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.IPIceBreaker:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.CaneOfSomaria, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.IPIceBreaker, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.IPIceBreaker, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.IPIceBreaker, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.MMMedallion:
                    {
                        return new List<object[]>
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.TRMedallion:
                    {
                        return new List<object[]>
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.EtherDungeons, 1),
                                    (ItemType.Quake, 0),
                                    (ItemType.QuakeDungeons, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.QuakeDungeons, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.EtherDungeons, 1),
                                    (ItemType.Quake, 0),
                                    (ItemType.QuakeDungeons, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.QuakeDungeons, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.EtherDungeons, 2),
                                    (ItemType.Quake, 0),
                                    (ItemType.QuakeDungeons, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.QuakeDungeons, 2)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.EtherDungeons, 2),
                                    (ItemType.Quake, 0),
                                    (ItemType.QuakeDungeons, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.QuakeDungeons, 2)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.TRKeyDoorsToMiddleExit:
                    {
                        return new List<object[]>
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 0)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 0)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 0)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 1)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 0)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 0)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 1)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 0)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 2)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 0)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 2)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 0)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 1)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[]
                                {
                                    (LocationID.TurtleRock, 2)
                                },
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.WaterWalk:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Boots, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalk, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalk, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalk, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.WaterWalkFromWaterfallCave:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EntranceShuffle = EntranceShuffle.None
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 0),
                                    (ItemType.MoonPearl, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EntranceShuffle = EntranceShuffle.None
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 1),
                                    (ItemType.MoonPearl, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EntranceShuffle = EntranceShuffle.None
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 0),
                                    (ItemType.MoonPearl, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EntranceShuffle = EntranceShuffle.Dungeon
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 0),
                                    (ItemType.MoonPearl, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EntranceShuffle = EntranceShuffle.Dungeon
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 1),
                                    (ItemType.MoonPearl, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EntranceShuffle = EntranceShuffle.Dungeon
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 0),
                                    (ItemType.MoonPearl, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EntranceShuffle = EntranceShuffle.All
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 0),
                                    (ItemType.MoonPearl, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalkFromWaterfallCave, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EntranceShuffle = EntranceShuffle.All
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 1),
                                    (ItemType.MoonPearl, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EntranceShuffle = EntranceShuffle.None
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 0),
                                    (ItemType.MoonPearl, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EntranceShuffle = EntranceShuffle.Dungeon
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 0),
                                    (ItemType.MoonPearl, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    EntranceShuffle = EntranceShuffle.All
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Flippers, 0),
                                    (ItemType.MoonPearl, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.WaterWalkFromWaterfallCave, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            }
                        };
                    }
                case RequirementType.LWMirror:
                    {
                        return new List<object[]>
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.DWMirror:
                    {
                        return new List<object[]>
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.SpikeCave:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.Cape, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Armos:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 1),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.Boomerang, 0),
                                    (ItemType.RedBoomerang, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Lanmolas:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.CaneOfSomaria, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.CaneOfSomaria, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Moldorm:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.HelmasaurKingSB:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 2)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.HelmasaurKing:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.HelmasaurKingBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.HelmasaurKingBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 2),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.HelmasaurKingBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 2),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.HelmasaurKingBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 2),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.HelmasaurKingBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.HelmasaurKingBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 3),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.HelmasaurKingBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.HelmasaurKingBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.ArrghusSB:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 0),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Arrghus:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 0),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 0),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 3),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.ArrghusBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.MothulaSB:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Mothula:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, false)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, false)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, false)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, false)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.MothulaBasic, true)
                                } ,
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                       };
                    }
                case RequirementType.BlindSB:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.CaneOfByrna, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Blind:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.Cape, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.CaneOfSomaria, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Cape, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.BlindBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.KholdstareSB:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Kholdstare:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 0),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 0),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 0),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.Bombos, 1),
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.KholdstareBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.VitreousSB:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hammer, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Vitreous:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.VitreousBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.VitreousBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hammer, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.VitreousBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.VitreousBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hammer, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.VitreousBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.VitreousBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hammer, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.VitreousBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 2),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.VitreousBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.VitreousBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 3),
                                    (ItemType.Bow, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.VitreousBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Hammer, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Bow, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.VitreousBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.TrinexxSB:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 4),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.Trinexx:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, false)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 4),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Sword, 4),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[]
                                {
                                    (SequenceBreakType.TrinexxBasic, true)
                                },
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.AgaBoss:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Net, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.None
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Net, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Net, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData(),
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Net, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
                case RequirementType.UnknownBoss:
                    {
                        return new List<object[]>
                        {
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.Hookshot, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.SequenceBreak
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Basic
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 1),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 0),
                                    (ItemType.IceRod, 0),
                                    (ItemType.Hookshot, 0),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 2),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            },
                            new object[]
                            {
                                new ModeSaveData()
                                {
                                    ItemPlacement = ItemPlacement.Advanced
                                },
                                new (ItemType, int)[]
                                {
                                    (ItemType.Sword, 4),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 1)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 0),
                                    (ItemType.Hammer, 1),
                                    (ItemType.Bow, 1),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Cape, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 1),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Cape, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 3),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 1),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 4),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Cape, 1),
                                    (ItemType.CaneOfByrna, 0),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
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
                                    (ItemType.Sword, 4),
                                    (ItemType.Hammer, 0),
                                    (ItemType.Bow, 0),
                                    (ItemType.FireRod, 1),
                                    (ItemType.IceRod, 1),
                                    (ItemType.Hookshot, 1),
                                    (ItemType.Bottle, 0),
                                    (ItemType.HalfMagic, 0),
                                    (ItemType.Cape, 0),
                                    (ItemType.CaneOfByrna, 1),
                                    (ItemType.Bombos, 0)
                                },
                                new (PrizeType, int)[0],
                                new (SequenceBreakType, bool)[0],
                                new (LocationID, int)[0],
                                new (LocationID, int)[0],
                                new RequirementNodeID[0],
                                type,
                                AccessibilityLevel.Normal
                            }
                        };
                    }
            }

            return new List<object[]>(0);
        }

        public static IEnumerable<object[]> ComplexItem_Data()
        {
            var result = new List<object[]>();

            for (int i = (int)RequirementType.AllMedallions;
                i <= (int)RequirementType.UnknownBoss; i++)
            {
                result.AddRange(GetRequirementTests((RequirementType)i));
            }

            return result;
        }
    }
}
