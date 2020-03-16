using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class BossControlVM : ViewModelBase, IItemControlVM
    {
        private readonly Game _game;
        private readonly BossSection _bossSection;

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        public BossControlVM(Game game, BossSection bossSection)
        {
            _game = game;
            _bossSection = bossSection;

            _bossSection.PropertyChanged += OnSectionChanged;

            Update();
        }

        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            string imageBaseString = "avares://OpenTracker/Assets/Images/";

            if (_bossSection.Boss == null)
                imageBaseString += "Items/unknown1";
            else
            {
                imageBaseString += "Bosses/";
                imageBaseString += _bossSection.Boss.Type.ToString().ToLower();
            }

            ImageSource = imageBaseString + ".png";
        }

        public void ChangeItem(bool alternate = false)
        {
            if (_bossSection.Boss == null)
                _bossSection.Boss = _game.Bosses[BossType.Armos];
            else if (_bossSection.Boss.Type == BossType.Trinexx)
                _bossSection.Boss = null;
            else
            {
                BossType newBoss = _bossSection.Boss.Type + 1;
                _bossSection.Boss = _game.Bosses[newBoss];
            }
        }
    }
}
