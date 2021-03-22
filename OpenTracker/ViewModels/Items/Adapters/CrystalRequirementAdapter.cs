using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items.Adapters
{
    /// <summary>
    /// This class contains the logic to adapt crystal requirement data to an item control. 
    /// </summary>
    public class CrystalRequirementAdapter : ViewModelBase, IItemAdapter
    {
        private readonly IColorSettings _colorSettings;
        private readonly ICrystalRequirementItem _item;
        
        public string ImageSource { get; }
        
        public string Label => _item.Known ? (7 - _item.Current).ToString(CultureInfo.InvariantCulture) : "?";
        public string LabelColor =>
            _item.Known ? _item.Current == 0 ? _colorSettings.EmphasisFontColor : "#ffffffff" :
                _colorSettings.AccessibilityColors[AccessibilityLevel.SequenceBreak];

        public IBossSelectPopupVM? BossSelect { get; } = null;
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public delegate CrystalRequirementAdapter Factory(ICrystalRequirementItem item, string imageSource);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="colorSettings">
        /// The color settings data.
        /// </param>A
        /// <param name="item">
        /// The crystal requirement item.
        /// </param>
        /// <param name="imageSource">
        /// The image source of the crystal requirement.
        /// </param>
        public CrystalRequirementAdapter(IColorSettings colorSettings, ICrystalRequirementItem item, string imageSource)
        {
            _colorSettings = colorSettings;

            _item = item;

            ImageSource = imageSource;

            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

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
        private async void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ICrystalRequirementItem.Current) ||
                e.PropertyName == nameof(ICrystalRequirementItem.Known))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Label)));
                await UpdateTextColor();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IColorSettings interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnColorsChanged(object sender, PropertyChangedEventArgs e)
        {
            await UpdateTextColor();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the TextColor property.
        /// </summary>
        private async Task UpdateTextColor()
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(LabelColor)));
        }

        /// <summary>
        /// Creates an undoable action to add to the crystal requirement item and sends it to the undo/redo manager.
        /// </summary>
        private void AddItem()
        {
            _item.AddItem();
        }

        /// <summary>
        /// Creates an undoable action to remove to the crystal requirement item and sends it to the undo/redo manager.
        /// </summary>
        private void RemoveItem()
        {
            _item.AddItem();
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