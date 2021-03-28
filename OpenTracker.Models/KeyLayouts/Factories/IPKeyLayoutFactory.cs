using System.Collections.Generic;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Ice Palace key layouts.
    /// </summary>
    public class IPKeyLayoutFactory : IIPKeyLayoutFactory
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
        public IPKeyLayoutFactory(
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
                    _smallKeyFactory(2,
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
                        new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOffBigKeyShuffleOnlyGuaranteedBossItemsOrItemPlacementBasic]),
                    _smallKeyFactory(2,
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
                        new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOnlyItemPlacementAdvanced]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(2,
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
                                    RequirementType
                                        .GuaranteedBossItemsOnOrItemPlacementBasicSmallKeyShuffleOff]),
                            _smallKeyFactory(2,
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
                                    RequirementType
                                        .GuaranteedBossItemsOffSmallKeyShuffleOffItemPlacementAdvanced])
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                    _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.IPJellyDrop}, false,
                        new List<IKeyLayout>
                        {
                            _smallKeyFactory(2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPJellyDrop,
                                    DungeonItemID.IPConveyerDrop
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(6,
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
                                    _smallKeyFactory(5,
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
                                            _smallKeyFactory(6,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[
                                            RequirementType.GuaranteedBossItemsOffItemPlacementAdvanced])
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                    _bigKeyFactory(
                        new List<DungeonItemID> {DungeonItemID.IPCompassChest, DungeonItemID.IPConveyerDrop},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.IPJellyDrop}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPJellyDrop,
                                            DungeonItemID.IPConveyerDrop
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(6,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[
                                                    RequirementType
                                                        .GuaranteedBossItemsOnOrItemPlacementBasic]),
                                            _smallKeyFactory(5,
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
                                                    _smallKeyFactory(6,
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
                                                            _endFactory(
                                                                _requirements[
                                                                    RequirementType
                                                                        .NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[
                                                    RequirementType
                                                        .GuaranteedBossItemsOffItemPlacementAdvanced])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1, new List<DungeonItemID> {DungeonItemID.IPJellyDrop}, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPJellyDrop,
                                            DungeonItemID.IPConveyerDrop
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _smallKeyFactory(6,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[
                                                    RequirementType
                                                        .GuaranteedBossItemsOnOrItemPlacementBasic]),
                                            _smallKeyFactory(5,
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
                                                    _smallKeyFactory(6,
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
                                                            _endFactory(
                                                                _requirements[
                                                                    RequirementType
                                                                        .NoRequirement])
                                                        }, dungeon,
                                                        _requirements[
                                                            RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[
                                                    RequirementType
                                                        .GuaranteedBossItemsOffItemPlacementAdvanced])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                };
        }
    }
}