using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using Avalonia.Input;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This class contains crystal requirement large items panel control ViewModel data.
    /// </summary>
    public class CrystalRequirementLargeItemVM : ViewModelBase, ILargeItemVMBase
    {
        private readonly IColorSettings _colorSettings;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly ICrystalRequirementItem _item;

        public string ImageSource { get; }

        public string ImageCount =>
            _item.Known ? (7 - _item.Current).ToString(CultureInfo.InvariantCulture) : "?";

        public string TextColor
        {
            get
            {
                if (_item.Known)
                {
                    return _item.Current == 0 ? _colorSettings.EmphasisFontColor : "#ffffffff";
                }

                return _colorSettings.AccessibilityColors[AccessibilityLevel.SequenceBreak];
            }
        }

        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        public delegate CrystalRequirementLargeItemVM Factory(
            ICrystalRequirementItem item, string imageSource);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="colorSettings">
        /// The color settings data.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// The factory for creating undoable actions.
        /// </param>
        /// <param name="imageSource">
        /// A string representing the image source.
        /// </param>
        /// <param name="item">
        /// An item that is to be represented by this control.
        /// </param>
        public CrystalRequirementLargeItemVM(
            IColorSettings colorSettings, IUndoRedoManager undoRedoManager,
            IUndoableFactory undoableFactory, ICrystalRequirementItem item, string imageSource)
        {
            _colorSettings = colorSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _item = item;

            ImageSource = imageSource;

            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

            _item.PropertyChanged += OnItemChanged;
            _colorSettings.PropertyChanged += OnColorsChanged;
            _colorSettings.AccessibilityColors.PropertyChanged += OnColorsChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ICrystalRequirementItem interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ICrystalRequirementItem.Current) ||
                e.PropertyName == nameof(ICrystalRequirementItem.Known))
            {
                this.RaisePropertyChanged(nameof(ImageCount));
                UpdateTextColor();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ColorSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnColorsChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTextColor();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the TextColor property.
        /// </summary>
        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(TextColor));
        }

        /// <summary>
        /// Creates an undoable action to add to the crystal requirement item and sends it to the undo/redo manager.
        /// </summary>
        private void AddItem()
        {
            _undoRedoManager.Execute(_undoableFactory.GetAddCrystalRequirement(_item));
        }

        /// <summary>
        /// Creates an undoable action to remove to the crystal requirement item and sends it to the undo/redo manager.
        /// </summary>
        private void RemoveItem()
        {
            _undoRedoManager.Execute(_undoableFactory.GetRemoveCrystalRequirement(_item));
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