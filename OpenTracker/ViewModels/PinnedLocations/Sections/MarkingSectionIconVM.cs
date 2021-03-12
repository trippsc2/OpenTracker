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

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This class contains the section marking icon control ViewModel data.
    /// </summary>
    public class MarkingSectionIconVM : ViewModelBase, ISectionIconVM
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
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public delegate MarkingSectionIconVM Factory(IMarkableSection section);

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
        /// The marking to be represented.
        /// </param>
        public MarkingSectionIconVM(
            IMarkingImageDictionary markingImages, IMarkingSelectFactory markingSelectFactory, IMarkableSection section)
        {
            _markingImages = markingImages;
            _marking = section.Marking;

            MarkingSelect = markingSelectFactory.GetMarkingSelectVM(section);
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

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
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                OpenMarkingSelect();
            }
        }
    }
}
