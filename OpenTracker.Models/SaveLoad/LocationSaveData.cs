using System.Collections.Generic;
using OpenTracker.Models.Markings;

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
