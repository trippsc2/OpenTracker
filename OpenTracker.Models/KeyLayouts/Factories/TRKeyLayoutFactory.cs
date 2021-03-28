using System.Collections.Generic;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Turtle Rock key layouts.
    /// </summary>
    public class TRKeyLayoutFactory : ITRKeyLayoutFactory
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
        public TRKeyLayoutFactory(
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
                            DungeonItemID.TRCompassChest,
                            DungeonItemID.TRRollerRoomLeft,
                            DungeonItemID.TRRollerRoomRight,
                            DungeonItemID.TRChainChomps
                        }, false,
                        new List<IKeyLayout>
                        {
                            _smallKeyFactory(3,
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
                                    _smallKeyFactory(4,
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
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon,
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOffWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOnly]),
                    _smallKeyFactory(4,
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
                        new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOffWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOnly]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps
                                }, true,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(3,
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
                                            _smallKeyFactory(4,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOffWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.TRBigKeyChest},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(3,
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
                                            _smallKeyFactory(4,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOffWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(4,
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
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOffWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.TRBigKeyChest},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(4,
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
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOffWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff]),
                    _smallKeyFactory(3,
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
                            _smallKeyFactory(5,
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
                                    _smallKeyFactory(6,
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
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon,
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOnWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOnly]),
                    _smallKeyFactory(6,
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
                        new List<IKeyLayout> {_endFactory(_requirements[RequirementType.NoRequirement])}, dungeon,
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOnWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOnly]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(3,
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
                                    _smallKeyFactory(5,
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
                                            _smallKeyFactory(6,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOnWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.TRPokey2Drop},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(3,
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
                                    _smallKeyFactory(5,
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
                                            _smallKeyFactory(6,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOnWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.TRBigKeyChest},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(4,
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
                                    _smallKeyFactory(5,
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
                                            _smallKeyFactory(6,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOnWorldStateStandardOpenEntranceShuffleNoneBigKeyShuffleOff]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(6,
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
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOnWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.TRBigKeyChest},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(6,
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
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        },
                        _requirements[
                            RequirementType
                                .KeyDropShuffleOnWorldStateInvertedOrEntranceShuffleDungeonAllInsanityBigKeyShuffleOff]),
                };
        }
    }
}