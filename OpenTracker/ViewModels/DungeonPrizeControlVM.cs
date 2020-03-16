using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class DungeonPrizeControlVM : ViewModelBase, IItemControlVM
    {
        private readonly Game _game;
        private readonly BossSection _prizeSection;

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        public DungeonPrizeControlVM(Game game, BossSection prizeSection)
        {
            _game = game;
            _prizeSection = prizeSection;

            _prizeSection.PropertyChanged += OnSectionChanged;

            Update();
        }

        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            string imageBaseString = "avares://OpenTracker/Assets/Images/Items/";

            if (_prizeSection.Prize == null)
                imageBaseString += "unknown";
            else
                imageBaseString += _prizeSection.Prize.Type.ToString().ToLower();

            ImageSource = imageBaseString + (_prizeSection.Available ? "0" : "1") + ".png";
        }

        public void ChangeItem(bool alternate = false)
        {
            if (alternate)
            {
                if (_prizeSection.Prize == null)
                    _prizeSection.Prize = _game.Items[ItemType.GreenPendant];
                else if (_prizeSection.Prize.Type == ItemType.RedCrystal)
                    _prizeSection.Prize = null;
                else
                {
                    ItemType newPrize = _prizeSection.Prize.Type + 1;
                    _prizeSection.Prize = _game.Items[newPrize];
                }
            }
            else
                _prizeSection.Available = !_prizeSection.Available;
        }
    }
}
