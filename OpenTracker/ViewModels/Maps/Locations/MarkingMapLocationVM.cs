using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;
using System.Threading.Tasks;

namespace OpenTracker.ViewModels.Maps.Locations
{
    /// <summary>
    /// This class contains the map location marking control ViewModel data.
    /// </summary>
    public class MarkingMapLocationVM : ViewModelBase, IMarkingMapLocationVM
    {
        private readonly IMarkingImageDictionary _markingImages;

        private readonly IMarking _marking;

        private IMarkingImageVMBase? _image;
        public IMarkingImageVMBase Image
        {
            get => _image!;
            private set => this.RaiseAndSetIfChanged(ref _image, value);
        }

        public IMarkingSelectVM MarkingSelect { get; }
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="markingImages">
        /// The marking image control dictionary.
        /// </param>
        /// <param name="markingSelectFactory">
        /// A factory for creating marking select controls.
        /// </param>
        /// <param name="section">
        /// The section marking to be represented.
        /// </param>
        public MarkingMapLocationVM(
            IMarkingImageDictionary markingImages, IMarkingSelectFactory markingSelectFactory, IMarkableSection section)
        {
            _markingImages = markingImages;

            _marking = section.Marking;

            MarkingSelect = markingSelectFactory.GetMarkingSelectVM(section);
            
            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

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

        /// <summary>
        /// Opens the marking select popup.
        /// </summary>
        private void OpenMarkingSelect()
        {
            MarkingSelect.PopupOpen = true;
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The PointerReleased event args.
        /// </param>
        private void HandleClick(PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                OpenMarkingSelect();
            }
        }
    }
}
