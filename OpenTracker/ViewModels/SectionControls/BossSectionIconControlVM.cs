using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Sections;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.SectionControls
{
    /// <summary>
    /// This is the ViewModel of the section icon control representing a dungeon boss.
    /// </summary>
    public class BossSectionIconControlVM : SectionIconControlVMBase
    {
        private readonly IBossSection _bossSection;

        public bool Visible =>
            Mode.Instance.BossShuffle;
        public string ImageSource =>
            _bossSection.BossPlacement.Boss.HasValue ?
            "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{_bossSection.BossPlacement.Boss.ToString().ToLowerInvariant()}.png" :
            "avares://OpenTracker/Assets/Images/Items/unknown1.png";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossSection">
        /// The boss section to be represented.
        /// </param>
        public BossSectionIconControlVM(IBossSection bossSection)
        {
            _bossSection = bossSection ?? throw new ArgumentNullException(nameof(bossSection));

            Mode.Instance.PropertyChanged += OnModeChanged;
            _bossSection.BossPlacement.PropertyChanged += OnBossChanged;
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
    }
}
