using OpenTracker.Models.SequenceBreaks;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.SequenceBreaks
{
    /// <summary>
    /// This is the class containing creation logic for sequence break dialog window ViewModel
    /// classes.
    /// </summary>
    internal static class SequenceBreakDialogVMFactory
    {
        /// <summary>
        /// Returns the observable collection of bomb duplication sequence break control ViewModel
        /// instances.
        /// </summary>
        /// <returns>
        /// The observable collection of bomb duplication sequence break control ViewModel
        /// instances.
        /// </returns>
        private static ObservableCollection<SequenceBreakControlVM> GetBombDuplicationSequenceBreaks()
        {
            ObservableCollection<SequenceBreakControlVM> bombDuplicationSequenceBreaks =
                new ObservableCollection<SequenceBreakControlVM>();

            for (int i = (int)SequenceBreakType.BombDuplicationAncillaOverload;
                i <= (int)SequenceBreakType.BombDuplicationMirror; i++)
            {
                bombDuplicationSequenceBreaks.Add(SequenceBreakControlVMFactory
                    .GetSequenceBreakControlVM((SequenceBreakType)i));
            }

            return bombDuplicationSequenceBreaks;
        }

        /// <summary>
        /// Returns the observable collection of bomb jump sequence break control ViewModel
        /// instances.
        /// </summary>
        /// <returns>
        /// The observable collection of bomb jump sequence break control ViewModel instances.
        /// </returns>
        private static ObservableCollection<SequenceBreakControlVM> GetBombJumpsSequenceBreaks()
        {
            ObservableCollection<SequenceBreakControlVM> bombJumpsSequenceBreaks =
                new ObservableCollection<SequenceBreakControlVM>();

            for (int i = (int)SequenceBreakType.BombJumpPoDHammerJump;
                i <= (int)SequenceBreakType.BombJumpIPFreezorRoomGap; i++)
            {
                bombJumpsSequenceBreaks.Add(SequenceBreakControlVMFactory
                    .GetSequenceBreakControlVM((SequenceBreakType)i));
            }

            return bombJumpsSequenceBreaks;
        }

        /// <summary>
        /// Returns the observable collection of dark room sequence break control ViewModel
        /// instances.
        /// </summary>
        /// <returns>
        /// The observable collection of dark room sequence break control ViewModel instances.
        /// </returns>
        private static ObservableCollection<SequenceBreakControlVM> GetDarkRoomsSequenceBreaks()
        {
            ObservableCollection<SequenceBreakControlVM> darkRoomsSequenceBreaks =
                new ObservableCollection<SequenceBreakControlVM>();

            for (int i = (int)SequenceBreakType.DarkRoomDeathMountainEntry;
                i <= (int)SequenceBreakType.DarkRoomTR; i++)
            {
                darkRoomsSequenceBreaks.Add(SequenceBreakControlVMFactory
                    .GetSequenceBreakControlVM((SequenceBreakType)i));
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
        private static ObservableCollection<SequenceBreakControlVM> GetFakeFlippersWaterWalkSequenceBreaks()
        {
            ObservableCollection<SequenceBreakControlVM> fakeFlippersWaterWalkSequenceBreaks =
                new ObservableCollection<SequenceBreakControlVM>();

            for (int i = (int)SequenceBreakType.FakeFlippersFairyRevival;
                i <= (int)SequenceBreakType.WaterWalkFromWaterfallCave; i++)
            {
                fakeFlippersWaterWalkSequenceBreaks.Add(SequenceBreakControlVMFactory
                    .GetSequenceBreakControlVM((SequenceBreakType)i));
            }

            return fakeFlippersWaterWalkSequenceBreaks;
        }

        /// <summary>
        /// Returns the observable collection of super bunny sequence break control ViewModel
        /// instances.
        /// </summary>
        /// <returns>
        /// The observable collection of super bunny sequence break control ViewModel instances.
        /// </returns>
        private static ObservableCollection<SequenceBreakControlVM> GetSuperBunnySequenceBreaks()
        {
            ObservableCollection<SequenceBreakControlVM> superBunnySequenceBreaks =
                new ObservableCollection<SequenceBreakControlVM>();

            for (int i = (int)SequenceBreakType.SuperBunnyMirror;
                i <= (int)SequenceBreakType.SuperBunnyFallInHole; i++)
            {
                superBunnySequenceBreaks.Add(SequenceBreakControlVMFactory
                    .GetSequenceBreakControlVM((SequenceBreakType)i));
            }

            return superBunnySequenceBreaks;
        }

        /// <summary>
        /// Returns the observable collection of other sequence break control ViewModel instances.
        /// </summary>
        /// <returns>
        /// The observable collection of other sequence break control ViewModel instances.
        /// </returns>
        private static ObservableCollection<SequenceBreakControlVM> GetOtherSequenceBreaks()
        {
            ObservableCollection<SequenceBreakControlVM> otherSequenceBreaks =
                new ObservableCollection<SequenceBreakControlVM>();

            for (int i = (int)SequenceBreakType.CameraUnlock;
                i <= (int)SequenceBreakType.IPIceBreaker; i++)
            {
                otherSequenceBreaks.Add(SequenceBreakControlVMFactory
                    .GetSequenceBreakControlVM((SequenceBreakType)i));
            }

            return otherSequenceBreaks;
        }

        /// <summary>
        /// Returns a new sequence break dialog window ViewModel instance.
        /// </summary>
        /// <returns>
        /// A new sequence break dialog window ViewModel instance.
        /// </returns>
        internal static SequenceBreakDialogVM GetSequenceBreakDialogVM()
        {
            return new SequenceBreakDialogVM(
                GetBombDuplicationSequenceBreaks(), GetBombJumpsSequenceBreaks(),
                GetDarkRoomsSequenceBreaks(), GetFakeFlippersWaterWalkSequenceBreaks(),
                GetSuperBunnySequenceBreaks(), GetOtherSequenceBreaks());
        }
    }
}
