using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private readonly Game _game;

        public ObservableCollection<MapControlVM> Maps { get; }
        public ObservableCollection<ItemControlVM> Items { get; }
        public ObservableCollection<PinnedLocationControlVM> PinnedLocations { get; }
        public ObservableCollection<DungeonItemControlVM> HCItems { get; }
        public ObservableCollection<DungeonItemControlVM> ATItems { get; }
        public ObservableCollection<DungeonItemControlVM> SmallKeyItems { get; }
        public ObservableCollection<DungeonItemControlVM> BigKeyItems { get; }
        public ObservableCollection<DungeonPrizeControlVM> Prizes { get; }
        public ObservableCollection<BossControlVM> Bosses { get; }

        private AppSettingsVM _appSettings;
        public AppSettingsVM AppSettings
        {
            get => _appSettings;
            set => this.RaiseAndSetIfChanged(ref _appSettings, value);
        }

        private bool _modeSettingsPopupOpen;
        public bool ModeSettingsPopupOpen
        {
            get => _modeSettingsPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _modeSettingsPopupOpen, value);
        }

        private ItemPlacement _itemPlacement;
        public ItemPlacement ItemPlacement
        {
            get => _itemPlacement;
            set => this.RaiseAndSetIfChanged(ref _itemPlacement, value);
        }

        private DungeonItemShuffle _dungeonItemShuffle;
        public DungeonItemShuffle DungeonItemShuffle
        {
            get => _dungeonItemShuffle;
            set => this.RaiseAndSetIfChanged(ref _dungeonItemShuffle, value);
        }

        private WorldState _worldState;
        public WorldState WorldState
        {
            get => _worldState;
            set => this.RaiseAndSetIfChanged(ref _worldState, value);
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

        public MainWindowVM()
        {
            _appSettings = new AppSettingsVM();
            _game = new Game();

            ItemPlacement = _game.Mode.ItemPlacement.Value;
            DungeonItemShuffle = _game.Mode.DungeonItemShuffle.Value;
            WorldState = _game.Mode.WorldState.Value;
            EntranceShuffle = _game.Mode.EntranceShuffle.Value;
            BossShuffle = _game.Mode.BossShuffle.Value;
            EnemyShuffle = _game.Mode.EnemyShuffle.Value;

            Maps = new ObservableCollection<MapControlVM>();
            PinnedLocations = new ObservableCollection<PinnedLocationControlVM>();
            HCItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.HCSmallKey])
            };
            ATItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.ATSmallKey])
            };
            SmallKeyItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_appSettings, _game.Mode, null),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.DPSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.ToHSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.PoDSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.SPSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.SWSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.TTSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.IPSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.MMSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.TRSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.GTSmallKey])
            };
            BigKeyItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.EPBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.DPBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.ToHBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.PoDBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.SPBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.SWBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.TTBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.IPBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.MMBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.TRBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.GTBigKey])
            };
            Prizes = new ObservableCollection<DungeonPrizeControlVM>()
            {
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.EasternPalace].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.DesertPalace].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.TowerOfHera].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.PalaceOfDarkness].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.SwampPalace].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.SkullWoods].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.ThievesTown].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.IcePalace].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.MiseryMire].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.TurtleRock].BossSection)
            };
            Bosses = new ObservableCollection<BossControlVM>()
            {
                new BossControlVM(_game, _game.Locations[LocationID.EasternPalace].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.DesertPalace].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.TowerOfHera].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.PalaceOfDarkness].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.SwampPalace].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.SkullWoods].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.ThievesTown].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.IcePalace].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.MiseryMire].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.TurtleRock].BossSection)
            };

            for (int i = 0; i < Enum.GetValues(typeof(MapID)).Length; i++)
                Maps.Add(new MapControlVM(_appSettings, _game, this, (MapID)i));

            Items = new ObservableCollection<ItemControlVM>();

            for (int i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                switch ((ItemType)i)
                {
                    case ItemType.Bow:
                    case ItemType.Boomerang:
                    case ItemType.Bomb:
                    case ItemType.Powder:
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                    case ItemType.Flute:
                        Items.Add(new ItemControlVM(new Item[2] {
                            _game.Items[(ItemType)i], _game.Items[(ItemType)(i + 1)] }));
                        break;
                    case ItemType.MoonPearl:
                        Items.Add(new ItemControlVM(new Item[1] { _game.Items[(ItemType)i] }));
                        Items.Add(new ItemControlVM(null));
                        break;
                    case ItemType.Hookshot:
                    case ItemType.Mushroom:
                    case ItemType.TowerCrystals:
                    case ItemType.FireRod:
                    case ItemType.IceRod:
                    case ItemType.Shovel:
                    case ItemType.GanonCrystals:
                    case ItemType.Lamp:
                    case ItemType.Hammer:
                    case ItemType.Net:
                    case ItemType.Book:
                    case ItemType.Bottle:
                    case ItemType.CaneOfSomaria:
                    case ItemType.CaneOfByrna:
                    case ItemType.Cape:
                    case ItemType.Mirror:
                    case ItemType.GoMode:
                    case ItemType.Aga:
                    case ItemType.Gloves:
                    case ItemType.Boots:
                    case ItemType.Flippers:
                    case ItemType.HalfMagic:
                    case ItemType.Sword:
                    case ItemType.Shield:
                    case ItemType.Mail:
                        Items.Add(new ItemControlVM(new Item[1] { _game.Items[(ItemType)i] }));
                        break;
                    default:
                        break;
                }
            }

            this.PropertyChanged += OnPropertyChanged;
        }

        private void Update()
        {
            if (_game != null)
            {
                _game.Mode.ItemPlacement = ItemPlacement;
                _game.Mode.DungeonItemShuffle = DungeonItemShuffle;
                _game.Mode.WorldState = WorldState;
                _game.Mode.EntranceShuffle = EntranceShuffle;
                _game.Mode.BossShuffle = BossShuffle;
                _game.Mode.EnemyShuffle = EnemyShuffle;
            }

            if (_game.Mode.DungeonItemShuffle >= DungeonItemShuffle.MapsCompassesSmallKeys)
            {
                SmallKeyShuffle = true;

                if (_game.Mode.DungeonItemShuffle == DungeonItemShuffle.Keysanity)
                    BigKeyShuffle = true;
                else
                    BigKeyShuffle = false;
            }
            else
            {
                SmallKeyShuffle = false;
                BigKeyShuffle = false;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Update();
        }
    }
}
