namespace OpenTracker.Models.Requirements.GuaranteedBossItems
{
    /// <summary>
    ///     This interface contains guaranteed boss items requirement data.
    /// </summary>
    public interface IGuaranteedBossItemsRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new guaranteed boss items requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     A boolean expected guaranteed boss items value.
        /// </param>
        /// <returns>
        ///     A new guaranteed boss items requirement.
        /// </returns>
        delegate IGuaranteedBossItemsRequirement Factory(bool expectedValue);
    }
}