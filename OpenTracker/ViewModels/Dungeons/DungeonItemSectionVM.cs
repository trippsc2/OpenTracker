using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.Dungeons
{
    /// <summary>
    /// This class contains dungeon items small items panel control ViewModel data.
    /// </summary>
    public class DungeonItemSectionVM : ViewModelBase, IDungeonItemSectionVM
    {
        private readonly IColorSettings _colorSettings;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly ISection _section;

        public string FontColor =>
            _section.Available == 0 ? "#ffffffff" : _colorSettings.AccessibilityColors[_section.Accessibility];
        public string ImageSource =>
            "avares://OpenTracker/Assets/Images/chest" +
            (_section.IsAvailable() ? _section.Accessibility switch
            {
                AccessibilityLevel.None => "0",
                AccessibilityLevel.Inspect => "0",
                _ => "1"
            } : "2") + ".png";

        public string NumberString => _section.Available.ToString(CultureInfo.InvariantCulture);
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }
        
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
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="section">
        /// The dungeon section to be represented.
        /// </param>
        public DungeonItemSectionVM(
            IColorSettings colorSettings, IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            ISection section)
        {
            _colorSettings = colorSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _section = section;
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);
            
            _colorSettings.AccessibilityColors.PropertyChanged += OnColorChanged;
            _section.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ObservableCollection class for the
        /// accessibility colors.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnColorChanged(object sender, PropertyChangedEventArgs e)
        {
            await UpdateTextColor();
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
            if (e.PropertyName == nameof(ISection.Accessibility))
            {
                await UpdateTextColor();
                await UpdateImage();
            }

            if (e.PropertyName == nameof(ISection.Available))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(NumberString)));
                await UpdateTextColor();
                await UpdateImage();
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the FontColor property.
        /// </summary>
        private async Task UpdateTextColor()
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(FontColor)));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the ImageSource property.
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
        /// Creates an undoable action to un-collect the section and sends it to the undo/redo manager.
        /// </summary>
        private void UncollectSection()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetUncollectSection(_section));
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
                    CollectSection((e.KeyModifiers & KeyModifiers.Control) > 0);
                    break;
                case MouseButton.Right:
                    UncollectSection();
                    break;
            }
        }
    }
}
