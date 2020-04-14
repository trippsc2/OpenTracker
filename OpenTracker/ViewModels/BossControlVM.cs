using OpenTracker.Actions;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class BossControlVM : ViewModelBase, IItemControlVM
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly Game _game;
        private readonly BossSection _bossSection;

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        public BossControlVM(UndoRedoManager undoRedoManager, Game game,
            BossSection bossSection)
        {
            _undoRedoManager = undoRedoManager;
            _game = game;
            _bossSection = bossSection;

            _bossSection.PropertyChanged += OnSectionChanged;

            UpdateImage();
        }

        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BossSection.Boss))
                UpdateImage();
        }

        private void UpdateImage()
        {
            string imageBaseString = "avares://OpenTracker/Assets/Images/";

            if (_bossSection.Boss == null)
                imageBaseString += "Items/unknown1";
            else
                imageBaseString += "Bosses/" + _bossSection.Boss.Type.ToString().ToLower();

            ImageSource = imageBaseString + ".png";
        }

        public void ChangeItem(bool rightClick = false)
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
}
