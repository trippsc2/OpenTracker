using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    /// This interface contains boss placement requirement data.
    /// </summary>
    public interface IBossRequirement : IRequirement
    {
        delegate IBossRequirement Factory(IBossPlacement bossPlacement);
    }
}