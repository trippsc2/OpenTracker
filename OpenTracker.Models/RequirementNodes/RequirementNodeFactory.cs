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
                RequirementNodeID.Start => null,
                RequirementNodeID.LightWorld => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.LightWorldAccess]
                        })),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEntry),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainExit),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWKakarikoPortal,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.LWLift2],
                            RequirementDictionary.Instance[RequirementType.LWHammer]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.GrassHouse,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(
                        RequirementNodeID.BombHut,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(RequirementNodeID.RaceGameLedge),
                    new RequirementNodeConnection(RequirementNodeID.SouthOfGroveLedge),
                    new RequirementNodeConnection(RequirementNodeID.DesertLedge),
                    new RequirementNodeConnection(RequirementNodeID.CheckerboardLedge),
                    new RequirementNodeConnection(RequirementNodeID.BombosTabletLedge),
                    new RequirementNodeConnection(RequirementNodeID.LWMirePortal),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWGraveyardLedge,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(RequirementNodeID.HyruleCastleTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWSouthPortal,
                        RequirementDictionary.Instance[RequirementType.LWHammer]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWWitchArea,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWEastPortal,
                        RequirementDictionary.Instance[RequirementType.LWHammer]),
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHylia),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Aga1],
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]
                        }))
                },
                RequirementNodeID.LumberjackTreeActive => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.Aga1]),
                },
                RequirementNodeID.LumberjackCaveEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LumberjackTreeActive,
                        RequirementDictionary.Instance[RequirementType.LWDash]),
                },
                RequirementNodeID.ForestHideout => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                },
                RequirementNodeID.DeathMountainEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DeathMountainEntryAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWLift1]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainExit,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[
                                RequirementType.WorldStateInvertedEntranceShuffleOff],
                            RequirementDictionary.Instance[RequirementType.BumperCave]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCave,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DeathMountainExit => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DeathMountainExitAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEntry,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[
                                RequirementType.WorldStateInvertedEntranceShuffleOff],
                            RequirementDictionary.Instance[RequirementType.BumperCave]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestBottom,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[
                                RequirementType.WorldStateNonInvertedEntranceShuffleOff],
                            RequirementDictionary.Instance[RequirementType.DarkRoomDeathMountainExit]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCaveTop,
                        RequirementDictionary.Instance[RequirementType.DWMirror]),
                },
                RequirementNodeID.LWKakarikoPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.LWLift2],
                            RequirementDictionary.Instance[RequirementType.LWHammer]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.DWLift1]
                        })),
                },
                RequirementNodeID.GrassHouse => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.GrassHouseAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.BombHut => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.BombHutAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.MagicBatLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWHammer]),
                    new RequirementNodeConnection(
                        RequirementNodeID.HammerPegsArea,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.RaceGameLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.RaceGameLedgeAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOff],
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        RequirementDictionary.Instance[RequirementType.DWMirror]),
                },
                RequirementNodeID.SouthOfGroveLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.SouthOfGroveLedgeAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        RequirementDictionary.Instance[RequirementType.DWMirror]),
                },
                RequirementNodeID.DesertLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DesertLedgeAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DesertPalaceBackEntrance,
                        RequirementDictionary.Instance[RequirementType.LWLift1]),
                    new RequirementNodeConnection(
                        RequirementNodeID.MireArea,
                        RequirementDictionary.Instance[RequirementType.DWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DPFrontEntry,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                },
                RequirementNodeID.DesertPalaceBackEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DesertPalaceBackEntranceAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DesertLedge,
                        RequirementDictionary.Instance[RequirementType.LWLift1]),
                    new RequirementNodeConnection(
                        RequirementNodeID.MireArea,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.CheckerboardLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.CheckerboardLedgeAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.MireArea,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.CheckerboardCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.CheckerboardLedge,
                        RequirementDictionary.Instance[RequirementType.LWLift1])
                },
                RequirementNodeID.DesertPalaceFrontEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.Book]),
                    new RequirementNodeConnection(
                        RequirementNodeID.MireArea,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.BombosTabletLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.RupeeCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWLift1])
                },
                RequirementNodeID.LWMirePortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWFlute]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWMirePortal,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.DWLift2]
                        })),
                },
                RequirementNodeID.NorthBonkRocks => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWDash])
                },
                RequirementNodeID.LWGraveyard => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(RequirementNodeID.LWGraveyardLedge),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWKingsTomb,
                        RequirementDictionary.Instance[RequirementType.LWLift2]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.DWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWGraveyard,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.EscapeGrave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWGraveyard,
                        RequirementDictionary.Instance[RequirementType.LWLift1])
                },
                RequirementNodeID.LWGraveyardLedge => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.LWGraveyardLedgeAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWGraveyard,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWGraveyardLedge,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.LWKingsTomb => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.LWKingsTombAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWGraveyard,
                        RequirementDictionary.Instance[RequirementType.LWLift2]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWGraveyard,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.KingsTombGrave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWKingsTomb,
                        RequirementDictionary.Instance[RequirementType.LWDash])
                },
                RequirementNodeID.HyruleCastleTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.HyruleCastleTopAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEast,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.AgahnimTowerEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HyruleCastleTop,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                                RequirementDictionary.Instance[RequirementType.ATBarrier]
                            }),
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                RequirementDictionary.Instance[RequirementType.GTCrystal]
                            })
                        }))
                },
                RequirementNodeID.GanonHole => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HyruleCastleTop,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.Aga2]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEast,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.Aga2]
                        }))
                },
                RequirementNodeID.GanonHoleBack => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                },
                RequirementNodeID.CastleSecretFront => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.CastleSecretBack => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEast,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.CentralBonkRocks => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWDash])
                },
                RequirementNodeID.LWSouthPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWHammer]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWSouthPortal,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.DWMirror],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                RequirementDictionary.Instance[RequirementType.DWLift1]
                            })
                        }))
                },
                RequirementNodeID.HypeFairyCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.MiniMoldormCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.Zora => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.WaterfallFairy),
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHylia),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWWitchArea,
                        RequirementDictionary.Instance[RequirementType.LWLift1]),
                    new RequirementNodeConnection(
                        RequirementNodeID.Catfish,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.WaterfallFairy => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.WaterfallFairyAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.Zora,
                        RequirementDictionary.Instance[RequirementType.LWSwim]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWLakeHylia,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.LWSwim],
                            RequirementDictionary.Instance[RequirementType.WaterWalkFromWaterfallCave]
                        })),
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHyliaWaterWalk)
                },
                RequirementNodeID.LWWitchArea => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.LWWitchAreaAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW]),
                    new RequirementNodeConnection(
                        RequirementNodeID.Zora,
                        RequirementDictionary.Instance[RequirementType.LWLift1]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWWitchArea,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.LWEastPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWHammer]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWEastPortal,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                RequirementDictionary.Instance[RequirementType.LWLift1]
                            }),
                            RequirementDictionary.Instance[RequirementType.DWMirror]
                        })),
                },
                RequirementNodeID.LWLakeHylia => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.LWSwim],
                            RequirementDictionary.Instance[RequirementType.LWFakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.LWFakeFlippersScreenTransition],
                            RequirementDictionary.Instance[RequirementType.LWFakeFlippersSplashDeletion]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWLakeHylia,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.LWLakeHyliaWaterWalk => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWWaterWalk]),
                    new RequirementNodeConnection(
                        RequirementNodeID.WaterfallFairy,
                        RequirementDictionary.Instance[RequirementType.WaterWalkFromWaterfallCave])
                },
                RequirementNodeID.LakeHyliaIsland => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWLakeHylia,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWLakeHylia,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.Flippers],
                            RequirementDictionary.Instance[RequirementType.DWMirror]
                        }))
                },
                RequirementNodeID.LakeHyliaFairyIsland => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.LakeHyliaFairyIslandAccess]),
                    new RequirementNodeConnection(RequirementNodeID.LWLakeHylia),
                    new RequirementNodeConnection(
                        RequirementNodeID.IcePalaceEntrance,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.DWMirror],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                RequirementDictionary.Instance[RequirementType.DWLift2]
                            })
                        }))
                },
                RequirementNodeID.IceRodCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.NotBunnyLW])
                },
                RequirementNodeID.IceFairyCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWLift1])
                },
                RequirementNodeID.DeathMountainWestBottom => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DeathMountainWestBottomAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWFlute]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEntry,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[
                                RequirementType.WorldStateNonInvertedEntranceShuffleOff],
                            RequirementDictionary.Instance[RequirementType.DarkRoomDeathMountainEntry]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainExit,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[
                                RequirementType.WorldStateNonInvertedEntranceShuffleOff],
                            RequirementDictionary.Instance[RequirementType.DarkRoomDeathMountainExit]
                        })),
                    new RequirementNodeConnection(RequirementNodeID.SpectacleRockTop),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainWestTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastBottom,
                        RequirementDictionary.Instance[RequirementType.LWHookshot]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainWestBottom,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.DWMirror]
                        }))
                },
                RequirementNodeID.SpectacleRockTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestTop,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainWestBottom,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DeathMountainWestTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DeathMountainWestTopAccess]),
                    new RequirementNodeConnection(RequirementNodeID.SpectacleRockTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTop,
                        RequirementDictionary.Instance[RequirementType.LWHammer]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainTop,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DeathMountainEastBottom => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DeathMountainWestBottomAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestBottom,
                        RequirementDictionary.Instance[RequirementType.LWHookshot]),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottomConnector),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTopConnector),
                    new RequirementNodeConnection(RequirementNodeID.SpiralCave),
                    new RequirementNodeConnection(
                        RequirementNodeID.MimicCave,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainEastBottom,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DeathMountainEastBottomConnector => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DeathMountainEastBottomConnectorAccess]),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTopConnector),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastBottom,
                        RequirementDictionary.Instance[RequirementType.LWLift2]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainEastBottomConnector,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DeathMountainEastTopConnector => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DeathMountainEastTopConnectorAccess]),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.TurtleRockSafetyDoor,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.SpiralCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.SpiralCaveAccess]),
                    new RequirementNodeConnection(RequirementNodeID.DeathMountainEastTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.TurtleRockTunnel,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.MimicCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.MimicCaveAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTop,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.TurtleRockTunnel,
                        RequirementDictionary.Instance[RequirementType.DWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.TRKeyDoorsToMiddleExit,
                        RequirementDictionary.Instance[RequirementType.DWMirror])
                },
                RequirementNodeID.DeathMountainEastTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DeathMountainEastTopAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestTop,
                        RequirementDictionary.Instance[RequirementType.LWHammer]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastBottom,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWFloatingIsland,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWTurtleRockTop,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.LWHammer]
                        }))
                },
                RequirementNodeID.LWFloatingIsland => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWFloatingIsland,
                        RequirementDictionary.Instance[RequirementType.DWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTop,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.LWTurtleRockTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTop,
                        RequirementDictionary.Instance[RequirementType.LWLift2]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWTurtleRockTop,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                                RequirementDictionary.Instance[RequirementType.DWLift2]
                            }),
                            RequirementDictionary.Instance[RequirementType.DWMirror]
                        }))
                },
                RequirementNodeID.DWKakarikoPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWKakarikoPortal,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                                RequirementDictionary.Instance[RequirementType.LWLift1]
                            }),
                            RequirementDictionary.Instance[RequirementType.LWMirror]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.DarkWorldWest => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleOff],
                            RequirementDictionary.Instance[RequirementType.DarkWorldWestAccess]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(RequirementNodeID.BumperCaveTop),
                    new RequirementNodeConnection(RequirementNodeID.BumperCave),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWKakarikoPortal,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWWitchArea,
                        RequirementDictionary.Instance[RequirementType.DWHookshot])
                },
                RequirementNodeID.SkullWoodsBackEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.DWFireRod]),
                },
                RequirementNodeID.BumperCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.BumperCaveAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.DWLift1]),
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCaveTop,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.BumperCave],
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInvertedEntranceShuffleOff]
                        }))
                },
                RequirementNodeID.BumperCaveTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.BumperCaveTopAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCave,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.BumperCave],
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInvertedEntranceShuffleOff]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainExit,
                        RequirementDictionary.Instance[RequirementType.LWMirror])
                },
                RequirementNodeID.ThievesTownEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.BombableShack => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.HammerHouse => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.DWHammer])
                },
                RequirementNodeID.HammerPegsArea => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.HammerPegsAreaAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.DWLift2])
                },
                RequirementNodeID.HammerPegs => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.HammerPegsArea,
                        RequirementDictionary.Instance[RequirementType.DWHammer])
                },
                RequirementNodeID.BlacksmithPrison => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.DWLift2]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWMirror])
                },
                RequirementNodeID.DarkWorldSouth => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInvertedEntranceShuffleOff],
                            RequirementDictionary.Instance[RequirementType.DarkWorldSouthAccess]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWSouthPortal,
                        RequirementDictionary.Instance[RequirementType.DWHammer]),
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWest),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEast,
                        RequirementDictionary.Instance[RequirementType.DWHammer])
                },
                RequirementNodeID.DWCentralBonkRocks => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        RequirementDictionary.Instance[RequirementType.DWDash])
                },
                RequirementNodeID.BuyBigBomb => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.RedCrystal]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.RedCrystal]
                        }))
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
                RequirementNodeID.BigBombToDWLakeHylia => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BuyBigBomb,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            new AlternativeRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.BombDuplicationAncillaOverload],
                                RequirementDictionary.Instance[RequirementType.BombDuplicationMirror]
                            })
                        }))
                },
                RequirementNodeID.BigBombToWall => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.BuyBigBomb,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.DWHammer]
                        })),
                    new RequirementNodeConnection(RequirementNodeID.BigBombToDWLakeHylia),
                    new RequirementNodeConnection(
                        RequirementNodeID.BigBombToLightWorld,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                                RequirementDictionary.Instance[RequirementType.Aga1]
                            }),
                            RequirementDictionary.Instance[RequirementType.LWMirror]
                        }))
                },
                RequirementNodeID.DWSouthPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWSouthPortal,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                                RequirementDictionary.Instance[RequirementType.LWLift1]
                            }),
                            RequirementDictionary.Instance[RequirementType.DWMirror]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        RequirementDictionary.Instance[RequirementType.DWHammer])
                },
                RequirementNodeID.HypeCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW]),
                },
                RequirementNodeID.MireArea => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.MireAreaAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(RequirementNodeID.DWMirePortal)
                },
                RequirementNodeID.DWMirePortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.DWFlute]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWMirePortal,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.LWMirror],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                                RequirementDictionary.Instance[RequirementType.LWLift2]
                            })
                        })),
                },
                RequirementNodeID.MiseryMireEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.MireArea,
                        RequirementDictionary.Instance[RequirementType.MMMedallion])
                },
                RequirementNodeID.DWGraveyard => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWGraveyard,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
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
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DWWitchAreaAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWWitchArea,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.DWSwim],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersQirnJump],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersSplashDeletion],
                            RequirementDictionary.Instance[RequirementType.DWWaterWalk]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEast,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.DWHammer],
                            RequirementDictionary.Instance[RequirementType.DWLift1],
                            RequirementDictionary.Instance[RequirementType.DWSwim],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersSplashDeletion]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.Catfish,
                        RequirementDictionary.Instance[RequirementType.DWLift1])
                },
                RequirementNodeID.Catfish => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Zora,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWWitchArea,
                        RequirementDictionary.Instance[RequirementType.DWLift1])
                },
                RequirementNodeID.DarkWorldEast => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DarkWorldEastAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                                RequirementDictionary.Instance[RequirementType.Aga1]
                            }),
                            RequirementDictionary.Instance[RequirementType.LWMirror]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWWitchArea,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.DWLift1],
                            RequirementDictionary.Instance[RequirementType.DWHammer]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.DWSwim],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersSplashDeletion],
                            RequirementDictionary.Instance[RequirementType.DWHammer]
                        })),
                    new RequirementNodeConnection(RequirementNodeID.DWLakeHylia),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWEastPortal,
                        RequirementDictionary.Instance[RequirementType.DWHammer])
                },
                RequirementNodeID.FatFairy => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.BigBombToWall)
                },
                RequirementNodeID.PalaceOfDarknessEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEast,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.DWEastPortal => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWEastPortal,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.LWLift1]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEast,
                        RequirementDictionary.Instance[RequirementType.DWHammer])
                },
                RequirementNodeID.DWLakeHylia => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWLakeHylia,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.DWSwim],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersSplashDeletion]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldEast,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.DWSwim],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersSplashDeletion]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthEast,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.DWSwim],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersFairyRevival],
                            RequirementDictionary.Instance[RequirementType.DWFakeFlippersSplashDeletion]
                        })),
                },
                RequirementNodeID.IcePalaceEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.IcePalaceAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LakeHyliaFairyIsland,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.LWMirror],
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                                RequirementDictionary.Instance[RequirementType.LWLift2]
                            })
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DWLakeHylia,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.DarkWorldSouthEast => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DarkWorldSouthEastAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(RequirementNodeID.DWLakeHylia)
                },
                RequirementNodeID.DWIceRodCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthEast,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.DWIceRodRock => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouthEast,
                        RequirementDictionary.Instance[RequirementType.DWLift1])
                },
                RequirementNodeID.DarkDeathMountainWestBottom => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DarkDeathMountainWestBottomAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        RequirementDictionary.Instance[RequirementType.DWFlute]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestBottom,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.LWMirror]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.BumperCave,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[
                                RequirementType.WorldStateInvertedEntranceShuffleOff],
                            RequirementDictionary.Instance[RequirementType.DarkRoomDeathMountainEntry]
                        })),
                    new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop)
                },
                RequirementNodeID.DarkDeathMountainTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DarkDeathMountainTopAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestTop,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTop,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(RequirementNodeID.DWFloatingIsland),
                    new RequirementNodeConnection(RequirementNodeID.DWTurtleRockTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainWestBottom,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted]),
                    new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainEastBottom)
                },
                RequirementNodeID.GanonsTowerEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainTop,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.GTCrystal]
                        }))
                },
                RequirementNodeID.DWFloatingIsland => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DWFloatingIslandAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LWFloatingIsland,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.HookshotCave,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOff])
                },
                RequirementNodeID.HookshotCave => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainTop,
                        RequirementDictionary.Instance[RequirementType.DWLift1])
                },
                RequirementNodeID.DWTurtleRockTop => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.LWTurtleRockTop,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                                RequirementDictionary.Instance[RequirementType.LWHammer]
                            }),
                            RequirementDictionary.Instance[RequirementType.LWMirror]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainTop,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.TurtleRockFrontEntrance => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DWTurtleRockTop,
                        RequirementDictionary.Instance[RequirementType.TRMedallion])
                },
                RequirementNodeID.DarkDeathMountainEastBottom => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.DarkDeathMountainEastBottomAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastBottom,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AggregateRequirement(new List<IRequirement>
                            {
                                RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                                RequirementDictionary.Instance[RequirementType.LWLift2]
                            }),
                            RequirementDictionary.Instance[RequirementType.LWMirror]
                        })),
                    new RequirementNodeConnection(RequirementNodeID.DarkDeathMountainTop),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainEastBottomConnector,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.DarkDeathMountainEastBottomConnector => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastBottomConnector,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkDeathMountainEastBottom,
                        RequirementDictionary.Instance[RequirementType.NotBunnyDW])
                },
                RequirementNodeID.TurtleRockTunnel => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.TurtleRockTunnelAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.SpiralCave,
                        RequirementDictionary.Instance[RequirementType.LWMirror]),
                    new RequirementNodeConnection(
                        RequirementNodeID.MimicCave,
                        RequirementDictionary.Instance[RequirementType.LWMirror])
                },
                RequirementNodeID.TurtleRockSafetyDoor => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.TurtleRockSafetyDoorAccess]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainEastTopConnector,
                        RequirementDictionary.Instance[RequirementType.LWMirror])
                },
                RequirementNodeID.HCSanctuaryEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOn],
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.SuperBunnyMirror]
                        }))
                },
                RequirementNodeID.HCFrontEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]
                        }))
                },
                RequirementNodeID.HCBackEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(RequirementNodeID.EscapeGrave)
                },
                RequirementNodeID.ATEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(
                        RequirementNodeID.AgahnimTowerEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.GanonsTowerEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
                },
                RequirementNodeID.EPEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]
                        })),
                },
                RequirementNodeID.DPFrontEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
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
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DesertLedge,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]
                        }))
                },
                RequirementNodeID.DPBackEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DesertPalaceBackEntrance,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]
                        }))
                },
                RequirementNodeID.ToHEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DeathMountainWestTop,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW],
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]
                        }))
                },
                RequirementNodeID.PoDEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(RequirementNodeID.PalaceOfDarknessEntrance)
                },
                RequirementNodeID.SPEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldSouth,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateNonInverted],
                            RequirementDictionary.Instance[RequirementType.SPEntry]
                        })),
                    new RequirementNodeConnection(
                        RequirementNodeID.LightWorld,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted],
                            RequirementDictionary.Instance[RequirementType.SPEntry]
                        }))
                },
                RequirementNodeID.SWFrontEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWest)
                },
                RequirementNodeID.SWFrontLeftDropEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWest)
                },
                RequirementNodeID.SWPinballRoomEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWest)
                },
                RequirementNodeID.SWFrontTopDropEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.DarkWorldWest,
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleOn],
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]
                        }))
                },
                RequirementNodeID.SWFrontBackConnectorEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(RequirementNodeID.DarkWorldWest)
                },
                RequirementNodeID.SWBackEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(RequirementNodeID.SkullWoodsBackEntrance)
                },
                RequirementNodeID.TTEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(RequirementNodeID.ThievesTownEntrance)
                },
                RequirementNodeID.IPEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(RequirementNodeID.IcePalaceEntrance)
                },
                RequirementNodeID.MMEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(RequirementNodeID.MiseryMireEntrance)
                },
                RequirementNodeID.TRFrontEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(RequirementNodeID.TurtleRockFrontEntrance)
                },
                RequirementNodeID.TRFrontToKeyDoors => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.TRFrontEntry,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[
                                RequirementType.WorldStateNonInvertedEntranceShuffleOff],
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]
                        }))
                },
                RequirementNodeID.TRKeyDoorsToMiddleExit => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.TRFrontToKeyDoors,
                        new AggregateRequirement(new List<IRequirement>
                        {
                            RequirementDictionary.Instance[
                                RequirementType.WorldStateNonInvertedEntranceShuffleOff],
                            RequirementDictionary.Instance[RequirementType.TRKeyDoorsToMiddleExit]
                        }))
                },
                RequirementNodeID.TRMiddleEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(RequirementNodeID.TurtleRockTunnel)
                },
                RequirementNodeID.TRBackEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(RequirementNodeID.TurtleRockSafetyDoor)
                },
                RequirementNodeID.GTEntry => new List<RequirementNodeConnection>
                {
                    new RequirementNodeConnection(
                        RequirementNodeID.Start,
                        RequirementDictionary.Instance[RequirementType.EntranceShuffleOn]),
                    new RequirementNodeConnection(
                        RequirementNodeID.GanonsTowerEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateNonInverted]),
                    new RequirementNodeConnection(
                        RequirementNodeID.AgahnimTowerEntrance,
                        RequirementDictionary.Instance[RequirementType.WorldStateInverted])
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
