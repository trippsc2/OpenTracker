using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Locations;
using OpenTracker.Models.UndoRedo.Notes;
using ReactiveUI;

namespace OpenTracker.Models.Locations;

/// <summary>
/// This interface contains location data.
/// </summary>
public interface ILocation : IReactiveObject, IResettable, ISaveable<LocationSaveData>
{
    /// <summary>
    /// The <see cref="LocationID"/>.
    /// </summary>
    LocationID ID { get; }
        
    /// <summary>
    /// A <see cref="string"/> representing the location name.
    /// </summary>
    string Name { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="IMapLocation"/>.
    /// </summary>
    IList<IMapLocation> MapLocations { get; }
        
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="ISection"/>.
    /// </summary>
    IList<ISection> Sections { get; }
        
    /// <summary>
    /// A <see cref="ILocationNoteCollection"/> for this location.
    /// </summary>
    ILocationNoteCollection Notes { get; }
        
    /// <summary>
    /// The <see cref="AccessibilityLevel"/>.
    /// </summary>
    AccessibilityLevel Accessibility { get; }
        
    /// <summary>
    /// A <see cref="int"/> representing the number of accessible items.
    /// </summary>
    int Accessible { get; }
        
    /// <summary>
    /// A <see cref="int"/> representing the number of unchecked items.
    /// </summary>
    int Available { get; }
        
    /// <summary>
    /// A <see cref="int"/> representing the number of checked and unchecked items.
    /// </summary>
    int Total { get; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether the location should be displayed normally.
    /// </summary>
    bool ShouldBeDisplayed { get; }

    /// <summary>
    /// A <see cref="bool"/> representing whether the location is active.
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// A factory for creating new <see cref="ILocation"/> objects.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <param name="name">
    ///     A <see cref="string"/> representing the location name.
    /// </param>
    /// <returns>
    ///     A new <see cref="ILocation"/> object.
    /// </returns>
    delegate ILocation Factory(LocationID id, string name);

    /// <summary>
    /// Returns whether the location can be cleared.
    /// </summary>
    /// <param name="force">
    ///     A <see cref="bool"/> representing whether the accessibility logic should be obeyed.
    /// </param>
    /// <returns>
    ///     A <see cref="bool"/> representing whether the location can be cleared.
    /// </returns>
    bool CanBeCleared(bool force);

    /// <summary>
    /// Returns a new <see cref="IAddNote"/> object.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IAddNote"/> object.
    /// </returns>
    IUndoable CreateAddNoteAction();

    /// <summary>
    /// Returns a new <see cref="IRemoveNote"/> object.
    /// </summary>
    /// <param name="note">
    ///     A <see cref="IMarking"/> representing the note to be removed.
    /// </param>
    /// <returns>
    ///     A new <see cref="IRemoveNote"/> object.
    /// </returns>
    IUndoable CreateRemoveNoteAction(IMarking note);

    /// <summary>
    /// Returns a new <see cref="IClearLocation"/> object.
    /// </summary>
    /// <param name="force">
    ///     A <see cref="bool"/> representing whether the accessibility logic should be obeyed.
    /// </param>
    /// <returns>
    ///     A new <see cref="IClearLocation"/> object.
    /// </returns>
    IUndoable CreateClearLocationAction(bool force = false);

    /// <summary>
    /// Returns a new <see cref="IPinLocation"/> object.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IPinLocation"/> object.
    /// </returns>
    IUndoable CreatePinLocationAction();

    /// <summary>
    /// Returns a new <see cref="IUnpinLocation"/> object.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IUnpinLocation"/> object.
    /// </returns>
    IUndoable CreateUnpinLocationAction();
}