using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Swamp Palace key layouts.
    /// </summary>
    public class SPKeyLayoutFactory : ISPKeyLayoutFactory
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
        public SPKeyLayoutFactory(
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
                    _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                        new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.SPBoss},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOff]),
                    _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                        new List<IKeyLayout>
                        {
                            _smallKeyFactory(2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPTrench1Pot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(5,
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
                                                    _smallKeyFactory(6,
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
                                                            _endFactory(
                                                                _requirements[
                                                                    RequirementType
                                                                        .NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.SPMapChest, DungeonItemID.SPPotRowPot},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPTrench1Pot
                                                }, true,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(5,
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
                                                            _smallKeyFactory(6,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID
                                                                        .SPEntrance,
                                                                    DungeonItemID
                                                                        .SPMapChest,
                                                                    DungeonItemID
                                                                        .SPPotRowPot,
                                                                    DungeonItemID
                                                                        .SPBigChest,
                                                                    DungeonItemID
                                                                        .SPCompassChest,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomLeft,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomRight,
                                                                    DungeonItemID
                                                                        .SPWaterfallRoom,
                                                                    DungeonItemID
                                                                        .SPTrench1Pot,
                                                                    DungeonItemID
                                                                        .SPHookshotPot,
                                                                    DungeonItemID
                                                                        .SPWaterwayPot
                                                                }, true,
                                                                new List<IKeyLayout>
                                                                {
                                                                    _endFactory(
                                                                        _requirements[
                                                                            RequirementType
                                                                                .NoRequirement])
                                                                }, dungeon,
                                                                _requirements[
                                                                    RequirementType
                                                                        .NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.SPTrench1Pot},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(5,
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
                                                            _smallKeyFactory(6,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID
                                                                        .SPEntrance,
                                                                    DungeonItemID
                                                                        .SPMapChest,
                                                                    DungeonItemID
                                                                        .SPPotRowPot,
                                                                    DungeonItemID
                                                                        .SPBigChest,
                                                                    DungeonItemID
                                                                        .SPCompassChest,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomLeft,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomRight,
                                                                    DungeonItemID
                                                                        .SPWaterfallRoom,
                                                                    DungeonItemID
                                                                        .SPHookshotPot,
                                                                    DungeonItemID
                                                                        .SPWaterwayPot
                                                                }, false,
                                                                new List<IKeyLayout>
                                                                {
                                                                    _endFactory(
                                                                        _requirements[
                                                                            RequirementType
                                                                                .NoRequirement])
                                                                }, dungeon,
                                                                _requirements[
                                                                    RequirementType
                                                                        .NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.SPBigChest, DungeonItemID.SPCompassChest, DungeonItemID.SPHookshotPot
                        },
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPTrench1Pot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(5,
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
                                                            _smallKeyFactory(6,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID
                                                                        .SPEntrance,
                                                                    DungeonItemID
                                                                        .SPMapChest,
                                                                    DungeonItemID
                                                                        .SPPotRowPot,
                                                                    DungeonItemID
                                                                        .SPBigChest,
                                                                    DungeonItemID
                                                                        .SPCompassChest,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomLeft,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomRight,
                                                                    DungeonItemID
                                                                        .SPWaterfallRoom,
                                                                    DungeonItemID
                                                                        .SPTrench1Pot,
                                                                    DungeonItemID
                                                                        .SPHookshotPot,
                                                                    DungeonItemID
                                                                        .SPWaterwayPot
                                                                }, true,
                                                                new List<IKeyLayout>
                                                                {
                                                                    _endFactory(
                                                                        _requirements[
                                                                            RequirementType
                                                                                .NoRequirement])
                                                                }, dungeon,
                                                                _requirements[
                                                                    RequirementType
                                                                        .NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPTrench1Pot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(5,
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
                                                            _smallKeyFactory(6,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID
                                                                        .SPEntrance,
                                                                    DungeonItemID
                                                                        .SPMapChest,
                                                                    DungeonItemID
                                                                        .SPPotRowPot,
                                                                    DungeonItemID
                                                                        .SPBigChest,
                                                                    DungeonItemID
                                                                        .SPCompassChest,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomLeft,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomRight,
                                                                    DungeonItemID
                                                                        .SPWaterfallRoom,
                                                                    DungeonItemID
                                                                        .SPTrench1Pot,
                                                                    DungeonItemID
                                                                        .SPHookshotPot,
                                                                    DungeonItemID
                                                                        .SPWaterwayPot
                                                                }, true,
                                                                new List<IKeyLayout>
                                                                {
                                                                    _endFactory(
                                                                        _requirements[
                                                                            RequirementType
                                                                                .NoRequirement])
                                                                }, dungeon,
                                                                _requirements[
                                                                    RequirementType
                                                                        .NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.SPWestChest, DungeonItemID.SPBigKeyChest, DungeonItemID.SPTrench2Pot
                        },
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPTrench1Pot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(5,
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
                                                            _smallKeyFactory(6,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID
                                                                        .SPEntrance,
                                                                    DungeonItemID
                                                                        .SPMapChest,
                                                                    DungeonItemID
                                                                        .SPPotRowPot,
                                                                    DungeonItemID
                                                                        .SPCompassChest,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomLeft,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomRight,
                                                                    DungeonItemID
                                                                        .SPWaterfallRoom,
                                                                    DungeonItemID
                                                                        .SPTrench1Pot,
                                                                    DungeonItemID
                                                                        .SPHookshotPot,
                                                                    DungeonItemID
                                                                        .SPWaterwayPot
                                                                }, false,
                                                                new List<IKeyLayout>
                                                                {
                                                                    _endFactory(
                                                                        _requirements[
                                                                            RequirementType
                                                                                .NoRequirement])
                                                                }, dungeon,
                                                                _requirements[
                                                                    RequirementType
                                                                        .NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.SPBoss},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPTrench1Pot
                                                }, false,
                                                new List<IKeyLayout>
                                                {
                                                    _smallKeyFactory(5,
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
                                                            _smallKeyFactory(6,
                                                                new List<DungeonItemID>
                                                                {
                                                                    DungeonItemID
                                                                        .SPEntrance,
                                                                    DungeonItemID
                                                                        .SPMapChest,
                                                                    DungeonItemID
                                                                        .SPPotRowPot,
                                                                    DungeonItemID
                                                                        .SPCompassChest,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomLeft,
                                                                    DungeonItemID
                                                                        .SPFloodedRoomRight,
                                                                    DungeonItemID
                                                                        .SPWaterfallRoom,
                                                                    DungeonItemID
                                                                        .SPTrench1Pot,
                                                                    DungeonItemID
                                                                        .SPHookshotPot,
                                                                    DungeonItemID
                                                                        .SPWaterwayPot
                                                                }, false,
                                                                new List<IKeyLayout>
                                                                {
                                                                    _endFactory(
                                                                        _requirements[
                                                                            RequirementType
                                                                                .NoRequirement])
                                                                }, dungeon,
                                                                _requirements[
                                                                    RequirementType
                                                                        .NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[RequirementType.KeyDropShuffleOnGuaranteedBossItemsOffBigKeyShuffleOff])
                };
        }
    }
}