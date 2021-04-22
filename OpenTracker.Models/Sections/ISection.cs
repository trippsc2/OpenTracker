using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Markings;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    ///     This base interface contains section data.
    /// </summary>
    public interface ISection : IReactiveObject, ISaveable<SectionSaveData>
    {
        /// <summary>
        ///     A string representing the name of the section.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        ///     A boolean representing whether the user has manipulated this section.
        /// </summary>
        bool UserManipulated { get; set; }
        
        /// <summary>
        ///     The marking of this section.
        /// </summary>
        IMarking? Marking { get; }

        /// <summary>
        ///     The accessibility level of the section.
        /// </summary>
        AccessibilityLevel Accessibility { get; }

        /// <summary>
        ///     A 32-bit signed integer representing the section availability.
        /// </summary>
        int Available { get; set; }
        
        /// <summary>
        ///     A boolean representing whether the section is active.
        /// </summary>
        bool IsActive { get; }
        
        /// <summary>
        ///     A boolean representing whether the section should be displayed on the map.
        /// </summary>
        bool ShouldBeDisplayed { get; }

        int Total { get; }

        /// <summary>
        ///     A boolean representing whether the section is available.
        /// </summary>
        /// <returns>
        ///     Whether the section is available.
        /// </returns>
        bool IsAvailable();
        
        /// <summary>
        ///     A boolean representing whether the section can be cleared/collected.
        /// </summary>
        /// <param name="force">
        ///     A boolean representing whether the accessibility logic should be obeyed.
        /// </param>
        /// <returns>
        ///     Whether the section can be cleared/collected.
        /// </returns>
        bool CanBeCleared(bool force = false);
        
        /// <summary>
        ///     Clears the section, specifying whether to obey accessibility logic.
        /// </summary>
        /// <param name="force">
        ///     A boolean representing whether the accessibility logic should be obeyed.
        /// </param>
        void Clear(bool force);

        /// <summary>
        ///     Creates an undoable action to collect the section and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="force">
        ///     A boolean representing whether to override the logic while collecting the section.
        /// </param>
        IUndoable CreateCollectSectionAction(bool force);

        /// <summary>
        ///     Returns whether the section can be uncollected.
        /// </summary>
        /// <returns>
        ///     A boolean representing whether the section can be uncollected.
        /// </returns>
        bool CanBeUncleared();

        /// <summary>
        ///     Creates an undoable action to uncollect the section and sends it to the undo/redo manager.
        /// </summary>
        IUndoable CreateUncollectSectionAction();
        
        /// <summary>
        ///     Resets the section to its starting values.
        /// </summary>
        void Reset();
    }
}
