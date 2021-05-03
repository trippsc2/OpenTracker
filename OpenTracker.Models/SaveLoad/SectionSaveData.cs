using OpenTracker.Models.Markings;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This class contains section save data.
    /// </summary>
    public class SectionSaveData
    {
        public MarkType? Marking { get; set; }
        public bool UserManipulated { get; set; }
        public int Available { get; set; }
    }
}
