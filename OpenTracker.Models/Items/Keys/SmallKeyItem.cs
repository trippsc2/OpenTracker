using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;
using OpenTracker.Utils.Autofac;
using ReactiveUI;

namespace OpenTracker.Models.Items.Keys;

/// <summary>
/// This class contains the small key item data.
/// </summary>
[DependencyInjection]
public sealed class SmallKeyItem : Item, ISmallKeyItem
{
    private readonly IMode _mode;

    private readonly ICycleItem.Factory _cycleItemFactory;

    private readonly IItem _genericKey;
    private readonly int _nonKeyDropMaximum;
    private readonly int _keyDropMaximum;
        
    public int Maximum => _mode.KeyDropShuffle ? _keyDropMaximum : _nonKeyDropMaximum;

    private int _effectiveCurrent;
    public int EffectiveCurrent
    {
        get => _effectiveCurrent;
        private set => this.RaiseAndSetIfChanged(ref _effectiveCurrent, value);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="saveLoadManager">
    ///     The <see cref="ISaveLoadManager"/>.
    /// </param>
    /// <param name="addItemFactory">
    ///     An Autofac factory for creating new <see cref="IAddItem"/> objects.
    /// </param>
    /// <param name="removeItemFactory">
    ///     An Autofac factory for creating new <see cref="IRemoveItem"/> objects.
    /// </param>
    /// <param name="cycleItemFactory">
    ///     An Autofac factory for creating new <see cref="ICycleItem"/> objects.
    /// </param>
    /// <param name="genericKey">
    ///     The <see cref="IItem"/> representing the generic keys item.
    /// </param>
    /// <param name="nonKeyDropMaximum">
    ///     A <see cref="int"/> representing the item maximum when key drop shuffle is disabled.
    /// </param>
    /// <param name="keyDropMaximum">
    ///     A <see cref="int"/> representing the item maximum when key drop shuffle is enabled.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    public SmallKeyItem(
        IMode mode, ISaveLoadManager saveLoadManager, IAddItem.Factory addItemFactory,
        IRemoveItem.Factory removeItemFactory, ICycleItem.Factory cycleItemFactory, IItem genericKey,
        int nonKeyDropMaximum, int keyDropMaximum, IAutoTrackValue? autoTrackValue)
        : base(saveLoadManager, addItemFactory, removeItemFactory, 0, autoTrackValue)
    {
        _mode = mode;

        _cycleItemFactory = cycleItemFactory;

        _genericKey = genericKey;
        _nonKeyDropMaximum = nonKeyDropMaximum;
        _keyDropMaximum = keyDropMaximum;
            
        UpdateEffectiveCurrent();

        PropertyChanged += OnPropertyChanged;
        _mode.PropertyChanged += OnModeChanged;
        _genericKey.PropertyChanged += OnGenericKeyChanged;
    }

    public override bool CanAdd()
    {
        return Current < Maximum;
    }

    public override void Add()
    {
        if (Current >= Maximum)
        {
            throw new Exception("Cannot add item, because it is already at maximum.");
        }

        base.Add();
    }

    public override void Remove()
    {
        if (Current <= 0)
        {
            throw new Exception("Cannot remove item, because it is already 0.");
        }
            
        base.Remove();
    }

    public IUndoable CreateCycleItemAction()
    {
        return _cycleItemFactory(this);
    }

    public void Cycle(bool reverse = false)
    {
        if (reverse)
        {
            if (CanRemove())
            {
                Remove();
                return;
            }

            Current = Maximum;
            return;
        }

        if (CanAdd())
        {
            Add();
            return;
        }

        Current = 0;
    }

    public IList<int> GetKeyValues()
    {
        return _mode.SmallKeyShuffle ? new List<int> {EffectiveCurrent}
            : Enumerable.Range(0, Maximum + 1).ToList();
    }
        
    /// <summary>
    /// Subscribes to the <see cref="ISmallKeyItem.PropertyChanged"/> event on this object.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IItem.Current))
        {
            UpdateEffectiveCurrent();
        }
    }

    /// <summary>
    /// Subscribes to the <see cref="IMode.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(IMode.KeyDropShuffle):
                if (Current > Maximum)
                {
                    Current = Maximum;
                }

                this.RaisePropertyChanged(nameof(Maximum));
                break;
            case nameof(IMode.GenericKeys):
                UpdateEffectiveCurrent();
                break;
        }
    }

    /// <summary>
    /// Subscribes to the <see cref="IItem.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnGenericKeyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IItem.Current) && _mode.GenericKeys)
        {
            UpdateEffectiveCurrent();
        }
    }

    /// <summary>
    /// Updates the value of the <see cref="EffectiveCurrent"/> property.
    /// </summary>
    private void UpdateEffectiveCurrent()
    {
        var effectiveCurrent = Current;

        if (_mode.GenericKeys)
        {
            effectiveCurrent += _genericKey.Current;
        }
            
        EffectiveCurrent = Math.Min(Maximum, effectiveCurrent);
    }
}