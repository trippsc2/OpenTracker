using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Ganon's Tower key layouts.
    /// </summary>
    public class GTKeyLayoutFactory : IGTKeyLayoutFactory
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
        public GTKeyLayoutFactory(
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
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOnly]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(3,
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
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(3,
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
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                    _bigKeyFactory(new List<DungeonItemID> {DungeonItemID.GTMapChest},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(3,
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
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(2,
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
                                    _smallKeyFactory(3,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(2,
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
                                    _smallKeyFactory(3,
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
                                                    _endFactory(
                                                        _requirements
                                                            [RequirementType.NoRequirement])
                                                }, dungeon,
                                                _requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOffBigKeyShuffleOff]),
                    _smallKeyFactory(7,
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
                            _smallKeyFactory(8,
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
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOnly]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(7,
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
                                    _smallKeyFactory(8,
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
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(7,
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
                                    _smallKeyFactory(8,
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
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff]),
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
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(7,
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
                                    _smallKeyFactory(8,
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
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.KeyDropShuffleOnBigKeyShuffleOff])
                };
        }
    }
}