using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels;

/// <summary>
/// This class contains the mode settings popup control ViewModel data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class ModeSettingsVM : ViewModel, IModeSettingsVM
{
    private readonly IUndoRedoManager _undoRedoManager;
    
    private IMode Mode { get; }

    [Reactive]
    public bool PopupOpen { get; set; }

    [ObservableAsProperty]
    public bool BasicItemPlacement {get; }
    [ObservableAsProperty]
    public bool AdvancedItemPlacement { get; }

    [ObservableAsProperty]
    public bool MapShuffle { get; }
    [ObservableAsProperty]
    public bool CompassShuffle { get; }
    [ObservableAsProperty]
    public bool SmallKeyShuffle { get; }
    [ObservableAsProperty]
    public bool BigKeyShuffle { get; }

    [ObservableAsProperty]
    public bool StandardOpenWorldState { get; }
    [ObservableAsProperty]
    public bool InvertedWorldState { get; }

    [ObservableAsProperty]
    public bool NoneEntranceShuffle { get; }
    [ObservableAsProperty]
    public bool DungeonEntranceShuffle { get; }
    [ObservableAsProperty]
    public bool AllEntranceShuffle { get; }
    [ObservableAsProperty]
    public bool InsanityEntranceShuffle { get; }

    [ObservableAsProperty]
    public bool BossShuffle { get; }
    [ObservableAsProperty]
    public bool EnemyShuffle { get; }
    [ObservableAsProperty]
    public bool GuaranteedBossItems { get; }
    [ObservableAsProperty]
    public bool ShopShuffle { get; }
    [ObservableAsProperty]
    public bool GenericKeys { get; }
    [ObservableAsProperty]
    public bool TakeAnyLocations { get; }
    [ObservableAsProperty]
    public bool KeyDropShuffle { get; }

    public ReactiveCommand<Unit, Unit> OpenPopupCommand { get; }

    public ReactiveCommand<Unit, Unit> SetBasicItemPlacementCommand { get; }
    public ReactiveCommand<Unit, Unit> SetAdvancedItemPlacementCommand { get; }
    
    public ReactiveCommand<Unit, Unit> SetStandardOpenWorldStateCommand { get; }
    public ReactiveCommand<Unit, Unit> SetInvertedWorldStateCommand { get; }
    
    public ReactiveCommand<Unit, Unit> SetNoneEntranceShuffleCommand { get; }
    public ReactiveCommand<Unit, Unit> SetDungeonEntranceShuffleCommand { get; }
    public ReactiveCommand<Unit, Unit> SetAllEntranceShuffleCommand { get; }
    public ReactiveCommand<Unit, Unit> SetInsanityEntranceShuffleCommand { get; }

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
        _undoRedoManager = undoRedoManager;
        Mode = mode;
        
        OpenPopupCommand = ReactiveCommand.Create(OpenPopup);
        
        SetBasicItemPlacementCommand = ReactiveCommand.Create(() => ChangeItemPlacement(ItemPlacement.Basic));
        SetAdvancedItemPlacementCommand = ReactiveCommand
            .Create(() => ChangeItemPlacement(ItemPlacement.Advanced));
        
        SetStandardOpenWorldStateCommand = ReactiveCommand
            .Create(() => ChangeWorldState(WorldState.StandardOpen));
        SetInvertedWorldStateCommand = ReactiveCommand.Create(() => ChangeWorldState(WorldState.Inverted));
        
        SetNoneEntranceShuffleCommand = ReactiveCommand
            .Create(() => ChangeEntranceShuffle(EntranceShuffle.None));
        SetDungeonEntranceShuffleCommand = ReactiveCommand
            .Create(() => ChangeEntranceShuffle(EntranceShuffle.Dungeon));
        SetAllEntranceShuffleCommand = ReactiveCommand.Create(() => ChangeEntranceShuffle(EntranceShuffle.All));
        SetInsanityEntranceShuffleCommand = ReactiveCommand
            .Create(() => ChangeEntranceShuffle(EntranceShuffle.Insanity));
        
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

        this.WhenActivated(disposables =>
        {
            var itemPlacement = this
                .WhenAnyValue(x => x.Mode.ItemPlacement);

            itemPlacement
                .Select(x => x == ItemPlacement.Basic)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.BasicItemPlacement)
                .DisposeWith(disposables);
            itemPlacement
                .Select(x => x == ItemPlacement.Advanced)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.AdvancedItemPlacement)
                .DisposeWith(disposables);

            this.WhenAnyValue(x => x.Mode.MapShuffle)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.MapShuffle)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Mode.CompassShuffle)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.CompassShuffle)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Mode.SmallKeyShuffle)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.SmallKeyShuffle)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Mode.BigKeyShuffle)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.BigKeyShuffle)
                .DisposeWith(disposables);

            var worldState = this
                .WhenAnyValue(x => x.Mode.WorldState);

            worldState
                .Select(x => x == WorldState.StandardOpen)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.StandardOpenWorldState)
                .DisposeWith(disposables);
            worldState
                .Select(x => x == WorldState.Inverted)
                .ToPropertyEx(this, x => x.InvertedWorldState)
                .DisposeWith(disposables);

            var entranceShuffle = this
                .WhenAnyValue(x => x.Mode.EntranceShuffle);

            entranceShuffle
                .Select(x => x == EntranceShuffle.None)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.NoneEntranceShuffle)
                .DisposeWith(disposables);
            entranceShuffle
                .Select(x => x == EntranceShuffle.Dungeon)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.DungeonEntranceShuffle)
                .DisposeWith(disposables);
            entranceShuffle
                .Select(x => x == EntranceShuffle.All)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.AllEntranceShuffle)
                .DisposeWith(disposables);
            entranceShuffle
                .Select(x => x == EntranceShuffle.Insanity)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.InsanityEntranceShuffle)
                .DisposeWith(disposables);
            
            this.WhenAnyValue(x => x.Mode.BossShuffle)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.BossShuffle)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Mode.EnemyShuffle)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.EnemyShuffle)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Mode.GuaranteedBossItems)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.GuaranteedBossItems)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Mode.ShopShuffle)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ShopShuffle)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Mode.GenericKeys)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.GenericKeys)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Mode.TakeAnyLocations)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.TakeAnyLocations)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Mode.KeyDropShuffle)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.KeyDropShuffle)
                .DisposeWith(disposables);
        });
    }
    
    private void OpenPopup()
    {
        PopupOpen = true;
    }

    private void ChangeItemPlacement(ItemPlacement itemPlacement)
    {
        if (Mode.ItemPlacement != itemPlacement)
        {
            _undoRedoManager.NewAction(Mode.CreateChangeItemPlacementAction(itemPlacement));
        }
    }

    private void ChangeWorldState(WorldState worldState)
    {
        if (Mode.WorldState != worldState)
        {
            _undoRedoManager.NewAction(Mode.CreateChangeWorldStateAction(worldState));
        }
    }

    private void ChangeEntranceShuffle(EntranceShuffle entranceShuffle)
    {
        if (Mode.EntranceShuffle != entranceShuffle)
        {
            _undoRedoManager.NewAction(Mode.CreateChangeEntranceShuffleAction(entranceShuffle));
        }
    }

    private void ToggleMapShuffle()
    {
        _undoRedoManager.NewAction(Mode.CreateChangeMapShuffleAction(!Mode.MapShuffle));
    }

    private void ToggleCompassShuffle()
    {
        _undoRedoManager.NewAction(Mode.CreateChangeCompassShuffleAction(!Mode.CompassShuffle));
    }

    private void ToggleSmallKeyShuffle()
    {
        _undoRedoManager.NewAction(Mode.CreateChangeSmallKeyShuffleAction(!Mode.SmallKeyShuffle));
    }

    private void ToggleBigKeyShuffle()
    {
        _undoRedoManager.NewAction(Mode.CreateChangeBigKeyShuffleAction(!Mode.BigKeyShuffle));
    }

    private void ToggleBossShuffle()
    {
        _undoRedoManager.NewAction(Mode.CreateChangeBossShuffleAction(!Mode.BossShuffle));
    }

    private void ToggleEnemyShuffle()
    {
        _undoRedoManager.NewAction(Mode.CreateChangeEnemyShuffleAction(!Mode.EnemyShuffle));
    }

    private void ToggleGuaranteedBossItems()
    {
        _undoRedoManager.NewAction(Mode.CreateChangeGuaranteedBossItemsAction(!Mode.GuaranteedBossItems));
    }

    private void ToggleShopShuffle()
    {
        _undoRedoManager.NewAction(Mode.CreateChangeShopShuffleAction(!Mode.ShopShuffle));
    }

    private void ToggleGenericKeys()
    {
        _undoRedoManager.NewAction(Mode.CreateChangeGenericKeysAction(!Mode.GenericKeys));
    }

    private void ToggleTakeAnyLocations()
    {
        _undoRedoManager.NewAction(Mode.CreateChangeTakeAnyLocationsAction(!Mode.TakeAnyLocations));
    }

    private void ToggleKeyDropShuffle()
    {
        _undoRedoManager.NewAction(Mode.CreateChangeKeyDropShuffleAction(!Mode.KeyDropShuffle));
    }
}