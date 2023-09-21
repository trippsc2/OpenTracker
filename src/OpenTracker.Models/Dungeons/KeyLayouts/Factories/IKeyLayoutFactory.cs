using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories;

/// <summary>
/// This interface contains creation logic for key layout data.
/// </summary>
public interface IKeyLayoutFactory
{
    /// <summary>
    /// Returns the <see cref="IList{T}"/> of <see cref="IKeyLayout"/> for the specified <see cref="IDungeon"/>.
    /// </summary>
    /// <param name="dungeon">
    ///     The <see cref="IDungeon"/> parent class.
    /// </param>
    /// <returns>
    ///     The <see cref="IList{T}"/> of <see cref="IKeyLayout"/>.
    /// </returns>
    IList<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon);
}