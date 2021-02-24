namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This interface contains crystal requirement data.
    /// </summary>
    public interface ICrystalRequirementItem : IItem
    {
        bool Known { get; set; }
    }
}