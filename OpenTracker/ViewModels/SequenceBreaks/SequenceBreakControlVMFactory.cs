using OpenTracker.Models.SequenceBreaks;
using System;

namespace OpenTracker.ViewModels.SequenceBreaks
{
    /// <summary>
    /// This is the class containing creation logic for sequence break control ViewModel classes.
    /// </summary>
    public static class SequenceBreakControlVMFactory
    {
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
            switch (type)
            {
                case SequenceBreakType.BlindPedestal:
                    {
                        return "Blind Pedestal (No Book)";
                    }
                case SequenceBreakType.BonkOverLedge:
                    {
                        return "Bonk over Ledge";
                    }
                case SequenceBreakType.BumperCaveHookshot:
                    {
                        return "Bumper Cape without Hookshot";
                    }
                case SequenceBreakType.SpikeCave:
                    {
                        return "Spike Cave";
                    }
                case SequenceBreakType.TRLaserSkip:
                    {
                        return "TR Laser Bridge without safety";
                    }
                case SequenceBreakType.LanmolasBombs:
                    {
                        return "Lanmolas - Bombs only";
                    }
                case SequenceBreakType.ArrghusBasic:
                    {
                        return "Arrghus";
                    }
                case SequenceBreakType.MothulaBasic:
                    {
                        return "Mothula";
                    }
                case SequenceBreakType.BlindBasic:
                    {
                        return "Blind";
                    }
                case SequenceBreakType.KholdstareBasic:
                    {
                        return "Kholdstare";
                    }
                case SequenceBreakType.VitreousBasic:
                    {
                        return "Vitreous";
                    }
                case SequenceBreakType.TrinexxBasic:
                    {
                        return "Trinexx";
                    }
                case SequenceBreakType.BombDuplicationAncillaOverload:
                    {
                        return "Ancilla Overload";
                    }
                case SequenceBreakType.BombDuplicationMirror:
                    {
                        return "Mirror";
                    }
                case SequenceBreakType.BombJumpPoDHammerJump:
                    {
                        return "Palace of Darkness - Hammer Jump";
                    }
                case SequenceBreakType.BombJumpSWBigChest:
                    {
                        return "Skull Woods - Big Chest";
                    }
                case SequenceBreakType.BombJumpIPBJ:
                    {
                        return "Ice Palace - IPBJ";
                    }
                case SequenceBreakType.BombJumpIPHookshotGap:
                    {
                        return "Ice Palace - Hookshot Gap";
                    }
                case SequenceBreakType.BombJumpIPFreezorRoomGap:
                    {
                        return "Ice Palace - Freezor Room Gap";
                    }
                case SequenceBreakType.DarkRoomDeathMountainEntry:
                    {
                        return "Death Mountain Entry";
                    }
                case SequenceBreakType.DarkRoomDeathMountainExit:
                    {
                        return "Death Mountain Exit";
                    }
                case SequenceBreakType.DarkRoomHC:
                    {
                        return "Hyrule Castle";
                    }
                case SequenceBreakType.DarkRoomAT:
                    {
                        return "Agahnim's Tower";
                    }
                case SequenceBreakType.DarkRoomEPRight:
                    {
                        return "Eastern Palace - Right";
                    }
                case SequenceBreakType.DarkRoomEPBack:
                    {
                        return "Eastern Palace - Back";
                    }
                case SequenceBreakType.DarkRoomPoDDarkBasement:
                    {
                        return "Palace of Darkness - Dark Basement";
                    }
                case SequenceBreakType.DarkRoomPoDDarkMaze:
                    {
                        return "Palace of Darkness - Dark Maze";
                    }
                case SequenceBreakType.DarkRoomPoDBossArea:
                    {
                        return "Palace of Darkness - Boss Area";
                    }
                case SequenceBreakType.DarkRoomMM:
                    {
                        return "Misery Mire";
                    }
                case SequenceBreakType.DarkRoomTR:
                    {
                        return "Turtle Rock";
                    }
                case SequenceBreakType.FakeFlippersFairyRevival:
                    {
                        return "Fairy Revival";
                    }
                case SequenceBreakType.FakeFlippersQirnJump:
                    {
                        return "Qirn Jump";
                    }
                case SequenceBreakType.FakeFlippersScreenTransition:
                    {
                        return "Screen Transition";
                    }
                case SequenceBreakType.FakeFlippersSplashDeletion:
                    {
                        return "Splash Deletion";
                    }
                case SequenceBreakType.WaterWalk:
                    {
                        return "Water Walk";
                    }
                case SequenceBreakType.WaterWalkFromWaterfallCave:
                    {
                        return "Water Walk from Waterfall Cave";
                    }
                case SequenceBreakType.SuperBunnyMirror:
                    {
                        return "Mirror";
                    }
                case SequenceBreakType.SuperBunnyFallInHole:
                    {
                        return "Fall in Hole";
                    }
                case SequenceBreakType.CameraUnlock:
                    {
                        return "Camera Unlock";
                    }
                case SequenceBreakType.DungeonRevive:
                    {
                        return "Dungeon Revive";
                    }
                case SequenceBreakType.FakePowder:
                    {
                        return "Fake Powder";
                    }
                case SequenceBreakType.Hover:
                    {
                        return "Hover";
                    }
                case SequenceBreakType.MimicClip:
                    {
                        return "Mimic Clip";
                    }
                case SequenceBreakType.ToHHerapot:
                    {
                        return "Tower of Hera - Herapot";
                    }
                case SequenceBreakType.IPIceBreaker:
                    {
                        return "Ice Palace - Icebreaker";
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(type));
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
            switch (type)
            {
                case SequenceBreakType.BlindPedestal:
                    {
                        return "Blind Pedestal (No Book)";
                    }
                case SequenceBreakType.BonkOverLedge:
                    {
                        return "Bonk over Ledge";
                    }
                case SequenceBreakType.BumperCaveHookshot:
                    {
                        return "Bumper Cape without Hookshot";
                    }
                case SequenceBreakType.SpikeCave:
                    {
                        return "Spike Cave";
                    }
                case SequenceBreakType.TRLaserSkip:
                    {
                        return "TR Laser Bridge without safety";
                    }
                case SequenceBreakType.LanmolasBombs:
                    {
                        return "Lanmolas - Bombs only";
                    }
                case SequenceBreakType.ArrghusBasic:
                    {
                        return "Arrghus";
                    }
                case SequenceBreakType.MothulaBasic:
                    {
                        return "Mothula";
                    }
                case SequenceBreakType.BlindBasic:
                    {
                        return "Blind";
                    }
                case SequenceBreakType.KholdstareBasic:
                    {
                        return "Kholdstare";
                    }
                case SequenceBreakType.VitreousBasic:
                    {
                        return "Vitreous";
                    }
                case SequenceBreakType.TrinexxBasic:
                    {
                        return "Trinexx";
                    }
                case SequenceBreakType.BombDuplicationAncillaOverload:
                    {
                        return "Ancilla Overload";
                    }
                case SequenceBreakType.BombDuplicationMirror:
                    {
                        return "Mirror";
                    }
                case SequenceBreakType.BombJumpPoDHammerJump:
                    {
                        return "Palace of Darkness - Hammer Jump";
                    }
                case SequenceBreakType.BombJumpSWBigChest:
                    {
                        return "Skull Woods - Big Chest";
                    }
                case SequenceBreakType.BombJumpIPBJ:
                    {
                        return "Ice Palace - IPBJ";
                    }
                case SequenceBreakType.BombJumpIPHookshotGap:
                    {
                        return "Ice Palace - Hookshot Gap";
                    }
                case SequenceBreakType.BombJumpIPFreezorRoomGap:
                    {
                        return "Ice Palace - Freezor Room Gap";
                    }
                case SequenceBreakType.DarkRoomDeathMountainEntry:
                    {
                        return "Death Mountain Entry";
                    }
                case SequenceBreakType.DarkRoomDeathMountainExit:
                    {
                        return "Death Mountain Exit";
                    }
                case SequenceBreakType.DarkRoomHC:
                    {
                        return "Hyrule Castle";
                    }
                case SequenceBreakType.DarkRoomAT:
                    {
                        return "Agahnim's Tower";
                    }
                case SequenceBreakType.DarkRoomEPRight:
                    {
                        return "Eastern Palace - Right";
                    }
                case SequenceBreakType.DarkRoomEPBack:
                    {
                        return "Eastern Palace - Back";
                    }
                case SequenceBreakType.DarkRoomPoDDarkBasement:
                    {
                        return "Palace of Darkness - Dark Basement";
                    }
                case SequenceBreakType.DarkRoomPoDDarkMaze:
                    {
                        return "Palace of Darkness - Dark Maze";
                    }
                case SequenceBreakType.DarkRoomPoDBossArea:
                    {
                        return "Palace of Darkness - Boss Area";
                    }
                case SequenceBreakType.DarkRoomMM:
                    {
                        return "Misery Mire";
                    }
                case SequenceBreakType.DarkRoomTR:
                    {
                        return "Turtle Rock";
                    }
                case SequenceBreakType.FakeFlippersFairyRevival:
                    {
                        return "Fairy Revival";
                    }
                case SequenceBreakType.FakeFlippersQirnJump:
                    {
                        return "Qirn Jump";
                    }
                case SequenceBreakType.FakeFlippersScreenTransition:
                    {
                        return "Screen Transition";
                    }
                case SequenceBreakType.FakeFlippersSplashDeletion:
                    {
                        return "Splash Deletion";
                    }
                case SequenceBreakType.WaterWalk:
                    {
                        return "Water Walk";
                    }
                case SequenceBreakType.WaterWalkFromWaterfallCave:
                    {
                        return "Water Walk from Waterfall Cave";
                    }
                case SequenceBreakType.SuperBunnyMirror:
                    {
                        return "Mirror";
                    }
                case SequenceBreakType.SuperBunnyFallInHole:
                    {
                        return "Fall in Hole";
                    }
                case SequenceBreakType.CameraUnlock:
                    {
                        return "Camera Unlock";
                    }
                case SequenceBreakType.DungeonRevive:
                    {
                        return "Dungeon Revive";
                    }
                case SequenceBreakType.FakePowder:
                    {
                        return "Fake Powder";
                    }
                case SequenceBreakType.Hover:
                    {
                        return "Hover";
                    }
                case SequenceBreakType.MimicClip:
                    {
                        return "Mimic Clip";
                    }
                case SequenceBreakType.ToHHerapot:
                    {
                        return "Tower of Hera - Herapot";
                    }
                case SequenceBreakType.IPIceBreaker:
                    {
                        return "Ice Palace - Icebreaker";
                    }
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }

        /// <summary>
        /// Returns a new sequence break control ViewModel instance of the specified sequence break
        /// type.
        /// </summary>
        /// <param name="type">
        /// The sequence break type.
        /// </param>
        /// <returns>
        /// A new sequence break control ViewModel instance.
        /// </returns>
        public static SequenceBreakControlVM GetSequenceBreakControlVM(SequenceBreakType type)
        {
            return new SequenceBreakControlVM(
                SequenceBreakDictionary.Instance[type], GetSequenceBreakName(type),
                GetSequenceBreakToolTip(type));
        }
    }
}
