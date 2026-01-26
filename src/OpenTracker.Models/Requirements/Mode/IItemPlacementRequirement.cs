using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode;

/// <summary>
/// This interface contains <see cref="IMode.ItemPlacement"/> <see cref="IRequirement"/> data.
/// </summary>
public interface IItemPlacementRequirement : IRequirement
{
    /// <summary>
    /// A factory for creating new <see cref="IItemPlacementRequirement"/> objects.
    /// </summary>
    /// <param name="expectedValue">
    ///     A <see cref="ItemPlacement"/> representing the expected <see cref="IMode.ItemPlacement"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IItemPlacementRequirement"/> object.
    /// </returns>
    delegate IItemPlacementRequirement Factory(ItemPlacement expectedValue);
}