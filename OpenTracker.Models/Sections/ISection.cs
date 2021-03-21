using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This base interface contains section data.
    /// </summary>
    public interface ISection : IReactiveObject
    {
        string Name { get; }
        IRequirement Requirement { get; }
        AccessibilityLevel Accessibility { get; }
        bool UserManipulated { get; set; }
        int Available { get; set; }

        bool IsAvailable();
        bool CanBeCleared(bool force);
        void Clear(bool force);
        void Reset();
        SectionSaveData Save();
        void Load(SectionSaveData saveData);

        /// <summary>
        /// Creates an undoable action to collect the section and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to override the logic while collecting the section.
        /// </param>
        void CollectSection(bool force);

        /// <summary>
        /// Returns whether the section can be uncollected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section can be uncollected.
        /// </returns>
        bool CanBeUncleared();

        /// <summary>
        /// Creates an undoable action to uncollect the section and sends it to the undo/redo manager.
        /// </summary>
        void UncollectSection();
    }
}
