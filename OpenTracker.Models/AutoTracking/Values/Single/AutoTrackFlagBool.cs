using System.Reactive.Linq;
using OpenTracker.Models.AutoTracking.Memory;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.AutoTracking.Values.Single;

/// <summary>
/// This class represents an auto-tracking result value from a SNES memory address bitwise flag.
/// </summary>
public sealed class AutoTrackFlagBool : ReactiveObject, IAutoTrackValue
{
    private readonly int _trueValue;
        
    private IMemoryFlag Flag { get; }
        
    [ObservableAsProperty]
    public int? CurrentValue { get; }

    /// <summary>
    /// Initializes a new <see cref="AutoTrackFlagBool"/> object with the specified flag and resultant value.
    /// </summary>
    /// <param name="flag">
    ///     A <see cref="IMemoryFlag"/> represents the memory flag to be monitored.
    /// </param>
    /// <param name="trueValue">
    ///     A <see cref="int"/> representing the resultant value, if the flag is set.
    /// </param>
    public AutoTrackFlagBool(IMemoryFlag flag, int trueValue)
    {
        _trueValue = trueValue;

        Flag = flag;
            
        this.WhenAnyValue(x => x.Flag.Status)
            .Select(GetNewValueFromFlagStatus)
            .ToPropertyEx(this, x => x.CurrentValue);
    }

    private int? GetNewValueFromFlagStatus(bool? flagStatus)
    {
        if (flagStatus is null)
        {
            return null;
        }
            
        return flagStatus.Value ? _trueValue : 0;
    }
}