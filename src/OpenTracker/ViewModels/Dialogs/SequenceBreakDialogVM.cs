using System.Collections.Generic;
using OpenTracker.Utils.Dialog;
using OpenTracker.ViewModels.SequenceBreaks;

namespace OpenTracker.ViewModels.Dialogs
{
    /// <summary>
    /// This class contains the sequence break dialog window ViewModel data.
    /// </summary>
    public class SequenceBreakDialogVM : DialogViewModelBase, ISequenceBreakDialogVM
    {
        public List<ISequenceBreakControlVM> BombDuplication { get; }
        public List<ISequenceBreakControlVM> BombJumps { get; }
        public List<ISequenceBreakControlVM> DarkRooms { get; }
        public List<ISequenceBreakControlVM> FakeFlippersWaterWalk { get; }
        public List<ISequenceBreakControlVM> SuperBunny { get; }
        public List<ISequenceBreakControlVM> Other { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The factory for creating sequence break controls.
        /// </param>
        public SequenceBreakDialogVM(ISequenceBreakControlFactory factory)
        {
            BombDuplication = factory.GetBombDuplicationSequenceBreaks();
            BombJumps = factory.GetBombJumpsSequenceBreaks();
            DarkRooms = factory.GetDarkRoomsSequenceBreaks();
            FakeFlippersWaterWalk = factory.GetFakeFlippersWaterWalkSequenceBreaks();
            SuperBunny = factory.GetSuperBunnySequenceBreaks();
            Other = factory.GetOtherSequenceBreaks();
        }
    }
}
