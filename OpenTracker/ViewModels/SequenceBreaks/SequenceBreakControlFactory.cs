using System;
using System.Collections.Generic;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.ViewModels.SequenceBreaks;

/// <summary>
/// This is a class for creating new sequence break controls.
/// </summary>
public class SequenceBreakControlFactory : ISequenceBreakControlFactory
{
    private readonly ISequenceBreakDictionary _sequenceBreakDictionary;
    private readonly ISequenceBreakControlVM.Factory _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="sequenceBreakDictionary">
    /// The sequence break dictionary.
    /// </param>
    /// <param name="factory">
    /// The Autofac factory for creating new sequence break controls.
    /// </param>
    public SequenceBreakControlFactory(
        ISequenceBreakDictionary sequenceBreakDictionary,
        ISequenceBreakControlVM.Factory factory)
    {
        _sequenceBreakDictionary = sequenceBreakDictionary;
        _factory = factory;
    }

    /// <summary>
    /// Returns a string representing the name of the specified sequence break.
    /// </summary>
    /// <param name="type">
    /// The sequence break type.
    /// </param>
    /// <returns>
    /// A string representing the name.
    /// </returns>
    private static string GetSequenceBreakName(SequenceBreakType type)
    {
        return type switch
        {
            SequenceBreakType.BlindPedestal => "Blind Pedestal (No Book)",
            SequenceBreakType.BonkOverLedge => "Bonk over Ledge",
            SequenceBreakType.BumperCaveHookshot => "Bumper Cape without Hookshot",
            SequenceBreakType.SpikeCave => "Spike Cave",
            SequenceBreakType.TRLaserSkip => "TR Laser Bridge without safety",
            SequenceBreakType.LanmolasBombs => "Lanmolas - Bombs only",
            SequenceBreakType.HelmasaurKingBasic => "Helmasaur King",
            SequenceBreakType.ArrghusBasic => "Arrghus",
            SequenceBreakType.MothulaBasic => "Mothula",
            SequenceBreakType.BlindBasic => "Blind",
            SequenceBreakType.KholdstareBasic => "Kholdstare",
            SequenceBreakType.VitreousBasic => "Vitreous",
            SequenceBreakType.TrinexxBasic => "Trinexx",
            SequenceBreakType.BombDuplicationAncillaOverload => "Ancilla Overload",
            SequenceBreakType.BombDuplicationMirror => "Mirror",
            SequenceBreakType.BombJumpPoDHammerJump => "Palace of Darkness - Hammer Jump",
            SequenceBreakType.BombJumpSWBigChest => "Skull Woods - Big Chest",
            SequenceBreakType.BombJumpIPBJ => "Ice Palace - IPBJ",
            SequenceBreakType.BombJumpIPHookshotGap => "Ice Palace - Hookshot Gap",
            SequenceBreakType.BombJumpIPFreezorRoomGap => "Ice Palace - Freezor Room Gap",
            SequenceBreakType.DarkRoomDeathMountainEntry => "Death Mountain Entry",
            SequenceBreakType.DarkRoomDeathMountainExit => "Death Mountain Exit",
            SequenceBreakType.DarkRoomHC => "Hyrule Castle",
            SequenceBreakType.DarkRoomAT => "Agahnim's Tower",
            SequenceBreakType.DarkRoomEPRight => "Eastern Palace - Right",
            SequenceBreakType.DarkRoomEPBack => "Eastern Palace - Back",
            SequenceBreakType.DarkRoomPoDDarkBasement => "Palace of Darkness - Dark Basement",
            SequenceBreakType.DarkRoomPoDDarkMaze => "Palace of Darkness - Dark Maze",
            SequenceBreakType.DarkRoomPoDBossArea => "Palace of Darkness - Boss Area",
            SequenceBreakType.DarkRoomMM => "Misery Mire",
            SequenceBreakType.DarkRoomTR => "Turtle Rock",
            SequenceBreakType.FakeFlippersFairyRevival => "Fairy Revival",
            SequenceBreakType.FakeFlippersQirnJump => "Qirn Jump",
            SequenceBreakType.FakeFlippersScreenTransition => "Screen Transition",
            SequenceBreakType.FakeFlippersSplashDeletion => "Splash Deletion",
            SequenceBreakType.WaterWalk => "Water Walk",
            SequenceBreakType.WaterWalkFromWaterfallCave => "Water Walk from Waterfall Cave",
            SequenceBreakType.SuperBunnyMirror => "Mirror",
            SequenceBreakType.SuperBunnyFallInHole => "Fall in Hole",
            SequenceBreakType.CameraUnlock => "Camera Unlock",
            SequenceBreakType.DungeonRevive => "Dungeon Revive",
            SequenceBreakType.FakePowder => "Fake Powder",
            SequenceBreakType.Hover => "Hover",
            SequenceBreakType.MimicClip => "Mimic Clip",
            SequenceBreakType.ToHHerapot => "Tower of Hera - Herapot",
            SequenceBreakType.IPIceBreaker => "Ice Palace - Icebreaker",
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }

    /// <summary>
    /// Returns a string representing the tooltip text of the specified sequence break.
    /// </summary>
    /// <param name="type">
    /// The sequence break type.
    /// </param>
    /// <returns>
    /// A string representing the tooltip text.
    /// </returns>
    private static string GetSequenceBreakToolTip(SequenceBreakType type)
    {
        return type switch
        {
            _ => GetSequenceBreakName(type)
        };
    }

    /// <summary>
    /// Returns a new sequence break control ViewModel.
    /// </summary>
    /// <param name="type">
    /// The type of sequence break.
    /// </param>
    /// <returns>
    /// A new sequence break control ViewModel.
    /// </returns>
    private ISequenceBreakControlVM GetSequenceBreakControl(SequenceBreakType type)
    {
        return _factory(
            _sequenceBreakDictionary[type], GetSequenceBreakName(type), GetSequenceBreakToolTip(type));
    }

    /// <summary>
    /// Returns the list of bomb duplication sequence break control ViewModel
    /// instances.
    /// </summary>
    /// <returns>
    /// The list of bomb duplication sequence break control ViewModel instances.
    /// </returns>
    public List<ISequenceBreakControlVM> GetBombDuplicationSequenceBreaks()
    {
        var bombDuplicationSequenceBreaks = new List<ISequenceBreakControlVM>();

        for (var i = (int)SequenceBreakType.BombDuplicationAncillaOverload;
             i <= (int)SequenceBreakType.BombDuplicationMirror; i++)
        {
            var type = (SequenceBreakType)i;
            bombDuplicationSequenceBreaks.Add(GetSequenceBreakControl(type));
        }

        return bombDuplicationSequenceBreaks;
    }

    /// <summary>
    /// Returns the list of bomb jump sequence break control ViewModel
    /// instances.
    /// </summary>
    /// <returns>
    /// The list of bomb jump sequence break control ViewModel instances.
    /// </returns>
    public List<ISequenceBreakControlVM> GetBombJumpsSequenceBreaks()
    {
        var bombJumpsSequenceBreaks = new List<ISequenceBreakControlVM>();

        for (var i = (int)SequenceBreakType.BombJumpPoDHammerJump;
             i <= (int)SequenceBreakType.BombJumpIPFreezorRoomGap; i++)
        {
            var type = (SequenceBreakType)i;
            bombJumpsSequenceBreaks.Add(GetSequenceBreakControl(type));
        }

        return bombJumpsSequenceBreaks;
    }

    /// <summary>
    /// Returns the list of dark room sequence break control ViewModel
    /// instances.
    /// </summary>
    /// <returns>
    /// The list of dark room sequence break control ViewModel instances.
    /// </returns>
    public List<ISequenceBreakControlVM> GetDarkRoomsSequenceBreaks()
    {
        var darkRoomsSequenceBreaks = new List<ISequenceBreakControlVM>();

        for (var i = (int)SequenceBreakType.DarkRoomDeathMountainEntry; i <= (int)SequenceBreakType.DarkRoomTR; i++)
        {
            var type = (SequenceBreakType)i;
            darkRoomsSequenceBreaks.Add(GetSequenceBreakControl(type));
        }

        return darkRoomsSequenceBreaks;
    }

    /// <summary>
    /// Returns the observable collection of fake flippers and water walk sequence break
    /// control ViewModel instances.
    /// </summary>
    /// <returns>
    /// The observable collection of fake flippers and water walk sequence break control
    /// ViewModel instances.
    /// </returns>
    public List<ISequenceBreakControlVM> GetFakeFlippersWaterWalkSequenceBreaks()
    {
        var fakeFlippersWaterWalkSequenceBreaks = new List<ISequenceBreakControlVM>();

        for (var i = (int)SequenceBreakType.FakeFlippersFairyRevival;
             i <= (int)SequenceBreakType.WaterWalkFromWaterfallCave; i++)
        {
            var type = (SequenceBreakType)i;
            fakeFlippersWaterWalkSequenceBreaks.Add(GetSequenceBreakControl(type));
        }

        return fakeFlippersWaterWalkSequenceBreaks;
    }

    /// <summary>
    /// Returns the list of super bunny sequence break control ViewModel
    /// instances.
    /// </summary>
    /// <returns>
    /// The list of super bunny sequence break control ViewModel instances.
    /// </returns>
    public List<ISequenceBreakControlVM> GetSuperBunnySequenceBreaks()
    {
        var superBunnySequenceBreaks = new List<ISequenceBreakControlVM>();

        for (var i = (int)SequenceBreakType.SuperBunnyMirror; i <= (int)SequenceBreakType.SuperBunnyFallInHole; i++)
        {
            var type = (SequenceBreakType)i;
            superBunnySequenceBreaks.Add(GetSequenceBreakControl(type));
        }

        return superBunnySequenceBreaks;
    }

    /// <summary>
    /// Returns the list of other sequence break control ViewModel instances.
    /// </summary>
    /// <returns>
    /// The list of other sequence break control ViewModel instances.
    /// </returns>
    public List<ISequenceBreakControlVM> GetOtherSequenceBreaks()
    {
        var otherSequenceBreaks = new List<ISequenceBreakControlVM>();

        for (var i = (int)SequenceBreakType.CameraUnlock; i <= (int)SequenceBreakType.IPIceBreaker; i++)
        {
            var type = (SequenceBreakType)i;
            otherSequenceBreaks.Add(GetSequenceBreakControl(type));
        }

        return otherSequenceBreaks;
    }
}