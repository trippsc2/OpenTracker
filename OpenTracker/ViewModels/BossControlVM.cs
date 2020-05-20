using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
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

        public string ImageSource
        {
            get
            {
                string imageBaseString = "avares://OpenTracker/Assets/Images/";

                if (_bossSection.Boss == null)
                    imageBaseString += "Items/unknown1";
                else
                {
                    imageBaseString += "Bosses/" + _bossSection.Boss.Type.ToString()
                        .ToLowerInvariant();
                }

                return imageBaseString + ".png";
            }
        }

        public BossControlVM(UndoRedoManager undoRedoManager, Game game,
            BossSection bossSection)
        {
            _undoRedoManager = undoRedoManager;
            _game = game;
            _bossSection = bossSection ?? throw new ArgumentNullException(nameof(bossSection));

            _bossSection.PropertyChanged += OnSectionChanged;
        }

        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BossSection.Boss))
                this.RaisePropertyChanged(nameof(ImageSource));
        }

        private void ChangeBoss(bool backward = false)
        {
            if (backward)
            {
                if (_bossSection.Boss == null)
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection,
                        _game.Bosses[BossType.Trinexx]));
                }
                else if (_bossSection.Boss.Type == BossType.Armos)
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, null));
                else
                {
                    Boss newBoss = _game.Bosses[_bossSection.Boss.Type - 1];
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, newBoss));
                }
            }
            else
            {
                if (_bossSection.Boss == null)
                {
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection,
                        _game.Bosses[BossType.Armos]));
                }
                else if (_bossSection.Boss.Type == BossType.Trinexx)
                    _undoRedoManager.Execute(new ChangeBoss(_bossSection, null));
                else
                {
                    Boss newBoss = _game.Bosses[_bossSection.Boss.Type + 1];
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
