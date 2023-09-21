using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Items.Adapters;

/// <summary>
/// This class contains the logic to adapt dungeon boss data to an item control. 
/// </summary>
[DependencyInjection]
public sealed class BossAdapter : ViewModel, IItemAdapter
{
    private IBossPlacement BossPlacement { get; }

    public string? Label => null;
    public SolidColorBrush? LabelColor => null;
    public BossSelectPopupVM? BossSelect { get; }

    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;
    
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    public delegate BossAdapter Factory(IBossPlacement bossPlacement);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bossSelectFactory">
    /// An Autofac factory for creating the boss select popup control ViewModel.
    /// </param>
    /// <param name="bossPlacement">
    /// The boss placement to be represented.
    /// </param>
    public BossAdapter(BossSelectPopupVM.Factory bossSelectFactory, IBossPlacement bossPlacement)
    {
        BossPlacement = bossPlacement;
        BossSelect = bossSelectFactory(BossPlacement);

        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.BossPlacement.Boss)
                .Select(_ =>
                    BossPlacement.Boss is null
                        ? $"avares://OpenTracker/Assets/Images/Bosses/{BossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0.png"
                        : $"avares://OpenTracker/Assets/Images/Bosses/{BossPlacement.Boss.ToString()!.ToLowerInvariant()}1.png")
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
        });
    }

    private void HandleClick(PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton != MouseButton.Left || BossSelect is null)
        {
            return;
        }

        BossSelect.PopupOpen = true;
    }
}