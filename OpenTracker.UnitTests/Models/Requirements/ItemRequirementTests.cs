using System.Collections.Generic;
using Autofac;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements
{
    [Collection("Tests")]
    public class ItemRequirementTests
    {
        [Theory]
        [MemberData(nameof(Swordless))]
        [MemberData(nameof(Sword1))]
        [MemberData(nameof(Sword2))]
        [MemberData(nameof(Sword3))]
        [MemberData(nameof(Bow))]
        [MemberData(nameof(Boomerang))]
        [MemberData(nameof(RedBoomerang))]
        [MemberData(nameof(Hookshot))]
        [MemberData(nameof(Powder))]
        [MemberData(nameof(Mushroom))]
        [MemberData(nameof(Boots))]
        [MemberData(nameof(FireRod))]
        [MemberData(nameof(IceRod))]
        [MemberData(nameof(Bombos))]
        [MemberData(nameof(BombosMM))]
        [MemberData(nameof(BombosTR))]
        [MemberData(nameof(BombosBoth))]
        [MemberData(nameof(Ether))]
        [MemberData(nameof(EtherMM))]
        [MemberData(nameof(EtherTR))]
        [MemberData(nameof(EtherBoth))]
        [MemberData(nameof(Quake))]
        [MemberData(nameof(QuakeMM))]
        [MemberData(nameof(QuakeTR))]
        [MemberData(nameof(QuakeBoth))]
        [MemberData(nameof(Gloves1))]
        [MemberData(nameof(Gloves2))]
        [MemberData(nameof(Lamp))]
        [MemberData(nameof(Hammer))]
        [MemberData(nameof(Flute))]
        [MemberData(nameof(FluteActivated))]
        [MemberData(nameof(Net))]
        [MemberData(nameof(Book))]
        [MemberData(nameof(Shovel))]
        [MemberData(nameof(NoFlippers))]
        [MemberData(nameof(Flippers))]
        [MemberData(nameof(Bottle))]
        [MemberData(nameof(CaneOfSomaria))]
        [MemberData(nameof(CaneOfByrna))]
        [MemberData(nameof(Cape))]
        [MemberData(nameof(Mirror))]
        [MemberData(nameof(HalfMagic))]
        [MemberData(nameof(MoonPearl))]
        [MemberData(nameof(Aga1))]
        [MemberData(nameof(Aga2))]
        [MemberData(nameof(RedCrystal))]
        [MemberData(nameof(Pendant))]
        [MemberData(nameof(GreenPendant))]
        public void AccessibilityTests(
            RequirementType type, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            AccessibilityLevel expected)
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var itemDictionary = scope.Resolve<IItemDictionary>();
            var prizeDictionary = scope.Resolve<IPrizeDictionary>();
            var requirements = scope.Resolve<IRequirementDictionary>();
            
            foreach (var item in items)
            {
                itemDictionary[item.Item1].Current = item.Item2;
            }
    
            foreach (var prize in prizes)
            {
                prizeDictionary[prize.Item1].Current = prize.Item2;
            }
    
            Assert.Equal(expected, requirements[type].Accessibility);
        }
    
        public static IEnumerable<object[]> Swordless =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Swordless,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Swordless,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Sword1 =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Sword1,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Sword1,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Sword1,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Sword2 =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Sword2,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Sword2,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Sword2,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Sword2,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Sword3 =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Sword3,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Sword3,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Sword3,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Sword3,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 3)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Sword3,
                    new (ItemType, int)[]
                    {
                        (ItemType.Sword, 4)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Bow =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Bow,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Bow,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bow, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Boomerang =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Boomerang,
                    new (ItemType, int)[]
                    {
                        (ItemType.Boomerang, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Boomerang,
                    new (ItemType, int)[]
                    {
                        (ItemType.Boomerang, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> RedBoomerang =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.RedBoomerang,
                    new (ItemType, int)[]
                    {
                        (ItemType.RedBoomerang, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.RedBoomerang,
                    new (ItemType, int)[]
                    {
                        (ItemType.RedBoomerang, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Hookshot =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Hookshot,
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Hookshot,
                    new (ItemType, int)[]
                    {
                        (ItemType.Hookshot, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Powder =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Powder,
                    new (ItemType, int)[]
                    {
                        (ItemType.Powder, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Powder,
                    new (ItemType, int)[]
                    {
                        (ItemType.Powder, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Mushroom =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Mushroom,
                    new (ItemType, int)[]
                    {
                        (ItemType.Mushroom, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Mushroom,
                    new (ItemType, int)[]
                    {
                        (ItemType.Mushroom, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Mushroom,
                    new (ItemType, int)[]
                    {
                        (ItemType.Mushroom, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Boots =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Boots,
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Boots,
                    new (ItemType, int)[]
                    {
                        (ItemType.Boots, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> FireRod =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.FireRod,
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.FireRod,
                    new (ItemType, int)[]
                    {
                        (ItemType.FireRod, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> IceRod =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.IceRod,
                    new (ItemType, int)[]
                    {
                        (ItemType.IceRod, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.IceRod,
                    new (ItemType, int)[]
                    {
                        (ItemType.IceRod, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Bombos =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Bombos,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bombos, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Bombos,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bombos, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> BombosMM =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.BombosMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.BombosMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.BombosMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.BombosMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> BombosTR =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.BombosTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.BombosTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.BombosTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.BombosTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> BombosBoth =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.BombosBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.BombosBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.BombosBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.BombosBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.BombosDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Ether =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Ether,
                    new (ItemType, int)[]
                    {
                        (ItemType.Ether, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Ether,
                    new (ItemType, int)[]
                    {
                        (ItemType.Ether, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EtherMM =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.EtherMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.EtherMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.EtherMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.EtherMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EtherTR =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.EtherTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.EtherTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.EtherTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.EtherTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EtherBoth =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.EtherBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.EtherBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.EtherBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.EtherBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.EtherDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Quake =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Quake,
                    new (ItemType, int)[]
                    {
                        (ItemType.Quake, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Quake,
                    new (ItemType, int)[]
                    {
                        (ItemType.Quake, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> QuakeMM =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.QuakeMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.QuakeMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.QuakeMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.QuakeMM,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> QuakeTR =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.QuakeTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.QuakeTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.QuakeTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.QuakeTR,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> QuakeBoth =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.QuakeBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.QuakeBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.QuakeBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.QuakeBoth,
                    new (ItemType, int)[]
                    {
                        (ItemType.QuakeDungeons, 3)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Gloves1 =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Gloves1,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Gloves1,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Gloves2 =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Gloves2,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Gloves2,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Gloves2,
                    new (ItemType, int)[]
                    {
                        (ItemType.Gloves, 2)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Lamp =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Lamp,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Lamp,
                    new (ItemType, int)[]
                    {
                        (ItemType.Lamp, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Hammer =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Hammer,
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Hammer,
                    new (ItemType, int)[]
                    {
                        (ItemType.Hammer, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Flute =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Flute,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Flute,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flute, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> FluteActivated =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.FluteActivated,
                    new (ItemType, int)[]
                    {
                        (ItemType.FluteActivated, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.FluteActivated,
                    new (ItemType, int)[]
                    {
                        (ItemType.FluteActivated, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Net =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Net,
                    new (ItemType, int)[]
                    {
                        (ItemType.Net, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Net,
                    new (ItemType, int)[]
                    {
                        (ItemType.Net, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Book =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Book,
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Book,
                    new (ItemType, int)[]
                    {
                        (ItemType.Book, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Shovel =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Shovel,
                    new (ItemType, int)[]
                    {
                        (ItemType.Shovel, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Shovel,
                    new (ItemType, int)[]
                    {
                        (ItemType.Shovel, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> NoFlippers =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.NoFlippers,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.NoFlippers,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Flippers =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Flippers,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Flippers,
                    new (ItemType, int)[]
                    {
                        (ItemType.Flippers, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Bottle =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Bottle,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bottle, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Bottle,
                    new (ItemType, int)[]
                    {
                        (ItemType.Bottle, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> CaneOfSomaria =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.CaneOfSomaria,
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.CaneOfSomaria,
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfSomaria, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> CaneOfByrna =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.CaneOfByrna,
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfByrna, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.CaneOfByrna,
                    new (ItemType, int)[]
                    {
                        (ItemType.CaneOfByrna, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Cape =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Cape,
                    new (ItemType, int)[]
                    {
                        (ItemType.Cape, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Cape,
                    new (ItemType, int)[]
                    {
                        (ItemType.Cape, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Mirror =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Mirror,
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Mirror,
                    new (ItemType, int)[]
                    {
                        (ItemType.Mirror, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> HalfMagic =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.HalfMagic,
                    new (ItemType, int)[]
                    {
                        (ItemType.HalfMagic, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.HalfMagic,
                    new (ItemType, int)[]
                    {
                        (ItemType.HalfMagic, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> MoonPearl =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.MoonPearl,
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 0)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.MoonPearl,
                    new (ItemType, int)[]
                    {
                        (ItemType.MoonPearl, 1)
                    },
                    new (PrizeType, int)[0],
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Aga1 =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Aga1,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga1, 0)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Aga1,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga1, 1)
                    },
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Aga2 =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Aga2,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga2, 0)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Aga2,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Aga2, 1)
                    },
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> RedCrystal =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.RedCrystal,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 0)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.RedCrystal,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 1)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.RedCrystal,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.RedCrystal, 2)
                    },
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> Pendant =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.Pendant,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Pendant, 0)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Pendant,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Pendant, 1)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.Pendant,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.Pendant, 2)
                    },
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> GreenPendant =>
            new List<object[]>
            {
                new object[]
                {
                    RequirementType.GreenPendant,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.GreenPendant, 0)
                    },
                    AccessibilityLevel.None
                },
                new object[]
                {
                    RequirementType.GreenPendant,
                    new (ItemType, int)[0],
                    new (PrizeType, int)[]
                    {
                        (PrizeType.GreenPendant, 1)
                    },
                    AccessibilityLevel.Normal
                }
            };
    }
}
