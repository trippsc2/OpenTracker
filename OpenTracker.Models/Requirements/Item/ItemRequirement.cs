using System.ComponentModel;
using OpenTracker.Models.Items;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Item;

/// <summary>
/// This class contains <see cref="IItem"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class ItemRequirement : BooleanRequirement, IItemRequirement
{
    private readonly IItem _item;
    private readonly int _count;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">
    ///     The <see cref="IItem"/>.
    /// </param>
    /// <param name="count">
    ///     A <see cref="int"/> representing the number of the item required.
    /// </param>
    public ItemRequirement(IItem item, int count = 1)
    {
        _item = item;
        _count = count;

        _item.PropertyChanged += OnItemChanged;

        UpdateValue();
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
    private void OnItemChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IItem.Current))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _item.Current >= _count;
    }
}