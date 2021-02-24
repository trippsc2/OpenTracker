using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings.Images;

namespace OpenTracker.ViewModels.Markings
{
    /// <summary>
    /// This is the ViewModel for the marking select button control.
    /// </summary>
    public class MarkingSelectButtonVM : ViewModelBase, IMarkingSelectItemVMBase
    {
        public MarkType? Marking { get; }
        public IMarkingImageVMBase? Image { get; }

        public delegate MarkingSelectButtonVM Factory(MarkType? marking);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marking">
        /// The marking to be represented by this button.
        /// </param>
        public MarkingSelectButtonVM(IMarkingImageDictionary markingImages, MarkType? marking)
        {
            Marking = marking;

            if (Marking != null)
            {
                Image = markingImages[Marking.Value];
            }
        }
    }
}
