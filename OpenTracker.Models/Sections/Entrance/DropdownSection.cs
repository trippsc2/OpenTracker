using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Entrance
{
    /// <summary>
    ///     This class contains dropdown section data.
    /// </summary>
    public class DropdownSection : EntranceSectionBase, IDropdownSection
    {
        private readonly IMode _mode;

        private readonly INode _exitNode;
        private readonly INode _holeNode;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        ///     The save/load manager.
        /// </param>
        /// <param name="mode">
        ///     The mode settings data.
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
        /// <param name="exitNode">
        ///     The node to which the exit belongs.
        /// </param>
        /// <param name="holeNode">
        ///     The node to which the hole belongs.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be active.
        /// </param>
        public DropdownSection(ISaveLoadManager saveLoadManager, IMode mode,
            ICollectSection.Factory collectSectionFactory, IUncollectSection.Factory uncollectSectionFactory,
            IMarking.Factory markingFactory, INode exitNode, INode holeNode, IRequirement requirement)
            : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, markingFactory, "Dropdown",
                EntranceShuffle.None, requirement)
        {
            _mode = mode;
            
            _exitNode = exitNode;
            _holeNode = holeNode;

            _mode.PropertyChanged += OnModeChanged;
            _exitNode.PropertyChanged += OnNodeChanged;
            _holeNode.PropertyChanged += OnNodeChanged;
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.EntranceShuffle))
            {
                UpdateAccessibility();
            }
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
            if (e.PropertyName == nameof(IOverworldNode.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        ///     Updates the value of the Accessibility property.
        /// </summary>
        private void UpdateAccessibility()
        {
            if (_mode.EntranceShuffle == EntranceShuffle.Insanity)
            {
                Accessibility = _holeNode.Accessibility;
                return;
            }

            Accessibility = AccessibilityLevelMethods.Max(
                _holeNode.Accessibility,
                _exitNode.Accessibility > AccessibilityLevel.Inspect
                    ? AccessibilityLevel.Inspect
                    : AccessibilityLevel.None);
        }
    }
}