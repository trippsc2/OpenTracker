using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items.Adapters
{
    /// <summary>
    /// This class contains the logic to adapt dungeon boss data to an item control. 
    /// </summary>
    public class BossAdapter : ViewModelBase, IItemAdapter
    {
        private readonly IBossPlacement _bossPlacement;
        
        public string ImageSource =>
            "avares://OpenTracker/Assets/Images/Bosses/" +
            (_bossPlacement.Boss.HasValue ? $"{_bossPlacement.Boss.ToString()!.ToLowerInvariant()}1" : 
                $"{_bossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0") + ".png";

        public string? Label { get; } = null;
        public string LabelColor { get; } = "#ffffffff";
        
        public IBossSelectPopupVM? BossSelect { get; }
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

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
        public BossAdapter(IBossSelectPopupVM.Factory bossSelectFactory, IBossPlacement bossPlacement)
        {
            _bossPlacement = bossPlacement;
            
            BossSelect = bossSelectFactory(_bossPlacement);
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            _bossPlacement.PropertyChanged += OnBossChanged;
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
            if (BossSelect is null)
            {
                return;
            }
            
            BossSelect.PopupOpen = true;
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The pointer released event args.
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