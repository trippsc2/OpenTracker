using System.Reactive.Linq;
using OpenTracker.Models.AutoTracking.Memory;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.AutoTracking.Values.Single;

/// <summary>
/// This class represents an auto-tracking result value that is the direct SNES memory address byte value.
/// </summary>
public sealed class AutoTrackAddressValue : ReactiveObject, IAutoTrackValue
{
    private readonly byte _maximum;
    private readonly int _adjustment;
        
    private MemoryAddress Address { get; }
        
    [ObservableAsProperty]
    public int? CurrentValue { get; }

    /// <summary>
    /// Initializes a new <see cref="AutoTrackAddressValue"/> object with the specified memory address, maximum
    /// value, and adjustment.
    /// </summary>
    /// <param name="address">
    ///     A <see cref="MemoryAddress"/> representing the memory address to monitor.
    /// </param>
    /// <param name="maximum">
    ///     A <see cref="byte"/> representing the maximum valid value of the memory address.
    ///     If the result value is greater than this maximum, the result value will be null.
    /// </param>
    /// <param name="adjustment">
    ///     An <see cref="int"/> representing the amount that the result value should be adjusted from the memory
    ///     address value.
    ///     This defaults to 0.
    /// </param>
    public AutoTrackAddressValue(MemoryAddress address, byte maximum, int adjustment = 0)
    {
        _maximum = maximum;
        _adjustment = adjustment;
            
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
            
        var newValue = addressValue.Value + _adjustment;
            
        return newValue > _maximum ? null : newValue;
    }
}