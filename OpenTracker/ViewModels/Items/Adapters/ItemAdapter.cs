using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Autofac;
using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items.Adapters;

/// <summary>
/// This class contains the logic to adapts item data to an item control. 
/// </summary>
[DependencyInjection]
public sealed class ItemAdapter : ViewModel, IItemAdapter
{
    private readonly IUndoRedoManager _undoRedoManager;
        
    private readonly IItem _item;
    private readonly string _imageSourceBase;

    public string ImageSource => $"{_imageSourceBase}{_item.Current.ToString(CultureInfo.InvariantCulture)}.png";
    public string? Label { get; } = null;
    public string LabelColor { get; } = "#ffffffff";
        
    public IBossSelectPopupVM? BossSelect { get; } = null;
        
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

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
            
        _item = item;
        _imageSourceBase = imageSourceBase;

        HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

        _item.PropertyChanged += OnItemChanged;
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
    private async void OnItemChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IItem.Current))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
        }
    }

    /// <summary>
    /// Creates an undoable action to add an item and sends it to the undo/redo manager.
    /// </summary>
    private void AddItem()
    {
        _undoRedoManager.NewAction(_item.CreateAddItemAction());
    }

    /// <summary>
    /// Creates an undoable action to remove an item and sends it to the undo/redo manager.
    /// </summary>
    private void RemoveItem()
    {
        _undoRedoManager.NewAction(_item.CreateRemoveItemAction());
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