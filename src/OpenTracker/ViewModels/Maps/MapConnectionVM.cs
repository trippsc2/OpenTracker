using System.ComponentModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Areas;
using ReactiveUI;

namespace OpenTracker.ViewModels.Maps
{
    /// <summary>
    /// This class contains map connection control ViewModel data.
    /// </summary>
    public class MapConnectionVM : ViewModelBase, IMapConnectionVM
    {
        private readonly IColorSettings _colorSettings;
        private readonly IMode _mode;
        private readonly IUndoRedoManager _undoRedoManager;

        private readonly IMapAreaVM _mapArea;

        private readonly IMapConnection _connection;

        private bool _highlighted;
        public bool Highlighted
        {
            get => _highlighted;
            private set => this.RaiseAndSetIfChanged(ref _highlighted, value);
        }

        public bool Visible => _mode.EntranceShuffle > EntranceShuffle.None;

        public object Model => _connection;

        public Point Start => _mapArea.Orientation switch
            {
                Orientation.Vertical => _connection.Location1.Map == MapID.DarkWorld ?
                    new Point(_connection.Location1.X + 23, _connection.Location1.Y + 2046) :
                    new Point(_connection.Location1.X + 23, _connection.Location1.Y + 13),
                _ => _connection.Location1.Map == MapID.DarkWorld ?
                    new Point(_connection.Location1.X + 2046, _connection.Location1.Y + 23) :
                    new Point(_connection.Location1.X + 13, _connection.Location1.Y + 23)
            };
        public Point End =>
            _mapArea.Orientation switch
            {
                Orientation.Vertical => _connection.Location2.Map == MapID.DarkWorld ?
                    new Point(_connection.Location2.X + 23, _connection.Location2.Y + 2046) :
                    new Point(_connection.Location2.X + 23, _connection.Location2.Y + 13),
                _ => _connection.Location2.Map == MapID.DarkWorld ?
                    new Point(_connection.Location2.X + 2046, _connection.Location2.Y + 23) :
                    new Point(_connection.Location2.X + 13, _connection.Location2.Y + 23)
            };
        public string Color => Highlighted ? "#ffffffff" : _colorSettings.ConnectorColor;
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEntered { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerExited { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="colorSettings">
        /// The color settings data.
        /// </param>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="connection">
        /// The connection data.
        /// </param>
        /// <param name="mapArea">
        /// The map area ViewModel parent class.
        /// </param>
        public MapConnectionVM(
            IColorSettings colorSettings, IMode mode, IUndoRedoManager undoRedoManager, IMapAreaVM mapArea,
            IMapConnection connection)
        {
            _colorSettings = colorSettings;
            _mode = mode;
            _mapArea = mapArea;

            _connection = connection;
            _undoRedoManager = undoRedoManager;

            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);
            HandlePointerEntered = ReactiveCommand.Create<PointerEventArgs>(HandlePointerEnteredImpl);
            HandlePointerExited = ReactiveCommand.Create<PointerEventArgs>(HandlePointerExitedImpl);

            PropertyChanged += OnPropertyChanged;
            _mapArea.PropertyChanged += OnMapAreaChanged;
            _mode.PropertyChanged += OnModeChanged;
            _colorSettings.PropertyChanged += OnColorsChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on itself.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Highlighted))
            {
                await UpdateColor();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the MapAreaControlVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnMapAreaChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MapAreaVM.Orientation))
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    this.RaisePropertyChanged(nameof(Start));
                    this.RaisePropertyChanged(nameof(End));
                });             }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.EntranceShuffle))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
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
        private async void OnColorsChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ColorSettings.ConnectorColor))
            {
                await UpdateColor();
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the Color property.
        /// </summary>
        private async Task UpdateColor()
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Color)));
        }

        /// <summary>
        /// Creates an undoable action to remove the connection and sends it to the undo/redo manager.
        /// </summary>
        private void RemoveConnection()
        {
            _undoRedoManager.NewAction(_connection.CreateRemoveConnectionAction());
        }

        /// <summary>
        /// Highlights the control.
        /// </summary>
        private void Highlight()
        {
            Highlighted = true;
        }

        /// <summary>
        /// Un-highlights the control.
        /// </summary>
        private void Unhighlight()
        {
            Highlighted = false;
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The pointer released event args.
        /// </param>
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Right)
            {
                RemoveConnection();
            }
        }

        /// <summary>
        /// Handles pointer entering the control.
        /// </summary>
        /// <param name="e">
        /// The PointerEntered event args.
        /// </param>
        private void HandlePointerEnteredImpl(PointerEventArgs e)
        {
            Highlight();
        }

        /// <summary>
        /// Handles pointer leaving the control.
        /// </summary>
        /// <param name="e">
        /// The PointerExited event args.
        /// </param>
        private void HandlePointerExitedImpl(PointerEventArgs e)
        {
            Unhighlight();
        }
    }
}
