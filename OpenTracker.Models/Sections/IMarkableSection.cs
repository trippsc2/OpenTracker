using OpenTracker.Models.Markings;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This interface contains markable section data.
    /// </summary>
    public interface IMarkableSection : ISection
    {
        IMarking Marking { get; }
    }
}
