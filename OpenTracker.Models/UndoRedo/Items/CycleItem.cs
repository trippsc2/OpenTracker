using OpenTracker.Models.Items;

namespace OpenTracker.Models.UndoRedo.Items;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to cycle a <see cref="ICappedItem"/>.
/// </summary>
public class CycleItem : ICycleItem
{
    private readonly ICappedItem _item;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">
    ///     The <see cref="ICappedItem"/>.
    /// </param>
    public CycleItem(ICappedItem item)
    {
        _item = item;
    }

    public bool CanExecute()
    {
        return true;
    }

    public void ExecuteDo()
    {
        _item.Cycle();
    }

    public void ExecuteUndo()
    {
        _item.Cycle(true);
    }
}