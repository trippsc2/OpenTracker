using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This class contains the boss small items panel control ViewModel data.
    /// </summary>
    public class BossSmallItemVM : ViewModelBase, ISmallItemVMBase
    {
        private readonly IBossPlacement _bossPlacement;
        private readonly IRequirement _requirement;

        public bool Visible =>
            _requirement.Met;
        public string ImageSource =>
            _bossPlacement.Boss.HasValue ? "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{_bossPlacement.Boss.ToString()!.ToLowerInvariant()}1.png" :
            "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{_bossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0.png";

        public IBossSelectPopupVM BossSelect { get; }
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        public delegate BossSmallItemVM Factory(IBossPlacement bossPlacement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        /// The requirements dictionary.
        /// </param>
        /// <param name="bossSelectFactory">
        /// An Autofac factory for creating the boss select popup control ViewModel.
        /// </param>
        /// <param name="bossPlacement">
        /// The boss section to which the boss belongs.
        /// </param>
        public BossSmallItemVM(
            IRequirementDictionary requirements, IBossSelectPopupVM.Factory bossSelectFactory,
            IBossPlacement bossPlacement)
        {
            _bossPlacement = bossPlacement;
            _requirement = requirements[RequirementType.BossShuffleOn];
            
            BossSelect = bossSelectFactory(_bossPlacement);
            
            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

            _bossPlacement.PropertyChanged += OnBossChanged;
            _requirement.PropertyChanged += OnRequirementChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnBossChanged(object sender, PropertyChangedEventArgs e)
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
        /// The pointer released event args.
        /// </param>
        private void HandleClick(PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                OpenBossSelect();
            }
        }
    }
}
