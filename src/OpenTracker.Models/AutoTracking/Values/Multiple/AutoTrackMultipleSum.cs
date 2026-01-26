using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OpenTracker.Models.AutoTracking.Values.Multiple;

/// <summary>
/// This class contains the auto-tracking result value of a list of results to be summed.
/// </summary>
public class AutoTrackMultipleSum : AutoTrackValueBase, IAutoTrackMultipleSum
{
    private readonly IList<IAutoTrackValue> _values;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="values">
    ///     The <see cref="IList{T}"/> of <see cref="IAutoTrackValue"/>.
    /// </param>
    public AutoTrackMultipleSum(IList<IAutoTrackValue> values)
    {
        _values = values;
            
        UpdateValue();

        foreach (var value in values)
        {
            value.PropertyChanged += OnValueChanged;
        }
    }

    /// <summary>
    /// Subscribes to the <see cref="IAutoTrackValue.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event was sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnValueChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
        {
            UpdateValue();
        }
    }

    protected override int? GetNewValue()
    {
        if (!_values.Any(x => x.CurrentValue.HasValue))
        {
            return null;
        }
            
        var newValue = _values.Sum(value => value.CurrentValue ?? 0);

        return newValue;
    }
}