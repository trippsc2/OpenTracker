using System.Collections.Generic;
using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.PinnedLocations
{
    public interface IPinnedLocationDictionary : IDictionary<LocationID, IPinnedLocationVM>
    {
    }
}
