using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.KeyLayouts.Factories
{
    /// <summary>
    /// This class contains the creation logic for Palace of Darkness key layouts.
    /// </summary>
    public class PoDKeyLayoutFactory : IPoDKeyLayoutFactory
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
        public PoDKeyLayoutFactory(
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
                    _smallKeyFactory(4,
                        new List<DungeonItemID>
                        {
                            DungeonItemID.PoDShooterRoom,
                            DungeonItemID.PoDMapChest,
                            DungeonItemID.PoDArenaLedge,
                            DungeonItemID.PoDBigKeyChest,
                            DungeonItemID.PoDStalfosBasement,
                            DungeonItemID.PoDArenaBridge
                        }, false,
                        new List<IKeyLayout>
                        {
                            _smallKeyFactory(6,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDShooterRoom,
                                    DungeonItemID.PoDMapChest,
                                    DungeonItemID.PoDArenaLedge,
                                    DungeonItemID.PoDBigKeyChest,
                                    DungeonItemID.PoDStalfosBasement,
                                    DungeonItemID.PoDArenaBridge,
                                    DungeonItemID.PoDCompassChest,
                                    DungeonItemID.PoDDarkBasementLeft,
                                    DungeonItemID.PoDDarkBasementRight,
                                    DungeonItemID.PoDHarmlessHellway
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _endFactory(_requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.NoRequirement])
                        }, dungeon, _requirements[RequirementType.BigKeyShuffleOnly]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.PoDShooterRoom,
                            DungeonItemID.PoDMapChest,
                            DungeonItemID.PoDArenaLedge,
                            DungeonItemID.PoDBigKeyChest,
                            DungeonItemID.PoDStalfosBasement,
                            DungeonItemID.PoDArenaBridge
                        },
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(4,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDShooterRoom,
                                    DungeonItemID.PoDMapChest,
                                    DungeonItemID.PoDArenaLedge,
                                    DungeonItemID.PoDBigKeyChest,
                                    DungeonItemID.PoDStalfosBasement,
                                    DungeonItemID.PoDArenaBridge
                                }, true,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(6,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.PoDShooterRoom,
                                            DungeonItemID.PoDMapChest,
                                            DungeonItemID.PoDArenaLedge,
                                            DungeonItemID.PoDBigKeyChest,
                                            DungeonItemID.PoDStalfosBasement,
                                            DungeonItemID.PoDArenaBridge,
                                            DungeonItemID.PoDCompassChest,
                                            DungeonItemID.PoDDarkBasementLeft,
                                            DungeonItemID.PoDDarkBasementRight,
                                            DungeonItemID.PoDHarmlessHellway
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.BigKeyShuffleOff]),
                    _bigKeyFactory(
                        new List<DungeonItemID>
                        {
                            DungeonItemID.PoDCompassChest,
                            DungeonItemID.PoDDarkBasementLeft,
                            DungeonItemID.PoDDarkBasementRight,
                            DungeonItemID.PoDHarmlessHellway
                        },
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(4,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDShooterRoom,
                                    DungeonItemID.PoDMapChest,
                                    DungeonItemID.PoDArenaLedge,
                                    DungeonItemID.PoDBigKeyChest,
                                    DungeonItemID.PoDStalfosBasement,
                                    DungeonItemID.PoDArenaBridge
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(6,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.PoDShooterRoom,
                                            DungeonItemID.PoDMapChest,
                                            DungeonItemID.PoDArenaLedge,
                                            DungeonItemID.PoDBigKeyChest,
                                            DungeonItemID.PoDStalfosBasement,
                                            DungeonItemID.PoDArenaBridge,
                                            DungeonItemID.PoDCompassChest,
                                            DungeonItemID.PoDDarkBasementLeft,
                                            DungeonItemID.PoDDarkBasementRight,
                                            DungeonItemID.PoDHarmlessHellway
                                        }, true,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.BigKeyShuffleOff]),
                    _bigKeyFactory(
                        new List<DungeonItemID> {DungeonItemID.PoDDarkMazeTop, DungeonItemID.PoDDarkMazeBottom},
                        new List<IKeyLayout>
                        {
                            _endFactory(_requirements[RequirementType.SmallKeyShuffleOn]),
                            _smallKeyFactory(4,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDShooterRoom,
                                    DungeonItemID.PoDMapChest,
                                    DungeonItemID.PoDArenaLedge,
                                    DungeonItemID.PoDBigKeyChest,
                                    DungeonItemID.PoDStalfosBasement,
                                    DungeonItemID.PoDArenaBridge
                                }, false,
                                new List<IKeyLayout>
                                {
                                    _smallKeyFactory(6,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.PoDShooterRoom,
                                            DungeonItemID.PoDMapChest,
                                            DungeonItemID.PoDArenaLedge,
                                            DungeonItemID.PoDBigKeyChest,
                                            DungeonItemID.PoDStalfosBasement,
                                            DungeonItemID.PoDArenaBridge,
                                            DungeonItemID.PoDCompassChest,
                                            DungeonItemID.PoDDarkBasementLeft,
                                            DungeonItemID.PoDDarkBasementRight,
                                            DungeonItemID.PoDHarmlessHellway
                                        }, false,
                                        new List<IKeyLayout>
                                        {
                                            _endFactory(_requirements[RequirementType.NoRequirement])
                                        }, dungeon, _requirements[RequirementType.NoRequirement])
                                }, dungeon, _requirements[RequirementType.SmallKeyShuffleOff])
                        }, _requirements[RequirementType.BigKeyShuffleOff])
                };
        }
    }
}