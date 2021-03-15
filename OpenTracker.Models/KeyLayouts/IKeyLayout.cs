using OpenTracker.Models.Dungeons;

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
