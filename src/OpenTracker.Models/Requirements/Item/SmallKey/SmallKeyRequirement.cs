using System.ComponentModel;
using OpenTracker.Models.Items.Keys;

namespace OpenTracker.Models.Requirements.Item.SmallKey;

/// <summary>
/// This class contains small key <see cref="IRequirement"/> data.
/// </summary>
public class SmallKeyRequirement : BooleanRequirement, ISmallKeyRequirement
{
    private readonly ISmallKeyItem _item;
    private readonly int _count;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">
    ///     The <see cref="ISmallKeyItem"/>.
    /// </param>
    /// <param name="count">
    ///     A <see cref="int"/> representing the number of the item required.
    /// </param>
    public SmallKeyRequirement(ISmallKeyItem item, int count = 1)
    {
        _item = item;
        _count = count;

        UpdateValue();
            
        _item.PropertyChanged += OnItemChanged;
    }
        
    /// <summary>
    /// Subscribes to the <see cref="ISmallKeyItem.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnItemChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ISmallKeyItem.EffectiveCurrent))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _item.EffectiveCurrent >= _count;
    }
}