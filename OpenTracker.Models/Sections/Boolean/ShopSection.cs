using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Boolean
{
    /// <summary>
    ///     This class contains shop section data.
    /// </summary>
    public class ShopSection : BooleanSectionBase, IShopSection
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        ///     The save/load manager.
        /// </param>
        /// <param name="collectSectionFactory">
        ///     An Autofac factory for creating collect section undoable actions.
        /// </param>
        /// <param name="uncollectSectionFactory">
        ///     An Autofac factory for creating uncollect section undoable actions.
        /// </param>
        /// <param name="node">
        ///     The node to which this take any section belongs.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for this section to be active.
        /// </param>
        public ShopSection(
            ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, INode node, IRequirement requirement)
            : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, "Shop", node, requirement)
        {
        }
    }
}