using System;
using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;
using OpenTracker.Utils.Autofac;
using ReactiveUI;

namespace OpenTracker.Models.Items;

/// <summary>
/// This class contains item data.
/// </summary>
[DependencyInjection]
public class Item : ReactiveObject, IItem
{
    private readonly ISaveLoadManager _saveLoadManager;

    private readonly IAddItem.Factory _addItemFactory;
    private readonly IRemoveItem.Factory _removeItemFactory;
        
    private readonly int _starting;
    private readonly IAutoTrackValue? _autoTrackValue;

    private int _current;
    public int Current
    {
        get => _current;
        set => this.RaiseAndSetIfChanged(ref _current, value);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="saveLoadManager">
    ///     The <see cref="ISaveLoadManager"/>.
    /// </param>
    /// <param name="addItemFactory">
    ///     An Autofac factory for creating new <see cref="IAddItem"/> objects.
    /// </param>
    /// <param name="removeItemFactory">
    ///     An Autofac factory for creating new <see cref="IRemoveItem"/> objects.
    /// </param>
    /// <param name="starting">
    ///     A <see cref="int"/> representing the starting value.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    public Item(
        ISaveLoadManager saveLoadManager, IAddItem.Factory addItemFactory, IRemoveItem.Factory removeItemFactory,
        int starting, IAutoTrackValue? autoTrackValue)
    {
        _saveLoadManager = saveLoadManager;
            
        _starting = starting;
        _autoTrackValue = autoTrackValue;
        _addItemFactory = addItemFactory;
        _removeItemFactory = removeItemFactory;

        Current = _starting;

        if (!(_autoTrackValue is null))
        {
            _autoTrackValue.PropertyChanged += OnAutoTrackChanged;
        }
    }

    public IUndoable CreateAddItemAction()
    {
        return _addItemFactory(this);
    }

    public virtual bool CanAdd()
    {
        return true;
    }

    public virtual void Add()
    {
        Current++;
    }

    public IUndoable CreateRemoveItemAction()
    {
        return _removeItemFactory(this);
    }

    public virtual bool CanRemove()
    {
        return Current > 0;
    }

    public virtual void Remove()
    {
        if (Current <= 0)
        {
            throw new Exception("Item cannot be removed, because it is already 0.");
        }

        Current--;
    }

    public virtual void Reset()
    {
        Current = _starting;
    }

    public virtual ItemSaveData Save()
    {
        return new()
        {
            Current = Current
        };
    }

    public virtual void Load(ItemSaveData? saveData)
    {
        if (saveData is null)
        {
            return;
        }

        Current = saveData.Current;
    }
        
    /// <summary>
    /// Subscribes to the <see cref="IAutoTrackValue.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnAutoTrackChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
        {
            AutoTrackUpdate();
        }
    }

    /// <summary>
    /// Update the <see cref="Current"/> property to match the auto-tracking value.
    /// </summary>
    private void AutoTrackUpdate()
    {
        if (!_autoTrackValue!.CurrentValue.HasValue)
        {
            return;
        }

        if (Current == _autoTrackValue.CurrentValue.Value)
        {
            return;
        }
            
        Current = _autoTrackValue.CurrentValue.Value;
        _saveLoadManager.Unsaved = true;
    }
}