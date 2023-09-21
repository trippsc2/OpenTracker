using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Utils.Autofac;
using OpenTracker.Utils.Themes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.ThemeSelected;

/// <summary>
///     This class contains theme selected requirement data.
/// </summary>
[DependencyInjection]
public sealed class ThemeSelectedRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private IThemeManager ThemeManager { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    
    public event EventHandler? ChangePropagated;
        
    /// <summary>
    /// A factory method for creating new theme selected requirements.
    /// </summary>
    public delegate ThemeSelectedRequirement Factory(Theme expectedValue);

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="themeManager">
    ///     The theme manager.
    /// </param>
    /// <param name="expectedValue">
    ///     The expected theme value.
    /// </param>
    public ThemeSelectedRequirement(IThemeManager themeManager, Theme expectedValue)
    {
        ThemeManager = themeManager;

        this.WhenAnyValue(x => x.ThemeManager.SelectedTheme)
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