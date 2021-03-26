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
    /// This interface contains location data.
    /// </summary>
    public interface ILocation : IReactiveObject, ISaveable<LocationSaveData>
    {
        AccessibilityLevel Accessibility { get; }
        int Accessible { get; }
        int Available { get; }
        LocationID ID { get; }
        List<IMapLocation> MapLocations { get; }
        string Name { get; }
        List<ISection> Sections { get; }
        int Total { get; }
        ILocationNoteCollection Notes { get; }
        bool Visible { get; }

        delegate ILocation Factory(LocationID id);

        bool CanBeCleared(bool force);
        void Reset();

        /// <summary>
        /// Creates a new undoable action to add a note and sends it to the undo/redo manager.
        /// </summary>
        IUndoable CreateAddNoteAction();

        /// <summary>
        /// Creates a new undoable action to remove a note and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="note">
        ///     The note to be removed.
        /// </param>
        IUndoable CreateRemoveNoteAction(IMarking note);

        /// <summary>
        /// Returns a new undoable action to clear the location.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to ignore the logic.
        /// </param>
        /// <returns>
        /// A new undoable action to clear the location.
        /// </returns>
        IUndoable CreateClearLocationAction(bool force = false);

        /// <summary>
        /// Returns a new undoable action to pin the location.
        /// </summary>
        /// <returns>
        /// A new undoable action to pin the location.
        /// </returns>
        IUndoable CreatePinLocationAction();

        /// <summary>
        /// Returns a new undoable action to unpin the location.
        /// </summary>
        /// <returns>
        /// A new undoable action to unpin the location.
        /// </returns>
        IUndoable CreateUnpinLocationAction();
    }
}