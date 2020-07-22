using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.ViewModels.LargeItemControls;
using OpenTracker.ViewModels.SmallItemControls;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel class for the Items panel control.
    /// </summary>
    public class ItemsPanelControlVM : ViewModelBase
    {
        private readonly MainWindowVM _mainWindow;

        public bool ATItemsVisible =>
            Mode.Instance.SmallKeyShuffle;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mainWindow">
        /// The main window ViewModel parent class.
        /// </param>
        public ItemsPanelControlVM(MainWindowVM mainWindow)
        {
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            ModeSettings = new ModeSettingsControlVM();

            LargeItemControlVMFactory.GetLargeItemControlVMs(Items);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.HyruleCastle, this, HCItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.AgahnimTower, this, ATItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.EasternPalace, this, EPItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.DesertPalace, this, DPItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.TowerOfHera, this, ToHItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.PalaceOfDarkness, this, PoDItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.SwampPalace, this, SPItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.SkullWoods, this, SWItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.ThievesTown, this, TTItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.IcePalace, this, IPItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.MiseryMire, this, MMItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.TurtleRock, this, TRItems);
            SmallItemControlVMFactory.GetSmallItemControlVMs(LocationID.GanonsTower, this, GTItems);

            PropertyChanged += OnPropertyChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
            Mode.Instance.PropertyChanged += OnModeChanged;
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
            if (e.PropertyName == nameof(UIPanelOrientationDock))
            {
                this.RaisePropertyChanged(nameof(PanelMargin));
            }

            if (e.PropertyName == nameof(ItemsPanelOrientation))
            {
                this.RaisePropertyChanged(nameof(ItemsPanelHorizontalOrientation));
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
            if (e.PropertyName == nameof(MainWindowVM.UIPanelOrientationDock))
            {
                this.RaisePropertyChanged(nameof(UIPanelOrientationDock));
            }

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
    }
}
