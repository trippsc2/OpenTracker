using OpenTracker.Enums;
using OpenTracker.Interfaces;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class EntranceLocation : ILocation
    {
        public List<LocationPlacement> Placements { get; }

        public Accessibility GetAccessibility()
        {
            throw new System.NotImplementedException();
        }
    }
}
