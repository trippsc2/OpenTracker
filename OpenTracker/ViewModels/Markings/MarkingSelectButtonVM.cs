using OpenTracker.Models.Markings;
using OpenTracker.ViewModels.Markings.Images;

namespace OpenTracker.ViewModels.Markings
{
    /// <summary>
    /// This is the ViewModel for the marking select button control.
    /// </summary>
    public class MarkingSelectButtonVM : ViewModelBase
    {
        public MarkType? Marking { get; }
        public MarkingImageVMBase Image { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marking">
        /// The marking to be represented by this button.
        /// </param>
        public MarkingSelectButtonVM(MarkType? marking)
        {
            Marking = marking;

            if (Marking != null)
            {
                Image = MarkingImageDictionary.Instance[Marking.Value];
            }
        }
    }
}
