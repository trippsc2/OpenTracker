using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Threading.Tasks;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This class contains item section icon control ViewModel data.
    /// </summary>
    public class ItemSectionIconVM : ViewModelBase, ISectionIconVMBase
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IItemSection _section;

        public string ImageSource
        {
            get
            {
                if (!_section.IsAvailable())
                {
                    return "avares://OpenTracker/Assets/Images/chest2.png";
                }
                
                switch (_section.Accessibility)
                {
                    case AccessibilityLevel.None:
                    case AccessibilityLevel.Inspect:
                        return "avares://OpenTracker/Assets/Images/chest0.png";
                    case AccessibilityLevel.Partial:
                    case AccessibilityLevel.SequenceBreak:
                    case AccessibilityLevel.Normal:
                        return "avares://OpenTracker/Assets/Images/chest1.png";
                    default:
                        return "avares://OpenTracker/Assets/Images/chest2.png";
                }

            }
        }
        public string AvailableCount => _section.Available.ToString(CultureInfo.InvariantCulture);
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public delegate ItemSectionIconVM Factory(IItemSection section);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="section">
        /// The item section to be represented.
        /// </param>
        public ItemSectionIconVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory, IItemSection section)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _section = section;
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            _section.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItemSection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Accessibility))
            {
                await UpdateImage();
            }

            if (e.PropertyName == nameof(ISection.Available))
            {
                await UpdateImage();
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(AvailableCount)));
            }
        }

        /// <summary>
        /// Updates the ImageSource property asynchronously.
        /// </summary>
        private async Task UpdateImage()
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
        }

        /// <summary>
        /// Creates an undoable action to collect the section and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        private void CollectSection(bool force)
        {
            _undoRedoManager.NewAction(_undoableFactory.GetCollectSection(_section, force));
        }

        /// <summary>
        /// Creates an undoable action to un-collect the section and send it to the undo/redo manager.
        /// </summary>
        private void UncollectSection()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetUncollectSection(_section));
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
                    CollectSection((e.KeyModifiers & KeyModifiers.Control) > 0);
                }
                    break;
                case MouseButton.Right:
                {
                    UncollectSection();
                }
                    break;
            }
        }
    }
}
