using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Locations;

/// <summary>
/// This class contains the <see cref="ObservableCollection{T}"/> container for pinned location data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class PinnedLocationCollection : ObservableCollection<ILocation>, IPinnedLocationCollection
{
    private readonly ILocationDictionary _locations;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="locations">
    ///     The <see cref="ILocationDictionary"/>.
    /// </param>
    public PinnedLocationCollection(ILocationDictionary locations)
    {
        _locations = locations;
    }

    public IList<LocationID> Save()
    {
        return this.Select(pinnedLocation => pinnedLocation.ID).ToList();
    }

    public void Load(IList<LocationID>? saveData)
    {
        if (saveData == null)
        {
            return;
        }

        Clear();

        foreach (var location in saveData)
        {
            Add(_locations[location]);
        }
    }
}