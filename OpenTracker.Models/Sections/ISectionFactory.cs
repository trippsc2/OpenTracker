using OpenTracker.Models.Locations;
using System.Collections.Generic;

namespace OpenTracker.Models.Sections
{
    public interface ISectionFactory
    {
        List<ISection> GetSections(LocationID id);
    }
}