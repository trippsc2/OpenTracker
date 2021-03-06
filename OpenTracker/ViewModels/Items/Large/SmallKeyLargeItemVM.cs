using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This class contains small key large items panel control ViewModel data.
    /// </summary>
    public class SmallKeyLargeItemVM : ViewModelBase, ILargeItemVMBase
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IItem _item;
        private readonly IRequirement _requirement;
        private readonly string _imageSourceBase;

        public bool Visible =>
            _requirement.Met;
        public string ImageSource => $"{_imageSourceBase}{(_item.Current > 0 ? "1" : "0")}.png";
        public string ImageCount => _item.Current.ToString(CultureInfo.InvariantCulture);
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public delegate SmallKeyLargeItemVM Factory(IItem item, string imageSourceBase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        /// The requirements dictionary.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="imageSourceBase">
        /// A string representing the image source base.
        /// </param>
        /// <param name="item">
        /// An item that is to be represented by this control.
        /// </param>
        public SmallKeyLargeItemVM(
            IRequirementDictionary requirements, IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            IItem item, string imageSourceBase)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _item = item;
            _requirement = requirements[RequirementType.GenericKeys];
            _imageSourceBase = imageSourceBase;
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            _item.PropertyChanged += OnItemChanged;
            _requirement.PropertyChanged += OnRequirementChanged;
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
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    this.RaisePropertyChanged(nameof(ImageSource));
                    this.RaisePropertyChanged(nameof(ImageCount));
                });
            }
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
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    AddItem();
                    break;
                case MouseButton.Right:
                    RemoveItem();
                    break;
            }
        }
    }
}
