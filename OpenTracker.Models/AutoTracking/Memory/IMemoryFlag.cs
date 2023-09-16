using System;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.Memory;

/// <summary>
/// Represents a SNES memory bitwise boolean flag.
/// </summary>
public interface IMemoryFlag : INotifyPropertyChanged
{
    /// <summary>
    /// A <see cref="Nullable{T}"/> of <see cref="bool"/> representing whether the flag is set.
    /// </summary>
    bool? Status { get; }

    /// <summary>
    /// A factory for creating new <see cref="IMemoryFlag"/> objects.
    /// </summary>
    /// <param name="memoryAddress">
    ///     A <see cref="MemoryAddress"/> representing the memory address containing the bitwise boolean flag.
    /// </param>
    /// <param name="flag">
    ///     A <see cref="byte"/> representing the bitwise flag.
    /// </param>
    /// <returns>
    ///     A new <see cref="IMemoryFlag"/> object.
    /// </returns>
    delegate IMemoryFlag Factory(MemoryAddress memoryAddress, byte flag);
}