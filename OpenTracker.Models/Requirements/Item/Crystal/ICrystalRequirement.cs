namespace OpenTracker.Models.Requirements.Item.Crystal
{
    /// <summary>
    ///     This interface contains GT crystal requirement data.
    /// </summary>
    public interface ICrystalRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new crystal requirements.
        /// </summary>
        /// <returns>
        ///     A new crystal requirement.
        /// </returns>
        delegate ICrystalRequirement Factory();
    }
}