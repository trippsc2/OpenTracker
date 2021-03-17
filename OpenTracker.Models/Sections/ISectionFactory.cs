using System.Collections.Generic;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This interface contains creation logic for section data.
    /// </summary>
    public interface ISectionFactory
    {
        List<ISection> GetSections(LocationID id);
    }
}