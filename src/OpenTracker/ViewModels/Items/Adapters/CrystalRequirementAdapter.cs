using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Items;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.BossSelect;
using Reactive.Bindings;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveCommand = ReactiveUI.ReactiveCommand;

namespace OpenTracker.ViewModels.Items.Adapters;

/// <summary>
/// This class contains the logic to adapt crystal requirement data to an item control. 
/// </summary>
[DependencyInjection]
public sealed class CrystalRequirementAdapter : ViewModel, IItemAdapter
{
    private static readonly SolidColorBrush NonEmphasisColor = SolidColorBrush.Parse("#ffffff");
    
    private readonly IUndoRedoManager _undoRedoManager;

    private ReactiveProperty<SolidColorBrush> EmphasisFontColor { get; }
    private ReactiveProperty<SolidColorBrush> UnknownFontColor { get; }
    private ICrystalRequirementItem Item { get; }

    public string ImageSource { get; }
    public BossSelectPopupVM? BossSelect => null;

    [ObservableAsProperty]
    public string Label { get; } = "?";
    [ObservableAsProperty]
    public SolidColorBrush LabelColor { get; } = NonEmphasisColor;
    
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    public delegate CrystalRequirementAdapter Factory(ICrystalRequirementItem item, string imageSource);

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
    /// The crystal requirement item.
    /// </param>
    /// <param name="imageSource">
    /// The image source of the crystal requirement.
    /// </param>
    public CrystalRequirementAdapter(
        ColorSettings colorSettings,
        IUndoRedoManager undoRedoManager,
        ICrystalRequirementItem item,
        string imageSource)
    {
        _undoRedoManager = undoRedoManager;
        EmphasisFontColor = colorSettings.EmphasisFontColor;
        UnknownFontColor = colorSettings.AccessibilityColors[AccessibilityLevel.SequenceBreak];
        Item = item;

        ImageSource = imageSource;
        
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);    

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(
                    x => x.Item.Known,
                    x => x.Item.Current,
                    (known, current) => known ? (7 - current).ToString() : "?")
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Label)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.Item.Known,
                    x => x.Item.Current,
                    x => x.EmphasisFontColor.Value,
                    x => x.UnknownFontColor.Value,
                    (known, current, emphasisColor, unknownFontColor) => known
                        ? current == 0
                            ? emphasisColor
                            : NonEmphasisColor
                        : unknownFontColor)
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