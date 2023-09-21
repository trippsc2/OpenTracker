using System;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.Values;

/// <summary>
/// This interface represents an auto-tracking result value.
/// </summary>
public interface IAutoTrackValue : INotifyPropertyChanged
{
    /// <summary>
    /// A <see cref="Nullable{T}"/> of <see cref="int"/> representing the current result value.
    /// </summary>
    int? CurrentValue { get; }
}