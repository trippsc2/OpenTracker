using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This class contains creation logic for key layout data.
    /// </summary>
    public class KeyLayoutFactory : IKeyLayoutFactory
    {
        private readonly IRequirementDictionary _requirements;
        private readonly BigKeyLayout.Factory _bigKeyFactory;
        private readonly EndKeyLayout.Factory _endFactory;
        private readonly SmallKeyLayout.Factory _smallKeyFactory;
        private readonly AlternativeRequirement.Factory _alternativeFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        /// The requirement dictionary.
        /// </param>
        /// <param name="bigKeyFactory">
        /// An Autofac factory for creating big key layouts.
        /// </param>
        /// <param name="endFactory">
        /// An Autofac factory for ending key layouts.
        /// </param>
        /// <param name="smallKeyFactory">
        /// An Autofac factory for creating small key layouts.
        /// </param>
        /// <param name="alternativeFactory">
        /// An Autofac factory for creating an alternative requirement.
        /// </param>
        public KeyLayoutFactory(
            IRequirementDictionary requirements, BigKeyLayout.Factory bigKeyFactory,
            EndKeyLayout.Factory endFactory, SmallKeyLayout.Factory smallKeyFactory,
            AlternativeRequirement.Factory alternativeFactory)
        {
            _requirements = requirements;
            _bigKeyFactory = bigKeyFactory;
            _endFactory = endFactory;
            _smallKeyFactory = smallKeyFactory;
            _alternativeFactory = alternativeFactory;
        }

        /// <summary>
        /// Returns the list of key layouts for the specified dungeon.
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon parent class.
        /// </param>
        /// <returns>
        /// The list of key layouts.
        /// </returns>
        public List<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            switch (dungeon.ID)
            {
                case LocationID.HyruleCastle:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _alternativeFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.KeyDropShuffleOnAllKeyShuffle],
                                    _requirements[RequirementType.KeyDropShuffleOffSmallKeyShuffleOn]
                                })),
                            _smallKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOffSmallKeyShuffleOff]),
                            _smallKeyFactory(
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
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            _bigKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnSmallKeyShuffleOnly]),
                            _bigKeyFactory(
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
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.HCBoomerangChest,
                                    DungeonItemID.HCBoomerangGuardDrop
                                },
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnNoKeyShuffle]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.HCBigKeyDrop
                                },
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnNoKeyShuffle])
                        };
                    }
                case LocationID.AgahnimTower:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(
                                2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ATRoom03,
                                    DungeonItemID.ATDarkMaze
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOffSmallKeyShuffleOff]),
                            _smallKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnSmallKeyShuffleOff])
                        };
                    }
                case LocationID.EasternPalace:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _alternativeFactory(new List<IRequirement>
                                {
                                    _requirements[RequirementType.AllKeyShuffle],
                                    _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOn]
                                })),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.EPCannonballChest,
                                    DungeonItemID.EPMapChest,
                                    DungeonItemID.EPCompassChest,
                                    DungeonItemID.EPBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _smallKeyFactory(
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
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.EPCannonballChest,
                                    DungeonItemID.EPMapChest,
                                    DungeonItemID.EPCompassChest,
                                    DungeonItemID.EPDarkSquarePot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement]),
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.EPBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                        };
                    }
                case LocationID.DesertPalace:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _requirements[RequirementType.AllKeyShuffle]),
                            _smallKeyFactory(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPBigChest
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPCompassChest,
                                    DungeonItemID.DPBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _smallKeyFactory(
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
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            _bigKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnSmallKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPTiles1Pot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _smallKeyFactory
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
                                                    _smallKeyFactory(
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
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPBeamosHallPot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPTiles1Pot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                    _smallKeyFactory(
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
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPTiles2Pot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPTiles1Pot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                        };
                    }
                case LocationID.TowerOfHera:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _requirements[RequirementType.AllKeyShuffle]),
                            _smallKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.GuaranteedBossItemsOffBigKeyShuffleOnly]),
                            _smallKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.GuaranteedBossItemsOnBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBasementCage,
                                    DungeonItemID.ToHMapChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.GuaranteedBossItemsOffSmallKeyShuffleOff]),
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.GuaranteedBossItemsOnSmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.BigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.ToHBasementCage,
                                            DungeonItemID.ToHMapChest
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.BigKeyShuffleOff])
                        };
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _requirements[RequirementType.AllKeyShuffle]),
                            _smallKeyFactory(
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
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.BigKeyShuffleOnly]),
                            _bigKeyFactory(
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
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.BigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDCompassChest,
                                    DungeonItemID.PoDDarkBasementLeft,
                                    DungeonItemID.PoDDarkBasementRight,
                                    DungeonItemID.PoDHarmlessHellway
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.BigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDDarkMazeTop,
                                    DungeonItemID.PoDDarkMazeBottom
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.BigKeyShuffleOff])
                        };
                    }
                case LocationID.SwampPalace:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _requirements[RequirementType.AllKeyShuffle]),
                            _smallKeyFactory(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly]),
                            _bigKeyFactory(
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
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPBoss
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOff]),
                            _smallKeyFactory(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                    _smallKeyFactory(
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
                                                            _smallKeyFactory(
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
                                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        },
                                        dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
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
                                                            _smallKeyFactory(
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
                                                                    _smallKeyFactory(
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
                                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                },
                                                dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPTrench1Pot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        3,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            _smallKeyFactory(
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
                                                                    _smallKeyFactory(
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
                                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                },
                                                dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPBigChest,
                                    DungeonItemID.SPCompassChest,
                                    DungeonItemID.SPHookshotPot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
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
                                                            _smallKeyFactory(
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
                                                                    _smallKeyFactory(
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
                                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                },
                                                dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPFloodedRoomLeft,
                                    DungeonItemID.SPFloodedRoomRight,
                                    DungeonItemID.SPWaterfallRoom,
                                    DungeonItemID.SPWaterwayPot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
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
                                                            _smallKeyFactory(
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
                                                                    _smallKeyFactory(
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
                                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                },
                                                dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPWestChest,
                                    DungeonItemID.SPBigKeyChest,
                                    DungeonItemID.SPTrench2Pot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
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
                                                            _smallKeyFactory(
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
                                                                    _smallKeyFactory(
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
                                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                },
                                                dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPBoss
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
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
                                                            _smallKeyFactory(
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
                                                                    _smallKeyFactory(
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
                                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                },
                                                dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffBigKeyShuffleOff])
                        };
                    }
                case LocationID.SkullWoods:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _requirements[RequirementType.AllKeyShuffle]),
                            _smallKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[
                                    RequirementType.KeyDropShuffleOffBigKeyShuffleOnlyItemPlacementAdvanced]),
                            _smallKeyFactory(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWPinballRoom
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[
                                    RequirementType.KeyDropShuffleOffBigKeyShuffleOnlyItemPlacementBasic]),
                            _bigKeyFactory(
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
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOffItemPlacementAdvanced]),
                            _bigKeyFactory(
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
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOffItemPlacementBasic]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBoss
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOffItemPlacementAdvanced]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOffItemPlacementBasic])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOff]),
                            _smallKeyFactory(
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
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnlyItemPlacementAdvanced]),
                            _smallKeyFactory(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWPinballRoom
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnlyItemPlacementBasic]),
                            _bigKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffSmallKeyShuffleOnly]),
                            _bigKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOnSmallKeyShuffleOnly]),
                            _bigKeyFactory(
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
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOffItemPlacementAdvanced]),
                            _bigKeyFactory(
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
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                    _smallKeyFactory(
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
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOffItemPlacementBasic]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWSpikeCornerDrop
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOffItemPlacementAdvanced]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOffItemPlacementBasic])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBoss
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOffItemPlacementAdvanced]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                    _smallKeyFactory(
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
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOffItemPlacementBasic])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffBigKeyShuffleOff])
                        };
                    }
                case LocationID.ThievesTown:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _requirements[RequirementType.AllKeyShuffle]),
                            _smallKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TTMapChest,
                                    DungeonItemID.TTAmbushChest,
                                    DungeonItemID.TTCompassChest,
                                    DungeonItemID.TTBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _smallKeyFactory(
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
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TTMapChest,
                                    DungeonItemID.TTAmbushChest,
                                    DungeonItemID.TTCompassChest,
                                    DungeonItemID.TTBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                        };
                    }
                case LocationID.IcePalace:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _requirements[RequirementType.AllKeyShuffle]),
                            _smallKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[
                                    RequirementType.KeyDropShuffleOffBigKeyShuffleOnlyGuaranteedBossItemsOrItemPlacementBasic]),
                            _smallKeyFactory(
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
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[
                                    RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOnlyItemPlacementAdvanced]),
                            _bigKeyFactory(
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
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[
                                            RequirementType.GuaranteedBossItemsOnOrItemPlacementBasicSmallKeyShuffleOff]),
                                    _smallKeyFactory(
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
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[
                                            RequirementType.GuaranteedBossItemsOffSmallKeyShuffleOffItemPlacementAdvanced])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _smallKeyFactory(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.IPJellyDrop
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPJellyDrop,
                                            DungeonItemID.IPConveyerDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[
                                                    RequirementType.GuaranteedBossItemsOnOrItemPlacementBasic]),
                                            _smallKeyFactory(
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
                                                    _smallKeyFactory(
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
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[
                                                    RequirementType.GuaranteedBossItemsOffItemPlacementAdvanced])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPConveyerDrop
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPJellyDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.IPCompassChest,
                                                    DungeonItemID.IPJellyDrop,
                                                    DungeonItemID.IPConveyerDrop
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
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
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.GuaranteedBossItemsOnOrItemPlacementBasic]),
                                                    _smallKeyFactory(
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
                                                            _smallKeyFactory(
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
                                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.GuaranteedBossItemsOffItemPlacementAdvanced])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
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
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPJellyDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.IPCompassChest,
                                                    DungeonItemID.IPJellyDrop,
                                                    DungeonItemID.IPConveyerDrop
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
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
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.GuaranteedBossItemsOnOrItemPlacementBasic]),
                                                    _smallKeyFactory(
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
                                                            _smallKeyFactory(
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
                                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.GuaranteedBossItemsOffItemPlacementAdvanced])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                        };
                    }
                case LocationID.MiseryMire:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _requirements[RequirementType.AllKeyShuffle]),
                            _smallKeyFactory(
                                3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMMapChest,
                                    DungeonItemID.MMBoss
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOnly]),
                            _smallKeyFactory(
                                3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMMapChest
                                }, true,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOnBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMMapChest,
                                            DungeonItemID.MMBoss
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.GuaranteedBossItemsOffSmallKeyShuffleOff]),
                                    _smallKeyFactory(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMMapChest
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.GuaranteedBossItemsOnSmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMMapChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMBigChest,
                                                    DungeonItemID.MMMapChest,
                                                    DungeonItemID.MMBoss
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.GuaranteedBossItemsOff]),
                                            _smallKeyFactory(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMBigChest,
                                                    DungeonItemID.MMMapChest
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.GuaranteedBossItemsOn])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMCompassChest,
                                    DungeonItemID.MMBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMMapChest
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(3,
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
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                                        _requirements[RequirementType.GuaranteedBossItemsOff]),
                                                    _smallKeyFactory(3,
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
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                                        _requirements[RequirementType.GuaranteedBossItemsOn])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _smallKeyFactory(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMSpikesPot,
                                    DungeonItemID.MMFishbonePot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        5,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMMapChest,
                                            DungeonItemID.MMSpikesPot,
                                            DungeonItemID.MMFishbonePot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                6,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMBigChest,
                                                    DungeonItemID.MMMapChest,
                                                    DungeonItemID.MMSpikesPot,
                                                    DungeonItemID.MMFishbonePot,
                                                    DungeonItemID.MMConveyerCrystalDrop
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMSpikesPot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMSpikesPot,
                                            DungeonItemID.MMFishbonePot
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMBigChest,
                                                    DungeonItemID.MMMapChest,
                                                    DungeonItemID.MMSpikesPot,
                                                    DungeonItemID.MMFishbonePot
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.MMBridgeChest,
                                                            DungeonItemID.MMSpikeChest,
                                                            DungeonItemID.MMMainLobby,
                                                            DungeonItemID.MMBigChest,
                                                            DungeonItemID.MMMapChest,
                                                            DungeonItemID.MMSpikesPot,
                                                            DungeonItemID.MMFishbonePot,
                                                            DungeonItemID.MMConveyerCrystalDrop
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMMapChest,
                                    DungeonItemID.MMFishbonePot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMSpikesPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMBigChest,
                                                    DungeonItemID.MMMapChest,
                                                    DungeonItemID.MMSpikesPot,
                                                    DungeonItemID.MMFishbonePot
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.MMBridgeChest,
                                                            DungeonItemID.MMSpikeChest,
                                                            DungeonItemID.MMMainLobby,
                                                            DungeonItemID.MMBigChest,
                                                            DungeonItemID.MMMapChest,
                                                            DungeonItemID.MMSpikesPot,
                                                            DungeonItemID.MMFishbonePot,
                                                            DungeonItemID.MMConveyerCrystalDrop
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMConveyerCrystalDrop
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMSpikesPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMMapChest,
                                                    DungeonItemID.MMSpikesPot,
                                                    DungeonItemID.MMFishbonePot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.MMBridgeChest,
                                                            DungeonItemID.MMSpikeChest,
                                                            DungeonItemID.MMMainLobby,
                                                            DungeonItemID.MMBigChest,
                                                            DungeonItemID.MMMapChest,
                                                            DungeonItemID.MMSpikesPot,
                                                            DungeonItemID.MMFishbonePot,
                                                            DungeonItemID.MMConveyerCrystalDrop
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMCompassChest,
                                    DungeonItemID.MMBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMSpikesPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMMapChest,
                                                    DungeonItemID.MMSpikesPot,
                                                    DungeonItemID.MMFishbonePot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6,
                                                        new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.MMBridgeChest,
                                                            DungeonItemID.MMSpikeChest,
                                                            DungeonItemID.MMMainLobby,
                                                            DungeonItemID.MMMapChest,
                                                            DungeonItemID.MMSpikesPot,
                                                            DungeonItemID.MMFishbonePot,
                                                            DungeonItemID.MMConveyerCrystalDrop
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                        };
                    }
                case LocationID.TurtleRock:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _requirements[RequirementType.AllKeyShuffle]),
                            _smallKeyFactory(
                                2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
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
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                    DungeonItemID.TRLaserBridgeBottomRight
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOffWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOnly]),
                            _smallKeyFactory(
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
                                    DungeonItemID.TRLaserBridgeBottomRight
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOffWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
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
                                                            DungeonItemID.TRLaserBridgeBottomRight
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.TRCompassChest,
                                                    DungeonItemID.TRRollerRoomLeft,
                                                    DungeonItemID.TRRollerRoomRight,
                                                    DungeonItemID.TRChainChomps,
                                                    DungeonItemID.TRBigChest,
                                                    DungeonItemID.TRCrystarollerRoom
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
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
                                                            DungeonItemID.TRLaserBridgeBottomRight
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]),
                            _bigKeyFactory(
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
                                    DungeonItemID.TRLaserBridgeBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            DungeonItemID.TRLaserBridgeBottomRight
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                            DungeonItemID.TRLaserBridgeBottomRight
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff]),
                            _smallKeyFactory(
                                3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps,
                                    DungeonItemID.TRPokey1Drop
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        5,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps,
                                            DungeonItemID.TRBigChest,
                                            DungeonItemID.TRCrystarollerRoom,
                                            DungeonItemID.TRPokey1Drop,
                                            DungeonItemID.TRPokey2Drop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                6,
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
                                                    DungeonItemID.TRLaserBridgeBottomRight,
                                                    DungeonItemID.TRPokey1Drop,
                                                    DungeonItemID.TRPokey2Drop
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOnly]),
                            _smallKeyFactory(
                                6,
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
                                    DungeonItemID.TRLaserBridgeBottomRight,
                                    DungeonItemID.TRPokey1Drop,
                                    DungeonItemID.TRPokey2Drop
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps,
                                    DungeonItemID.TRPokey1Drop
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps,
                                            DungeonItemID.TRPokey1Drop
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.TRCompassChest,
                                                    DungeonItemID.TRRollerRoomLeft,
                                                    DungeonItemID.TRRollerRoomRight,
                                                    DungeonItemID.TRChainChomps,
                                                    DungeonItemID.TRBigChest,
                                                    DungeonItemID.TRCrystarollerRoom,
                                                    DungeonItemID.TRPokey1Drop,
                                                    DungeonItemID.TRPokey2Drop
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6,
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
                                                            DungeonItemID.TRLaserBridgeBottomRight,
                                                            DungeonItemID.TRPokey1Drop,
                                                            DungeonItemID.TRPokey2Drop
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRPokey2Drop
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps,
                                            DungeonItemID.TRPokey1Drop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.TRCompassChest,
                                                    DungeonItemID.TRRollerRoomLeft,
                                                    DungeonItemID.TRRollerRoomRight,
                                                    DungeonItemID.TRChainChomps,
                                                    DungeonItemID.TRBigChest,
                                                    DungeonItemID.TRCrystarollerRoom,
                                                    DungeonItemID.TRPokey1Drop
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6,
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
                                                            DungeonItemID.TRLaserBridgeBottomRight,
                                                            DungeonItemID.TRPokey1Drop
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps,
                                            DungeonItemID.TRPokey1Drop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                5,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.TRCompassChest,
                                                    DungeonItemID.TRRollerRoomLeft,
                                                    DungeonItemID.TRRollerRoomRight,
                                                    DungeonItemID.TRChainChomps,
                                                    DungeonItemID.TRBigChest,
                                                    DungeonItemID.TRCrystarollerRoom,
                                                    DungeonItemID.TRPokey1Drop,
                                                    DungeonItemID.TRPokey2Drop
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
                                                        6,
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
                                                            DungeonItemID.TRLaserBridgeBottomRight,
                                                            DungeonItemID.TRPokey1Drop,
                                                            DungeonItemID.TRPokey2Drop
                                                        }, false,
                                                        new List<IKeyLayout>
                                                        {
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]),
                            _bigKeyFactory(
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
                                    DungeonItemID.TRLaserBridgeBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        6,
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
                                            DungeonItemID.TRLaserBridgeBottomRight,
                                            DungeonItemID.TRPokey1Drop,
                                            DungeonItemID.TRPokey2Drop
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        6,
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
                                            DungeonItemID.TRLaserBridgeBottomRight,
                                            DungeonItemID.TRPokey1Drop,
                                            DungeonItemID.TRPokey2Drop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff]),
                        };
                    }
                case LocationID.GanonsTower:
                    {
                        return new List<IKeyLayout>
                        {
                            _endFactory(
                                _requirements[RequirementType.AllKeyShuffle]),
                            _smallKeyFactory(
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
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
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
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly]),
                            _bigKeyFactory(
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
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _bigKeyFactory(
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
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTMapChest
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(4,
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
                                                _endFactory(_requirements[RequirementType.NoRequirement])
                                            },
                                            dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTRandomizerRoomTopLeft,
                                    DungeonItemID.GTRandomizerRoomTopRight,
                                    DungeonItemID.GTRandomizerRoomBottomLeft,
                                    DungeonItemID.GTRandomizerRoomBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
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
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
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
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
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
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(
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
                                                        }, true,
                                                        new List<IKeyLayout>
                                                        {
                                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                            _smallKeyFactory(
                                7,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                    DungeonItemID.GTConveyorCrossPot,
                                    DungeonItemID.GTDoubleSwitchPot,
                                    DungeonItemID.GTMiniHelmasaurDrop
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(
                                        8,
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
                                            DungeonItemID.GTCompassRoomTopLeft,
                                            DungeonItemID.GTCompassRoomTopRight,
                                            DungeonItemID.GTCompassRoomBottomLeft,
                                            DungeonItemID.GTCompassRoomBottomRight,
                                            DungeonItemID.GTBobsChest,
                                            DungeonItemID.GTBigKeyRoomTopLeft,
                                            DungeonItemID.GTBigKeyRoomTopRight,
                                            DungeonItemID.GTBigKeyChest,
                                            DungeonItemID.GTBigChest,
                                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                                            DungeonItemID.GTMiniHelmasaurRoomRight,
                                            DungeonItemID.GTPreMoldormChest,
                                            DungeonItemID.GTConveyorCrossPot,
                                            DungeonItemID.GTDoubleSwitchPot,
                                            DungeonItemID.GTConveyorStarPitsPot,
                                            DungeonItemID.GTMiniHelmasaurDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                }, dungeon,
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTConveyorCrossPot,
                                    DungeonItemID.GTDoubleSwitchPot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        7,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTTileRoom,
                                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                                            DungeonItemID.GTMiniHelmasaurRoomRight,
                                            DungeonItemID.GTConveyorCrossPot,
                                            DungeonItemID.GTDoubleSwitchPot,
                                            DungeonItemID.GTMiniHelmasaurDrop
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                8,
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
                                                    DungeonItemID.GTCompassRoomTopLeft,
                                                    DungeonItemID.GTCompassRoomTopRight,
                                                    DungeonItemID.GTCompassRoomBottomLeft,
                                                    DungeonItemID.GTCompassRoomBottomRight,
                                                    DungeonItemID.GTBobsChest,
                                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                                    DungeonItemID.GTBigKeyRoomTopRight,
                                                    DungeonItemID.GTBigKeyChest,
                                                    DungeonItemID.GTBigChest,
                                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                                    DungeonItemID.GTPreMoldormChest,
                                                    DungeonItemID.GTConveyorCrossPot,
                                                    DungeonItemID.GTDoubleSwitchPot,
                                                    DungeonItemID.GTConveyorStarPitsPot,
                                                    DungeonItemID.GTMiniHelmasaurDrop
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTConveyorStarPitsPot
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        7,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTTileRoom,
                                            DungeonItemID.GTConveyorCrossPot,
                                            DungeonItemID.GTDoubleSwitchPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                8,
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
                                                    DungeonItemID.GTCompassRoomTopLeft,
                                                    DungeonItemID.GTCompassRoomTopRight,
                                                    DungeonItemID.GTCompassRoomBottomLeft,
                                                    DungeonItemID.GTCompassRoomBottomRight,
                                                    DungeonItemID.GTBobsChest,
                                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                                    DungeonItemID.GTBigKeyRoomTopRight,
                                                    DungeonItemID.GTBigKeyChest,
                                                    DungeonItemID.GTBigChest,
                                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                                    DungeonItemID.GTPreMoldormChest,
                                                    DungeonItemID.GTConveyorCrossPot,
                                                    DungeonItemID.GTDoubleSwitchPot,
                                                    DungeonItemID.GTConveyorStarPitsPot,
                                                    DungeonItemID.GTMiniHelmasaurDrop
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                            _bigKeyFactory(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTRandomizerRoomTopLeft,
                                    DungeonItemID.GTRandomizerRoomTopRight,
                                    DungeonItemID.GTRandomizerRoomBottomLeft,
                                    DungeonItemID.GTRandomizerRoomBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    _endFactory(
                                        _requirements[RequirementType.SmallKeyShuffleOn]),
                                    _smallKeyFactory(
                                        7,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTTileRoom,
                                            DungeonItemID.GTConveyorCrossPot,
                                            DungeonItemID.GTDoubleSwitchPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(
                                                8,
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
                                                    DungeonItemID.GTCompassRoomTopLeft,
                                                    DungeonItemID.GTCompassRoomTopRight,
                                                    DungeonItemID.GTCompassRoomBottomLeft,
                                                    DungeonItemID.GTCompassRoomBottomRight,
                                                    DungeonItemID.GTBobsChest,
                                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                                    DungeonItemID.GTBigKeyRoomTopRight,
                                                    DungeonItemID.GTBigKeyChest,
                                                    DungeonItemID.GTConveyorCrossPot,
                                                    DungeonItemID.GTDoubleSwitchPot,
                                                    DungeonItemID.GTConveyorStarPitsPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                                }, dungeon,
                                        _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.SmallKeyShuffleOff])
                                },
                                _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(dungeon));
        }
    }
}
