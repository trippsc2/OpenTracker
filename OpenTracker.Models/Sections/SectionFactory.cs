using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class for creating section classes.
    /// </summary>
    internal static class SectionFactory
    {
        /// <summary>
        /// Returns the name of the specified section by location ID and section index.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A string representing the section name.
        /// </returns>
        private static string GetSectionName(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    {
                        return "By The Door";
                    }
                case LocationID.Pedestal:
                    {
                        return "Pedestal";
                    }
                case LocationID.BlindsHouse when index == 0:
                    {
                        return "Main";
                    }
                case LocationID.BlindsHouse when index == 1:
                case LocationID.TheWell when index == 1:
                    {
                        return "Bomb";
                    }
                case LocationID.BottleVendor:
                    {
                        return "Man";
                    }
                case LocationID.ChickenHouse:
                    {
                        return "Bombable Wall";
                    }
                case LocationID.Tavern:
                    {
                        return "Back Room";
                    }
                case LocationID.SickKid:
                    {
                        return "By The Bed";
                    }
                case LocationID.MagicBat:
                    {
                        return "Magic Bowl";
                    }
                case LocationID.RaceGame:
                    {
                        return "Take This Trash";
                    }
                case LocationID.Library:
                    {
                        return "On The Shelf";
                    }
                case LocationID.MushroomSpot:
                    {
                        return "Shroom";
                    }
                case LocationID.ForestHideout:
                    {
                        return "Hideout";
                    }
                case LocationID.CastleSecret when index == 0:
                    {
                        return "Uncle";
                    }
                case LocationID.CastleSecret when index == 1:
                    {
                        return "Hallway";
                    }
                case LocationID.WitchsHut:
                    {
                        return "Assistant";
                    }
                case LocationID.SahasrahlasHut when index == 0:
                    {
                        return "Back Room";
                    }
                case LocationID.SahasrahlasHut when index == 1:
                    {
                        return "Saha";
                    }
                case LocationID.KingsTomb:
                case LocationID.KingsTombEntrance:
                    {
                        return "The Crypt";
                    }
                case LocationID.GroveDiggingSpot:
                    {
                        return "Hidden Treasure";
                    }
                case LocationID.Dam when index == 0:
                    {
                        return "Inside";
                    }
                case LocationID.Dam when index == 1:
                    {
                        return "Outside";
                    }
                case LocationID.Hobo:
                    {
                        return "Under The Bridge";
                    }
                case LocationID.PyramidLedge:
                case LocationID.ZoraArea when index == 0:
                case LocationID.DesertLedge:
                case LocationID.BumperCave:
                    {
                        return "Ledge";
                    }
                case LocationID.FatFairy:
                    {
                        return "Big Bomb Spot";
                    }
                case LocationID.HauntedGrove:
                    {
                        return "Stumpy";
                    }
                case LocationID.BombosTablet:
                case LocationID.EtherTablet:
                    {
                        return "Tablet";
                    }
                case LocationID.SouthOfGrove:
                    {
                        return "Circle of Bushes";
                    }
                case LocationID.DiggingGame:
                    {
                        return "Dig For Treasure";
                    }
                case LocationID.WaterfallFairy:
                case LocationID.WaterfallFairyEntrance:
                    {
                        return "Waterfall Cave";
                    }
                case LocationID.ZoraArea when index == 1:
                    {
                        return "King Zora";
                    }
                case LocationID.Catfish:
                    {
                        return "Ring of Stones";
                    }
                case LocationID.CShapedHouse:
                case LocationID.LumberjackHouseEntrance:
                case LocationID.KakarikoFortuneTellerEntrance:
                case LocationID.WomanLeftDoor:
                case LocationID.WomanRightDoor:
                case LocationID.LeftSnitchHouseEntrance:
                case LocationID.RightSnitchHouseEntrance:
                case LocationID.BlindsHouseEntrance:
                case LocationID.ChickenHouseEntrance:
                case LocationID.GrassHouseEntrance:
                case LocationID.TavernFront:
                case LocationID.KakarikoShop:
                case LocationID.BombHutEntrance:
                case LocationID.SickKidEntrance:
                case LocationID.BlacksmithHouse:
                case LocationID.ChestGameEntrance:
                case LocationID.RaceHouseLeft:
                case LocationID.RaceHouseRight:
                case LocationID.LibraryEntrance:
                case LocationID.ForestChestGameEntrance:
                case LocationID.WitchsHutEntrance:
                case LocationID.SahasrahlasHutEntrance:
                case LocationID.CShapedHouseEntrance:
                case LocationID.HammerHouse:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.BombableShackEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.DarkWitchsHut:
                case LocationID.LinksHouseEntrance:
                    {
                        return "House";
                    }
                case LocationID.TreasureGame:
                    {
                        return "Prize";
                    }
                case LocationID.BombableShack:
                    {
                        return "Downstairs";
                    }
                case LocationID.Blacksmith:
                    {
                        return "Bring Frog Home";
                    }
                case LocationID.PurpleChest:
                    {
                        return "Gary";
                    }
                case LocationID.LakeHyliaIsland:
                case LocationID.FloatingIsland:
                    {
                        return "Island";
                    }
                case LocationID.MireShack:
                    {
                        return "Shack";
                    }
                case LocationID.OldMan:
                    {
                        return "Old Man";
                    }
                case LocationID.SpectacleRock when index == 0:
                case LocationID.ParadoxCave when index == 1:
                    {
                        return "Top";
                    }
                case LocationID.ParadoxCave when index == 0:
                    {
                        return "Bottom";
                    }
                case LocationID.HookshotCave when index == 0:
                    {
                        return "Bonkable Chest";
                    }
                case LocationID.HookshotCave when index == 1:
                    {
                        return "Back";
                    }
                case LocationID.HyruleCastle:
                case LocationID.AgahnimTower when index == 0:
                case LocationID.EasternPalace when index == 0:
                case LocationID.DesertPalace when index == 0:
                case LocationID.TowerOfHera when index == 0:
                case LocationID.PalaceOfDarkness when index == 0:
                case LocationID.SwampPalace when index == 0:
                case LocationID.SkullWoods when index == 0:
                case LocationID.ThievesTown when index == 0:
                case LocationID.IcePalace when index == 0:
                case LocationID.MiseryMire when index == 0:
                case LocationID.TurtleRock when index == 0:
                case LocationID.GanonsTower when index == 0:
                case LocationID.CastleMainEntrance:
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                case LocationID.CastleTowerEntrance:
                case LocationID.EasternPalaceEntrance:
                case LocationID.SkullWoodsBack:
                case LocationID.ThievesTownEntrance:
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.IcePalaceEntrance:
                case LocationID.MiseryMireEntrance:
                case LocationID.TowerOfHeraEntrance:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                    {
                        return "Dungeon";
                    }
                case LocationID.AgahnimTower when index == 1:
                case LocationID.EasternPalace when index == 1:
                case LocationID.DesertPalace when index == 1:
                case LocationID.TowerOfHera when index == 1:
                case LocationID.PalaceOfDarkness when index == 1:
                case LocationID.SwampPalace when index == 1:
                case LocationID.SkullWoods when index == 1:
                case LocationID.ThievesTown when index == 1:
                case LocationID.IcePalace when index == 1:
                case LocationID.MiseryMire when index == 1:
                case LocationID.TurtleRock when index == 1:
                    {
                        return "Boss";
                    }
                case LocationID.GanonsTower when index == 1:
                    {
                        return "Boss 1";
                    }
                case LocationID.GanonsTower when index == 2:
                    {
                        return "Boss 2";
                    }
                case LocationID.GanonsTower when index == 3:
                    {
                        return "Boss 3";
                    }
                case LocationID.GanonsTower when index == 4:
                    {
                        return "Aga 2";
                    }
                case LocationID.LumberjackCaveEntrance:
                case LocationID.TheWellEntrance:
                case LocationID.MagicBatEntrance:
                case LocationID.ForestHideoutEntrance:
                case LocationID.CastleSecretEntrance:
                case LocationID.HoulihanHole:
                case LocationID.SanctuaryGrave:
                case LocationID.GanonHole:
                    {
                        return "Dropdown";
                    }
                case LocationID.TreesFairyCaveTakeAny:
                case LocationID.PegsFairyCaveTakeAny:
                case LocationID.KakarikoFortuneTellerTakeAny:
                case LocationID.GrassHouseTakeAny:
                case LocationID.ForestChestGameTakeAny:
                case LocationID.LumberjackHouseTakeAny:
                case LocationID.LeftSnitchHouseTakeAny:
                case LocationID.RightSnitchHouseTakeAny:
                case LocationID.BombHutTakeAny:
                case LocationID.IceFairyCaveTakeAny:
                case LocationID.RupeeCaveTakeAny:
                case LocationID.CentralBonkRocksTakeAny:
                case LocationID.ThiefCaveTakeAny:
                case LocationID.IceBeeCaveTakeAny:
                case LocationID.FortuneTellerTakeAny:
                case LocationID.HypeFairyCaveTakeAny:
                case LocationID.ChestGameTakeAny:
                case LocationID.EDMFairyCaveTakeAny:
                case LocationID.DarkChapelTakeAny:
                case LocationID.DarkVillageFortuneTellerTakeAny:
                case LocationID.DarkTreesFairyCaveTakeAny:
                case LocationID.DarkSahasrahlaTakeAny:
                case LocationID.DarkFluteSpotFiveTakeAny:
                case LocationID.ArrowGameTakeAny:
                case LocationID.DarkCentralBonkRocksTakeAny:
                case LocationID.DarkIceRodCaveTakeAny:
                case LocationID.DarkFakeIceRodCaveTakeAny:
                case LocationID.DarkIceRodRockTakeAny:
                case LocationID.DarkMountainFairyTakeAny:
                case LocationID.MireRightShackTakeAny:
                case LocationID.MireCaveTakeAny:
                    {
                        return "Take Any";
                    }
                default:
                    {
                        return "Cave";
                    }
            }
        }

        /// <summary>
        /// Returns item section total.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer representing the total items in the section.
        /// </returns>
        private static int GetItemSectionTotal(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.BlindsHouse when index == 0:
                case LocationID.TheWell when index == 0:
                    {
                        return 4;
                    }
                case LocationID.SahasrahlasHut when index == 0:
                case LocationID.HookshotCave when index == 1:
                    {
                        return 3;
                    }
                case LocationID.MiniMoldormCave:
                case LocationID.HypeCave:
                case LocationID.ParadoxCave when index == 1:
                    {
                        return 5;
                    }
                case LocationID.FatFairy:
                case LocationID.WaterfallFairy:
                case LocationID.ParadoxCave when index == 0:
                case LocationID.SuperBunnyCave:
                    {
                        return 2;
                    }
                default:
                    {
                        return 1;
                    }
            }
        }

        /// <summary>
        /// Returns the connections to the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A list of connections to the section.
        /// </returns>
        private static List<RequirementNodeConnection> GetSectionConnections(
            LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(RequirementNodeID.Start)
                        };
                    }
                case LocationID.Pedestal:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Pedestal])
                        };
                    }
                case LocationID.LumberjackCave:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(RequirementNodeID.LumberjackCaveEntrance),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.BlindsHouse when index == 0:
                case LocationID.Tavern:
                case LocationID.Dam when index == 0:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.SuperBunnyMirror])
                        };
                    }
                case LocationID.BlindsHouse when index == 1:
                case LocationID.TheWell when index == 1:
                case LocationID.ChickenHouse:
                case LocationID.MushroomSpot:
                case LocationID.SahasrahlasHut when index == 0:
                case LocationID.AginahsCave:
                case LocationID.Dam when index == 1:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                        };
                    }
                case LocationID.TheWell when index == 0:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole])
                        };
                    }
                case LocationID.BottleVendor:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(RequirementNodeID.LightWorld)
                        };
                    }
                case LocationID.SickKid:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Bottle])
                        };
                    }
                case LocationID.MagicBat:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.MagicBatLedge,
                                RequirementDictionary.Instance[RequirementType.LWPowder])
                        };
                    }
                case LocationID.RaceGame:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.RaceGameLedge,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.Library:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.LWDash]),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.ForestHideout:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.ForestHideout),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.CastleSecret:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.CastleSecretFront),
                            new RequirementNodeConnection(
                                RequirementNodeID.CastleSecretBack)
                        };
                    }
                case LocationID.WitchsHut:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LWWitchArea,
                                RequirementDictionary.Instance[RequirementType.Mushroom])
                        };
                    }
                case LocationID.SahasrahlasHut when index == 1:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.GreenPendant])
                        };
                    }
                case LocationID.BonkRocks:
                case LocationID.NorthBonkRocks:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.NorthBonkRocks)
                        };
                    }
                case LocationID.KingsTomb:
                case LocationID.KingsTombEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.KingsTombGrave)
                        };
                    }
                case LocationID.GroveDiggingSpot:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.LWShovel])
                        };
                    }
                case LocationID.MiniMoldormCave:
                case LocationID.MiniMoldormCaveEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.MiniMoldormCave)
                        };
                    }
                case LocationID.IceRodCave:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.IceRodCave)
                        };
                    }
                case LocationID.Hobo:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LWLakeHylia)
                        };
                    }
                case LocationID.PyramidLedge:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkWorldEast)
                        };
                    }
                case LocationID.FatFairy:
                case LocationID.FatFairyEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.FatFairy)
                        };
                    }
                case LocationID.HauntedGrove:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkWorldSouth)
                        };
                    }
                case LocationID.HypeCave:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkWorldSouth,
                                RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                        };
                    }
                case LocationID.BombosTablet:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.BombosTabletLedge,
                                RequirementDictionary.Instance[RequirementType.Tablet])
                        };
                    }
                case LocationID.SouthOfGrove:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.SouthOfGroveLedge,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                        };
                    }
                case LocationID.DiggingGame:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkWorldSouth,
                                RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                        };
                    }
                case LocationID.WaterfallFairy:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.WaterfallFairy)
                        };
                    }
                case LocationID.ZoraArea when index == 0:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.Zora,
                                RequirementDictionary.Instance[RequirementType.LWSwim]),
                            new RequirementNodeConnection(
                                RequirementNodeID.Zora,
                                RequirementDictionary.Instance[RequirementType.LWFakeFlippersSplashDeletion]),
                            new RequirementNodeConnection(
                                RequirementNodeID.Zora,
                                RequirementDictionary.Instance[RequirementType.LWWaterWalk]),
                            new RequirementNodeConnection(
                                RequirementNodeID.Zora,
                                RequirementDictionary.Instance[RequirementType.Inspect]),
                        };
                    }
                case LocationID.ZoraArea when index == 1:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.Zora)
                        };
                    }
                case LocationID.Catfish:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.Catfish,
                                RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                        };
                    }
                case LocationID.GraveyardLedge:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LWGraveyardLedge,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                        };
                    }
                case LocationID.DesertLedge:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DesertLedge),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.CShapedHouse:
                case LocationID.TreasureGame:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkWorldWest,
                                RequirementDictionary.Instance[RequirementType.NotBunnyDW]),
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkWorldWest,
                                RequirementDictionary.Instance[RequirementType.SuperBunnyMirror])
                        };
                    }
                case LocationID.BombableShack:
                case LocationID.BombableShackEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.BombableShack)
                        };
                    }
                case LocationID.Blacksmith:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.BlacksmithPrison,
                                RequirementDictionary.Instance[RequirementType.LightWorld])
                        };
                    }
                case LocationID.PurpleChest:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.HammerPegsArea,
                                RequirementDictionary.Instance[RequirementType.LightWorld])
                        };
                    }
                case LocationID.HammerPegs:
                case LocationID.HammerPegsEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.HammerPegs)
                        };
                    }
                case LocationID.BumperCave:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.BumperCaveTop),
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkWorldWest,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.LakeHyliaIsland:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LakeHyliaIsland),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.MireShack:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.MireArea,
                                RequirementDictionary.Instance[RequirementType.NotBunnyDW]),
                            new RequirementNodeConnection(
                                RequirementNodeID.MireArea,
                                RequirementDictionary.Instance[RequirementType.SuperBunnyMirror])
                        };
                    }
                case LocationID.CheckerboardCave:
                case LocationID.CheckerboardCaveEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.CheckerboardCave)
                        };
                    }
                case LocationID.OldMan:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainWestBottom,
                                RequirementDictionary.Instance[RequirementType.DarkRoomDeathMountainEntry])
                        };
                    }
                case LocationID.SpectacleRock when index == 0:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.SpectacleRockTop),
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainWestBottom,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.SpectacleRock when index == 1:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainWestBottom)
                        };
                    }
                case LocationID.EtherTablet:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainWestTop,
                                RequirementDictionary.Instance[RequirementType.Tablet])
                        };
                    }
                case LocationID.SpikeCave:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkDeathMountainWestBottom,
                                RequirementDictionary.Instance[RequirementType.DWSpikeCave])
                        };
                    }
                case LocationID.SpiralCave:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastTop,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastTop,
                                RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole])
                        };
                    }
                case LocationID.ParadoxCave when index == 0:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastTop,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                             new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastBottom,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                       };
                    }
                case LocationID.ParadoxCave when index == 1:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastTop,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastBottom,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastTop,
                                RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole]),
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastBottom,
                                RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole])
                        };
                    }
                case LocationID.SuperBunnyCave:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkDeathMountainTop,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkDeathMountainEastBottom,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkDeathMountainTop,
                                RequirementDictionary.Instance[RequirementType.SuperBunnyMirror]),
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkDeathMountainEastBottom,
                                RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole])
                        };
                    }
                case LocationID.HookshotCave when index == 0:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.HookshotCave,
                                RequirementDictionary.Instance[RequirementType.DWHookshot]),
                            new RequirementNodeConnection(
                                RequirementNodeID.HookshotCave,
                                RequirementDictionary.Instance[RequirementType.DWBonkOverLedge])
                        };
                    }
                case LocationID.HookshotCave when index == 1:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.HookshotCave,
                                RequirementDictionary.Instance[RequirementType.DWHookshot]),
                            new RequirementNodeConnection(
                                RequirementNodeID.HookshotCave,
                                RequirementDictionary.Instance[RequirementType.DWHover])
                        };
                    }
                case LocationID.FloatingIsland:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LWFloatingIsland),
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastTop,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.MimicCave:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.MimicCave,
                                RequirementDictionary.Instance[RequirementType.LWHammer])
                        };
                    }
                case LocationID.LumberjackHouseEntrance:
                case LocationID.KakarikoFortuneTellerEntrance:
                case LocationID.WomanLeftDoor:
                case LocationID.WomanRightDoor:
                case LocationID.LeftSnitchHouseEntrance:
                case LocationID.RightSnitchHouseEntrance:
                case LocationID.BlindsHouseEntrance:
                case LocationID.TheWellEntrance:
                case LocationID.ChickenHouseEntrance:
                case LocationID.TavernFront:
                case LocationID.KakarikoShop:
                case LocationID.SickKidEntrance:
                case LocationID.BlacksmithHouse:
                case LocationID.ChestGameEntrance:
                case LocationID.RaceHouseRight:
                case LocationID.LibraryEntrance:
                case LocationID.ForestChestGameEntrance:
                case LocationID.CastleMainEntrance:
                case LocationID.DamEntrance:
                case LocationID.SahasrahlasHutEntrance:
                case LocationID.TreesFairyCaveEntrance:
                case LocationID.PegsFairyCaveEntrance:
                case LocationID.EasternPalaceEntrance:
                case LocationID.AginahsCaveEntrance:
                case LocationID.ThiefCaveEntrance:
                case LocationID.FortuneTellerEntrance:
                case LocationID.LakeShop:
                case LocationID.IceBeeCaveEntrance:
                case LocationID.LinksHouseEntrance:
                case LocationID.TreesFairyCaveTakeAny:
                case LocationID.PegsFairyCaveTakeAny:
                case LocationID.KakarikoFortuneTellerTakeAny:
                case LocationID.ForestChestGameTakeAny:
                case LocationID.LumberjackHouseTakeAny:
                case LocationID.LeftSnitchHouseTakeAny:
                case LocationID.RightSnitchHouseTakeAny:
                case LocationID.ThiefCaveTakeAny:
                case LocationID.IceBeeCaveTakeAny:
                case LocationID.FortuneTellerTakeAny:
                case LocationID.ChestGameTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(RequirementNodeID.LightWorld)
                        };
                    }
                case LocationID.LumberjackCaveEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LumberjackCaveEntrance),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.DeathMountainEntryCave:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEntry)
                        };
                    }
                case LocationID.DeathMountainExitCave:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainExit)
                        };
                    }
                case LocationID.GrassHouseEntrance:
                case LocationID.GrassHouseTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.GrassHouse)
                        };
                    }
                case LocationID.BombHutEntrance:
                case LocationID.BombHutTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.BombHut)
                        };
                    }
                case LocationID.MagicBatEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.MagicBatLedge),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.RaceHouseLeft:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.RaceGameLedge)
                        };
                    }
                case LocationID.ForestHideoutEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.ForestHideout),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.CastleSecretEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.CastleSecretBack),
                            new RequirementNodeConnection(
                                RequirementNodeID.CastleSecretFront,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.HyruleCastleTop)
                        };
                    }
                case LocationID.CastleTowerEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.AgahnimTowerEntrance)
                        };
                    }
                case LocationID.CentralBonkRocksEntrance:
                case LocationID.CentralBonkRocksTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.CentralBonkRocks)
                        };
                    }
                case LocationID.WitchsHutEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LWWitchArea)
                        };
                    }
                case LocationID.WaterfallFairyEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.WaterfallFairy)
                        };
                    }
                case LocationID.HoulihanHole:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.SanctuaryGrave:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.EscapeGrave),
                            new RequirementNodeConnection(
                                RequirementNodeID.LightWorld,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.GraveyardLedgeEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LWGraveyardLedge)
                        };
                    }
                case LocationID.DesertLeftEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DesertLedge)
                        };
                    }
                case LocationID.DesertBackEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DesertPalaceBackEntrance)
                        };
                    }
                case LocationID.DesertRightEntrance:
                    {
                        return new List<RequirementNodeConnection>(0);
                    }
                case LocationID.DesertFrontEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DesertPalaceFrontEntrance)
                        };
                    }
                case LocationID.RupeeCaveEntrance:
                case LocationID.RupeeCaveTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.RupeeCave)
                        };
                    }
                case LocationID.SkullWoodsBack:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.SkullWoodsBackEntrance)
                        };
                    }
                case LocationID.ThievesTownEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.ThievesTownEntrance)
                        };
                    }
                case LocationID.CShapedHouseEntrance:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.DarkChapelEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.DarkChapelTakeAny:
                case LocationID.DarkVillageFortuneTellerTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkWorldWest)
                        };
                    }
                case LocationID.HammerHouse:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.HammerHouse)
                        };
                    }
                case LocationID.BumperCaveExit:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.BumperCaveTop)
                        };
                    }
                case LocationID.BumperCaveEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.BumperCave)
                        };
                    }
                case LocationID.HypeCaveEntrance:
                case LocationID.SwampPalaceEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                case LocationID.ArrowGameTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkWorldSouth)
                        };
                    }
                case LocationID.DarkCentralBonkRocksEntrance:
                case LocationID.DarkCentralBonkRocksTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DWCentralBonkRocks)
                        };
                    }
                case LocationID.SouthOfGroveEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.SouthOfGroveLedge)
                        };
                    }
                case LocationID.DarkTreesFairyCaveEntrance:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.DarkFluteSpotFiveEntrance:
                case LocationID.DarkTreesFairyCaveTakeAny:
                case LocationID.DarkSahasrahlaTakeAny:
                case LocationID.DarkFluteSpotFiveTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkWorldEast)
                        };
                    }
                case LocationID.PalaceOfDarknessEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.PalaceOfDarknessEntrance)
                        };
                    }
                case LocationID.DarkWitchsHut:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DWWitchArea)
                        };
                    }
                case LocationID.GanonHole:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.GanonHole),
                            new RequirementNodeConnection(
                                RequirementNodeID.GanonHoleBack,
                                RequirementDictionary.Instance[RequirementType.Inspect])
                        };
                    }
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.DarkIceRodCaveTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DWIceRodCave)
                        };
                    }
                case LocationID.DarkFakeIceRodCaveEntrance:
                case LocationID.DarkFakeIceRodCaveTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkWorldSouthEast)
                        };
                    }
                case LocationID.DarkIceRodRockEntrance:
                case LocationID.DarkIceRodRockTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DWIceRodRock)
                        };
                    }
                case LocationID.HypeFairyCaveEntrance:
                case LocationID.HypeFairyCaveTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.HypeFairyCave)
                        };
                    }
                case LocationID.UpgradeFairy:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.LakeHyliaFairyIsland)
                        };
                    }
                case LocationID.IceRodCaveEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.IceRodCave)
                        };
                    }
                case LocationID.IceFairyCaveEntrance:
                case LocationID.IceFairyCaveTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.IceFairyCave)
                        };
                    }
                case LocationID.IcePalaceEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.IcePalaceEntrance)
                        };
                    }
                case LocationID.MiseryMireEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.MiseryMireEntrance)
                        };
                    }
                case LocationID.MireShackEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.MireCaveEntrance:
                case LocationID.MireRightShackTakeAny:
                case LocationID.MireCaveTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.MireArea)
                        };
                    }
                case LocationID.DeathMountainEntranceBack:
                case LocationID.OldManResidence:
                case LocationID.OldManBackResidence:
                case LocationID.DeathMountainExitFront:
                case LocationID.SpectacleRockLeft:
                case LocationID.SpectacleRockRight:
                case LocationID.SpectacleRockTop:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainWestBottom)
                        };
                    }
                case LocationID.SpikeCaveEntrance:
                case LocationID.DarkMountainFairyEntrance:
                case LocationID.DarkMountainFairyTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkDeathMountainWestBottom)
                        };
                    }
                case LocationID.TowerOfHeraEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainWestTop)
                        };
                    }
                case LocationID.SpiralCaveBottom:
                case LocationID.EDMFairyCaveEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                case LocationID.EDMFairyCaveTakeAny:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastBottom)
                        };
                    }
                case LocationID.EDMConnectorBottom:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastBottomConnector)
                        };
                    }
                case LocationID.SpiralCaveTop:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.SpiralCave)
                        };
                    }
                case LocationID.MimicCaveEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.MimicCave)
                        };
                    }
                case LocationID.EDMConnectorTop:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastTopConnector)
                        };
                    }
                case LocationID.ParadoxCaveTop:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DeathMountainEastTop)
                        };
                    }
                case LocationID.SuperBunnyCaveBottom:
                case LocationID.DeathMountainShop:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkDeathMountainEastBottom)
                        };
                    }
                case LocationID.SuperBunnyCaveTop:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DarkDeathMountainTop)
                        };
                    }
                case LocationID.HookshotCaveEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.HookshotCave)
                        };
                    }
                case LocationID.TurtleRockEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.TurtleRockFrontEntrance)
                        };
                    }
                case LocationID.GanonsTowerEntrance:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.GanonsTowerEntrance)
                        };
                    }
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.TurtleRockTunnel)
                        };
                    }
                case LocationID.TRSafetyDoor:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.TurtleRockSafetyDoor)
                        };
                    }
                case LocationID.HookshotCaveTop:
                    {
                        return new List<RequirementNodeConnection>
                        {
                            new RequirementNodeConnection(
                                RequirementNodeID.DWFloatingIsland)
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the autotracking function of the section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// The autotracking function of the section.
        /// </returns>
        private static Func<int?> GetSectionAutoTrackFunction(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 1, 4),
                                    (MemorySegmentType.Room, 520, 16)
                                });

                            if (result.HasValue)
                            {
                                return result.Value > 0 ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.Pedestal:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 128, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.LumberjackCave:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 453, 2);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.BlindsHouse when index == 0:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[4]
                                {
                                    (MemorySegmentType.Room, 570, 32),
                                    (MemorySegmentType.Room, 570, 64),
                                    (MemorySegmentType.Room, 570, 128),
                                    (MemorySegmentType.Room, 571, 1)
                                });

                            if (result.HasValue)
                            {
                                return 4 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.BlindsHouse when index == 1:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 570, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.TheWell when index == 0:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[4]
                                {
                                    (MemorySegmentType.Room, 94, 32),
                                    (MemorySegmentType.Room, 94, 64),
                                    (MemorySegmentType.Room, 94, 128),
                                    (MemorySegmentType.Room, 95, 1)
                                });

                            if (result.HasValue)
                            {
                                return 4 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.TheWell when index == 1:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 94, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.BottleVendor:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Item, 137, 2);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.ChickenHouse:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 528, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.Tavern:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 518, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.SickKid:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 0, 4);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.MagicBat:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 1, 128);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.RaceGame:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 40, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.Library:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 0, 128);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.MushroomSpot:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 1, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.ForestHideout:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 451, 2);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.CastleSecret when index == 0:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Item, 134, 1);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.CastleSecret when index == 1:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 170, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.WitchsHut:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 1, 32);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.SahasrahlasHut when index == 0:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[3]
                                {
                                    (MemorySegmentType.Room, 522, 16),
                                    (MemorySegmentType.Room, 522, 32),
                                    (MemorySegmentType.Room, 522, 64)
                                });

                            if (result.HasValue)
                            {
                                return 3 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.SahasrahlasHut when index == 1:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 0, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.BonkRocks:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 584, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.KingsTomb:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 550, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.AginahsCave:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 532, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.GroveDiggingSpot:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 42, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.Dam when index == 0:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 534, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.Dam when index == 1:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 59, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.MiniMoldormCave:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[5]
                                {
                                    (MemorySegmentType.Room, 582, 16),
                                    (MemorySegmentType.Room, 582, 32),
                                    (MemorySegmentType.Room, 582, 64),
                                    (MemorySegmentType.Room, 582, 128),
                                    (MemorySegmentType.Room, 583, 4)
                                });

                            if (result.HasValue)
                            {
                                return 5 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.IceRodCave:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 576, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.Hobo:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Item, 137, 1);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.PyramidLedge:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 91, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.FatFairy:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 556, 16),
                                    (MemorySegmentType.Room, 556, 32)
                                });

                            if (result.HasValue)
                            {
                                return 2 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.HauntedGrove:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 0, 8);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.HypeCave:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[5]
                                {
                                    (MemorySegmentType.Room, 572, 16),
                                    (MemorySegmentType.Room, 572, 32),
                                    (MemorySegmentType.Room, 572, 64),
                                    (MemorySegmentType.Room, 572, 128),
                                    (MemorySegmentType.Room, 573, 4)
                                });

                            if (result.HasValue)
                            {
                                return 5 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.BombosTablet:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 1, 2);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.SouthOfGrove:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 567, 4);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.DiggingGame:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 104, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.WaterfallFairy:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 552, 16),
                                    (MemorySegmentType.Room, 552, 32)
                                });

                            if (result.HasValue)
                            {
                                return 2 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.ZoraArea when index == 0:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 129, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.ZoraArea when index == 1:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 0, 2);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.Catfish:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 0, 32);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.GraveyardLedge:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 567, 2);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.DesertLedge:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 48, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.CShapedHouse:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 568, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.TreasureGame:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 525, 4);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.BombableShack:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 524, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.Blacksmith:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 1, 4);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.PurpleChest:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Item, 137, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.HammerPegs:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 591, 4);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.BumperCave:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 74, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.LakeHyliaIsland:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 53, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.MireShack:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 538, 16),
                                    (MemorySegmentType.Room, 538, 32)
                                });

                            if (result.HasValue)
                            {
                                return 2 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.CheckerboardCave:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 589, 2);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.OldMan:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 0, 1);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.SpectacleRock when index == 0:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 3, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.SpectacleRock when index == 1:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 469, 4);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.EtherTablet:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.NPCItem, 1, 1);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.SpikeCave:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 558, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.SpiralCave:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 508, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.ParadoxCave when index == 0:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 510, 16),
                                    (MemorySegmentType.Room, 510, 32)
                                });

                            if (result.HasValue)
                            {
                                return 2 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.ParadoxCave when index == 1:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[5]
                                {
                                    (MemorySegmentType.Room, 478, 16),
                                    (MemorySegmentType.Room, 478, 32),
                                    (MemorySegmentType.Room, 478, 64),
                                    (MemorySegmentType.Room, 478, 128),
                                    (MemorySegmentType.Room, 479, 1)
                                });

                            if (result.HasValue)
                            {
                                return 5 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.SuperBunnyCave:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[2]
                                {
                                    (MemorySegmentType.Room, 496, 16),
                                    (MemorySegmentType.Room, 496, 32)
                                });

                            if (result.HasValue)
                            {
                                return 2 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.HookshotCave when index == 0:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 120, 128);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.HookshotCave when index == 1:
                    {
                        return () =>
                        {
                            int? result = AutoTracker.Instance.CheckMemoryFlagArray(
                                new (MemorySegmentType, int, byte)[3]
                                {
                                    (MemorySegmentType.Room, 120, 16),
                                    (MemorySegmentType.Room, 120, 32),
                                    (MemorySegmentType.Room, 120, 64)
                                });

                            if (result.HasValue)
                            {
                                return 3 - result.Value;
                            }

                            return null;
                        };
                    }
                case LocationID.FloatingIsland:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.OverworldEvent, 5, 64);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.MimicCave:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 536, 16);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.AgahnimTower:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 133, 2);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.EasternPalace:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 401, 8);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.DesertPalace:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 103, 8);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.TowerOfHera:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 15, 8);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 181, 8);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.SwampPalace:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 13, 8);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.SkullWoods:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 83, 8);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.ThievesTown:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 345, 8);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.IcePalace:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 445, 8);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.MiseryMire:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 289, 8);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.TurtleRock:
                    {
                        return () =>
                        {
                            bool? result = AutoTracker.Instance.CheckMemoryFlag(
                                MemorySegmentType.Room, 329, 8);

                            if (result.HasValue)
                            {
                                return result.Value ? 0 : 1;
                            }

                            return null;
                        };
                    }
                case LocationID.GanonsTower:
                    {
                        return () => null;
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a list of memory addresses to subscribe to for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A list of memory addresses.
        /// </returns>
        private static List<(MemorySegmentType, int)> GetSectionMemoryAddresses(
            LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 1),
                            (MemorySegmentType.Room, 520)
                        };
                    }
                case LocationID.Pedestal:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 128)
                        };
                    }
                case LocationID.LumberjackCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 453)
                        };
                    }
                case LocationID.BlindsHouse when index == 0:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 570),
                            (MemorySegmentType.Room, 571)
                        };
                    }
                case LocationID.BlindsHouse when index == 1:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 570)
                        };
                    }
                case LocationID.TheWell when index == 0:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 94),
                            (MemorySegmentType.Room, 95)
                        };
                    }
                case LocationID.TheWell when index == 1:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 94)
                        };
                    }
                case LocationID.BottleVendor:
                case LocationID.Hobo:
                case LocationID.PurpleChest:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Item, 137)
                        };
                    }
                case LocationID.ChickenHouse:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 528)
                        };
                    }
                case LocationID.Tavern:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 518)
                        };
                    }
                case LocationID.SickKid:
                case LocationID.Library:
                case LocationID.SahasrahlasHut when index == 1:
                case LocationID.HauntedGrove:
                case LocationID.ZoraArea when index == 1:
                case LocationID.Catfish:
                case LocationID.OldMan:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.NPCItem, 0)
                        };
                    }
                case LocationID.MagicBat:
                case LocationID.MushroomSpot:
                case LocationID.WitchsHut:
                case LocationID.BombosTablet:
                case LocationID.Blacksmith:
                case LocationID.EtherTablet:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.NPCItem, 1)
                        };
                    }
                case LocationID.RaceGame:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 40)
                        };
                    }
                case LocationID.ForestHideout:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 451)
                        };
                    }
                case LocationID.CastleSecret when index == 0:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Item, 134)
                        };
                    }
                case LocationID.CastleSecret when index == 1:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 170)
                        };
                    }
                case LocationID.SahasrahlasHut when index == 0:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 522)
                        };
                    }
                case LocationID.BonkRocks:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 584)
                        };
                    }
                case LocationID.KingsTomb:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 550)
                        };
                    }
                case LocationID.AginahsCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 532)
                        };
                    }
                case LocationID.GroveDiggingSpot:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 42)
                        };
                    }
                case LocationID.Dam when index == 0:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 534)
                        };
                    }
                case LocationID.Dam when index == 1:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 59)
                        };
                    }
                case LocationID.MiniMoldormCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 582),
                            (MemorySegmentType.Room, 583)
                        };
                    }
                case LocationID.IceRodCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 576)
                        };
                    }
                case LocationID.PyramidLedge:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 91)
                        };
                    }
                case LocationID.FatFairy:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 556)
                        };
                    }
                case LocationID.HypeCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 572),
                            (MemorySegmentType.Room, 573)
                        };
                    }
                case LocationID.SouthOfGrove:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 567)
                        };
                    }
                case LocationID.DiggingGame:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 104)
                        };
                    }
                case LocationID.WaterfallFairy:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 552)
                        };
                    }
                case LocationID.ZoraArea when index == 0:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 129)
                        };
                    }
                case LocationID.GraveyardLedge:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 567)
                        };
                    }
                case LocationID.DesertLedge:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 48)
                        };
                    }
                case LocationID.CShapedHouse:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 568)
                        };
                    }
                case LocationID.TreasureGame:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 525)
                        };
                    }
                case LocationID.BombableShack:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 524)
                        };
                    }
                case LocationID.HammerPegs:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 591)
                        };
                    }
                case LocationID.BumperCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 74)
                        };
                    }
                case LocationID.LakeHyliaIsland:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 53)
                        };
                    }
                case LocationID.MireShack:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 538)
                        };
                    }
                case LocationID.CheckerboardCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 589)
                        };
                    }
                case LocationID.SpectacleRock when index == 0:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 3)
                        };
                    }
                case LocationID.SpectacleRock when index == 1:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 469)
                        };
                    }
                case LocationID.SpikeCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 558)
                        };
                    }
                case LocationID.SpiralCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 508)
                        };
                    }
                case LocationID.ParadoxCave when index == 0:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 510)
                        };
                    }
                case LocationID.ParadoxCave when index == 1:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 478),
                            (MemorySegmentType.Room, 479)
                        };
                    }
                case LocationID.SuperBunnyCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 496)
                        };
                    }
                case LocationID.HookshotCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 120)
                        };
                    }
                case LocationID.FloatingIsland:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.OverworldEvent, 5)
                        };
                    }
                case LocationID.MimicCave:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 536)
                        };
                    }
                case LocationID.AgahnimTower:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 133)
                        };
                    }
                case LocationID.EasternPalace:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 401)
                        };
                    }
                case LocationID.DesertPalace:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 103)
                        };
                    }
                case LocationID.TowerOfHera:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 15)
                        };
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 181)
                        };
                    }
                case LocationID.SwampPalace:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 13)
                        };
                    }
                case LocationID.SkullWoods:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 83)
                        };
                    }
                case LocationID.ThievesTown:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 345)
                        };
                    }
                case LocationID.IcePalace:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 445)
                        };
                    }
                case LocationID.MiseryMire:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 289)
                        };
                    }
                case LocationID.TurtleRock:
                    {
                        return new List<(MemorySegmentType, int)>
                        {
                            (MemorySegmentType.Room, 329)
                        };
                    }
                case LocationID.GanonsTower:
                    {
                        return new List<(MemorySegmentType, int)>(0);
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the requirement for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// The requirement.
        /// </returns>
        private static IRequirement GetSectionRequirement(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.Dam when index == 0:
                case LocationID.SpectacleRock when index == 1:
                    {
                        return RequirementDictionary.Instance[RequirementType.EntranceShuffleOff];
                    }
                case LocationID.AgahnimTower when index == 0:
                    {
                        return RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn];
                    }
                case LocationID.GanonsTower when index > 0 && index < 4:
                    {
                        return RequirementDictionary.Instance[RequirementType.BossShuffleOn];
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        /// <summary>
        /// Returns a new item section instance.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new item section instance.
        /// </returns>
        private static IItemSection GetItemSection(LocationID id, int index = 0)
        {
            return new ItemSection(
                GetSectionName(id, index), GetItemSectionTotal(id, index),
                GetSectionConnections(id, index), GetSectionAutoTrackFunction(id, index),
                GetSectionMemoryAddresses(id, index), GetSectionRequirement(id, index));
        }

        /// <summary>
        /// Returns a new markable item section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new markable item section.
        /// </returns>
        private static ISection GetMarkableItemSection(LocationID id, int index = 0)
        {
            return new MarkableItemSection(GetItemSection(id, index));
        }

        /// <summary>
        /// Returns a new dungeon item section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new dungeon item section.
        /// </returns>
        private static IDungeonItemSection GetDungeonItemSection(
            IDungeon dungeon, LocationID id, int index = 0)
        {
            return new DungeonItemSection(dungeon, GetSectionRequirement(id, index));
        }

        /// <summary>
        /// Returns a new markable dungeon item section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new markable dungeon item section.
        /// </returns>
        private static ISection GetMarkableDungeonItemSection(
            IDungeon dungeon, LocationID id, int index = 0)
        {
            return new MarkableDungeonItemSection(
                GetDungeonItemSection(dungeon, id, index));
        }

        /// <summary>
        /// Returns the boss placement for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// The boss placement.
        /// </returns>
        private static IBossPlacement GetSectionBossPlacement(LocationID id, int index = 0)
        {
            switch (id)
            {
                case LocationID.AgahnimTower:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.ATBoss];
                    }
                case LocationID.EasternPalace:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.EPBoss];
                    }
                case LocationID.DesertPalace:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.DPBoss];
                    }
                case LocationID.TowerOfHera:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.ToHBoss];
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.PoDBoss];
                    }
                case LocationID.SwampPalace:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.SPBoss];
                    }
                case LocationID.SkullWoods:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.SWBoss];
                    }
                case LocationID.ThievesTown:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.TTBoss];
                    }
                case LocationID.IcePalace:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.IPBoss];
                    }
                case LocationID.MiseryMire:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.MMBoss];
                    }
                case LocationID.TurtleRock:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.TRBoss];
                    }
                case LocationID.GanonsTower when index == 1:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.GTBoss1];
                    }
                case LocationID.GanonsTower when index == 2:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.GTBoss2];
                    }
                case LocationID.GanonsTower when index == 3:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.GTBoss3];
                    }
                case LocationID.GanonsTower when index == 4:
                    {
                        return BossPlacementDictionary.Instance[BossPlacementID.GTFinalBoss];
                    }
                case LocationID.GanonsTower:
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns the prize placement for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// The prize placement.
        /// </returns>
        private static IPrizePlacement GetSectionPrizePlacement(LocationID id)
        {
            switch (id)
            {
                case LocationID.AgahnimTower:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.ATPrize];
                    }
                case LocationID.EasternPalace:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.EPPrize];
                    }
                case LocationID.DesertPalace:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.DPPrize];
                    }
                case LocationID.TowerOfHera:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.ToHPrize];
                    }
                case LocationID.PalaceOfDarkness:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.PoDPrize];
                    }
                case LocationID.SwampPalace:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.SPPrize];
                    }
                case LocationID.SkullWoods:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.SWPrize];
                    }
                case LocationID.ThievesTown:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.TTPrize];
                    }
                case LocationID.IcePalace:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.IPPrize];
                    }
                case LocationID.MiseryMire:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.MMPrize];
                    }
                case LocationID.TurtleRock:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.TRPrize];
                    }
                case LocationID.GanonsTower:
                    {
                        return PrizePlacementDictionary.Instance[PrizePlacementID.GTPrize];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a new boss section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new boss section instance.
        /// </returns>
        private static ISection GetBossSection(LocationID id, int index = 0)
        {
            return new BossSection(
                GetSectionName(id, index), GetSectionBossPlacement(id, index),
                GetSectionRequirement(id, index));
        }

        /// <summary>
        /// Returns a new prize section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="index">
        /// The section index.
        /// </param>
        /// <returns>
        /// A new prize section instance.
        /// </returns>
        private static ISection GetPrizeSection(LocationID id, int index = 0)
        {
            return new PrizeSection(
                GetSectionName(id, index), GetSectionBossPlacement(id, index),
                GetSectionPrizePlacement(id), GetSectionAutoTrackFunction(id, index),
                GetSectionMemoryAddresses(id, index), GetSectionRequirement(id, index));
        }

        /// <summary>
        /// Returns the item provided for the specified entrance section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// The item provided.
        /// </returns>
        private static IItem GetEntranceSectionItemProvided(LocationID id)
        {
            switch (id)
            {
                case LocationID.LumberjackHouseEntrance:
                case LocationID.LumberjackCaveEntrance:
                case LocationID.KakarikoFortuneTellerEntrance:
                case LocationID.WomanLeftDoor:
                case LocationID.WomanRightDoor:
                case LocationID.LeftSnitchHouseEntrance:
                case LocationID.RightSnitchHouseEntrance:
                case LocationID.BlindsHouseEntrance:
                case LocationID.TheWellEntrance:
                case LocationID.ChickenHouseEntrance:
                case LocationID.GrassHouseEntrance:
                case LocationID.TavernFront:
                case LocationID.KakarikoShop:
                case LocationID.SickKidEntrance:
                case LocationID.BlacksmithHouse:
                case LocationID.MagicBatEntrance:
                case LocationID.ChestGameEntrance:
                case LocationID.RaceHouseRight:
                case LocationID.LibraryEntrance:
                case LocationID.ForestHideoutEntrance:
                case LocationID.ForestChestGameEntrance:
                case LocationID.CastleSecretEntrance:
                case LocationID.CastleMainEntrance:
                case LocationID.DamEntrance:
                case LocationID.CentralBonkRocksEntrance:
                case LocationID.SahasrahlasHutEntrance:
                case LocationID.TreesFairyCaveEntrance:
                case LocationID.PegsFairyCaveEntrance:
                case LocationID.EasternPalaceEntrance:
                case LocationID.HoulihanHole:
                case LocationID.SanctuaryGrave:
                case LocationID.NorthBonkRocks:
                case LocationID.DesertRightEntrance:
                case LocationID.AginahsCaveEntrance:
                case LocationID.ThiefCaveEntrance:
                case LocationID.RupeeCaveEntrance:
                case LocationID.SouthOfGroveEntrance:
                case LocationID.HypeFairyCaveEntrance:
                case LocationID.FortuneTellerEntrance:
                case LocationID.LakeShop:
                case LocationID.MiniMoldormCaveEntrance:
                case LocationID.IceRodCaveEntrance:
                case LocationID.IceBeeCaveEntrance:
                case LocationID.IceFairyCaveEntrance:
                case LocationID.LinksHouseEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.LightWorldAccess];
                    }
                case LocationID.DeathMountainEntryCave:
                    {
                        return ItemDictionary.Instance[ItemType.DeathMountainEntryAccess];
                    }
                case LocationID.DeathMountainExitCave:
                    {
                        return ItemDictionary.Instance[ItemType.DeathMountainExitAccess];
                    }
                case LocationID.BombHutEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.BombHutAccess];
                    }
                case LocationID.RaceHouseLeft:
                    {
                        return ItemDictionary.Instance[ItemType.RaceGameLedgeAccess];
                    }
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                case LocationID.CastleTowerEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.HyruleCastleTopAccess];
                    }
                case LocationID.WitchsHutEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.LWWitchAreaAccess];
                    }
                case LocationID.WaterfallFairyEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.WaterfallFairyAccess];
                    }
                case LocationID.KingsTombEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.LWKingsTombAccess];
                    }
                case LocationID.GraveyardLedgeEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.LWGraveyardLedgeAccess];
                    }
                case LocationID.DesertLeftEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.DesertLedgeAccess];
                    }
                case LocationID.DesertBackEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.DesertPalaceBackEntranceAccess];
                    }
                case LocationID.DesertFrontEntrance:
                case LocationID.GanonHole:
                    {
                        return null;
                    }
                case LocationID.SkullWoodsBack:
                case LocationID.ThievesTownEntrance:
                case LocationID.CShapedHouseEntrance:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.DarkChapelEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.BombableShackEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.DarkWorldWestAccess];
                    }
                case LocationID.HammerHouse:
                    {
                        return ItemDictionary.Instance[ItemType.HammerHouseAccess];
                    }
                case LocationID.HammerPegsEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.HammerPegsAreaAccess];
                    }
                case LocationID.BumperCaveExit:
                    {
                        return ItemDictionary.Instance[ItemType.BumperCaveTopAccess];
                    }
                case LocationID.BumperCaveEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.BumperCaveAccess];
                    }
                case LocationID.HypeCaveEntrance:
                case LocationID.SwampPalaceEntrance:
                case LocationID.DarkCentralBonkRocksEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                    {
                        return ItemDictionary.Instance[ItemType.DarkWorldSouthAccess];
                    }
                case LocationID.DarkTreesFairyCaveEntrance:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.DarkFluteSpotFiveEntrance:
                case LocationID.FatFairyEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.DarkWorldEastAccess];
                    }
                case LocationID.DarkWitchsHut:
                    {
                        return ItemDictionary.Instance[ItemType.DWWitchAreaAccess];
                    }
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.DarkFakeIceRodCaveEntrance:
                case LocationID.DarkIceRodRockEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.DarkWorldSouthEastAccess];
                    }
                case LocationID.UpgradeFairy:
                    {
                        return ItemDictionary.Instance[ItemType.LakeHyliaFairyIslandAccess];
                    }
                case LocationID.IcePalaceEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.IcePalaceAccess];
                    }
                case LocationID.MiseryMireEntrance:
                case LocationID.MireShackEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.MireCaveEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.MireAreaAccess];
                    }
                case LocationID.CheckerboardCaveEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.CheckerboardLedgeAccess];
                    }
                case LocationID.DeathMountainEntranceBack:
                case LocationID.OldManResidence:
                case LocationID.OldManBackResidence:
                case LocationID.DeathMountainExitFront:
                case LocationID.SpectacleRockLeft:
                case LocationID.SpectacleRockRight:
                case LocationID.SpectacleRockTop:
                    {
                        return ItemDictionary.Instance[ItemType.DeathMountainWestBottomAccess];
                    }
                case LocationID.SpikeCaveEntrance:
                case LocationID.DarkMountainFairyEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.DarkDeathMountainWestBottomAccess];
                    }
                case LocationID.TowerOfHeraEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.DeathMountainWestTopAccess];
                    }
                case LocationID.SpiralCaveBottom:
                case LocationID.EDMFairyCaveEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                    {
                        return ItemDictionary.Instance[ItemType.DeathMountainEastBottomAccess];
                    }
                case LocationID.EDMConnectorBottom:
                    {
                        return ItemDictionary.Instance[ItemType.DeathMountainEastBottomConnectorAccess];
                    }
                case LocationID.SpiralCaveTop:
                    {
                        return ItemDictionary.Instance[ItemType.SpiralCaveAccess];
                    }
                case LocationID.MimicCaveEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.MimicCaveAccess];
                    }
                case LocationID.EDMConnectorTop:
                    {
                        return ItemDictionary.Instance[ItemType.DeathMountainEastTopConnectorAccess];
                    }
                case LocationID.ParadoxCaveTop:
                    {
                        return ItemDictionary.Instance[ItemType.DeathMountainEastTopAccess];
                    }
                case LocationID.SuperBunnyCaveBottom:
                case LocationID.DeathMountainShop:
                    {
                        return ItemDictionary.Instance[ItemType.DarkDeathMountainEastBottomAccess];
                    }
                case LocationID.SuperBunnyCaveTop:
                case LocationID.HookshotCaveEntrance:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                    {
                        return ItemDictionary.Instance[ItemType.DarkDeathMountainTopAccess];
                    }
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                    {
                        return ItemDictionary.Instance[ItemType.TurtleRockTunnelAccess];
                    }
                case LocationID.TRSafetyDoor:
                    {
                        return ItemDictionary.Instance[ItemType.TurtleRockSafetyDoorAccess];
                    }
                case LocationID.HookshotCaveTop:
                    {
                        return ItemDictionary.Instance[ItemType.DWFloatingIslandAccess];
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Returns a new entrance section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new entrance section instance.
        /// </returns>
        private static ISection GetEntranceSection(LocationID id)
        {
            return new EntranceSection(
                GetSectionName(id), GetEntranceSectionItemProvided(id), GetSectionConnections(id),
                GetSectionRequirement(id));
        }

        /// <summary>
        /// Returns a new take any section instance for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new take any section instance.
        /// </returns>
        private static ISection GetTakeAnySection(LocationID id)
        {
            return new TakeAnySection(GetSectionConnections(id), GetSectionRequirement(id));
        }

        /// <summary>
        /// Returns a list of sections for the specified location.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <param name="location">
        /// The location parent class.
        /// </param>
        /// <returns>
        /// A list of sections.
        /// </returns>
        internal static List<ISection> GetSections(LocationID id, ILocation location)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                case LocationID.BottleVendor:
                case LocationID.ChickenHouse:
                case LocationID.Tavern:
                case LocationID.SickKid:
                case LocationID.MagicBat:
                case LocationID.WitchsHut:
                case LocationID.BonkRocks:
                case LocationID.KingsTomb:
                case LocationID.AginahsCave:
                case LocationID.GroveDiggingSpot:
                case LocationID.MiniMoldormCave:
                case LocationID.IceRodCave:
                case LocationID.Hobo:
                case LocationID.PyramidLedge:
                case LocationID.FatFairy:
                case LocationID.HauntedGrove:
                case LocationID.HypeCave:
                case LocationID.SouthOfGrove:
                case LocationID.DiggingGame:
                case LocationID.WaterfallFairy:
                case LocationID.Catfish:
                case LocationID.GraveyardLedge:
                case LocationID.CShapedHouse:
                case LocationID.TreasureGame:
                case LocationID.BombableShack:
                case LocationID.Blacksmith:
                case LocationID.PurpleChest:
                case LocationID.HammerPegs:
                case LocationID.MireShack:
                case LocationID.CheckerboardCave:
                case LocationID.OldMan:
                case LocationID.SpikeCave:
                case LocationID.SpiralCave:
                case LocationID.SuperBunnyCave:
                case LocationID.MimicCave:
                    {
                        return new List<ISection>
                        {
                            GetItemSection(id)
                        };
                    }
                case LocationID.Pedestal:
                case LocationID.LumberjackCave:
                case LocationID.RaceGame:
                case LocationID.Library:
                case LocationID.MushroomSpot:
                case LocationID.ForestHideout:
                case LocationID.BombosTablet:
                case LocationID.DesertLedge:
                case LocationID.BumperCave:
                case LocationID.LakeHyliaIsland:
                case LocationID.EtherTablet:
                case LocationID.FloatingIsland:
                    {
                        return new List<ISection>
                        {
                            GetMarkableItemSection(id)
                        };
                    }
                case LocationID.BlindsHouse:
                case LocationID.TheWell:
                case LocationID.CastleSecret:
                case LocationID.SahasrahlasHut:
                case LocationID.Dam:
                case LocationID.ParadoxCave:
                case LocationID.HookshotCave:
                    {
                        return new List<ISection>
                        {
                            GetItemSection(id, 0),
                            GetItemSection(id, 1)
                        };
                    }
                case LocationID.ZoraArea:
                case LocationID.SpectacleRock:
                    {
                        return new List<ISection>
                        {
                            GetMarkableItemSection(id, 0),
                            GetItemSection(id, 1)
                        };
                    }
                case LocationID.HyruleCastle:
                    {
                        if (location is IDungeon dungeon)
                        {
                            return new List<ISection>
                            {
                                GetDungeonItemSection(dungeon, id, 0)
                            };
                        }
                    }
                    break;
                case LocationID.AgahnimTower:
                case LocationID.EasternPalace:
                case LocationID.TowerOfHera:
                case LocationID.PalaceOfDarkness:
                case LocationID.SwampPalace:
                case LocationID.SkullWoods:
                case LocationID.ThievesTown:
                case LocationID.IcePalace:
                case LocationID.MiseryMire:
                case LocationID.TurtleRock:
                    {
                        if (location is IDungeon dungeon)
                        {
                            return new List<ISection>
                            {
                                GetDungeonItemSection(dungeon, id, 0),
                                GetPrizeSection(id, 1)
                            };
                        }
                    }
                    break;
                case LocationID.DesertPalace:
                    {
                        if (location is IDungeon dungeon)
                        {
                            return new List<ISection>
                            {
                                GetMarkableDungeonItemSection(dungeon, id, 0),
                                GetPrizeSection(id, 1)
                            };
                        }
                    }
                    break;
                case LocationID.GanonsTower:
                    {
                        if (location is IDungeon dungeon)
                        {
                            return new List<ISection>
                            {
                                GetMarkableDungeonItemSection(dungeon, id, 0),
                                GetBossSection(id, 1),
                                GetBossSection(id, 2),
                                GetBossSection(id, 3),
                                GetPrizeSection(id, 4)
                            };
                        }
                    }
                    break;
                case LocationID.LumberjackHouseEntrance:
                case LocationID.LumberjackCaveEntrance:
                case LocationID.DeathMountainEntryCave:
                case LocationID.DeathMountainExitCave:
                case LocationID.KakarikoFortuneTellerEntrance:
                case LocationID.WomanLeftDoor:
                case LocationID.WomanRightDoor:
                case LocationID.LeftSnitchHouseEntrance:
                case LocationID.RightSnitchHouseEntrance:
                case LocationID.BlindsHouseEntrance:
                case LocationID.TheWellEntrance:
                case LocationID.ChickenHouseEntrance:
                case LocationID.GrassHouseEntrance:
                case LocationID.TavernFront:
                case LocationID.KakarikoShop:
                case LocationID.BombHutEntrance:
                case LocationID.SickKidEntrance:
                case LocationID.BlacksmithHouse:
                case LocationID.MagicBatEntrance:
                case LocationID.ChestGameEntrance:
                case LocationID.RaceHouseLeft:
                case LocationID.RaceHouseRight:
                case LocationID.LibraryEntrance:
                case LocationID.ForestHideoutEntrance:
                case LocationID.ForestChestGameEntrance:
                case LocationID.CastleSecretEntrance:
                case LocationID.CastleMainEntrance:
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                case LocationID.CastleTowerEntrance:
                case LocationID.DamEntrance:
                case LocationID.CentralBonkRocksEntrance:
                case LocationID.WitchsHutEntrance:
                case LocationID.WaterfallFairyEntrance:
                case LocationID.SahasrahlasHutEntrance:
                case LocationID.TreesFairyCaveEntrance:
                case LocationID.PegsFairyCaveEntrance:
                case LocationID.EasternPalaceEntrance:
                case LocationID.HoulihanHole:
                case LocationID.SanctuaryGrave:
                case LocationID.NorthBonkRocks:
                case LocationID.KingsTombEntrance:
                case LocationID.GraveyardLedgeEntrance:
                case LocationID.DesertLeftEntrance:
                case LocationID.DesertBackEntrance:
                case LocationID.DesertRightEntrance:
                case LocationID.DesertFrontEntrance:
                case LocationID.AginahsCaveEntrance:
                case LocationID.ThiefCaveEntrance:
                case LocationID.RupeeCaveEntrance:
                case LocationID.SkullWoodsBack:
                case LocationID.ThievesTownEntrance:
                case LocationID.CShapedHouseEntrance:
                case LocationID.HammerHouse:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.DarkChapelEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.BombableShackEntrance:
                case LocationID.HammerPegsEntrance:
                case LocationID.BumperCaveExit:
                case LocationID.BumperCaveEntrance:
                case LocationID.HypeCaveEntrance:
                case LocationID.SwampPalaceEntrance:
                case LocationID.DarkCentralBonkRocksEntrance:
                case LocationID.SouthOfGroveEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                case LocationID.DarkTreesFairyCaveEntrance:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.DarkWitchsHut:
                case LocationID.DarkFluteSpotFiveEntrance:
                case LocationID.FatFairyEntrance:
                case LocationID.GanonHole:
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.DarkFakeIceRodCaveEntrance:
                case LocationID.DarkIceRodRockEntrance:
                case LocationID.HypeFairyCaveEntrance:
                case LocationID.FortuneTellerEntrance:
                case LocationID.LakeShop:
                case LocationID.UpgradeFairy:
                case LocationID.MiniMoldormCaveEntrance:
                case LocationID.IceRodCaveEntrance:
                case LocationID.IceBeeCaveEntrance:
                case LocationID.IceFairyCaveEntrance:
                case LocationID.IcePalaceEntrance:
                case LocationID.MiseryMireEntrance:
                case LocationID.MireShackEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.MireCaveEntrance:
                case LocationID.CheckerboardCaveEntrance:
                case LocationID.DeathMountainEntranceBack:
                case LocationID.OldManResidence:
                case LocationID.OldManBackResidence:
                case LocationID.DeathMountainExitFront:
                case LocationID.SpectacleRockLeft:
                case LocationID.SpectacleRockRight:
                case LocationID.SpectacleRockTop:
                case LocationID.SpikeCaveEntrance:
                case LocationID.DarkMountainFairyEntrance:
                case LocationID.TowerOfHeraEntrance:
                case LocationID.SpiralCaveBottom:
                case LocationID.EDMFairyCaveEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                case LocationID.EDMConnectorBottom:
                case LocationID.SpiralCaveTop:
                case LocationID.MimicCaveEntrance:
                case LocationID.EDMConnectorTop:
                case LocationID.ParadoxCaveTop:
                case LocationID.SuperBunnyCaveBottom:
                case LocationID.DeathMountainShop:
                case LocationID.SuperBunnyCaveTop:
                case LocationID.HookshotCaveEntrance:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                case LocationID.TRSafetyDoor:
                case LocationID.HookshotCaveTop:
                case LocationID.LinksHouseEntrance:
                    {
                        return new List<ISection>
                        {
                            GetEntranceSection(id)
                        };
                    }
                case LocationID.TreesFairyCaveTakeAny:
                case LocationID.PegsFairyCaveTakeAny:
                case LocationID.KakarikoFortuneTellerTakeAny:
                case LocationID.GrassHouseTakeAny:
                case LocationID.ForestChestGameTakeAny:
                case LocationID.LumberjackHouseTakeAny:
                case LocationID.LeftSnitchHouseTakeAny:
                case LocationID.RightSnitchHouseTakeAny:
                case LocationID.BombHutTakeAny:
                case LocationID.IceFairyCaveTakeAny:
                case LocationID.RupeeCaveTakeAny:
                case LocationID.CentralBonkRocksTakeAny:
                case LocationID.ThiefCaveTakeAny:
                case LocationID.IceBeeCaveTakeAny:
                case LocationID.FortuneTellerTakeAny:
                case LocationID.HypeFairyCaveTakeAny:
                case LocationID.ChestGameTakeAny:
                case LocationID.EDMFairyCaveTakeAny:
                case LocationID.DarkChapelTakeAny:
                case LocationID.DarkVillageFortuneTellerTakeAny:
                case LocationID.DarkTreesFairyCaveTakeAny:
                case LocationID.DarkSahasrahlaTakeAny:
                case LocationID.DarkFluteSpotFiveTakeAny:
                case LocationID.ArrowGameTakeAny:
                case LocationID.DarkCentralBonkRocksTakeAny:
                case LocationID.DarkIceRodCaveTakeAny:
                case LocationID.DarkFakeIceRodCaveTakeAny:
                case LocationID.DarkIceRodRockTakeAny:
                case LocationID.DarkMountainFairyTakeAny:
                case LocationID.MireRightShackTakeAny:
                case LocationID.MireCaveTakeAny:
                    {
                        return new List<ISection>
                        {
                            GetTakeAnySection(id)
                        };
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
}
