using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories
{
    /// <summary>
    /// This interface contains creation logic for key layout data.
    /// </summary>
    public interface IKeyLayoutFactory
    {
        IList<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon);
    }
}