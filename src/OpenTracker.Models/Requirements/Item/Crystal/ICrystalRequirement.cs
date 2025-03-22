namespace OpenTracker.Models.Requirements.Item.Crystal
{
    /// <summary>
    /// This interface contains GT crystal <see cref="IRequirement"/> data.
    /// </summary>
    public interface ICrystalRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating the <see cref="ICrystalRequirement"/> object.
        /// </summary>
        /// <returns>
        ///     The <see cref="ICrystalRequirement"/> object.
        /// </returns>
        delegate ICrystalRequirement Factory();
    }
}