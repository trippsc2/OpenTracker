using System.Reactive.Linq;
using OpenTracker.Models.AutoTracking.Memory;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.AutoTracking.Values.Single;

/// <summary>
/// This class represents an auto-tracking result value comparing of a SNES memory address byte value to a specified
/// value.
/// </summary>
public sealed class AutoTrackAddressBool : ReactiveObject, IAutoTrackValue
{
    private readonly byte _comparison;
    private readonly int _trueValue;
        
    private MemoryAddress Address { get; }
        
    [ObservableAsProperty]
    public int? CurrentValue { get; }
        
    /// <summary>
    /// Initializes a new <see cref="AutoTrackAddressBool"/> object with the specified memory address, comparison,
    /// and resultant value.
    /// </summary>
    /// <param name="address">
    ///     A <see cref="MemoryAddress"/> representing the memory address to monitor.
    /// </param>
    /// <param name="comparison">
    ///     A <see cref="byte"/> representing the value to which the memory address is compared.
    /// </param>
    /// <param name="trueValue">
    ///     An <see cref="int"/> representing the resultant value, if the comparison is true.
    /// </param>
    public AutoTrackAddressBool(MemoryAddress address, byte comparison, int trueValue)
    {
        _comparison = comparison;
        _trueValue = trueValue;

        Address = address;

        this.WhenAnyValue(x => x.Address.Value)
            .Select(GetNewValueFromAddressValue)
            .ToPropertyEx(this, x => x.CurrentValue);
    }

    private int? GetNewValueFromAddressValue(byte? addressValue)
    {
        if (addressValue is null)
        {
            return null;
        }
            
        return addressValue > _comparison ? _trueValue : 0;
    }
}