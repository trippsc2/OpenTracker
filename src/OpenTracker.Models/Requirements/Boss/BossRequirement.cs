using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.Boss;

/// <summary>
/// This class contains <see cref="IBossPlacement"/> requirement data.
/// </summary>
[DependencyInjection]
public sealed class BossRequirement : ReactiveObject, IBossRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private IBossPlacement BossPlacement { get; }
    
    [ObservableAsProperty]
    private IRequirement CurrentBossRequirement { get; } = default!;
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    [ObservableAsProperty]
    public bool Met { get; }
    
    public event EventHandler? ChangePropagated;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bossTypeRequirements">
    ///     The <see cref="IBossTypeRequirementDictionary"/>.
    /// </param>
    /// <param name="bossPlacement">
    ///     The <see cref="IBossPlacement"/>.
    /// </param>
    public BossRequirement(IBossTypeRequirementDictionary bossTypeRequirements, IBossPlacement bossPlacement)
    {
        BossPlacement = bossPlacement;

        this.WhenAnyValue(x => x.BossPlacement.CurrentBoss)
            .Select(x => x is not null ? bossTypeRequirements[x.Value] : bossTypeRequirements.NoBoss.Value)
            .ToPropertyEx(this, x => x.CurrentBossRequirement)
            .DisposeWith(_disposables);
        this.WhenAnyValue(
                x => x.CurrentBossRequirement,
                x => x.CurrentBossRequirement.Accessibility,
                (requirement, _) => requirement.Accessibility)
            .ToPropertyEx(this, x => x.Accessibility)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Accessibility)
            .Select(x => x > AccessibilityLevel.None)
            .ToPropertyEx(this, x => x.Met)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Met)
            .Subscribe(_ => ChangePropagated?.Invoke(this, EventArgs.Empty))
            .DisposeWith(_disposables);
    }
    
    public void Dispose()
    {
        _disposables.Dispose();
    }
}