using OpenTracker.Models.Markings;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the interface for markable sections.
    /// </summary>
    public interface IMarkableSection : ISection
    {
        IMarking Marking { get; }
    }
}
