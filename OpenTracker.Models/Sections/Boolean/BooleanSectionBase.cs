using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Boolean;

/// <summary>
/// This base class contains the boolean section data for shop and take any sections.
/// </summary>
public abstract class BooleanSectionBase : SectionBase
{
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
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for this section to be active.
    /// </param>
    protected BooleanSectionBase(
        ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
        IUncollectSection.Factory uncollectSectionFactory, string name, INode node, IRequirement requirement)
        : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, name, null,
            null, requirement)
    {
        _node = node;

        Total = 1;
        Available = 1;

        _node.PropertyChanged += OnNodeChanged;
            
        UpdateAccessibility();
    }

    public override bool CanBeCleared(bool force = false)
    {
        return IsAvailable() && (force || Accessibility > AccessibilityLevel.Inspect);
    }

    public override void Clear(bool force)
    {
        Available = 0;
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
    /// Updates the value of the <see cref="Accessibility"/> property.
    /// </summary>
    private void UpdateAccessibility()
    {
        Accessibility = _node.Accessibility;
    }
}