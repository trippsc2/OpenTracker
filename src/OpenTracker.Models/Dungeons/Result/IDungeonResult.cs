using System.Collections.Generic;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Dungeons.Result;

/// <summary>
/// This interface contains dungeon accessibility result data.
/// </summary>
public interface IDungeonResult
{
    /// <summary>
    /// The <see cref="IList{T}"/> of <see cref="AccessibilityLevel"/> for each boss.
    /// </summary>
    IList<AccessibilityLevel> BossAccessibility { get; }
        
    /// <summary>
    /// The <see cref="int"/> representing the accessible items.
    /// </summary>
    int Accessible { get; }
        
    /// <summary>
    /// The <see cref="bool"/> representing whether sequence-breaking was allowed.
    /// </summary>
    bool SequenceBreak { get; }
        
    /// <summary>
    /// The <see cref="bool"/> representing whether the final inaccessible item is visible.
    /// </summary>
    bool Visible { get; }
    int MinimumInaccessible { get; }

    /// <summary>
    /// A factory for creating new <see cref="IDungeonResult"/> objects.
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
    /// <returns>
    ///     A new <see cref="IDungeonResult"/> object.
    /// </returns>
    delegate IDungeonResult Factory(
        IList<AccessibilityLevel> bossAccessibility, int accessible, bool sequenceBreak, bool visible,
        int minimumInaccessible = 0);
}