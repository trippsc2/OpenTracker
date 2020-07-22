using OpenTracker.Interfaces;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.ComponentModel;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.BossPlacements;

namespace OpenTracker.ViewModels.UIPanels.ItemsPanel.SmallItems
{
    /// <summary>
    /// This is the ViewModel for the small Items panel control representing the boss.
    /// </summary>
    public class BossSmallItemVM : SmallItemVMBase, IClickHandler
    {
        private readonly IBossPlacement _bossPlacement;
        private readonly IRequirement _requirement;

        public bool Visible =>
            _requirement.Met;

        public string ImageSource =>
            _bossPlacement.Boss.HasValue ? "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{_bossPlacement.Boss.ToString().ToLowerInvariant()}1.png" :
            "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{_bossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0.png";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossPlacement">
        /// The boss section to which the boss belongs.
        /// </param>
        public BossSmallItemVM(IBossPlacement bossPlacement)
        {
            _bossPlacement = bossPlacement ?? throw new ArgumentNullException(nameof(bossPlacement));
            _requirement = RequirementDictionary.Instance[RequirementType.BossShuffleOn];

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
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(Visible));
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
        private void OnBossChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IBossPlacement.Boss))
            {
                this.RaisePropertyChanged(nameof(ImageSource));
            }
        }

        /// <summary>
        /// Change the boss associated with the boss section.
        /// </summary>
        /// <param name="backward">
        /// A boolean representing whether to cycle the boss backward.
        /// </param>
        private void ChangeBoss(bool backward = false)
        {
            if (backward)
            {
                if (!_bossPlacement.Boss.HasValue)
                {
                    UndoRedoManager.Instance.Execute(new ChangeBoss(_bossPlacement, BossType.Trinexx));
                }
                else if (_bossPlacement.Boss == BossType.Armos)
                {
                    UndoRedoManager.Instance.Execute(new ChangeBoss(_bossPlacement, null));
                }
                else
                {
                    UndoRedoManager.Instance.Execute(new ChangeBoss(_bossPlacement, _bossPlacement.Boss - 1));
                }
            }
            else
            {
                if (!_bossPlacement.Boss.HasValue)
                {
                    UndoRedoManager.Instance.Execute(new ChangeBoss(_bossPlacement, BossType.Armos));
                }
                else if (_bossPlacement.Boss == BossType.Trinexx)
                {
                    UndoRedoManager.Instance.Execute(new ChangeBoss(_bossPlacement, null));
                }
                else
                {
                    UndoRedoManager.Instance.Execute(new ChangeBoss(_bossPlacement, _bossPlacement.Boss + 1));
                }
            }
        }

        /// <summary>
        /// Handles left clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            ChangeBoss();
        }

        /// <summary>
        /// Handles right clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            ChangeBoss(true);
        }
    }
}
