using OpenTracker.Models.Dungeons;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This is the interface for key layouts.
    /// </summary>
    public interface IKeyLayout
    {
        bool CanBeTrue(IMutableDungeon dungeonData, IDungeonState state);
    }
}
