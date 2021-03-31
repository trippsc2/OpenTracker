using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.State;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This interface contains the key layout data.
    /// </summary>
    public interface IKeyLayout
    {
        bool CanBeTrue(IMutableDungeon dungeonData, IDungeonState state);
    }
}
