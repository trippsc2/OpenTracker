using OpenTracker.Models.Markings;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This class contains section save data.
    /// </summary>
    public class SectionSaveData
    {
        public MarkType? Marking { get; init; }
        public bool UserManipulated { get; init; }
        public int Available { get; init; }
    }
}
