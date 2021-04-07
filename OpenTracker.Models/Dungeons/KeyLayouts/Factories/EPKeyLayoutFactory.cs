using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Multiple;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Eastern Palace key layouts.
    /// </summary>
    public class EPKeyLayoutFactory : IEPKeyLayoutFactory
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
        public EPKeyLayoutFactory(
            IRequirementDictionary requirements, BigKeyLayout.Factory bigKeyFactory, EndKeyLayout.Factory endFactory,
            SmallKeyLayout.Factory smallKeyFactory, AlternativeRequirement.Factory alternativeFactory)
        {
            _requirements = requirements;
            
            _bigKeyFactory = bigKeyFactory;
            _endFactory = endFactory;
            _smallKeyFactory = smallKeyFactory;
            _alternativeFactory = alternativeFactory;
        }
        
        public IList<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
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
    }
}