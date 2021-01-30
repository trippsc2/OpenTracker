using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This is the class for creating key layouts.
    /// </summary>
    public static class KeyLayoutFactory
    {
        /// <summary>
        /// Returns the list of key layouts for the specified dungeon.
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon parent class.
        /// </param>
        /// <returns>
        /// The list of key layouts.
        /// </returns>
        public static List<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            if (dungeon == null)
            {
                throw new ArgumentNullException(nameof(dungeon));
            }

            switch (dungeon.ID)
            {
                case Locations.LocationID.HyruleCastle:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                new AlternativeRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnAllKeyShuffle],
                                    RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffSmallKeyShuffleOn]
                                })),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.HCSanctuary,
                                    DungeonItemID.HCMapChest,
                                    DungeonItemID.HCDarkCross,
                                    DungeonItemID.HCSecretRoomLeft,
                                    DungeonItemID.HCSecretRoomMiddle,
                                    DungeonItemID.HCSecretRoomRight
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffSmallKeyShuffleOff]),
                            new SmallKeyLayout(
                                3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.HCSanctuary,
                                    DungeonItemID.HCMapChest,
                                    DungeonItemID.HCDarkCross,
                                    DungeonItemID.HCSecretRoomLeft,
                                    DungeonItemID.HCSecretRoomMiddle,
                                    DungeonItemID.HCSecretRoomRight,
                                    DungeonItemID.HCMapGuardDrop
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.HCSanctuary,
                                            DungeonItemID.HCMapChest,
                                            DungeonItemID.HCBoomerangChest,
                                            DungeonItemID.HCDarkCross,
                                            DungeonItemID.HCSecretRoomLeft,
                                            DungeonItemID.HCSecretRoomMiddle,
                                            DungeonItemID.HCSecretRoomRight,
                                            DungeonItemID.HCMapGuardDrop,
                                            DungeonItemID.HCBoomerangGuardDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.HCSanctuary,
                                    DungeonItemID.HCMapChest,
                                    DungeonItemID.HCBoomerangChest,
                                    DungeonItemID.HCDarkCross,
                                    DungeonItemID.HCSecretRoomLeft,
                                    DungeonItemID.HCSecretRoomMiddle,
                                    DungeonItemID.HCSecretRoomRight,
                                    DungeonItemID.HCMapGuardDrop,
                                    DungeonItemID.HCBoomerangGuardDrop,
                                    DungeonItemID.HCBigKeyDrop
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.HCSanctuary,
                                    DungeonItemID.HCMapChest,
                                    DungeonItemID.HCDarkCross,
                                    DungeonItemID.HCSecretRoomLeft,
                                    DungeonItemID.HCSecretRoomMiddle,
                                    DungeonItemID.HCSecretRoomRight,
                                    DungeonItemID.HCMapGuardDrop
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.HCSanctuary,
                                            DungeonItemID.HCMapChest,
                                            DungeonItemID.HCDarkCross,
                                            DungeonItemID.HCSecretRoomLeft,
                                            DungeonItemID.HCSecretRoomMiddle,
                                            DungeonItemID.HCSecretRoomRight,
                                            DungeonItemID.HCMapGuardDrop
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.HCSanctuary,
                                                    DungeonItemID.HCMapChest,
                                                    DungeonItemID.HCBoomerangChest,
                                                    DungeonItemID.HCDarkCross,
                                                    DungeonItemID.HCSecretRoomLeft,
                                                    DungeonItemID.HCSecretRoomMiddle,
                                                    DungeonItemID.HCSecretRoomRight,
                                                    DungeonItemID.HCMapGuardDrop,
                                                    DungeonItemID.HCBoomerangGuardDrop
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.HCBoomerangChest,
                                    DungeonItemID.HCBoomerangGuardDrop
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.HCSanctuary,
                                            DungeonItemID.HCMapChest,
                                            DungeonItemID.HCDarkCross,
                                            DungeonItemID.HCSecretRoomLeft,
                                            DungeonItemID.HCSecretRoomMiddle,
                                            DungeonItemID.HCSecretRoomRight,
                                            DungeonItemID.HCMapGuardDrop
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.HCSanctuary,
                                                    DungeonItemID.HCMapChest,
                                                    DungeonItemID.HCBoomerangChest,
                                                    DungeonItemID.HCDarkCross,
                                                    DungeonItemID.HCSecretRoomLeft,
                                                    DungeonItemID.HCSecretRoomMiddle,
                                                    DungeonItemID.HCSecretRoomRight,
                                                    DungeonItemID.HCMapGuardDrop,
                                                    DungeonItemID.HCBoomerangGuardDrop
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.HCBigKeyDrop
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.HCSanctuary,
                                            DungeonItemID.HCMapChest,
                                            DungeonItemID.HCDarkCross,
                                            DungeonItemID.HCSecretRoomLeft,
                                            DungeonItemID.HCSecretRoomMiddle,
                                            DungeonItemID.HCSecretRoomRight,
                                            DungeonItemID.HCMapGuardDrop
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.HCSanctuary,
                                                    DungeonItemID.HCMapChest,
                                                    DungeonItemID.HCBoomerangChest,
                                                    DungeonItemID.HCDarkCross,
                                                    DungeonItemID.HCSecretRoomLeft,
                                                    DungeonItemID.HCSecretRoomMiddle,
                                                    DungeonItemID.HCSecretRoomRight,
                                                    DungeonItemID.HCMapGuardDrop,
                                                    DungeonItemID.HCBoomerangGuardDrop
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle])
                        };
                    }
                case Locations.LocationID.AgahnimTower:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]),
                            new SmallKeyLayout(
                                2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ATRoom03,
                                    DungeonItemID.ATDarkMaze
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffSmallKeyShuffleOff]),
                            new SmallKeyLayout(
                                4,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ATRoom03,
                                    DungeonItemID.ATDarkMaze,
                                    DungeonItemID.ATDarkArcherDrop,
                                    DungeonItemID.ATCircleOfPotsDrop
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnSmallKeyShuffleOff])
                        };
                    }
                case Locations.LocationID.EasternPalace:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                new AlternativeRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.AllKeyShuffle],
                                    RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffBigKeyShuffleOn]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.EPCannonballChest,
                                    DungeonItemID.EPMapChest,
                                    DungeonItemID.EPCompassChest,
                                    DungeonItemID.EPBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.EPCannonballChest,
                                    DungeonItemID.EPMapChest,
                                    DungeonItemID.EPCompassChest,
                                    DungeonItemID.EPBigChest,
                                    DungeonItemID.EPDarkSquarePot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.EPCannonballChest,
                                            DungeonItemID.EPMapChest,
                                            DungeonItemID.EPCompassChest,
                                            DungeonItemID.EPBigChest,
                                            DungeonItemID.EPBigKeyChest,
                                            DungeonItemID.EPDarkSquarePot,
                                            DungeonItemID.EPDarkEyegoreDrop
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.EPCannonballChest,
                                    DungeonItemID.EPMapChest,
                                    DungeonItemID.EPCompassChest,
                                    DungeonItemID.EPBigKeyChest,
                                    DungeonItemID.EPDarkSquarePot
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.EPCannonballChest,
                                    DungeonItemID.EPMapChest,
                                    DungeonItemID.EPCompassChest,
                                    DungeonItemID.EPDarkSquarePot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.EPCannonballChest,
                                            DungeonItemID.EPMapChest,
                                            DungeonItemID.EPCompassChest,
                                            DungeonItemID.EPDarkSquarePot,
                                            DungeonItemID.EPBigChest,
                                            DungeonItemID.EPDarkEyegoreDrop
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.EPCannonballChest,
                                                    DungeonItemID.EPMapChest,
                                                    DungeonItemID.EPCompassChest,
                                                    DungeonItemID.EPBigChest,
                                                    DungeonItemID.EPBigKeyChest,
                                                    DungeonItemID.EPDarkSquarePot,
                                                    DungeonItemID.EPDarkEyegoreDrop
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon),
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.EPBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.EPCannonballChest,
                                            DungeonItemID.EPMapChest,
                                            DungeonItemID.EPCompassChest,
                                            DungeonItemID.EPDarkSquarePot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.EPCannonballChest,
                                                    DungeonItemID.EPMapChest,
                                                    DungeonItemID.EPCompassChest,
                                                    DungeonItemID.EPDarkSquarePot,
                                                    DungeonItemID.EPBigChest,
                                                    DungeonItemID.EPDarkEyegoreDrop
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle])
                        };
                    }
                case Locations.LocationID.DesertPalace:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPCompassChest,
                                    DungeonItemID.DPBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPBigChest
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPCompassChest,
                                    DungeonItemID.DPBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffNoKeyShuffle]),
                            new SmallKeyLayout(
                                2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest,
                                    DungeonItemID.DPTiles1Pot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPBigChest,
                                            DungeonItemID.DPTiles1Pot,
                                            DungeonItemID.DPBeamosHallPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.DPMapChest,
                                                    DungeonItemID.DPTorch,
                                                    DungeonItemID.DPBigChest,
                                                    DungeonItemID.DPTiles1Pot,
                                                    DungeonItemID.DPBeamosHallPot,
                                                    DungeonItemID.DPTiles2Pot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPCompassChest,
                                    DungeonItemID.DPBigKeyChest,
                                    DungeonItemID.DPTiles1Pot,
                                    DungeonItemID.DPBeamosHallPot,
                                    DungeonItemID.DPTiles2Pot
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPTiles1Pot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPBigChest,
                                            DungeonItemID.DPTiles1Pot
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout
                                            (
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.DPMapChest,
                                                    DungeonItemID.DPTorch,
                                                    DungeonItemID.DPBigChest,
                                                    DungeonItemID.DPTiles1Pot,
                                                    DungeonItemID.DPBeamosHallPot
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        4,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.DPMapChest,
                                                            DungeonItemID.DPTorch,
                                                            DungeonItemID.DPBigChest,
                                                            DungeonItemID.DPTiles1Pot,
                                                            DungeonItemID.DPBeamosHallPot,
                                                            DungeonItemID.DPTiles2Pot
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        }, dungeon)
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPBeamosHallPot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPTiles1Pot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.DPMapChest,
                                                    DungeonItemID.DPTorch,
                                                    DungeonItemID.DPBigChest,
                                                    DungeonItemID.DPTiles1Pot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        4,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.DPMapChest,
                                                            DungeonItemID.DPTorch,
                                                            DungeonItemID.DPBigChest,
                                                            DungeonItemID.DPTiles1Pot,
                                                            DungeonItemID.DPTiles2Pot
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        }, dungeon)
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPTiles2Pot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPTiles1Pot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.DPMapChest,
                                                    DungeonItemID.DPTorch,
                                                    DungeonItemID.DPBigChest,
                                                    DungeonItemID.DPTiles1Pot,
                                                    DungeonItemID.DPBeamosHallPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle])
                        };
                    }
                case Locations.LocationID.TowerOfHera:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBasementCage,
                                    DungeonItemID.ToHMapChest,
                                    DungeonItemID.ToHBigKeyChest,
                                    DungeonItemID.ToHCompassChest,
                                    DungeonItemID.ToHBigChest,
                                    DungeonItemID.ToHBoss
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOffBigKeyShuffleOnly]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBasementCage,
                                    DungeonItemID.ToHMapChest,
                                    DungeonItemID.ToHBigKeyChest,
                                    DungeonItemID.ToHCompassChest,
                                    DungeonItemID.ToHBigChest
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOnBigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBasementCage,
                                    DungeonItemID.ToHMapChest,
                                    DungeonItemID.ToHBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBasementCage,
                                    DungeonItemID.ToHMapChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.ToHBasementCage,
                                            DungeonItemID.ToHMapChest,
                                            DungeonItemID.ToHBigKeyChest,
                                            DungeonItemID.ToHCompassChest,
                                            DungeonItemID.ToHBigChest,
                                            DungeonItemID.ToHBoss
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon,
                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff]),
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.ToHBasementCage,
                                            DungeonItemID.ToHMapChest,
                                            DungeonItemID.ToHBigKeyChest,
                                            DungeonItemID.ToHCompassChest,
                                            DungeonItemID.ToHBigChest
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon,
                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn])
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.ToHBasementCage,
                                            DungeonItemID.ToHMapChest
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle])
                        };
                    }
                case Locations.LocationID.PalaceOfDarkness:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                4,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDShooterRoom,
                                    DungeonItemID.PoDMapChest,
                                    DungeonItemID.PoDArenaLedge,
                                    DungeonItemID.PoDBigKeyChest,
                                    DungeonItemID.PoDStalfosBasement,
                                    DungeonItemID.PoDArenaBridge
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        6,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.PoDShooterRoom,
                                            DungeonItemID.PoDMapChest,
                                            DungeonItemID.PoDArenaLedge,
                                            DungeonItemID.PoDBigKeyChest,
                                            DungeonItemID.PoDStalfosBasement,
                                            DungeonItemID.PoDArenaBridge,
                                            DungeonItemID.PoDCompassChest,
                                            DungeonItemID.PoDDarkBasementLeft,
                                            DungeonItemID.PoDDarkBasementRight,
                                            DungeonItemID.PoDHarmlessHellway
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDShooterRoom,
                                    DungeonItemID.PoDMapChest,
                                    DungeonItemID.PoDArenaLedge,
                                    DungeonItemID.PoDBigKeyChest,
                                    DungeonItemID.PoDStalfosBasement,
                                    DungeonItemID.PoDArenaBridge,
                                    DungeonItemID.PoDCompassChest,
                                    DungeonItemID.PoDDarkBasementLeft,
                                    DungeonItemID.PoDDarkBasementRight,
                                    DungeonItemID.PoDHarmlessHellway,
                                    DungeonItemID.PoDDarkMazeTop,
                                    DungeonItemID.PoDDarkMazeBottom
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDShooterRoom,
                                    DungeonItemID.PoDMapChest,
                                    DungeonItemID.PoDArenaLedge,
                                    DungeonItemID.PoDBigKeyChest,
                                    DungeonItemID.PoDStalfosBasement,
                                    DungeonItemID.PoDArenaBridge
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.PoDShooterRoom,
                                            DungeonItemID.PoDMapChest,
                                            DungeonItemID.PoDArenaLedge,
                                            DungeonItemID.PoDBigKeyChest,
                                            DungeonItemID.PoDStalfosBasement,
                                            DungeonItemID.PoDArenaBridge
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                6,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.PoDShooterRoom,
                                                    DungeonItemID.PoDMapChest,
                                                    DungeonItemID.PoDArenaLedge,
                                                    DungeonItemID.PoDBigKeyChest,
                                                    DungeonItemID.PoDStalfosBasement,
                                                    DungeonItemID.PoDArenaBridge,
                                                    DungeonItemID.PoDCompassChest,
                                                    DungeonItemID.PoDDarkBasementLeft,
                                                    DungeonItemID.PoDDarkBasementRight,
                                                    DungeonItemID.PoDHarmlessHellway
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDCompassChest,
                                    DungeonItemID.PoDDarkBasementLeft,
                                    DungeonItemID.PoDDarkBasementRight,
                                    DungeonItemID.PoDHarmlessHellway
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.PoDShooterRoom,
                                            DungeonItemID.PoDMapChest,
                                            DungeonItemID.PoDArenaLedge,
                                            DungeonItemID.PoDBigKeyChest,
                                            DungeonItemID.PoDStalfosBasement,
                                            DungeonItemID.PoDArenaBridge
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                6,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.PoDShooterRoom,
                                                    DungeonItemID.PoDMapChest,
                                                    DungeonItemID.PoDArenaLedge,
                                                    DungeonItemID.PoDBigKeyChest,
                                                    DungeonItemID.PoDStalfosBasement,
                                                    DungeonItemID.PoDArenaBridge,
                                                    DungeonItemID.PoDCompassChest,
                                                    DungeonItemID.PoDDarkBasementLeft,
                                                    DungeonItemID.PoDDarkBasementRight,
                                                    DungeonItemID.PoDHarmlessHellway
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDDarkMazeTop,
                                    DungeonItemID.PoDDarkMazeBottom
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.PoDShooterRoom,
                                            DungeonItemID.PoDMapChest,
                                            DungeonItemID.PoDArenaLedge,
                                            DungeonItemID.PoDBigKeyChest,
                                            DungeonItemID.PoDStalfosBasement,
                                            DungeonItemID.PoDArenaBridge
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                6,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.PoDShooterRoom,
                                                    DungeonItemID.PoDMapChest,
                                                    DungeonItemID.PoDArenaLedge,
                                                    DungeonItemID.PoDBigKeyChest,
                                                    DungeonItemID.PoDStalfosBasement,
                                                    DungeonItemID.PoDArenaBridge,
                                                    DungeonItemID.PoDCompassChest,
                                                    DungeonItemID.PoDDarkBasementLeft,
                                                    DungeonItemID.PoDDarkBasementRight,
                                                    DungeonItemID.PoDHarmlessHellway
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle])
                        };
                    }
                case Locations.LocationID.SwampPalace:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPBigChest,
                                    DungeonItemID.SPCompassChest,
                                    DungeonItemID.SPWestChest,
                                    DungeonItemID.SPBigKeyChest,
                                    DungeonItemID.SPFloodedRoomLeft,
                                    DungeonItemID.SPFloodedRoomRight,
                                    DungeonItemID.SPWaterfallRoom
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPBoss
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPBigChest,
                                    DungeonItemID.SPCompassChest,
                                    DungeonItemID.SPWestChest,
                                    DungeonItemID.SPBigKeyChest,
                                    DungeonItemID.SPFloodedRoomLeft,
                                    DungeonItemID.SPFloodedRoomRight,
                                    DungeonItemID.SPWaterfallRoom
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPBoss
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffNoKeyShuffle]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPTrench1Pot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        5,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPBigChest,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPTrench1Pot,
                                                            DungeonItemID.SPHookshotPot
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            new SmallKeyLayout(
                                                                6,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID.SPEntrance,
                                                                    DungeonItemID.SPMapChest,
                                                                    DungeonItemID.SPPotRowPot,
                                                                    DungeonItemID.SPBigChest,
                                                                    DungeonItemID.SPCompassChest,
                                                                    DungeonItemID.SPFloodedRoomLeft,
                                                                    DungeonItemID.SPFloodedRoomRight,
                                                                    DungeonItemID.SPWaterfallRoom,
                                                                    DungeonItemID.SPTrench1Pot,
                                                                    DungeonItemID.SPHookshotPot,
                                                                    DungeonItemID.SPWaterwayPot
                                                                }, false,
                                                                new List<IKeyLayout>
                                                                {
                                                                    new EndKeyLayout()
                                                                }, dungeon)
                                                        }, dungeon)
                                                }, dungeon)
                                        },
                                        dungeon)
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPBigChest,
                                    DungeonItemID.SPCompassChest,
                                    DungeonItemID.SPWestChest,
                                    DungeonItemID.SPBigKeyChest,
                                    DungeonItemID.SPFloodedRoomLeft,
                                    DungeonItemID.SPFloodedRoomRight,
                                    DungeonItemID.SPWaterfallRoom,
                                    DungeonItemID.SPPotRowPot,
                                    DungeonItemID.SPTrench1Pot,
                                    DungeonItemID.SPHookshotPot,
                                    DungeonItemID.SPTrench2Pot,
                                    DungeonItemID.SPWaterwayPot
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPBoss
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        3,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPTrench1Pot
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new SmallKeyLayout(
                                                                5,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID.SPEntrance,
                                                                    DungeonItemID.SPMapChest,
                                                                    DungeonItemID.SPPotRowPot,
                                                                    DungeonItemID.SPBigChest,
                                                                    DungeonItemID.SPCompassChest,
                                                                    DungeonItemID.SPTrench1Pot,
                                                                    DungeonItemID.SPHookshotPot
                                                                }, true,
                                                                new List<IKeyLayout>
                                                                {
                                                                    new SmallKeyLayout(
                                                                        6,
                                                                        new List<DungeonItemID>
                                                                        {
                                                                            DungeonItemID.SPEntrance,
                                                                            DungeonItemID.SPMapChest,
                                                                            DungeonItemID.SPPotRowPot,
                                                                            DungeonItemID.SPBigChest,
                                                                            DungeonItemID.SPCompassChest,
                                                                            DungeonItemID.SPFloodedRoomLeft,
                                                                            DungeonItemID.SPFloodedRoomRight,
                                                                            DungeonItemID.SPWaterfallRoom,
                                                                            DungeonItemID.SPTrench1Pot,
                                                                            DungeonItemID.SPHookshotPot,
                                                                            DungeonItemID.SPWaterwayPot
                                                                        }, true,
                                                                        new List<IKeyLayout>
                                                                        {
                                                                            new EndKeyLayout()
                                                                        }, dungeon)
                                                                }, dungeon)
                                                        }, dungeon)
                                                },
                                                dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPTrench1Pot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        3,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            new SmallKeyLayout(
                                                                5,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID.SPEntrance,
                                                                    DungeonItemID.SPMapChest,
                                                                    DungeonItemID.SPPotRowPot,
                                                                    DungeonItemID.SPBigChest,
                                                                    DungeonItemID.SPCompassChest,
                                                                    DungeonItemID.SPHookshotPot
                                                                }, false,
                                                                new List<IKeyLayout>
                                                                {
                                                                    new SmallKeyLayout(
                                                                        6,
                                                                        new List<DungeonItemID>
                                                                        {
                                                                            DungeonItemID.SPEntrance,
                                                                            DungeonItemID.SPMapChest,
                                                                            DungeonItemID.SPPotRowPot,
                                                                            DungeonItemID.SPBigChest,
                                                                            DungeonItemID.SPCompassChest,
                                                                            DungeonItemID.SPFloodedRoomLeft,
                                                                            DungeonItemID.SPFloodedRoomRight,
                                                                            DungeonItemID.SPWaterfallRoom,
                                                                            DungeonItemID.SPHookshotPot,
                                                                            DungeonItemID.SPWaterwayPot
                                                                        }, false,
                                                                        new List<IKeyLayout>
                                                                        {
                                                                            new EndKeyLayout()
                                                                        }, dungeon)
                                                                }, dungeon)
                                                        }, dungeon)
                                                },
                                                dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPBigChest,
                                    DungeonItemID.SPCompassChest,
                                    DungeonItemID.SPHookshotPot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        3,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPTrench1Pot
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            new SmallKeyLayout(
                                                                5,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID.SPEntrance,
                                                                    DungeonItemID.SPMapChest,
                                                                    DungeonItemID.SPPotRowPot,
                                                                    DungeonItemID.SPBigChest,
                                                                    DungeonItemID.SPCompassChest,
                                                                    DungeonItemID.SPTrench1Pot,
                                                                    DungeonItemID.SPHookshotPot
                                                                }, true,
                                                                new List<IKeyLayout>
                                                                {
                                                                    new SmallKeyLayout(
                                                                        6,
                                                                        new List<DungeonItemID>
                                                                        {
                                                                            DungeonItemID.SPEntrance,
                                                                            DungeonItemID.SPMapChest,
                                                                            DungeonItemID.SPPotRowPot,
                                                                            DungeonItemID.SPBigChest,
                                                                            DungeonItemID.SPCompassChest,
                                                                            DungeonItemID.SPFloodedRoomLeft,
                                                                            DungeonItemID.SPFloodedRoomRight,
                                                                            DungeonItemID.SPWaterfallRoom,
                                                                            DungeonItemID.SPTrench1Pot,
                                                                            DungeonItemID.SPHookshotPot,
                                                                            DungeonItemID.SPWaterwayPot
                                                                        }, true,
                                                                        new List<IKeyLayout>
                                                                        {
                                                                            new EndKeyLayout()
                                                                        }, dungeon)
                                                                }, dungeon)
                                                        }, dungeon)
                                                },
                                                dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPFloodedRoomLeft,
                                    DungeonItemID.SPFloodedRoomRight,
                                    DungeonItemID.SPWaterfallRoom,
                                    DungeonItemID.SPWaterwayPot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        3,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPTrench1Pot
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            new SmallKeyLayout(
                                                                5,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID.SPEntrance,
                                                                    DungeonItemID.SPMapChest,
                                                                    DungeonItemID.SPPotRowPot,
                                                                    DungeonItemID.SPCompassChest,
                                                                    DungeonItemID.SPTrench1Pot,
                                                                    DungeonItemID.SPHookshotPot
                                                                }, false,
                                                                new List<IKeyLayout>
                                                                {
                                                                    new SmallKeyLayout(
                                                                        6,
                                                                        new List<DungeonItemID>
                                                                        {
                                                                            DungeonItemID.SPEntrance,
                                                                            DungeonItemID.SPMapChest,
                                                                            DungeonItemID.SPPotRowPot,
                                                                            DungeonItemID.SPBigChest,
                                                                            DungeonItemID.SPCompassChest,
                                                                            DungeonItemID.SPFloodedRoomLeft,
                                                                            DungeonItemID.SPFloodedRoomRight,
                                                                            DungeonItemID.SPWaterfallRoom,
                                                                            DungeonItemID.SPTrench1Pot,
                                                                            DungeonItemID.SPHookshotPot,
                                                                            DungeonItemID.SPWaterwayPot
                                                                        }, true,
                                                                        new List<IKeyLayout>
                                                                        {
                                                                            new EndKeyLayout()
                                                                        }, dungeon)
                                                                }, dungeon)
                                                        }, dungeon)
                                                },
                                                dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPWestChest,
                                    DungeonItemID.SPBigKeyChest,
                                    DungeonItemID.SPTrench2Pot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        3,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPTrench1Pot
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            new SmallKeyLayout(
                                                                5,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID.SPEntrance,
                                                                    DungeonItemID.SPMapChest,
                                                                    DungeonItemID.SPPotRowPot,
                                                                    DungeonItemID.SPCompassChest,
                                                                    DungeonItemID.SPTrench1Pot,
                                                                    DungeonItemID.SPHookshotPot
                                                                }, false,
                                                                new List<IKeyLayout>
                                                                {
                                                                    new SmallKeyLayout(
                                                                        6,
                                                                        new List<DungeonItemID>
                                                                        {
                                                                            DungeonItemID.SPEntrance,
                                                                            DungeonItemID.SPMapChest,
                                                                            DungeonItemID.SPPotRowPot,
                                                                            DungeonItemID.SPCompassChest,
                                                                            DungeonItemID.SPFloodedRoomLeft,
                                                                            DungeonItemID.SPFloodedRoomRight,
                                                                            DungeonItemID.SPWaterfallRoom,
                                                                            DungeonItemID.SPTrench1Pot,
                                                                            DungeonItemID.SPHookshotPot,
                                                                            DungeonItemID.SPWaterwayPot
                                                                        }, false,
                                                                        new List<IKeyLayout>
                                                                        {
                                                                            new EndKeyLayout()
                                                                        }, dungeon)
                                                                }, dungeon)
                                                        }, dungeon)
                                                },
                                                dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPBoss
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        3,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPTrench1Pot
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            new SmallKeyLayout(
                                                                5,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID.SPEntrance,
                                                                    DungeonItemID.SPMapChest,
                                                                    DungeonItemID.SPPotRowPot,
                                                                    DungeonItemID.SPCompassChest,
                                                                    DungeonItemID.SPTrench1Pot,
                                                                    DungeonItemID.SPHookshotPot
                                                                }, false,
                                                                new List<IKeyLayout>
                                                                {
                                                                    new SmallKeyLayout(
                                                                        6,
                                                                        new List<DungeonItemID>
                                                                        {
                                                                            DungeonItemID.SPEntrance,
                                                                            DungeonItemID.SPMapChest,
                                                                            DungeonItemID.SPPotRowPot,
                                                                            DungeonItemID.SPCompassChest,
                                                                            DungeonItemID.SPFloodedRoomLeft,
                                                                            DungeonItemID.SPFloodedRoomRight,
                                                                            DungeonItemID.SPWaterfallRoom,
                                                                            DungeonItemID.SPTrench1Pot,
                                                                            DungeonItemID.SPHookshotPot,
                                                                            DungeonItemID.SPWaterwayPot
                                                                        }, false,
                                                                        new List<IKeyLayout>
                                                                        {
                                                                            new EndKeyLayout()
                                                                        }, dungeon)
                                                                }, dungeon)
                                                        }, dungeon)
                                                },
                                                dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffNoKeyShuffle])
                        };
                    }
                case Locations.LocationID.SkullWoods:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                }, dungeon,
                                RequirementDictionary.Instance[
                                    RequirementType.KeyDropShuffleOffBigKeyShuffleOnlyItemPlacementAdvanced]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWPinballRoom
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                }, dungeon,
                                RequirementDictionary.Instance[
                                    RequirementType.KeyDropShuffleOffBigKeyShuffleOnlyItemPlacementBasic]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWBoss
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOnSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffNoKeyShuffleItemPlacementAdvanced]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWBridgeRoom
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWBigChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffNoKeyShuffleItemPlacementBasic]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBoss
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon,
                                        RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced]),
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon,
                                        RequirementDictionary.Instance[RequirementType.ItemPlacementBasic])
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffNoKeyShuffle]),
                            new SmallKeyLayout(
                                4,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWWestLobbyPot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        5,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom,
                                            DungeonItemID.SWWestLobbyPot,
                                            DungeonItemID.SWSpikeCornerDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnBigKeyShuffleOnlyItemPlacementAdvanced]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWPinballRoom
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom,
                                            DungeonItemID.SWWestLobbyPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWBigChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom,
                                                    DungeonItemID.SWWestLobbyPot,
                                                    DungeonItemID.SWSpikeCornerDrop
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnBigKeyShuffleOnlyItemPlacementBasic]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWBoss,
                                    DungeonItemID.SWWestLobbyPot,
                                    DungeonItemID.SWSpikeCornerDrop
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWWestLobbyPot,
                                    DungeonItemID.SWSpikeCornerDrop
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOnSmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWWestLobbyPot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom,
                                            DungeonItemID.SWWestLobbyPot
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWBigChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom,
                                                    DungeonItemID.SWWestLobbyPot,
                                                    DungeonItemID.SWSpikeCornerDrop
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffleItemPlacementAdvanced]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWWestLobbyPot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWBigChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom,
                                                    DungeonItemID.SWWestLobbyPot
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        5,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SWBigKeyChest,
                                                            DungeonItemID.SWMapChest,
                                                            DungeonItemID.SWBigChest,
                                                            DungeonItemID.SWPotPrison,
                                                            DungeonItemID.SWCompassChest,
                                                            DungeonItemID.SWPinballRoom,
                                                            DungeonItemID.SWBridgeRoom,
                                                            DungeonItemID.SWWestLobbyPot,
                                                            DungeonItemID.SWSpikeCornerDrop
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        }, dungeon)
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffleItemPlacementBasic]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWSpikeCornerDrop
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        5,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom,
                                            DungeonItemID.SWWestLobbyPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon,
                                        RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced]),
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWBigChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom,
                                                    DungeonItemID.SWWestLobbyPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon,
                                        RequirementDictionary.Instance[RequirementType.ItemPlacementBasic])
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBoss
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom,
                                            DungeonItemID.SWWestLobbyPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom,
                                                    DungeonItemID.SWWestLobbyPot,
                                                    DungeonItemID.SWSpikeCornerDrop
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon,
                                        RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced]),
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom,
                                                    DungeonItemID.SWWestLobbyPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        5,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SWBigKeyChest,
                                                            DungeonItemID.SWMapChest,
                                                            DungeonItemID.SWPotPrison,
                                                            DungeonItemID.SWCompassChest,
                                                            DungeonItemID.SWPinballRoom,
                                                            DungeonItemID.SWBridgeRoom,
                                                            DungeonItemID.SWWestLobbyPot,
                                                            DungeonItemID.SWSpikeCornerDrop
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        }, dungeon)
                                                }, dungeon)
                                        }, dungeon,
                                        RequirementDictionary.Instance[RequirementType.ItemPlacementBasic])
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffNoKeyShuffle])
                        };
                    }
                case Locations.LocationID.ThievesTown:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TTMapChest,
                                    DungeonItemID.TTAmbushChest,
                                    DungeonItemID.TTCompassChest,
                                    DungeonItemID.TTBigKeyChest,
                                    DungeonItemID.TTBlindsCell,
                                    DungeonItemID.TTBigChest
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TTMapChest,
                                    DungeonItemID.TTAmbushChest,
                                    DungeonItemID.TTCompassChest,
                                    DungeonItemID.TTBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]),
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TTMapChest,
                                            DungeonItemID.TTAmbushChest,
                                            DungeonItemID.TTCompassChest,
                                            DungeonItemID.TTBigKeyChest,
                                            DungeonItemID.TTBlindsCell,
                                            DungeonItemID.TTBigChest
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon,
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff])
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TTMapChest,
                                    DungeonItemID.TTAmbushChest,
                                    DungeonItemID.TTCompassChest,
                                    DungeonItemID.TTBigKeyChest,
                                    DungeonItemID.TTHallwayPot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TTMapChest,
                                            DungeonItemID.TTAmbushChest,
                                            DungeonItemID.TTCompassChest,
                                            DungeonItemID.TTBigKeyChest,
                                            DungeonItemID.TTBlindsCell,
                                            DungeonItemID.TTHallwayPot,
                                            DungeonItemID.TTSpikeSwitchPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon)
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TTMapChest,
                                    DungeonItemID.TTAmbushChest,
                                    DungeonItemID.TTCompassChest,
                                    DungeonItemID.TTBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]),
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TTMapChest,
                                            DungeonItemID.TTAmbushChest,
                                            DungeonItemID.TTCompassChest,
                                            DungeonItemID.TTBigKeyChest,
                                            DungeonItemID.TTHallwayPot
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.TTMapChest,
                                                    DungeonItemID.TTAmbushChest,
                                                    DungeonItemID.TTCompassChest,
                                                    DungeonItemID.TTBigKeyChest,
                                                    DungeonItemID.TTBlindsCell,
                                                    DungeonItemID.TTHallwayPot,
                                                    DungeonItemID.TTSpikeSwitchPot
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon)
                                        }, dungeon,
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff])
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                        };
                    }
                case Locations.LocationID.IcePalace:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPSpikeRoom,
                                    DungeonItemID.IPMapChest,
                                    DungeonItemID.IPBigKeyChest,
                                    DungeonItemID.IPFreezorChest,
                                    DungeonItemID.IPBigChest,
                                    DungeonItemID.IPIcedTRoom
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                }, dungeon,
                                RequirementDictionary.Instance[
                                    RequirementType.KeyDropShuffleOffBigKeyShuffleOnlyGuaranteedBossItemsOrItemPlacementBasic]),
                            new SmallKeyLayout(
                                2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPSpikeRoom,
                                    DungeonItemID.IPMapChest,
                                    DungeonItemID.IPBigKeyChest,
                                    DungeonItemID.IPFreezorChest,
                                    DungeonItemID.IPBigChest,
                                    DungeonItemID.IPIcedTRoom,
                                    DungeonItemID.IPBoss
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                }, dungeon,
                                RequirementDictionary.Instance[
                                    RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOnlyItemPlacementAdvanced]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPSpikeRoom,
                                    DungeonItemID.IPMapChest,
                                    DungeonItemID.IPBigKeyChest,
                                    DungeonItemID.IPFreezorChest,
                                    DungeonItemID.IPIcedTRoom
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]),
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPSpikeRoom,
                                            DungeonItemID.IPMapChest,
                                            DungeonItemID.IPBigKeyChest,
                                            DungeonItemID.IPFreezorChest,
                                            DungeonItemID.IPBigChest,
                                            DungeonItemID.IPIcedTRoom
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon,
                                        RequirementDictionary.Instance[
                                            RequirementType.GuaranteedBossItemsOnOrItemPlacementBasicSmallKeyShuffleOff]),
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPSpikeRoom,
                                            DungeonItemID.IPMapChest,
                                            DungeonItemID.IPBigKeyChest,
                                            DungeonItemID.IPFreezorChest,
                                            DungeonItemID.IPBigChest,
                                            DungeonItemID.IPIcedTRoom,
                                            DungeonItemID.IPBoss
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        }, dungeon,
                                        RequirementDictionary.Instance[
                                            RequirementType.GuaranteedBossItemsOffSmallKeyShuffleOffItemPlacementAdvanced])
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.IPJellyDrop
                                }, false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPJellyDrop,
                                            DungeonItemID.IPConveyerDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                6,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.IPCompassChest,
                                                    DungeonItemID.IPSpikeRoom,
                                                    DungeonItemID.IPMapChest,
                                                    DungeonItemID.IPBigKeyChest,
                                                    DungeonItemID.IPFreezorChest,
                                                    DungeonItemID.IPBigChest,
                                                    DungeonItemID.IPIcedTRoom,
                                                    DungeonItemID.IPJellyDrop,
                                                    DungeonItemID.IPConveyerDrop,
                                                    DungeonItemID.IPHammerBlockDrop,
                                                    DungeonItemID.IPManyPotsPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                }, dungeon,
                                                RequirementDictionary.Instance[
                                                    RequirementType.GuaranteedBossItemsOnOrItemPlacementBasic]),
                                            new SmallKeyLayout(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.IPCompassChest,
                                                    DungeonItemID.IPSpikeRoom,
                                                    DungeonItemID.IPMapChest,
                                                    DungeonItemID.IPBigKeyChest,
                                                    DungeonItemID.IPFreezorChest,
                                                    DungeonItemID.IPBigChest,
                                                    DungeonItemID.IPIcedTRoom,
                                                    DungeonItemID.IPJellyDrop,
                                                    DungeonItemID.IPConveyerDrop,
                                                    DungeonItemID.IPHammerBlockDrop,
                                                    DungeonItemID.IPManyPotsPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        6,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.IPCompassChest,
                                                            DungeonItemID.IPSpikeRoom,
                                                            DungeonItemID.IPMapChest,
                                                            DungeonItemID.IPBigKeyChest,
                                                            DungeonItemID.IPFreezorChest,
                                                            DungeonItemID.IPBigChest,
                                                            DungeonItemID.IPIcedTRoom,
                                                            DungeonItemID.IPBoss,
                                                            DungeonItemID.IPJellyDrop,
                                                            DungeonItemID.IPConveyerDrop,
                                                            DungeonItemID.IPHammerBlockDrop,
                                                            DungeonItemID.IPManyPotsPot
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        }, dungeon)
                                                }, dungeon,
                                                RequirementDictionary.Instance[
                                                    RequirementType.GuaranteedBossItemsOffItemPlacementAdvanced])
                                        }, dungeon)
                                }, dungeon,
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPConveyerDrop
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPJellyDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.IPCompassChest,
                                                    DungeonItemID.IPJellyDrop,
                                                    DungeonItemID.IPConveyerDrop
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        6,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.IPCompassChest,
                                                            DungeonItemID.IPSpikeRoom,
                                                            DungeonItemID.IPMapChest,
                                                            DungeonItemID.IPBigKeyChest,
                                                            DungeonItemID.IPFreezorChest,
                                                            DungeonItemID.IPBigChest,
                                                            DungeonItemID.IPIcedTRoom,
                                                            DungeonItemID.IPJellyDrop,
                                                            DungeonItemID.IPConveyerDrop,
                                                            DungeonItemID.IPHammerBlockDrop,
                                                            DungeonItemID.IPManyPotsPot
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        }, dungeon,
                                                        RequirementDictionary.Instance[
                                                            RequirementType.GuaranteedBossItemsOnOrItemPlacementBasic]),
                                                    new SmallKeyLayout(
                                                        5,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.IPCompassChest,
                                                            DungeonItemID.IPSpikeRoom,
                                                            DungeonItemID.IPMapChest,
                                                            DungeonItemID.IPBigKeyChest,
                                                            DungeonItemID.IPFreezorChest,
                                                            DungeonItemID.IPBigChest,
                                                            DungeonItemID.IPIcedTRoom,
                                                            DungeonItemID.IPJellyDrop,
                                                            DungeonItemID.IPConveyerDrop,
                                                            DungeonItemID.IPHammerBlockDrop,
                                                            DungeonItemID.IPManyPotsPot
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new SmallKeyLayout(
                                                                6,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID.IPCompassChest,
                                                                    DungeonItemID.IPSpikeRoom,
                                                                    DungeonItemID.IPMapChest,
                                                                    DungeonItemID.IPBigKeyChest,
                                                                    DungeonItemID.IPFreezorChest,
                                                                    DungeonItemID.IPBigChest,
                                                                    DungeonItemID.IPIcedTRoom,
                                                                    DungeonItemID.IPBoss,
                                                                    DungeonItemID.IPJellyDrop,
                                                                    DungeonItemID.IPConveyerDrop,
                                                                    DungeonItemID.IPHammerBlockDrop,
                                                                    DungeonItemID.IPManyPotsPot
                                                                }, true,
                                                                new List<IKeyLayout>
                                                                {
                                                                    new EndKeyLayout()
                                                                }, dungeon)
                                                        }, dungeon,
                                                        RequirementDictionary.Instance[
                                                            RequirementType.GuaranteedBossItemsOffItemPlacementAdvanced])
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.IPSpikeRoom,
                                    DungeonItemID.IPMapChest,
                                    DungeonItemID.IPBigKeyChest,
                                    DungeonItemID.IPFreezorChest,
                                    DungeonItemID.IPIcedTRoom,
                                    DungeonItemID.IPHammerBlockDrop,
                                    DungeonItemID.IPManyPotsPot
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPJellyDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.IPCompassChest,
                                                    DungeonItemID.IPJellyDrop,
                                                    DungeonItemID.IPConveyerDrop
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        6,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.IPCompassChest,
                                                            DungeonItemID.IPSpikeRoom,
                                                            DungeonItemID.IPMapChest,
                                                            DungeonItemID.IPBigKeyChest,
                                                            DungeonItemID.IPFreezorChest,
                                                            DungeonItemID.IPBigChest,
                                                            DungeonItemID.IPIcedTRoom,
                                                            DungeonItemID.IPJellyDrop,
                                                            DungeonItemID.IPConveyerDrop,
                                                            DungeonItemID.IPHammerBlockDrop,
                                                            DungeonItemID.IPManyPotsPot
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        }, dungeon,
                                                        RequirementDictionary.Instance[
                                                            RequirementType.GuaranteedBossItemsOnOrItemPlacementBasic]),
                                                    new SmallKeyLayout(
                                                        5,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.IPCompassChest,
                                                            DungeonItemID.IPSpikeRoom,
                                                            DungeonItemID.IPMapChest,
                                                            DungeonItemID.IPBigKeyChest,
                                                            DungeonItemID.IPFreezorChest,
                                                            DungeonItemID.IPBigChest,
                                                            DungeonItemID.IPIcedTRoom,
                                                            DungeonItemID.IPJellyDrop,
                                                            DungeonItemID.IPConveyerDrop,
                                                            DungeonItemID.IPHammerBlockDrop,
                                                            DungeonItemID.IPManyPotsPot
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new SmallKeyLayout(
                                                                6,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID.IPCompassChest,
                                                                    DungeonItemID.IPSpikeRoom,
                                                                    DungeonItemID.IPMapChest,
                                                                    DungeonItemID.IPBigKeyChest,
                                                                    DungeonItemID.IPFreezorChest,
                                                                    DungeonItemID.IPBigChest,
                                                                    DungeonItemID.IPIcedTRoom,
                                                                    DungeonItemID.IPBoss,
                                                                    DungeonItemID.IPJellyDrop,
                                                                    DungeonItemID.IPConveyerDrop,
                                                                    DungeonItemID.IPHammerBlockDrop,
                                                                    DungeonItemID.IPManyPotsPot
                                                                }, true,
                                                                new List<IKeyLayout>
                                                                {
                                                                    new EndKeyLayout()
                                                                }, dungeon)
                                                        }, dungeon,
                                                        RequirementDictionary.Instance[
                                                            RequirementType.GuaranteedBossItemsOffItemPlacementAdvanced])
                                                }, dungeon)
                                        }, dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                        };
                    }
                case Locations.LocationID.MiseryMire:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMMapChest,
                                    DungeonItemID.MMBoss
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly],
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff]
                                })),
                            new SmallKeyLayout(
                                3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMMapChest
                                },
                                true,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly],
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMCompassChest,
                                    DungeonItemID.MMBigKeyChest,
                                    DungeonItemID.MMMapChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMMapChest,
                                            DungeonItemID.MMBoss
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon,
                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff]),
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMMapChest
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon,
                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn])
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMMapChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMBigChest,
                                                    DungeonItemID.MMMapChest,
                                                    DungeonItemID.MMBoss
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon,
                                                RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff]),
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMBigChest,
                                                    DungeonItemID.MMMapChest
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon,
                                                RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn])
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMCompassChest,
                                    DungeonItemID.MMBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMMapChest
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(3,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.MMBridgeChest,
                                                            DungeonItemID.MMSpikeChest,
                                                            DungeonItemID.MMMainLobby,
                                                            DungeonItemID.MMBigChest,
                                                            DungeonItemID.MMCompassChest,
                                                            DungeonItemID.MMBigKeyChest,
                                                            DungeonItemID.MMMapChest,
                                                            DungeonItemID.MMBoss
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon,
                                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff]),
                                                    new SmallKeyLayout(3,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.MMBridgeChest,
                                                            DungeonItemID.MMSpikeChest,
                                                            DungeonItemID.MMMainLobby,
                                                            DungeonItemID.MMBigChest,
                                                            DungeonItemID.MMCompassChest,
                                                            DungeonItemID.MMBigKeyChest,
                                                            DungeonItemID.MMMapChest
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon,
                                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn]),
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle])
                        };
                    }
                case Locations.LocationID.TurtleRock:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps,
                                            DungeonItemID.TRBigKeyChest,
                                            DungeonItemID.TRBigChest,
                                            DungeonItemID.TRCrystarollerRoom
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.TRCompassChest,
                                                    DungeonItemID.TRRollerRoomLeft,
                                                    DungeonItemID.TRRollerRoomRight,
                                                    DungeonItemID.TRChainChomps,
                                                    DungeonItemID.TRBigKeyChest,
                                                    DungeonItemID.TRBigChest,
                                                    DungeonItemID.TRCrystarollerRoom,
                                                    DungeonItemID.TRLaserBridgeTopLeft,
                                                    DungeonItemID.TRLaserBridgeTopRight,
                                                    DungeonItemID.TRLaserBridgeBottomLeft,
                                                    DungeonItemID.TRLaserBrdigeBottomRight
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[
                                        RequirementType.WorldStateStandardOpenEntranceShuffleNone],
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]
                                })),
                            new SmallKeyLayout(
                                4,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps,
                                    DungeonItemID.TRBigKeyChest,
                                    DungeonItemID.TRBigChest,
                                    DungeonItemID.TRCrystarollerRoom,
                                    DungeonItemID.TRLaserBridgeTopLeft,
                                    DungeonItemID.TRLaserBridgeTopRight,
                                    DungeonItemID.TRLaserBridgeBottomLeft,
                                    DungeonItemID.TRLaserBrdigeBottomRight
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity]
                                    }),
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps,
                                    DungeonItemID.TRBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[
                                        RequirementType.WorldStateStandardOpenEntranceShuffleNone],
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps,
                                    DungeonItemID.TRBigKeyChest,
                                    DungeonItemID.TRCrystarollerRoom,
                                    DungeonItemID.TRLaserBridgeTopLeft,
                                    DungeonItemID.TRLaserBridgeTopRight,
                                    DungeonItemID.TRLaserBridgeBottomLeft,
                                    DungeonItemID.TRLaserBrdigeBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity]
                                    }),
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.TRCompassChest,
                                                    DungeonItemID.TRRollerRoomLeft,
                                                    DungeonItemID.TRRollerRoomRight,
                                                    DungeonItemID.TRChainChomps,
                                                    DungeonItemID.TRBigKeyChest,
                                                    DungeonItemID.TRBigChest,
                                                    DungeonItemID.TRCrystarollerRoom
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        4,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.TRCompassChest,
                                                            DungeonItemID.TRRollerRoomLeft,
                                                            DungeonItemID.TRRollerRoomRight,
                                                            DungeonItemID.TRChainChomps,
                                                            DungeonItemID.TRBigKeyChest,
                                                            DungeonItemID.TRBigChest,
                                                            DungeonItemID.TRCrystarollerRoom,
                                                            DungeonItemID.TRLaserBridgeTopLeft,
                                                            DungeonItemID.TRLaserBridgeTopRight,
                                                            DungeonItemID.TRLaserBridgeBottomLeft,
                                                            DungeonItemID.TRLaserBrdigeBottomRight
                                                        },
                                                        true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[
                                        RequirementType.WorldStateStandardOpenEntranceShuffleNone],
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.TRCompassChest,
                                                    DungeonItemID.TRRollerRoomLeft,
                                                    DungeonItemID.TRRollerRoomRight,
                                                    DungeonItemID.TRChainChomps,
                                                    DungeonItemID.TRBigChest,
                                                    DungeonItemID.TRCrystarollerRoom
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        4,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.TRCompassChest,
                                                            DungeonItemID.TRRollerRoomLeft,
                                                            DungeonItemID.TRRollerRoomRight,
                                                            DungeonItemID.TRChainChomps,
                                                            DungeonItemID.TRBigChest,
                                                            DungeonItemID.TRCrystarollerRoom,
                                                            DungeonItemID.TRLaserBridgeTopLeft,
                                                            DungeonItemID.TRLaserBridgeTopRight,
                                                            DungeonItemID.TRLaserBridgeBottomLeft,
                                                            DungeonItemID.TRLaserBrdigeBottomRight
                                                        },
                                                        false,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[
                                        RequirementType.WorldStateStandardOpenEntranceShuffleNone],
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps,
                                    DungeonItemID.TRCrystarollerRoom,
                                    DungeonItemID.TRLaserBridgeTopLeft,
                                    DungeonItemID.TRLaserBridgeTopRight,
                                    DungeonItemID.TRLaserBridgeBottomLeft,
                                    DungeonItemID.TRLaserBrdigeBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps,
                                            DungeonItemID.TRBigKeyChest,
                                            DungeonItemID.TRBigChest,
                                            DungeonItemID.TRCrystarollerRoom,
                                            DungeonItemID.TRLaserBridgeTopLeft,
                                            DungeonItemID.TRLaserBridgeTopRight,
                                            DungeonItemID.TRLaserBridgeBottomLeft,
                                            DungeonItemID.TRLaserBrdigeBottomRight
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity]
                                    }),
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps,
                                            DungeonItemID.TRCrystarollerRoom,
                                            DungeonItemID.TRLaserBridgeTopLeft,
                                            DungeonItemID.TRLaserBridgeTopRight,
                                            DungeonItemID.TRLaserBridgeBottomLeft,
                                            DungeonItemID.TRLaserBrdigeBottomRight
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonAllInsanity]
                                    }),
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                }))
                        };
                    }
                case Locations.LocationID.GanonsTower:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTTileRoom
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTMapChest,
                                            DungeonItemID.GTFiresnakeRoom,
                                            DungeonItemID.GTTileRoom,
                                            DungeonItemID.GTBobsChest,
                                            DungeonItemID.GTBigKeyRoomTopLeft,
                                            DungeonItemID.GTBigKeyRoomTopRight,
                                            DungeonItemID.GTBigKeyChest,
                                            DungeonItemID.GTBigChest,
                                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                                            DungeonItemID.GTMiniHelmasaurRoomRight,
                                            DungeonItemID.GTPreMoldormChest
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                dungeon,
                                RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTMapChest,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTRandomizerRoomTopLeft,
                                    DungeonItemID.GTRandomizerRoomTopRight,
                                    DungeonItemID.GTRandomizerRoomBottomLeft,
                                    DungeonItemID.GTRandomizerRoomBottomRight,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTTileRoom
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTTileRoom
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.GTHopeRoomLeft,
                                                    DungeonItemID.GTHopeRoomRight,
                                                    DungeonItemID.GTBobsTorch,
                                                    DungeonItemID.GTDMsRoomTopLeft,
                                                    DungeonItemID.GTDMsRoomTopRight,
                                                    DungeonItemID.GTDMsRoomBottomLeft,
                                                    DungeonItemID.GTDMsRoomBottomRight,
                                                    DungeonItemID.GTMapChest,
                                                    DungeonItemID.GTFiresnakeRoom,
                                                    DungeonItemID.GTTileRoom,
                                                    DungeonItemID.GTBobsChest,
                                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                                    DungeonItemID.GTBigKeyRoomTopRight,
                                                    DungeonItemID.GTBigKeyChest,
                                                    DungeonItemID.GTBigChest,
                                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                                    DungeonItemID.GTPreMoldormChest
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTTileRoom
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.GTHopeRoomLeft,
                                                    DungeonItemID.GTHopeRoomRight,
                                                    DungeonItemID.GTBobsTorch,
                                                    DungeonItemID.GTDMsRoomTopLeft,
                                                    DungeonItemID.GTDMsRoomTopRight,
                                                    DungeonItemID.GTDMsRoomBottomLeft,
                                                    DungeonItemID.GTDMsRoomBottomRight,
                                                    DungeonItemID.GTMapChest,
                                                    DungeonItemID.GTFiresnakeRoom,
                                                    DungeonItemID.GTTileRoom,
                                                    DungeonItemID.GTBobsChest,
                                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                                    DungeonItemID.GTBigKeyRoomTopRight,
                                                    DungeonItemID.GTBigKeyChest,
                                                    DungeonItemID.GTBigChest,
                                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                                    DungeonItemID.GTPreMoldormChest
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTMapChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTTileRoom
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(4,
                                            new List<DungeonItemID>
                                            {
                                                DungeonItemID.GTHopeRoomLeft,
                                                DungeonItemID.GTHopeRoomRight,
                                                DungeonItemID.GTBobsTorch,
                                                DungeonItemID.GTDMsRoomTopLeft,
                                                DungeonItemID.GTDMsRoomTopRight,
                                                DungeonItemID.GTDMsRoomBottomLeft,
                                                DungeonItemID.GTDMsRoomBottomRight,
                                                DungeonItemID.GTFiresnakeRoom,
                                                DungeonItemID.GTTileRoom,
                                                DungeonItemID.GTBobsChest,
                                                DungeonItemID.GTBigKeyRoomTopLeft,
                                                DungeonItemID.GTBigKeyRoomTopRight,
                                                DungeonItemID.GTBigKeyChest,
                                                DungeonItemID.GTBigChest,
                                                DungeonItemID.GTMiniHelmasaurRoomLeft,
                                                DungeonItemID.GTMiniHelmasaurRoomRight,
                                                DungeonItemID.GTPreMoldormChest
                                            }, false,
                                            new List<IKeyLayout>
                                            {
                                                new EndKeyLayout()
                                            },
                                            dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTRandomizerRoomTopLeft,
                                    DungeonItemID.GTRandomizerRoomTopRight,
                                    DungeonItemID.GTRandomizerRoomBottomLeft,
                                    DungeonItemID.GTRandomizerRoomBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTTileRoom
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.GTHopeRoomLeft,
                                                    DungeonItemID.GTHopeRoomRight,
                                                    DungeonItemID.GTBobsTorch,
                                                    DungeonItemID.GTDMsRoomTopLeft,
                                                    DungeonItemID.GTDMsRoomTopRight,
                                                    DungeonItemID.GTDMsRoomBottomLeft,
                                                    DungeonItemID.GTDMsRoomBottomRight,
                                                    DungeonItemID.GTFiresnakeRoom,
                                                    DungeonItemID.GTTileRoom
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        4,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.GTHopeRoomLeft,
                                                            DungeonItemID.GTHopeRoomRight,
                                                            DungeonItemID.GTBobsTorch,
                                                            DungeonItemID.GTDMsRoomTopLeft,
                                                            DungeonItemID.GTDMsRoomTopRight,
                                                            DungeonItemID.GTDMsRoomBottomLeft,
                                                            DungeonItemID.GTDMsRoomBottomRight,
                                                            DungeonItemID.GTMapChest,
                                                            DungeonItemID.GTFiresnakeRoom,
                                                            DungeonItemID.GTRandomizerRoomTopLeft,
                                                            DungeonItemID.GTRandomizerRoomTopRight,
                                                            DungeonItemID.GTRandomizerRoomBottomLeft,
                                                            DungeonItemID.GTRandomizerRoomBottomRight,
                                                            DungeonItemID.GTTileRoom,
                                                            DungeonItemID.GTBobsChest,
                                                            DungeonItemID.GTBigKeyRoomTopLeft,
                                                            DungeonItemID.GTBigKeyRoomTopRight,
                                                            DungeonItemID.GTBigKeyChest
                                                        },
                                                        true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTTileRoom
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.GTHopeRoomLeft,
                                                    DungeonItemID.GTHopeRoomRight,
                                                    DungeonItemID.GTBobsTorch,
                                                    DungeonItemID.GTDMsRoomTopLeft,
                                                    DungeonItemID.GTDMsRoomTopRight,
                                                    DungeonItemID.GTDMsRoomBottomLeft,
                                                    DungeonItemID.GTDMsRoomBottomRight,
                                                    DungeonItemID.GTFiresnakeRoom,
                                                    DungeonItemID.GTTileRoom
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        4,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.GTHopeRoomLeft,
                                                            DungeonItemID.GTHopeRoomRight,
                                                            DungeonItemID.GTBobsTorch,
                                                            DungeonItemID.GTDMsRoomTopLeft,
                                                            DungeonItemID.GTDMsRoomTopRight,
                                                            DungeonItemID.GTDMsRoomBottomLeft,
                                                            DungeonItemID.GTDMsRoomBottomRight,
                                                            DungeonItemID.GTMapChest,
                                                            DungeonItemID.GTFiresnakeRoom,
                                                            DungeonItemID.GTTileRoom,
                                                            DungeonItemID.GTCompassRoomTopLeft,
                                                            DungeonItemID.GTCompassRoomTopRight,
                                                            DungeonItemID.GTCompassRoomBottomLeft,
                                                            DungeonItemID.GTCompassRoomBottomRight,
                                                            DungeonItemID.GTBobsChest,
                                                            DungeonItemID.GTBigKeyRoomTopLeft,
                                                            DungeonItemID.GTBigKeyRoomTopRight,
                                                            DungeonItemID.GTBigKeyChest
                                                        },
                                                        true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle])
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(dungeon));
        }
    }
}
