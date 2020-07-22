using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Reactive;
using OpenTracker.Models.Modes;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the mode settings popup control.
    /// </summary>
    public class ModeSettingsControlVM : ViewModelBase
    {
        public ReactiveCommand<string, Unit> ItemPlacementCommand { get; }
        public ReactiveCommand<string, Unit> DungeonItemShuffleCommand { get; }
        public ReactiveCommand<string, Unit> WorldStateCommand { get; }
        public ReactiveCommand<Unit, Unit> EntranceShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> BossShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> EnemyShuffleCommand { get; }

        public bool BasicItemPlacement =>
            Mode.Instance.ItemPlacement == ItemPlacement.Basic;
        public bool AdvancedItemPlacement =>
            Mode.Instance.ItemPlacement == ItemPlacement.Advanced;

        public bool StandardDungeonItemShuffle =>
            Mode.Instance.DungeonItemShuffle == DungeonItemShuffle.Standard;
        public bool MapsCompassesDungeonItemShuffle =>
            Mode.Instance.DungeonItemShuffle == DungeonItemShuffle.MapsCompasses;
        public bool MapsCompassesSmallKeysDungeonItemShuffle =>
            Mode.Instance.DungeonItemShuffle == DungeonItemShuffle.MapsCompassesSmallKeys;
        public bool KeysanityDungeonItemShuffle =>
            Mode.Instance.DungeonItemShuffle == DungeonItemShuffle.Keysanity;

        public bool StandardOpenWorldState =>
            Mode.Instance.WorldState == WorldState.StandardOpen;
        public bool InvertedWorldState =>
            Mode.Instance.WorldState == WorldState.Inverted;
        public bool RetroWorldState =>
            Mode.Instance.WorldState == WorldState.Retro;

        public bool EntranceShuffle =>
            Mode.Instance.EntranceShuffle;
        public bool BossShuffle =>
            Mode.Instance.BossShuffle;
        public bool EnemyShuffle =>
            Mode.Instance.EnemyShuffle;

        private bool _modeSettingsPopupOpen;
        public bool ModeSettingsPopupOpen
        {
            get => _modeSettingsPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _modeSettingsPopupOpen, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ModeSettingsControlVM()
        {
            Mode.Instance.PropertyChanged += OnModeChanged;

            ItemPlacementCommand = ReactiveCommand.Create<string>(
                SetItemPlacement, this.WhenAnyValue(x => x.StandardOpenWorldState));
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
                this.RaisePropertyChanged(nameof(BasicItemPlacement));
                this.RaisePropertyChanged(nameof(AdvancedItemPlacement));
            }

            if (e.PropertyName == nameof(Mode.DungeonItemShuffle))
            {
                this.RaisePropertyChanged(nameof(StandardDungeonItemShuffle));
                this.RaisePropertyChanged(nameof(MapsCompassesDungeonItemShuffle));
                this.RaisePropertyChanged(nameof(MapsCompassesSmallKeysDungeonItemShuffle));
                this.RaisePropertyChanged(nameof(KeysanityDungeonItemShuffle));
            }

            if (e.PropertyName == nameof(Mode.WorldState))
            {
                this.RaisePropertyChanged(nameof(StandardOpenWorldState));
                this.RaisePropertyChanged(nameof(InvertedWorldState));
                this.RaisePropertyChanged(nameof(RetroWorldState));
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
        /// Sets the Item Placement setting to the specified value.
        /// </summary>
        /// <param name="itemPlacementString">
        /// A string representing the new item placement setting.
        /// </param>
        private void SetItemPlacement(string itemPlacementString)
        {
            if (Enum.TryParse(itemPlacementString, out ItemPlacement itemPlacement))
            {
                UndoRedoManager.Instance.Execute(new ChangeItemPlacement(itemPlacement));
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
                UndoRedoManager.Instance.Execute(new ChangeDungeonItemShuffle(dungeonItemShuffle));
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
                UndoRedoManager.Instance.Execute(new ChangeWorldState(worldState));
            }
        }

        /// <summary>
        /// Toggles the entrance shuffle setting.
        /// </summary>
        private void ToggleEntranceShuffle()
        {
            UndoRedoManager.Instance.Execute(new ChangeEntranceShuffle(!Mode.Instance.EntranceShuffle));
        }

        /// <summary>
        /// Toggles the boss shuffle setting.
        /// </summary>
        private void ToggleBossShuffle()
        {
            UndoRedoManager.Instance.Execute(new ChangeBossShuffle(!Mode.Instance.BossShuffle));
        }

        /// <summary>
        /// Toggles the enemy shuffle setting.
        /// </summary>
        private void ToggleEnemyShuffle()
        {
            UndoRedoManager.Instance.Execute(new ChangeEnemyShuffle(!Mode.Instance.EnemyShuffle));
        }
    }
}
