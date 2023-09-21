using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Maps;

/// <summary>
/// This class contains map connection control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class MapConnectionVM : ViewModel, IMapConnectionVM
{
    private static readonly SolidColorBrush HighlightedColor = SolidColorBrush.Parse("#ffffff");
    
    private readonly IUndoRedoManager _undoRedoManager;

    private ColorSettings ColorSettings { get; }
    private LayoutSettings LayoutSettings { get; }
    private IMode Mode { get; }
    private IMapConnection Connection { get; }

    public object Model => Connection;

    [Reactive]
    private bool Highlighted { get; set; }
    [ObservableAsProperty]
    public bool Visible { get; }
    [ObservableAsProperty]
    public Point Start { get; }
    [ObservableAsProperty]
    public Point End { get; }
    [ObservableAsProperty]
    public SolidColorBrush Color { get; } = HighlightedColor;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnterCommand { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeaveCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="colorSettings">
    ///     The color settings data.
    /// </param>
    /// <param name="layoutSettings"></param>
    /// <param name="mode">
    ///     The mode settings.
    /// </param>
    /// <param name="undoRedoManager">
    ///     The undo/redo manager.
    /// </param>
    /// <param name="connection">
    ///     The connection data.
    /// </param>
    public MapConnectionVM(
        ColorSettings colorSettings,
        LayoutSettings layoutSettings,
        IMode mode,
        IUndoRedoManager undoRedoManager,
        IMapConnection connection)
    {
        _undoRedoManager = undoRedoManager;
        ColorSettings = colorSettings;
        LayoutSettings = layoutSettings;
        Mode = mode;
        Connection = connection;
        
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        HandlePointerEnterCommand = ReactiveCommand.Create<PointerEventArgs>(HandlePointerEnter);
        HandlePointerLeaveCommand = ReactiveCommand.Create<PointerEventArgs>(HandlePointerLeave);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Mode.EntranceShuffle)
                .Select(x => x > EntranceShuffle.None)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Visible)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.LayoutSettings.CurrentMapOrientation,
                    x => x.Connection.Location1.Map,
                    x => x.Connection.Location1.X,
                    x => x.Connection.Location1.Y,
                    (orientation, map, x, y) => orientation == Orientation.Vertical
                        ? map == MapID.DarkWorld
                            ? new Point(x + 23, y + 2046)
                            : new Point(x + 23, y + 13)
                        : map == MapID.DarkWorld
                            ? new Point(x + 2046, y + 23)
                            : new Point(x + 13, y + 23))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Start)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.LayoutSettings.CurrentMapOrientation,
                    x => x.Connection.Location2.Map,
                    x => x.Connection.Location2.X,
                    x => x.Connection.Location2.Y,
                    (orientation, map, x, y) => orientation == Orientation.Vertical
                        ? map == MapID.DarkWorld
                            ? new Point(x + 23, y + 2046)
                            : new Point(x + 23, y + 13)
                        : map == MapID.DarkWorld
                            ? new Point(x + 2046, y + 23)
                            : new Point(x + 13, y + 23))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.End)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.Highlighted,
                    x => x.ColorSettings.ConnectorColor.Value,
                    (highlighted, color) => highlighted ? HighlightedColor : color)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Color)
                .DisposeWith(disposables);
        });
    }

    private void HandleClick(PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton == MouseButton.Right)
        {
            RemoveConnection();
        }
    }

    private void HandlePointerEnter(PointerEventArgs e)
    {
        Highlighted = true;
    }

    private void HandlePointerLeave(PointerEventArgs e)
    {
        Highlighted = false;
    }

    private void RemoveConnection()
    {
        _undoRedoManager.NewAction(Connection.CreateRemoveConnectionAction());
    }
}