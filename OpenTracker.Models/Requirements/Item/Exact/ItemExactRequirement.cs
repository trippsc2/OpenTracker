using System;
using System.ComponentModel;
using OpenTracker.Models.Items;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Item.Exact;

/// <summary>
/// This class contains <see cref="IItem"/> exact value <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class ItemExactRequirement : BooleanRequirement, IItemExactRequirement
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
    public ItemExactRequirement(IItem item, int count)
    {
        _item = item ?? throw new ArgumentNullException(nameof(item));
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
        return _item.Current == _count;
    }
}