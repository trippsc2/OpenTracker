using Avalonia.Input;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;
using Avalonia.Threading;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This class contains big key/compass/map small items panel control ViewModel data.
    /// </summary>
    public class SmallItemVM : ViewModelBase, ISmallItemVMBase
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IItem _item;
        private readonly IRequirement _requirement;
        private readonly string _imageSourceBase;

        public bool Visible =>
            _requirement.Met;
        public string ImageSource =>
            _imageSourceBase + (_item.Current > 0 ? "1" : "0") + ".png";
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        public delegate SmallItemVM Factory(
            IItem item, IRequirement requirement, string imageSourceBase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="imageSourceBase">
        /// A string representing the base image source.
        /// </param>
        /// <param name="item">
        /// The item of the key to be represented.
        /// </param>
        /// <param name="requirement">
        /// The requirement for displaying the control.
        /// </param>
        public SmallItemVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            IItem item, IRequirement requirement, string imageSourceBase)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _imageSourceBase = imageSourceBase;
            _item = item;
            _requirement = requirement;

            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
            
            _item.PropertyChanged += OnItemChanged;
            _requirement.PropertyChanged += OnRequirementChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItem interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
            }
        }
        
        /// <summary>
        /// Creates an undoable action to add an item and sends it to the undo/redo manager.
        /// </summary>
        private void AddItem()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetAddItem(_item));
        }

        /// <summary>
        /// Creates an undoable action to remove an item and sends it to the undo/redo manager.
        /// </summary>
        private void RemoveItem()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetRemoveItem(_item));
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The pointer released event args.
        /// </param>
        private void HandleClick(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                {
                    AddItem();
                }
                    break;
                case MouseButton.Right:
                {
                    RemoveItem();
                }
                    break;
            }
        }
    }
}
