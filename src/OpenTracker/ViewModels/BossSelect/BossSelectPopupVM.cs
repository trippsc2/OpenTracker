using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.BossSelect;

/// <summary>
/// This class contains the boss select popup control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class BossSelectPopupVM : ViewModel
{
    private LayoutSettings LayoutSettings { get; }
    private IBossPlacement BossPlacement { get; }

    public List<BossSelectButtonVM> Buttons { get; }

    [Reactive]
    public bool PopupOpen { get; set; }
    [ObservableAsProperty]
    public double Scale { get; }
    
    public delegate BossSelectPopupVM Factory(IBossPlacement bossPlacement);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    /// The layout settings data.
    /// </param>
    /// <param name="factory">
    /// A factory for creating boss select controls.
    /// </param>
    /// <param name="bossPlacement">
    /// The boss placement to be manipulated.
    /// </param>
    public BossSelectPopupVM(LayoutSettings layoutSettings, IBossSelectFactory factory, IBossPlacement bossPlacement)
    {
        LayoutSettings = layoutSettings;
        BossPlacement = bossPlacement;
        
        Buttons = factory.GetBossSelectButtonVMs(bossPlacement);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.LayoutSettings.UIScale)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Scale)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.BossPlacement.Boss)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(_ => PopupOpen = false)
                .DisposeWith(disposables);
        });
    }
}