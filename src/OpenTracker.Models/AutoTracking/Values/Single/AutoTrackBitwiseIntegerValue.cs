using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single;

/// <summary>
/// This class contains the auto-tracking result value of a memory address bitwise integer.
/// </summary>
public class AutoTrackBitwiseIntegerValue : AutoTrackValueBase, IAutoTrackBitwiseIntegerValue
{
    private readonly IMemoryAddress _address;
    private readonly byte _mask;
    private readonly int _shift;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="address">
    ///     The <see cref="IMemoryAddress"/> to be represented.
    /// </param>
    /// <param name="mask">
    ///     A <see cref="byte"/> representing the bitwise mask of relevant bits.
    /// </param>
    /// <param name="shift">
    ///     A <see cref="int"/> representing the number of bitwise digits to right shift the address value.
    /// </param>
    public AutoTrackBitwiseIntegerValue(IMemoryAddress address, byte mask, int shift)
    {
        _address = address;
        _mask = mask;
        _shift = shift;
            
        UpdateValue();

        _address.PropertyChanged += OnMemoryChanged;
    }

    /// <summary>
    /// Subscribes to the <see cref="IMemoryAddress.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event was sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnMemoryChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IMemoryAddress.Value))
        {
            UpdateValue();
        }
    }

    protected override int? GetNewValue()
    {
        if (_address.Value is null)
        {
            return null;
        }
            
        var maskedValue = (byte)(_address.Value & _mask);
        var newValue = (byte)(maskedValue >> _shift);
        return newValue;
    }
}