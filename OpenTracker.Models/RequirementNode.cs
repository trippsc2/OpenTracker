using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class RequirementNode : INotifyPropertyChanged
    {
        private readonly ItemType? _directAccessItem;

        protected Game Game { get; }
        protected List<RequirementNodeID> NodeSubscriptions { get; }
        protected List<RequirementType> RequirementSubscriptions { get; }
        public RequirementNodeID ID { get; }
        public List<RequirementNodeConnection> Connections { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        public RequirementNode(Game game, RequirementNodeID iD)
        {
            Game = game ?? throw new ArgumentNullException(nameof(game));
            ID = iD;
            Connections = new List<RequirementNodeConnection>();

            switch (ID)
            {
                //  Access to general Light World as a bunny
                case RequirementNodeID.LightWorld:
                    {
                        _directAccessItem = ItemType.LightWorldAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEntry,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainExit,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWKakarikoPortal,
                            RequirementType.LWLift2, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWKakarikoPortal,
                            RequirementType.LWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GrassHouse,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BombHut,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.RaceGameLedge,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SouthOfGroveLedge,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertLedge,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.CheckerboardLedge,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BombosTabletLedge,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWMirePortal,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWGraveyard,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HyruleCastleTop,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWSouthPortal,
                            RequirementType.LWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWWitchArea,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWEastPortal,
                            RequirementType.LWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWLakeHylia,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.Aga, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the hole entrance to Lumberjack cave
                case RequirementNodeID.LumberjackCave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWDashAga, new Mode()));
                    }
                    break;
                //  Access to the forest hideout hole entrance
                case RequirementNodeID.ForestHideout:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                    }
                    break;
                //  Access beyond the rock at the Death Mountain Entry cave
                case RequirementNodeID.DeathMountainEntry:
                    {
                        _directAccessItem = ItemType.DeathMountainEntryAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWLift1, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BumperCave,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the ledge on top of the Death Mountain Entry/Exit cave
                case RequirementNodeID.DeathMountainExit:
                    {
                        _directAccessItem = ItemType.DeathMountainExitAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.DarkRoom, new Mode()
                            {
                                WorldState = WorldState.StandardOpen,
                                EntranceShuffle = false
                            }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEntry,
                            RequirementType.BumperCave, new Mode()
                            {
                                WorldState = WorldState.Inverted,
                                EntranceShuffle = false
                            }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BumperCaveTop,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the area north of Kakariko with the Dark World portal
                case RequirementNodeID.LWKakarikoPortal:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWLift2, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWLift1, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the house with bushes blocking access
                case RequirementNodeID.GrassHouse:
                    {
                        _directAccessItem = ItemType.GrassHouseAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the bombable hut in the southwest of Kakariko Village
                case RequirementNodeID.BombHut:
                    {
                        _directAccessItem = ItemType.BombHutAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the ledge above the Magic Bat hole/cave
                case RequirementNodeID.MagicBatLedge:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HammerPegsArea,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the race game ledge
                case RequirementNodeID.RaceGameLedge:
                    {
                        _directAccessItem = ItemType.RaceGameLedgeAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode() { EntranceShuffle = false }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the ledge south of the grove
                case RequirementNodeID.SouthOfGroveLedge:
                    {
                        _directAccessItem = ItemType.SouthOfGroveLedgeAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the ledge above Desert Palace
                case RequirementNodeID.DesertLedge:
                    {
                        _directAccessItem = ItemType.DesertLedgeAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DPFrontEntry,
                            RequirementType.None, new Mode() { EntranceShuffle = false }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertPalaceBackEntrance,
                            RequirementType.LWLift1, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the back entrance of Desert Palace (beyond the rocks)
                case RequirementNodeID.DesertPalaceBackEntrance:
                    {
                        _directAccessItem = ItemType.DesertPalaceBackEntranceAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertLedge,
                            RequirementType.LWLift1, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the ledge where Checkboard cave is located
                case RequirementNodeID.CheckerboardLedge:
                    {
                        _directAccessItem = ItemType.CheckerboardLedgeAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the Checkerboard cave entrance (below the rock)
                case RequirementNodeID.CheckerboardCave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.CheckerboardLedge,
                            RequirementType.LWLift1, new Mode()));
                    }
                    break;
                //  Access to the Desert Palace front entrance
                case RequirementNodeID.DesertPalaceFrontEntrance:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Book, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the Bombos tablet ledge
                case RequirementNodeID.BombosTabletLedge:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the Rupee cave outside of the desert (below the rock)
                case RequirementNodeID.RupeeCave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWLift1, new Mode()));
                    }
                    break;
                //  Access to the ledge with the portal to the Mire area
                case RequirementNodeID.LWMirePortal:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.LWFlute, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWMirePortal,
                            RequirementType.DWLift2, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the northern bonk rocks entrance west of Sanctuary
                case RequirementNodeID.NorthBonkRocks:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWDash, new Mode()));
                    }
                    break;
                //  Access to the Light World graveyard (beyond the bushes and rocks)
                case RequirementNodeID.LWGraveyard:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWGraveyardLedge,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWKingsTomb,
                            RequirementType.LWLift2, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWGraveyard,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the hole under the grave that leads to Escape
                case RequirementNodeID.EscapeGrave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWGraveyard,
                            RequirementType.LWLift1, new Mode()));
                    }
                    break;
                //  Access to the area where the King's Tomb grave is located (beyond the dark rocks)
                case RequirementNodeID.LWKingsTomb:
                    {
                        _directAccessItem = ItemType.LWKingsTombAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWGraveyard,
                            RequirementType.LWLift2, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWGraveyard,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the King's Tomb entrance (under the grave)
                case RequirementNodeID.KingsTombGrave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWKingsTomb,
                            RequirementType.LWDash, new Mode()));
                    }
                    break;
                //  Access to the ledge above the graveyard in light world
                case RequirementNodeID.LWGraveyardLedge:
                    {
                        _directAccessItem = ItemType.LWGraveyardLedgeAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWGraveyard,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWGraveyardLedge,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the top of Hyrule Castle
                case RequirementNodeID.HyruleCastleTop:
                    {
                        _directAccessItem = ItemType.HyruleCastleTopAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode() { EntranceShuffle = false }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the Agahnim Tower entrance
                case RequirementNodeID.AgahnimTowerEntrance:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HyruleCastleTop,
                            RequirementType.ATBarrier, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HyruleCastleTop,
                            RequirementType.GTCrystals, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the hole entrance to Ganon
                case RequirementNodeID.GanonHole:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HyruleCastleTop,
                            RequirementType.Aga2, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.Aga2, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the walk-in entrance to Ganon (only accessible in Inverted)
                case RequirementNodeID.GanonHoleBack:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the walk-in entrance to the Hyrule Castle secret passage (beyond the bushes)
                case RequirementNodeID.CastleSecretFront:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the hole entrance to the Hyrule Castle secret passage (outside the castle walls, under the bush)
                case RequirementNodeID.CastleSecretBack:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.Mirror, new Mode()));
                    }
                    break;
                //  Access to the central bonk rocks entrance north of Link's House
                case RequirementNodeID.CentralBonkRocks:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWDash, new Mode()));
                    }
                    break;
                //  Access to the area north of the Dam with the Dark World portal
                case RequirementNodeID.LWSouthPortal:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWSouthPortal,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWSouthPortal,
                            RequirementType.DWLift1, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the cave where Hype cave is in light world
                case RequirementNodeID.HypeFairyCave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the Mini-Moldorm cave entrance
                case RequirementNodeID.MiniMoldormCave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the Zora area land (beyond the rock east of the Witch's Hut)
                case RequirementNodeID.Zora:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.WaterfallFairy,
                            RequirementType.LWSwim, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWLakeHylia,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWWitchArea,
                            RequirementType.LWLift1, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Catfish,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the Waterfall Fairy entrance
                case RequirementNodeID.WaterfallFairy:
                    {
                        _directAccessItem = ItemType.WaterfallFairyAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Zora,
                            RequirementType.LWSwimOrWaterWalk, new Mode()));
                    }
                    break;
                //  Access to the Witch's Hut area (beyond the rocks and bushes north of the bridge)
                case RequirementNodeID.LWWitchArea:
                    {
                        _directAccessItem = ItemType.LWWitchAreaAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Zora,
                            RequirementType.LWLift1, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWWitchArea,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the area south of Eastern Palace with the Dark World portal (inside the hammer pegs)
                case RequirementNodeID.LWEastPortal:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWEastPortal,
                            RequirementType.LWLift1, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the water of Lake Hylia
                case RequirementNodeID.LWLakeHylia:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWSwim, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWLakeHylia,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the island on Lake Hylia with the free-standing item
                case RequirementNodeID.LakeHyliaIsland:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWLakeHylia,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWLakeHylia,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the island on Lake Hylia with the capacity upgrade fairy
                case RequirementNodeID.LakeHyliaFairyIsland:
                    {
                        _directAccessItem = ItemType.LakeHyliaFairyIslandAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWLakeHylia,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IcePalaceEntrance,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IcePalaceEntrance,
                            RequirementType.DWLift2, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the Ice Rod cave entrance
                case RequirementNodeID.IceRodCave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the Ice Fariy cave entrance (under the rock)
                case RequirementNodeID.IceFairyCave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWLift1, new Mode()));
                    }
                    break;
                //  Access to the West side of Death Mountain south of Spectacle Rock
                case RequirementNodeID.DeathMountainWestBottom:
                    {
                        _directAccessItem = ItemType.DeathMountainWestBottomAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWFlute, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEntry,
                            RequirementType.DarkRoom, new Mode()
                            {
                                WorldState = WorldState.StandardOpen,
                                EntranceShuffle = false
                            }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SpectacleRockTop,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTop,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.LWHookshot, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the top of Spectacle Rock
                case RequirementNodeID.SpectacleRockTop:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTop,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the West side of Death Mountain north of Spectacle Rock
                case RequirementNodeID.DeathMountainWestTop:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SpectacleRockTop,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.LWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the East side of Death Mountain bottom
                case RequirementNodeID.DeathMountainEastBottom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.LWHookshot, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottomConnector,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTopConnector,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SpiralCave,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MimicCave,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottom,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the East side of Death Mountain bottom connector entrance (beyond the dark rocks)
                case RequirementNodeID.DeathMountainEastBottomConnector:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTopConnector,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.LWLift2, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottomConnector,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the ledge containing the Death Mountain East Top Connector entrance
                case RequirementNodeID.DeathMountainEastTopConnector:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockSafetyDoor,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the ledge containing the Spiral cave entrance
                case RequirementNodeID.SpiralCave:
                    {
                        _directAccessItem = ItemType.SpiralCaveAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockTunnel,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the ledge containing the Mimic cave entrance
                case RequirementNodeID.MimicCave:
                    {
                        _directAccessItem = ItemType.MimicCaveAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockTunnel,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the East side of Death Mountain top
                case RequirementNodeID.DeathMountainEastTop:
                    {
                        _directAccessItem = ItemType.DeathMountainEastTopAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTop,
                            RequirementType.LWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.None, new Mode() { EntranceShuffle = false }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWFloatingIsland,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWTurtleRockTop,
                            RequirementType.None, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWTurtleRockTop,
                            RequirementType.LWHammer, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the Floating Island on Death Mountain in the Light World
                case RequirementNodeID.LWFloatingIsland:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWFloatingIsland,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the top of Turtle Rock in Light World (beyond the dark rocks)
                case RequirementNodeID.LWTurtleRockTop:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.LWLift2, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWTurtleRockTop,
                            RequirementType.DWLift2, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWTurtleRockTop,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the area north of Village of Outcast where the Kakariko portal drops you
                case RequirementNodeID.DWKakarikoPortal:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWKakarikoPortal,
                            RequirementType.LWLift1, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWKakarikoPortal,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the area of Dark World north of Digging game and West of the river
                case RequirementNodeID.DarkWorldWest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode()
                            {
                                WorldState = WorldState.Inverted,
                                EntranceShuffle = false
                            }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BumperCaveTop,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BumperCave,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWKakarikoPortal,
                            RequirementType.DWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.LWLift2, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWWitchArea,
                            RequirementType.DWHookshot, new Mode()));
                    }
                    break;
                //  Access to the back entrance of Skull Woods
                case RequirementNodeID.SkullWoodsBackEntrance:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWFireRod, new Mode()));
                    }
                    break;
                //  Access to the Bumper Cave entrance (beyond the rocks)
                case RequirementNodeID.BumperCave:
                    {
                        _directAccessItem = ItemType.BumperCaveAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEntry,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWLift1, new Mode()));
                    }
                    break;
                //  Access to the ledge on top of Bumper Cave
                case RequirementNodeID.BumperCaveTop:
                    {
                        _directAccessItem = ItemType.BumperCaveTopAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.DarkRoom, new Mode()
                            {
                                WorldState = WorldState.Inverted,
                                EntranceShuffle = false
                            }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEntry,
                            RequirementType.BumperCave, new Mode()
                            {
                                WorldState = WorldState.StandardOpen,
                                EntranceShuffle = false
                            }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainExit,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the Thieves Town entrance
                case RequirementNodeID.ThievesTownEntrance:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWNotBunny, new Mode()));
                    }
                    break;
                //  Acccess to the Bombable Shack in Village of Outcasts
                case RequirementNodeID.BombableShack:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the house in Village of Outcasts with hammer pegs in front
                case RequirementNodeID.HammerHouse:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWHammer, new Mode()));
                    }
                    break;
                //  Access to the area east of Village of Outcasts (beyond the dark rocks)
                case RequirementNodeID.HammerPegsArea:
                    {
                        _directAccessItem = ItemType.HammerPegsAreaAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWLift2, new Mode()));
                    }
                    break;
                //  Access to the hammer pegs entrance
                case RequirementNodeID.HammerPegs:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HammerPegsArea,
                            RequirementType.DWHammer, new Mode()));
                    }
                    break;
                //  Access to the frog blacksmith's prison in Dark World
                case RequirementNodeID.BlacksmithPrison:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWLift2, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the area of Dark World south of Village of Outcasts and the Hammer peg blocked path
                //    to Palace of Darkness and the Pyramid
                case RequirementNodeID.DarkWorldSouth:
                    {
                        _directAccessItem = ItemType.DarkWorldSouthAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode()
                            {
                                WorldState = WorldState.Inverted,
                                EntranceShuffle = false
                            }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWSouthPortal,
                            RequirementType.DWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.DWHammer, new Mode()));
                    }
                    break;
                //  Access to the central bonk rocks north of the bomb shop
                case RequirementNodeID.DWCentralBonkRocks:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.DWDash, new Mode()));
                    }
                    break;
                //  Access to buying the big bomb in the bomb shop
                case RequirementNodeID.BuyBigBomb:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.RedCrystals, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.RedCrystals, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to take the big bomb to Light World
                case RequirementNodeID.BigBombToLightWorld:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BuyBigBomb,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BuyBigBomb,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to take the big bomb to the fat fairy wall
                case RequirementNodeID.BigBombToWall:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BuyBigBomb,
                            RequirementType.DWHammer, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BuyBigBomb,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.StandardOpen },
                            AccessibilityLevel.SequenceBreak));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BigBombToLightWorld,
                            RequirementType.Aga, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BigBombToLightWorld,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the area north of Swamp Palace where the Dark World portal places you
                case RequirementNodeID.DWSouthPortal:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWSouthPortal,
                            RequirementType.LWLift1, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWSouthPortal,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.DWHammer, new Mode()));
                    }
                    break;
                //  Access to the Hype Cave entrance
                case RequirementNodeID.HypeCave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.DWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the swamp area containing Misery Mire
                case RequirementNodeID.MireArea:
                    {
                        _directAccessItem = ItemType.MireAreaAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWMirePortal,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to the ledge whether the Dark World portal takes you in Mire area
                case RequirementNodeID.DWMirePortal:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.DWFlute, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWMirePortal,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWMirePortal,
                            RequirementType.LWLift2, new Mode() { WorldState = WorldState.StandardOpen }));
                    }
                    break;
                //  Access to the Misery Mire entrance
                case RequirementNodeID.MiseryMireEntrance:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.MMMedallion, new Mode()));
                    }
                    break;
                //  Access to the area where the Graveyard is in Dark World (beyond the bushes and rocks)
                case RequirementNodeID.DWGraveyard:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWGraveyard,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the ledge north of the Graveyard in Dark World
                case RequirementNodeID.DWGraveyardLedge:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWGraveyardLedge,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWGraveyard,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to the area where the Witch's Hut is in Dark World (beyond the hammer peg and rocks)
                case RequirementNodeID.DWWitchArea:
                    {
                        _directAccessItem = ItemType.DWWitchAreaAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWWitchArea,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWSwim, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.DWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.DWLift1, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.DWSwim, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Catfish,
                            RequirementType.DWLift1, new Mode()));
                    }
                    break;
                //  Access to the area where Catfish is located
                case RequirementNodeID.Catfish:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Zora,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWWitchArea,
                            RequirementType.DWLift1, new Mode()));
                    }
                    break;
                //  Access to the area of Dark World containing Palace of Darkness and the Pyramid
                case RequirementNodeID.DarkWorldEast:
                    {
                        _directAccessItem = ItemType.DarkWorldEastAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Aga, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWWitchArea,
                            RequirementType.DWLift1, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWWitchArea,
                            RequirementType.DWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.DWSwim, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.DWHammer, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWLakeHylia,
                            RequirementType.DWSwim, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWEastPortal,
                            RequirementType.DWHammer, new Mode()));
                    }
                    break;
                //  Access to the super bomb entrance in the pyramid
                case RequirementNodeID.FatFairy:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.BigBombToWall, new Mode()));
                    }
                    break;
                //  Access to the Palace of Darkness entrance (Kiki won't follow bunny)
                case RequirementNodeID.PalaceOfDarknessEntrance:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.DWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the area where the Dark World portal south of Eastern Palace places you
                //    (inside the hammer pegs)
                case RequirementNodeID.DWEastPortal:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWEastPortal,
                            RequirementType.LWLift1, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.DWHammer, new Mode()));
                    }
                    break;
                //  Access to the water of Lake Hylia in the Dark World
                case RequirementNodeID.DWLakeHylia:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWLakeHylia,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.DWSwim, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.DWSwim, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthEast,
                            RequirementType.DWSwim, new Mode()));
                    }
                    break;
                //  Access to the Ice Palace entrance/island
                case RequirementNodeID.IcePalaceEntrance:
                    {
                        _directAccessItem = ItemType.IcePalaceAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LakeHyliaFairyIsland,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LakeHyliaFairyIsland,
                            RequirementType.LWLift2, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWLakeHylia,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the "Dark World Shopping Mall" southeast of Dark World Lake Hylia
                case RequirementNodeID.DarkWorldSouthEast:
                    {
                        _directAccessItem = ItemType.DarkWorldSouthEastAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWLakeHylia,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to the Ice Rod cave in dark world
                case RequirementNodeID.DWIceRodCave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthEast,
                            RequirementType.DWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the Ice Rod cave rock in dark world
                case RequirementNodeID.DWIceRodRock:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthEast,
                            RequirementType.DWLift1, new Mode()));
                    }
                    break;
                //  Access to the West side of Dark Death Mountain south of Spectacle Rock
                case RequirementNodeID.DarkDeathMountainWestBottom:
                    {
                        _directAccessItem = ItemType.DarkDeathMountainWestBottomAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.BumperCave,
                            RequirementType.DarkRoom, new Mode()
                            {
                                WorldState = WorldState.Inverted,
                                EntranceShuffle = false
                            }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Dark Death Mountain top area
                case RequirementNodeID.DarkDeathMountainTop:
                    {
                        _directAccessItem = ItemType.DarkDeathMountainTopAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTop,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWFloatingIsland,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWTurtleRockTop,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to the Ganon's Tower entrance
                case RequirementNodeID.GanonsTowerEntrance:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop,
                            RequirementType.GTCrystals, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the Dark World Floating Island on Death Mountain
                case RequirementNodeID.DWFloatingIsland:
                    {
                        _directAccessItem = ItemType.DWFloatingIslandAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWFloatingIsland,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HookshotCave,
                            RequirementType.None, new Mode() { EntranceShuffle = false }));
                    }
                    break;
                //  Access to the Hookshot Cave entrance
                case RequirementNodeID.HookshotCave:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop,
                            RequirementType.DWLift1, new Mode()));
                    }
                    break;
                //  Access to the top of Turtle Rock in Dark World
                case RequirementNodeID.DWTurtleRockTop:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWTurtleRockTop,
                            RequirementType.LWHammer, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LWTurtleRockTop,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the front entrance of Turtle Rock
                case RequirementNodeID.TurtleRockFrontEntrance:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DWTurtleRockTop,
                            RequirementType.TRMedallion, new Mode()));
                    }
                    break;
                //  Access to the East side of Dark Death Mountain bottom
                case RequirementNodeID.DarkDeathMountainEastBottom:
                    {
                        _directAccessItem = ItemType.DarkDeathMountainEastBottomAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.LWLift2, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottomConnector,
                            RequirementType.DWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the Death Mountain East Bottom Connector area in Dark World (beyond the bushes)
                case RequirementNodeID.DarkDeathMountainEastBottomConnector:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottomConnector,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottom,
                            RequirementType.DWNotBunny, new Mode()));
                    }
                    break;
                //  Access to the Turtle Rock tunnel between middle entrances
                case RequirementNodeID.TurtleRockTunnel:
                    {
                        _directAccessItem = ItemType.TurtleRockTunnelAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SpiralCave,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MimicCave,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the ledge from the back exit/entrance of Turtle Rock
                case RequirementNodeID.TurtleRockSafetyDoor:
                    {
                        _directAccessItem = ItemType.TurtleRockSafetyDoorAccess;

                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTopConnector,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the Sanctuary (entry node)
                case RequirementNodeID.HCSanctuaryEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.Mirror, new Mode() { WorldState = WorldState.Inverted },
                            AccessibilityLevel.SequenceBreak));
                    }
                    break;
                //  Access to the Hyrule Castle front area (entry node)
                case RequirementNodeID.HCFrontEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
                    }
                    break;
                //  Access to the back of the sewers (entry node)
                case RequirementNodeID.HCBackEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.EscapeGrave,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Agahnim Tower entrance (entry node)
                case RequirementNodeID.ATEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.AgahnimTowerEntrance,
                            RequirementType.None, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GanonsTowerEntrance,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to Eastern Palace entrance (entry node)
                case RequirementNodeID.EPEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
                    }
                    break;
                //  Access to Desert Palace front entrance (entry node)
                case RequirementNodeID.DPFrontEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertPalaceFrontEntrance,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertPalaceFrontEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
                    }
                    break;
                //  Access to Desert Palace left entrance (entry node)
                case RequirementNodeID.DPLeftEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertLedge,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertLedge,
                            RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
                    }
                    break;
                //  Access to Desert Palace back entrance (entry node)
                case RequirementNodeID.DPBackEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertPalaceBackEntrance,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DesertPalaceBackEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
                    }
                    break;
                //  Access to Tower of Hera entrance (entry node)
                case RequirementNodeID.ToHEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTop,
                            RequirementType.LWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTop,
                            RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
                    }
                    break;
                //  Access to Palace of Darkness entrance (entry node)
                case RequirementNodeID.PoDEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PalaceOfDarknessEntrance,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Swamp Palace entrance (entry node)
                case RequirementNodeID.SPEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.SPEntry, new Mode()));
                    }
                    break;
                //  Access to Skull Woods front entrance (entry node)
                case RequirementNodeID.SWFrontEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Skull Woods left drop entrance (entry node)
                case RequirementNodeID.SWFrontLeftDropEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Skull Woods right drop entrance (entry node)
                case RequirementNodeID.SWPinballRoomEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Skull Woods top drop entrance (entry node)
                case RequirementNodeID.SWFrontTopDropEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.DWNotBunny, new Mode()));
                    }
                    break;
                //  Access to Skull Woods front to back connector entrance (entry node)
                case RequirementNodeID.SWFrontBackConnectorEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Skull Woods back entrance (entry node)
                case RequirementNodeID.SWBackEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SkullWoodsBackEntrance,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Thieves' Town entrance (entry node)
                case RequirementNodeID.TTEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.ThievesTownEntrance,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Ice Palace entrance (entry node)
                case RequirementNodeID.IPEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IcePalaceEntrance,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Misery Mire entrance (entry node)
                case RequirementNodeID.MMEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MiseryMireEntrance,
                            RequirementType.DWNotBunny, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MiseryMireEntrance,
                            RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
                    }
                    break;
                //  Access to Turtle Rock front entrance (entry node)
                case RequirementNodeID.TRFrontEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockFrontEntrance,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Turtle Rock middle entrances (entry node)
                case RequirementNodeID.TRMiddleEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockTunnel,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Turtle Rock front entrance (entry node)
                case RequirementNodeID.TRBackEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TurtleRockSafetyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Turtle Rock front entrance (entry node)
                case RequirementNodeID.GTEntry:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.Start,
                            RequirementType.None, new Mode() { EntranceShuffle = true }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GanonsTowerEntrance,
                            RequirementType.None, new Mode() { WorldState = WorldState.StandardOpen }));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.AgahnimTowerEntrance,
                            RequirementType.None, new Mode() { WorldState = WorldState.Inverted }));
                    }
                    break;
                //  Access to the Sanctuary (dungeon node)
                case RequirementNodeID.HCSanctuary:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HCSanctuaryEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to the Hyrule Castle front area (dungeon node)
                case RequirementNodeID.HCFront:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HCFrontEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to the area of Escape past the first key door
                case RequirementNodeID.HCPastEscapeFirstKeyDoor:
                    {
                    }
                    break;
                //  Access to the area of Escape past the second key door
                case RequirementNodeID.HCPastEscapeSecondKeyDoor:
                    {
                    }
                    break;
                //  Access to the Sewer dark rooms from the front
                case RequirementNodeID.HCDarkRoomFront:
                    {
                    }
                    break;
                //  Access to past the Dark Cross key door
                case RequirementNodeID.HCPastDarkCrossKeyDoor:
                    {
                    }
                    break;
                //  Access past the sewer rat room key door
                case RequirementNodeID.HCPastSewerRatRoomKeyDoor:
                    {
                    }
                    break;
                //  Access to the Sewer dark rooms from the back
                case RequirementNodeID.HCDarkRoomBack:
                    {
                    }
                    break;
                //  Access to the back of the sewers (dungeon node)
                case RequirementNodeID.HCBack:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HCBackEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Agahnim Tower (dungeon node)
                case RequirementNodeID.AT:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.ATEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access past the first key door in Agahnim's Tower
                case RequirementNodeID.ATPastFirstKeyDoor:
                    {
                    }
                    break;
                //  Access to the dark rooms in Agahnim's Tower
                case RequirementNodeID.ATDarkMaze:
                    {
                    }
                    break;
                //  Access past the second key door in Agahnim's Tower
                case RequirementNodeID.ATPastSecondKeyDoor:
                    {
                    }
                    break;
                //  Access past the third key door in Agahnim's Tower
                case RequirementNodeID.ATPastThirdKeyDoor:
                    {
                    }
                    break;
                //  Access past the fourth key door in Agahnim's Tower
                case RequirementNodeID.ATPastFourthKeyDoor:
                    {
                    }
                    break;
                //  Access to the boss room of Agahnim's Tower (past the curtain)
                case RequirementNodeID.ATBossRoom:
                    {
                    }
                    break;
                //  Access to the boss in Agahnim's Tower
                case RequirementNodeID.ATBoss:
                    {
                    }
                    break;
                //  Access to Eastern Palace (dungeon node)
                case RequirementNodeID.EP:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.EPEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Eastern Palace big chest
                case RequirementNodeID.EPBigChest:
                    {
                    }
                    break;
                //  Access to Eastern Palace right wing dark room
                case RequirementNodeID.EPRightWingDarkRoom:
                    {
                    }
                    break;
                //  Access to Eastern Palace past the right wing key door
                case RequirementNodeID.EPPastRightWingKeyDoor:
                    {
                    }
                    break;
                //  Access to Eastern Palace past the big key door
                case RequirementNodeID.EPPastBigKeyDoor:
                    {
                    }
                    break;
                //  Access to Eastern Palace boss area dark room
                case RequirementNodeID.EPBackDarkRoom:
                    {
                    }
                    break;
                //  Access to Eastern Palace past the boss area key door
                case RequirementNodeID.EPPastBackKeyDoor:
                    {
                    }
                    break;
                //  Access to Eastern Palace boss room (past Red Eyegors)
                case RequirementNodeID.EPBossRoom:
                    {
                    }
                    break;
                //  Access to Eastern Palace boss
                case RequirementNodeID.EPBoss:
                    {
                    }
                    break;
                //  Access to Desert Palace front (dungeon node)
                case RequirementNodeID.DPFront:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DPFrontEntry,
                            RequirementType.None, new Mode()));
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DPLeftEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Desert Palace big chest
                case RequirementNodeID.DPBigChest:
                    {
                    }
                    break;
                //  Access to Desert Palace past the right wing key door
                case RequirementNodeID.DPPastRightWingKeyDoor:
                    {
                    }
                    break;
                //  Access to Desert Palace back
                case RequirementNodeID.DPBack:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DPBackEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Desert Palace 2F
                case RequirementNodeID.DP2F:
                    {
                    }
                    break;
                //  Access to Desert Palace past first 2F key door
                case RequirementNodeID.DP2FPastFirstKeyDoor:
                    {
                    }
                    break;
                //  Access to Desert Palace past second 2F key door
                case RequirementNodeID.DP2FPastSecondKeyDoor:
                    {
                    }
                    break;
                //  Access to Desert Palace four torch wall
                case RequirementNodeID.DPPastFourTorchWall:
                    {
                    }
                    break;
                //  Access to Desert Palace boss room
                case RequirementNodeID.DPBossRoom:
                    {
                    }
                    break;
                //  Access to Desert Palace boss
                case RequirementNodeID.DPBoss:
                    {
                    }
                    break;
                //  Access to Tower of Hera (dungeon node)
                case RequirementNodeID.ToH:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.ToHEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Tower of Hera past the key door
                case RequirementNodeID.ToHPastKeyDoor:
                    {
                    }
                    break;
                //  Access to Tower of Hera basement torch room
                case RequirementNodeID.ToHBasementTorchRoom:
                    {
                    }
                    break;
                //  Access to Tower of Hera past the big key door
                case RequirementNodeID.ToHPastBigKeyDoor:
                    {
                    }
                    break;
                //  Access to Tower of Hera big chest
                case RequirementNodeID.ToHBigChest:
                    {
                    }
                    break;
                //  Access to Tower of Hera boss
                case RequirementNodeID.ToHBoss:
                    {
                    }
                    break;
                //  Access to Palace of Darkness (dungeon node)
                case RequirementNodeID.PoD:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Palace of Darkness past the first red Goriya room
                case RequirementNodeID.PoDPastFirstRedGoriyaRoom:
                    {
                    }
                    break;
                //  Access to Palace of Darkness lobby and arena areas
                case RequirementNodeID.PoDLobbyArena:
                    {
                    }
                    break;
                //  Access to Palace of Darkness Big Key Chest area
                case RequirementNodeID.PoDBigKeyChestArea:
                    {
                    }
                    break;
                //  Access to Palace of Darkness Collapsing Walkway section
                case RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor:
                    {
                    }
                    break;
                //  Access to Palace of Darkness Dark basement area (outside of the boss room tunnel)
                case RequirementNodeID.PoDDarkBasement:
                    {
                    }
                    break;
                //  Access to Palace of Darkness "Harmless Hellway" room
                case RequirementNodeID.PoDHarmlessHellwayRoom:
                    {
                    }
                    break;
                //  Access to Palace of Darkness Dark Maze via key door
                case RequirementNodeID.PoDPastDarkMazeKeyDoor:
                    {
                    }
                    break;
                //  Access to Palace of Darkness Dark Maze
                case RequirementNodeID.PoDDarkMaze:
                    {
                    }
                    break;
                //  Access to Palace of Darkness Big Chest ledge
                case RequirementNodeID.PoDBigChestLedge:
                    {
                    }
                    break;
                //  Access to Palace of Darkness Big Chest
                case RequirementNodeID.PoDBigChest:
                    {
                    }
                    break;
                //  Access to Palace of Darkness past the second red Goriya room
                case RequirementNodeID.PoDPastSecondRedGoriyaRoom:
                    {
                    }
                    break;
                //  Access to Palace of Darkness past the bow eyeball statue
                case RequirementNodeID.PoDPastBowStatue:
                    {
                    }
                    break;
                //  Access to Palace of Darkness boss area dark rooms
                case RequirementNodeID.PoDBossAreaDarkRooms:
                    {
                    }
                    break;
                //  Access to Palace of Darkness past the boss area hammer blocks
                case RequirementNodeID.PoDPastHammerBlocks:
                    {
                    }
                    break;
                //  Access to Palace of Darkness past the boss area key door
                case RequirementNodeID.PoDPastBossAreaKeyDoor:
                    {
                    }
                    break;
                //  Access to Palace of Darkness boss room
                case RequirementNodeID.PoDBossRoom:
                    {
                    }
                    break;
                //  Access to Palace of Darkness boss
                case RequirementNodeID.PoDBoss:
                    {
                    }
                    break;
                //  Access to Swamp Palace (dungeon node)
                case RequirementNodeID.SP:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SPEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Swamp Palace past the river
                case RequirementNodeID.SPAfterRiver:
                    {
                    }
                    break;
                //  Access to Swamp Palace B1
                case RequirementNodeID.SPB1:
                    {
                    }
                    break;
                //  Access to Swamp Palace past the first right side key door
                case RequirementNodeID.SPPastFirstRightSideKeyDoor:
                    {
                    }
                    break;
                //  Access to Swamp Palace past the second right side key door
                case RequirementNodeID.SPPastSecondRightSideKeyDoor:
                    {
                    }
                    break;
                //  Access to Swamp Palace past the hammer blocks on the right side
                case RequirementNodeID.SPPastRightSideHammerBlocks:
                    {
                    }
                    break;
                //  Access to Swamp Palace the key under the skull
                case RequirementNodeID.SPB1KeyLedge:
                    {
                    }
                    break;
                //  Access to Swamp Palace past the left side key door
                case RequirementNodeID.SPPastLeftSideKeyDoor:
                    {
                    }
                    break;
                //  Access to Swamp Palace big chest
                case RequirementNodeID.SPBigChest:
                    {
                    }
                    break;
                //  Access to Swamp Palace back
                case RequirementNodeID.SPBack:
                    {
                    }
                    break;
                //  Access to Swamp Palace back past the first key door
                case RequirementNodeID.SPPastBackFirstKeyDoor:
                    {
                    }
                    break;
                //  Access to Swamp Palace boss room
                case RequirementNodeID.SPBossRoom:
                    {
                    }
                    break;
                //  Access to Swamp Palace boss
                case RequirementNodeID.SPBoss:
                    {
                    }
                    break;
                //  Access to Skull Woods big chest area bottom
                case RequirementNodeID.SWBigChestAreaBottom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Skull Woods big chest area top
                case RequirementNodeID.SWBigChestAreaTop:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontTopDropEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Skull Woods big chest
                case RequirementNodeID.SWBigChest:
                    {
                    }
                    break;
                //  Access to Skull Woods front left side (behind key door)
                case RequirementNodeID.SWFrontLeftSide:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontLeftDropEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Skull Woods front right side (pinball room)
                case RequirementNodeID.SWFrontRightSide:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWPinballRoomEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Skull Woods front to back connector
                case RequirementNodeID.SWFrontBackConnector:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontBackConnectorEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Skull Woods behind the worthless key door
                case RequirementNodeID.SWPastTheWorthlessKeyDoor:
                    {
                    }
                    break;
                //  Access to Skull Woods back (dungeon node)
                case RequirementNodeID.SWBack:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWBackEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Skull Woods back past first key door
                case RequirementNodeID.SWBackPastFirstKeyDoor:
                    {
                    }
                    break;
                //  Access to Skull Woods back past four torch room
                case RequirementNodeID.SWBackPastFourTorchRoom:
                    {
                    }
                    break;
                //  Access to Skull Woods back past the curtain
                case RequirementNodeID.SWBackPastCurtains:
                    {
                    }
                    break;
                //  Access to Skull Woods boss room
                case RequirementNodeID.SWBossRoom:
                    {
                    }
                    break;
                //  Access to Skull Woods boss
                case RequirementNodeID.SWBoss:
                    {
                    }
                    break;
                //  Access to Thieves' Town entrance (dungeon node)
                case RequirementNodeID.TT:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TTEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Thieves' Town past the big key door
                case RequirementNodeID.TTPastBigKeyDoor:
                    {
                    }
                    break;
                //  Access to Thieves' Town past the first key door
                case RequirementNodeID.TTPastFirstKeyDoor:
                    {
                    }
                    break;
                //  Access to Thieves' Town past the second key door
                case RequirementNodeID.TTPastSecondKeyDoor:
                    {
                    }
                    break;
                //  Access to Thieves' Town past the big chest room key door
                case RequirementNodeID.TTPastBigChestRoomKeyDoor:
                    {
                    }
                    break;
                //  Access to Thieves' Town past the big chest hammer blocks
                case RequirementNodeID.TTPastHammerBlocks:
                    {
                    }
                    break;
                //  Access to Thieves' Town big chest
                case RequirementNodeID.TTBigChest:
                    {
                    }
                    break;
                //  Access to Thieves' Town boss room (with access to the boss)
                case RequirementNodeID.TTBossRoom:
                    {
                    }
                    break;
                //  Access to Thieves' Town boss
                case RequirementNodeID.TTBoss:
                    {
                    }
                    break;
                //  Access to Ice Palace entrance (dungeon node)
                case RequirementNodeID.IP:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IPEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Ice Palace past the entrance freezor room
                case RequirementNodeID.IPPastEntranceFreezorRoom:
                    {
                    }
                    break;
                //  Access to Ice Palace B1 left side (past the first key door)
                case RequirementNodeID.IPB1LeftSide:
                    {
                    }
                    break;
                //  Access to Ice Palace B1 right side (big key chest location)
                case RequirementNodeID.IPB1RightSide:
                    {
                    }
                    break;
                //  Access to Ice Palace B2 left side
                case RequirementNodeID.IPB2LeftSide:
                    {
                    }
                    break;
                //  Access to Ice Palace B2 past the key door (IPBJ room)
                case RequirementNodeID.IPB2PastKeyDoor:
                    {
                    }
                    break;
                //  Access to Ice Palace past the hammer blocks on B2
                case RequirementNodeID.IPB2PastHammerBlocks:
                    {
                    }
                    break;
                //  Access to Ice Palace past the lift block on B2
                case RequirementNodeID.IPB2PastLiftBlock:
                    {
                    }
                    break;
                //  Access to Ice Palace spike room left side
                case RequirementNodeID.IPSpikeRoom:
                    {
                    }
                    break;
                //  Access to Ice Palace B4 right side
                case RequirementNodeID.IPB4RightSide:
                    {
                    }
                    break;
                //  Access to Ice Palace B4 ice room
                case RequirementNodeID.IPB4IceRoom:
                    {
                    }
                    break;
                //  Access to Ice Palace freezor room
                case RequirementNodeID.IPB4FreezorRoom:
                    {
                    }
                    break;
                //  Access to Ice Palace B4 past south key door
                case RequirementNodeID.IPB4PastKeyDoor:
                    {
                    }
                    break;
                //  Access to Ice Palace big chest area
                case RequirementNodeID.IPBigChestArea:
                    {
                    }
                    break;
                //  Access to Ice Palace big chest
                case RequirementNodeID.IPBigChest:
                    {
                    }
                    break;
                //  Access to Ice Palace B5
                case RequirementNodeID.IPB5:
                    {
                    }
                    break;
                //  Access to Ice Palace B5 past the big key door
                case RequirementNodeID.IPB5PastBigKeyDoor:
                    {
                    }
                    break;
                //  Access to Ice Palace B6
                case RequirementNodeID.IPB6:
                    {
                    }
                    break;
                //  Access to Ice Palace past the B6 key door
                case RequirementNodeID.IPB6PastKeyDoor:
                    {
                    }
                    break;
                //  Access to Ice Palace past the B6 hammer blocks
                case RequirementNodeID.IPB6PastHammerBlocks:
                    {
                    }
                    break;
                //  Access to Ice Palace past the B6 lift block
                case RequirementNodeID.IPB6PastLiftBlock:
                    {
                    }
                    break;
                //  Access to Ice Palace boss
                case RequirementNodeID.IPBoss:
                    {
                    }
                    break;
                //  Access to Misery Mire entrance (dungeon node)
                case RequirementNodeID.MM:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MMEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Misery Mire past the entrance gap
                case RequirementNodeID.MMPastEntranceGap:
                    {
                    }
                    break;
                //  Access to Misery Mire big chest
                case RequirementNodeID.MMBigChest:
                    {
                    }
                    break;
                //  Access to Misery Mire past the top right key door
                case RequirementNodeID.MMB1TopSide:
                    {
                    }
                    break;
                //  Access to Misery Mire lobby beyond the blue blocks
                case RequirementNodeID.MMB1LobbyBeyondBlueBlocks:
                    {
                    }
                    break;
                //  Access to Misery Mire right side beyond the blue blocks
                case RequirementNodeID.MMB1RightSideBeyondBlueBlocks:
                    {
                    }
                    break;
                //  Access to Misery Mire left side past first key door
                case RequirementNodeID.MMB1LeftSidePastFirstKeyDoor:
                    {
                    }
                    break;
                //  Access to Misery Mire left side past second key door
                case RequirementNodeID.MMB1LeftSidePastSecondKeyDoor:
                    {
                    }
                    break;
                //  Access to Misery Mire past the B1 four torch room (north of tile room)
                case RequirementNodeID.MMB1PastFourTorchRoom:
                    {
                    }
                    break;
                //  Access to Misery Mire past the F1 four torch room (cutscene room)
                case RequirementNodeID.MMF1PastFourTorchRoom:
                    {
                    }
                    break;
                //  Access to Misery Mire past the big key door leading to the portal
                case RequirementNodeID.MMB1PastPortalBigKeyDoor:
                    {
                    }
                    break;
                //  Access to Misery Mire past the big key door leading to the boss area
                case RequirementNodeID.MMB1PastBridgeBigKeyDoor:
                    {
                    }
                    break;
                //  Access to Misery Mire dark room
                case RequirementNodeID.MMDarkRoom:
                    {
                    }
                    break;
                //  Access to Misery Mire past the worthless key door
                case RequirementNodeID.MMB2PastWorthlessKeyDoor:
                    {
                    }
                    break;
                //  Access to Misery Mire dark room
                case RequirementNodeID.MMB2PastCaneOfSomariaSwitch:
                    {
                    }
                    break;
                //  Access to Misery Mire boss room (past Somaria switch)
                case RequirementNodeID.MMBossRoom:
                    {
                    }
                    break;
                //  Access to Misery Mire boss
                case RequirementNodeID.MMBoss:
                    {
                    }
                    break;
                //  Access to Turtle Rock front entrance (dungeon node)
                case RequirementNodeID.TRFront:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRFrontEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Turtle Rock compass chest area
                case RequirementNodeID.TRF1CompassChestArea:
                    {
                    }
                    break;
                //  Access to Turtle Rock F1 four torch room
                case RequirementNodeID.TRF1FourTorchRoom:
                    {
                    }
                    break;
                case RequirementNodeID.TRF1RollerRoom:
                    {
                    }
                    break;
                //  Access to Turtle Rock F1 first key door
                case RequirementNodeID.TRF1FirstKeyDoorArea:
                    {
                    }
                    break;
                //  Access to Turtle Rock F1 past first key door
                case RequirementNodeID.TRF1PastFirstKeyDoor:
                    {
                    }
                    break;
                //  Access to Turtle Rock F1 past second key door
                case RequirementNodeID.TRF1PastSecondKeyDoor:
                    {
                    }
                    break;
                //  Access to Turtle Rock B1
                case RequirementNodeID.TRB1:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRMiddleEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Turtle Rock B1 past big key chest key door
                case RequirementNodeID.TRB1PastBigKeyChestKeyDoor:
                    {
                    }
                    break;
                //  Access to Turtle Rock B1 middle right entrance area
                case RequirementNodeID.TRB1MiddleRightEntranceArea:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRMiddleEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Turtle Rock B1 big chest area
                case RequirementNodeID.TRB1BigChestArea:
                    {
                    }
                    break;
                //  Access to Turtle Rock big chest
                case RequirementNodeID.TRBigChest:
                    {
                    }
                    break;
                //  Access to Turtle Rock B1 right side
                case RequirementNodeID.TRB1RightSide:
                    {
                    }
                    break;
                //  Access to Turtle Rock past the key door connecting B1 and B2
                case RequirementNodeID.TRPastB1toB2KeyDoor:
                    {
                    }
                    break;
                //  Access to Turtle Rock B2 dark room top
                case RequirementNodeID.TRB2DarkRoomTop:
                    {
                    }
                    break;
                //  Access to Turtle Rock B2 dark room bottom
                case RequirementNodeID.TRB2DarkRoomBottom:
                    {
                    }
                    break;
                //  Access to Turtle Rock B2 past dark maze
                case RequirementNodeID.TRB2PastDarkMaze:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRBackEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Turtle Rock B2 past key door
                case RequirementNodeID.TRB2PastKeyDoor:
                    {
                    }
                    break;
                //  Access to Turtle Rock B3
                case RequirementNodeID.TRB3:
                    {
                    }
                    break;
                //  Access to Turtle Rock B3 boss room entry
                case RequirementNodeID.TRB3BossRoomEntry:
                    {
                    }
                    break;
                //  Access to Turtle Rock boss room
                case RequirementNodeID.TRBossRoom:
                    {
                    }
                    break;
                //  Access to Turtle Rock boss
                case RequirementNodeID.TRBoss:
                    {
                    }
                    break;
                //  Access to Ganon's Tower entrance (dungeon node)
                case RequirementNodeID.GT:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GTEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                //  Access to Ganon's Tower 1F left side
                case RequirementNodeID.GT1FLeft:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 1F left side past the hammer blocks
                case RequirementNodeID.GT1FLeftPastHammerBlocks:
                    {
                    }
                    break;
                //  Access to Ganon's Tower dark magician's room
                case RequirementNodeID.GT1FLeftDMsRoom:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 1F left side past the bonkable gaps
                case RequirementNodeID.GT1FLeftPastBonkableGaps:
                    {
                    }
                    break;
                //  Access to Ganon's Tower map chest room
                case RequirementNodeID.GT1FLeftMapChestRoom:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 1F left side room that portals to firesnake room
                case RequirementNodeID.GT1FLeftSpikeTrapPortalRoom:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 1F left side firesnake room
                case RequirementNodeID.GT1FLeftFiresnakeRoom:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 1F left across the firesnake room gap
                case RequirementNodeID.GT1FLeftPastFiresnakeRoomGap:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 1F left past the firesnake room key door
                case RequirementNodeID.GT1FLeftPastFiresnakeRoomKeyDoor:
                    {
                    }
                    break;
                //  Access to Ganon's Tower randomizer room
                case RequirementNodeID.GT1FLeftRandomizerRoom:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 1F right side
                case RequirementNodeID.GT1FRight:
                    {
                    }
                    break;
                //  Access to Ganon's Tower tile room
                case RequirementNodeID.GT1FRightTileRoom:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 1F right side four torch room
                case RequirementNodeID.GT1FRightFourTorchRoom:
                    {
                    }
                    break;
                //  Access to Ganon's Tower compass room
                case RequirementNodeID.GT1FRightCompassRoom:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 1F right side past the compass room portal
                case RequirementNodeID.GT1FRightPastCompassRoomPortal:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 1F right side collapsing walkway room
                case RequirementNodeID.GT1FRightCollapsingWalkway:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 1F room where left and right side converge on the bottom
                case RequirementNodeID.GT1FBottomRoom:
                    {
                    }
                    break;
                //  Access to Ganon's Tower first boss (B1/vanilla Armos)
                case RequirementNodeID.GTBoss1:
                    {
                    }
                    break;
                //  Access to Ganon's Tower room north of first boss
                case RequirementNodeID.GTB1BossChests:
                    {
                    }
                    break;
                //  Access to Ganon's Tower big chest
                case RequirementNodeID.GTBigChest:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 3F past the red Goriya rooms
                case RequirementNodeID.GT3FPastRedGoriyaRooms:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 3F past the big key door
                case RequirementNodeID.GT3FPastBigKeyDoor:
                    {
                    }
                    break;
                //  Access to Ganon's Tower second boss (3F/vanilla Lanmolas)
                case RequirementNodeID.GTBoss2:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 3F past the second boss
                case RequirementNodeID.GT3FPastBoss2:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 5F past the four torch rooms
                case RequirementNodeID.GT5FPastFourTorchRooms:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 6F past the first key door
                case RequirementNodeID.GT6FPastFirstKeyDoor:
                    {
                    }
                    break;
                //  Access to Ganon's Tower 6F boss room (past the second key door)
                case RequirementNodeID.GT6FBossRoom:
                    {
                    }
                    break;
                //  Access to Ganon's Tower third boss (6F/vanilla Moldorm)
                case RequirementNodeID.GTBoss3:
                    {
                    }
                    break;
                //  Access to Ganon's Tower past the boss room gap (hookshot or hover)
                case RequirementNodeID.GT6FPastBossRoomGap:
                    {
                    }
                    break;
                //  Access to Ganon's Tower past the big key door
                case RequirementNodeID.GTFinalBossRoom:
                    {
                    }
                    break;
                //  Access to Ganon's Tower final boss
                case RequirementNodeID.GTFinalBoss:
                    {
                    }
                    break;
            }

            NodeSubscriptions = new List<RequirementNodeID>();
            RequirementSubscriptions = new List<RequirementType>();

            Game.Mode.PropertyChanged += OnModeChanged;

            if (_directAccessItem.HasValue)
                Game.Items[_directAccessItem.Value].PropertyChanged += OnRequirementChanged;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState) ||
                e.PropertyName == nameof(Mode.EntranceShuffle))
                UpdateAccessibility();
        }

        protected void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void OnNodeConnectionChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        public virtual AccessibilityLevel GetNodeAccessibility(List<RequirementNodeID> excludedNodes)
        {
            if (ID == RequirementNodeID.Start)
                return AccessibilityLevel.Normal;

            if (_directAccessItem.HasValue && Game.Items.Has(_directAccessItem.Value))
                return AccessibilityLevel.Normal;

            if (excludedNodes == null)
                throw new ArgumentNullException(nameof(excludedNodes));

            List<RequirementNodeID> newExcludedNodes =
                excludedNodes.GetRange(0, excludedNodes.Count);

            newExcludedNodes.Add(ID);

            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            foreach (RequirementNodeConnection connection in Connections)
            {
                if (!Game.Mode.Validate(connection.RequiredMode))
                    continue;

                if (newExcludedNodes.Contains(connection.FromNode))
                    continue;

                AccessibilityLevel nodeAccessibility =
                    Game.RequirementNodes[connection.FromNode].GetNodeAccessibility(newExcludedNodes);

                if (nodeAccessibility < AccessibilityLevel.SequenceBreak)
                    continue;

                AccessibilityLevel requirementAccessibility = Game.Requirements[connection.Requirement].Accessibility;

                AccessibilityLevel finalConnectionAccessibility =
                    (AccessibilityLevel)Math.Min(Math.Min((byte)nodeAccessibility, (byte)requirementAccessibility),
                    (byte)connection.MaximumAccessibility);

                if (finalConnectionAccessibility == AccessibilityLevel.Normal)
                    return AccessibilityLevel.Normal;

                if (finalConnectionAccessibility > finalAccessibility)
                    finalAccessibility = finalConnectionAccessibility;
            }

            return finalAccessibility;
        }

        protected void UpdateAccessibility()
        {
            Accessibility = GetNodeAccessibility(new List<RequirementNodeID>());
        }

        public virtual void Initialize()
        {
            foreach (RequirementNodeConnection connection in Connections)
            {
                if (!NodeSubscriptions.Contains(connection.FromNode))
                {
                    Game.RequirementNodes[connection.FromNode].PropertyChanged += OnRequirementChanged;
                    NodeSubscriptions.Add(connection.FromNode);
                }

                if (!RequirementSubscriptions.Contains(connection.Requirement))
                {
                    Game.Requirements[connection.Requirement].PropertyChanged += OnRequirementChanged;
                    RequirementSubscriptions.Add(connection.Requirement);
                }
            }

            UpdateAccessibility();
        }
    }
}
