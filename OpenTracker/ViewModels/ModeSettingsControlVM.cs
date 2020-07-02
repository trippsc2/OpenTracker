using OpenTracker.Models;
using OpenTracker.Models.Undoables;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Reactive;
using OpenTracker.ViewModels.Bases;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the view-model for the mode settings popup control.
    /// </summary>
    public class ModeSettingsControlVM : ViewModelBase
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly Mode _mode;

        public ReactiveCommand<string, Unit> ItemPlacementCommand { get; }
        public ReactiveCommand<string, Unit> DungeonItemShuffleCommand { get; }
        public ReactiveCommand<string, Unit> WorldStateCommand { get; }
        public ReactiveCommand<Unit, Unit> EntranceShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> BossShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> EnemyShuffleCommand { get; }

        public bool BasicItemPlacement =>
            _mode.ItemPlacement == ItemPlacement.Basic;
        public bool AdvancedItemPlacement =>
            _mode.ItemPlacement == ItemPlacement.Advanced;

        public bool StandardDungeonItemShuffle =>
            _mode.DungeonItemShuffle == DungeonItemShuffle.Standard;
        public bool MapsCompassesDungeonItemShuffle =>
            _mode.DungeonItemShuffle == DungeonItemShuffle.MapsCompasses;
        public bool MapsCompassesSmallKeysDungeonItemShuffle =>
            _mode.DungeonItemShuffle == DungeonItemShuffle.MapsCompassesSmallKeys;
        public bool KeysanityDungeonItemShuffle =>
            _mode.DungeonItemShuffle == DungeonItemShuffle.Keysanity;

        public bool StandardOpenWorldState =>
            _mode.WorldState == WorldState.StandardOpen;
        public bool InvertedWorldState =>
            _mode.WorldState == WorldState.Inverted;
        public bool RetroWorldState =>
            _mode.WorldState == WorldState.Retro;

        public bool EntranceShuffle =>
            _mode.EntranceShuffle;
        public bool BossShuffle =>
            _mode.BossShuffle;
        public bool EnemyShuffle =>
            _mode.EnemyShuffle;

        private bool _modeSettingsPopupOpen;
        public bool ModeSettingsPopupOpen
        {
            get => _modeSettingsPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _modeSettingsPopupOpen, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The game mode data.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        public ModeSettingsControlVM(Mode mode, UndoRedoManager undoRedoManager)
        {
            _undoRedoManager = undoRedoManager;
            _mode = mode ?? throw new ArgumentNullException(nameof(mode));

            _mode.PropertyChanged += OnModeChanged;

            ItemPlacementCommand = ReactiveCommand.Create<string>(SetItemPlacement, this.WhenAnyValue(x => x.StandardOpenWorldState));
            DungeonItemShuffleCommand = ReactiveCommand.Create<string>(SetDungeonItemShuffle);
            WorldStateCommand = ReactiveCommand.Create<string>(SetWorldState);
            EntranceShuffleCommand = ReactiveCommand.Create(ToggleEntranceShuffle);
            BossShuffleCommand = ReactiveCommand.Create(ToggleBossShuffle);
            EnemyShuffleCommand = ReactiveCommand.Create(ToggleEnemyShuffle);
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
            if (e.PropertyName == nameof(Mode.ItemPlacement))
            {
                UpdateItemPlacement();
            }

            if (e.PropertyName == nameof(Mode.DungeonItemShuffle))
            {
                UpdateDungeonItemShuffle();
            }

            if (e.PropertyName == nameof(Mode.WorldState))
            {
                UpdateWorldState();
            }

            if (e.PropertyName == nameof(Mode.EntranceShuffle))
            {
                this.RaisePropertyChanged(nameof(EntranceShuffle));
            }

            if (e.PropertyName == nameof(Mode.BossShuffle))
            {
                this.RaisePropertyChanged(nameof(BossShuffle));
            }

            if (e.PropertyName == nameof(Mode.EnemyShuffle))
            {
                this.RaisePropertyChanged(nameof(EnemyShuffle));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the BasicItemPlacement and AdvancedItemPlacement
        /// properties.
        /// </summary>
        private void UpdateItemPlacement()
        {
            this.RaisePropertyChanged(nameof(BasicItemPlacement));
            this.RaisePropertyChanged(nameof(AdvancedItemPlacement));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the StandardDungeonItemShuffle,
        /// MapsCompassesDungeonItemShuffle, MapsCompassesSmallKeysDungeonItemShuffle,
        /// and KeysanityDungeonItemShuffle properties.
        /// properties.
        /// </summary>
        private void UpdateDungeonItemShuffle()
        {
            this.RaisePropertyChanged(nameof(StandardDungeonItemShuffle));
            this.RaisePropertyChanged(nameof(MapsCompassesDungeonItemShuffle));
            this.RaisePropertyChanged(nameof(MapsCompassesSmallKeysDungeonItemShuffle));
            this.RaisePropertyChanged(nameof(KeysanityDungeonItemShuffle));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the StandardOpenWorldState, InvertedWorldState,
        /// and RetroWorldState properties.
        /// </summary>
        private void UpdateWorldState()
        {
            this.RaisePropertyChanged(nameof(StandardOpenWorldState));
            this.RaisePropertyChanged(nameof(InvertedWorldState));
            this.RaisePropertyChanged(nameof(RetroWorldState));
        }

        /// <summary>
        /// Sets the Item Placement setting to the specified value.
        /// </summary>
        /// <param name="itemPlacementString">
        /// A string representing the new item placement setting.
        /// </param>
        private void SetItemPlacement(string itemPlacementString)
        {
            if (Enum.TryParse(itemPlacementString, out ItemPlacement itemPlacement))
            {
                _undoRedoManager.Execute(new ChangeItemPlacement(_mode, itemPlacement));
            }
        }

        /// <summary>
        /// Sets the Dungeon Item Shuffle setting to the specified value.
        /// </summary>
        /// <param name="dungeonItemShuffleString">
        /// A string representing the new dungeon item shuffle setting.
        /// </param>
        private void SetDungeonItemShuffle(string dungeonItemShuffleString)
        {
            if (Enum.TryParse(dungeonItemShuffleString, out DungeonItemShuffle dungeonItemShuffle))
            {
                _undoRedoManager.Execute(new ChangeDungeonItemShuffle(_mode, dungeonItemShuffle));
            }
        }

        /// <summary>
        /// Sets the World State setting to the specified value.
        /// </summary>
        /// <param name="worldStateString">
        /// A string representing the new world state setting.
        /// </param>
        private void SetWorldState(string worldStateString)
        {
            if (Enum.TryParse(worldStateString, out WorldState worldState))
            {
                _undoRedoManager.Execute(new ChangeWorldState(_mode, worldState));
            }
        }

        /// <summary>
        /// Toggles the entrance shuffle setting.
        /// </summary>
        private void ToggleEntranceShuffle()
        {
            _undoRedoManager.Execute(new ChangeEntranceShuffle(_mode, !_mode.EntranceShuffle));
        }

        /// <summary>
        /// Toggles the boss shuffle setting.
        /// </summary>
        private void ToggleBossShuffle()
        {
            _undoRedoManager.Execute(new ChangeBossShuffle(_mode, !_mode.BossShuffle));
        }

        /// <summary>
        /// Toggles the enemy shuffle setting.
        /// </summary>
        private void ToggleEnemyShuffle()
        {
            _undoRedoManager.Execute(new ChangeEnemyShuffle(_mode, !_mode.EnemyShuffle));
        }
    }
}
