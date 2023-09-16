using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.UnitTests.Models.Items;

public sealed class MockItem : ReactiveObject, IItem
{
    [Reactive]
    public int Current { get; set; }
    
    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public ItemSaveData Save()
    {
        throw new System.NotImplementedException();
    }

    public void Load(ItemSaveData? saveData)
    {
        throw new System.NotImplementedException();
    }

    public IUndoable CreateAddItemAction()
    {
        throw new System.NotImplementedException();
    }

    public bool CanAdd()
    {
        throw new System.NotImplementedException();
    }

    public void Add()
    {
        throw new System.NotImplementedException();
    }

    public IUndoable CreateRemoveItemAction()
    {
        throw new System.NotImplementedException();
    }

    public bool CanRemove()
    {
        throw new System.NotImplementedException();
    }

    public void Remove()
    {
        throw new System.NotImplementedException();
    }
}