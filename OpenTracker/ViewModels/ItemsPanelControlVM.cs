using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.ViewModels.Bases;
using OpenTracker.ViewModels.Factories;
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

        public bool ATItemsVisible =>
            _mode.SmallKeyShuffle;

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

        public ObservableCollection<LargeItemControlVMBase> Items { get; } =
            new ObservableCollection<LargeItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> HCItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> ATItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> EPItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> DPItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> ToHItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> PoDItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> SPItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> SWItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> TTItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> IPItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> MMItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> TRItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();
        public ObservableCollection<SmallItemControlVMBase> GTItems { get; } =
            new ObservableCollection<SmallItemControlVMBase>();

        public ItemsPanelControlVM(
            MainWindowVM mainWindow, AppSettings appSettings, UndoRedoManager undoRedoManager)
        {
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            if (undoRedoManager == null)
            {
                throw new ArgumentNullException(nameof(undoRedoManager));
            }

            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            _mode = Mode.Instance;

            ModeSettings = new ModeSettingsControlVM(Mode.Instance, undoRedoManager);

            LargeItemControlVMFactory.GetLargeItemControlVMs(
                undoRedoManager, appSettings, Items);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.HyruleCastle, HCItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.AgahnimTower, ATItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.EasternPalace, EPItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.DesertPalace, DPItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.TowerOfHera, ToHItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.PalaceOfDarkness, PoDItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.SwampPalace, SPItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.SkullWoods, SWItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.ThievesTown, TTItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.IcePalace, IPItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.MiseryMire, MMItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.TurtleRock, TRItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(
                undoRedoManager, appSettings, this, LocationID.GanonsTower, GTItems);

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
            if (e.PropertyName == nameof(Mode.WorldState) ||
                e.PropertyName == nameof(Mode.DungeonItemShuffle))
            {
                this.RaisePropertyChanged(nameof(ATItemsVisible));
            }
        }
    }
}
