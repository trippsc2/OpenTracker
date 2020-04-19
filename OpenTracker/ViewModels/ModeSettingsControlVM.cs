using OpenTracker.Actions;
using OpenTracker.Models;
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

        private bool _modeSettingsPopupOpen;
        public bool ModeSettingsPopupOpen
        {
            get => _modeSettingsPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _modeSettingsPopupOpen, value);
        }

        private bool _itemPlacementBasic;
        public bool ItemPlacementBasic
        {
            get => _itemPlacementBasic;
            set => this.RaiseAndSetIfChanged(ref _itemPlacementBasic, value);
        }

        private bool _itemPlacementAdvanced;
        public bool ItemPlacementAdvanced
        {
            get => _itemPlacementAdvanced;
            set => this.RaiseAndSetIfChanged(ref _itemPlacementAdvanced, value);
        }

        private bool _dungeonItemShuffleStandard;
        public bool DungeonItemShuffleStandard
        {
            get => _dungeonItemShuffleStandard;
            set => this.RaiseAndSetIfChanged(ref _dungeonItemShuffleStandard, value);
        }

        private bool _dungeonItemShuffleMapsCompasses;
        public bool DungeonItemShuffleMapsCompasses
        {
            get => _dungeonItemShuffleMapsCompasses;
            set => this.RaiseAndSetIfChanged(ref _dungeonItemShuffleMapsCompasses, value);
        }

        private bool _dungeonItemShuffleMapsCompassesSmallKeys;
        public bool DungeonItemShuffleMapsCompassesSmallKeys
        {
            get => _dungeonItemShuffleMapsCompassesSmallKeys;
            set => this.RaiseAndSetIfChanged(ref _dungeonItemShuffleMapsCompassesSmallKeys, value);
        }

        private bool _dungeonItemShuffleKeysanity;
        public bool DungeonItemShuffleKeysanity
        {
            get => _dungeonItemShuffleKeysanity;
            set => this.RaiseAndSetIfChanged(ref _dungeonItemShuffleKeysanity, value);
        }

        private bool _worldStateStandardOpen;
        public bool WorldStateStandardOpen
        {
            get => _worldStateStandardOpen;
            set => this.RaiseAndSetIfChanged(ref _worldStateStandardOpen, value);
        }

        private bool _worldStateInverted;
        public bool WorldStateInverted
        {
            get => _worldStateInverted;
            set => this.RaiseAndSetIfChanged(ref _worldStateInverted, value);
        }

        private bool _worldStateRetro;
        public bool WorldStateRetro
        {
            get => _worldStateRetro;
            set => this.RaiseAndSetIfChanged(ref _worldStateRetro, value);
        }

        private bool _entranceShuffle;
        public bool EntranceShuffle
        {
            get => _entranceShuffle;
            set => this.RaiseAndSetIfChanged(ref _entranceShuffle, value);
        }

        private bool _bossShuffle;
        public bool BossShuffle
        {
            get => _bossShuffle;
            set => this.RaiseAndSetIfChanged(ref _bossShuffle, value);
        }

        private bool _enemyShuffle;
        public bool EnemyShuffle
        {
            get => _enemyShuffle;
            set => this.RaiseAndSetIfChanged(ref _enemyShuffle, value);
        }

        private bool _smallKeyShuffle;
        public bool SmallKeyShuffle
        {
            get => _smallKeyShuffle;
            set => this.RaiseAndSetIfChanged(ref _smallKeyShuffle, value);
        }

        private bool _bigKeyShuffle;
        public bool BigKeyShuffle
        {
            get => _bigKeyShuffle;
            set => this.RaiseAndSetIfChanged(ref _bigKeyShuffle, value);
        }

        public ModeSettingsControlVM(Mode mode, UndoRedoManager undoRedoManager)
        {
            _undoRedoManager = undoRedoManager;
            _mode = mode;

            _mode.PropertyChanged += OnModeChanged;

            ItemPlacementCommand = ReactiveCommand.Create<string>(SetItemPlacement, this.WhenAnyValue(x => x.WorldStateStandardOpen));
            DungeonItemShuffleCommand = ReactiveCommand.Create<string>(SetDungeonItemShuffle);
            WorldStateCommand = ReactiveCommand.Create<string>(SetWorldState);
            EntranceShuffleCommand = ReactiveCommand.Create(ToggleEntranceShuffle);
            BossShuffleCommand = ReactiveCommand.Create(ToggleBossShuffle);
            EnemyShuffleCommand = ReactiveCommand.Create(ToggleEnemyShuffle);

            UpdateItemPlacement();
            UpdateDungeonItemShuffle();
            UpdateWorldState();
            UpdateEntranceShuffle();
            UpdateBossShuffle();
            UpdateEnemyShuffle();
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
                UpdateEntranceShuffle();

            if (e.PropertyName == nameof(Mode.BossShuffle))
                UpdateBossShuffle();

            if (e.PropertyName == nameof(Mode.EnemyShuffle))
                UpdateEnemyShuffle();
        }

        private void UpdateItemPlacement()
        {
            switch (_mode.ItemPlacement)
            {
                case null:
                    ItemPlacementBasic = false;
                    ItemPlacementAdvanced = false;
                    break;
                case ItemPlacement.Basic:
                    ItemPlacementBasic = true;
                    ItemPlacementAdvanced = false;
                    break;
                case ItemPlacement.Advanced:
                    ItemPlacementBasic = false;
                    ItemPlacementAdvanced = true;
                    break;
            }
        }

        private void UpdateDungeonItemShuffle()
        {
            switch (_mode.DungeonItemShuffle)
            {
                case null:
                    DungeonItemShuffleStandard = false;
                    DungeonItemShuffleMapsCompasses = false;
                    DungeonItemShuffleMapsCompassesSmallKeys = false;
                    DungeonItemShuffleKeysanity = false;
                    SmallKeyShuffle = false;
                    BigKeyShuffle = false;
                    break;
                case DungeonItemShuffle.Standard:
                    DungeonItemShuffleStandard = true;
                    DungeonItemShuffleMapsCompasses = false;
                    DungeonItemShuffleMapsCompassesSmallKeys = false;
                    DungeonItemShuffleKeysanity = false;
                    SmallKeyShuffle = false;
                    BigKeyShuffle = false;
                    break;
                case DungeonItemShuffle.MapsCompasses:
                    DungeonItemShuffleStandard = false;
                    DungeonItemShuffleMapsCompasses = true;
                    DungeonItemShuffleMapsCompassesSmallKeys = false;
                    DungeonItemShuffleKeysanity = false;
                    SmallKeyShuffle = false;
                    BigKeyShuffle = false;
                    break;
                case DungeonItemShuffle.MapsCompassesSmallKeys:
                    DungeonItemShuffleStandard = false;
                    DungeonItemShuffleMapsCompasses = false;
                    DungeonItemShuffleMapsCompassesSmallKeys = true;
                    DungeonItemShuffleKeysanity = false;
                    SmallKeyShuffle = true;
                    BigKeyShuffle = false;
                    break;
                case DungeonItemShuffle.Keysanity:
                    DungeonItemShuffleStandard = false;
                    DungeonItemShuffleMapsCompasses = false;
                    DungeonItemShuffleMapsCompassesSmallKeys = false;
                    DungeonItemShuffleKeysanity = true;
                    SmallKeyShuffle = true;
                    BigKeyShuffle = true;
                    break;
            }
        }

        private void UpdateWorldState()
        {
            switch (_mode.WorldState)
            {
                case null:
                    WorldStateStandardOpen = false;
                    WorldStateInverted = false;
                    WorldStateRetro = false;
                    break;
                case WorldState.StandardOpen:
                    WorldStateStandardOpen = true;
                    WorldStateInverted = false;
                    WorldStateRetro = false;
                    break;
                case WorldState.Inverted:
                    WorldStateStandardOpen = false;
                    WorldStateInverted = true;
                    WorldStateRetro = false;
                    break;
                case WorldState.Retro:
                    WorldStateStandardOpen = false;
                    WorldStateInverted = false;
                    WorldStateRetro = true;
                    break;
            }
        }

        private void UpdateEntranceShuffle()
        {
            if (_mode.EntranceShuffle.HasValue && _mode.EntranceShuffle.Value)
                EntranceShuffle = true;
            else
                EntranceShuffle = false;
        }

        private void UpdateBossShuffle()
        {
            if (_mode.BossShuffle.HasValue && _mode.BossShuffle.Value)
                BossShuffle = true;
            else
                BossShuffle = false;
        }

        private void UpdateEnemyShuffle()
        {
            if (_mode.EnemyShuffle.HasValue && _mode.EnemyShuffle.Value)
                EnemyShuffle = true;
            else
                EnemyShuffle = false;
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
