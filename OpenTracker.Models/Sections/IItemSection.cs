namespace OpenTracker.Models.Sections
{
    public interface IItemSection : ISection
    {
        int Total { get; }
        int Accessible { get; }
    }
}
