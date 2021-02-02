namespace OpenTracker.Models.Items
{
    public interface ICrystalRequirementItem : IItem
    {
        bool Known { get; set; }
    }
}