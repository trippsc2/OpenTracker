using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Sections;
using ReactiveUI;

namespace OpenTracker.Models.Sections;

/// <summary>
/// This interface contains section data.
/// </summary>
public interface ISection : IReactiveObject, IResettable, ISaveable<SectionSaveData>
{
    /// <summary>
    /// A <see cref="string"/> representing the section name.
    /// </summary>
    string Name { get; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether the user has manipulated the section.
    /// </summary>
    bool UserManipulated { get; set; }
        
    /// <summary>
    /// A nullable <see cref="IMarking"/>.
    /// </summary>
    IMarking? Marking { get; }

    /// <summary>
    /// The <see cref="AccessibilityLevel"/>.
    /// </summary>
    AccessibilityLevel Accessibility { get; }

    /// <summary>
    /// A <see cref="int"/> representing the number of available items.
    /// </summary>
    int Available { get; set; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether the section is active.
    /// </summary>
    bool IsActive { get; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether the section should be displayed on the map.
    /// </summary>
    bool ShouldBeDisplayed { get; }

    /// <summary>
    /// A <see cref="int"/> representing the total number of items.
    /// </summary>
    int Total { get; }

    /// <summary>
    /// Returns whether the section is available.
    /// </summary>
    /// <returns>
    ///     A <see cref="bool"/> representing whether the section is available.
    /// </returns>
    bool IsAvailable();
        
    /// <summary>
    /// Returns whether the section can be cleared/collected.
    /// </summary>
    /// <param name="force">
    ///     A <see cref="bool"/> representing whether the accessibility logic should be obeyed.
    /// </param>
    /// <returns>
    ///     A <see cref="bool"/> representing whether the section can be cleared/collected.
    /// </returns>
    bool CanBeCleared(bool force = false);
        
    /// <summary>
    /// Clears the section.
    /// </summary>
    /// <param name="force">
    ///     A <see cref="bool"/> representing whether the accessibility logic should be obeyed.
    /// </param>
    void Clear(bool force);

    /// <summary>
    /// Returns a new <see cref="ICollectSection"/> object.
    /// </summary>
    /// <param name="force">
    ///     A <see cref="bool"/> representing whether the accessibility logic should be obeyed.
    /// </param>
    IUndoable CreateCollectSectionAction(bool force);

    /// <summary>
    /// Returns whether the section can be uncollected.
    /// </summary>
    /// <returns>
    ///     A <see cref="bool"/> representing whether the section can be uncollected.
    /// </returns>
    bool CanBeUncleared();

    /// <summary>
    /// Returns a new <see cref="IUncollectSection"/> object.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IUncollectSection"/> object.
    /// </returns>
    IUndoable CreateUncollectSectionAction();
}