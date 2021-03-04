using Avalonia.Input;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This class contains entrance section icon control ViewModel data.
    /// </summary>
    public class EntranceSectionIconVM : ViewModelBase, ISectionIconVMBase
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IEntranceSection _section;

        public string ImageSource =>
            _section.IsAvailable() ? "avares://OpenTracker/Assets/Images/door0.png" :
            "avares://OpenTracker/Assets/Images/door1.png";
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        public delegate EntranceSectionIconVM Factory(IEntranceSection section);

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
        /// The entrance section to be represented.
        /// </param>
        public EntranceSectionIconVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory, IEntranceSection section)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _section = section;
            
            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

            _section.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IBossSection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Accessibility) ||
                e.PropertyName == nameof(ISection.Available))
            {
                this.RaisePropertyChanged(nameof(ImageSource));
            }
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
        private void HandleClick(PointerReleasedEventArgs e)
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
