using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Sections.Boolean;

/// <summary>
/// This class contains shop section data.
/// </summary>
[DependencyInjection]
public sealed class ShopSection : BooleanSectionBase, IShopSection
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="saveLoadManager">
    ///     The <see cref="ISaveLoadManager"/>.
    /// </param>
    /// <param name="collectSectionFactory">
    ///     An Autofac factory for creating new <see cref="ICollectSection"/> objects.
    /// </param>
    /// <param name="uncollectSectionFactory">
    ///     An Autofac factory for creating new <see cref="IUncollectSection"/> objects.
    /// </param>
    /// <param name="node">
    ///     The <see cref="INode"/> to which this section belongs.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for this section to be active.
    /// </param>
    public ShopSection(
        ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
        IUncollectSection.Factory uncollectSectionFactory, INode node, IRequirement requirement)
        : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, "Shop", node, requirement)
    {
    }
}