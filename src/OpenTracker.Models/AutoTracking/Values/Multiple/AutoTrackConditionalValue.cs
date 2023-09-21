using System.Reactive.Linq;
using OpenTracker.Models.Requirements;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.AutoTracking.Values.Multiple;

/// <summary>
/// This class represents an auto-tracking result value that changes based on a condition.
/// </summary>
public sealed class AutoTrackConditionalValue : ReactiveObject, IAutoTrackValue
{
    private IRequirement Condition { get; }
    private IAutoTrackValue? TrueValue { get; }
    private IAutoTrackValue? FalseValue { get; }

    [ObservableAsProperty]
    public int? CurrentValue { get; }

    /// <summary>
    /// Initializes a new <see cref="AutoTrackConditionalValue"/> object with the specified condition and true and false
    /// values.
    /// </summary>
    /// <param name="condition">
    ///     A <see cref="IRequirement"/> representing the condition for determining which value to use.
    /// </param>
    /// <param name="trueValue">
    ///     An <see cref="IAutoTrackValue"/> representing the auto-tracking result value if the condition is met.
    /// </param>
    /// <param name="falseValue">
    ///     An <see cref="IAutoTrackValue"/> representing the auto-tracking result value if the condition is not met.
    /// </param>
    public AutoTrackConditionalValue(
        IRequirement condition, IAutoTrackValue? trueValue, IAutoTrackValue? falseValue)
    {
        Condition = condition;
        TrueValue = trueValue;
        FalseValue = falseValue;

        this.WhenAnyValue(
                x => x.Condition.Met,
                x => x.TrueValue!.CurrentValue,
                x => x.FalseValue!.CurrentValue)
            .Select(_ => GetNewValue())
            .ToPropertyEx(this, x => x.CurrentValue);
    }

    private int? GetNewValue()
    {
        return Condition.Met ? TrueValue?.CurrentValue : FalseValue?.CurrentValue;
    }
}