using System.Collections.Generic;
using OpenTracker.Models.Markings;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This contains location save data.
    /// </summary>
    public class LocationSaveData
    {
        public IList<SectionSaveData>? Sections { get; set; }
        public IList<MarkType?>? Markings { get; set; }
    }
}
