using OpenTracker.Models.Locations;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.PinnedLocations
{
    public interface IPinnedLocationDictionary : IDictionary<LocationID, IPinnedLocationVM>
    {
    }
}
