using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Markings;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    ///     This interface contains location data.
    /// </summary>
    public interface ILocation : IReactiveObject, ISaveable<LocationSaveData>
    {
        /// <summary>
        ///     The location ID.
        /// </summary>
        LocationID ID { get; }
        
        /// <summary>
        ///     A string representing the name of the location.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        ///     A list of map locations for the location.
        /// </summary>
        IList<IMapLocation> MapLocations { get; }
        
        /// <summary>
        ///     A list of sections for this location.
        /// </summary>
        IList<ISection> Sections { get; }
        
        /// <summary>
        ///     A collection of notes for this location.
        /// </summary>
        ILocationNoteCollection Notes { get; }
        
        /// <summary>
        ///     The accessibility of the location.
        /// </summary>
        AccessibilityLevel Accessibility { get; }
        
        /// <summary>
        ///     The number of accessible items for the location.
        /// </summary>
        int Accessible { get; }
        
        /// <summary>
        ///     The number of unchecked items for the location.
        /// </summary>
        int Available { get; }
        
        /// <summary>
        ///     The number of checked and unchecked items for the location.
        /// </summary>
        int Total { get; }
        
        /// <summary>
        ///     A boolean representing whether the location is visible in the UI.
        /// </summary>
        bool Visible { get; }

        delegate ILocation Factory(LocationID id);

        /// <summary>
        ///     Returns whether the location can be cleared.
        /// </summary>
        /// <returns>
        ///     A boolean representing whether the location can be cleared.
        /// </returns>
        bool CanBeCleared(bool force);

        /// <summary>
        ///     Creates a new undoable action to add a note and sends it to the undo/redo manager.
        /// </summary>
        IUndoable CreateAddNoteAction();

        /// <summary>
        ///     Creates a new undoable action to remove a note and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="note">
        ///     The note to be removed.
        /// </param>
        IUndoable CreateRemoveNoteAction(IMarking note);

        /// <summary>
        ///     Returns a new undoable action to clear the location.
        /// </summary>
        /// <param name="force">
        ///     A boolean representing whether to ignore the logic.
        /// </param>
        /// <returns>
        ///     A new undoable action to clear the location.
        /// </returns>
        IUndoable CreateClearLocationAction(bool force = false);

        /// <summary>
        ///     Returns a new undoable action to pin the location.
        /// </summary>
        /// <returns>
        ///     A new undoable action to pin the location.
        /// </returns>
        IUndoable CreatePinLocationAction();

        /// <summary>
        ///     Returns a new undoable action to unpin the location.
        /// </summary>
        /// <returns>
        ///     A new undoable action to unpin the location.
        /// </returns>
        IUndoable CreateUnpinLocationAction();

        /// <summary>
        ///     Resets the location to its starting values.
        /// </summary>
        void Reset();
    }
}