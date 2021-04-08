using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Desert Palace key layouts.
    /// </summary>
    public class DPKeyLayoutFactory : IDPKeyLayoutFactory
    {
        private readonly IRequirementDictionary _requirements;
        
        private readonly IBigKeyLayout.Factory _bigKeyFactory;
        private readonly IEndKeyLayout.Factory _endFactory;
        private readonly ISmallKeyLayout.Factory _smallKeyFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="requirements">
        ///     The requirement dictionary.
        /// </param>
        /// <param name="bigKeyFactory">
        ///     An Autofac factory for creating big key layouts.
        /// </param>
        /// <param name="endFactory">
        ///     An Autofac factory for ending key layouts.
        /// </param>
        /// <param name="smallKeyFactory">
        ///     An Autofac factory for creating small key layouts.
        /// </param>
        public DPKeyLayoutFactory(
            IRequirementDictionary requirements, IBigKeyLayout.Factory bigKeyFactory, IEndKeyLayout.Factory endFactory,
            ISmallKeyLayout.Factory smallKeyFactory)
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
                        DungeonItemID.DPMapChest, DungeonItemID.DPTorch, DungeonItemID.DPBigChest
                    }, false,
                    new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
                    _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly]),
                _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.DPMapChest, DungeonItemID.DPTorch},
                    new List<IKeyLayout>
                    {
                        _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                        _smallKeyFactory(1,
                            new List<DungeonItemID>
                            {
                                DungeonItemID.DPMapChest,
                                DungeonItemID.DPTorch,
                                DungeonItemID.DPBigChest
                            }, true,
                            new List<IKeyLayout>
                            {
                                _endFactory(_requirements[RequirementType.NoRequirement])
                            }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                    }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.DPCompassChest, DungeonItemID.DPBigKeyChest},
                    new List<IKeyLayout>
                    {
                        _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                        _smallKeyFactory(1,
                            new List<DungeonItemID> {DungeonItemID.DPMapChest, DungeonItemID.DPTorch},
                            false,
                            new List<IKeyLayout>
                            {
                                _endFactory(_requirements[RequirementType.NoRequirement])
                            }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                    }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                _smallKeyFactory(2,
                    new List<DungeonItemID>
                    {
                        DungeonItemID.DPMapChest,
                        DungeonItemID.DPTorch,
                        DungeonItemID.DPBigChest,
                        DungeonItemID.DPTiles1Pot
                    }, false,
                    new List<IKeyLayout>
                    {
                        _smallKeyFactory(3,
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
                                _smallKeyFactory(4,
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
                                    }, dungeon, _requirements[RequirementType.NoRequirement])
                            }, dungeon, _requirements[RequirementType.NoRequirement])
                    }, dungeon, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
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
                    }, new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])},
                    _requirements[RequirementType.KeyDropShuffleOnSmallKeyShuffleOnly]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.DPMapChest, DungeonItemID.DPTorch, DungeonItemID.DPTiles1Pot
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                        _smallKeyFactory(2,
                            new List<DungeonItemID>
                            {
                                DungeonItemID.DPMapChest,
                                DungeonItemID.DPTorch,
                                DungeonItemID.DPBigChest,
                                DungeonItemID.DPTiles1Pot
                            }, true,
                            new List<IKeyLayout>
                            {
                                _smallKeyFactory(3,
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
                                        _smallKeyFactory(4,
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
                                                _endFactory(
                                                    _requirements
                                                        [RequirementType.NoRequirement])
                                            }, dungeon,
                                            _requirements[RequirementType.NoRequirement])
                                    }, dungeon, _requirements[RequirementType.NoRequirement])
                            }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                    }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.DPBeamosHallPot},
                    new List<IKeyLayout>
                    {
                        _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                        _smallKeyFactory(2,
                            new List<DungeonItemID>
                            {
                                DungeonItemID.DPMapChest,
                                DungeonItemID.DPTorch,
                                DungeonItemID.DPTiles1Pot
                            }, false,
                            new List<IKeyLayout>
                            {
                                _smallKeyFactory(3,
                                    new List<DungeonItemID>
                                    {
                                        DungeonItemID.DPMapChest,
                                        DungeonItemID.DPTorch,
                                        DungeonItemID.DPBigChest,
                                        DungeonItemID.DPTiles1Pot
                                    }, false,
                                    new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(4,
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
                                                _endFactory(
                                                    _requirements
                                                        [RequirementType.NoRequirement])
                                            }, dungeon,
                                            _requirements[RequirementType.NoRequirement])
                                    }, dungeon, _requirements[RequirementType.NoRequirement])
                            }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                    }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
                _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.DPTiles2Pot},
                    new List<IKeyLayout>
                    {
                        _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                        _smallKeyFactory(2,
                            new List<DungeonItemID>
                            {
                                DungeonItemID.DPMapChest,
                                DungeonItemID.DPTorch,
                                DungeonItemID.DPTiles1Pot
                            }, false,
                            new List<IKeyLayout>
                            {
                                _smallKeyFactory(4,
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
                                    }, dungeon, _requirements[RequirementType.NoRequirement])
                            }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                    }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                };
        }
    }
}