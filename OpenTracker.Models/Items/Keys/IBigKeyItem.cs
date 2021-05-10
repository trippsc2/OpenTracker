using System.Collections.Generic;
using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items.Keys
{
    /// <summary>
    /// This interface contains the big key item data.
    /// </summary>
    public interface IBigKeyItem : ICappedItem
    {
        /// <summary>
        /// A factory for creating new <see cref="IBigKeyItem"/> objects.
        /// </summary>
        /// <param name="nonKeyDropMaximum">
        ///     A <see cref="int"/> representing the item maximum when key drop shuffle is disabled.
        /// </param>
        /// <param name="keyDropMaximum">
        ///     A <see cref="int"/> representing the item maximum when key drop shuffle is enabled.
        /// </param>
        /// <param name="autoTrackValue">
        ///     A nullable <see cref="IAutoTrackValue"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IBigKeyItem"/> object.
        /// </returns>
        new delegate IBigKeyItem Factory(int nonKeyDropMaximum, int keyDropMaximum, IAutoTrackValue? autoTrackValue);
        
        /// <summary>
        /// Returns a <see cref="IList{T}"/> of possible key values.
        /// </summary>
        /// <returns>
        ///     A <see cref="IList{T}"/> of <see cref="bool"/> representing the possible big key values.
        /// </returns>
        IList<bool> GetKeyValues();
    }
}