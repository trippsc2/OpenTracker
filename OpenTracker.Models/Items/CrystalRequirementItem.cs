using System;
using System.ComponentModel;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Items;
using ReactiveUI;

namespace OpenTracker.Models.Items;

/// <summary>
/// This class contains crystal requirement data.
/// </summary>
public class CrystalRequirementItem : CappedItem, ICrystalRequirementItem
{
    private bool _known;
    public bool Known
    {
        get => _known;
        set => this.RaiseAndSetIfChanged(ref _known, value);
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
    /// <param name="cycleItemFactory">
    ///     An Autofac factory for creating new <see cref="ICycleItem"/> objects.
    /// </param>
    public CrystalRequirementItem(
        ISaveLoadManager saveLoadManager, IAddItem.Factory addItemFactory, IRemoveItem.Factory removeItemFactory,
        ICycleItem.Factory cycleItemFactory)
        : base(saveLoadManager, addItemFactory, removeItemFactory, cycleItemFactory, 0, 7,
            null)
    {
        PropertyChanged += OnPropertyChanged;
    }

    public override void Add()
    {
        if (!Known)
        {
            Known = true;
            return;
        }

        base.Add();
    }
        
    public override bool CanRemove()
    {
        return Known || base.CanRemove();
    }

    public override void Remove()
    {
        if (Current > 0)
        {
            base.Remove();
            return;
        }

        if (!Known)
        {
            throw new Exception("The item cannot be removed, because it is already 0.");
        }
            
        Known = false;
    }

    public override void Cycle(bool reverse = false)
    {
        switch (reverse)
        {
            case true when !CanRemove():
                Known = true;
                Current = Maximum;
                return;
            case false when !CanAdd():
                Known = false;
                Current = 0;
                return;
            default:
                base.Cycle(reverse);
                break;
        }
    }

    public override void Reset()
    {
        Known = false;
        base.Reset();
    }

    public override ItemSaveData Save()
    {
        var saveData = base.Save();
        saveData.Known = Known;
            
        return saveData;
    }

    public override void Load(ItemSaveData? saveData)
    {
        if (saveData is null)
        {
            return;
        }
            
        base.Load(saveData);
        Known = saveData.Known;
    }
        
    /// <summary>
    /// Subscribes to the <see cref="ICrystalRequirementItem.PropertyChanged"/> event on this object.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Known))
        {
            this.RaisePropertyChanged(nameof(Current));
        }
    }
}