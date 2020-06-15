using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Sections;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
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
                string imageBaseString = "avares://OpenTracker/Assets/Images/";

                if (_bossSection.BossPlacement.Boss == null)
                    imageBaseString += "Items/unknown1";
                else
                {
                    imageBaseString += "Bosses/" + _bossSection.BossPlacement.Boss.Type
                        .ToString().ToLowerInvariant();
                }

                return imageBaseString + ".png";
            }
        }

        public BossControlVM(UndoRedoManager undoRedoManager, Game game,
            BossSection bossSection)
        {
            _undoRedoManager = undoRedoManager;
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _bossSection = bossSection ?? throw new ArgumentNullException(nameof(bossSection));

            _game.Mode.PropertyChanged += OnModeChanged;
            _bossSection.BossPlacement.PropertyChanged += OnBossChanged;
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.BossShuffle))
                this.RaisePropertyChanged(nameof(BossShuffle));
        }

        private void OnBossChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BossPlacement.Boss))
                this.RaisePropertyChanged(nameof(ImageSource));
        }

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
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, null));
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
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, null));
                else
                {
                    Boss newBoss = _game.Bosses[_bossSection.BossPlacement.Boss.Type + 1];
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, newBoss));
                }
            }
        }

        public void OnLeftClick(bool force = false)
        {
            ChangeBoss();
        }

        public void OnRightClick(bool force = false)
        {
            ChangeBoss(true);
        }
    }
}
