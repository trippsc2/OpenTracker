namespace OpenTracker.Models.Requirements.Item
{
    /// <summary>
    /// This interface contains GT crystal requirement data.
    /// </summary>
    public interface ICrystalRequirement : IRequirement
    {
        delegate ICrystalRequirement Factory();
    }
}