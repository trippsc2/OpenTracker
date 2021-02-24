using OpenTracker.Models.Dungeons;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyLayouts
{
    public interface IKeyLayoutFactory
    {
        List<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon);
    }
}