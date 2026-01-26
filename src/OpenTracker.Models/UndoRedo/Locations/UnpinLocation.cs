using System;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo.Locations;

/// <summary>
/// This class contains undoable action data to unpin a location.
/// </summary>
public class UnpinLocation : IUnpinLocation
{
    private readonly IPinnedLocationCollection _pinnedLocations;

    private readonly ILocation _location;

    private int? _existingIndex;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="pinnedLocations">
    ///     The <see cref="IPinnedLocationCollection"/>.
    /// </param>
    /// <param name="location">
    ///     The <see cref="ILocation"/>.
    /// </param>
    public UnpinLocation(IPinnedLocationCollection pinnedLocations, ILocation location)
    {
        _pinnedLocations = pinnedLocations;
        _location = location;
    }

    public bool CanExecute()
    {
        return true;
    }

    public void ExecuteDo()
    {
        _existingIndex = _pinnedLocations.IndexOf(_location);
        _pinnedLocations.Remove(_location);
    }

    public void ExecuteUndo()
    {
        if (_existingIndex is null)
        {
            throw new NullReferenceException("_existingIndex is not defined.");
        }
            
        _pinnedLocations.Insert(_existingIndex.Value, _location);
    }
}