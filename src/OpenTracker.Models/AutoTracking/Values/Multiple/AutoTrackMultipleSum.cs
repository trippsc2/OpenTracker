using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.AutoTracking.Values.Multiple;

/// <summary>
/// This class represents an auto-tracking result value of a group of auto-tracking result values to be summed.
/// </summary>
public sealed class AutoTrackMultipleSum : ReactiveObject, IAutoTrackValue
{
    private readonly SourceList<IAutoTrackValue> _values = new();

    [ObservableAsProperty]
    public int? CurrentValue { get; }

    /// <summary>
    /// Initializes a new <see cref="AutoTrackMultipleSum"/> object with the specified group of auto-tracking result
    /// values.
    /// </summary>
    /// <param name="values">
    ///     The <see cref="IEnumerable{T}"/> of <see cref="IAutoTrackValue"/> representing the auto-tracking result
    ///     values to be summed.
    /// </param>
    public AutoTrackMultipleSum(IEnumerable<IAutoTrackValue> values)
    {
        _values.AddRange(values);

        _values
            .Connect()
            .WhenPropertyChanged(x => x.CurrentValue)
            .Select(_ => GetNewValue())
            .ToPropertyEx(this, x => x.CurrentValue);
    }

    private int? GetNewValue()
    {
        if (!_values.Items.Any(x => x.CurrentValue.HasValue))
        {
            return null;
        }

        var newValue = _values.Items.Sum(value => value.CurrentValue ?? 0);

        return newValue;
    }
}