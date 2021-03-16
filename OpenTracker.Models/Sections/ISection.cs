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
        bool CanBeUncleared();
        SectionSaveData Save();
        void Load(SectionSaveData saveData);
    }
}
