﻿namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the enum type for requirement types.
    /// </summary>
    public enum RequirementType
    {
        NoRequirement,
        Inspect,
        SequenceBreak,
        WorldStateStandardOpen,
        WorldStateInverted,
        WorldStateRetro,
        DungeonItemShuffleStandard,
        DungeonItemShuffleMapsCompasses,
        DungeonItemShuffleMapsCompassesSmallKeys,
        DungeonItemShuffleKeysanity,
        ItemPlacementBasic,
        ItemPlacementAdvanced,
        BossShuffleOff,
        BossShuffleOn,
        EnemyShuffleOff,
        EnemyShuffleOn,
        EntranceShuffleNone,
        EntranceShuffleDungeon,
        EntranceShuffleAll,
        GuaranteedBossItemsOff,
        GuaranteedBossItemsOn,
        WorldStateNonInverted,
        SmallKeyShuffleOff,
        SmallKeyShuffleOn,
        WorldStateNonInvertedEntranceShuffleNone,
        WorldStateNonInvertedEntranceShuffleNotAll,
        WorldStateInvertedEntranceShuffleNone,
        WorldStateInvertedEntranceShuffleNotAll,
        WorldStateInvertedEntranceShuffleAll,
        WorldStateRetroEntranceShuffleNotAll,
        EntranceShuffleNotAll,
        EntranceShuffleDungeonOn,
        Swordless,
        Sword1,
        Sword2,
        Sword3,
        Shield3,
        Bow,
        Boomerang,
        RedBoomerang,
        Hookshot,
        Powder,
        Mushroom,
        Boots,
        FireRod,
        IceRod,
        Bombos,
        BombosMM,
        BombosTR,
        BombosBoth,
        Ether,
        EtherMM,
        EtherTR,
        EtherBoth,
        Quake,
        QuakeMM,
        QuakeTR,
        QuakeBoth,
        Gloves1,
        Gloves2,
        Lamp,
        Hammer,
        Flute,
        Net,
        Book,
        Shovel,
        NoFlippers,
        Flippers,
        Bottle,
        CaneOfSomaria,
        CaneOfByrna,
        Cape,
        Mirror,
        HalfMagic,
        MoonPearl,
        Aga1,
        Aga2,
        RedCrystal,
        Pendant,
        GreenPendant,
        TRSmallKey2,
        SBBlindPedestal,
        SBBonkOverLedge,
        SBBumperCaveHookshot,
        SBTRLaserSkip,
        SBLanmolasBombs,
        SBHelmasaurKingBasic,
        SBArrghusBasic,
        SBMothulaBasic,
        SBBlindBasic,
        SBKholdstareBasic,
        SBVitreousBasic,
        SBTrinexxBasic,
        SBBombDuplicationAncillaOverload,
        SBBombDuplicationMirror,
        SBBombJumpPoDHammerJump,
        SBBombJumpSWBigChest,
        SBBombJumpIPBJ,
        SBBombJumpIPHookshotGap,
        SBBombJumpIPFreezorRoomGap,
        SBDarkRoomDeathMountainEntry,
        SBDarkRoomDeathMountainExit,
        SBDarkRoomHC,
        SBDarkRoomAT,
        SBDarkRoomEPRight,
        SBDarkRoomEPBack,
        SBDarkRoomPoDDarkBasement,
        SBDarkRoomPoDDarkMaze,
        SBDarkRoomPoDBossArea,
        SBDarkRoomMM,
        SBDarkRoomTR,
        SBFakeFlippersFairyRevival,
        SBFakeFlippersQirnJump,
        SBFakeFlippersScreenTransition,
        SBFakeFlippersSplashDeletion,
        SBWaterWalk,
        SBWaterWalkFromWaterfallCave,
        SBSuperBunnyFallInHole,
        SBSuperBunnyMirror,
        SBCameraUnlock,
        SBDungeonRevive,
        SBFakePowder,
        SBHover,
        SBMimicClip,
        SBSpikeCave,
        SBToHHerapot,
        SBIPIceBreaker,
        GTCrystal,
        LightWorld,
        AllMedallions,
        ExtendMagic1,
        ExtendMagic2,
        FireRodDarkRoom,
        UseMedallion,
        MeltThings,
        NotBunnyLW,
        NotBunnyDW,
        ATBarrier,
        BombDuplicationAncillaOverload,
        BombDuplicationMirror,
        BombJumpPoDHammerJump,
        BombJumpSWBigChest,
        BombJumpIPBJ,
        BombJumpIPHookshotGap,
        BombJumpIPFreezorRoomGap,
        BonkOverLedge,
        BumperCave,
        CameraUnlock,
        Curtains,
        DarkRoomDeathMountainEntry,
        DarkRoomDeathMountainExit,
        DarkRoomHC,
        DarkRoomAT,
        DarkRoomEPRight,
        DarkRoomEPBack,
        DarkRoomPoDDarkBasement,
        DarkRoomPoDDarkMaze,
        DarkRoomPoDBossArea,
        DarkRoomMM,
        DarkRoomTR,
        DungeonRevive,
        FakeFlippersFairyRevival,
        FakeFlippersQirnJump,
        FakeFlippersScreenTransition,
        FakeFlippersSplashDeletion,
        FireSource,
        Hover,
        LaserBridge,
        MimicClip,
        Pedestal,
        RedEyegoreGoriya,
        SPEntry,
        SpikeCave,
        SuperBunnyFallInHole,
        SuperBunnyMirror,
        Tablet,
        Torch,
        ToHHerapot,
        IPIceBreaker,
        MMMedallion,
        TRMedallion,
        TRKeyDoorsToMiddleExit,
        WaterWalk,
        WaterWalkFromWaterfallCave,
        LWMirror,
        DWMirror,
        Armos,
        Lanmolas,
        Moldorm,
        HelmasaurKingSB,
        HelmasaurKing,
        ArrghusSB,
        Arrghus,
        MothulaSB,
        Mothula,
        BlindSB,
        Blind,
        KholdstareSB,
        Kholdstare,
        VitreousSB,
        Vitreous,
        TrinexxSB,
        Trinexx,
        AgaBoss,
        UnknownBoss,
        ATBoss,
        EPBoss,
        DPBoss,
        ToHBoss,
        PoDBoss,
        SPBoss,
        SWBoss,
        TTBoss,
        IPBoss,
        MMBoss,
        TRBoss,
        GTBoss1,
        GTBoss2,
        GTBoss3,
        GTFinalBoss
    }
}
