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
/// This class contains the logic to adapts item data to an item control. 
/// </summary>
[DependencyInjection]
public sealed class ItemAdapter : ViewModel, IItemAdapter
{
    private readonly IUndoRedoManager _undoRedoManager;

    private IItem Item { get; }
    
    public string? Label => null;
    public SolidColorBrush? LabelColor => null;
    public BossSelectPopupVM? BossSelect => null;
    
    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    public delegate ItemAdapter Factory(IItem item, string imageSourceBase);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="item">
    /// An item that is to be represented by this control.
    /// </param>
    /// <param name="imageSourceBase">
    /// A string representing the base image source.
    /// </param>
    public ItemAdapter(IUndoRedoManager undoRedoManager, IItem item, string imageSourceBase)
    {
        _undoRedoManager = undoRedoManager;
        Item = item;
        
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Item.Current)
                .Select(x => $"{imageSourceBase}{x}.png")
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