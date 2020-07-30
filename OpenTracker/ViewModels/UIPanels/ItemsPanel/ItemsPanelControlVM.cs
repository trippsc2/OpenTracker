using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.ViewModels.UIPanels.ItemsPanel.LargeItems;
using OpenTracker.ViewModels.UIPanels.ItemsPanel.SmallItems;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels.UIPanels.ItemsPanel
{
    /// <summary>
    /// This is the ViewModel class for the Items panel control.
    /// </summary>
    public class ItemsPanelControlVM : ViewModelBase
    {
        private readonly MainWindowVM _mainWindow;
        private readonly UIPanelVM _uiPanel;

        public double Scale =>
            AppSettings.Instance.UIScale;
        public bool ATItemsVisible =>
            Mode.Instance.SmallKeyShuffle;
        public Thickness PanelMargin
        {
            get
            {
                return _uiPanel.UIPanelOrientationDock switch
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
        public ModeSettingsVM ModeSettings { get; }

        public ObservableCollection<LargeItemVMBase> Items { get; } =
            new ObservableCollection<LargeItemVMBase>();
        public ObservableCollection<SmallItemVMBase> HCItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> ATItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> EPItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> DPItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> ToHItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> PoDItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> SPItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> SWItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> TTItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> IPItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> MMItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> TRItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> GTItems { get; } =
            new ObservableCollection<SmallItemVMBase>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uiPanel">
        /// The UI panels ViewModel parent class.
        /// </param>
        /// <param name="mainWindow">
        /// The main window ViewModel parent class.
        /// </param>
        public ItemsPanelControlVM(UIPanelVM uiPanel, MainWindowVM mainWindow)
        {
            _uiPanel = uiPanel ?? throw new ArgumentNullException(nameof(uiPanel));
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            ModeSettings = new ModeSettingsVM();

            LargeItemControlVMFactory.GetLargeItemControlVMs(Items);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.HyruleCastle, this, HCItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.AgahnimTower, this, ATItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.EasternPalace, this, EPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.DesertPalace, this, DPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.TowerOfHera, this, ToHItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.PalaceOfDarkness, this, PoDItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.SwampPalace, this, SPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.SkullWoods, this, SWItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.ThievesTown, this, TTItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.IcePalace, this, IPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.MiseryMire, this, MMItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.TurtleRock, this, TRItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.GanonsTower, this, GTItems);

            PropertyChanged += OnPropertyChanged;
            _uiPanel.PropertyChanged += OnUIPanelChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
            Mode.Instance.PropertyChanged += OnModeChanged;
            AppSettings.Instance.PropertyChanged += OnAppSettingsChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on this class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemsPanelOrientation))
            {
                this.RaisePropertyChanged(nameof(ItemsPanelHorizontalOrientation));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the UIPanelVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnUIPanelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UIPanelVM.UIPanelOrientationDock))
            {
                this.RaisePropertyChanged(nameof(PanelMargin));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the MainWindowVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMainWindowChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowVM.UIPanelDock))
            {
                this.RaisePropertyChanged(nameof(ItemsPanelOrientation));
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
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState) ||
                e.PropertyName == nameof(Mode.DungeonItemShuffle))
            {
                this.RaisePropertyChanged(nameof(ATItemsVisible));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AppSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(Scale));
            }
        }
    }
}
