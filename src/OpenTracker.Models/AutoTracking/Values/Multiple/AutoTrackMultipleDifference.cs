using System;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.AutoTracking.Values.Multiple;

/// <summary>
/// This class represents an auto-tracking result value from subtracting a pair of values.
/// </summary>
public sealed class AutoTrackMultipleDifference : ReactiveObject, IAutoTrackValue
{
    private IAutoTrackValue Value1 { get; }
    private IAutoTrackValue Value2 { get; }

    [ObservableAsProperty]
    public int? CurrentValue { get; }
        
    /// <summary>
    /// Initializes a new <see cref="AutoTrackMultipleDifference"/> object with the specified pair of values.
    /// </summary>
    /// <param name="value1">
    ///     An <see cref="IAutoTrackValue"/> representing the auto-tracking result value from which to subtract.
    /// </param>
    /// <param name="value2">
    ///     An <see cref="IAutoTrackValue"/> representing the auto-tracking result value to be subtracted.
    /// </param>
    public AutoTrackMultipleDifference(IAutoTrackValue value1, IAutoTrackValue value2)
    {
        Value1 = value1;
        Value2 = value2;

        this.WhenAnyValue(
                x => x.Value1.CurrentValue,
                x => x.Value2.CurrentValue)
            .Select(_ => GetNewValue())
            .ToPropertyEx(this, x => x.CurrentValue);

    }

    private int? GetNewValue()
    {
        if (Value1.CurrentValue is null)
        {
            return null;
        }
            
        return Value2.CurrentValue is null
            ? Value1.CurrentValue
            : Math.Max(0, Value1.CurrentValue.Value - Value2.CurrentValue.Value);
    }
}