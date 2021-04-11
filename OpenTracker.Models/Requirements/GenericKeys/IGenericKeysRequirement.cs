namespace OpenTracker.Models.Requirements.GenericKeys
{
    /// <summary>
    ///     This class contains generic keys requirement data.
    /// </summary>
    public interface IGenericKeysRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new generic key requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     The required enemy shuffle value.
        /// </param>
        /// <returns>
        ///     A new generic key requirement.
        /// </returns>
        delegate IGenericKeysRequirement Factory(bool expectedValue);
    }
}