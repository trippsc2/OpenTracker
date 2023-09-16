using System;
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
/// This class contains the logic to adapt data from a pair of items to an item control.
/// </summary>
[DependencyInjection]
public sealed class PairItemAdapter : ViewModel, IItemAdapter
{
    private readonly IUndoRedoManager _undoRedoManager;

    private readonly ICappedItem[] _items;
    private readonly string _imageSourceBase;

    public string ImageSource =>
        _imageSourceBase + _items[0].Current.ToString(CultureInfo.InvariantCulture) +
        $"{_items[1].Current.ToString(CultureInfo.InvariantCulture)}.png";

    public string? Label { get; } = null;
    public string LabelColor { get; } = "#ffffffff";
        
    public IBossSelectPopupVM? BossSelect { get; } = null;
        
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

    public delegate PairItemAdapter Factory(ICappedItem[] items, string imageSourceBase);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="imageSourceBase">
    /// A string representing the image source base.
    /// </param>
    /// <param name="items">
    /// An array of items that are to be represented by this control.
    /// </param>
    public PairItemAdapter(IUndoRedoManager undoRedoManager, ICappedItem[] items, string imageSourceBase)
    {
        _undoRedoManager = undoRedoManager;

        _items = items;
        _imageSourceBase = imageSourceBase;

        if (_items.Length != 2)
        {
            throw new ArgumentOutOfRangeException(nameof(items));
        }
            
        HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

        foreach (var item in _items)
        {
            item.PropertyChanged += OnItemChanged;
        }
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
        await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
    }

    /// <summary>
    /// Creates an undoable action to add an item to the first item in the pair and sends it to the undo/redo
    /// manager.
    /// </summary>
    private void AddFirstItem()
    {
        _undoRedoManager.NewAction(_items[0].CreateCycleItemAction());
    }

    /// <summary>
    /// Creates an undoable action to add an item to the second item in the pair and sends it to the undo/redo
    /// manager.
    /// </summary>
    private void AddSecondItem()
    {
        _undoRedoManager.NewAction(_items[1].CreateCycleItemAction());
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
                AddFirstItem();
                break;
            case MouseButton.Right:
                AddSecondItem();
                break;
        }
    }
}