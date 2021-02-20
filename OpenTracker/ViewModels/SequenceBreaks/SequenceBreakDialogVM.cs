using OpenTracker.Utils.Dialog;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.SequenceBreaks
{
    /// <summary>
    /// This is the ViewModel of the sequence break dialog window.
    /// </summary>
    public class SequenceBreakDialogVM : DialogViewModelBase, ISequenceBreakDialogVM
    {
        private readonly ISequenceBreakControlFactory _factory;

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
            _factory = factory;

            BombDuplication = _factory.GetBombDuplicationSequenceBreaks();
            BombJumps = _factory.GetBombJumpsSequenceBreaks();
            DarkRooms = _factory.GetDarkRoomsSequenceBreaks();
            FakeFlippersWaterWalk = _factory.GetFakeFlippersWaterWalkSequenceBreaks();
            SuperBunny = _factory.GetSuperBunnySequenceBreaks();
            Other = _factory.GetOtherSequenceBreaks();
        }
    }
}
