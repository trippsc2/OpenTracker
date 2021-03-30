using System.Collections.Generic;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    /// <summary>
    ///     This interface contains the logic for updating the dungeon accessibility.
    /// </summary>
    public interface IDungeonAccessibilityProvider : IReactiveObject
    {
        /// <summary>
        ///     A boolean representing whether the first inaccessible item is visible.
        /// </summary>
        bool Visible { get; }
        
        /// <summary>
        ///     A boolean representing whether the accessible items require a sequence break.
        /// </summary>
        bool SequenceBreak { get; }
        
        /// <summary>
        ///     A 32-bit signed integer representing the accessible items.
        /// </summary>
        int Accessible { get; }
        
        /// <summary>
        ///     A list of boss accessibility providers.
        /// </summary>
        List<IBossAccessibilityProvider> BossAccessibilityProviders { get; }

        /// <summary>
        ///     A factory for creating dungeon accessibility providers.
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon data.
        /// </param>
        /// <returns>
        ///     A new dungeon accessibility provider.
        /// </returns>
        delegate IDungeonAccessibilityProvider Factory(IDungeon dungeon);
    }
}