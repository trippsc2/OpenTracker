using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Boolean;

/// <summary>
/// This interface contains shop section data.
/// </summary>
public interface IShopSection : ISection
{
    /// <summary>
    /// A factory for creating new <see cref="IShopSection"/> objects.
    /// </summary>
    /// <param name="node">
    ///     The <see cref="INode"/> to which this section belongs.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for this section to be active.
    /// </param>
    /// <returns>
    ///     A new <see cref="IShopSection"/> object.
    /// </returns>
    delegate IShopSection Factory(INode node, IRequirement requirement);
}