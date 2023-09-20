using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Models.Items;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Items.Adapters;

/// <summary>
/// This class contains the logic to adapt generic small key data to an item control.
/// </summary>
[DependencyInjection]
public sealed class SmallKeyAdapter : ViewModel, IItemAdapter
{
    private static readonly SolidColorBrush NormalLabelColor = SolidColorBrush.Parse("#ffffff");
    
    private readonly IUndoRedoManager _undoRedoManager;

    private IColorSettings ColorSettings { get; }
    private IItem Item { get; }
    
    public BossSelectPopupVM? BossSelect => null;

    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;
    [ObservableAsProperty]
    public string Label { get; } = string.Empty;
    [ObservableAsProperty]
    public SolidColorBrush LabelColor { get; } = NormalLabelColor;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    public delegate SmallKeyAdapter Factory(IItem item);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="colorSettings">
    /// The color settings data.
    /// </param>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="item">
    /// An item that is to be represented by this control.
    /// </param>
    public SmallKeyAdapter(IColorSettings colorSettings, IUndoRedoManager undoRedoManager, IItem item)
    {
        _undoRedoManager = undoRedoManager;
        ColorSettings = colorSettings;
        Item = item;
        
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

        this.WhenActivated(disposables =>
        {
            var itemCurrent = this
                .WhenAnyValue(x => x.Item.Current);

            itemCurrent
                .Select(x => x > 0
                    ? "avares://OpenTracker/Assets/Images/Items/smallkey1.png"
                    : "avares://OpenTracker/Assets/Images/Items/smallkey0.png")
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
            itemCurrent
                .Select(x => x + (Item.CanAdd() ? "" : "*"))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Label)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.Item.Current,
                    x => x.ColorSettings.EmphasisFontColor.Value,
                    (_, emphasisFontColor) => Item.CanAdd() ? emphasisFontColor : NormalLabelColor)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.LabelColor)
                .DisposeWith(disposables);
        });
    }

    private void HandleClick(PointerReleasedEventArgs e)
    {
        switch (e.InitialPressMouseButton)
        {
            case MouseButton.Left:
                AddItem();
                break;
            case MouseButton.Right:
                RemoveItem();
                break;
            case MouseButton.None:
            case MouseButton.Middle:
            case MouseButton.XButton1:
            case MouseButton.XButton2:
            default:
                break;
        }
    }

    private void AddItem()
    {
        _undoRedoManager.NewAction(Item.CreateAddItemAction());
    }

    private void RemoveItem()
    {
        _undoRedoManager.NewAction(Item.CreateRemoveItemAction());
    }

}