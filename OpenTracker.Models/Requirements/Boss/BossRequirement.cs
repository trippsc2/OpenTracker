using System;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Boss;

/// <summary>
/// This class contains <see cref="IBossPlacement"/> requirement data.
/// </summary>
[DependencyInjection]
public sealed class BossRequirement : AccessibilityRequirement, IBossRequirement
{
    private readonly IBossTypeRequirementDictionary _bossTypeRequirements;
    private readonly IBossPlacement _bossPlacement;

    private IRequirement? _currentBossRequirement;
    private IRequirement CurrentBossRequirement
    {
        get => _currentBossRequirement ?? throw new NullReferenceException();
        set
        {
            if (_currentBossRequirement == value)
            {
                return;
            }
                
            if (_currentBossRequirement is not null)
            {
                _currentBossRequirement.PropertyChanged -= OnRequirementChanged;
            }

            _currentBossRequirement = value;

            if (_currentBossRequirement is not null)
            {
                _currentBossRequirement.PropertyChanged += OnRequirementChanged;
            }

            UpdateValue();
        }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="bossTypeRequirements">
    ///     The <see cref="IBossTypeRequirementDictionary"/>.
    /// </param>
    /// <param name="bossPlacement">
    ///     The <see cref="IBossPlacement"/>.
    /// </param>
    public BossRequirement(
        IMode mode, IBossTypeRequirementDictionary bossTypeRequirements, IBossPlacement bossPlacement)
    {
        _bossTypeRequirements = bossTypeRequirements;

        _bossPlacement = bossPlacement;

        mode.PropertyChanged += OnModeChanged;
        _bossPlacement.PropertyChanged += OnBossPlacementChanged;
            
        UpdateRequirement();
    }

    /// <summary>
    /// Subscribes to the <see cref="IMode.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IMode.BossShuffle))
        {
            UpdateRequirement();
        }
    }

    /// <summary>
    /// Subscribes to the <see cref="IBossPlacement.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnBossPlacementChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IBossPlacement.Boss))
        {
            UpdateRequirement();
        }
    }

    /// <summary>
    /// Subscribes to the <see cref="IRequirement.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IRequirement.Accessibility))
        {
            UpdateValue();
        }
    }

    /// <summary>
    /// Updates the <see cref="CurrentBossRequirement"/> property value.
    /// </summary>
    private void UpdateRequirement()
    {
        var boss = _bossPlacement.GetCurrentBoss();

        CurrentBossRequirement = boss is null ? _bossTypeRequirements.NoBoss.Value
            : _bossTypeRequirements[boss.Value];
    }

    protected override AccessibilityLevel GetAccessibility()
    {
        return CurrentBossRequirement.Accessibility;
    }
}