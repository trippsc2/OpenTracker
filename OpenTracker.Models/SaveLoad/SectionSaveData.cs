using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This is the class for containing save data for the ISection-implementing classes.
    /// </summary>
    public class SectionSaveData
    {
        public int Available { get; set; }
        public bool UserManipulated { get; set; }
        public MarkingType? Marking { get; set; }
    }
}
