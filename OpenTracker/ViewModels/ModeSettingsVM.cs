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
    public class ModeSettingsVM : ViewModelBase
    {
        public ReactiveCommand<string, Unit> ItemPlacementCommand { get; }
        public ReactiveCommand<Unit, Unit> MapShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> CompassShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> SmallKeyShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> BigKeyShuffleCommand { get; }
        public ReactiveCommand<string, Unit> WorldStateCommand { get; }
        public ReactiveCommand<string, Unit> EntranceShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> BossShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> EnemyShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> GuaranteedBossItemsCommand { get; }
        public ReactiveCommand<Unit, Unit> GenericKeysCommand { get; }
        public ReactiveCommand<Unit, Unit> TakeAnyLocationsCommand { get; }

        public static bool BasicItemPlacement =>
            Mode.Instance.ItemPlacement == ItemPlacement.Basic;
        public static bool AdvancedItemPlacement =>
            Mode.Instance.ItemPlacement == ItemPlacement.Advanced;

        public static bool MapShuffle =>
            Mode.Instance.MapShuffle;
        public static bool CompassShuffle =>
            Mode.Instance.CompassShuffle;
        public static bool SmallKeyShuffle =>
            Mode.Instance.SmallKeyShuffle;
        public static bool BigKeyShuffle =>
            Mode.Instance.BigKeyShuffle;

        public bool StandardOpenWorldState =>
            Mode.Instance.WorldState == WorldState.StandardOpen;
        public static bool InvertedWorldState =>
            Mode.Instance.WorldState == WorldState.Inverted;

        public static bool EntranceShuffleNone =>
            Mode.Instance.EntranceShuffle == Models.Modes.EntranceShuffle.None;
        public static bool EntranceShuffleDungeon =>
            Mode.Instance.EntranceShuffle == Models.Modes.EntranceShuffle.Dungeon;
        public static bool EntranceShuffleAll =>
            Mode.Instance.EntranceShuffle == Models.Modes.EntranceShuffle.All;
        public static bool EntranceShuffleInsanity =>
            Mode.Instance.EntranceShuffle == EntranceShuffle.Insanity;

        public static bool BossShuffle =>
            Mode.Instance.BossShuffle;
        public static bool EnemyShuffle =>
            Mode.Instance.EnemyShuffle;
        public static bool GuaranteedBossItems =>
            Mode.Instance.GuaranteedBossItems;
        public static bool GenericKeys =>
            Mode.Instance.GenericKeys;
        public static bool TakeAnyLocations =>
            Mode.Instance.TakeAnyLocations;

        private bool _modeSettingsPopupOpen;
        public bool ModeSettingsPopupOpen
        {
            get => _modeSettingsPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _modeSettingsPopupOpen, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ModeSettingsVM()
        {
            ItemPlacementCommand = ReactiveCommand.Create<string>(
                SetItemPlacement, this.WhenAnyValue(x => x.StandardOpenWorldState));
            MapShuffleCommand = ReactiveCommand.Create(ToggleMapShuffle);
            CompassShuffleCommand = ReactiveCommand.Create(ToggleCompassShuffle);
            SmallKeyShuffleCommand = ReactiveCommand.Create(ToggleSmallKeyShuffle);
            BigKeyShuffleCommand = ReactiveCommand.Create(ToggleBigKeyShuffle);
            WorldStateCommand = ReactiveCommand.Create<string>(SetWorldState);
            EntranceShuffleCommand = ReactiveCommand.Create<string>(SetEntranceShuffle);
            BossShuffleCommand = ReactiveCommand.Create(ToggleBossShuffle);
            EnemyShuffleCommand = ReactiveCommand.Create(ToggleEnemyShuffle);
            GuaranteedBossItemsCommand = ReactiveCommand.Create(ToggleGuaranteedBossItems);
            GenericKeysCommand = ReactiveCommand.Create(ToggleGenericKeys);
            TakeAnyLocationsCommand = ReactiveCommand.Create(ToggleTakeAnyLocations);

            Mode.Instance.PropertyChanged += OnModeChanged;
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

            if (e.PropertyName == nameof(Mode.MapShuffle))
            {
                this.RaisePropertyChanged(nameof(MapShuffle));
            }

            if (e.PropertyName == nameof(Mode.CompassShuffle))
            {
                this.RaisePropertyChanged(nameof(CompassShuffle));
            }

            if (e.PropertyName == nameof(Mode.SmallKeyShuffle))
            {
                this.RaisePropertyChanged(nameof(SmallKeyShuffle));
            }

            if (e.PropertyName == nameof(Mode.BigKeyShuffle))
            {
                this.RaisePropertyChanged(nameof(BigKeyShuffle));
            }

            if (e.PropertyName == nameof(Mode.WorldState))
            {
                this.RaisePropertyChanged(nameof(StandardOpenWorldState));
                this.RaisePropertyChanged(nameof(InvertedWorldState));
            }

            if (e.PropertyName == nameof(Mode.EntranceShuffle))
            {
                this.RaisePropertyChanged(nameof(EntranceShuffleNone));
                this.RaisePropertyChanged(nameof(EntranceShuffleDungeon));
                this.RaisePropertyChanged(nameof(EntranceShuffleAll));
                this.RaisePropertyChanged(nameof(EntranceShuffleInsanity));
            }

            if (e.PropertyName == nameof(Mode.BossShuffle))
            {
                this.RaisePropertyChanged(nameof(BossShuffle));
            }

            if (e.PropertyName == nameof(Mode.EnemyShuffle))
            {
                this.RaisePropertyChanged(nameof(EnemyShuffle));
            }

            if (e.PropertyName == nameof(Mode.GuaranteedBossItems))
            {
                this.RaisePropertyChanged(nameof(GuaranteedBossItems));
            }

            if (e.PropertyName == nameof(Mode.GenericKeys))
            {
                this.RaisePropertyChanged(nameof(GenericKeys));
            }

            if (e.PropertyName == nameof(Mode.TakeAnyLocations))
            {
                this.RaisePropertyChanged(nameof(TakeAnyLocations));
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
        /// Toggles the map shuffle setting.
        /// </summary>
        private void ToggleMapShuffle()
        {
            UndoRedoManager.Instance.Execute(new ChangeMapShuffle(!Mode.Instance.MapShuffle));
        }

        /// <summary>
        /// Toggles the compass shuffle setting.
        /// </summary>
        private void ToggleCompassShuffle()
        {
            UndoRedoManager.Instance.Execute(new ChangeCompassShuffle(!Mode.Instance.CompassShuffle));
        }

        /// <summary>
        /// Toggles the small key shuffle setting.
        /// </summary>
        private void ToggleSmallKeyShuffle()
        {
            UndoRedoManager.Instance.Execute(new ChangeSmallKeyShuffle(!Mode.Instance.SmallKeyShuffle));
        }

        /// <summary>
        /// Toggles the big key shuffle setting.
        /// </summary>
        private void ToggleBigKeyShuffle()
        {
            UndoRedoManager.Instance.Execute(new ChangeBigKeyShuffle(!Mode.Instance.BigKeyShuffle));
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
        private void SetEntranceShuffle(string entranceShuffleString)
        {
            if (Enum.TryParse(entranceShuffleString, out EntranceShuffle entranceShuffle))
            {
                UndoRedoManager.Instance.Execute(new ChangeEntranceShuffle(entranceShuffle));
            }
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

        /// <summary>
        /// Toggles the guaranteed boss items setting.
        /// </summary>
        private void ToggleGuaranteedBossItems()
        {
            UndoRedoManager.Instance.Execute(new ChangeGuaranteedBossItems(
                !Mode.Instance.GuaranteedBossItems));
        }

        /// <summary>
        /// Toggles the generic keys setting.
        /// </summary>
        private void ToggleGenericKeys()
        {
            UndoRedoManager.Instance.Execute(new ChangeGenericKeys(
                !Mode.Instance.GenericKeys));
        }

        /// <summary>
        /// Toggles the take any locations setting.
        /// </summary>
        private void ToggleTakeAnyLocations()
        {
            UndoRedoManager.Instance.Execute(new ChangeTakeAnyLocations(
                !Mode.Instance.TakeAnyLocations));
        }
    }
}
