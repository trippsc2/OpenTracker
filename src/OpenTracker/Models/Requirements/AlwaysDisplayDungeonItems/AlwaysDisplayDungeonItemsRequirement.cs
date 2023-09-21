using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Settings;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.AlwaysDisplayDungeonItems;

/// <summary>
/// This class contains the <see cref="LayoutSettings.AlwaysDisplayDungeonItems"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class AlwaysDisplayDungeonItemsRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private LayoutSettings LayoutSettings { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }

    public event EventHandler? ChangePropagated;
    
    /// <summary>
    /// A factory method for creating new <see cref="AlwaysDisplayDungeonItemsRequirement"/> objects.
    /// </summary>
    public delegate AlwaysDisplayDungeonItemsRequirement Factory(bool expectedValue);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    ///     The <see cref="LayoutSettings"/>.
    /// </param>
    /// <param name="expectedValue">
    ///     A <see cref="bool"/> representing the expected <see cref="LayoutSettings.AlwaysDisplayDungeonItems"/>
    ///     value.
    /// </param>
    public AlwaysDisplayDungeonItemsRequirement(LayoutSettings layoutSettings, bool expectedValue)
    {
        LayoutSettings = layoutSettings;

        this.WhenAnyValue(x => x.LayoutSettings.AlwaysDisplayDungeonItems)
            .Select(x => x == expectedValue)
            .ToPropertyEx(this, x => x.Met)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Met)
            .Select(x => x ? AccessibilityLevel.Normal : AccessibilityLevel.None)
            .ToPropertyEx(this, x => x.Accessibility)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Accessibility)
            .Subscribe(_ => ChangePropagated?.Invoke(this, EventArgs.Empty))
            .DisposeWith(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}