using OpenTracker.Actions;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class DungeonPrizeControlVM : ViewModelBase, IItemControlVM
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly Game _game;
        private readonly BossSection _prizeSection;

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        public DungeonPrizeControlVM(UndoRedoManager undoRedoManager,
            Game game, BossSection prizeSection)
        {
            _undoRedoManager = undoRedoManager;
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

            ImageSource = imageBaseString + (_prizeSection.IsAvailable() ? "0" : "1") + ".png";
        }

        public void ChangeItem(bool rightClick = false)
        {
            if (rightClick)
            {
                if (_prizeSection.Prize == null)
                {
                    _undoRedoManager.Execute(new ChangePrize(_prizeSection,
                        _game.Items[ItemType.GreenPendant]));
                }
                else if (_prizeSection.Prize.Type == ItemType.RedCrystal)
                    _undoRedoManager.Execute(new ChangePrize(_prizeSection, null));
                else
                {
                    Item newPrize = _game.Items[_prizeSection.Prize.Type + 1];
                    _undoRedoManager.Execute(new ChangePrize(_prizeSection, newPrize));
                }
            }
            else
                _undoRedoManager.Execute(new TogglePrize(_prizeSection));
        }
    }
}
