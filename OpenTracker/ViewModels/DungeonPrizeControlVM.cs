﻿using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class DungeonPrizeControlVM : ViewModelBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly Game _game;
        private readonly BossSection _prizeSection;

        public string ImageSource
        {
            get
            {
                string imageBaseString = "avares://OpenTracker/Assets/Images/Items/";

                if (_prizeSection.Prize == null)
                    imageBaseString += "unknown";
                else
                    imageBaseString += _prizeSection.Prize.Type.ToString().ToLower();

                return imageBaseString + (_prizeSection.IsAvailable() ? "0" : "1") + ".png";
            }
        }

        public DungeonPrizeControlVM(UndoRedoManager undoRedoManager,
            Game game, BossSection prizeSection)
        {
            _undoRedoManager = undoRedoManager;
            _game = game;
            _prizeSection = prizeSection;

            _prizeSection.PropertyChanged += OnSectionChanged;
        }

        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BossSection.Prize) ||
                e.PropertyName == nameof(BossSection.Available))
                this.RaisePropertyChanged(nameof(ImageSource));
        }

        private void TogglePrize()
        {
            _undoRedoManager.Execute(new TogglePrize(_prizeSection));
        }

        private void ChangePrize()
        {
            if (_prizeSection.Prize == null)
            {
                _undoRedoManager.Execute(new ChangePrize(_prizeSection,
                    _game.Items[ItemType.Crystal]));
            }
            else if (_prizeSection.Prize.Type == ItemType.GreenPendant)
                _undoRedoManager.Execute(new ChangePrize(_prizeSection, null));
            else
            {
                Item newPrize = _game.Items[_prizeSection.Prize.Type + 1];
                _undoRedoManager.Execute(new ChangePrize(_prizeSection, newPrize));
            }
        }

        public void OnLeftClick()
        {
            TogglePrize();
        }

        public void OnRightClick()
        {
            ChangePrize();
        }
    }
}
