using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Dungeons.KeyLayouts;

/// <summary>
/// This class contains the end of key layout data.
/// </summary>
[DependencyInjection]
public sealed class EndKeyLayout : IEndKeyLayout
{
    private readonly IRequirement? _requirement;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for this key layout to be valid.
    /// </param>
    public EndKeyLayout(IRequirement? requirement = null)
    {
        _requirement = requirement;
    }

    public bool CanBeTrue(IList<DungeonItemID> inaccessible, IList<DungeonItemID> accessible, IDungeonState state)
    {
        return _requirement is null || _requirement.Met;
    }
}