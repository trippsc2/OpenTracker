using OpenTracker.Models.Locations;
using System.Collections.Generic;

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