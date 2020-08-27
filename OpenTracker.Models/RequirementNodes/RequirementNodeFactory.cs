using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This is the class for creating requirement node classes.
    /// </summary>
    internal static class RequirementNodeFactory
    {
        /// <summary>
        /// Returns a list of connections to the specified requirement node ID.
        /// </summary>
        /// <param name="id">
        /// The requirement node ID.
        /// </param>
        /// <returns>
        /// A list of connections.
        /// </returns>
        private static List<RequirementNodeConnection> GetRequirementNodeConnections(
            RequirementNodeID id)
        {
            return id switch
            {
                RequirementNodeID.Inaccessible => null,
                RequirementNodeID.Start => null,
                RequirementNodeID.EntranceShuffle => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn])
                },
                RequirementNodeID.NonEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                },
                RequirementNodeID.NonEntranceInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.NonEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.LightWorld => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEntry),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainExit),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWKakarikoPortalNotBunny,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Hammer],
                            RequirementDictionary.Instance[RequirementType.Gloves2]
                        })),
                    new RequirementNodeConnection(RequirementNodeID.DesertLedge),
                    new RequirementNodeConnection(
                        RequirementNodeID.GrassHouse,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(
                        RequirementNodeID.BombHut,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(RequirementNodeID.RaceGameLedge),
                    new RequirementNodeConnection(RequirementNodeID.SouthOfGroveLedge),
                    new RequirementNodeConnection(RequirementNodeID.CheckerboardLedge),
                    new RequirementNodeConnection(RequirementNodeID.BombosTabletLedge),
                    new RequirementNodeConnection(RequirementNodeID.LWMirePortal),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWSouthPortalNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer]),
                    new RequirementNodeConnection(RequirementNodeID.LWWitchAreaNotBunny),
                    new RequirementNodeConnection(RequirementNodeID.LWEastPortalNotBunny),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWestMirror),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthInverted,
                        RequirementDictionary.Instance[RequirementType.Aga1]),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthMirror),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldEastMirror),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthEast,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.LightWorldInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.LightWorldNonInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.LightWorldInspect => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.Inspect])
                },
                RequirementNodeID.LightWorldMirror => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWMirror])
                },
                RequirementNodeID.LightWorldNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.LightWorldNotBunnyOrDungeonRevive =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(RequirementNodeID.LightWorldNotBunny),
                        new RequirementNodeConnection(
                            RequirementNodeID.LightWorld,
                            RequirementDictionary.Instance[RequirementType.DungeonRevive])
                    },
                RequirementNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(RequirementNodeID.LightWorldNotBunny),
                        new RequirementNodeConnection(
                            RequirementNodeID.LightWorld,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole])
                    },
                RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(RequirementNodeID.LightWorldNotBunny),
                        new RequirementNodeConnection(
                            RequirementNodeID.LightWorld,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyMirror])
                    },
                RequirementNodeID.LightWorldDash => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunny,
                        RequirementDictionary.Instance[RequirementType.Boots])
                },
                RequirementNodeID.LightWorldHammer => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer])
                },
                RequirementNodeID.LightWorldLift1 => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves1])
                },
                RequirementNodeID.GroveDiggingSpot => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunny,
                        RequirementDictionary.Instance[RequirementType.Shovel])
                },
                RequirementNodeID.Flute => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunny,
                        RequirementDictionary.Instance[RequirementType.Flute])
                },
                RequirementNodeID.FluteInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Flute,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.FluteNonInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Flute,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.Pedestal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.Pedestal])
                },
                RequirementNodeID.LumberjackCaveHole => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldDash,
                        RequirementDictionary.Instance[RequirementType.Aga1])
                },
                RequirementNodeID.LumberjackCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LumberjackCaveHole),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInspect)
                },
                RequirementNodeID.DeathMountainEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldLift1),
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCaveEntry,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DeathMountainEntryNonEntrance =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainEntry,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                    },
                RequirementNodeID.DeathMountainEntryCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEntryNonEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestBottomNonEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCaveEntryNonEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainWestBottomNonEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.DeathMountainEntryCaveDark => new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainEntryCave,
                            RequirementDictionary.Instance[
                                RequirementType.DarkRoomDeathMountainEntry])
                    },
                RequirementNodeID.DeathMountainExit => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainExitCaveDark,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCaveBack,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCaveTop,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DeathMountainExitNonEntrance =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainExit,
                            RequirementDictionary.Instance[
                                RequirementType.EntranceShuffleOff])
                    },
                RequirementNodeID.DeathMountainExitCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainExitNonEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestBottomNonEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.DeathMountainExitCaveDark => new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainExitCave,
                            RequirementDictionary.Instance[
                                RequirementType.DarkRoomDeathMountainExit])
                    },
                RequirementNodeID.ForestHideout => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldNotBunny),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInspect)
                },
                RequirementNodeID.LWKakarikoPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves2]),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldHammer),
                    new RequirementNodeConnection(RequirementNodeID.DWKakarikoPortalInverted,
                        RequirementDictionary.Instance[RequirementType.Gloves1])
                },
                RequirementNodeID.LWKakarikoPortalNonInverted =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.LWKakarikoPortal,
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                    },
                RequirementNodeID.LWKakarikoPortalNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWKakarikoPortal,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.SickKid => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.Bottle])
                },
                RequirementNodeID.GrassHouse => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldNotBunny)
                },
                RequirementNodeID.BombHut => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldNotBunny)
                },
                RequirementNodeID.MagicBatLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldHammer),
                    new RequirementNodeConnection(
                        RequirementNodeID.HammerPegsArea,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.MagicBat => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.MagicBatLedge,
                        RequirementDictionary.Instance[RequirementType.Powder])
                },
                RequirementNodeID.MagicBatEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.MagicBatLedge),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInspect)
                },
                RequirementNodeID.Library => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldDash),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInspect)
                },
                RequirementNodeID.RaceGameLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunny,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff]),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthMirror)
                },
                RequirementNodeID.RaceGame => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.RaceGameLedge,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInspect)
                },
                RequirementNodeID.SouthOfGroveLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInverted),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthMirror)
                },
                RequirementNodeID.SouthOfGrove => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.SouthOfGroveLedge,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(
                        RequirementNodeID.SouthOfGroveLedge,
                        RequirementDictionary.Instance[RequirementType.SuperBunnyMirror])
                },
                RequirementNodeID.DesertLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DesertBackNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves1]),
                    new RequirementNodeConnection(RequirementNodeID.MireAreaMirror),
                    new RequirementNodeConnection(
                        RequirementNodeID.DPFrontEntry,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                },
                RequirementNodeID.DesertLedgeItem => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInspect),
                    new RequirementNodeConnection(RequirementNodeID.DesertLedge)
                },
                RequirementNodeID.DesertLedgeNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DesertLedge,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.DesertBack => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DesertLedgeNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves1]),
                    new RequirementNodeConnection(RequirementNodeID.MireAreaMirror)
                },
                RequirementNodeID.DesertBackNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DesertBack,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.CheckerboardLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInverted),
                    new RequirementNodeConnection(RequirementNodeID.MireAreaMirror)
                },
                RequirementNodeID.CheckerboardLedgeNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.CheckerboardLedge,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.CheckerboardCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.CheckerboardLedgeNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves1])
                },
                RequirementNodeID.DesertPalaceFrontEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.Book]),
                    new RequirementNodeConnection(RequirementNodeID.MireAreaMirror)
                },
                RequirementNodeID.BombosTabletLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInverted),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthMirror)
                },
                RequirementNodeID.BombosTablet => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BombosTabletLedge,
                        RequirementDictionary.Instance[RequirementType.Tablet])
                },
                RequirementNodeID.LWMirePortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.FluteNonInverted),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWMirePortalInverted,
                        RequirementDictionary.Instance[RequirementType.Gloves2])
                },
                RequirementNodeID.LWMirePortalNonInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWMirePortal,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.LWGraveyard => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldNotBunny),
                    new RequirementNodeConnection(RequirementNodeID.LWGraveyardLedge),
                    new RequirementNodeConnection(
                        RequirementNodeID.KingsTombNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves2]),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWestMirror),
                    new RequirementNodeConnection(RequirementNodeID.DWGraveyardMirror)
                },
                RequirementNodeID.LWGraveyardNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWGraveyard,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.LWGraveyardLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWGraveyardNotBunny,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWGraveyardLedge,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.EscapeGrave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWGraveyardNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves1])
                },
                RequirementNodeID.SanctuaryGraveEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EscapeGrave),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInspect)
                },
                RequirementNodeID.KingsTomb => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWGraveyardNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves2]),
                    new RequirementNodeConnection(RequirementNodeID.DWGraveyardMirror)
                },
                RequirementNodeID.KingsTombNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.KingsTomb,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.KingsTombGrave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.KingsTombNotBunny,
                        RequirementDictionary.Instance[RequirementType.Boots])
                },
                RequirementNodeID.HoulihanHoleEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldNotBunny),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInspect)
                },
                RequirementNodeID.HyruleCastleTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff]),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldEastMirror)
                },
                RequirementNodeID.HyruleCastleTopInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HyruleCastleTop,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.HyruleCastleTopNonInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HyruleCastleTop,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.AgahnimTowerEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HyruleCastleTopInverted,
                        RequirementDictionary.Instance[RequirementType.GTCrystal]),
                    new RequirementNodeConnection(
                        RequirementNodeID.HyruleCastleTopNonInverted,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Cape],
                            RequirementDictionary.Instance[RequirementType.Sword2]
                        }))
                },
                RequirementNodeID.GanonHole => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HyruleCastleTopInverted,
                        RequirementDictionary.Instance[RequirementType.Aga2]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEastNonInverted,
                        RequirementDictionary.Instance[RequirementType.Aga2]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldInspect,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.LWSouthPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldHammer),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWSouthPortalInverted,
                        RequirementDictionary.Instance[RequirementType.Gloves1])
                },
                RequirementNodeID.LWSouthPortalNonInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWSouthPortal,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.LWSouthPortalNotBunny=> new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWSouthPortal,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.ZoraArea => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaFlippers),
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaFakeFlippers),
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaWaterWalk),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWWitchAreaNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves1]),
                    new RequirementNodeConnection(
                        RequirementNodeID.CatfishArea,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.ZoraLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.ZoraArea,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Flippers],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion],
                            RequirementDictionary.Instance[RequirementType.WaterWalk],
                            RequirementDictionary.Instance[RequirementType.Inspect]
                        }))
                },
                RequirementNodeID.WaterfallFairy => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaFlippers),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWLakeHyliaFakeFlippers,
                        RequirementDictionary.Instance[RequirementType.MoonPearl]),
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaWaterWalk)
                },
                RequirementNodeID.WaterfallFairyNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.WaterfallFairy,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.LWWitchArea => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldNotBunny),
                    new RequirementNodeConnection(
                        RequirementNodeID.ZoraArea,
                        RequirementDictionary.Instance[RequirementType.Gloves1]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWWitchArea,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.LWWitchAreaNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWWitchArea,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.WitchsHut => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWWitchArea,
                        RequirementDictionary.Instance[RequirementType.Mushroom])
                },
                RequirementNodeID.Sahasrahla => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.GreenPendant])
                },
                RequirementNodeID.LWEastPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldHammer),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWEastPortalInverted,
                        RequirementDictionary.Instance[RequirementType.Gloves1])
                },
                RequirementNodeID.LWEastPortalNonInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWEastPortal,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.LWEastPortalNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWEastPortal,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.LWLakeHyliaFakeFlippers => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunny,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[
                                RequirementType.FakeFlippersFairyRevival],
                            RequirementDictionary.Instance[
                                RequirementType.FakeFlippersScreenTransition],
                            RequirementDictionary.Instance[
                                RequirementType.FakeFlippersSplashDeletion]
                        }))
                },
                RequirementNodeID.LWLakeHyliaFlippers => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunny,
                        RequirementDictionary.Instance[RequirementType.Flippers]),
                    new RequirementNodeConnection(
                        RequirementNodeID.WaterfallFairyNotBunny,
                        RequirementDictionary.Instance[RequirementType.Flippers])
                },
                RequirementNodeID.LWLakeHyliaWaterWalk => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunny,
                        RequirementDictionary.Instance[RequirementType.WaterWalk]),
                    new RequirementNodeConnection(
                        RequirementNodeID.WaterfallFairyNotBunny,
                        RequirementDictionary.Instance[RequirementType.WaterWalkFromWaterfallCave])
                },
                RequirementNodeID.Hobo => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaFlippers),
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaFakeFlippers),
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaWaterWalk)
                },
                RequirementNodeID.LakeHyliaIsland => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWLakeHyliaFlippers,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWLakeHyliaFakeFlippers,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWLakeHyliaWaterWalk,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWLakeHyliaFlippers,
                        RequirementDictionary.Instance[RequirementType.DWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWLakeHyliaWaterWalk,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.LakeHyliaIslandItem => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LakeHyliaIsland),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldInspect)
                },
                RequirementNodeID.LakeHyliaFairyIsland => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaFlippers),
                    new RequirementNodeConnection(
                        RequirementNodeID.IcePalaceIsland,
                        RequirementDictionary.Instance[RequirementType.DWMirror]),
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaFakeFlippers),
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaWaterWalk)
                },
                RequirementNodeID.LakeHyliaFairyIslandNonInverted =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.LakeHyliaFairyIsland,
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                    },
                RequirementNodeID.DeathMountainWestBottom => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.FluteNonInverted),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEntryCaveDark,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainExitCaveDark,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastBottomNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hookshot]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainWestBottomInverted)
                },
                RequirementNodeID.DeathMountainWestBottomNonEntrance =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainWestBottom,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                    },
                RequirementNodeID.DeathMountainWestBottomNotBunny =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainWestBottom,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                    },
                RequirementNodeID.SpectacleRockTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestTop,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainWestBottomMirror)
                },
                RequirementNodeID.SpectacleRockTopItem => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.SpectacleRockTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestBottom,
                        RequirementDictionary.Instance[RequirementType.Inspect])
                },
                RequirementNodeID.DeathMountainWestTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.SpectacleRockTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTopNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer]),
                    new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTopMirror)
                },
                RequirementNodeID.DeathMountainWestTopNotBunny =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainWestTop,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                    },
                RequirementNodeID.EtherTablet => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestTop,
                        RequirementDictionary.Instance[RequirementType.Tablet])
                },
                RequirementNodeID.DeathMountainEastBottom => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestBottomNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hookshot]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastBottomConnector),
                    new RequirementNodeConnection(
                        RequirementNodeID.ParadoxCave,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff]),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop),
                    new RequirementNodeConnection(RequirementNodeID.SpiralCaveLedge),
                    new RequirementNodeConnection(
                        RequirementNodeID.MimicCaveLedge,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainEastBottom,
                        RequirementDictionary.Instance[RequirementType.DWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainEastBottomInverted,
                        RequirementDictionary.Instance[RequirementType.Gloves2])
                },
                RequirementNodeID.DeathMountainEastBottomNotBunny =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainEastBottom,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                    },
                RequirementNodeID.DeathMountainEastBottomLift2 =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainEastBottomNotBunny,
                            RequirementDictionary.Instance[RequirementType.Gloves2])
                    },
                RequirementNodeID.DeathMountainEastBottomConnector =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainEastBottomNotBunny,
                            RequirementDictionary.Instance[RequirementType.Gloves2]),
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainEastTopConnector),
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkDeathMountainEastBottomConnector,
                            RequirementDictionary.Instance[RequirementType.DWMirror])
                    },
                RequirementNodeID.ParadoxCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastBottom,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTop,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                },
                RequirementNodeID.ParadoxCaveNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.ParadoxCave,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.DeathMountainEastTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestTopNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer]),
                    new RequirementNodeConnection(RequirementNodeID.ParadoxCave),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWTurtleRockTopInvertedNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer]),
                    new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTopMirror)
                },
                RequirementNodeID.DeathMountainEastTopInverted =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainEastTop,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                    },
                RequirementNodeID.DeathMountainEastTopNotBunny =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainEastTop,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                    },
                RequirementNodeID.DeathMountainEastTopConnector =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop),
                        new RequirementNodeConnection(
                            RequirementNodeID.TurtleRockSafetyDoor,
                            RequirementDictionary.Instance[RequirementType.DWMirror])
                    },
                RequirementNodeID.SpiralCaveLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop),
                    new RequirementNodeConnection(RequirementNodeID.TurtleRockTunnelMirror)
                },
                RequirementNodeID.SpiralCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.SpiralCaveLedge,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(
                        RequirementNodeID.SpiralCaveLedge,
                        RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole])
                },
                RequirementNodeID.MimicCaveLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTopInverted),
                    new RequirementNodeConnection(RequirementNodeID.TurtleRockTunnelMirror)
                },
                RequirementNodeID.MimicCaveLedgeNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.MimicCaveLedge,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.MimicCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.MimicCaveLedgeNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer])
                },
                RequirementNodeID.LWFloatingIsland => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTopInverted),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWFloatingIsland,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.FloatingIsland => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTop,
                        RequirementDictionary.Instance[RequirementType.Inspect]),
                    new RequirementNodeConnection(RequirementNodeID.LWFloatingIsland)
                },
                RequirementNodeID.LWTurtleRockTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTopNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves2]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWTurtleRockTopNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves2])
                },
                RequirementNodeID.LWTurtleRockTopInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWTurtleRockTop,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.LWTurtleRockTopInvertedNotBunny =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.LWTurtleRockTopInverted,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                    },
                RequirementNodeID.LWTurtleRockTopNonInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWTurtleRockTop,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.DWKakarikoPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWKakarikoPortalNonInverted,
                        RequirementDictionary.Instance[RequirementType.Gloves1]),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWestNotBunny)
                },
                RequirementNodeID.DWKakarikoPortalInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWKakarikoPortal,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.DarkWorldWest => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.NonEntranceInverted),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldMirror),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWKakarikoPortal,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW]),
                    new RequirementNodeConnection(RequirementNodeID.BumperCaveEntry),
                    new RequirementNodeConnection(RequirementNodeID.BumperCaveTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.HammerHouseNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves2]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWWitchAreaNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hookshot])
                },
                RequirementNodeID.DarkWorldWestMirror => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DarkWorldWestNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(RequirementNodeID.DarkWorldWestNotBunny),
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkWorldWest,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyMirror])
                    },
                RequirementNodeID.DarkWorldWestLift2 => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWestNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves2])
                },
                RequirementNodeID.SkullWoodsBack => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWestNotBunny,
                        RequirementDictionary.Instance[RequirementType.FireRod])
                },
                RequirementNodeID.BumperCaveEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWestNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves1]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEntry,
                        RequirementDictionary.Instance[RequirementType.LWMirror])
                },
                RequirementNodeID.BumperCaveEntryNonEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCaveEntry,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                },
                RequirementNodeID.BumperCaveFront => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEntryNonEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCaveEntryNonEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.BumperCaveNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCaveFront,
                        RequirementDictionary.Instance[RequirementType.MoonPearl])
                },
                RequirementNodeID.BumperCavePastGap => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCaveNotBunny,
                        RequirementDictionary.Instance[RequirementType.BumperCave])
                },
                RequirementNodeID.BumperCaveBack => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCavePastGap,
                        RequirementDictionary.Instance[RequirementType.Cape])
                },
                RequirementNodeID.BumperCaveTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainExit,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCaveBack,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.BumperCaveItem => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.BumperCaveTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.Inspect])
                },
                RequirementNodeID.HammerHouse => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWestNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer])
                },
                RequirementNodeID.HammerHouseNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HammerHouse,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.HammerPegsArea => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldMirror),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWestLift2)
                },
                RequirementNodeID.HammerPegs => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HammerPegsArea,
                        RequirementDictionary.Instance[RequirementType.Hammer])
                },
                RequirementNodeID.PurpleChest => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HammerPegsArea,
                        RequirementDictionary.Instance[RequirementType.LightWorld])
                },
                RequirementNodeID.BlacksmithPrison => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldMirror),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWestLift2)
                },
                RequirementNodeID.Blacksmith => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BlacksmithPrison,
                        RequirementDictionary.Instance[RequirementType.LightWorld])
                },
                RequirementNodeID.DarkWorldSouth => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.NonEntranceInverted),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldMirror),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWSouthPortalNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer]),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWest),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldEastHammer)
                },
                RequirementNodeID.DarkWorldSouthInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.DarkWorldSouthNonInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.DarkWorldSouthMirror => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DarkWorldSouthNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.DarkWorldSouthDash => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthNotBunny,
                        RequirementDictionary.Instance[RequirementType.Boots])
                },
                RequirementNodeID.DarkWorldSouthHammer => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer])
                },
                RequirementNodeID.BuyBigBomb => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldInverted,
                        RequirementDictionary.Instance[RequirementType.RedCrystal]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthNonInverted,
                        RequirementDictionary.Instance[RequirementType.RedCrystal])
                },
                RequirementNodeID.BuyBigBombNonInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BuyBigBomb,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.BigBombToLightWorld => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BuyBigBomb,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.DWMirror],
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]
                        }))
                },
                RequirementNodeID.BigBombToLightWorldNonInverted =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.BigBombToLightWorld,
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                    },
                RequirementNodeID.BigBombToDWLakeHylia => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BuyBigBombNonInverted,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.BombDuplicationAncillaOverload],
                            RequirementDictionary.Instance[RequirementType.BombDuplicationMirror]
                        }))
                },
                RequirementNodeID.BigBombToWall => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BuyBigBombNonInverted,
                        RequirementDictionary.Instance[RequirementType.Hammer]),
                    new RequirementNodeConnection(
                        RequirementNodeID.BigBombToLightWorld,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.BigBombToLightWorldNonInverted,
                        RequirementDictionary.Instance[RequirementType.Aga1]),
                    new RequirementNodeConnection(RequirementNodeID.BigBombToDWLakeHylia)
                },
                RequirementNodeID.DWSouthPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWSouthPortalNonInverted,
                        RequirementDictionary.Instance[RequirementType.Gloves1]),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthHammer)
                },
                RequirementNodeID.DWSouthPortalInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWSouthPortal,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.DWSouthPortalNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWSouthPortal,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.MireArea => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldMirror),
                    new RequirementNodeConnection(RequirementNodeID.DWMirePortal)
                },
                RequirementNodeID.MireAreaMirror => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.MireArea,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.MireAreaNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.MireArea,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(RequirementNodeID.MireAreaNotBunny),
                        new RequirementNodeConnection(
                            RequirementNodeID.MireArea,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyMirror])
                    },
                RequirementNodeID.MiseryMireEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.MireAreaNotBunny,
                        RequirementDictionary.Instance[RequirementType.MMMedallion])
                },
                RequirementNodeID.DWMirePortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.FluteInverted),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWMirePortal,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWMirePortalNonInverted,
                        RequirementDictionary.Instance[RequirementType.Gloves2])
                },
                RequirementNodeID.DWMirePortalInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWMirePortal,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.DWGraveyard => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWGraveyard,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWestNotBunny)
                },
                RequirementNodeID.DWGraveyardMirror => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWGraveyard,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DWGraveyardLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWGraveyardLedge,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(RequirementNodeID.DWGraveyard)
                },
                RequirementNodeID.DWWitchArea => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWWitchArea,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEastNotBunny,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Hammer],
                            RequirementDictionary.Instance[RequirementType.Gloves1]
                        })),
                    new RequirementNodeConnection(RequirementNodeID.DWLakeHyliaFlippers),
                    new RequirementNodeConnection(RequirementNodeID.DWLakeHyliaFakeFlippers),
                    new RequirementNodeConnection(RequirementNodeID.DWLakeHyliaWaterWalk)
                },
                RequirementNodeID.DWWitchAreaNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWWitchArea,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.CatfishArea => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.ZoraArea,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWWitchAreaNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves1])
                },
                RequirementNodeID.DarkWorldEast => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNonInverted,
                        RequirementDictionary.Instance[RequirementType.Aga1]),
                    new RequirementNodeConnection(RequirementNodeID.LightWorldMirror),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWWitchAreaNotBunny,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Gloves1],
                            RequirementDictionary.Instance[RequirementType.Hammer]
                        })),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthHammer),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWEastPortalNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer]),
                    new RequirementNodeConnection(RequirementNodeID.DWLakeHyliaFlippers),
                    new RequirementNodeConnection(RequirementNodeID.DWLakeHyliaFakeFlippers),
                    new RequirementNodeConnection(RequirementNodeID.DWLakeHyliaWaterWalk),
                },
                RequirementNodeID.DarkWorldEastNonInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEast,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.DarkWorldEastMirror => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEast,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DarkWorldEastNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEast,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.DarkWorldEastHammer => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEastNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer])
                },
                RequirementNodeID.DWEastPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWEastPortalNonInverted,
                        RequirementDictionary.Instance[RequirementType.Gloves1]),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldEastHammer)
                },
                RequirementNodeID.DWEastPortalInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWEastPortal,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.DWEastPortalNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWEastPortal,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.DWLakeHyliaFlippers => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWestNotBunny,
                        RequirementDictionary.Instance[RequirementType.Flippers]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthNotBunny,
                        RequirementDictionary.Instance[RequirementType.Flippers]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWWitchAreaNotBunny,
                        RequirementDictionary.Instance[RequirementType.Flippers]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEastNotBunny,
                        RequirementDictionary.Instance[RequirementType.Flippers]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthEastNotBunny,
                        RequirementDictionary.Instance[RequirementType.Flippers]),
                    new RequirementNodeConnection(
                        RequirementNodeID.IcePalaceIslandInverted,
                        RequirementDictionary.Instance[RequirementType.Flippers])
                },
                RequirementNodeID.DWLakeHyliaFakeFlippers => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWestNotBunny,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersQirnJump],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthNotBunny,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWWitchAreaNotBunny,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEastNotBunny,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthEastNotBunny,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.IcePalaceIslandInverted,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]
                        }))
                },
                RequirementNodeID.DWLakeHyliaWaterWalk => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWestNotBunny,
                        RequirementDictionary.Instance[RequirementType.WaterWalk])
                },
                RequirementNodeID.IcePalaceIsland => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LakeHyliaFairyIsland,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LakeHyliaFairyIslandNonInverted,
                        RequirementDictionary.Instance[RequirementType.Gloves2]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWLakeHyliaFlippers,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWLakeHyliaFakeFlippers,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWLakeHyliaWaterWalk,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.IcePalaceIslandInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.IcePalaceIsland,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.DarkWorldSouthEast => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.LightWorldMirror),
                    new RequirementNodeConnection(RequirementNodeID.DWLakeHyliaFlippers),
                    new RequirementNodeConnection(RequirementNodeID.DWLakeHyliaFakeFlippers),
                    new RequirementNodeConnection(RequirementNodeID.DWLakeHyliaWaterWalk)
                },
                RequirementNodeID.DarkWorldSouthEastNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthEast,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.DarkWorldSouthEastLift1 => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthEastNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves1])
                },
                RequirementNodeID.DarkDeathMountainWestBottom =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(RequirementNodeID.FluteInverted),
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainEntryCaveDark,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainWestBottom,
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                                RequirementDictionary.Instance[RequirementType.LWMirror]
                            })),
                        new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop)
                    },
                RequirementNodeID.DarkDeathMountainWestBottomInverted =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                    },
                RequirementNodeID.DarkDeathMountainWestBottomNonEntrance =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                    },
                RequirementNodeID.DarkDeathMountainWestBottomMirror =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementDictionary.Instance[RequirementType.DWMirror])
                    },
                RequirementNodeID.DarkDeathMountainWestBottomNotBunny =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkDeathMountainWestBottom,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                    },
                RequirementNodeID.SpikeCavePastHammerBlocks => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainWestBottomNotBunny,
                        RequirementDictionary.Instance[RequirementType.Hammer])
                },
                RequirementNodeID.SpikeCavePastSpikes => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.SpikeCavePastHammerBlocks,
                        RequirementDictionary.Instance[RequirementType.SpikeCave])
                },
                RequirementNodeID.SpikeCaveChest => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.SpikeCavePastSpikes,
                        RequirementDictionary.Instance[RequirementType.Gloves1])
                },
                RequirementNodeID.DarkDeathMountainTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestTop,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTop,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTopInverted),
                    new RequirementNodeConnection(RequirementNodeID.SuperBunnyCave),
                    new RequirementNodeConnection(RequirementNodeID.DWFloatingIsland),
                    new RequirementNodeConnection(RequirementNodeID.DWTurtleRockTop)
                },
                RequirementNodeID.DarkDeathMountainTopInverted =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkDeathMountainTop,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                    },
                RequirementNodeID.DarkDeathMountainTopNonInverted =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkDeathMountainTop,
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                    },
                RequirementNodeID.DarkDeathMountainTopMirror =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkDeathMountainTop,
                            RequirementDictionary.Instance[RequirementType.DWMirror])
                    },
                RequirementNodeID.DarkDeathMountainTopNotBunny =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkDeathMountainTop,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                    },
                RequirementNodeID.SuperBunnyCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainTop,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainEastBottom,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                },
                RequirementNodeID.SuperBunnyCaveChests => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.SuperBunnyCave,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole]
                        }))
                },
                RequirementNodeID.GanonsTowerEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainTopInverted),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainTopNonInverted,
                        RequirementDictionary.Instance[RequirementType.GTCrystal])
                },
                RequirementNodeID.GanonsTowerEntranceNonInverted =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.GanonsTowerEntrance,
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                    },
                RequirementNodeID.DWFloatingIsland => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWFloatingIsland,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.HookshotCaveEntrance,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                },
                RequirementNodeID.HookshotCaveEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainTopNotBunny,
                        RequirementDictionary.Instance[RequirementType.Gloves1])
                },
                RequirementNodeID.HookshotCaveEntranceHookshot =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.HookshotCaveEntrance,
                            RequirementDictionary.Instance[RequirementType.Hookshot])
                    },
                RequirementNodeID.HookshotCaveEntranceHover => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HookshotCaveEntrance,
                        RequirementDictionary.Instance[RequirementType.Hover])
                },
                RequirementNodeID.HookshotCaveBonkableChest => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HookshotCaveEntrance,
                        RequirementDictionary.Instance[RequirementType.BonkOverLedge]),
                    new RequirementNodeConnection(
                        RequirementNodeID.HookshotCaveEntranceHookshot),
                    new RequirementNodeConnection(RequirementNodeID.HookshotCaveEntranceHover)
                },
                RequirementNodeID.HookshotCaveBack => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.HookshotCaveEntranceHookshot),
                    new RequirementNodeConnection(RequirementNodeID.HookshotCaveEntranceHover)
                },
                RequirementNodeID.DWTurtleRockTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWTurtleRockTopNonInverted,
                        RequirementDictionary.Instance[RequirementType.Hammer]),
                    new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTopInverted)
                },
                RequirementNodeID.DWTurtleRockTopNotBunny => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWTurtleRockTop,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.TurtleRockFrontEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWTurtleRockTopNotBunny,
                        RequirementDictionary.Instance[RequirementType.TRMedallion])
                },
                RequirementNodeID.DarkDeathMountainEastBottom =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainEastBottom,
                            RequirementDictionary.Instance[RequirementType.LWMirror]),
                        new RequirementNodeConnection(
                            RequirementNodeID.DeathMountainEastBottomLift2,
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]),
                        new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop)
                    },
                RequirementNodeID.DarkDeathMountainEastBottomInverted =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkDeathMountainEastBottom,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                    },
                RequirementNodeID.DarkDeathMountainEastBottomConnector =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.DarkDeathMountainEastBottom,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                    },
                RequirementNodeID.TurtleRockTunnel => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.SpiralCaveLedge,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.MimicCaveLedge,
                        RequirementDictionary.Instance[RequirementType.LWMirror])
                },
                RequirementNodeID.TurtleRockTunnelMirror => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.TurtleRockTunnel,
                        RequirementDictionary.Instance[RequirementType.DWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.TRKeyDoorsToMiddleExit,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.TurtleRockSafetyDoor => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTopConnector,
                        RequirementDictionary.Instance[RequirementType.LWMirror])
                },
                RequirementNodeID.HCSanctuaryEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror)
                },
                RequirementNodeID.HCFrontEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunnyOrDungeonRevive)
                },
                RequirementNodeID.HCBackEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(RequirementNodeID.EscapeGrave)
                },
                RequirementNodeID.ATEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(
                        RequirementNodeID.AgahnimTowerEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.GanonsTowerEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.EPEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldNotBunnyOrDungeonRevive)
                },
                RequirementNodeID.DPFrontEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(
                        RequirementNodeID.DesertPalaceFrontEntrance,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]
                        }))
                },
                RequirementNodeID.DPLeftEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(RequirementNodeID.DesertLedge)
                },
                RequirementNodeID.DPBackEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(RequirementNodeID.DesertBack)
                },
                RequirementNodeID.ToHEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestTop,
                        RequirementDictionary.Instance[RequirementType.DungeonRevive]),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTopNotBunny)
                },
                RequirementNodeID.PoDEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldEastNotBunny)
                },
                RequirementNodeID.SPEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorldInverted,
                        RequirementDictionary.Instance[RequirementType.SPEntry]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthNonInverted,
                        RequirementDictionary.Instance[RequirementType.SPEntry])
                },
                RequirementNodeID.SWFrontEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.DungeonRevive]),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWestNotBunny)
                },
                RequirementNodeID.SWBackEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(RequirementNodeID.SkullWoodsBack)
                },
                RequirementNodeID.TTEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWestNotBunny)
                },
                RequirementNodeID.IPEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(RequirementNodeID.IcePalaceIsland,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]
                        }))
                },
                RequirementNodeID.MMEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(RequirementNodeID.MiseryMireEntrance)
                },
                RequirementNodeID.TRFrontEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(RequirementNodeID.TurtleRockFrontEntrance)
                },
                RequirementNodeID.TRFrontEntryNonInverted => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.TRFrontEntry,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted])
                },
                RequirementNodeID.TRFrontEntryNonInvertedNonEntrance =>
                    new List<RequirementNodeConnection>
                    {
                        new RequirementNodeConnection(
                            RequirementNodeID.TRFrontEntryNonInverted,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                    },
                RequirementNodeID.TRFrontToKeyDoors => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.TRFrontEntryNonInvertedNonEntrance,
                        RequirementDictionary.Instance[RequirementType.CaneOfSomaria])
                },
                RequirementNodeID.TRKeyDoorsToMiddleExit => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.TRFrontToKeyDoors,
                        RequirementDictionary.Instance[RequirementType.TRKeyDoorsToMiddleExit])
                },
                RequirementNodeID.TRMiddleEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(RequirementNodeID.TurtleRockTunnel)
                },
                RequirementNodeID.TRBackEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(RequirementNodeID.TurtleRockSafetyDoor)
                },
                RequirementNodeID.GTEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.EntranceShuffle),
                    new RequirementNodeConnection(
                        RequirementNodeID.AgahnimTowerEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.GanonsTowerEntranceNonInverted,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW],
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]
                        }))
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        /// <summary>
        /// Returns a new requirement node for the specified requirement node ID.
        /// </summary>
        /// <param name="id">
        /// The requirement node ID.
        /// </param>
        /// <returns>
        /// A new requirement node.
        /// </returns>
        internal static IRequirementNode GetRequirementNode(RequirementNodeID id)
        {
            return new RequirementNode(id, GetRequirementNodeConnections(id));
        }
    }
}
