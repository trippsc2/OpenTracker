using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Sections;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items.Adapters
{
    /// <summary>
    /// This class contains the logic to adapt prize data to an item control.
    /// </summary>
    public class StaticPrizeAdapter : ViewModelBase, IItemAdapter
    {
        private readonly IPrizeSection _section;
        private readonly string _imageSourceBase;

        public string ImageSource => _imageSourceBase + (_section.IsAvailable() ? "0.png" : "1.png");
        public string? Label { get; } = null;
        public string LabelColor { get; } = "#ffffffff";

        public IBossSelectPopupVM? BossSelect { get; } = null;
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public delegate StaticPrizeAdapter Factory(IPrizeSection section, string imageSourceBase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imageSourceBase">
        /// A string representing the base image source.
        /// </param>
        /// <param name="section">
        /// An item that is to be represented by this control.
        /// </param>
        public StaticPrizeAdapter(IPrizeSection section, string imageSourceBase)
        {
            _section = section;
            _imageSourceBase = imageSourceBase;
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            _section.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IPrizeSection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Available))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
            }
        }

        /// <summary>
        /// Creates an undoable action to collect the prize section, ignoring logic, and sends it to the undo/redo
        /// manager.
        /// </summary>
        private void CollectSection()
        {
            _section.CollectSection(true);
        }

        /// <summary>
        /// Creates an undoable action to un-collect the prize section and sends it to the undo/redo manager.
        /// </summary>
        private void UncollectSection()
        {
            _section.UncollectSection();
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The pointer released event args.
        /// </param>
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    CollectSection();
                    break;
                case MouseButton.Right:
                    UncollectSection();
                    break;
            }
        }
    }
}