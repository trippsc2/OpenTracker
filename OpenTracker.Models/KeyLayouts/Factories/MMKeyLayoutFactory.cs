using System.Collections.Generic;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Misery Mire key layouts.
    /// </summary>
    public class MMKeyLayoutFactory : IMMKeyLayoutFactory
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
        public MMKeyLayoutFactory(
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
                    _smallKeyFactory(3,
                        new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMMainLobby,
                            DungeonItemID.MMBigChest,
                            DungeonItemID.MMMapChest,
                            DungeonItemID.MMBoss
                        }, false,
                        new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
                        _requirements[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOffBigKeyShuffleOnly]),
                    _smallKeyFactory(3,
                        new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMMainLobby,
                            DungeonItemID.MMBigChest,
                            DungeonItemID.MMMapChest
                        }, true,
                        new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
                        _requirements[RequirementType.KeyDropShuffleOffGuaranteedBossItemsOnBigKeyShuffleOnly]),
                    _bigKeyFactory(
                        new List<DungeonItemID> {DungeonItemID.MMBridgeChest, DungeonItemID.MMSpikeChest},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(3,
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
                            _smallKeyFactory(3,
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
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.MMMainLobby, DungeonItemID.MMMapChest},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest, DungeonItemID.MMSpikeChest
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
                                            DungeonItemID.MMMapChest
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon,
                                        _requirements[RequirementType.GuaranteedBossItemsOn])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                    _bigKeyFactory(
                        new List<DungeonItemID> {DungeonItemID.MMCompassChest, DungeonItemID.MMBigKeyChest},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest, DungeonItemID.MMSpikeChest
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(2,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.GuaranteedBossItemsOn])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                    _smallKeyFactory(1,
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
                            _smallKeyFactory(5,
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
                                    _smallKeyFactory(6,
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
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest, DungeonItemID.MMSpikeChest, DungeonItemID.MMSpikesPot
                        },
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1,
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
                                    _smallKeyFactory(5,
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
                                            _smallKeyFactory(6,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.MMMainLobby, DungeonItemID.MMMapChest, DungeonItemID.MMFishbonePot
                        },
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMSpikesPot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(5,
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
                                            _smallKeyFactory(6,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.MMConveyerCrystalDrop},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMSpikesPot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(5,
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
                                            _smallKeyFactory(6,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                    _bigKeyFactory(
                        new List<DungeonItemID> {DungeonItemID.MMCompassChest, DungeonItemID.MMBigKeyChest},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMSpikesPot
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(5,
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
                                            _smallKeyFactory(6,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                };
        }
    }
}