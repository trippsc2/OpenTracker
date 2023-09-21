using OpenTracker.Models.Items;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.UndoRedo.Items;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to add an <see cref="IItem"/>.
/// </summary>
[DependencyInjection]
public sealed class AddItem : IAddItem
{
    private readonly IItem _item;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">
    ///     The <see cref="IItem"/>.
    /// </param>
    public AddItem(IItem item)
    {
        _item = item;
    }

    public bool CanExecute()
    {
        return _item.CanAdd();
    }

    public void ExecuteDo()
    {
        _item.Add();
    }

    public void ExecuteUndo()
    {
        _item.Remove();
    }
}