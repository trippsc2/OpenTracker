using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Dungeons.Result;

/// <summary>
/// This class contains dungeon accessibility result data.
/// </summary>
[DependencyInjection]
public sealed class DungeonResult : IDungeonResult
{
    public IList<AccessibilityLevel> BossAccessibility { get; }
    public int Accessible { get; }
    public bool SequenceBreak { get; }
    public bool Visible { get; }
    public int MinimumInaccessible { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bossAccessibility">
    ///     A <see cref="IList{T}"/> of <see cref="AccessibilityLevel"/> of each boss.
    /// </param>
    /// <param name="accessible">
    ///     A <see cref="int"/> representing the number of accessible items.
    /// </param>
    /// <param name="sequenceBreak">
    ///     A <see cref="bool"/> representing whether the result allowed sequence-breaking.
    /// </param>
    /// <param name="visible">
    ///     A <see cref="bool"/> representing whether one inaccessible item is visible.
    /// </param>
    /// <param name="minimumInaccessible">
    ///     A <see cref="int"/> representing the minimum number of checks that are inaccessible.
    /// </param>
    public DungeonResult(
        IList<AccessibilityLevel> bossAccessibility, int accessible, bool sequenceBreak, bool visible,
        int minimumInaccessible = 0)
    {
        BossAccessibility = bossAccessibility;
        Accessible = accessible;
        Visible = visible;
        MinimumInaccessible = minimumInaccessible;
        SequenceBreak = sequenceBreak;
    }
}