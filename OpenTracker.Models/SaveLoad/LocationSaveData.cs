using OpenTracker.Models.Markings;
using System.Collections.Generic;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This contains location save data.
    /// </summary>
    public class LocationSaveData
    {
        public List<SectionSaveData>? Sections { get; set; }
        public List<MarkType?>? Markings { get; set; }
    }
}
