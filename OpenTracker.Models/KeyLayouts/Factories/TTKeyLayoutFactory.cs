using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Thieves Town key layouts.
    /// </summary>
    public class TTKeyLayoutFactory : ITTKeyLayoutFactory
    {
        private readonly IRequirementDictionary _requirements;
        
        private readonly BigKeyLayout.Factory _bigKeyFactory;
        private readonly EndKeyLayout.Factory _endFactory;
        private readonly SmallKeyLayout.Factory _smallKeyFactory;

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
        public TTKeyLayoutFactory(
            IRequirementDictionary requirements, BigKeyLayout.Factory bigKeyFactory, EndKeyLayout.Factory endFactory,
            SmallKeyLayout.Factory smallKeyFactory)
        {
            _requirements = requirements;
            
            _bigKeyFactory = bigKeyFactory;
            _endFactory = endFactory;
            _smallKeyFactory = smallKeyFactory;
        }
        
        public IList<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            return new List<IKeyLayout>
            {
                    _endFactory(_requirements[RequirementType.AllKeyShuffle]),
                    _smallKeyFactory(1,
                        new List<DungeonItemID>
                        {
                            DungeonItemID.TTMapChest,
                            DungeonItemID.TTAmbushChest,
                            DungeonItemID.TTCompassChest,
                            DungeonItemID.TTBigKeyChest,
                            DungeonItemID.TTBlindsCell,
                            DungeonItemID.TTBigChest
                        }, false,
                        new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1,
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
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                    _smallKeyFactory(1,
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
                            _smallKeyFactory(3,
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
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1,
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
                                    _smallKeyFactory(3,
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
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                };
        }
    }
}