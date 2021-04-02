using System.Collections.Generic;
using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items.Keys
{
    /// <summary>
    ///     This interface contains the item data for small keys.
    /// </summary>
    public interface ISmallKeyItem : ICappedItem
    {
        /// <summary>
        ///     A 32-bit signed integer representing the effective current.
        /// </summary>
        int EffectiveCurrent { get; }

        /// <summary>
        ///     A factory for creating small key items.
        /// </summary>
        /// <param name="genericKey">
        ///     The generic key item.
        /// </param>
        /// <param name="nonKeyDropMaximum">
        ///     A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        /// <param name="keyDropMaximum">
        ///     A 32-bit signed integer representing the delta maximum for key drop shuffle of the item.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-track value.
        /// </param>
        /// <returns>
        ///     A new small key item.
        /// </returns>
        delegate ISmallKeyItem Factory(
            IItem genericKey, int nonKeyDropMaximum, int keyDropMaximum, IAutoTrackValue? autoTrackValue);
        
        /// <summary>
        ///     Returns a list of possible key values.
        /// </summary>
        /// <returns>
        ///     A list of possible small key values.
        /// </returns>
        IList<int> GetKeyValues();
    }
}