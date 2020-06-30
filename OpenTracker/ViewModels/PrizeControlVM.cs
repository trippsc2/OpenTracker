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
    /// This is the view-model of the prize control on the items panel.
    /// </summary>
    public class PrizeControlVM : ViewModelBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly Game _game;
        private readonly BossSection _prizeSection;

        public string ImageSource
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("avares://OpenTracker/Assets/Images/Items/");

                if (_prizeSection.Prize == null)
                {
                    sb.Append("unknown");
                }
                else
                {
                    sb.Append(_prizeSection.Prize.Type.ToString().ToLowerInvariant());
                }

                sb.Append(_prizeSection.IsAvailable() ? "0" : "1");
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
        /// <param name="prizeSection">
        /// The prize section to be represented.
        /// </param>
        public PrizeControlVM(UndoRedoManager undoRedoManager, Game game, BossSection prizeSection)
        {
            _undoRedoManager = undoRedoManager;
            _game = game;
            _prizeSection = prizeSection ?? throw new ArgumentNullException(nameof(prizeSection));

            _prizeSection.PropertyChanged += OnSectionChanged;
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
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BossSection.Prize) ||
                e.PropertyName == nameof(BossSection.Available))
            {
                this.RaisePropertyChanged(nameof(ImageSource));
            }
        }

        /// <summary>
        /// Toggles whether the prize is collected or not.
        /// </summary>
        private void TogglePrize()
        {
            _undoRedoManager.Execute(new TogglePrize(_prizeSection));
        }

        /// <summary>
        /// Change the prize of the dungeon.
        /// </summary>
        private void ChangePrize()
        {
            if (_prizeSection.Prize == null)
            {
                _undoRedoManager.Execute(new ChangePrize(_prizeSection,
                    _game.Items[ItemType.Crystal]));
            }
            else if (_prizeSection.Prize.Type == ItemType.GreenPendant)
            {
                _undoRedoManager.Execute(new ChangePrize(_prizeSection, null));
            }
            else
            {
                var newPrize = _game.Items[_prizeSection.Prize.Type + 1];
                _undoRedoManager.Execute(new ChangePrize(_prizeSection, newPrize));
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
            TogglePrize();
        }

        /// <summary>
        /// Click handler for right click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            ChangePrize();
        }
    }
}
