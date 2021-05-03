using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories
{
    /// <summary>
    ///     This interface contains creation logic for key layout data.
    /// </summary>
    public interface IKeyLayoutFactory
    {
        /// <summary>
        ///     Returns the list of key layouts for the specified dungeon.
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon parent class.
        /// </param>
        /// <returns>
        ///     The list of key layouts.
        /// </returns>
        IList<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon);
    }
}