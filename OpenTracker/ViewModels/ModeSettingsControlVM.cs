using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels
{
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

        public bool BasicItemPlacement => _mode.ItemPlacement == ItemPlacement.Basic;
        public bool AdvancedItemPlacement => _mode.ItemPlacement == ItemPlacement.Advanced;

        public bool StandardDungeonItemShuffle =>
            _mode.DungeonItemShuffle == DungeonItemShuffle.Standard;
        public bool MapsCompassesDungeonItemShuffle =>
            _mode.DungeonItemShuffle == DungeonItemShuffle.MapsCompasses;
        public bool MapsCompassesSmallKeysDungeonItemShuffle =>
            _mode.DungeonItemShuffle == DungeonItemShuffle.MapsCompassesSmallKeys;
        public bool KeysanityDungeonItemShuffle =>
            _mode.DungeonItemShuffle == DungeonItemShuffle.Keysanity;

        public bool StandardOpenRetroWorldState =>
            _mode.WorldState == WorldState.StandardOpen;
        public bool InvertedWorldState =>
            _mode.WorldState == WorldState.Inverted;
        public bool RetroWorldState =>
            _mode.WorldState == WorldState.Retro;

        public bool EntranceShuffle => _mode.EntranceShuffle.Value;
        public bool BossShuffle => _mode.BossShuffle.Value;
        public bool EnemyShuffle => _mode.EnemyShuffle.Value;

        private bool _modeSettingsPopupOpen;
        public bool ModeSettingsPopupOpen
        {
            get => _modeSettingsPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _modeSettingsPopupOpen, value);
        }

        public ModeSettingsControlVM(Mode mode, UndoRedoManager undoRedoManager)
        {
            _undoRedoManager = undoRedoManager;
            _mode = mode;

            _mode.PropertyChanged += OnModeChanged;

            ItemPlacementCommand = ReactiveCommand.Create<string>(SetItemPlacement, this.WhenAnyValue(x => x.StandardOpenRetroWorldState));
            DungeonItemShuffleCommand = ReactiveCommand.Create<string>(SetDungeonItemShuffle);
            WorldStateCommand = ReactiveCommand.Create<string>(SetWorldState);
            EntranceShuffleCommand = ReactiveCommand.Create(ToggleEntranceShuffle);
            BossShuffleCommand = ReactiveCommand.Create(ToggleBossShuffle);
            EnemyShuffleCommand = ReactiveCommand.Create(ToggleEnemyShuffle);
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.ItemPlacement))
                UpdateItemPlacement();

            if (e.PropertyName == nameof(Mode.DungeonItemShuffle))
                UpdateDungeonItemShuffle();

            if (e.PropertyName == nameof(Mode.WorldState))
                UpdateWorldState();

            if (e.PropertyName == nameof(Mode.EntranceShuffle))
                this.RaisePropertyChanged(nameof(EntranceShuffle));

            if (e.PropertyName == nameof(Mode.BossShuffle))
                this.RaisePropertyChanged(nameof(BossShuffle));

            if (e.PropertyName == nameof(Mode.EnemyShuffle))
                this.RaisePropertyChanged(nameof(EnemyShuffle));
        }

        private void UpdateItemPlacement()
        {
            this.RaisePropertyChanged(nameof(BasicItemPlacement));
            this.RaisePropertyChanged(nameof(AdvancedItemPlacement));
        }

        private void UpdateDungeonItemShuffle()
        {
            this.RaisePropertyChanged(nameof(StandardDungeonItemShuffle));
            this.RaisePropertyChanged(nameof(MapsCompassesDungeonItemShuffle));
            this.RaisePropertyChanged(nameof(MapsCompassesSmallKeysDungeonItemShuffle));
            this.RaisePropertyChanged(nameof(KeysanityDungeonItemShuffle));
        }

        private void UpdateWorldState()
        {
            this.RaisePropertyChanged(nameof(StandardOpenRetroWorldState));
            this.RaisePropertyChanged(nameof(InvertedWorldState));
            this.RaisePropertyChanged(nameof(RetroWorldState));
        }

        private void SetItemPlacement(string itemPlacementString)
        {
            if (Enum.TryParse(itemPlacementString, out ItemPlacement itemPlacement))
                _undoRedoManager.Execute(new ChangeItemPlacement(_mode, itemPlacement));
        }

        private void SetDungeonItemShuffle(string dungeonItemShuffleString)
        {
            if (Enum.TryParse(dungeonItemShuffleString, out DungeonItemShuffle dungeonItemShuffle))
                _undoRedoManager.Execute(new ChangeDungeonItemShuffle(_mode, dungeonItemShuffle));
        }

        private void SetWorldState(string worldStateString)
        {
            if (Enum.TryParse(worldStateString, out WorldState worldState))
                _undoRedoManager.Execute(new ChangeWorldState(_mode, worldState));
        }

        private void ToggleEntranceShuffle()
        {
            _undoRedoManager.Execute(new ChangeEntranceShuffle(_mode, !_mode.EntranceShuffle.Value));
        }

        private void ToggleBossShuffle()
        {
            _undoRedoManager.Execute(new ChangeBossShuffle(_mode, !_mode.BossShuffle.Value));
        }

        private void ToggleEnemyShuffle()
        {
            _undoRedoManager.Execute(new ChangeEnemyShuffle(_mode, !_mode.EnemyShuffle.Value));
        }
    }
}
