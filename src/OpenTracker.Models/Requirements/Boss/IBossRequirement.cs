using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Requirements.Boss;

/// <summary>
/// This interface contains <see cref="IBossPlacement"/> requirement data.
/// </summary>
public interface IBossRequirement : IRequirement
{
    /// <summary>
    /// A factory method for creating new <see cref="IBossRequirement"/> objects.
    /// </summary>
    delegate IBossRequirement Factory(IBossPlacement bossPlacement);

}