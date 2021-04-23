using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Item
{
    /// <summary>
    ///     This class contains item sections with marking data.
    /// </summary>
    public class ItemSection : ItemSectionBase
    {
        private readonly INode? _visibleNode;
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
        /// <param name="name">
        ///     A string representing the name of the section.
        /// </param>
        /// <param name="node">
        ///     The requirement node to which this section belongs.
        /// </param>
        /// <param name="total">
        ///     A 32-bit signed integer representing the total number of items.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-tracking value for this section.
        /// </param>
        /// <param name="marking">
        ///     The section marking.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be visible.
        /// </param>
        /// <param name="visibleNode">
        ///     The node that provides Inspect accessibility for this section.
        /// </param>
        public ItemSection(
            ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
            IUncollectSection.Factory uncollectSectionFactory, string name, INode node, int total,
            IAutoTrackValue? autoTrackValue = null, IMarking? marking = null, IRequirement? requirement = null,
            INode? visibleNode = null)
            : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, name, autoTrackValue, marking, requirement)
        {
            _visibleNode = visibleNode;
            _node = node;

            Total = total;
            Available = Total;

            _node.PropertyChanged += OnNodeChanged;

            if (_visibleNode is not null)
            {
                _visibleNode.PropertyChanged += OnNodeChanged;
            }
            
            UpdateAccessibility();
            UpdateAccessible();
        }

        public override void Clear(bool force)
        {
            if (CanBeCleared(force))
            {
                Available = 0;
            }
        }

        protected override void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(Accessibility):
                    UpdateAccessible();
                    break;
                case nameof(Available):
                    UpdateAccessibility();
                    UpdateAccessible();
                    break;
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the INode interface.
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
        ///     Updates values of the Accessibility property.
        /// </summary>
        private void UpdateAccessibility()
        {
            if (_node.Accessibility > AccessibilityLevel.Inspect || _visibleNode is null)
            {
                Accessibility = _node.Accessibility;
                return;
            }

            Accessibility = AccessibilityLevelMethods.Max(_node.Accessibility,
                _visibleNode.Accessibility > AccessibilityLevel.Inspect
                    ? AccessibilityLevel.Inspect
                    : AccessibilityLevel.None);
        }

        /// <summary>
        ///     Updates values of the Accessibility property.
        /// </summary>
        private void UpdateAccessible()
        {
            Accessible = Accessibility > AccessibilityLevel.Inspect ? Available : 0;
        }
    }
}