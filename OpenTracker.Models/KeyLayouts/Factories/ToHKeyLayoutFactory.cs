using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Tower of Hera key layouts.
    /// </summary>
    public class ToHKeyLayoutFactory : IToHKeyLayoutFactory
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
        public ToHKeyLayoutFactory(
            IRequirementDictionary requirements, BigKeyLayout.Factory bigKeyFactory, EndKeyLayout.Factory endFactory,
            SmallKeyLayout.Factory smallKeyFactory)
        {
            _requirements = requirements;
            
            _bigKeyFactory = bigKeyFactory;
            _endFactory = endFactory;
            _smallKeyFactory = smallKeyFactory;
        }
        
        public List<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            return new()
            {
                    _endFactory(_requirements[RequirementType.AllKeyShuffle]),
                    _smallKeyFactory(1,
                        new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBasementCage,
                            DungeonItemID.ToHMapChest,
                            DungeonItemID.ToHBigKeyChest,
                            DungeonItemID.ToHCompassChest,
                            DungeonItemID.ToHBigChest,
                            DungeonItemID.ToHBoss
                        }, false,
                        new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
                        _requirements[RequirementType.GuaranteedBossItemsOffBigKeyShuffleOnly]),
                    _smallKeyFactory(1,
                        new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBasementCage,
                            DungeonItemID.ToHMapChest,
                            DungeonItemID.ToHBigKeyChest,
                            DungeonItemID.ToHCompassChest,
                            DungeonItemID.ToHBigChest
                        }, false,
                        new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
                        _requirements[RequirementType.GuaranteedBossItemsOnBigKeyShuffleOnly]),
                    _bigKeyFactory(
                        new List<DungeonItemID> {DungeonItemID.ToHBasementCage, DungeonItemID.ToHMapChest},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1,
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
                            _smallKeyFactory(1,
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
                        }, _requirements[RequirementType.BigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.ToHBigKeyChest},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBasementCage, DungeonItemID.ToHMapChest
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.BigKeyShuffleOff])
                };
        }
    }
}