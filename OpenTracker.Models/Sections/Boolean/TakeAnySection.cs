using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Boolean
{
    /// <summary>
    ///     This class contains take any section data.
    /// </summary>
    public class TakeAnySection : BooleanSectionBase, ITakeAnySection
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
        public TakeAnySection(ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, INode node, IRequirement requirement)
            : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, "Take Any", node, requirement)
        {
        }
    }
}