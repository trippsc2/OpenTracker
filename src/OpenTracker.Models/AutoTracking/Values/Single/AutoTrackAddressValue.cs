using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single;

/// <summary>
/// This class contains the auto-tracking result value of a memory address value.
/// </summary>
public class AutoTrackAddressValue : AutoTrackValueBase, IAutoTrackAddressValue
{
    private readonly IMemoryAddress _address;
    private readonly byte _maximum;
    private readonly int _adjustment;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="address">
    ///     The <see cref="IMemoryAddress"/> for the comparison.
    /// </param>
    /// <param name="maximum">
    ///     A <see cref="byte"/> representing the maximum valid value of the memory address.
    /// </param>
    /// <param name="adjustment">
    ///     A <see cref="int"/> representing the amount that the result value should be adjusted from the actual
    ///     value.
    /// </param>
    public AutoTrackAddressValue(IMemoryAddress address, byte maximum, int adjustment)
    {
        _address = address;
        _maximum = maximum;
        _adjustment = adjustment;

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
            
        var newValue = _address.Value.Value + _adjustment;

        return newValue > _maximum ? null : newValue;
    }
}