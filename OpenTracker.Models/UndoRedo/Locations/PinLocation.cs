using OpenTracker.Models.Locations;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.UndoRedo.Locations;

/// <summary>
/// This class contains the <see cref="IUndoable"/> action to pin a <see cref="ILocation"/>.
/// </summary>
[DependencyInjection]
public sealed class PinLocation : IPinLocation
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
    public PinLocation(IPinnedLocationCollection pinnedLocations, ILocation location)
    {
        _pinnedLocations = pinnedLocations;
        _location = location;
    }

    public bool CanExecute()
    {
        if (!_pinnedLocations.Contains(_location))
        {
            return true;
        }

        return _pinnedLocations.IndexOf(_location) != 0;
    }

    public void ExecuteDo()
    {
        if (_pinnedLocations.Contains(_location))
        {
            _existingIndex = _pinnedLocations.IndexOf(_location);
            _pinnedLocations.Remove(_location);
        }
            
        _pinnedLocations.Insert(0, _location);
    }

    public void ExecuteUndo()
    {
        _pinnedLocations.Remove(_location);

        if (_existingIndex.HasValue)
        {
            _pinnedLocations.Insert(_existingIndex.Value, _location);
        }
    }
}