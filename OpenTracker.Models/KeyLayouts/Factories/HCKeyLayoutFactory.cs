using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Multiple;

namespace OpenTracker.Models.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Hyrule Castle key layouts.
    /// </summary>
    public class HCKeyLayoutFactory : IHCKeyLayoutFactory
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
        public HCKeyLayoutFactory(
            IRequirementDictionary requirements, BigKeyLayout.Factory bigKeyFactory, EndKeyLayout.Factory endFactory,
            SmallKeyLayout.Factory smallKeyFactory, AlternativeRequirement.Factory alternativeFactory)
        {
            _requirements = requirements;
            
            _bigKeyFactory = bigKeyFactory;
            _endFactory = endFactory;
            _smallKeyFactory = smallKeyFactory;
            _alternativeFactory = alternativeFactory;
        }
        
        public List<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            return new()
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
    }
}