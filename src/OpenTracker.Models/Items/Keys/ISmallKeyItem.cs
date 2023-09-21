using System.Collections.Generic;
using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items.Keys;

/// <summary>
/// This interface contains the small key item data.
/// </summary>
public interface ISmallKeyItem : ICappedItem
{
    /// <summary>
    /// A <see cref="int"/> representing the effective current value.
    /// </summary>
    int EffectiveCurrent { get; }

    /// <summary>
    /// A factory for creating new <see cref="ISmallKeyItem"/> objects.
    /// </summary>
    /// <param name="genericKey">
    ///     The <see cref="IItem"/> representing the generic keys item.
    /// </param>
    /// <param name="nonKeyDropMaximum">
    ///     A <see cref="int"/> representing the item maximum when key drop shuffle is disabled.
    /// </param>
    /// <param name="keyDropMaximum">
    ///     A <see cref="int"/> representing the item maximum when key drop shuffle is enabled.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="ISmallKeyItem"/> object.
    /// </returns>
    new delegate ISmallKeyItem Factory(
        IItem genericKey, int nonKeyDropMaximum, int keyDropMaximum, IAutoTrackValue? autoTrackValue);
        
    /// <summary>
    /// Returns a <see cref="IList{T}"/> of possible key values.
    /// </summary>
    /// <returns>
    ///     A <see cref="IList{T}"/> of <see cref="int"/> representing possible small key values.
    /// </returns>
    IList<int> GetKeyValues();
}