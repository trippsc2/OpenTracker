﻿using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Markings.Images
{
    /// <summary>
    /// This class contains the static marking image control ViewModel data.
    /// </summary>
    public class MarkingImageVM : ViewModelBase, IMarkingImageVMBase
    {
        public string ImageSource { get; }

        public delegate MarkingImageVM Factory(string imageSource);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imageSource">
        /// A string representing the image source.
        /// </param>
        public MarkingImageVM(string imageSource)
        {
            ImageSource = imageSource;
        }
    }
}
