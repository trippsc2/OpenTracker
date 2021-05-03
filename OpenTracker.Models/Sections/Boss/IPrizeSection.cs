using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.Sections.Boss
{
    /// <summary>
    ///     This interface contains end of boss section with prize data.
    /// </summary>
    public interface IPrizeSection : IBossSection
    {
        /// <summary>
        ///     The prize placement for the section.
        /// </summary>
        IPrizePlacement PrizePlacement { get; }
        
        /// <summary>
        ///     A factory for creating new prize sections.
        /// </summary>
        /// <param name="accessibilityProvider">
        ///     The boss accessibility provider for this section.
        /// </param>
        /// <param name="name">
        ///     A string representing the name of the section.
        /// </param>
        /// <param name="bossPlacement">
        ///     The boss placement for the section.
        /// </param>
        /// <param name="prizePlacement">
        ///     The prize placement for the section.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The section auto-track value.
        /// </param>
        /// <returns>
        ///     A prize section.
        /// </returns>
        new delegate IPrizeSection Factory(
            IBossAccessibilityProvider accessibilityProvider, string name, IBossPlacement bossPlacement,
            IPrizePlacement prizePlacement, IAutoTrackValue? autoTrackValue = null);

        /// <summary>
        ///     Returns a new undoable action to toggle the prize.
        /// </summary>
        /// <param name="force">
        ///     A boolean representing whether to ignore the logic.
        /// </param>
        /// <returns>
        ///     An undoable action to toggle the prize.
        /// </returns>
        IUndoable CreateTogglePrizeSectionAction(bool force);
    }
}
