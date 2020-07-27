using OpenTracker.Models.PrizePlacements;

namespace OpenTracker.Models.Sections
{
    public interface IPrizeSection : IBossSection
    {
        IPrizePlacement PrizePlacement { get; }
    }
}
