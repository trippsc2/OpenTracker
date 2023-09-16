using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.AutoTracking.Values.Multiple;

/// <summary>
/// This class represents an auto-tracking result value of an ordered priority list of auto-tracking result values.
/// </summary>
public sealed class AutoTrackMultipleOverride : ReactiveObject, IAutoTrackValue
{
    private readonly SourceList<IAutoTrackValue> _values = new();

    [ObservableAsProperty]
    public int? CurrentValue { get; }

    /// <summary>
    /// Initializes a new <see cref="AutoTrackMultipleOverride"/> object with the specified enumerable of
    /// auto-tracker values.
    /// </summary>
    /// <param name="values">
    ///     An <see cref="IEnumerable{T}"/> of <see cref="IAutoTrackValue"/> representing the auto-trackers in order
    ///     of priority.
    /// </param>
    public AutoTrackMultipleOverride(IEnumerable<IAutoTrackValue> values)
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
        var valuesNotNull = _values.Items
            .Where(x => x.CurrentValue.HasValue)
            .ToList();

        if (valuesNotNull.Count == 0)
        {
            return null;
        }

        return valuesNotNull
            .Where(value => value.CurrentValue > 0)
            .Select(value => value.CurrentValue!.Value)
            .FirstOrDefault();
    }
}