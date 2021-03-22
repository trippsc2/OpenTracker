using System;
using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This class contains the mode settings popup control ViewModel data.
    /// </summary>
    public class ModeSettingsVM : ViewModelBase, IModeSettingsVM
    {
        private readonly IMode _mode;
        private readonly IUndoRedoManager _undoRedoManager;

        private bool _popupOpen;
        public bool PopupOpen
        {
            get => _popupOpen;
            set => this.RaiseAndSetIfChanged(ref _popupOpen, value);
        }

        public bool BasicItemPlacement => _mode.ItemPlacement == ItemPlacement.Basic;
        public bool AdvancedItemPlacement => _mode.ItemPlacement == ItemPlacement.Advanced;

        public bool MapShuffle => _mode.MapShuffle;
        public bool CompassShuffle => _mode.CompassShuffle;
        public bool SmallKeyShuffle => _mode.SmallKeyShuffle;
        public bool BigKeyShuffle => _mode.BigKeyShuffle;

        public bool StandardOpenWorldState => _mode.WorldState == WorldState.StandardOpen;
        public bool InvertedWorldState => _mode.WorldState == WorldState.Inverted;

        public bool EntranceShuffleNone => _mode.EntranceShuffle == EntranceShuffle.None;
        public bool EntranceShuffleDungeon => _mode.EntranceShuffle == EntranceShuffle.Dungeon;
        public bool EntranceShuffleAll => _mode.EntranceShuffle == EntranceShuffle.All;
        public bool EntranceShuffleInsanity => _mode.EntranceShuffle == EntranceShuffle.Insanity;

        public bool BossShuffle => _mode.BossShuffle;
        public bool EnemyShuffle => _mode.EnemyShuffle;
        public bool GuaranteedBossItems => _mode.GuaranteedBossItems;
        public bool ShopShuffle => _mode.ShopShuffle;
        public bool GenericKeys => _mode.GenericKeys;
        public bool TakeAnyLocations => _mode.TakeAnyLocations;
        public bool KeyDropShuffle => _mode.KeyDropShuffle;
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public ReactiveCommand<string, Unit> ChangeItemPlacement { get; }
        public ReactiveCommand<string, Unit> ChangeWorldState { get; }
        public ReactiveCommand<string, Unit> ChangeEntranceShuffle { get; }

        public ReactiveCommand<Unit, Unit> ToggleMapShuffle { get; }
        public ReactiveCommand<Unit, Unit> ToggleCompassShuffle { get; }
        public ReactiveCommand<Unit, Unit> ToggleSmallKeyShuffle { get; }
        public ReactiveCommand<Unit, Unit> ToggleBigKeyShuffle { get; }

        public ReactiveCommand<Unit, Unit> ToggleBossShuffle { get; }
        public ReactiveCommand<Unit, Unit> ToggleEnemyShuffle { get; }
        public ReactiveCommand<Unit, Unit> ToggleGuaranteedBossItems { get; }
        public ReactiveCommand<Unit, Unit> ToggleShopShuffle { get; }
        public ReactiveCommand<Unit, Unit> ToggleGenericKeys { get; }
        public ReactiveCommand<Unit, Unit> ToggleTakeAnyLocations { get; }
        public ReactiveCommand<Unit, Unit> ToggleKeyDropShuffle { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings data.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        public ModeSettingsVM(IMode mode, IUndoRedoManager undoRedoManager)
        {
            _mode = mode;
            _undoRedoManager = undoRedoManager;

            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            ChangeItemPlacement = ReactiveCommand.Create<string>(
                ChangeItemPlacementImpl, this.WhenAnyValue(x => x.StandardOpenWorldState));
            ChangeWorldState = ReactiveCommand.Create<string>(ChangeWorldStateImpl);
            ChangeEntranceShuffle = ReactiveCommand.Create<string>(ChangeEntranceShuffleImpl);
            
            ToggleMapShuffle = ReactiveCommand.Create(ToggleMapShuffleImpl);
            ToggleCompassShuffle = ReactiveCommand.Create(ToggleCompassShuffleImpl);
            ToggleSmallKeyShuffle = ReactiveCommand.Create(ToggleSmallKeyShuffleImpl);
            ToggleBigKeyShuffle = ReactiveCommand.Create(ToggleBigKeyShuffleImpl);
            ToggleBossShuffle = ReactiveCommand.Create(ToggleBossShuffleImpl);
            ToggleEnemyShuffle = ReactiveCommand.Create(ToggleEnemyShuffleImpl);
            ToggleGuaranteedBossItems = ReactiveCommand.Create(ToggleGuaranteedBossItemsImpl);
            ToggleShopShuffle = ReactiveCommand.Create(ToggleShopShuffleImpl);
            ToggleGenericKeys = ReactiveCommand.Create(ToggleGenericKeysImpl);
            ToggleTakeAnyLocations = ReactiveCommand.Create(ToggleTakeAnyLocationsImpl);
            ToggleKeyDropShuffle = ReactiveCommand.Create(ToggleKeyDropShuffleImpl);

            _mode.PropertyChanged += OnModeChanged;
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
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                OpenModeSettingsPopup();
            }
        }

        /// <summary>
        /// Sets the Item Placement setting to the specified value.
        /// </summary>
        /// <param name="itemPlacementString">
        /// A string representing the new item placement setting.
        /// </param>
        private void ChangeItemPlacementImpl(string itemPlacementString)
        {
            if (Enum.TryParse(itemPlacementString, out ItemPlacement itemPlacement))
            {
                _undoRedoManager.NewAction(_mode.CreateChangeItemPlacementAction(itemPlacement));
            }
        }

        /// <summary>
        /// Changes the World State setting to the specified value.
        /// </summary>
        /// <param name="worldStateString">
        /// A string representing the new world state setting.
        /// </param>
        private void ChangeWorldStateImpl(string worldStateString)
        {
            if (Enum.TryParse(worldStateString, out WorldState worldState))
            {
                _undoRedoManager.NewAction(_mode.CreateChangeWorldStateAction(worldState));
            }
        }

        /// <summary>
        /// Changes the entrance shuffle setting.
        /// </summary>
        private void ChangeEntranceShuffleImpl(string entranceShuffleString)
        {
            if (Enum.TryParse(entranceShuffleString, out EntranceShuffle entranceShuffle))
            {
                _undoRedoManager.NewAction(_mode.CreateChangeEntranceShuffleAction(entranceShuffle));
            }
        }

        /// <summary>
        /// Toggles the map shuffle setting.
        /// </summary>
        private void ToggleMapShuffleImpl()
        {
            _undoRedoManager.NewAction(_mode.CreateChangeMapShuffleAction(!_mode.MapShuffle));
        }

        /// <summary>
        /// Toggles the compass shuffle setting.
        /// </summary>
        private void ToggleCompassShuffleImpl()
        {
            _undoRedoManager.NewAction(_mode.CreateChangeCompassShuffleAction(!_mode.CompassShuffle));
        }

        /// <summary>
        /// Toggles the small key shuffle setting.
        /// </summary>
        private void ToggleSmallKeyShuffleImpl()
        {
            _undoRedoManager.NewAction(_mode.CreateChangeSmallKeyShuffleAction(!_mode.SmallKeyShuffle));
        }

        /// <summary>
        /// Toggles the big key shuffle setting.
        /// </summary>
        private void ToggleBigKeyShuffleImpl()
        {
            _undoRedoManager.NewAction(_mode.CreateChangeBigKeyShuffleAction(!_mode.BigKeyShuffle));
        }

        /// <summary>
        /// Toggles the boss shuffle setting.
        /// </summary>
        private void ToggleBossShuffleImpl()
        {
            _undoRedoManager.NewAction(_mode.CreateChangeBossShuffleAction(!_mode.BossShuffle));
        }

        /// <summary>
        /// Toggles the enemy shuffle setting.
        /// </summary>
        private void ToggleEnemyShuffleImpl()
        {
            _undoRedoManager.NewAction(_mode.CreateChangeEnemyShuffleAction(!_mode.EnemyShuffle));
        }

        /// <summary>
        /// Toggles the guaranteed boss items setting.
        /// </summary>
        private void ToggleGuaranteedBossItemsImpl()
        {
            _undoRedoManager.NewAction(_mode.CreateChangeGuaranteedBossItemsAction(!_mode.GuaranteedBossItems));
        }

        /// <summary>
        /// Toggles the shop shuffle setting.
        /// </summary>
        private void ToggleShopShuffleImpl()
        {
            _undoRedoManager.NewAction(_mode.CreateChangeShopShuffleAction(!_mode.ShopShuffle));
        }

        /// <summary>
        /// Toggles the generic keys setting.
        /// </summary>
        private void ToggleGenericKeysImpl()
        {
            _undoRedoManager.NewAction(_mode.CreateChangeGenericKeysAction(!_mode.GenericKeys));
        }

        /// <summary>
        /// Toggles the take any locations setting.
        /// </summary>
        private void ToggleTakeAnyLocationsImpl()
        {
            _undoRedoManager.NewAction(_mode.CreateChangeTakeAnyLocationsAction(!_mode.TakeAnyLocations));
        }

        /// <summary>
        /// Toggles the key drop shuffle setting.
        /// </summary>
        private void ToggleKeyDropShuffleImpl()
        {
            _undoRedoManager.NewAction(_mode.CreateChangeKeyDropShuffleAction(!_mode.KeyDropShuffle));
        }
    }
}
