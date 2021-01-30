using OpenTracker.Models.Dungeons;

namespace OpenTracker.Models.KeyLayouts
{
    /// <summary>
    /// This is the interface for key layouts.
    /// </summary>
    public interface IKeyLayout
    {
        ValidationStatus CanBeTrue(IMutableDungeon dungeonData, int smallKeys, bool bigKey);
    }
}
