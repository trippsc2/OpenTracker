using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.PinnedLocations.Sections;

/// <summary>
/// This class contains boss section icon control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class BossSectionIconVM : ViewModel, ISectionIconVM
{
    private IMode Mode { get; }
    private IBossPlacement BossPlacement { get; }

    public BossSelectPopupVM BossSelect { get; }
    
    [ObservableAsProperty]
    public bool Visible { get; }
    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;
    
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    public delegate BossSectionIconVM Factory(IBossPlacement bossPlacement);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    /// The mode settings data.
    /// </param>
    /// <param name="bossSelectFactory">
    /// An Autofac factory for creating boss select controls.
    /// </param>
    /// <param name="bossPlacement">
    /// The boss section to be represented.
    /// </param>
    public BossSectionIconVM(
        IMode mode, BossSelectPopupVM.Factory bossSelectFactory, IBossPlacement bossPlacement)
    {
        Mode = mode;
        BossPlacement = bossPlacement;
        BossSelect = bossSelectFactory(bossPlacement);
            
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Mode.BossShuffle)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Visible)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.BossPlacement.Boss)
                .Select(x => x is null
                    ? $"avares://OpenTracker/Assets/Images/Bosses/{BossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0.png"
                    : $"avares://OpenTracker/Assets/Images/Bosses/{x.ToString()!.ToLowerInvariant()}1.png")
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
        });
    }

    private void HandleClick(PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton == MouseButton.Left)
        {
            BossSelect.PopupOpen = true;
        }
    }
}