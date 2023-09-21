using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.KeyDoor;
using OpenTracker.Utils.Autofac;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.KeyDoors;

/// <summary>
/// This class contains key door data.
/// </summary>
[DependencyInjection]
public sealed class KeyDoor : ReactiveObject, IKeyDoor
{
    private readonly INode _node;

    public IRequirement Requirement { get; }

    private bool _unlocked;
    public bool Unlocked
    {
        get => _unlocked;
        set => this.RaiseAndSetIfChanged(ref _unlocked, value);
    }

    public AccessibilityLevel Accessibility => _node.Accessibility; 

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="requirementFactory">
    ///     An Autofac factory for creating new <see cref="KeyDoorRequirement"/> objects.
    /// </param>
    /// <param name="node">
    ///     The <see cref="INode"/> to which the key door belongs.
    /// </param>
    public KeyDoor(KeyDoorRequirement.Factory requirementFactory, INode node)
    {
        _node = node;
            
        Requirement = requirementFactory(this);
            
        UpdateAccessibility();

        _node.PropertyChanged += OnNodeChanged;
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
        if (e.PropertyName == nameof(IOverworldNode.Accessibility))
        {
            UpdateAccessibility();
        }
    }

    /// <summary>
    /// Updates the accessibility of the key door.
    /// </summary>
    private void UpdateAccessibility()
    {
        this.RaisePropertyChanged(nameof(Accessibility));
    }
}