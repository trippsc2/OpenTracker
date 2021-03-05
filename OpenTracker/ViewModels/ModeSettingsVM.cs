using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the mode settings popup control.
    /// </summary>
    public class ModeSettingsVM : ViewModelBase, IModeSettingsVM
    {
        private readonly IMode _mode;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private bool _popupOpen;
        public bool PopupOpen
        {
            get => _popupOpen;
            set => this.RaiseAndSetIfChanged(ref _popupOpen, value);
        }

        public bool BasicItemPlacement =>
            _mode.ItemPlacement == ItemPlacement.Basic;
        public bool AdvancedItemPlacement =>
            _mode.ItemPlacement == ItemPlacement.Advanced;

        public bool MapShuffle =>
            _mode.MapShuffle;
        public bool CompassShuffle =>
            _mode.CompassShuffle;
        public bool SmallKeyShuffle =>
            _mode.SmallKeyShuffle;
        public bool BigKeyShuffle =>
            _mode.BigKeyShuffle;

        public bool StandardOpenWorldState =>
            _mode.WorldState == WorldState.StandardOpen;
        public bool InvertedWorldState =>
            _mode.WorldState == WorldState.Inverted;

        public bool EntranceShuffleNone =>
            _mode.EntranceShuffle == Models.Modes.EntranceShuffle.None;
        public bool EntranceShuffleDungeon =>
            _mode.EntranceShuffle == Models.Modes.EntranceShuffle.Dungeon;
        public bool EntranceShuffleAll =>
            _mode.EntranceShuffle == Models.Modes.EntranceShuffle.All;
        public bool EntranceShuffleInsanity =>
            _mode.EntranceShuffle == EntranceShuffle.Insanity;

        public bool BossShuffle =>
            _mode.BossShuffle;
        public bool EnemyShuffle =>
            _mode.EnemyShuffle;
        public bool GuaranteedBossItems =>
            _mode.GuaranteedBossItems;
        public bool ShopShuffle =>
            _mode.ShopShuffle;
        public bool GenericKeys =>
            _mode.GenericKeys;
        public bool TakeAnyLocations =>
            _mode.TakeAnyLocations;
        public bool KeyDropShuffle =>
            _mode.KeyDropShuffle;
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        public ReactiveCommand<string, Unit> ChangeItemPlacementCommand { get; }
        public ReactiveCommand<string, Unit> ChangeWorldStateCommand { get; }
        public ReactiveCommand<string, Unit> ChangeEntranceShuffleCommand { get; }

        public ReactiveCommand<Unit, Unit> ToggleMapShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleCompassShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleSmallKeyShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleBigKeyShuffleCommand { get; }

        public ReactiveCommand<Unit, Unit> ToggleBossShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleEnemyShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleGuaranteedBossItemsCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleShopShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleGenericKeysCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleTakeAnyLocationsCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleKeyDropShuffleCommand { get; }

        public ModeSettingsVM(
            IMode mode, IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory)
        {
            _mode = mode;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

            ChangeItemPlacementCommand = ReactiveCommand.Create<string>(
                ChangeItemPlacement, this.WhenAnyValue(x => x.StandardOpenWorldState));
            ChangeWorldStateCommand = ReactiveCommand.Create<string>(ChangeWorldState);
            ChangeEntranceShuffleCommand = ReactiveCommand.Create<string>(ChangeEntranceShuffle);
            
            ToggleMapShuffleCommand = ReactiveCommand.Create(ToggleMapShuffle);
            ToggleCompassShuffleCommand = ReactiveCommand.Create(ToggleCompassShuffle);
            ToggleSmallKeyShuffleCommand = ReactiveCommand.Create(ToggleSmallKeyShuffle);
            ToggleBigKeyShuffleCommand = ReactiveCommand.Create(ToggleBigKeyShuffle);
            ToggleBossShuffleCommand = ReactiveCommand.Create(ToggleBossShuffle);
            ToggleEnemyShuffleCommand = ReactiveCommand.Create(ToggleEnemyShuffle);
            ToggleGuaranteedBossItemsCommand = ReactiveCommand.Create(ToggleGuaranteedBossItems);
            ToggleShopShuffleCommand = ReactiveCommand.Create(ToggleShopShuffle);
            ToggleGenericKeysCommand = ReactiveCommand.Create(ToggleGenericKeys);
            ToggleTakeAnyLocationsCommand = ReactiveCommand.Create(ToggleTakeAnyLocations);
            ToggleKeyDropShuffleCommand = ReactiveCommand.Create(ToggleKeyDropShuffle);

            _mode.PropertyChanged += OnModeChanged;
        }

        /// <summary>
        /// Opens the mode settings popup control.
        /// </summary>
        private void OpenModeSettingsPopup()
        {
            PopupOpen = true;
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The pointer released event args.
        /// </param>
        private void HandleClick(PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                OpenModeSettingsPopup();
            }
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
        private async void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IMode.ItemPlacement):
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        this.RaisePropertyChanged(nameof(BasicItemPlacement));
                        this.RaisePropertyChanged(nameof(AdvancedItemPlacement));
                    });
                    break;
                case nameof(IMode.MapShuffle):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(MapShuffle)));
                    break;
                case nameof(IMode.CompassShuffle):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(CompassShuffle)));
                    break;
                case nameof(IMode.SmallKeyShuffle):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(SmallKeyShuffle)));
                    break;
                case nameof(IMode.BigKeyShuffle):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(BigKeyShuffle)));
                    break;
                case nameof(IMode.WorldState):
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        this.RaisePropertyChanged(nameof(StandardOpenWorldState));
                        this.RaisePropertyChanged(nameof(InvertedWorldState));
                    });
                    break;
                case nameof(IMode.EntranceShuffle):
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        this.RaisePropertyChanged(nameof(EntranceShuffleNone));
                        this.RaisePropertyChanged(nameof(EntranceShuffleDungeon));
                        this.RaisePropertyChanged(nameof(EntranceShuffleAll));
                        this.RaisePropertyChanged(nameof(EntranceShuffleInsanity));
                    });
                    break;
                case nameof(IMode.BossShuffle):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(BossShuffle)));
                    break;
                case nameof(IMode.EnemyShuffle):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(EnemyShuffle)));
                    break;
                case nameof(IMode.GuaranteedBossItems):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(GuaranteedBossItems)));
                    break;
                case nameof(IMode.ShopShuffle):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ShopShuffle)));
                    break;
                case nameof(IMode.GenericKeys):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(GenericKeys)));
                    break;
                case nameof(IMode.TakeAnyLocations):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(TakeAnyLocations)));
                    break;
                case nameof(IMode.KeyDropShuffle):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(KeyDropShuffle)));
                    break;
            }
        }

        /// <summary>
        /// Sets the Item Placement setting to the specified value.
        /// </summary>
        /// <param name="itemPlacementString">
        /// A string representing the new item placement setting.
        /// </param>
        private void ChangeItemPlacement(string itemPlacementString)
        {
            PopupOpen = false;
            
            if (Enum.TryParse(itemPlacementString, out ItemPlacement itemPlacement))
            {
                _undoRedoManager.NewAction(_undoableFactory.GetChangeItemPlacement(itemPlacement));
            }
        }

        /// <summary>
        /// Changes the World State setting to the specified value.
        /// </summary>
        /// <param name="worldStateString">
        /// A string representing the new world state setting.
        /// </param>
        private void ChangeWorldState(string worldStateString)
        {
            PopupOpen = false;
            
            if (Enum.TryParse(worldStateString, out WorldState worldState))
            {
                _undoRedoManager.NewAction(_undoableFactory.GetChangeWorldState(worldState));
            }
        }

        /// <summary>
        /// Changes the entrance shuffle setting.
        /// </summary>
        private void ChangeEntranceShuffle(string entranceShuffleString)
        {
            PopupOpen = false;
            
            if (Enum.TryParse(entranceShuffleString, out EntranceShuffle entranceShuffle))
            {
                _undoRedoManager.NewAction(_undoableFactory.GetChangeEntranceShuffle(entranceShuffle));
            }
        }

        /// <summary>
        /// Toggles the map shuffle setting.
        /// </summary>
        private void ToggleMapShuffle()
        {
            PopupOpen = false;
            
            _undoRedoManager.NewAction(_undoableFactory.GetChangeMapShuffle(!_mode.MapShuffle));
        }

        /// <summary>
        /// Toggles the compass shuffle setting.
        /// </summary>
        private void ToggleCompassShuffle()
        {
            PopupOpen = false;
            
            _undoRedoManager.NewAction(_undoableFactory.GetChangeCompassShuffle(!_mode.CompassShuffle));
        }

        /// <summary>
        /// Toggles the small key shuffle setting.
        /// </summary>
        private void ToggleSmallKeyShuffle()
        {
            PopupOpen = false;
            
            _undoRedoManager.NewAction(_undoableFactory.GetChangeSmallKeyShuffle(!_mode.SmallKeyShuffle));
        }

        /// <summary>
        /// Toggles the big key shuffle setting.
        /// </summary>
        private void ToggleBigKeyShuffle()
        {
            PopupOpen = false;
            
            _undoRedoManager.NewAction(_undoableFactory.GetChangeBigKeyShuffle(!_mode.BigKeyShuffle));
        }

        /// <summary>
        /// Toggles the boss shuffle setting.
        /// </summary>
        private void ToggleBossShuffle()
        {
            PopupOpen = false;
            
            _undoRedoManager.NewAction(_undoableFactory.GetChangeBossShuffle(!_mode.BossShuffle));
        }

        /// <summary>
        /// Toggles the enemy shuffle setting.
        /// </summary>
        private void ToggleEnemyShuffle()
        {
            PopupOpen = false;
            
            _undoRedoManager.NewAction(_undoableFactory.GetChangeEnemyShuffle(!_mode.EnemyShuffle));
        }

        /// <summary>
        /// Toggles the guaranteed boss items setting.
        /// </summary>
        private void ToggleGuaranteedBossItems()
        {
            PopupOpen = false;
            
            _undoRedoManager.NewAction(_undoableFactory.GetChangeGuaranteedBossItems(!_mode.GuaranteedBossItems));
        }

        /// <summary>
        /// Toggles the shop shuffle setting.
        /// </summary>
        private void ToggleShopShuffle()
        {
            PopupOpen = false;
            
            _undoRedoManager.NewAction(_undoableFactory.GetChangeShopShuffle(!_mode.ShopShuffle));
        }

        /// <summary>
        /// Toggles the generic keys setting.
        /// </summary>
        private void ToggleGenericKeys()
        {
            PopupOpen = false;
            
            _undoRedoManager.NewAction(_undoableFactory.GetChangeGenericKeys(!_mode.GenericKeys));
        }

        /// <summary>
        /// Toggles the take any locations setting.
        /// </summary>
        private void ToggleTakeAnyLocations()
        {
            PopupOpen = false;
            
            _undoRedoManager.NewAction(_undoableFactory.GetChangeTakeAnyLocations(!_mode.TakeAnyLocations));
        }

        /// <summary>
        /// Toggles the key drop shuffle setting.
        /// </summary>
        private void ToggleKeyDropShuffle()
        {
            PopupOpen = false;
            
            _undoRedoManager.NewAction(_undoableFactory.GetChangeKeyDropShuffle(!_mode.KeyDropShuffle));
        }
    }
}
