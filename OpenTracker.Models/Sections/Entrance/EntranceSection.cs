using System.ComponentModel;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.Sections.Entrance;

/// <summary>
/// This class contains entrance shuffle data.
/// </summary>
public class EntranceSection : EntranceSectionBase
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
    /// <param name="markingFactory">
    ///     An Autofac factory for creating new <see cref="IMarking"/> objects.
    /// </param>
    /// <param name="name">
    ///     A <see cref="string"/> representing the section name.
    /// </param>
    /// <param name="entranceShuffleLevel">
    ///     The minimum <see cref="EntranceShuffle"/> level.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for the section to be active.
    /// </param>
    /// <param name="node">
    ///     The <see cref="INode"/> to which this section belongs.
    /// </param>
    /// <param name="exitProvided">
    ///     The nullable <see cref="IOverworldNode"/> exit that this section provides.
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
    /// Subscribes to the <see cref="IOverworldNode.PropertyChanged"/> event.
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