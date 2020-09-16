using OpenTracker.Models.PrizePlacements;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the interface for prize sections.
    /// </summary>
    public interface IPrizeSection : IBossSection
    {
        IPrizePlacement PrizePlacement { get; }
    }
}
