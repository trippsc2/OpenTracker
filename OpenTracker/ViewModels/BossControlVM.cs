using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Undoables;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Sections;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Text;
using OpenTracker.ViewModels.Bases;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.BossPlacements;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the view-model for the boss control in the items panel.
    /// </summary>
    public class BossControlVM : SmallItemControlVMBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly BossSection _bossSection;
        private readonly IRequirement _requirement;

        public bool Visible =>
            _requirement.Met;

        public string ImageSource
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("avares://OpenTracker/Assets/Images/");

                if (!_bossSection.BossPlacement.Boss.HasValue)
                {
                    sb.Append("Items/unknown1");
                }
                else
                {
                    sb.Append($"Bosses/" +
                        $"{_bossSection.BossPlacement.Boss.ToString().ToLowerInvariant()}");
                }

                sb.Append(".png");

                return sb.ToString();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="bossSection">
        /// The boss section to which the boss belongs.
        /// </param>
        public BossControlVM(
            UndoRedoManager undoRedoManager, BossSection bossSection)
        {
            _undoRedoManager = undoRedoManager ?? throw new ArgumentNullException(nameof(undoRedoManager));
            _bossSection = bossSection ?? throw new ArgumentNullException(nameof(bossSection));
            _requirement = RequirementDictionary.Instance[RequirementType.BossShuffleOn];

            _bossSection.BossPlacement.PropertyChanged += OnBossChanged;
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
        /// Subscribes to the PropertyChanged event on the BossSection class.
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
                if (!_bossSection.BossPlacement.Boss.HasValue)
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, BossType.Trinexx));
                }
                else if (_bossSection.BossPlacement.Boss == BossType.Armos)
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, null));
                }
                else
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, _bossSection.BossPlacement.Boss - 1));
                }
            }
            else
            {
                if (!_bossSection.BossPlacement.Boss.HasValue)
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, BossType.Armos));
                }
                else if (_bossSection.BossPlacement.Boss == BossType.Trinexx)
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, null));
                }
                else
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, _bossSection.BossPlacement.Boss + 1));
                }
            }
        }

        /// <summary>
        /// Click handler for left click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            ChangeBoss();
        }

        /// <summary>
        /// Click handler for right click.
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
