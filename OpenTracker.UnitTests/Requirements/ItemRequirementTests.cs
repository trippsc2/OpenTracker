using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Requirements
{
    [Collection("Tests")]
    public class ItemRequirementTests
    {
        [Theory]
        [MemberData(nameof(Sword_Data))]
        [MemberData(nameof(Aga1_Data))]
        [MemberData(nameof(Bow_Data))]
        [MemberData(nameof(Boomerang_Data))]
        [MemberData(nameof(RedBoomerang_Data))]
        [MemberData(nameof(Hookshot_Data))]
        [MemberData(nameof(Powder_Data))]
        [MemberData(nameof(Mushroom_Data))]
        [MemberData(nameof(Boots_Data))]
        [MemberData(nameof(FireRod_Data))]
        [MemberData(nameof(IceRod_Data))]
        [MemberData(nameof(Bombos_Data))]
        [MemberData(nameof(BombosDungeon_Data))]
        [MemberData(nameof(Ether_Data))]
        [MemberData(nameof(EtherDungeon_Data))]
        [MemberData(nameof(Quake_Data))]
        [MemberData(nameof(QuakeDungeon_Data))]
        [MemberData(nameof(Gloves_Data))]
        [MemberData(nameof(Lamp_Data))]
        [MemberData(nameof(Hammer_Data))]
        [MemberData(nameof(Flute_Data))]
        [MemberData(nameof(Net_Data))]
        [MemberData(nameof(Book_Data))]
        [MemberData(nameof(Shovel_Data))]
        [MemberData(nameof(Flippers_Data))]
        [MemberData(nameof(Bottle_Data))]
        [MemberData(nameof(CaneOfSomaria_Data))]
        [MemberData(nameof(CaneOfByrna_Data))]
        [MemberData(nameof(Cape_Data))]
        [MemberData(nameof(Mirror_Data))]
        [MemberData(nameof(HalfMagic_Data))]
        [MemberData(nameof(MoonPearl_Data))]
        [MemberData(nameof(Aga2_Data))]
        [MemberData(nameof(RedCrystal_Data))]
        [MemberData(nameof(Pendant_Data))]
        [MemberData(nameof(GreenPendant_Data))]
        public void AccessibilityTests(
            (ItemType, int)[] items, RequirementType type, AccessibilityLevel expected)
        {
            ItemDictionary.Instance.Reset();

            foreach (var item in items)
            {
                ItemDictionary.Instance[item.Item1].Current = item.Item2;
            }

            Assert.Equal(expected, RequirementDictionary.Instance[type].Accessibility);
        }

        public static IEnumerable<object[]> Sword_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Swordless,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    RequirementType.Swordless,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    RequirementType.Swordless,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2)
                    },
                    RequirementType.Swordless,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3)
                    },
                    RequirementType.Swordless,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 4)
                    },
                    RequirementType.Swordless,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 5)
                    },
                    RequirementType.Swordless,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Sword1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    RequirementType.Sword1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    RequirementType.Sword1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2)
                    },
                    RequirementType.Sword1,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3)
                    },
                    RequirementType.Sword1,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 4)
                    },
                    RequirementType.Sword1,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 5)
                    },
                    RequirementType.Sword1,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Sword2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    RequirementType.Sword2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    RequirementType.Sword2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2)
                    },
                    RequirementType.Sword2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3)
                    },
                    RequirementType.Sword2,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 4)
                    },
                    RequirementType.Sword2,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 5)
                    },
                    RequirementType.Sword2,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Sword3,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    RequirementType.Sword3,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    RequirementType.Sword3,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2)
                    },
                    RequirementType.Sword3,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3)
                    },
                    RequirementType.Sword3,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 4)
                    },
                    RequirementType.Sword3,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 5)
                    },
                    RequirementType.Sword3,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Aga1_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Aga1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 0)
                    },
                    RequirementType.Aga1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga1, 1)
                    },
                    RequirementType.Aga1,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Bow_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Bow,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0)
                    },
                    RequirementType.Bow,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1)
                    },
                    RequirementType.Bow,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Boomerang_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Boomerang,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Boomerang, 0)
                    },
                    RequirementType.Boomerang,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Boomerang, 1)
                    },
                    RequirementType.Boomerang,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> RedBoomerang_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.RedBoomerang,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.RedBoomerang, 0)
                    },
                    RequirementType.RedBoomerang,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.RedBoomerang, 1)
                    },
                    RequirementType.RedBoomerang,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Hookshot_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Hookshot,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0)
                    },
                    RequirementType.Hookshot,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1)
                    },
                    RequirementType.Hookshot,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Powder_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Powder,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Powder, 0)
                    },
                    RequirementType.Powder,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Powder, 1)
                    },
                    RequirementType.Powder,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Mushroom_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Mushroom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Mushroom, 0)
                    },
                    RequirementType.Mushroom,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Mushroom, 1)
                    },
                    RequirementType.Mushroom,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Mushroom, 2)
                    },
                    RequirementType.Mushroom,
                    AccessibilityLevel.None
                }
            };

        public static IEnumerable<object[]> Boots_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Boots,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 0)
                    },
                    RequirementType.Boots,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 1)
                    },
                    RequirementType.Boots,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> FireRod_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.FireRod,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 0)
                    },
                    RequirementType.FireRod,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    RequirementType.FireRod,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> IceRod_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.IceRod,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.IceRod, 0)
                    },
                    RequirementType.IceRod,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.IceRod, 1)
                    },
                    RequirementType.IceRod,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Bombos_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Bombos,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Bombos, 0)
                    },
                    RequirementType.Bombos,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Bombos, 1)
                    },
                    RequirementType.Bombos,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> BombosDungeon_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.BombosMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 0)
                    },
                    RequirementType.BombosMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 1)
                    },
                    RequirementType.BombosMM,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 2)
                    },
                    RequirementType.BombosMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 3)
                    },
                    RequirementType.BombosMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.BombosTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 0)
                    },
                    RequirementType.BombosTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 1)
                    },
                    RequirementType.BombosTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 2)
                    },
                    RequirementType.BombosTR,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 3)
                    },
                    RequirementType.BombosTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.BombosBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 0)
                    },
                    RequirementType.BombosBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 1)
                    },
                    RequirementType.BombosBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 2)
                    },
                    RequirementType.BombosBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 3)
                    },
                    RequirementType.BombosBoth,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Ether_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Ether,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Ether, 0)
                    },
                    RequirementType.Ether,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Ether, 1)
                    },
                    RequirementType.Ether,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> EtherDungeon_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.EtherMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 0)
                    },
                    RequirementType.EtherMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 1)
                    },
                    RequirementType.EtherMM,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 2)
                    },
                    RequirementType.EtherMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 3)
                    },
                    RequirementType.EtherMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.EtherTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 0)
                    },
                    RequirementType.EtherTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 1)
                    },
                    RequirementType.EtherTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 2)
                    },
                    RequirementType.EtherTR,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 3)
                    },
                    RequirementType.EtherTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.EtherBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 0)
                    },
                    RequirementType.EtherBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 1)
                    },
                    RequirementType.EtherBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 2)
                    },
                    RequirementType.EtherBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 3)
                    },
                    RequirementType.EtherBoth,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Quake_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Quake,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Quake, 0)
                    },
                    RequirementType.Quake,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Quake, 1)
                    },
                    RequirementType.Quake,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> QuakeDungeon_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.QuakeMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 0)
                    },
                    RequirementType.QuakeMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 1)
                    },
                    RequirementType.QuakeMM,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 2)
                    },
                    RequirementType.QuakeMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 3)
                    },
                    RequirementType.QuakeMM,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.QuakeTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 0)
                    },
                    RequirementType.QuakeTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 1)
                    },
                    RequirementType.QuakeTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 2)
                    },
                    RequirementType.QuakeTR,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 3)
                    },
                    RequirementType.QuakeTR,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.QuakeBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 0)
                    },
                    RequirementType.QuakeBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 1)
                    },
                    RequirementType.QuakeBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 2)
                    },
                    RequirementType.QuakeBoth,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 3)
                    },
                    RequirementType.QuakeBoth,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Gloves_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Gloves1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
                    },
                    RequirementType.Gloves1,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    RequirementType.Gloves1,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2)
                    },
                    RequirementType.Gloves1,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Gloves2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
                    },
                    RequirementType.Gloves2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    RequirementType.Gloves2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2)
                    },
                    RequirementType.Gloves2,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Lamp_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Lamp,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    RequirementType.Lamp,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    RequirementType.Lamp,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Hammer_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Hammer,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    RequirementType.Hammer,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    RequirementType.Hammer,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Flute_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Flute,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 0)
                    },
                    RequirementType.Flute,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1)
                    },
                    RequirementType.Flute,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Net_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Net,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Net, 0)
                    },
                    RequirementType.Net,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Net, 1)
                    },
                    RequirementType.Net,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Book_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Book,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 0)
                    },
                    RequirementType.Book,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 1)
                    },
                    RequirementType.Book,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Shovel_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Shovel,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Shovel, 0)
                    },
                    RequirementType.Shovel,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Shovel, 1)
                    },
                    RequirementType.Shovel,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Flippers_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.NoFlippers,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    RequirementType.NoFlippers,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    RequirementType.NoFlippers,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Flippers,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    RequirementType.Flippers,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    RequirementType.Flippers,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Bottle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Bottle,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Bottle, 0)
                    },
                    RequirementType.Bottle,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Bottle, 1)
                    },
                    RequirementType.Bottle,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> CaneOfSomaria_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.CaneOfSomaria,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    RequirementType.CaneOfSomaria,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    RequirementType.CaneOfSomaria,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> CaneOfByrna_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.CaneOfByrna,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfByrna, 0)
                    },
                    RequirementType.CaneOfByrna,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfByrna, 1)
                    },
                    RequirementType.CaneOfByrna,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Cape_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Cape,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Cape, 0)
                    },
                    RequirementType.Cape,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Cape, 1)
                    },
                    RequirementType.Cape,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Mirror_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Mirror,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 0)
                    },
                    RequirementType.Mirror,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    RequirementType.Mirror,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> HalfMagic_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.HalfMagic,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.HalfMagic, 0)
                    },
                    RequirementType.HalfMagic,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.HalfMagic, 1)
                    },
                    RequirementType.HalfMagic,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> MoonPearl_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.MoonPearl,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0)
                    },
                    RequirementType.MoonPearl,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1)
                    },
                    RequirementType.MoonPearl,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> Aga2_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Aga2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga2, 0)
                    },
                    RequirementType.Aga2,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Aga2, 1)
                    },
                    RequirementType.Aga2,
                    AccessibilityLevel.Normal
                }
            };

        public static IEnumerable<object[]> RedCrystal_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.RedCrystal,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.RedCrystal, 0)
                    },
                    RequirementType.RedCrystal,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.RedCrystal, 1)
                    },
                    RequirementType.RedCrystal,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.RedCrystal, 2)
                    },
                    RequirementType.RedCrystal,
                    AccessibilityLevel.Normal
                },
            };

        public static IEnumerable<object[]> Pendant_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.Pendant,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Pendant, 0)
                    },
                    RequirementType.Pendant,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Pendant, 1)
                    },
                    RequirementType.Pendant,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.Pendant, 2)
                    },
                    RequirementType.Pendant,
                    AccessibilityLevel.Normal
                },
            };

        public static IEnumerable<object[]> GreenPendant_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new (ItemType, int)[0],
                    RequirementType.GreenPendant,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.GreenPendant, 0)
                    },
                    RequirementType.GreenPendant,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new (ItemType, int)[]
                    {
                        (ItemType.GreenPendant, 1)
                    },
                    RequirementType.GreenPendant,
                    AccessibilityLevel.Normal
                }
            };
    }
}
