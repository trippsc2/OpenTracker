using OpenTracker.Models.Items;

namespace OpenTracker.Models.Requirements.Item;

/// <summary>
/// This interface contains <see cref="IItem"/> <see cref="IRequirement"/> data.
/// </summary>
public interface IItemRequirement : IRequirement
{
    /// <summary>
    ///     A factory for creating new <see cref="IItemRequirement"/> objects.
    /// </summary>
    /// <param name="item">
    ///     The <see cref="IItem"/>.
    /// </param>
    /// <param name="count">
    ///     A <see cref="int"/> representing the number of the item required.
    /// </param>
    /// <returns>
    ///     A new <see cref="IItemRequirement"/> object.
    /// </returns>
    delegate IItemRequirement Factory(IItem item, int count = 1);
}