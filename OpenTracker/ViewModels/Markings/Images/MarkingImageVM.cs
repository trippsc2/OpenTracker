using System;

namespace OpenTracker.ViewModels.Markings.Images
{
    /// <summary>
    /// This is the ViewModel class for the marking image control.
    /// </summary>
    public class MarkingImageVM : MarkingImageVMBase
    {
        public string ImageSource { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imageSource">
        /// A string representing the image source.
        /// </param>
        public MarkingImageVM(string imageSource)
        {
            ImageSource = imageSource ?? throw new ArgumentNullException(nameof(imageSource));
        }
    }
}
