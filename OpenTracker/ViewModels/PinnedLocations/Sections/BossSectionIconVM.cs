using OpenTracker.Interfaces;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This is the ViewModel of the section icon control representing a dungeon boss.
    /// </summary>
    public class BossSectionIconVM : ViewModelBase, ISectionIconVMBase, IClickHandler
    {
        private readonly IMode _mode;
        private readonly IBossPlacement _bossPlacement;

        public bool Visible =>
            _mode.BossShuffle;
        public string ImageSource =>
            _bossPlacement.Boss.HasValue ?
            "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{_bossPlacement.Boss.ToString()!.ToLowerInvariant()}1.png" :
            "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{_bossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0.png";

        public IBossSelectPopupVM BossSelect { get; }

        public delegate BossSectionIconVM Factory(IBossPlacement bossPlacement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossPlacement">
        /// The boss section to be represented.
        /// </param>
        public BossSectionIconVM(
            IMode mode, IBossSelectPopupVM.Factory bossSelectFactory, IBossPlacement bossPlacement)
        {
            _mode = mode;
            _bossPlacement = bossPlacement;
            BossSelect = bossSelectFactory(bossPlacement);

            _mode.PropertyChanged += OnModeChanged;
            _bossPlacement.PropertyChanged += OnBossChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.BossShuffle))
            {
                this.RaisePropertyChanged(nameof(Visible));
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
        private void OnBossChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IBossPlacement.Boss))
            {
                this.RaisePropertyChanged(nameof(ImageSource));
            }
        }

        /// <summary>
        /// Handles left clicks and opens the boss select popup.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            BossSelect.PopupOpen = true;
        }

        /// <summary>
        /// Handles right clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
        }
    }
}
