using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Sections;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Sections.Boss;

/// <summary>
/// This class contains boss section data.  It will be used directly for GT LW boss re-fights and as a base class
/// for final bosses that provide a prize.
/// </summary>
[DependencyInjection]
public class BossSection : SectionBase, IBossSection
{
    private readonly BossAccessibilityProvider _accessibilityProvider;

    public IBossPlacement BossPlacement { get; }

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
    /// <param name="accessibilityProvider">
    ///     The <see cref="BossAccessibilityProvider"/>.
    /// </param>
    /// <param name="name">
    ///     A <see cref="string"/> representing the section name.
    /// </param>
    /// <param name="bossPlacement">
    ///     The <see cref="IBossPlacement"/>.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for the section to be active.
    /// </param>
    public BossSection(
        ISaveLoadManager saveLoadManager, ICollectSection.Factory collectSectionFactory,
        IUncollectSection.Factory uncollectSectionFactory, BossAccessibilityProvider accessibilityProvider,
        string name, IBossPlacement bossPlacement, IAutoTrackValue? autoTrackValue = null,
        IRequirement? requirement = null)
        : base(saveLoadManager, collectSectionFactory, uncollectSectionFactory, name, autoTrackValue, null,
            requirement)
    {
        _accessibilityProvider = accessibilityProvider;

        BossPlacement = bossPlacement;

        _accessibilityProvider.PropertyChanged += OnAccessibilityProviderChanged;
            
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
    /// Subscribes to the <see cref="BossAccessibilityProvider.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnAccessibilityProviderChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(BossAccessibilityProvider.Accessibility))
        {
            UpdateAccessibility();
        }
    }

    /// <summary>
    /// Updates the value of the <see cref="Accessibility"/> property.
    /// </summary>
    private void UpdateAccessibility()
    {
        Accessibility = _accessibilityProvider.Accessibility;
    }
}