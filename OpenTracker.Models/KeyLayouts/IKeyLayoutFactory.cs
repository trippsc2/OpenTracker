using OpenTracker.Models.Dungeons;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This interface contains creation logic for key layout data.
    /// </summary>
    public interface IKeyLayoutFactory
    {
        List<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon);
    }
}