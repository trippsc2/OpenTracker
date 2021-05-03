using System.Collections.Generic;
using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items.Keys
{
    /// <summary>
    ///     This interface contains the item data for big keys.
    /// </summary>
    public interface IBigKeyItem : ICappedItem
    {
        /// <summary>
        ///     A factory for creating big key items.
        /// </summary>
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
        ///     A new big key item.
        /// </returns>
        new delegate IBigKeyItem Factory(int nonKeyDropMaximum, int keyDropMaximum, IAutoTrackValue? autoTrackValue);
        
        /// <summary>
        ///     Returns a list of possible key values.
        /// </summary>
        /// <returns>
        ///     A list of possible big key values.
        /// </returns>
        IList<bool> GetKeyValues();
    }
}