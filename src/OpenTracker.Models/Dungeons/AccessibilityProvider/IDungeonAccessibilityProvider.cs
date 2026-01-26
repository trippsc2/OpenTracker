using System.Collections.Generic;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider;

/// <summary>
/// This interface contains the logic for updating the dungeon accessibility.
/// </summary>
public interface IDungeonAccessibilityProvider : IReactiveObject
{
    /// <summary>
    /// A <see cref="bool"/> representing whether the first inaccessible item is visible.
    /// </summary>
    bool Visible { get; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether the accessible items require a sequence break.
    /// </summary>
    bool SequenceBreak { get; }
        
    /// <summary>
    /// A <see cref="int"/> integer representing the accessible items.
    /// </summary>
    int Accessible { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="IBossAccessibilityProvider"/> for the dungeon.
    /// </summary>
    IList<IBossAccessibilityProvider> BossAccessibilityProviders { get; }

    /// <summary>
    /// A factory for creating new <see cref="IDungeonAccessibilityProvider"/> objects.
    /// </summary>
    /// <param name="dungeon">
    ///     The <see cref="IDungeon"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IDungeonAccessibilityProvider"/> object.
    /// </returns>
    delegate IDungeonAccessibilityProvider Factory(IDungeon dungeon);
}