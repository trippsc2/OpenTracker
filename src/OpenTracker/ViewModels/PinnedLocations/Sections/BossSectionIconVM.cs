using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This class contains boss section icon control ViewModel data.
    /// </summary>
    public class BossSectionIconVM : ViewModelBase, ISectionIconVM
    {
        private readonly IMode _mode;
        private readonly IBossPlacement _bossPlacement;

        public bool Visible => _mode.BossShuffle;
        public string ImageSource =>
            "avares://OpenTracker/Assets/Images/Bosses/" +
            (_bossPlacement.Boss.HasValue ? $"{_bossPlacement.Boss.ToString()!.ToLowerInvariant()}1" :
            $"{_bossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0") + ".png";

        public IBossSelectPopupVM BossSelect { get; }
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

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
            IMode mode, IBossSelectPopupVM.Factory bossSelectFactory, IBossPlacement bossPlacement)
        {
            _mode = mode;
            _bossPlacement = bossPlacement;
            BossSelect = bossSelectFactory(bossPlacement);
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            _mode.PropertyChanged += OnModeChanged;
            _bossPlacement.PropertyChanged += OnBossChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.BossShuffle))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IBossSection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnBossChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IBossPlacement.Boss))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
            }
        }

        /// <summary>
        /// Opens the boss select popup.
        /// </summary>
        private void OpenBossSelect()
        {
            BossSelect.PopupOpen = true;
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The PointerReleased event args.
        /// </param>
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                OpenBossSelect();
            }
        }
    }
}
