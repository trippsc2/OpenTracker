using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This is the class for creating key layouts.
    /// </summary>
    public static class KeyLayoutFactory
    {
        /// <summary>
        /// Returns the list of key layouts for the specified dungeon.
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon parent class.
        /// </param>
        /// <returns>
        /// The list of key layouts.
        /// </returns>
        public static List<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            if (dungeon == null)
            {
                throw new ArgumentNullException(nameof(dungeon));
            }

            switch (dungeon.ID)
            {
                case Locations.LocationID.HyruleCastle:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.HCSanctuary,
                                    DungeonItemID.HCMapChest,
                                    DungeonItemID.HCDarkCross,
                                    DungeonItemID.HCSecretRoomLeft,
                                    DungeonItemID.HCSecretRoomMiddle,
                                    DungeonItemID.HCSecretRoomRight
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon)
                        };
                    }
                case Locations.LocationID.AgahnimTower:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]),
                            new SmallKeyLayout(
                                2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ATRoom03,
                                    DungeonItemID.ATDarkMaze
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon)
                        };
                    }
                case Locations.LocationID.EasternPalace:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.BigKeyShuffleOn]),
                            new BigKeyLayout(new List<DungeonItemID>
                                {
                                    DungeonItemID.EPCannonballChest,
                                    DungeonItemID.EPMapChest,
                                    DungeonItemID.EPCompassChest,
                                    DungeonItemID.EPBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff])
                        };
                    }
                case Locations.LocationID.DesertPalace:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPCompassChest,
                                    DungeonItemID.DPBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPBigChest
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.DPCompassChest,
                                    DungeonItemID.DPBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle])
                        };
                    }
                case Locations.LocationID.TowerOfHera:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBasementCage,
                                    DungeonItemID.ToHMapChest,
                                    DungeonItemID.ToHBigKeyChest,
                                    DungeonItemID.ToHCompassChest,
                                    DungeonItemID.ToHBigChest,
                                    DungeonItemID.ToHBoss
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly],
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff]
                                })),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBasementCage,
                                    DungeonItemID.ToHMapChest,
                                    DungeonItemID.ToHBigKeyChest,
                                    DungeonItemID.ToHCompassChest,
                                    DungeonItemID.ToHBigChest
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly],
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBasementCage,
                                    DungeonItemID.ToHMapChest,
                                    DungeonItemID.ToHBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBasementCage,
                                    DungeonItemID.ToHMapChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.ToHBasementCage,
                                            DungeonItemID.ToHMapChest,
                                            DungeonItemID.ToHBigKeyChest,
                                            DungeonItemID.ToHCompassChest,
                                            DungeonItemID.ToHBigChest,
                                            DungeonItemID.ToHBoss
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon,
                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff]),
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.ToHBasementCage,
                                            DungeonItemID.ToHMapChest,
                                            DungeonItemID.ToHBigKeyChest,
                                            DungeonItemID.ToHCompassChest,
                                            DungeonItemID.ToHBigChest
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon,
                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn]),
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.ToHBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.ToHBasementCage,
                                            DungeonItemID.ToHMapChest
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle])
                        };
                    }
                case Locations.LocationID.PalaceOfDarkness:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                4,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDShooterRoom,
                                    DungeonItemID.PoDMapChest,
                                    DungeonItemID.PoDArenaLedge,
                                    DungeonItemID.PoDBigKeyChest,
                                    DungeonItemID.PoDStalfosBasement,
                                    DungeonItemID.PoDArenaBridge
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        6,
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
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                dungeon,
                                RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]),
                            new BigKeyLayout(
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
                                    DungeonItemID.PoDHarmlessHellway,
                                    DungeonItemID.PoDDarkMazeTop,
                                    DungeonItemID.PoDDarkMazeBottom
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]),
                            new BigKeyLayout(
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
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.PoDShooterRoom,
                                            DungeonItemID.PoDMapChest,
                                            DungeonItemID.PoDArenaLedge,
                                            DungeonItemID.PoDBigKeyChest,
                                            DungeonItemID.PoDStalfosBasement,
                                            DungeonItemID.PoDArenaBridge
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                6,
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
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDCompassChest,
                                    DungeonItemID.PoDDarkBasementLeft,
                                    DungeonItemID.PoDDarkBasementRight,
                                    DungeonItemID.PoDHarmlessHellway
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.PoDShooterRoom,
                                            DungeonItemID.PoDMapChest,
                                            DungeonItemID.PoDArenaLedge,
                                            DungeonItemID.PoDBigKeyChest,
                                            DungeonItemID.PoDStalfosBasement,
                                            DungeonItemID.PoDArenaBridge
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                6,
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
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.PoDDarkMazeTop,
                                    DungeonItemID.PoDDarkMazeBottom
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.PoDShooterRoom,
                                            DungeonItemID.PoDMapChest,
                                            DungeonItemID.PoDArenaLedge,
                                            DungeonItemID.PoDBigKeyChest,
                                            DungeonItemID.PoDStalfosBasement,
                                            DungeonItemID.PoDArenaBridge
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                6,
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
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle])
                        };
                    }
                case Locations.LocationID.SwampPalace:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPBigChest,
                                    DungeonItemID.SPCompassChest,
                                    DungeonItemID.SPWestChest,
                                    DungeonItemID.SPBigKeyChest,
                                    DungeonItemID.SPFloodedRoomLeft,
                                    DungeonItemID.SPFloodedRoomRight,
                                    DungeonItemID.SPWaterfallRoom,
                                    DungeonItemID.SPBoss
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff],
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
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
                                    new EndKeyLayout()
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn],
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPBigChest,
                                    DungeonItemID.SPCompassChest,
                                    DungeonItemID.SPWestChest,
                                    DungeonItemID.SPBigKeyChest,
                                    DungeonItemID.SPFloodedRoomLeft,
                                    DungeonItemID.SPFloodedRoomRight,
                                    DungeonItemID.SPWaterfallRoom,
                                    DungeonItemID.SPBoss
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff],
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                })),
                            new BigKeyLayout(
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
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn],
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                }))
                        };
                    }
                case Locations.LocationID.SkullWoods:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]
                                })),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWPinballRoom
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ItemPlacementBasic],
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom,
                                    DungeonItemID.SWBoss
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff],
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn],
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWBridgeRoom
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWBigChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ItemPlacementBasic],
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWBigChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWBigChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBoss
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWPinballRoom
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SWBigKeyChest,
                                                    DungeonItemID.SWMapChest,
                                                    DungeonItemID.SWPotPrison,
                                                    DungeonItemID.SWCompassChest,
                                                    DungeonItemID.SWPinballRoom,
                                                    DungeonItemID.SWBridgeRoom
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon)
                                        },
                                        dungeon,
                                        RequirementDictionary.Instance[RequirementType.ItemPlacementBasic]),
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.SWBigKeyChest,
                                            DungeonItemID.SWMapChest,
                                            DungeonItemID.SWPotPrison,
                                            DungeonItemID.SWCompassChest,
                                            DungeonItemID.SWPinballRoom,
                                            DungeonItemID.SWBridgeRoom
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon,
                                        RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced])
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff],
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                }))
                        };
                    }
                case Locations.LocationID.ThievesTown:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                1,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TTMapChest,
                                    DungeonItemID.TTAmbushChest,
                                    DungeonItemID.TTCompassChest,
                                    DungeonItemID.TTBigKeyChest,
                                    DungeonItemID.TTBlindsCell,
                                    DungeonItemID.TTBigChest
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TTMapChest,
                                    DungeonItemID.TTAmbushChest,
                                    DungeonItemID.TTCompassChest,
                                    DungeonItemID.TTBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]),
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TTMapChest,
                                            DungeonItemID.TTAmbushChest,
                                            DungeonItemID.TTCompassChest,
                                            DungeonItemID.TTBigKeyChest,
                                            DungeonItemID.TTBlindsCell,
                                            DungeonItemID.TTBigChest
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon,
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff])
                                },
                                RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff])
                        };
                    }
                case Locations.LocationID.IcePalace:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.IPCompassChest,
                                    DungeonItemID.IPSpikeRoom,
                                    DungeonItemID.IPMapChest,
                                    DungeonItemID.IPBigKeyChest,
                                    DungeonItemID.IPFreezorChest,
                                    DungeonItemID.IPBigChest,
                                    DungeonItemID.IPIcedTRoom
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.ItemPlacementBasic],
                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn]
                                    }),
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]
                                })),
                            new SmallKeyLayout(
                                2,
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
                                },
                                true,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff],
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
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
                                    new EndKeyLayout(
                                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]),
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.IPCompassChest,
                                            DungeonItemID.IPSpikeRoom,
                                            DungeonItemID.IPMapChest,
                                            DungeonItemID.IPBigKeyChest,
                                            DungeonItemID.IPFreezorChest,
                                            DungeonItemID.IPBigChest,
                                            DungeonItemID.IPIcedTRoom
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon,
                                        new AggregateRequirement(new List<IRequirement>
                                        {
                                            new AlternativeRequirement(new List<IRequirement>
                                            {
                                                RequirementDictionary.Instance[RequirementType.ItemPlacementBasic],
                                                RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn]
                                            }),
                                            RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff]
                                        })),
                                    new SmallKeyLayout(
                                        2,
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
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon,
                                        new AggregateRequirement(new List<IRequirement>
                                        {
                                            RequirementDictionary.Instance[RequirementType.ItemPlacementAdvanced],
                                            RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff],
                                            RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff]
                                        }))
                                },
                                RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff])
                        };
                    }
                case Locations.LocationID.MiseryMire:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMMapChest,
                                    DungeonItemID.MMBoss
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly],
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff]
                                })),
                            new SmallKeyLayout(
                                3,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMMapChest
                                },
                                true,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly],
                                    RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMCompassChest,
                                    DungeonItemID.MMBigKeyChest,
                                    DungeonItemID.MMMapChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMMapChest,
                                            DungeonItemID.MMBoss
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon,
                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff]),
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMMapChest
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon,
                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn])
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMMapChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMBigChest,
                                                    DungeonItemID.MMMapChest,
                                                    DungeonItemID.MMBoss
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon,
                                                RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff]),
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMBigChest,
                                                    DungeonItemID.MMMapChest
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon,
                                                RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn])
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.MMCompassChest,
                                    DungeonItemID.MMBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        1,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                2,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.MMBridgeChest,
                                                    DungeonItemID.MMSpikeChest,
                                                    DungeonItemID.MMMainLobby,
                                                    DungeonItemID.MMMapChest
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(3,
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
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon,
                                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOff]),
                                                    new SmallKeyLayout(3,
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
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon,
                                                        RequirementDictionary.Instance[RequirementType.GuaranteedBossItemsOn]),
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle])
                        };
                    }
                case Locations.LocationID.TurtleRock:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                2,
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps,
                                            DungeonItemID.TRBigKeyChest,
                                            DungeonItemID.TRBigChest,
                                            DungeonItemID.TRCrystarollerRoom
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
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
                                                    DungeonItemID.TRLaserBrdigeBottomRight
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[
                                        RequirementType.WorldStateStandardOpenEntranceShuffleNone],
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]
                                })),
                            new SmallKeyLayout(
                                4,
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
                                    DungeonItemID.TRLaserBrdigeBottomRight
                                },
                                false,
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                dungeon,
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn]
                                    }),
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps,
                                    DungeonItemID.TRBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[
                                        RequirementType.WorldStateStandardOpenEntranceShuffleNone],
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps,
                                    DungeonItemID.TRBigKeyChest,
                                    DungeonItemID.TRCrystarollerRoom,
                                    DungeonItemID.TRLaserBridgeTopLeft,
                                    DungeonItemID.TRLaserBridgeTopRight,
                                    DungeonItemID.TRLaserBridgeBottomLeft,
                                    DungeonItemID.TRLaserBrdigeBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn]
                                    }),
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRCompassChest,
                                    DungeonItemID.TRRollerRoomLeft,
                                    DungeonItemID.TRRollerRoomRight,
                                    DungeonItemID.TRChainChomps
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.TRCompassChest,
                                                    DungeonItemID.TRRollerRoomLeft,
                                                    DungeonItemID.TRRollerRoomRight,
                                                    DungeonItemID.TRChainChomps,
                                                    DungeonItemID.TRBigKeyChest,
                                                    DungeonItemID.TRBigChest,
                                                    DungeonItemID.TRCrystarollerRoom
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        4,
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
                                                            DungeonItemID.TRLaserBrdigeBottomRight
                                                        },
                                                        true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[
                                        RequirementType.WorldStateStandardOpenEntranceShuffleNone],
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
                                        new List<DungeonItemID>
                                        {
                                            DungeonItemID.TRCompassChest,
                                            DungeonItemID.TRRollerRoomLeft,
                                            DungeonItemID.TRRollerRoomRight,
                                            DungeonItemID.TRChainChomps
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
                                                new List<DungeonItemID>
                                                {
                                                    DungeonItemID.TRCompassChest,
                                                    DungeonItemID.TRRollerRoomLeft,
                                                    DungeonItemID.TRRollerRoomRight,
                                                    DungeonItemID.TRChainChomps,
                                                    DungeonItemID.TRBigChest,
                                                    DungeonItemID.TRCrystarollerRoom
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        4,
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
                                                            DungeonItemID.TRLaserBrdigeBottomRight
                                                        },
                                                        false,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    RequirementDictionary.Instance[
                                        RequirementType.WorldStateStandardOpenEntranceShuffleNone],
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                })),
                            new BigKeyLayout(
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
                                    DungeonItemID.TRLaserBrdigeBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
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
                                            DungeonItemID.TRLaserBrdigeBottomRight
                                        },
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn]
                                    }),
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                })),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.TRBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
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
                                            DungeonItemID.TRLaserBrdigeBottomRight
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                new AggregateRequirement(new List<IRequirement>
                                {
                                    new AlternativeRequirement(new List<IRequirement>
                                    {
                                        RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                        RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn]
                                    }),
                                    RequirementDictionary.Instance[RequirementType.NoKeyShuffle]
                                }))
                        };
                    }
                case Locations.LocationID.GanonsTower:
                    {
                        return new List<IKeyLayout>
                        {
                            new EndKeyLayout(
                                RequirementDictionary.Instance[RequirementType.AllKeyShuffle]),
                            new SmallKeyLayout(
                                3,
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
                                false,
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        4,
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
                                        },
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new EndKeyLayout()
                                        },
                                        dungeon)
                                },
                                dungeon,
                                RequirementDictionary.Instance[RequirementType.BigKeyShuffleOnly]),
                            new BigKeyLayout(
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
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest
                                },
                                new List<IKeyLayout>
                                {
                                    new EndKeyLayout()
                                },
                                RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOnly]),
                            new BigKeyLayout(
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
                                    new SmallKeyLayout(
                                        3,
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
                                        true,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
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
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
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
                                    new SmallKeyLayout(
                                        3,
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
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                4,
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
                                                },
                                                true,
                                                new List<IKeyLayout>
                                                {
                                                    new EndKeyLayout()
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTMapChest
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        3,
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
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(4,
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
                                                new EndKeyLayout()
                                            },
                                            dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTRandomizerRoomTopLeft,
                                    DungeonItemID.GTRandomizerRoomTopRight,
                                    DungeonItemID.GTRandomizerRoomBottomLeft,
                                    DungeonItemID.GTRandomizerRoomBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
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
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
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
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        4,
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
                                                        },
                                                        true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle]),
                            new BigKeyLayout(
                                new List<DungeonItemID>
                                {
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight
                                },
                                new List<IKeyLayout>
                                {
                                    new SmallKeyLayout(
                                        2,
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
                                        false,
                                        new List<IKeyLayout>
                                        {
                                            new SmallKeyLayout(
                                                3,
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
                                                },
                                                false,
                                                new List<IKeyLayout>
                                                {
                                                    new SmallKeyLayout(
                                                        4,
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
                                                        },
                                                        true,
                                                        new List<IKeyLayout>
                                                        {
                                                            new EndKeyLayout()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                RequirementDictionary.Instance[RequirementType.NoKeyShuffle])
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(dungeon));
        }
    }
}
