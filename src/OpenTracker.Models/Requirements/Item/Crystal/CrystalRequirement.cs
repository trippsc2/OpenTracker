using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Items;
using OpenTracker.Models.Prizes;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.Item.Crystal;

/// <summary>
/// This class contains GT crystal <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class CrystalRequirement : ReactiveObject, ICrystalRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private ICrystalRequirementItem GTCrystal { get; }
    private IItem Crystal { get; }
    private IItem RedCrystal { get; }
    
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    [ObservableAsProperty]
    public bool Met { get; }
    
    public event EventHandler? ChangePropagated;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="items">
    ///     The <see cref="IItemDictionary"/>.
    /// </param>
    /// <param name="prizes">
    ///     The <see cref="IPrizeDictionary"/>.
    /// </param>
    public CrystalRequirement(IItemDictionary items, IPrizeDictionary prizes)
    {
        GTCrystal = (ICrystalRequirementItem)items[ItemType.TowerCrystals];
        Crystal = prizes[PrizeType.Crystal];
        RedCrystal = prizes[PrizeType.RedCrystal];

        this.WhenAnyValue(
                x => x.GTCrystal.Known,
                x => x.GTCrystal.Current,
                x => x.Crystal.Current,
                x => x.RedCrystal.Current,
                GetAccessibility)
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

    private static AccessibilityLevel GetAccessibility(
        bool requirementKnown,
        int gtRequirement,
        int crystal,
        int redCrystal)
    {
        if (crystal + redCrystal >= 7)
        {
            return AccessibilityLevel.Normal;
        }

        if (requirementKnown)
        {
            return gtRequirement + crystal + redCrystal >= 7
                ? AccessibilityLevel.Normal
                : AccessibilityLevel.None;
        }

        return AccessibilityLevel.SequenceBreak;
    }
}