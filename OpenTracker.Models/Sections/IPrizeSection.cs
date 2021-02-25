using OpenTracker.Models.PrizePlacements;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This interface contains boss/prize section data (end of each dungeon).
    /// </summary>
    public interface IPrizeSection : IBossSection
    {
        IPrizePlacement PrizePlacement { get; }
    }
}
