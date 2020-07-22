using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.SequenceBreaks
{
    /// <summary>
    /// This is the ViewModel of the sequence break dialog window.
    /// </summary>
    public class SequenceBreakDialogVM : ViewModelBase
    {
        public ObservableCollection<SequenceBreakControlVM> BombDuplication { get; }
        public ObservableCollection<SequenceBreakControlVM> BombJumps { get; }
        public ObservableCollection<SequenceBreakControlVM> DarkRooms { get; }
        public ObservableCollection<SequenceBreakControlVM> FakeFlippersWaterWalk { get; }
        public ObservableCollection<SequenceBreakControlVM> SuperBunny { get; }
        public ObservableCollection<SequenceBreakControlVM> Other { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bombDuplication">
        /// The observable collection of bomb duplication sequence break control ViewModel
        /// instances.
        /// </param>
        /// <param name="bombJumps">
        /// The observable collection of bomb jump sequence break control ViewModel instances.
        /// </param>
        /// <param name="darkRooms">
        /// The observable collection of dark room sequence break control ViewModel instances.
        /// </param>
        /// <param name="fakeFlippersWaterWalk">
        /// The observable collection of fake flippers and water walk sequence break control
        /// ViewModel instances.
        /// </param>
        /// <param name="superBunny">
        /// The observable collection of super bunny sequence break control ViewModel instances.
        /// </param>
        /// <param name="other">
        /// The observable collection of other sequence break control ViewModel instances.
        /// </param>
        public SequenceBreakDialogVM(
            ObservableCollection<SequenceBreakControlVM> bombDuplication,
            ObservableCollection<SequenceBreakControlVM> bombJumps,
            ObservableCollection<SequenceBreakControlVM> darkRooms,
            ObservableCollection<SequenceBreakControlVM> fakeFlippersWaterWalk,
            ObservableCollection<SequenceBreakControlVM> superBunny,
            ObservableCollection<SequenceBreakControlVM> other)
        {
            BombDuplication = bombDuplication ??
                throw new ArgumentNullException(nameof(bombDuplication));
            BombJumps = bombJumps ?? throw new ArgumentNullException(nameof(bombJumps));
            DarkRooms = darkRooms ?? throw new ArgumentNullException(nameof(darkRooms));
            FakeFlippersWaterWalk = fakeFlippersWaterWalk ??
                throw new ArgumentNullException(nameof(fakeFlippersWaterWalk));
            SuperBunny = superBunny ?? throw new ArgumentNullException(nameof(superBunny));
            Other = other ?? throw new ArgumentNullException(nameof(other));
        }
    }
}
