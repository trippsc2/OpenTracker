using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This class contains the prize section icon control ViewModel data.
    /// </summary>
    public class PrizeSectionIconVM : ViewModelBase, ISectionIconVM
    {
        private readonly IPrizeDictionary _prizes;
        private readonly IUndoRedoManager _undoRedoManager;

        private readonly IPrizeSection _section;

        public string ImageSource
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("avares://OpenTracker/Assets/Images/Prizes/");

                if (_section.PrizePlacement.Prize == null)
                {
                    sb.Append("unknown");
                }
                else
                {
                    sb.Append(
                        _prizes.FirstOrDefault(
                            x => x.Value == _section.PrizePlacement.Prize).Key.ToString()
                                .ToLowerInvariant());
                }

                sb.Append(_section.IsAvailable() ? "0.png" : "1.png");

                return sb.ToString();
            }
        }
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public delegate PrizeSectionIconVM Factory(IPrizeSection section);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prizes">
        /// The prize dictionary.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="section">
        /// The prize section to be presented.
        /// </param>
        public PrizeSectionIconVM(IPrizeDictionary prizes, IUndoRedoManager undoRedoManager, IPrizeSection section)
        {
            _prizes = prizes;
            _undoRedoManager = undoRedoManager;

            _section = section;
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            _section.PropertyChanged += OnSectionChanged;
            _section.PrizePlacement.PropertyChanged += OnPrizeChanged; 
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection interface.
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
                await UpdateImage();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IPrizePlacement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnPrizeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPrizePlacement.Prize))
            {
                await UpdateImage();
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the ImageSource property.
        /// </summary>
        private async Task UpdateImage()
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
        }

        /// <summary>
        /// Creates an undoable action to toggle the prize section and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        private void TogglePrize(bool force = false)
        {
            _undoRedoManager.NewAction(_section.CreateTogglePrizeSectionAction(force));
        }

        /// <summary>
        /// Creates an undoable action to change the prize and sends it to the undo/redo manager.
        /// </summary>
        private void ChangePrize()
        {
            _undoRedoManager.NewAction(_section.PrizePlacement.CreateChangePrizeAction());
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The PointerReleased event args.
        /// </param>
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                {
                    TogglePrize((e.KeyModifiers & KeyModifiers.Control) > 0);
                }
                    break;
                case MouseButton.Right:
                {
                    ChangePrize();
                }
                    break;
            }
        }
    }
}
