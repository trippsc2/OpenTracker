using OpenTracker.Models.Markings;
using System.Collections.Generic;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This is the class for containing save data for the Location class.
    /// </summary>
    public class LocationSaveData
    {
        public List<SectionSaveData>? Sections { get; set; }
        public List<MarkType?>? Markings { get; set; }
    }
}
