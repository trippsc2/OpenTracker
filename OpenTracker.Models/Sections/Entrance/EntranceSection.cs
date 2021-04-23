using System.ComponentModel;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Entrance
{
    /// <summary>
    ///     This class contains entrance shuffle data.
    /// </summary>
    public class EntranceSection : EntranceSectionBase
    {
        private readonly INode _node;

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
        /// <param name="markingFactory">
        ///     An Autofac factory for creating new markings.
        /// </param>
        /// <param name="name">
        ///     A string representing the name of the section.
        /// </param>
        /// <param name="entranceShuffleLevel">
        ///     The minimum entrance shuffle level.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be active.
        /// </param>
        /// <param name="node">
        ///     The node to which this section belongs.
        /// </param>
        /// <param name="exitProvided">
        ///     The overworld node exit that this section provides.
        /// </param>
        public EntranceSection(
            ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, IMarking.Factory markingFactory, string name,
            EntranceShuffle entranceShuffleLevel, IRequirement requirement, INode node,
            IOverworldNode? exitProvided = null)
            : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, markingFactory, name,
                entranceShuffleLevel, requirement, exitProvided)
        {
            _node = node;

            _node.PropertyChanged += OnNodeChanged;
            
            UpdateAccessibility();
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IOverworldNode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(INode.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        ///     Updates the value of the Accessibility property.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = _node.Accessibility;
        }
    }
}