using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.GuaranteedBossItems;

/// <summary>
/// This interface contains <see cref="IMode.GuaranteedBossItems"/> requirement data.
/// </summary>
public interface IGuaranteedBossItemsRequirement : IRequirement
{
    /// <summary>
    /// A factory for creating new <see cref="IGuaranteedBossItemsRequirement"/> objects.
    /// </summary>
    /// <param name="expectedValue">
    ///     A <see cref="bool"/> representing the expected <see cref="IMode.GuaranteedBossItems"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IGuaranteedBossItemsRequirement"/> object.
    /// </returns>
    delegate IGuaranteedBossItemsRequirement Factory(bool expectedValue);
}