using System;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Sections.Item;

/// <summary>
/// This class contains dungeon item section data.
/// </summary>
[DependencyInjection]
public sealed class DungeonItemSection : ItemSectionBase, IDungeonItemSection
{
    private readonly IDungeon _dungeon;
    private readonly IDungeonAccessibilityProvider _accessibilityProvider;

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
    /// <param name="dungeon">
    ///     The <see cref="IDungeon"/>.
    /// </param>
    /// <param name="accessibilityProvider">
    ///     The <see cref="IDungeonAccessibilityProvider"/>.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    /// <param name="marking">
    ///     The nullable <see cref="IMarking"/>.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for this section to be visible.
    /// </param>
    public DungeonItemSection(
        ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
        IUncollectSection.Factory uncollectSectionFactory, IDungeon dungeon,
        IDungeonAccessibilityProvider accessibilityProvider, IAutoTrackValue? autoTrackValue = null,
        IMarking? marking = null, IRequirement? requirement = null)
        : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, "Dungeon", autoTrackValue,
            marking, requirement)
    {
        _dungeon = dungeon;
        _accessibilityProvider = accessibilityProvider;

        _dungeon.PropertyChanged += OnDungeonChanged;
        _accessibilityProvider.PropertyChanged += OnAccessibilityProviderChanged;
            
        UpdateTotal();
        UpdateAccessibility();
    }

    public override void Clear(bool force)
    {
        if (force)
        {
            Available = 0;
            return;
        }

        Available -= Accessible;
    }

    protected override void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(sender, e);

        if (e.PropertyName == nameof(Available))
        {
            UpdateAccessibility();
        }
    }

    /// <summary>
    /// Subscribes to the <see cref="IDungeon.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnDungeonChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IDungeon.Total))
        {
            UpdateTotal();
        }
    }
        
    /// <summary>
    /// Subscribes to the <see cref="IDungeonAccessibilityProvider.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnAccessibilityProviderChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(IDungeonAccessibilityProvider.Accessible):
            case nameof(IDungeonAccessibilityProvider.SequenceBreak):
            case nameof(IDungeonAccessibilityProvider.Visible):
                UpdateAccessibility();
                break;
        }
    }

    /// <summary>
    /// Updates value of the <see cref="ISection.Total"/> property.
    /// </summary>
    private void UpdateTotal()
    {
        var newTotal = _dungeon.Total;
        var delta = newTotal - Total;

        Total = newTotal;
        Available = Math.Max(0, Math.Min(Total, Available + delta));
    }

    /// <summary>
    /// Updates values of the <see cref="IItemSection.Accessible"/> and <see cref="ISection.Available"/> properties.
    /// </summary>
    private void UpdateAccessibility()
    {
        var unavailable = Total - Available;
        var accessible = _accessibilityProvider.Accessible - unavailable;
            
        Accessible = Math.Max(0, accessible);

        if (accessible >= Available)
        {
            Accessibility = _accessibilityProvider.SequenceBreak
                ? AccessibilityLevel.SequenceBreak
                : AccessibilityLevel.Normal;
            return;
        }

        if (accessible > 0)
        {
            Accessibility = AccessibilityLevel.Partial;
            return;
        }
            
        if (unavailable == _accessibilityProvider.Accessible && _accessibilityProvider.Visible)
        {
            Accessibility = AccessibilityLevel.Inspect;
            return;
        }

        Accessibility = AccessibilityLevel.None;
    }
}