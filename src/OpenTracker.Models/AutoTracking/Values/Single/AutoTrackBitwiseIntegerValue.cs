using System.Reactive.Linq;
using OpenTracker.Models.AutoTracking.Memory;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.AutoTracking.Values.Single;

/// <summary>
/// This class represents an auto-tracking result value that is a bitwise masked SNES memory address byte value.
/// </summary>
public sealed class AutoTrackBitwiseIntegerValue : ReactiveObject, IAutoTrackValue
{
    private readonly byte _mask;
    private readonly int _shift;

    private MemoryAddress Address { get; }
        
    [ObservableAsProperty]
    public int? CurrentValue { get; }
        
    /// <summary>
    /// Initializes a new <see cref="AutoTrackBitwiseIntegerValue"/> object with the specified memory address,
    /// bitwise mask, and bitwise shift.
    /// </summary>
    /// <param name="address">
    ///     A <see cref="MemoryAddress"/> representing the memory address to monitor.
    /// </param>
    /// <param name="mask">
    ///     A <see cref="byte"/> representing the bitwise mask of relevant bits.
    /// </param>
    /// <param name="shift">
    ///     An <see cref="int"/> representing the number of bitwise digits to right shift the memory address value.
    /// </param>
    public AutoTrackBitwiseIntegerValue(MemoryAddress address, byte mask, int shift)
    {
        _mask = mask;
        _shift = shift;

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
            
        var maskedValue = (byte)(addressValue & _mask);
        var newValue = (byte)(maskedValue >> _shift);
            
        return newValue;
    }
}