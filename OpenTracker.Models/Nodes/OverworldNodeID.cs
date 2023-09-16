﻿namespace OpenTracker.Models.Nodes;

/// <summary>
/// This enum type defines the overworld node ID values.
/// </summary>
public enum OverworldNodeID
{
    Inaccessible,
    Start,
    EntranceDungeonAllInsanity,
    EntranceNone,
    EntranceNoneInverted,
    Flute,
    FluteActivated,
    FluteInverted,
    FluteStandardOpen,
    LightWorld,
    LightWorldInverted,
    LightWorldInvertedNotBunny,
    LightWorldStandardOpen,
    LightWorldMirror,
    LightWorldNotBunny,
    LightWorldNotBunnyOrDungeonRevive,
    LightWorldNotBunnyOrSuperBunnyFallInHole,
    LightWorldNotBunnyOrSuperBunnyMirror,
    LightWorldDash,
    LightWorldHammer,
    LightWorldLift1,
    LightWorldFlute,
    LightWorldBook,
    Pedestal,
    LumberjackCaveHole,
    DeathMountainEntry,
    DeathMountainEntryNonEntrance,
    DeathMountainEntryCave,
    DeathMountainEntryCaveDark,
    DeathMountainExit,
    DeathMountainExitNonEntrance,
    DeathMountainExitCave,
    DeathMountainExitCaveDark,
    LWKakarikoPortal,
    LWKakarikoPortalStandardOpen,
    LWKakarikoPortalNotBunny,
    SickKid,
    GrassHouse,
    BombHut,
    MagicBatLedge,
    MagicBat,
    RaceGameLedge,
    RaceGameLedgeNotBunny,
    LWGraveyard,
    LWGraveyardNotBunny,
    LWGraveyardLedge,
    EscapeGrave,
    KingsTomb,
    KingsTombNotBunny,
    KingsTombGrave,
    SouthOfGroveLedge,
    SouthOfGrove,
    GroveDiggingSpot,
    DesertLedge,
    DesertLedgeNotBunny,
    DesertBack,
    DesertBackNotBunny,
    CheckerboardLedge,
    CheckerboardLedgeNotBunny,
    CheckerboardCave,
    DesertPalaceFrontEntrance,
    BombosTabletLedge,
    BombosTabletLedgeBook,
    BombosTablet,
    LWMirePortal,
    LWMirePortalStandardOpen,
    LWSouthPortal,
    LWSouthPortalStandardOpen,
    LWSouthPortalNotBunny,
    LWLakeHyliaFakeFlippers,
    LWLakeHyliaFlippers,
    LWLakeHyliaWaterWalk,
    Hobo,
    LakeHyliaIsland,
    LakeHyliaFairyIsland,
    LakeHyliaFairyIslandStandardOpen,
    HyruleCastleTop,
    HyruleCastleTopInverted,
    HyruleCastleTopStandardOpen,
    AgahnimTowerEntrance,
    CastleSecretExitArea,
    ZoraArea,
    ZoraLedge,
    WaterfallFairy,
    WaterfallFairyNotBunny,
    LWWitchArea,
    LWWitchAreaNotBunny,
    WitchsHut,
    Sahasrahla,
    LWEastPortal,
    LWEastPortalStandardOpen,
    LWEastPortalNotBunny,
    DeathMountainWestBottom,
    DeathMountainWestBottomNonEntrance,
    DeathMountainWestBottomNotBunny,
    SpectacleRockTop,
    DeathMountainWestTop,
    DeathMountainWestTopBook,
    DeathMountainWestTopNotBunny,
    EtherTablet,
    DeathMountainEastBottom,
    DeathMountainEastBottomNotBunny,
    DeathMountainEastBottomLift2,
    DeathMountainEastBottomConnector,
    ParadoxCave,
    ParadoxCaveSuperBunnyFallInHole,
    ParadoxCaveNotBunny,
    ParadoxCaveTop,
    DeathMountainEastTop,
    DeathMountainEastTopInverted,
    DeathMountainEastTopNotBunny,
    DeathMountainEastTopConnector,
    SpiralCaveLedge,
    SpiralCave,
    MimicCaveLedge,
    MimicCaveLedgeNotBunny,
    MimicCave,
    LWFloatingIsland,
    LWTurtleRockTop,
    LWTurtleRockTopInverted,
    LWTurtleRockTopInvertedNotBunny,
    LWTurtleRockTopStandardOpen,
    DWKakarikoPortal,
    DWKakarikoPortalInverted,
    DarkWorldWest,
    DarkWorldWestNotBunny,
    DarkWorldWestNotBunnyOrSuperBunnyMirror,
    DarkWorldWestLift2,
    SkullWoodsBackArea,
    SkullWoodsBackAreaNotBunny,
    SkullWoodsBack,
    BumperCaveEntry,
    BumperCaveEntryNonEntrance,
    BumperCaveFront,
    BumperCaveNotBunny,
    BumperCavePastGap,
    BumperCaveBack,
    BumperCaveTop,
    HammerHouse,
    HammerHouseNotBunny,
    HammerPegsArea,
    HammerPegs,
    PurpleChest,
    BlacksmithPrison,
    Blacksmith,
    DWGraveyard,
    DWGraveyardMirror,
    DarkWorldSouth,
    DarkWorldSouthInverted,
    DarkWorldSouthStandardOpen,
    DarkWorldSouthStandardOpenNotBunny,
    DarkWorldSouthMirror,
    DarkWorldSouthNotBunny,
    DarkWorldSouthDash,
    DarkWorldSouthHammer,
    BuyBigBomb,
    BuyBigBombStandardOpen,
    BigBombToLightWorld,
    BigBombToLightWorldStandardOpen,
    BigBombToDWLakeHylia,
    BigBombToWall,
    DWSouthPortal,
    DWSouthPortalInverted,
    DWSouthPortalNotBunny,
    MireArea,
    MireAreaMirror,
    MireAreaNotBunny,
    MireAreaNotBunnyOrSuperBunnyMirror,
    MiseryMireEntrance,
    DWMirePortal,
    DWMirePortalInverted,
    DWLakeHyliaFlippers,
    DWLakeHyliaFakeFlippers,
    DWLakeHyliaWaterWalk,
    IcePalaceIsland,
    IcePalaceIslandInverted,
    DarkWorldSouthEast,
    DarkWorldSouthEastNotBunny,
    DarkWorldSouthEastLift1,
    DWWitchArea,
    DWWitchAreaNotBunny,
    CatfishArea,
    DarkWorldEast,
    DarkWorldEastStandardOpen,
    DarkWorldEastNotBunny,
    DarkWorldEastHammer,
    FatFairyEntrance,
    DWEastPortal,
    DWEastPortalInverted,
    DWEastPortalNotBunny,
    DarkDeathMountainWestBottom,
    DarkDeathMountainWestBottomInverted,
    DarkDeathMountainWestBottomNonEntrance,
    DarkDeathMountainWestBottomMirror,
    DarkDeathMountainWestBottomNotBunny,
    SpikeCavePastHammerBlocks,
    SpikeCavePastSpikes,
    SpikeCaveChest,
    DarkDeathMountainTop,
    DarkDeathMountainTopInverted,
    DarkDeathMountainTopStandardOpen,
    DarkDeathMountainTopMirror,
    DarkDeathMountainTopNotBunny,
    SuperBunnyCave,
    SuperBunnyCaveChests,
    GanonsTowerEntrance,
    GanonsTowerEntranceStandardOpen,
    DWFloatingIsland,
    HookshotCaveEntrance,
    HookshotCaveEntranceHookshot,
    HookshotCaveEntranceHover,
    HookshotCaveBonkableChest,
    HookshotCaveBack,
    DWTurtleRockTop,
    DWTurtleRockTopInverted,
    DWTurtleRockTopNotBunny,
    TurtleRockFrontEntrance,
    DarkDeathMountainEastBottom,
    DarkDeathMountainEastBottomInverted,
    DarkDeathMountainEastBottomConnector,
    TurtleRockTunnel,
    TurtleRockTunnelMirror,
    TurtleRockSafetyDoor,
    GanonHole,
    HCSanctuaryEntry,
    HCFrontEntry,
    HCBackEntry,
    ATEntry,
    EPEntry,
    DPFrontEntry,
    DPLeftEntry,
    DPBackEntry,
    ToHEntry,
    PoDEntry,
    SPEntry,
    SWFrontEntry,
    SWBackEntry,
    TTEntry,
    IPEntry,
    MMEntry,
    TRFrontEntry,
    TRFrontEntryStandardOpen,
    TRFrontEntryStandardOpenEntranceNone,
    TRFrontToKeyDoors,
    TRKeyDoorsToMiddleExit,
    TRMiddleEntry,
    TRBackEntry,
    GTEntry
}