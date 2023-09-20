using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Items.Adapters;

/// <summary>
/// This class contains the logic to adapt data from a pair of items to an item control.
/// </summary>
[DependencyInjection]
public sealed class PairItemAdapter : ViewModel, IItemAdapter
{
    private readonly IUndoRedoManager _undoRedoManager;

    private ICappedItem Item1 { get; }
    private ICappedItem Item2 { get; }

    public string? Label => null;
    public SolidColorBrush? LabelColor => null;
    public BossSelectPopupVM? BossSelect => null;

    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    public delegate PairItemAdapter Factory(ICappedItem item1, ICappedItem item2, string imageSourceBase);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="undoRedoManager">
    ///     The <see cref="IUndoRedoManager"/>
    /// </param>
    /// <param name="item1">
    ///     A <see cref="ICappedItem"/> representing the first item in the pair.
    /// </param>
    /// <param name="item2">
    ///     A <see cref="ICappedItem"/> representing the second item in the pair.
    /// </param>
    /// <param name="imageSourceBase">
    ///     A <see cref="string"/> representing the image source base.
    /// </param>
    public PairItemAdapter(IUndoRedoManager undoRedoManager, ICappedItem item1, ICappedItem item2, string imageSourceBase)
    {
        _undoRedoManager = undoRedoManager;
        Item1 = item1;
        Item2 = item2;

        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(
                    x => x.Item1.Current,
                    x => x.Item2.Current,
                    (item1Current, item2Current) => $"{imageSourceBase}{item1Current}{item2Current}.png")
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
        });
    }

    private void HandleClick(PointerReleasedEventArgs e)
    {
        switch (e.InitialPressMouseButton)
        {
            case MouseButton.Left:
                AddFirstItem();
                break;
            case MouseButton.Right:
                AddSecondItem();
                break;
            case MouseButton.None:
            case MouseButton.Middle:
            case MouseButton.XButton1:
            case MouseButton.XButton2:
            default:
                break;
        }
    }
    
    private void AddFirstItem()
    {
        _undoRedoManager.NewAction(Item1.CreateCycleItemAction());
    }
    
    private void AddSecondItem()
    {
        _undoRedoManager.NewAction(Item2.CreateCycleItemAction());
    }

}