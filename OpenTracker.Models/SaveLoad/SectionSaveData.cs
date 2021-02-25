using OpenTracker.Models.Markings;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This class contains section save data.
    /// </summary>
    public class SectionSaveData
    {
        public int Available { get; set; }
        public bool UserManipulated { get; set; }
        public MarkType? Marking { get; set; }
    }
}
