using Avalonia.Threading;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;
using System.ComponentModel;
using System.Threading.Tasks;

namespace OpenTracker.ViewModels.Maps.Locations.Tooltip
{
    /// <summary>
    /// This class contains the map location tooltip marking control ViewModel data.
    /// </summary>
    public class MapLocationToolTipMarkingVM : ViewModelBase, IMapLocationToolTipMarkingVM
    {
        private readonly IMarkingImageDictionary _markingImages;
        private readonly IMarking _marking;

        public object Model => _marking;

        private IMarkingImageVMBase? _image;
        public IMarkingImageVMBase Image
        {
            get => _image!;
            private set => this.RaiseAndSetIfChanged(ref _image, value);
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="markingImages">
        /// The marking image control dictionary.
        /// </param>
        /// <param name="marking">
        /// The marking to be represented.
        /// </param>
        public MapLocationToolTipMarkingVM(IMarkingImageDictionary markingImages, IMarking marking)
        {
            _markingImages = markingImages;

            _marking = marking;

            _marking.PropertyChanged += OnMarkingChanged;

            UpdateImage();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMarking interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnMarkingChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMarking.Mark))
            {
                await UpdateImageAsync();
            }
        }

        /// <summary>
        /// Updates the image.
        /// </summary>
        private void UpdateImage()
        {
            Image = _markingImages[_marking.Mark];
        }

        /// <summary>
        /// Updates the image asynchronously.
        /// </summary>
        private async Task UpdateImageAsync()
        {
            await Dispatcher.UIThread.InvokeAsync(UpdateImage);
        }
    }
}
