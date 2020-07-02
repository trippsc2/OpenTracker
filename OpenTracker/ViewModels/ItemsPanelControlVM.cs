using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Items;
using OpenTracker.ViewModels.Bases;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class ItemsPanelControlVM : ViewModelBase
    {
        private readonly MainWindowVM _mainWindow;
        private readonly Mode _mode;

        public bool SmallKeyShuffle =>
            _mode.SmallKeyShuffle;
        public bool BigKeyShuffle =>
            _mode.BigKeyShuffle;
        public bool BossShuffle =>
            _mode.BossShuffle;

        public Dock UIPanelOrientationDock =>
            _mainWindow.UIPanelOrientationDock;

        public Thickness PanelMargin
        {
            get
            {
                return UIPanelOrientationDock switch
                {
                    Dock.Left => new Thickness(2, 0, 1, 2),
                    Dock.Bottom => new Thickness(2, 1, 0, 2),
                    Dock.Right => new Thickness(1, 0, 2, 2),
                    _ => new Thickness(2, 2, 0, 1),
                };
            }
        }

        public Orientation ItemsPanelOrientation
        {
            get
            {
                return _mainWindow.UIPanelDock switch
                {
                    Dock.Left => Orientation.Vertical,
                    Dock.Right => Orientation.Vertical,
                    _ => Orientation.Horizontal,
                };
            }
        }

        public bool ItemsPanelHorizontalOrientation =>
            ItemsPanelOrientation == Orientation.Horizontal;

        public ModeSettingsControlVM ModeSettings { get; }

        public KeyControlVM HCSmallKeys { get; }
        public KeyControlVM ATSmallKeys { get; }
        public KeyControlVM DPSmallKeys { get; }
        public KeyControlVM THSmallKeys { get; }
        public KeyControlVM PDSmallKeys { get; }
        public KeyControlVM SPSmallKeys { get; }
        public KeyControlVM SWSmallKeys { get; }
        public KeyControlVM TTSmallKeys { get; }
        public KeyControlVM IPSmallKeys { get; }
        public KeyControlVM MMSmallKeys { get; }
        public KeyControlVM TRSmallKeys { get; }
        public KeyControlVM GTSmallKeys { get; }

        public KeyControlVM EPBigKey { get; }
        public KeyControlVM DPBigKey { get; }
        public KeyControlVM THBigKey { get; }
        public KeyControlVM PDBigKey { get; }
        public KeyControlVM SPBigKey { get; }
        public KeyControlVM SWBigKey { get; }
        public KeyControlVM TTBigKey { get; }
        public KeyControlVM IPBigKey { get; }
        public KeyControlVM MMBigKey { get; }
        public KeyControlVM TRBigKey { get; }
        public KeyControlVM GTBigKey { get; }

        public DungeonChestControlVM HCItems { get; }
        public DungeonChestControlVM ATItems { get; }
        public DungeonChestControlVM EPItems { get; }
        public DungeonChestControlVM DPItems { get; }
        public DungeonChestControlVM THItems { get; }
        public DungeonChestControlVM PDItems { get; }
        public DungeonChestControlVM SPItems { get; }
        public DungeonChestControlVM SWItems { get; }
        public DungeonChestControlVM TTItems { get; }
        public DungeonChestControlVM IPItems { get; }
        public DungeonChestControlVM MMItems { get; }
        public DungeonChestControlVM TRItems { get; }
        public DungeonChestControlVM GTItems { get; }

        public PrizeControlVM EPPrize { get; }
        public PrizeControlVM DPPrize { get; }
        public PrizeControlVM THPrize { get; }
        public PrizeControlVM PDPrize { get; }
        public PrizeControlVM SPPrize { get; }
        public PrizeControlVM SWPrize { get; }
        public PrizeControlVM TTPrize { get; }
        public PrizeControlVM IPPrize { get; }
        public PrizeControlVM MMPrize { get; }
        public PrizeControlVM TRPrize { get; }

        public BossControlVM EPBoss { get; }
        public BossControlVM DPBoss { get; }
        public BossControlVM THBoss { get; }
        public BossControlVM PDBoss { get; }
        public BossControlVM SPBoss { get; }
        public BossControlVM SWBoss { get; }
        public BossControlVM TTBoss { get; }
        public BossControlVM IPBoss { get; }
        public BossControlVM MMBoss { get; }
        public BossControlVM TRBoss { get; }
        public BossControlVM GTBoss1 { get; }
        public BossControlVM GTBoss2 { get; }
        public BossControlVM GTBoss3 { get; }

        public ObservableCollection<ItemControlVM> Items { get; }

        public ItemsPanelControlVM(
            MainWindowVM mainWindow, AppSettings appSettings,
            Game game, UndoRedoManager undoRedoManager)
        {
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            if (undoRedoManager == null)
            {
                throw new ArgumentNullException(nameof(undoRedoManager));
            }

            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            _mode = game.Mode;

            ModeSettings = new ModeSettingsControlVM(game.Mode, undoRedoManager);

            HCSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.HCSmallKey]);
            ATSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.ATSmallKey]);
            DPSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.DPSmallKey]);
            THSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.ToHSmallKey]);
            PDSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.PoDSmallKey]);
            SPSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.SPSmallKey]);
            SWSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.SWSmallKey]);
            TTSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.TTSmallKey]);
            IPSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.IPSmallKey]);
            MMSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.MMSmallKey]);
            TRSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.TRSmallKey]);
            GTSmallKeys = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.GTSmallKey]);

            EPBigKey = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.EPBigKey]);
            DPBigKey = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.DPBigKey]);
            THBigKey = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.ToHBigKey]);
            PDBigKey = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.PoDBigKey]);
            SPBigKey = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.SPBigKey]);
            SWBigKey = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.SWBigKey]);
            TTBigKey = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.TTBigKey]);
            IPBigKey = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.IPBigKey]);
            MMBigKey = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.MMBigKey]);
            TRBigKey = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.TRBigKey]);
            GTBigKey = new KeyControlVM(undoRedoManager, appSettings, game,
                game.Items[ItemType.GTBigKey]);

            HCItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.HyruleCastle].Sections[0]);
            ATItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.AgahnimTower].Sections[0]);
            EPItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.EasternPalace].Sections[0]);
            DPItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.DesertPalace].Sections[0]);
            THItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.TowerOfHera].Sections[0]);
            PDItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.PalaceOfDarkness].Sections[0]);
            SPItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.SwampPalace].Sections[0]);
            SWItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.SkullWoods].Sections[0]);
            TTItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.ThievesTown].Sections[0]);
            IPItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.IcePalace].Sections[0]);
            MMItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.MiseryMire].Sections[0]);
            TRItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.TurtleRock].Sections[0]);
            GTItems = new DungeonChestControlVM(undoRedoManager, appSettings, game,
                game.Locations[LocationID.GanonsTower].Sections[0]);

            EPPrize = new PrizeControlVM(undoRedoManager, game,
                game.Locations[LocationID.EasternPalace].BossSections[0]);
            DPPrize = new PrizeControlVM(undoRedoManager, game,
                game.Locations[LocationID.DesertPalace].BossSections[0]);
            THPrize = new PrizeControlVM(undoRedoManager, game,
                game.Locations[LocationID.TowerOfHera].BossSections[0]);
            PDPrize = new PrizeControlVM(undoRedoManager, game,
                game.Locations[LocationID.PalaceOfDarkness].BossSections[0]);
            SPPrize = new PrizeControlVM(undoRedoManager, game,
                game.Locations[LocationID.SwampPalace].BossSections[0]);
            SWPrize = new PrizeControlVM(undoRedoManager, game,
                game.Locations[LocationID.SkullWoods].BossSections[0]);
            TTPrize = new PrizeControlVM(undoRedoManager, game,
                game.Locations[LocationID.ThievesTown].BossSections[0]);
            IPPrize = new PrizeControlVM(undoRedoManager, game,
                game.Locations[LocationID.IcePalace].BossSections[0]);
            MMPrize = new PrizeControlVM(undoRedoManager, game,
                game.Locations[LocationID.MiseryMire].BossSections[0]);
            TRPrize = new PrizeControlVM(undoRedoManager, game,
                game.Locations[LocationID.TurtleRock].BossSections[0]);

            EPBoss = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.EasternPalace].BossSections[0]);
            DPBoss = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.DesertPalace].BossSections[0]);
            THBoss = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.TowerOfHera].BossSections[0]);
            PDBoss = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.PalaceOfDarkness].BossSections[0]);
            SPBoss = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.SwampPalace].BossSections[0]);
            SWBoss = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.SkullWoods].BossSections[0]);
            TTBoss = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.ThievesTown].BossSections[0]);
            IPBoss = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.IcePalace].BossSections[0]);
            MMBoss = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.MiseryMire].BossSections[0]);
            TRBoss = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.TurtleRock].BossSections[0]);
            GTBoss1 = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.GanonsTower].BossSections[0]);
            GTBoss2 = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.GanonsTower].BossSections[1]);
            GTBoss3 = new BossControlVM(undoRedoManager, game,
                game.Locations[LocationID.GanonsTower].BossSections[2]);

            Items = new ObservableCollection<ItemControlVM>();

            for (int i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                switch ((ItemType)i)
                {
                    case ItemType.Sword:
                    case ItemType.Shield:
                    case ItemType.Aga:
                    case ItemType.TowerCrystals:
                    case ItemType.GanonCrystals:
                    case ItemType.Hookshot:
                    case ItemType.Mushroom:
                    case ItemType.Boots:
                    case ItemType.FireRod:
                    case ItemType.IceRod:
                    case ItemType.SmallKey:
                    case ItemType.Gloves:
                    case ItemType.Lamp:
                    case ItemType.Hammer:
                    case ItemType.Net:
                    case ItemType.Book:
                    case ItemType.Shovel:
                    case ItemType.Flippers:
                    case ItemType.Bottle:
                    case ItemType.CaneOfSomaria:
                    case ItemType.CaneOfByrna:
                    case ItemType.Cape:
                    case ItemType.Mirror:
                    case ItemType.HalfMagic:
                    case ItemType.MoonPearl:
                        {
                            Items.Add(new ItemControlVM(undoRedoManager, appSettings, game,
                                new IItem[1] { game.Items[(ItemType)i] }));
                        }
                        break;
                    case ItemType.Bow:
                    case ItemType.Boomerang:
                    case ItemType.Bomb:
                    case ItemType.Powder:
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                    case ItemType.Flute:
                        {
                            Items.Add(new ItemControlVM(undoRedoManager, appSettings, game,
                                new IItem[2] {
                                    game.Items[(ItemType)i],
                                    game.Items[(ItemType)(i + 1)]
                                }));
                        }
                        break;
                    case ItemType.Mail:
                        {
                            Items.Add(new ItemControlVM(undoRedoManager, appSettings, game,
                                new IItem[1] { game.Items[(ItemType)i] }));
                            Items.Add(new ItemControlVM(undoRedoManager, appSettings, game, null));
                        }
                        break;
                    default:
                        break;
                }
            }

            PropertyChanged += OnPropertyChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
            _mode.PropertyChanged += OnModeChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UIPanelOrientationDock))
            {
                this.RaisePropertyChanged(nameof(PanelMargin));
            }

            if (e.PropertyName == nameof(ItemsPanelOrientation))
            {
                this.RaisePropertyChanged(nameof(ItemsPanelHorizontalOrientation));
            }
        }

        private void OnMainWindowChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowVM.UIPanelOrientationDock))
            {
                this.RaisePropertyChanged(nameof(UIPanelOrientationDock));
            }

            if (e.PropertyName == nameof(MainWindowVM.UIPanelDock))
            {
                this.RaisePropertyChanged(nameof(ItemsPanelOrientation));
            }
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState))
            {
                UpdateSmallKeyShuffle();
            }

            if (e.PropertyName == nameof(Mode.DungeonItemShuffle))
            {
                UpdateSmallKeyShuffle();
                this.RaisePropertyChanged(nameof(BigKeyShuffle));
            }

            if (e.PropertyName == nameof(Mode.BossShuffle))
            {
                this.RaisePropertyChanged(nameof(BossShuffle));
            }
        }

        private void UpdateSmallKeyShuffle()
        {
            this.RaisePropertyChanged(nameof(SmallKeyShuffle));
        }
    }
}
