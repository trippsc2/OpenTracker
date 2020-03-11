using OpenTracker.Enums;
using OpenTracker.Models;
using System.Collections.Generic;

namespace OpenTracker.Interfaces
{
    public interface ILocation
    {
        List<LocationPlacement> Placements { get; }
        Accessibility GetAccessibility();
    }
}
