using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Undoables;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Sections;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Text;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the view-model for the boss control in the items panel.
    /// </summary>
    public class BossControlVM : ViewModelBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly Game _game;
        private readonly BossSection _bossSection;

        public bool BossShuffle =>
            _game.Mode.BossShuffle;

        public string ImageSource
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("avares://OpenTracker/Assets/Images/");

                if (_bossSection.BossPlacement.Boss == null)
                {
                    sb.Append("Items/unknown1");
                }
                else
                {
                    sb.Append($"Bosses/" +
                        $"{ _bossSection.BossPlacement.Boss.Type.ToString().ToLowerInvariant() }");
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
        public BossControlVM(UndoRedoManager undoRedoManager, Game game,
            BossSection bossSection)
        {
            _undoRedoManager = undoRedoManager;
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _bossSection = bossSection ?? throw new ArgumentNullException(nameof(bossSection));

            _game.Mode.PropertyChanged += OnModeChanged;
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
                this.RaisePropertyChanged(nameof(BossShuffle));
            }
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
            if (e.PropertyName == nameof(BossPlacement.Boss))
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
                if (_bossSection.BossPlacement.Boss == null)
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection,
                        _game.Bosses[BossType.Trinexx]));
                }
                else if (_bossSection.BossPlacement.Boss.Type == BossType.Armos)
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, null));
                }
                else
                {
                    Boss newBoss = _game.Bosses[_bossSection.BossPlacement.Boss.Type - 1];
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, newBoss));
                }
            }
            else
            {
                if (_bossSection.BossPlacement.Boss == null)
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection,
                        _game.Bosses[BossType.Armos]));
                }
                else if (_bossSection.BossPlacement.Boss.Type == BossType.Trinexx)
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, null));
                }
                else
                {
                    Boss newBoss = _game.Bosses[_bossSection.BossPlacement.Boss.Type + 1];
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, newBoss));
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
