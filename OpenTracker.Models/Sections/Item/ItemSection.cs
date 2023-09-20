using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Sections.Item;

/// <summary>
/// This class contains item sections with marking data.
/// </summary>
[DependencyInjection]
public sealed class ItemSection : ItemSectionBase
{
    private readonly INode? _visibleNode;
    private readonly INode _node;

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
    /// <param name="name">
    ///     A <see cref="string"/> representing the section name.
    /// </param>
    /// <param name="node">
    ///     The <see cref="INode"/> to which this section belongs.
    /// </param>
    /// <param name="total">
    ///     A <see cref="int"/> representing the total number of items.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    /// <param name="marking">
    ///     The nullable <see cref="IMarking"/>.
    /// </param>
    /// <param name="requirement">
    ///     The nullable <see cref="IRequirement"/> for the section to be visible.
    /// </param>
    /// <param name="visibleNode">
    ///     The nullable <see cref="INode"/> that provides Inspect <see cref="AccessibilityLevel"/> for the section.
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
    /// Subscribes to the <see cref="INode.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(INode.Accessibility))
        {
            UpdateAccessibility();
        }
    }

    /// <summary>
    /// Updates values of the <see cref="Accessibility"/> property.
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
    /// Updates values of the <see cref="IItemSection.Accessible"/> property.
    /// </summary>
    private void UpdateAccessible()
    {
        Accessible = Accessibility > AccessibilityLevel.Inspect ? Available : 0;
    }
}